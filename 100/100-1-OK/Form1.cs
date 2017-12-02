using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace _100_1
{
    public partial class Form1 : Form
    {
        Graphics g;
        Pen pen;
        public Form1()
        {
            InitializeComponent();
            g = panel1.CreateGraphics();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int n;
            if (int.TryParse(textBox1.Text, out n) && (n < 3 || n > 10))
            {
                MessageBox.Show("3 <= N <= 10");
                return;
            }

            pen = new Pen(GetColor(textBox2.Text));
            PointF[] points = new PointF[n];

            int r = 200;
            double add = Math.PI * 2 / n; // 計算間隔
            double angle = -Math.PI / 2;// 起始角度
            for (int i = 0; i < n; i++)
            {
                // 取得每個點
                points[i] = new PointF((float)Math.Cos(angle) * r, (float)Math.Sin(angle) * r);
                angle += add;
            }


            g.ResetTransform();
            g.Clear(BackColor);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            g.TranslateTransform(panel1.Width / 2, panel1.Height / 2);
            // 在每個點之間畫線
            for (int i = 0; i < n - 1; i++)
                for (int j = i + 1; j < n; j++)
                    g.DrawLine(pen, points[i], points[j]);
        }
        Color GetColor(string str)
        {
            Color c = Color.White;
            switch (str)
            {
                case "B":
                    c = Color.Black;
                    break;
                case "R":
                    c = Color.Red;
                    break;
                case "G":
                    c = Color.Green;
                    break;
                case "L":
                    c = Color.Blue;
                    break;
            }
            return c;
        }
    }
}
