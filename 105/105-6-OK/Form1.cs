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
namespace _105_6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Directory.GetCurrentDirectory();
            if (ofd.ShowDialog() == DialogResult.OK)
                pictureBox1.Image = Image.FromFile(ofd.FileName);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox1.Image);

            for (int r = 0; r < bmp.Height; r++)
            {
                for (int c = 0; c < bmp.Width; c++)
                {
                    Color color = bmp.GetPixel(c, r);

                    int colorInt = color.ToArgb();
                    var s = Convert.ToString(colorInt, 2);
                    int R = (colorInt >> 16 & 1) == 1 ? 16 : 235,
                        G = (colorInt >> 8 & 1) == 1 ? 16 : 235,
                        B = (colorInt & 1) == 1 ? 16 : 235;
                    bmp.SetPixel(c, r, Color.FromArgb(R, G, B));
                }
            }

            pictureBox1.Image = bmp;
        }
    }
}
