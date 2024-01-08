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
    public partial class Funds : Form
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
        connection init = new connection();

        int dlFund = 0;
        int plFund = 0;
        int price = 0;
       
        public Funds()
        {
            InitializeComponent();
            conn = new MySqlConnection(dbConnect());
            loadRecord();
            loading();
        }

        public void loadRecord()
        {

            fundsList.Rows.Clear();
            init.connect();
            MySqlCommand cmd = new MySqlCommand("SELECT student_ID, Lname, Fname, type, funds, date_claimed FROM scholars WHERE claim_status = 'claimed'", init.con);
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                fundsList.Rows.Add(dr["student_ID"].ToString(), dr["Lname"].ToString(), dr["Fname"].ToString(), dr["type"].ToString(), dr["funds"].ToString(), dr["date_claimed"].ToString());

            }
            dr.Close();
            init.disconnect();

        }

        private void loading() 
        {
            init.connect();

            string sqlchecking = "SELECT COUNT(funds) from scholars";
            MySqlCommand cmdchecking = new MySqlCommand(sqlchecking, init.con);
            int checking = Convert.ToInt32(cmdchecking.ExecuteScalar());
            if (checking == 0)
            {
                return;
            }
            else
            {
                string sqlFunds = "SELECT SUM(funds) from scholars";
                MySqlCommand cmdFunds = new MySqlCommand(sqlFunds, init.con);
                price = Convert.ToInt32(cmdFunds.ExecuteScalar());
            }



            string checkerDl = "SELECT COUNT(*) FROM scholars WHERE type = 'Dean Lister' AND claim_status = 'claimed'";
            MySqlCommand dlcmd = new MySqlCommand(checkerDl, init.con);
            int dlCount = Convert.ToInt32(dlcmd.ExecuteScalar());

            string checkerPl = "SELECT COUNT(*) FROM scholars WHERE type = 'President Lister' AND claim_status = 'claimed'";
            MySqlCommand plcmd = new MySqlCommand(checkerPl, init.con);
            int plCount = Convert.ToInt32(plcmd.ExecuteScalar());

            init.disconnect();
            for (int a = 0; a < dlCount; a++)
            {
                dlFund += 1000;
            }

            for (int b = 0; b < plCount; b++)
            {
                plFund += 1500;
            }

            //this.label8.Text = dlFund.ToString();

            price = price - (dlFund + plFund);
            //MessageBox.Show(price.ToString());
            //MessageBox.Show(dlFund.ToString());
            //MessageBox.Show(plFund.ToString());
            label_funds.Text = price.ToString();

            /*string sql = "SELECT student_ID, Lname, Fname, type, funds, date_claimed FROM scholars WHERE claim_status = 'claimed' ";
            MySqlCommand cmd = new MySqlCommand(sql, init.con);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            this.fundsList.DataSource = dt;

            init.disconnect();*/
        }
        private void Funds_Load(object sender, EventArgs e)
        {
            
        }

        private void addScholars_Click(object sender, EventArgs e)
        {
            Form3 addScholars = new Form3();
            this.Hide();
            addScholars.Show();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        public void filterStatus()
        {
            if (filter.SelectedItem.ToString() == "All")
            {
                fundsList.Rows.Clear();
                conn.Open();
                cmd = new MySqlCommand("SELECT `student_ID`, `Lname`, `Fname`, `type`, `funds`, `date_claimed` FROM `scholars`", conn);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    fundsList.Rows.Add(dr["student_ID"].ToString(), dr["Lname"].ToString(), dr["Fname"].ToString(), dr["type"].ToString(), dr["funds"].ToString(), dr["date_claimed"].ToString());
                }
                dr.Close();
                conn.Close();
            }
            else if (filter.SelectedIndex == 3)
            {
                fundsList.Rows.Clear();
                conn.Open();
                cmd = new MySqlCommand("SELECT `student_ID`, `Lname`, `Fname`, `type`, `funds`, `date_claimed` FROM `scholars` ORDER BY `date_claimed` ASC", conn);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    fundsList.Rows.Add(dr["student_ID"].ToString(), dr["Lname"].ToString(), dr["Fname"].ToString(), dr["type"].ToString(), dr["funds"].ToString(), dr["date_claimed"].ToString());
                }
                dr.Close();
                conn.Close();
            }
            else if (filter.SelectedIndex == 4)
            {
                fundsList.Rows.Clear();
                conn.Open();
                cmd = new MySqlCommand("SELECT `student_ID`, `Lname`, `Fname`, `type`, `funds`, `date_claimed` FROM `scholars` ORDER BY `date_claimed` DESC", conn);

                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    fundsList.Rows.Add(dr["student_ID"].ToString(), dr["Lname"].ToString(), dr["Fname"].ToString(), dr["type"].ToString(), dr["funds"].ToString(), dr["date_claimed"].ToString());
                }
                dr.Close();
                conn.Close();
            }
            else
            {
                fundsList.Rows.Clear();
                conn.Open();
                cmd = new MySqlCommand("SELECT `student_ID`, `Lname`, `Fname`, `type`, `funds`, `date_claimed` FROM `scholars` WHERE type = '" + filter.SelectedItem.ToString() + "'", conn);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    fundsList.Rows.Add(dr["student_ID"].ToString(), dr["Lname"].ToString(), dr["Fname"].ToString(), dr["type"].ToString(), dr["funds"].ToString(), dr["date_claimed"].ToString());
                }
                dr.Close();
                conn.Close();
            }
        }

        private void scholarsList_Click(object sender, EventArgs e)
        {
            displayScholars scholarsList = new displayScholars();
            this.Hide();
            scholarsList.Show();
        }

        private void filter_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterStatus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            login loginPage = new login();
            this.Hide();
            loginPage.Show();
        }

        private void fundsList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
