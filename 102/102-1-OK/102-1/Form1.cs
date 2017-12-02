using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _102_1
{
    public partial class Form1 : Form
    {
        TextBox[,] tb_input;
        TextBox[,] tb_mask;

        int inputSize = 6;
        int maskSize = 3;
        public Form1()
        {
            InitializeComponent();
            tb_input = makeTable(new Point(50, 100), inputSize, inputSize, 0);
            tb_mask = makeTable(new Point(400, 100), maskSize, maskSize, 1);
        }



        private void button1_Click(object sender, EventArgs e)
        {
            int sx = int.Parse(textBox1.Text);
            int sy = int.Parse(textBox2.Text);

            int cx = sx + 1;
            int cy = sy + 1;

            int result = 0;
            for (int r = sy; r < maskSize + sy; r++)
            {
                for (int c = sx; c < maskSize + sx; c++)
                {
                    int delta = int.Parse(tb_input[r, c].Text) - int.Parse(tb_input[cy, cx].Text);
                    int Tx = delta >= 0 ? 1 : 0;

                    result += Tx * int.Parse(tb_mask[r - sy, c - sx].Text);
                }
            }

            textBox3.Text = result.ToString();
        }



        TextBox[,] makeTable(Point pos, int col, int row, int type)
        {
            TextBox[,] tb_arr = new TextBox[row, col];
            Size size = new Size(40, 25);
            int gap = 5;
            int index = 0;
            for (int r = 0; r < row; r++)
            {
                for (int c = 0; c < col; c++, index++)
                {
                    tb_arr[r, c] = new TextBox()
                    {
                        Name = $"{type}{index}",
                        Multiline = true,
                        Size = size,
                        Location = new Point(c * (size.Width + gap) + pos.X, r * (size.Height + gap) + pos.Y),
                    };
                    tb_arr[r, c].Leave += new EventHandler(text_Leave1);

                    this.Controls.Add(tb_arr[r, c]);
                }
            }

            Label[] lab_arr = new Label[col + row];
            for (int c = 0; c < col; c++)
            {
                lab_arr[c] = new Label()
                {
                    Size = size,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = c.ToString(),
                    Location = new Point(c * (size.Width + gap) + pos.X, pos.Y - size.Height)
                };
            }
            for (int r = 0; r < row; r++)
            {
                lab_arr[col + r] = new Label()
                {
                    Size = size,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = r.ToString(),
                    Location = new Point(pos.X - size.Width, r * (size.Height + gap) + pos.Y)
                };
            }

            this.Controls.AddRange(lab_arr);

            return tb_arr;
        }

        void text_Leave1(object sender, EventArgs e)
        {
            // 輸入檢驗
            TextBox tb = (sender as TextBox);
            string type = tb.Name.Substring(0, 1);

            int val;
            if (type == "0" && int.TryParse(tb.Text, out val))
            {
                if (val < 0)
                {
                    MessageBox.Show("不可小於0");
                    tb.Text = "";
                    return;
                }
            }
            if (type == "1" && int.TryParse(tb.Text, out val))
            {
                if (!BaseIs2(val))
                {
                    tb.Text = "";
                    MessageBox.Show("權重為2的次方");
                }
            }
        }

        bool BaseIs2(int n)
        {
            return (n & (n - 1)) == 0;
        }
    }
}
