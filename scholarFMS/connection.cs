using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace scholarFMS
{
    internal class connection
    {
        private string server = Properties.Settings.Default.server;
        private string port = Properties.Settings.Default.port;
        private string username = Properties.Settings.Default.username;
        private string password = Properties.Settings.Default.password;
        private string database = Properties.Settings.Default.database;

        public MySqlConnection con = new MySqlConnection();

        public void connect()
        {
            string constring = "server=" + server + "; port=" + port + "; username=" + username + "; password=" + password + "; database=" + database + "; charset=utf8";
            con = new MySqlConnection(constring);
            con.Open();
        }

        public void disconnect()
        {
            con.Close();
        }
    }
}
