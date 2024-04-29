using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentInfoApp
{
    public partial class OTPVerificationForm : Form
    {
        private int otp;
        private long id;
        private string email;
        public void SaveDetails(long id, string email)
        {
            this.id = id;
            this.email = email;
        }
       

        public OTPVerificationForm(int otp)
        {
            InitializeComponent();
            this.otp = otp;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int userValue = Int32.Parse(textBox1.Text);
            if(userValue == otp ) 
            {
                MessageBox.Show("Mail Verification succesfully completed");
                string connectionString = "Server=localhost;Port=5432;Database=VisualProgramming;User Id=postgres;Password=Hem@kumari9;";

                string selectStudentQuery = "SELECT firstname, lastname FROM student WHERE id = @id";

                string insertStudentPersonalQuery = "INSERT INTO studentPersonal (id, mail, firstname, lastname) VALUES (@id, @mail, @firstName, @lastName)";

                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string firstname, lastname;
                    using (NpgsqlCommand selectCmd = new NpgsqlCommand(selectStudentQuery, conn))
                    {
                        selectCmd.Parameters.AddWithValue("@id", id);
                        using (NpgsqlDataReader reader = selectCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                firstname = reader.GetString(0);
                                lastname = reader.GetString(1);
                            }
                            else
                            {
                                MessageBox.Show("Failed to fetch", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }

                    using (NpgsqlCommand insertCmd = new NpgsqlCommand(insertStudentPersonalQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@id", id);
                        insertCmd.Parameters.AddWithValue("@mail", email);
                        insertCmd.Parameters.AddWithValue("@firstName", firstname);
                        insertCmd.Parameters.AddWithValue("@lastName", lastname);

                        int rowsAffected = insertCmd.ExecuteNonQuery();
                        //MessageBox.Show("Succesfully inserted Data", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Session session = new Session(id);
                        this.Hide();
                        Profile profile = new Profile(session);
                        profile.ShowDialog();
                        this.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Invalid OTP", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
