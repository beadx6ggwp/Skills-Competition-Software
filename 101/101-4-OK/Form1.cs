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

namespace _101_4
{
    public partial class Form1 : Form
    {
        Graphics g;
        TextBox[] tb;
        Point startPos;// 基準點(0,0)位置
        int width, height;// 坐標系寬高
        int scaleX, scaleY;// 實際座標與顯示座標的比例
        public Form1()
        {
            InitializeComponent();
            g = panel1.CreateGraphics();
            tb = new TextBox[] { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6 };

            width = panel1.Width - 2;
            height = panel1.Height - 2;
            startPos = new Point(width / 2, height / 2);
            
            scaleX = width / 80;
            scaleY = height / 60;

        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            drawBack(g);
        }

        private void button1_Click(object sender, EventArgs e_)
        {
            /*
             p[0]:x1,y1
             p[1]:x2,y2
             p[3]:x3,y3
             p[4]:x4,y4
             */
            Point[] p = new Point[4];// L1 = p0~p1, L2 = p2~p3
            string[] temp;
            for (int i = 0; i < 4; i++)
            {
                temp = tb[i].Text.Split(',');
                p[i] = new Point(int.Parse(temp[0]), int.Parse(temp[1]));
            }

            // L1
            float dx1 = p[1].X - p[0].X;
            float dy1 = p[1].Y - p[0].Y;

            float a = dy1;
            float b = -dx1;
            float c = dy1 * p[0].X - dx1 * p[0].Y;

            // L2
            float dx2 = p[3].X - p[2].X;
            float dy2 = p[3].Y - p[2].Y;

            float d = dy2;
            float e = -dx2;
            float f = dy2 * p[2].X - dx2 * p[2].Y;

            // 交點，這裡算出的為實際座標
            float x = ((c * e - b * f) / (a * e - b * d));
            float y = ((d * c - a * f) / (d * b - a * e));

            // draw

            drawBack(g);

            GraphicsState gs = g.Save();


            g.TranslateTransform(startPos.X, startPos.Y);
            g.ScaleTransform(1, -1);// 先平移後反轉Y軸
            g.TranslateTransform(-startPos.X, -startPos.Y);
            
            // 顯示時，以基準點座標為參考，並乘上顯示的縮放比例
            g.DrawLine(Pens.Black, startPos.X + p[0].X * scaleX, startPos.Y + p[0].Y * scaleY, startPos.X + p[1].X * scaleX, startPos.Y + p[1].Y * scaleY);
            g.DrawLine(Pens.Black, startPos.X + p[2].X * scaleX, startPos.Y + p[2].Y * scaleY, startPos.X + p[3].X * scaleX, startPos.Y + p[3].Y * scaleY);
            
            g.DrawEllipse(Pens.Red, startPos.X + x * scaleX - 5, startPos.Y + y * scaleY - 5, 10, 10);


            g.Restore(gs);

            // show result

            tb[4].Text = "未相交";
            tb[5].Text = "";
            if (inRange(p[0].X, p[1].X, x) && inRange(p[0].Y, p[1].Y, y))
            {
                tb[4].Text = "有相交";
                tb[5].Text = $"{Math.Round(x, 2)},{Math.Round(y, 2)}";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            drawBack(g);
            Array.ForEach(tb, item => item.Clear());
        }
        bool inRange(float n1, float n2, float r)
        {
            return Math.Min(n1, n2) <= r && r <= Math.Max(n1, n2);
        }

        void drawBack(Graphics g)
        {
            g.Clear(BackColor);

            g.DrawLine(Pens.Black, 0, startPos.Y, width, startPos.Y);
            g.DrawLine(Pens.Black, startPos.X, 0, startPos.X, height);

            drawString(g, "-40,0", 0, startPos.Y);
            drawString(g, "40,0", width - 30, startPos.Y);
            drawString(g, "0,30", startPos.X, startPos.Y - height / 2);
            drawString(g, "0,-30", startPos.X, startPos.Y + height / 2 - 20);
            drawString(g, "0,0", startPos.X, startPos.Y);

            // 分成10段，然後乘上顯示比例
            for (int i = 0; i < 9; i++)
            {
                g.FillEllipse(Brushes.Black, i * scaleX * 10 - 2, startPos.Y - 2, 4, 4);
                g.FillEllipse(Brushes.Black, startPos.X - 2, i * scaleY * 10 - 2, 4, 4);
            }
        }


        void drawString(Graphics g, string str, int x, int y)
        {
            g.DrawString(str, new Font("consolas", 8), Brushes.Black, x, y);
        }
    }
}
