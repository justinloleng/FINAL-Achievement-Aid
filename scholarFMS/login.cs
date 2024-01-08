
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace scholarFMS
{
    public partial class login : Form
    {
        MySqlConnection conn = new MySqlConnection("datasource=localhost; port=3306; username=root; password=");
        public login()
        {
            InitializeComponent();
            pass.PasswordChar = '*';
        }
        //INSERT INTO `stud_acc` (`acc_id`, `student_ID`, `password`) VALUES(NULL, '21-UR-0209', 'jloleng10');
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string idnum = this.idnum.Text.Trim();
            string password = this.pass.Text.Trim();

            if (this.idnum.Text.Trim().Equals("") || this.pass.Text.Trim().Equals(""))
            {
                MessageBox.Show("Please enter your username and/or password.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string select = "SELECT * FROM im_etr.stud_acc WHERE student_ID = @student_ID AND password = @password UNION SELECT * FROM im_etr.admin_acc WHERE faculty_ID = @faculty_ID AND password = @password"; ;
            using (MySqlCommand cmd = new MySqlCommand(select, conn))
            {
                cmd.Parameters.AddWithValue("@student_ID", this.idnum.Text.Trim());
                cmd.Parameters.AddWithValue("@password", this.pass.Text.Trim());
                cmd.Parameters.AddWithValue("@faculty_ID", this.idnum.Text.Trim());
                DataTable dt = new DataTable();

                try
                {
                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    dt.Load(reader);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    conn.Close();
                }

                if (idnum.Length == 10 && dt.Rows.Count > 0)
                {
                    Form2 studform = new Form2();
                    studform.Show();
                    this.Hide();
                }
                else if (idnum.Length == 11 && dt.Rows.Count > 0)
                {
                    Form3 admin = new Form3();
                    admin.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Incorrect username and/or password.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void createAccBtn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            regisAdmin register = new regisAdmin();
            this.Hide();
            register.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            registrationcs studentReg = new registrationcs();
            this.Hide();
            studentReg.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
              
                pass.UseSystemPasswordChar = false;
                pass.PasswordChar = '\0';
            }
            else
            {
                
                pass.PasswordChar = '*';
                pass.UseSystemPasswordChar = true;
                
            }
        }
    }
}
