using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 模擬2_Q1
{
    public partial class Form1 : Form
    {
        TextBox[,] input, K;
        public Form1()
        {
            InitializeComponent();

            input = CreateTb(groupBox1, 6);
            K = CreateTb(groupBox2, 3);

            foreach (var item in input)
            {
                item.Leave += (s, e) =>
                {
                    TextBox tb = (TextBox)s;
                    if (tb.Text == "") return;
                    int n;
                    if (int.TryParse(tb.Text, out n))
                    {
                        if (n < 0)
                        {
                            MessageBox.Show("資料不可小於0");
                        }
                    }
                };
            }
            foreach (var item in K)
            {
                item.Leave += (s, e) =>
                {
                    TextBox tb = (TextBox)s;
                    if (tb.Text == "") return;
                    int n;
                    if (int.TryParse(tb.Text, out n))
                    {
                        for (int i = 1; i <= n; i *= 2)
                        {
                            if (i == n) return;
                        }
                        MessageBox.Show("輸入2的次方");
                    }
                };
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int sx = int.Parse(textBox1.Text);
            int sy = int.Parse(textBox2.Text);

            int center = int.Parse(input[sy + 1, sx + 1].Text);
            int result = 0;
            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    if (r == 1 && c == 1) continue;

                    int y = sy + r;
                    int x = sx + c;

                    int val = int.Parse(input[y, x].Text) - center;

                    if (val >= 0) val = 1;
                    else val = 0;

                    result += int.Parse(K[r, c].Text) * val;
                }
            }


            textBox3.Text = result.ToString();
        }

        TextBox[,] CreateTb(GroupBox gb, int size)
        {
            TextBox[,] t = new TextBox[size, size];

            for (int r = 0; r < size; r++)
            {
                for (int c = 0; c < size; c++)
                {
                    t[r, c] = new TextBox()
                    {
                        Size = new Size(40, 20),
                        Location = new Point(50 + c * 45, 50 + r * 45),
                        Font = new Font("consolas", 12)
                    };

                    gb.Controls.Add(t[r, c]);
                }
            }
            Label lab;
            for (int i = 0; i < size; i++)
            {
                lab = new Label()
                {
                    Text = i.ToString(),
                    Location = new Point(i * 45 + 50, 20),
                    Size = new Size(20, 20)
                };
                gb.Controls.Add(lab);
            }
            for (int i = 0; i < size; i++)
            {
                lab = new Label()
                {
                    Text = i.ToString(),
                    Location = new Point(20, i * 45 + 50),
                    Size = new Size(20, 20)
                };
                gb.Controls.Add(lab);
            }


            return t;
        }
    }
}
