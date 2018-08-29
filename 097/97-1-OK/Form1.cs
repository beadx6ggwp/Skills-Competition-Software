using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _97_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double minX = double.Parse(textBox1.Text);
            double maxX = double.Parse(textBox2.Text);

            double sx = 500 / (maxX - minX);
            double sy = 500 / 2;
            List<PointF> pts = new List<PointF>();
            for (double x = minX; x <= maxX; x += 0.1)
            {
                pts.Add(new PointF((float)(x * sx), -(float)(calc(x) * sy)));
            }

            
            Bitmap bmp = new Bitmap(600, 600);
            Graphics g = Graphics.FromImage(bmp);
            g.TranslateTransform(300, 300);
            g.DrawLines(Pens.Red, pts.ToArray());

            g.DrawLine(Pens.Black, -300, 0, 300, 0);
            g.DrawLine(Pens.Black, 0, 300, 0, -300);
            for (int i = (int)minX; i <= (int)maxX; i++)
            {
                if (i % 5 == 0)
                    g.DrawString(i.ToString(), Font, Brushes.Black, (float)(i * sx) - 10, 0);
                g.FillRectangle(Brushes.Black, (float)(i * sx) - 1, -1, 2, 2);
            }

            for (int i = -10; i <= 10; i++)
            {
                if (i == 0) continue;
                if (i % 2 == 0)
                    g.DrawString((-i / 10.0).ToString(), Font, Brushes.Black, 0, (float)(i / 10.0 * sy));
                g.FillRectangle(Brushes.Black, -1, (float)(i / 10.0 * sy) - 1, 2, 2);
            }

            pictureBox1.Image = bmp;
        }


        double calc(double x)
        {
            if (Math.Abs(x) <= 0.01) return 1;
            return Math.Sin(x) / x;
        }
    }
}
