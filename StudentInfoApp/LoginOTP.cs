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
    public partial class LoginOTP : Form
    {
        private int otp;
        private long id;
        public LoginOTP(int otp, long id)
        {
            InitializeComponent();
            this.otp = otp;
            this.id = id;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int userValue = Int32.Parse(textBox2.Text);
            if (userValue == otp)
            {
                MessageBox.Show("OTP Verification succesfully completed");
                Session session = new Session(id);
                this.Hide();
                Profile profile = new Profile(session);
                profile.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid OTP", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
