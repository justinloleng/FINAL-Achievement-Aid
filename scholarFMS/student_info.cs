using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace scholarFMS
{
    public partial class student_info : Form
    {
        MySqlConnection conn = new MySqlConnection("datasource=localhost; port=3306; username=root; password=");
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        String studentID;
        public student_info()
        {
            InitializeComponent();
        }

        public void SetInfo(string student_id, string lastName, string firstName, string middleName)
        {
            label_lastName.Text = "Last Name: " + lastName;
            label_firstName.Text = "First Name: " + firstName;
            label_midName.Text = "Middle Initial: " + middleName;
            studentID = student_id;
            //MessageBox.Show(studentID);
            forValidId(studentID);
            forSignature(studentID);
            loading();


        }

        private void forValidId(String student_id)
        {
            try
            {
                string sql = "SELECT scholar_ID, signature FROM im_etr.scholars WHERE student_ID = '" + student_id + "'";

                conn.Open();
                cmd = new MySqlCommand(sql, conn);

                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();


                if (reader.HasRows)
                {
                    byte[] img = (byte[])(reader[1]);

                    if (img == null)
                    {
                        pictureBox_id.Image = null;
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream(img);
                        pictureBox_id.Image = Image.FromStream(ms);
                    }
                    
                    conn.Close();
                }
                else
                {
                    
                    conn.Close();
                    MessageBox.Show("Image Does not Exist");

                }
                
                conn.Close();
            }
            catch (Exception exa)
            {
                conn.Close();

            }
        }

        private void forSignature(String student_id)
        {
            try
            {
                string sql = "SELECT scholar_ID, valid_id FROM im_etr.scholars WHERE student_ID = '" + student_id + "'";

                conn.Open();
                cmd = new MySqlCommand(sql, conn);

                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();


                if (reader.HasRows)
                {
                    byte[] img = (byte[])(reader[1]);

                    if (img == null)
                    {
                        pictureBox_signature.Image = null;
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream(img);
                        pictureBox_signature.Image = Image.FromStream(ms);
                    }
                    conn.Close();
                }
                else
                {
                    conn.Close();
                    MessageBox.Show("Image Does not Exist");

                }
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
            }
        }

        private void loading() 
        {
            conn.Open();
            cmd = new MySqlCommand("SELECT signature FROM im_etr.scholars WHERE student_ID = '" + studentID + "'", conn);

            object result = cmd.ExecuteScalar();
            
            string claimChecker = result != null ? result.ToString() : null;
            //MessageBox.Show(claimChecker);
            if (claimChecker == "")
            {
                //MessageBox.Show("gumagana");
                checkBox1.Enabled = false;
            }
            else {
                //MessageBox.Show("guamagaga");
                checkBox1.Enabled = true;
            }
            conn.Close();
            

        }

        


        private void student_info_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            string claimedStatus = "unclaimed";

            if (checkBox1.Checked)
            {
                claimedStatus = "claimed";
                checkBox1.Enabled = false;

                using (MySqlConnection conn = new MySqlConnection("datasource=localhost; port=3306; username=root; password="))
                {
                    conn.Open();
                    
                    string sql = "UPDATE im_etr.scholars SET claim_status = @claimedStatus, date_claimed = @currentDateTime WHERE student_id = @studentID";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    
                    cmd.Parameters.AddWithValue("@claimedStatus", claimedStatus);
                    cmd.Parameters.AddWithValue("@currentDateTime", DateTime.Now.ToString("yyyy-MM-dd")); 
                    cmd.Parameters.AddWithValue("@studentID", studentID);

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox_id_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            displayScholars displayPage = new displayScholars();
            this.Hide();
            displayPage.Show();
        }
    }
}
