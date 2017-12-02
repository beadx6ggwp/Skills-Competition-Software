using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _104_5
{
    public partial class Form1 : Form
    {
        const int SIZE_IO = 7;
        const int SIZE_K = 3;
        TextBox[][] Input, K, Output;


        public Form1()
        {
            InitializeComponent();


            Input = MakeTable(new Point(50, 100), SIZE_IO, SIZE_IO, 0);
            K = MakeTable(new Point(400, 100), SIZE_K, SIZE_K, -1);
            Output = MakeTable(new Point(600, 100), SIZE_IO, SIZE_IO, 0);

        }

        private void button1_Click(object sender, EventArgs e)// Clac Result
        {
            solve1();
            solve2();
        }

        public void solve1()
        {
            for (int r = 0; r < 7; r++)
            {
                for (int c = 0; c < 7; c++)
                {
                    Output[r][c].Text = getAnser(c, r).ToString();
                }
            }
        }

        public int getAnser(int c, int r)
        {
            int result = 0;

            int mn = SIZE_K - 1;

            // (x,y) = (i,j)
            for (int j = 0; j < SIZE_K; j++)
            {
                for (int i = 0; i < SIZE_K; i++)
                {
                    int offSetX = i - 1;
                    int offSetY = j - 1;

                    int tx = c + offSetX;
                    int ty = r + offSetY;

                    if (tx < 0 || tx > SIZE_IO - 1 || ty < 0 || ty > SIZE_IO - 1) continue;

                    result += int.Parse(Input[ty][tx].Text) * int.Parse(K[mn - j][mn - i].Text);
                }
            }


            return result;
        }



        public void solve2()
        {
            int MSE_sum = 0, MAE_sum = 0;
            double MSE = 0, MAE = 0, PSNR = 0;
            for (int r = 0; r < 7; r++)
            {
                for (int c = 0; c < 7; c++)
                {
                    int sub = int.Parse(Input[r][c].Text) - int.Parse(Output[r][c].Text);

                    MSE_sum += sub * sub;
                    MAE_sum += Math.Abs(sub);
                }
            }

            MSE = MSE_sum / (double)(SIZE_IO * SIZE_IO);
            MAE = MAE_sum / (double)(SIZE_IO * SIZE_IO);
            PSNR = 10 * Math.Log10((255 * 255) / MSE);



            textBox1.Text = MSE.ToString();
            textBox2.Text = MAE.ToString();
            textBox3.Text = PSNR.ToString();
        }




        public TextBox[][] MakeTable(Point pos, int col, int row, int startIndex)
        {
            TextBox[][] textbox_arr = new TextBox[row][];
            Label[] lab_arr = new Label[col + row];

            int boxWidth = 40, boxHeight = 25;
            int gapWidth = 5, gapHeight = 5;
            int index = 0;

            for (int r = 0; r < row; r++)
            {
                textbox_arr[r] = new TextBox[col];
                for (int c = 0; c < col; c++, index++)
                {
                    textbox_arr[r][c] = new TextBox()
                    {
                        Name = c.ToString() + "," + r.ToString(),

                        Multiline = true,
                        TextAlign = HorizontalAlignment.Center,
                        Font = new Font("consolas", 10f),
                        Text = "0",

                        Size = new Size(boxWidth, boxHeight),
                        Location = new Point(
                            pos.X + c * (boxWidth + gapWidth),
                            pos.Y + r * (boxHeight + gapHeight))
                    };

                    this.Controls.Add(textbox_arr[r][c]);
                }
            }

            for (int i = 0; i < lab_arr.Length; i++)
            {
                lab_arr[i] = new Label()
                {
                    Name = i.ToString(),
                    Size = new Size(boxWidth, boxHeight),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("consolas", 10f)
                };
                if (i < col)
                {
                    int c = i;
                    lab_arr[i].Text = (c + startIndex).ToString();
                    lab_arr[i].Location = new Point(pos.X + c * (boxWidth + gapWidth), pos.Y - boxHeight);
                }
                else
                {
                    int r = i - col;
                    lab_arr[i].Text = (r + startIndex).ToString();
                    lab_arr[i].Location = new Point(pos.X - boxWidth, pos.Y + r * (boxHeight + gapHeight));
                }

            }
            this.Controls.AddRange(lab_arr);

            return textbox_arr;
        }
    }
}
