using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace StudentInfoApp
{
    public partial class AdminLogin : Form
    {
        public AdminLogin()
        {
            InitializeComponent();
            InitializeMyControl();
        }
        private void InitializeMyControl()
        {
           
            
            button1.Text = "🔒"; 
            button1.Font = new Font("Segoe UI Emoji", 10f); 
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0; 
            button1.Cursor = Cursors.Hand;
            button1.Click += EmojiButton_Click;
           
        }

        private void EmojiButton_Click(object sender, EventArgs e)
        {
            if (textBox2.PasswordChar == '\0')
            {
                textBox2.PasswordChar = '*';
            }
            else
            {
                textBox2.PasswordChar = '\0';
            }
            if (textBox2.PasswordChar == '\0')
            {
                button1.Text = "🔓"; 
            }
            else
            {
                button1.Text = "🔒";
            }
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void ShowErrorMessage(string message)
        {
           
            label4.Text = message;
           
            label4.ForeColor = Color.Black;
            label4.BackColor = Color.Red;
            label4.Padding = new Padding(5); 
           
           
            this.Controls.Add(label4);
            Timer timer = new Timer();
            timer.Interval = 5000; 
            timer.Tick += (sender, e) =>
            {
                this.Controls.Remove(label4);
                timer.Stop(); 
                timer.Dispose();
            };
            timer.Start();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            if (username != "Admin")
            {
                //MessageBox.Show("Bro, you are not admin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
                ShowErrorMessage("Bro, you are not admin.");
                return;
            }

            if (password != "admin@123")
            {
                //MessageBox.Show("Wrong password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ShowErrorMessage("Wrong password.");
                return; 
            }
            else
            {
                this.Hide();
                AdminModule adminModuleForm = new AdminModule();
                adminModuleForm.ShowDialog();
                this.Close();
            }
        }
    }
}
