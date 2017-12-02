using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 模擬1_Q5
{
    public partial class Form1 : Form
    {
        TextBox[,] input, K, output;
        public Form1()
        {
            InitializeComponent();

            input = CreateTextBox(7, groupBox1, 0);
            K = CreateTextBox(3, groupBox2, -1);
            output = CreateTextBox(7, groupBox3, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int r = 1; r < 6; r++)
            {
                for (int c = 1; c < 6; c++)
                {
                    output[r, c].Text = getAns(r, c).ToString();
                }
            }

            double MSE = 0, MAE = 0, PSNR = 0;

            for (int r = 0; r < 7; r++)
            {
                for (int c = 0; c < 7; c++)
                {
                    MSE += Math.Pow(int.Parse(input[r, c].Text) - int.Parse(output[r, c].Text), 2);
                }
            }

            MSE /= 7 * 7;

            for (int r = 0; r < 7; r++)
            {
                for (int c = 0; c < 7; c++)
                {
                    MAE += Math.Abs(int.Parse(input[r, c].Text) - int.Parse(output[r, c].Text));
                }
            }

            MAE /= 7 * 7;


            PSNR = 10 * Math.Log10((255 * 255) / MSE);

            textBox1.Text = MSE.ToString();
            textBox2.Text = MAE.ToString();
            textBox3.Text = PSNR.ToString();
        }

        int getAns(int y, int x)
        {
            int result = 0;

            for (int r = -1; r < 2; r++)
            {
                for (int c = -1; c < 2; c++)
                {
                    result += int.Parse(input[y + r, x + c].Text) * int.Parse(K[1 - r, 1 - c].Text);
                }
            }

            return result;
        }

        TextBox[,] CreateTextBox(int n, GroupBox groupBox, int start)
        {
            TextBox[,] tb = new TextBox[n, n];
            for (int r = 0; r < n; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    tb[r, c] = new TextBox()
                    {
                        Text = "0",
                        Size = new Size(30, 20),
                        Location = new Point(c * 40 + 50, r * 40 + 50),
                        TextAlign = HorizontalAlignment.Center
                    };

                    groupBox.Controls.Add(tb[r, c]);
                }
            }

            for (int i = 0; i < n; i++)
            {
                Label lab = new Label()
                {
                    Text = (i + start).ToString(),
                    Location = new Point(i * 40 + 50, 20),
                    Size = new Size(20, 20)
                };
                groupBox.Controls.Add(lab);
            }

            for (int i = 0; i < n; i++)
            {
                Label lab = new Label()
                {
                    Text = (i + start).ToString(),
                    Location = new Point(20, i * 40 + 50),
                    Size = new Size(20, 20)
                };
                groupBox.Controls.Add(lab);
            }


            return tb;
        }
    }
}
