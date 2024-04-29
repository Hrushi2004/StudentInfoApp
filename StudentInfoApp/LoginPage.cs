using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace StudentInfoApp
{
    public partial class LoginPage : Form
    {
        private int otp;
        private long Id;
        private void SendEmail(string toEmail, int otp, string fname, long id)
        {
            string frommail = "hrushikesh2004.k@gmail.com";
            string pass = "pylf zpcn ogbs zqmu";
            MailMessage message = new MailMessage();
            message.From = new MailAddress(frommail);
            message.Subject = "One-Time Password (OTP) for Student Login";
            message.To.Add(new MailAddress(toEmail));
            message.Body = $"<html><body><p>Dear {fname}!</p><br><p>We have noticed that you are trying to login to your student portal</p><br>Your OTP for the login is<br>OTP: {otp}<br>Don't Share the OTP with anyone because this can give access to sensitive information<br><p>If not done by you don't worry em avvadu le </p></body></html>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(frommail, pass),
                EnableSsl = true,

            };
            smtpClient.Send(message);
            MessageBox.Show("OTP sent to mail id");
        }
        private bool idAlreadyExists(long id)
        {
            string connectionString = "Server=localhost;Port=5432;Database=VisualProgramming;User Id=postgres;Password=Hem@kumari9;";

            string query = "SELECT COUNT(*) FROM studentPersonal WHERE id = @id";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    return count == 0;
                }
            }
        }
            public LoginPage()
        {
            InitializeComponent();
            
        }
        public Tuple<string, string> GetEmailAndFirstName(long id)
        {
            string connectionString = "Server=localhost;Port=5432;Database=VisualProgramming;User Id=postgres;Password=Hem@kumari9;";

            string query = "SELECT mail, firstname FROM studentpersonal WHERE id = @id";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string email = reader.GetString(0);
                            string firstName = reader.GetString(1);
                            return Tuple.Create(email, firstName);
                        }
                        else
                        {
                            throw new Exception("ID not found in the studentPersonal table.");
                        }
                    }
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            long id = Int64.Parse(textBox1.Text);
            if(!idAlreadyExists(id) )
            {
                this.Id = id;
                var result = GetEmailAndFirstName(id);
                
                string email = result.Item1;
                string firstName = result.Item2;

                Random random = new Random();
                int key = random.Next(1000, 10000);
                this.otp = key;
                SendEmail(email, key, firstName, id);
                this.Hide();
                LoginOTP loginOTP = new LoginOTP(otp, id); 
                loginOTP.ShowDialog();
                this.Close();

            }
            else
            {
                MessageBox.Show("ID not found please register to continue", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            RegisterPage adminModuleForm = new RegisterPage();
            adminModuleForm.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
