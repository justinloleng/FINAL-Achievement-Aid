using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace scholarFMS
{
    public partial class regisAdmin : Form
    {
        public string dbConnect()
        {
            string conn = "server=localhost;user=root;password=;database=im_etr";
            return conn;
        }

        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataReader dr;
        MySqlDataAdapter da = new MySqlDataAdapter();
        int i = 0;
        public regisAdmin()
        {
            InitializeComponent();
            txt_password.PasswordChar = '*';

           conn = new MySqlConnection(dbConnect());
        }

        public void clear()
        {
            txt_adminID.Clear();
            txt_password.Clear();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txt_adminID.Text == string.Empty || txt_password.Text == string.Empty)
            {
                MessageBox.Show("Warning : Required Fill Filled ?", "CURD",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                // Check if admin ID already exists
                if (IsAdminIDExists(txt_adminID.Text))
                {
                    MessageBox.Show("Admin ID already exists. Please choose a different one.", "CURD",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

               
                conn.Open();
                cmd = new MySqlCommand("INSERT INTO `admin_acc`(`faculty_ID`, `password`) VALUES (@faculty_ID,@password)", conn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@faculty_ID", txt_adminID.Text);
                cmd.Parameters.AddWithValue("@password", txt_password.Text);

                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Admin Added Successfully!", "CURD",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Admin Addition Failed!", "CURD",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                conn.Close();
                clear();
            }
        }

        private bool IsAdminIDExists(string adminID)
        {
            
            MySqlCommand checkCmd = new MySqlCommand("SELECT COUNT(*) FROM `admin_acc` WHERE `faculty_ID` = @faculty_ID", conn);
            checkCmd.Parameters.AddWithValue("@faculty_ID", adminID);

            conn.Open();
            int count = Convert.ToInt32(checkCmd.ExecuteScalar());
            conn.Close();

            return count > 0;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            login loginPage = new login();
            this.Hide();
            loginPage.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void regisAdmin_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                // Debug point or output

                txt_password.UseSystemPasswordChar = false;
                txt_password.PasswordChar = '\0';
            }
            else
            {
                // Debug point or output
                txt_password.PasswordChar = '*';
                txt_password.UseSystemPasswordChar = true;
            }

        }

        private void txt_adminID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txt_adminID.Text.Length >= 11 && e.KeyChar != '\b') // '\b' is the backspace character
            {
                e.Handled = true; // Cancel the input
                MessageBox.Show("The length must be 10 characters.", "Invalid Length", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
