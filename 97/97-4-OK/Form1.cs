using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _97_4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] input = textBox1.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            List<PointF> pts = new List<PointF>();
            for (int i = 0; i < input.Length; i++)
            {
                string[] temp = input[i].Split(' ');
                float x = float.Parse(temp[0]);
                float y = float.Parse(temp[1]);

                pts.Add(new PointF(x, y));
            }

            double x_sum = pts.Sum(p => p.X);
            double x2_sum = pts.Sum(p => p.X * p.X);
            double xy_sum = pts.Sum(p => p.X * p.Y);
            double x_avg = pts.Average(p => p.X);
            double y_avg = pts.Average(p => p.Y);

            double minX = pts.Min(p => p.X);
            double maxX = pts.Max(p => p.X);

            double m = (xy_sum - x_sum * y_avg) / (x2_sum - x_sum * x_avg);
            double b = y_avg - m * x_avg;

            // 縮放比例
            double sx = 400 / pts.Max(p => p.X);
            double sy = -400 / pts.Max(p => p.Y);
            Bitmap bmp = new Bitmap(600, 600);// range = 0~400
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);

            g.TranslateTransform(100, 500);

            // 畫座標系
            g.DrawLine(Pens.Black, 0, 0, 400, 0);
            g.DrawLine(Pens.Black, 0, 0, 0, -400);
            for (int x = 0; x <= maxX; x += 1) g.DrawString(x.ToString(), Font, Brushes.Black, (float)(x * sx), 0);
            for (int y = 0; y <= pts.Max(p => p.Y); y += 1) g.DrawString(y.ToString(), Font, Brushes.Black, 0, (float)(y * sy));

            // 畫點
            int r = 4;
            foreach (var p in pts)
                g.DrawEllipse(Pens.Blue, (float)(p.X * sx) - r / 2, (float)(p.Y * sy) - r / 2, r, r);

            // 畫平均線
            g.DrawLine(Pens.Red, (float)(minX * sx), (float)((minX * m + b) * sy), (float)(maxX * sx), (float)((maxX * m + b) * sy));



            pictureBox1.Image = bmp;

            label1.Text = $"m : {m:0.000}\r\nb : {b:0.000}";
        }
    }
}
