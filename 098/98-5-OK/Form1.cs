using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _98_5
{
    public partial class Form1 : Form
    {
        int n = 0;
        TextBox[][] tb1;
        TextBox[] tb2;
        List<Vector> list = new List<Vector>();
        Vector vec;
        List<double> dist = new List<double>();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out n))
            {
                MessageBox.Show("輸入錯誤");
                return;
            }

            panel1.Controls.Clear();
            panel2.Controls.Clear();

            tb1 = new TextBox[n][];
            for (int i = 0; i < n; i++)
            {
                Label lab1 = new Label()
                {
                    Location = new Point(10, i * 30),
                    Text = $"x{i + 1}座標",
                    AutoSize = true
                };
                tb1[i] = new TextBox[2];
                tb1[i][0] = new TextBox() { Location = new Point(60, i * 30), Size = new Size(50, 20) };
                tb1[i][1] = new TextBox() { Location = new Point(120, i * 30), Size = new Size(50, 20) };

                panel1.Controls.Add(lab1);
                panel1.Controls.AddRange(tb1[i]);
            }

            Label lab2 = new Label() { Location = new Point(0, 0), Text = "輸入測試點(x, y)" };

            tb2 = new TextBox[2];
            tb2[0] = new TextBox() { Location = new Point(60, 30), Size = new Size(50, 20) };
            tb2[1] = new TextBox() { Location = new Point(120, 30), Size = new Size(50, 20) };

            panel2.Controls.Add(lab2);
            panel2.Controls.AddRange(tb2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 驗證輸入
            try
            {
                for (int i = 0; i < tb1.Length; i++)
                {
                    list.Add(new Vector(tb1[i][0].Text, tb1[i][1].Text));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            vec = new Vector(tb2[0].Text, tb2[1].Text);

            list.ForEach(p => dist.Add(p.GetDist(vec)));

            textBox2.Text = dist.Max().ToString();
            textBox3.Text = dist.Min().ToString();
            textBox4.Text = dist.Average().ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (list.Count <= 0)
            {
                MessageBox.Show("集合輸入錯誤");
                return;
            }


            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics canvas = Graphics.FromImage(bmp);

            list.Add(vec);

            int maxX = (int)list.Max(p => p.x) + 1;
            int maxY = (int)list.Max(p => p.y) + 1;
            int max = maxX > maxY ? maxX : maxY;

            list.Remove(vec);// 先加入找出最大座標，再移除

            int size = 200;
            int gap = size / max;

            int sx = 20, sy = 250 - 20;

            canvas.TranslateTransform(sx, sy);

            for (int i = 0; i <= max; i++)
            {
                canvas.DrawLine(Pens.Black, 0, i * -gap, size, i * -gap);
                canvas.DrawLine(Pens.Black, i * gap, 0, i * gap, -size);
                canvas.DrawString($"{i}", Font, Brushes.Black, -20, i * -gap);
                canvas.DrawString($"{i}", Font, Brushes.Black, i * gap, 10);
            }

            int r = 8;
            for (int i = 0; i < list.Count; i++)
            {
                Vector p = list[i];
                canvas.FillEllipse(Brushes.Red, (float)p.x * gap - r / 2, (float)p.y * -gap - r / 2, r, r);
                canvas.DrawString($"x{i + 1}", Font, Brushes.Black, (float)p.x * gap, (float)p.y * -gap);
            }
            canvas.FillEllipse(Brushes.Blue, (float)vec.x * gap - r / 2, (float)vec.y * -gap - r / 2, r, r);
            canvas.DrawString($"x", Font, Brushes.Black, (float)vec.x * gap, (float)vec.y * -gap);

            pictureBox1.Image = bmp;
        }
    }
    public class Vector
    {
        public double x, y;
        public Vector(double x, double y) { this.x = x; this.y = y; }
        public Vector(string x, string y) { this.x = double.Parse(x); this.y = double.Parse(y); }
        public override string ToString() { return $"{x}, {y}"; }
        public double GetDist(Vector v2)
        {
            double dx = v2.x - x;
            double dy = v2.y - y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}
