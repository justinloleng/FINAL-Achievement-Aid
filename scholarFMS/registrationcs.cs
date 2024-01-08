using MySql.Data.MySqlClient;
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

namespace scholarFMS
{
    public partial class registrationcs : Form
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
        public registrationcs()
        {
            InitializeComponent();
            InitializeTextBox();
            textBox_password.PasswordChar = '*';
            conn = new MySqlConnection(dbConnect());
        }
        private void InitializeTextBox()
        {
            textBox_studID.Click +=  textBox_studID_TextChanged;
        }

        public void clear()
        {
            textBox_studID.Clear();
            textBox_password.Clear();
        }

        private void registrationcs_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox_studID.Text == string.Empty || textBox_password.Text == string.Empty)
            {
                MessageBox.Show("Warning : Required Fill Filled ?", "CURD",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                // Check if student ID already exists
                if (IsStudentIDExists(textBox_studID.Text))
                {
                    MessageBox.Show("Student ID already exists. Please choose a different one.", "CURD",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                conn.Open();
                cmd = new MySqlCommand("INSERT INTO `stud_acc`(`student_ID`, `password`) VALUES (@student_ID,@password)", conn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@student_ID", textBox_studID.Text);
                cmd.Parameters.AddWithValue("@password", textBox_password.Text);

                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Account Added Successfully!", "CURD",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Account Addition Failed!", "CURD",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                conn.Close();
                clear();
            }
        }

        private bool IsStudentIDExists(string studentID)
        {
            
            MySqlCommand checkCmd = new MySqlCommand("SELECT COUNT(*) FROM `stud_acc` WHERE `student_ID` = @student_ID", conn);
            checkCmd.Parameters.AddWithValue("@student_ID", studentID);

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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
               

                textBox_password.UseSystemPasswordChar = false;
                textBox_password.PasswordChar = '\0';
            }
            else
            {
              
                textBox_password.PasswordChar = '*';
                textBox_password.UseSystemPasswordChar = true;
            }
        }

        private void textBox_studID_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox_studID_MouseClick(object sender, MouseEventArgs e)
        {
           
        }

        private void textBox_studID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textBox_studID.Text.Length >= 10 && e.KeyChar != '\b') // '\b' is the backspace character
            {
                e.Handled = true; // Cancel the input
                MessageBox.Show("The length must be 10 characters.", "Invalid Length", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
