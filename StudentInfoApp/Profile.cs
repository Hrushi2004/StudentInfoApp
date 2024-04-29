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
    public partial class Profile : Form
    {
        private long id;
        string firstname;
        string lastname;
        string mail;
        private Session session;
        public Profile(Session session)
        {
            InitializeComponent();
            this.session = session;
            session.getdetails();
            id = session.getId();
            firstname = session.getFname();
            lastname = session.getLastname();
            mail = session.getMail();
            label1.Text = id.ToString();
            label2.Text = firstname + " " + lastname;
            label3.Text = mail;
        }

        private void Profile_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
