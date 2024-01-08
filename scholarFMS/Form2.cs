using MySql.Data.MySqlClient;
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

namespace scholarFMS
{
    public partial class Form2 : Form
    {
        MySqlConnection conn = new MySqlConnection("datasource=localhost; port=3306; username=root; password=");
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        int success = 0;
        public Form2()
        {
            InitializeComponent();
        }
        public void Insert(byte[] signature, byte[] valid_id, string stud_id)
        {
            conn.Open();

            // Check if stud_id exists in the database
            using (MySqlCommand checkCmd = new MySqlCommand("SELECT COUNT(*) FROM `im_etr`.`scholars` WHERE student_ID = @stud_id", conn))
            {
                checkCmd.Parameters.AddWithValue("@stud_id", stud_id);

                int rowCount = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (rowCount == 0)
                {
                    // stud_id not found, handle the case as needed (e.g., show a message)
                    MessageBox.Show($"Student with ID {stud_id} not found in the database.");
                    success++;
                    conn.Close();
                    return;
                }
            }

            // If stud_id exists, proceed with the update
            using (MySqlCommand updateCmd = new MySqlCommand("UPDATE `im_etr`.`scholars` SET `signature` = @signature, `valid_id` = @valid_id WHERE student_ID = @stud_id", conn))
            {
                updateCmd.Parameters.AddWithValue("@signature", signature);
                updateCmd.Parameters.AddWithValue("@valid_id", valid_id);
                updateCmd.Parameters.AddWithValue("@stud_id", stud_id);
                updateCmd.ExecuteNonQuery();
            }

            conn.Close();
        }

        byte[] ConvertImageToByte(Image img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }

        private void button_imgID_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Image files(*.jpg;*.jpeg)|*.jpg;*.jpeg", Multiselect = false })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pictureBox3.Image = Image.FromFile(ofd.FileName);
                }
            }
        }

        private void button_img2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Image files(*.jpg;*.jpeg)|*.jpg;*.jpeg", Multiselect = false })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pictureBox2.Image = Image.FromFile(ofd.FileName);
                }
            }
        }

        private void button_upload_Click(object sender, EventArgs e)
        {
            DataValidator validator = new DataValidator();

            // Validate if images are selected
            if (!validator.AreImagesSelected(pictureBox2.Image, pictureBox3.Image))
            {
                MessageBox.Show("Please select both images.");
                success++;
                return;
            }

            // Validate if all fields are filled
            if (!validator.AreTextFieldsFilled(textBox_studID, textBox_firstName, textBox_lastName, textBox_midName))
            {
                MessageBox.Show("Please fill up all fields.");
                success++;
                return;
            }

            // Validate if the two images are not the same
            if (validator.AreImagesEqual(pictureBox2.Image, pictureBox3.Image))
            {
                MessageBox.Show("Please select different images for upload.");
                success++;
                return;
            }

            Insert(ConvertImageToByte(pictureBox2.Image), ConvertImageToByte(pictureBox3.Image), textBox_studID.Text);
            if (success == 0)
            {
                MessageBox.Show("Upload Success");
            }
            else
            {
                MessageBox.Show("Upload Unsuccessful");
                success = 0;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
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
public class DataValidator
{
    public bool AreImagesSelected(params Image[] images)
    {
        return images.All(image => image != null);
    }

    public bool AreTextFieldsFilled(params TextBox[] textFields)
    {
        return textFields.All(textBox => !string.IsNullOrEmpty(textBox.Text));
    }

    public bool AreImagesEqual(Image image1, Image image2)
    {
        if (image1 == null || image2 == null)
        {
            return false;
        }

        Bitmap bmp1 = new Bitmap(image1);
        Bitmap bmp2 = new Bitmap(image2);

        for (int i = 0; i < bmp1.Width; i++)
        {
            for (int j = 0; j < bmp1.Height; j++)
            {
                if (bmp1.GetPixel(i, j) != bmp2.GetPixel(i, j))
                {
                    return false;
                }
            }
        }

        return true;
    }
}
