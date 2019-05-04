using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Lab01
{
    public partial class Form1 : Form
    {
        OpenFileDialog ofd = new OpenFileDialog();
        Preprocessing p = new Preprocessing();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            p.ConvertToGray();
            pictureBox2.ImageLocation = "2.jpg";

        }

        private void button4_Click(object sender, EventArgs e)
        {
            ofd.Title = "Select an image";
            ofd.Filter = "Images(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF |" + "All Files(*.*)|*.*";

            if (ofd.ShowDialog()== DialogResult.OK){

                p.LoadOriginalImage(ofd.FileName);
                String path = ofd.FileName.ToString();
                pictureBox1.ImageLocation = path;          

            }
            p.extract();
        }

        private void button3_Click(object sender, EventArgs e)
        {
           // p.convertToNegative();
            p.negative();
            pictureBox2.ImageLocation = "neg.jpg";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int t = Convert.ToInt32(textBox1.Text);
            p.ConvertGrayToBinary(t);
            pictureBox2.ImageLocation = "3.jpg";
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            File.Delete("1.jpg");
            File.Delete("2.jpg");
            File.Delete("3.jpg");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            p.log();
            pictureBox2.ImageLocation = "4.jpg";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            p.inverseLog();
            pictureBox2.ImageLocation = "5.jpg";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            p.gammaCorrection();
            pictureBox2.ImageLocation = "6.jpg";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            p.grayLevelSlicing();
            pictureBox2.ImageLocation = "7.jpg";
        }
    }
}
