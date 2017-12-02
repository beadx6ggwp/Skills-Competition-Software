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
namespace _105_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        double chairRealH = 830, chairRealW;
        double rightRealH, rightRealW;
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Directory.GetCurrentDirectory();
            if (ofd.ShowDialog() == DialogResult.OK)
                pictureBox1.Image = Image.FromFile(ofd.FileName);

            Bitmap bmp = new Bitmap(pictureBox1.Image);
            Point[] chairPts = GetSize(bmp, new Point(0, 0));
            int chairPixelW = chairPts[1].X - chairPts[0].X;
            int chairPixelH = chairPts[1].Y - chairPts[0].Y - 1;

            chairRealW = chairRealH * (double)chairPixelW / (double)chairPixelH;


            Point[] rightPts = GetSize(bmp, new Point(chairPts[1].X, 0));
            int rightPixelW = rightPts[1].X - rightPts[0].X;
            int rightPixelH = rightPts[1].Y - rightPts[0].Y - 1;

            rightRealH = (double)rightPixelH / (double)chairPixelH * chairRealH;
            rightRealW = (double)rightPixelW / (double)chairPixelW * chairRealW;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = $"{rightRealH:0.####}";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label2.Text = $"{rightRealW:0.####}";
        }

        Point[] GetSize(Bitmap bmp, Point start)
        {
            Point[] pts = new Point[2];
            int sx = 0, sy = 0, ex = 0, ey = 0;

            bool isTouch = false;
            for (int x = start.X; x < bmp.Width; x++)
            {
                bool isObj = false;
                for (int y = start.Y; y < bmp.Height; y++)
                {
                    if (!isBackColor(bmp.GetPixel(x, y)))
                    {
                        isObj = true;
                        break;
                    }
                }
                if (isObj && !isTouch)
                {
                    sx = x;
                    isTouch = true;
                }
                else if (!isObj && isTouch)
                {
                    ex = x;
                    break;
                }
            }

            isTouch = false;
            for (int y = start.Y; y < bmp.Height; y++)
            {
                bool isObj = false;
                for (int x = sx; x < ex; x++)
                {
                    if (!isBackColor(bmp.GetPixel(x, y)))
                    {
                        isObj = true;
                        break;
                    }
                }
                if (isObj && !isTouch)
                {
                    sy = y;
                    isTouch = true;
                }
                else if (!isObj && isTouch)
                {
                    ey = y;
                    break;
                }
            }

            pts[0] = new Point(sx, sy);
            pts[1] = new Point(ex, ey);
            return pts;
        }

        bool isBackColor(Color c)
        {
            return c.R * 0.3 + c.G * 0.59 + c.B * 0.11 >= 200;
        }
    }
}