using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _96_1
{
    public partial class Form1 : Form
    {
        Graphics g;
        Bitmap bmp;

        Point[] pts = new Point[3];
        Random ran = new Random();

        double n45_Len = 0, on45_Len = 0, saving = 0;
        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(400, 400);
            g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Point HorPoint, low, high, left, right;

            n45_Len = 0;
            on45_Len = 0;
            g.Clear(Color.White);
            for (int i = 0; i < 3; i++)
                pts[i] = RandomPoint(50, 400 - 50);

            // 先畫垂直
            Array.Sort(pts, (a, b) => a.Y.CompareTo(b.Y));

            low = pts[0];
            HorPoint = pts[1];
            high = pts[2];
            n45_Len += high.Y - low.Y;

            g.DrawLine(Pens.Black, high.X, high.Y, high.X, HorPoint.Y);
            g.DrawLine(Pens.Black, low.X, low.Y, low.X, HorPoint.Y);

            // 在畫水平
            Array.Sort(pts, (a, b) => a.X.CompareTo(b.X));

            left = pts[0];
            right = pts[2];
            n45_Len += right.X - left.X;

            g.DrawLine(Pens.Black, left.X, HorPoint.Y, HorPoint.X, HorPoint.Y);
            g.DrawLine(Pens.Black, right.X, HorPoint.Y, HorPoint.X, HorPoint.Y);

            drawPoint(pts);

            pictureBox1.Image = bmp;
            label1.Text = $"Non-45 Length:{n45_Len}\non-45 Length:{on45_Len}\nSaving:{(n45_Len - on45_Len) / n45_Len * 100:0.00}";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            Point HorPoint, low, high;
            Point p45_1, p45_2;
            int dist1 = 0, dist2 = 0;
            on45_Len = 0;

            Array.Sort(pts, (a, b) => a.Y.CompareTo(b.Y));
            low = pts[0];
            HorPoint = pts[1];
            high = pts[2];

            dist1 = HorPoint.Y - low.Y;
            dist2 = high.Y - HorPoint.Y;

            // 先判斷是否需要折彎，如果原本的長度比較短就用原本長度，否則折彎
            // 防止兩點很近的時候折彎，導致長度變更常
            // low Point
            if (Math.Abs(HorPoint.X - low.X) > dist1)
            {
                p45_1 = new Point(low.X + Math.Sign(HorPoint.X - low.X) * dist1, HorPoint.Y);
                on45_Len += Math.Sqrt(dist1 * dist1);
            }
            else
            {
                p45_1 = new Point(low.X, HorPoint.Y);
                on45_Len += dist1;
            }

            // high Point
            if (Math.Abs(HorPoint.X - high.X) > dist2)
            {
                p45_2 = new Point(high.X + Math.Sign(HorPoint.X - high.X) * dist2, HorPoint.Y);
                on45_Len += Math.Sqrt(dist2 * dist2);
            }
            else
            {
                p45_2 = new Point(high.X, HorPoint.Y);
                on45_Len += dist2;
            }

            var temp = new Point[] { HorPoint, p45_1, p45_2 };
            Array.Sort(temp, (a, b) => a.X.CompareTo(b.X));
            on45_Len += Math.Abs(temp[0].X - temp[2].X);
            
            // 畫出
            g.DrawLine(Pens.Black, low.X, low.Y, p45_1.X, p45_1.Y);
            g.DrawLine(Pens.Black, p45_1.X, p45_1.Y, HorPoint.X, HorPoint.Y);

            g.DrawLine(Pens.Black, high.X, high.Y, p45_2.X, p45_2.Y);
            g.DrawLine(Pens.Black, p45_2.X, p45_2.Y, HorPoint.X, HorPoint.Y);

            drawPoint(pts);


            pictureBox1.Image = bmp;
            label1.Text = $"Non-45 Length:{n45_Len}\non-45 Length:{on45_Len}\nSaving:{(n45_Len - on45_Len) / n45_Len * 100:0.00}";
        }

        Point RandomPoint(int min, int max)
        {
            return new Point(ran.Next(min, max), ran.Next(min, max));
        }
        void drawPoint(Point[] p)
        {
            int r = 2;
            for (int i = 0; i < p.Length; i++)
            {
                g.FillRectangle(Brushes.Blue, p[i].X - r, p[i].Y - r, 2 * r, 2 * r);
            }
        }
    }
}
