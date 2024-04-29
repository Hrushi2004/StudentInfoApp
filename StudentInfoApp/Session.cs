using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfoApp
{
    public  class Session
    {
        private long id;
        string firstname;
        string lastname;
        string mail;
        public Session(long id) 
        {
            this.id = id;
        }
        public void setDetails(string firstname, string lastname, string mail) 
        {
            this.firstname = firstname;
            this.lastname = lastname;
            this.mail = mail;
        }
        public long getId()
        {
            return id;
        }
        public string getFname() 
        { 
            return firstname;
        }
        public string getLastname()
        {
            return lastname;
        }
        public string getMail() 
        {
            return mail;
        }
        public void getdetails()
        {
            string connectionString = "Server=localhost;Port=5432;Database=VisualProgramming;User Id=postgres;Password=Hem@kumari9;";
            string query = "SELECT firstname, lastname, mail FROM studentpersonal WHERE id = @id";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (var cmd = new NpgsqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string firstname = reader.GetString(0);
                            string lastname = reader.GetString(1);
                            string mail = reader.GetString(2);
                            setDetails(firstname, lastname, mail); 
                        }
                    }
                }
            }
        }
    }
}
