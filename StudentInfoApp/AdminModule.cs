using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace StudentInfoApp
{
    public partial class AdminModule : Form
    {
        public AdminModule()
        {
            InitializeComponent();
            Add.Visible = false;
            Course.Visible = false;

            Details.Visible = false;

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Add.Visible = false;
            Course.Visible = false;
            Details.Visible = false;

            Details.Visible = true;
            NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Port=5432;Database=VisualProgramming; User Id=postgres;Password=Hem@kumari9;");
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from student";
            NpgsqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(reader);
                dataGridView1.DataSource = dt;
            }
            cmd.Dispose();
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string[] branchOptions = { "CSE-H", "CSE", "ECE", "EEE", "IOT", "AI&DS", "BBA", "BT", "ME", "CE" };

            for (int i = 0; i < branchOptions.Length; i++)
            {
                string branch1 = branchOptions[i];
                comboBox1.Items.Add(branch1);
            }
            Add.Visible = false;
            Course.Visible = false;
            Details.Visible = false;

            Add.Visible = true;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Add.Visible = false;
            Course.Visible = false;
            Details.Visible = false;

            Course.Visible = true;
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            

        }

        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void Course_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            string connectionString = "Server=localhost;Port=5432;Database=VisualProgramming;User Id=postgres;Password=Hem@kumari9;";

            string insertCommand = "INSERT INTO student (id, firstname, lastname, branch) VALUES (@id, @fname, @lname, @branch)";


            try
            {
                long id = Int64.Parse(textBox1.Text);
                string fname = textBox2.Text;
                string lname = textBox3.Text;
                string branch = comboBox1.Text;
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    using (NpgsqlCommand cmd = new NpgsqlCommand(insertCommand, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@fname", fname);
                        cmd.Parameters.AddWithValue("@lname", lname);
                        cmd.Parameters.AddWithValue("@branch", branch);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Succesfully inserted Data", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to insert", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid ID. Please enter a valid integer ID.");
            }
        }
    }
}
