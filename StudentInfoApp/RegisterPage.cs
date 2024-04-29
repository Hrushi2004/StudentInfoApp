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

namespace StudentInfoApp
{
    public partial class RegisterPage : Form
    {
        public RegisterPage()
        {
            InitializeComponent();
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        private void SendEmail(string toEmail, int otp, string fname, long id)
        {
            string frommail = "hrushikesh2004.k@gmail.com";
            string pass = "pylf zpcn ogbs zqmu";
            MailMessage message = new MailMessage();
            message.From = new MailAddress(frommail);
            message.Subject = "Student Portal verification";
            message.To.Add(new MailAddress(toEmail));
            message.Body = $"<html><body>Hello {fname}!<br>Your OTP for the verification is<br>OTP: {otp}</body></html>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(frommail, pass),
                EnableSsl = true,
                
            };
            smtpClient.Send(message);
            MessageBox.Show("OTP sent to mail id");
            OTPVerificationForm otpForm = new OTPVerificationForm(otp);
            otpForm.SaveDetails(id, toEmail);
            otpForm.ShowDialog();
            this.Close();
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
        private void button1_Click(object sender, EventArgs e)
        {
            long id;
            if (!long.TryParse(textBox1.Text, out id))
            {
                MessageBox.Show("Invalid ID. Please enter a valid ID number.");
                return;
            }

            string email = textBox2.Text;
            if (!IsValidEmail(email))
            {
                MessageBox.Show("Invalid email. Please enter a valid email address.");
                return;
            }
            if (!idAlreadyExists(id))
            {
                MessageBox.Show("User already exists please login to continue");
            }
            else { 
            string connectionString = "Server=localhost;Port=5432;Database=VisualProgramming;User Id=postgres;Password=Hem@kumari9;";


            string firstName = "";
            string selectCommand = "SELECT COUNT(*) FROM student WHERE id = @id";

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(selectCommand, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        if (count == 0)
                        {
                            MessageBox.Show("Invalid ID. Please enter a valid College ID number.");
                        }
                        else
                        {
                            string selectFirstNameCommand = "SELECT firstName FROM student WHERE id = @id";
                            using (NpgsqlCommand firstNameCmd = new NpgsqlCommand(selectFirstNameCommand, conn))
                            {
                                firstNameCmd.Parameters.AddWithValue("@id", id);
                                firstName = Convert.ToString(firstNameCmd.ExecuteScalar());
                            }


                            Random random = new Random();
                            int otp = random.Next(1000, 10000);
                            SendEmail(email, otp, firstName, id);

                           
                        }
                    }
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            LoginPage LoginForm = new LoginPage();
            LoginForm.ShowDialog();
            this.Close();
        }
    }
}
