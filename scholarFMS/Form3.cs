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
    public partial class Form3 : Form
    {
        int price;
        connection init = new connection();
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            init.connect();

            //para sa sum sa display kay janela
            //string sql = "SELECT SUM(funds) from scholars";
            //MySqlCommand cmd = new MySqlCommand(sql, init.con);
            //price = Convert.ToInt32(cmd.ExecuteScalar());
            //label_funds.Text = price.ToString();

            for (int a = 20; a <= 30; a++)
            {
                this.schoolYear.Items.Add("20" + a.ToString() + "-20" + (a + 1).ToString());
            }

            this.schoolYear.SelectedIndex = 3;
            this.semester.SelectedIndex = 0;
            this.scholarship.SelectedIndex = 0;

            init.disconnect();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            admin_dashboard admin = new admin_dashboard();

            admin.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            init.connect();
            this.addBtn.Enabled = true;
            string search = this.searchBox.Text;
            string sql = "SELECT students.Lname, students.Fname, students.middle_initial FROM students WHERE '" + this.searchBox.Text + "' = students.student_ID";

            MySqlCommand cmd = new MySqlCommand(sql, init.con);
            MySqlDataReader dr = cmd.ExecuteReader();
            dr.Read();

            if (dr.HasRows)
            {
                string Lname = dr.GetString(0);
                string Fname = dr.GetString(1);
                string Minitial = dr.GetString(2);


                this.Lname.Text = Lname;
                this.Fname.Text = Fname;
                this.Minitial.Text = Minitial;
            }
            else
            {
                this.Lname.Text = "None";
                this.Fname.Text = "None";
                this.Minitial.Text = "None";
            }

            init.disconnect();
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            int svalueStatus = scholarship.SelectedIndex;

            if (scholarship.SelectedItem != null)
            {
                if (svalueStatus == 0)
                {
                    price = 1000;
                }
                else
                {
                    price = 1500;
                }
            }

            init.connect();
            string checker = "SELECT COUNT(*) FROM scholars WHERE student_ID = '" + this.searchBox.Text + "'";
            MySqlCommand checkcmd = new MySqlCommand(checker, init.con);
            int same = Convert.ToInt32(checkcmd.ExecuteScalar());

            if (same > 0)
            {
                MessageBox.Show("Scholar has already been added");
                this.Lname.Text = "";
                this.Fname.Text = "";
                this.Minitial.Text = "";
                this.searchBox.Text = "";
                this.schoolYear.SelectedIndex = 3;
                this.semester.SelectedIndex = 0;
                this.scholarship.SelectedIndex = 0;
            }
            else
            {
                if (string.IsNullOrEmpty(this.searchBox.Text))
                {
                    MessageBox.Show("No Student Found");
                    this.Lname.Text = "";
                    this.Fname.Text = "";
                    this.Minitial.Text = "";
                    this.searchBox.Text = "";
                    this.schoolYear.SelectedIndex = 3;
                    this.semester.SelectedIndex = 0;
                    this.scholarship.SelectedIndex = 0;
                }

                else if (this.Lname.Text != "None")
                {
                    checkcmd.Dispose();

                    string id = this.searchBox.Text;
                    string lname = this.Lname.Text;
                    string fname = this.Fname.Text;
                    string mi = this.Minitial.Text;
                    string syear = this.schoolYear.SelectedItem.ToString();
                    string sem = this.semester.SelectedItem.ToString();
                    string type = this.scholarship.SelectedItem.ToString();
                    string fundss = price.ToString();

                    
                    string sql = "INSERT INTO scholars(student_ID, Lname, Fname, Middle_initial, type, funds, school_year, semester, claim_status) " +
                                 "VALUES(@id, @lname, @fname, @mi, @type, @funds, @syear, @sem, 'unclaimed')";

                    MySqlCommand cmd = new MySqlCommand(sql, init.con);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@lname", lname);
                    cmd.Parameters.AddWithValue("@fname", fname);
                    cmd.Parameters.AddWithValue("@mi", mi);
                    cmd.Parameters.AddWithValue("@type", type);
                    cmd.Parameters.AddWithValue("@funds", fundss);
                    cmd.Parameters.AddWithValue("@syear", syear);
                    cmd.Parameters.AddWithValue("@sem", sem);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    MessageBox.Show("Scholar has been added");
                    this.Lname.Text = "";
                    this.Fname.Text = "";
                    this.Minitial.Text = "";
                    this.searchBox.Text = "";
                    this.schoolYear.SelectedIndex = 3;
                    this.semester.SelectedIndex = 0;
                    this.scholarship.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("No Student Found");
                    this.Lname.Text = "";
                    this.Fname.Text = "";
                    this.Minitial.Text = "";
                    this.searchBox.Text = "";
                    this.schoolYear.SelectedIndex = 3;
                    this.semester.SelectedIndex = 0;
                    this.scholarship.SelectedIndex = 0;
                }

            }


            this.addBtn.Enabled = false;

            init.disconnect();
        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void funds_Click(object sender, EventArgs e)
        {
            Funds funds = new Funds();
            this.Hide();
            funds.Show();
        }

        private void addScholars_Click(object sender, EventArgs e)
        {

        }

        private void scholarsList_Click(object sender, EventArgs e)
        {
            displayScholars scholarsList = new displayScholars();
            this.Hide();
            scholarsList.Show();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            login loginPage = new login();
            this.Hide();
            loginPage.Show();
        }
    }
}
