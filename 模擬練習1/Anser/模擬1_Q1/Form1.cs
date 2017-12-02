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

namespace 模擬1_Q1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int N = int.Parse(textBox1.Text);
            int M = int.Parse(textBox2.Text);

            int N10 = N / 10;// 十位數
            int N1 = N % 10;// 各位數

            int M10 = M / 10;
            int M1 = M % 10;


            Bitmap bmp = new Bitmap(400, 400);
            Graphics g = Graphics.FromImage(bmp);

            g.TranslateTransform(50, 50);// 300,300

            Pen p = new Pen(Color.Red, 1);

            int gap = 5;
            int offset = 20;

            p.DashStyle = DashStyle.Dash;
            g.DrawLine(p, N10 + gap + offset, 0, N10 + gap + offset, 300);
            g.DrawLine(p, 300 - N1 - gap - offset, 0, 300 - N1 - gap - offset, 300);

            p.DashStyle = DashStyle.Solid;
            for (int i = 0; i < N10; i++)
            {
                g.DrawLine(p, N10 * i + gap * i + offset, 0, N10 * i + gap * i + offset, 300);
            }
            for (int i = 0; i < N1; i++)
            {
                g.DrawLine(p, (300 - N1 * i) - gap * i - offset, 0, 300 - N1 * i - gap * i - offset, 300);
            }

            p.Color = Color.Blue;
            p.DashStyle = DashStyle.Dash;
            g.DrawLine(p, 0, 300 - M10 - gap - offset, 300, 300 - M10 - gap - offset);
            g.DrawLine(p, 0, M10 + gap + offset, 300, M10 + gap + offset);

            p.DashStyle = DashStyle.Solid;
            for (int i = 0; i < M10; i++)
            {
                g.DrawLine(p, 0, 300 - M10 * i - gap * i - offset, 300, 300 - M10 * i - gap * i - offset);
            }
            for (int i = 0; i < M1; i++)
            {
                g.DrawLine(p, 0, M10 * i + gap * i + offset, 300, M10 * i + gap * i + offset);
            }

            Bitmap bmp2 = new Bitmap(400, 400);
            g = Graphics.FromImage(bmp2);

            g.ResetTransform();
            g.TranslateTransform(200, 200);
            g.RotateTransform(45);
            g.DrawImage(bmp, -200, -200);

            pictureBox1.Image = bmp2;
        }

    }
}
