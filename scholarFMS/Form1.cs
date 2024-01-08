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
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panel2.Width += 3;
            if (panel2.Width >= 750)
            {
                timer1.Stop();
                login loginscreen = new login();
                loginscreen.Show();
                this.Hide();

            }
            

        }

        private void Splash_Load(object sender, EventArgs e)
        {
            this.timer1.Start();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
