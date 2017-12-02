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

namespace _100_4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = Directory.GetCurrentDirectory();
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(ofd.FileName);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap srcBmp = new Bitmap(pictureBox1.Image);
            Bitmap newBmp = new Bitmap(srcBmp.Width, srcBmp.Height);
            int[] arr = new int[256];
            int minG = 999, maxG = 0, maxNum = 0, maxPixel = 0;

            for (int h = 0; h < srcBmp.Height; h++)
            {
                for (int w = 0; w < srcBmp.Width; w++)
                {
                    Color currP = srcBmp.GetPixel(w, h);
                    int gray = (int)(currP.R * 0.3 + currP.G * 0.59 + currP.B * 0.11);
                    newBmp.SetPixel(w, h, Color.FromArgb(gray, gray, gray));

                    if (gray < minG) minG = gray;
                    if (gray > maxG) maxG = gray;

                    arr[gray]++;

                }
            }

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != 0 && arr[i] > maxNum)
                {
                    maxNum = arr[i];
                    maxPixel = i;
                }
            }

            pictureBox2.Image = newBmp;

            label1.Text = string.Join("\r\n", new string[] {
                $"最小灰階亮度:{minG}",
                $"最大灰階亮度:{maxG}",
                $"出現最多之灰階亮度:{maxPixel}",
                $"最多灰階之機率:{(float)maxNum/(float)(srcBmp.Width*srcBmp.Height)}"
            });


            Bitmap showBmp = new Bitmap(pictureBox3.Width, pictureBox3.Height);
            Graphics g = Graphics.FromImage(showBmp);

            int draw_w = 300, draw_h = 300;
            int offset = 20;
            double scaleX = draw_w / 256.0, scaleY = draw_h / (double)(srcBmp.Width * srcBmp.Height);
            g.TranslateTransform(offset, pictureBox3.Height - offset);

            g.DrawLines(Pens.Black, new Point[] { new Point(0, -draw_h), new Point(0, 0), new Point(draw_w, 0) });

            Point[] points = new Point[256];
            for (int i = 0; i < arr.Length; i++)
                points[i] = new Point((int)(i * scaleX), (int)(- arr[i] * scaleY));

            g.DrawLines(Pens.Blue, points);

            pictureBox3.Image = showBmp;
        }
    }
}
