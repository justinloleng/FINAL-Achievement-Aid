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

namespace scholarFMS
{
    public partial class displayScholars : Form
    {
        int price;
        public string dbConnect()
        {
            string conn = "server=localhost;user=root;password=;database=im_etr";
            return conn;
        }

        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataReader dr;
        MySqlDataAdapter da = new MySqlDataAdapter();
        public displayScholars()
        {
            InitializeComponent();
            conn = new MySqlConnection(dbConnect());
            loadRecord();
            loading();
        }

        public void loadRecord()
        {

            dataGridView1.Rows.Clear();
            conn.Open();
            cmd = new MySqlCommand("SELECT `scholar_ID`, `student_ID`, `Lname`, `Fname`, `Middle_initial`, `claim_status` FROM `scholars`", conn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr["student_ID"].ToString(), dr["Lname"].ToString(), dr["Fname"].ToString(), dr["middle_initial"].ToString(), dr["claim_status"].ToString());

            }
            dr.Close();
            conn.Close();

        }

        public void filterStatus()
        {
            if (filter.SelectedItem.ToString() == "All")
            {
                dataGridView1.Rows.Clear();
                conn.Open();
                cmd = new MySqlCommand("SELECT `scholar_ID`, `student_ID`, `Lname`, `Fname`, `Middle_initial`, `claim_status` FROM `scholars`", conn);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    dataGridView1.Rows.Add(dr["student_ID"].ToString(), dr["Lname"].ToString(), dr["Fname"].ToString(), dr["middle_initial"].ToString(), dr["claim_status"].ToString());
                }
                dr.Close();
                conn.Close();
            }
            else
            {
                dataGridView1.Rows.Clear();
                conn.Open();
                cmd = new MySqlCommand("SELECT `scholar_ID`, `student_ID`, `Lname`, `Fname`, `Middle_initial`, `claim_status` FROM `scholars` WHERE claim_status = '" + filter.SelectedItem.ToString() + "'", conn);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    dataGridView1.Rows.Add(dr["student_ID"].ToString(), dr["Lname"].ToString(), dr["Fname"].ToString(), dr["middle_initial"].ToString(), dr["claim_status"].ToString());
                }
                dr.Close();
                conn.Close();
            }
        }

        private void loading() 
        {
            label7.Text = dataGridView1.Rows.Count.ToString();
            string sqlFunds = "SELECT SUM(funds) FROM scholars";
            MySqlCommand cmdFunds = new MySqlCommand(sqlFunds, conn);

            try
            {
                conn.Open();

                // Assuming 'funds' is of numeric type (e.g., INT, DECIMAL)
                object result = cmdFunds.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    price = Convert.ToInt32(result);
                    label_fundss.Text = price.ToString();
                    // Now 'price' contains the sum of funds
                }
                else
                {
                    label_fundss.Text = "---";
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log the error, display a message)
                // You might want to throw an exception or set a default value for 'price' in case of an error
            }
            finally
            {
                conn.Close(); 
            }
        }

        private void displayScholars_Load(object sender, EventArgs e)
        {
           
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            conn.Open();
            cmd = new MySqlCommand("SELECT `scholar_ID`, `student_ID`, `Lname`, `Fname`, `Middle_initial`, `claim_status` FROM `scholars` WHERE Lname like '%" + textBox1.Text + "%' or Fname like '%" + textBox1.Text + "%' or middle_initial like '%" + textBox1.Text + "%' or student_ID like '%" + textBox1.Text + "%' or  scholar_ID like '%" + textBox1.Text + "%'", conn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr["student_ID"].ToString(), dr["Lname"].ToString(), dr["Fname"].ToString(), dr["middle_initial"].ToString(), dr["claim_status"].ToString());
            }
            dr.Close();
            conn.Close();
        }

        private void filter_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterStatus();
        }

        private void addScholars_Click(object sender, EventArgs e)
        {
            Form3 addScholars = new Form3();
            this.Hide();
            addScholars.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Funds funds = new Funds();
            this.Hide();
            funds.Show();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string student_id = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            string lastName = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            string firstName = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();
            string middleName = this.dataGridView1.CurrentRow.Cells[3].Value.ToString();


            // Create an instance of the Student_info form
            student_info student_info = new student_info();

            // Set the value of label_lastName
            student_info.SetInfo(student_id, lastName, firstName, middleName);

            // Show the Student_info form
            student_info.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            login loginPage = new login();
            this.Hide();
            loginPage.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you Want to Remove this Student?", "Remove Student", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string selectedStudent = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();

                conn.Open();
                cmd = new MySqlCommand("DELETE FROM `scholars` WHERE student_ID = @student_ID", conn);

                cmd.Parameters.AddWithValue("@student_ID", selectedStudent);

                MySqlDataReader reader = cmd.ExecuteReader();

                conn.Close();

                loadRecord();
                loading();
                
            }
            else 
            {
                MessageBox.Show("Student Not Removed", "Remove Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

           



        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Do You Want To Remove All Student?", "Remove All", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string selectedStudent = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();

                conn.Open();
                cmd = new MySqlCommand("DELETE FROM `scholars`", conn);

                MySqlDataReader reader = cmd.ExecuteReader();
                conn.Close();
                 
                loadRecord();
                loading();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
