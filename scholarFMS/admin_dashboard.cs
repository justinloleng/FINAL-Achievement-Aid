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
    public partial class admin_dashboard : Form
    {
        MySqlConnection conn = new MySqlConnection("datasource=localhost; port=3306; username=root; password=");
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        public admin_dashboard()
        {
            InitializeComponent();
            display();
        }
        private void display()
        {
            conn.Open();
            DataTable dt = new DataTable();
            adapter = new MySqlDataAdapter("SELECT student_ID, Lname, Fname, Middle_initial FROM im_etr.scholars", conn);
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();

        }

        private void admin_dashboard_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string student_id = this.dataGridView1.CurrentRow.Cells["student_ID"].Value.ToString();
            string lastName = this.dataGridView1.CurrentRow.Cells["Lname"].Value.ToString();
            string firstName = this.dataGridView1.CurrentRow.Cells["Fname"].Value.ToString();
            string middleName = this.dataGridView1.CurrentRow.Cells["Middle_initial"].Value.ToString();


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
    }
}
