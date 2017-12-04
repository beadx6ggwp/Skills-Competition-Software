using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 許展維_Q1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int N = 0, Q = 0;

        Random ran = new Random();

        List<Data> list = new List<Data>();
        List<double> mse_list = new List<double>();

        private void button1_Click(object sender, EventArgs e)
        {
            N = int.Parse(textBox1.Text);

            // reset
            list.Clear();
            label6.Text = textBox3.Text = textBox4.Text = textBox5.Text = "";

            for (int i = 0; i < N; i++)
            {
                label6.Text += $"{i + 1}".PadLeft(4, ' ');
                textBox3.Text += $"{ran.Next(0, 101)}".PadLeft(4, ' ');
                textBox4.Text += $"{ran.Next(0, 101)}".PadLeft(4, ' ');
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mse_list.Clear();
            list.Clear();
            textBox5.Clear();

            Q = int.Parse(textBox2.Text);// 執行次數

            double[] mathArr = Array.ConvertAll(textBox3.Text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries), double.Parse);
            double[] engArr = Array.ConvertAll(textBox4.Text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries), double.Parse);

            for (int i = 0; i < N; i++)
            {
                // 加入資料
                Data d = new Data(mathArr[i], engArr[i]);
                list.Add(d);
            }

            // 分兩堆的暫存
            List<Data>[] two = new List<Data>[] { new List<Data>(), new List<Data>() };

            int index1 = ran.Next(0, list.Count);
            int index2 = ran.Next(0, list.Count);
            while (index1 == index2) index2 = ran.Next(0, list.Count);

            // 先隨便找兩個資料當重心
            Data[] avg2 = new Data[2] { list[index1], list[index2] };

            // 開始分堆
            for (int i = 0; i < Q; i++)
            {
                Array.ForEach(two, item => item.Clear());
                for (int j = 0; j < list.Count; j++)
                {
                    Data now = list[j];

                    int minIndex = 0;
                    double min = 999999;
                    // 比較now離哪堆比較近
                    for (int k = 0; k < 2; k++)
                    {
                        double dist = getDist(now, avg2[k]);
                        if (dist < min)
                        {
                            minIndex = k;
                            min = dist;
                        }
                    }

                    // 將now分配到離他最近的那堆
                    now.index = minIndex;
                    two[minIndex].Add(now);
                }

                // 取得新的重心
                for (int j = 0; j < 2; j++)
                {
                    double avgM = two[j].Average(item => item.math);
                    double avgE = two[j].Average(item => item.eng);
                    avg2[j] = new Data(avgM, avgE);
                }

                // 計算MSE
                double mse = 0;

                for (int k = 0; k < 2; k++)
                {
                    List<Data> S = two[k];
                    foreach (var item in S)
                    {
                        double dx = item.math - avg2[k].math;
                        double dy = item.eng - avg2[k].eng;
                        mse += Math.Sqrt(dx * dx + dy * dy) / list.Count;
                    }
                }

                mse_list.Add(mse);
            }

            // 顯示分堆結果
            foreach (var item in list)
            {
                textBox5.Text += $"{item.index}".PadLeft(4);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(800, 400);// range 50~350 300
            Graphics g = Graphics.FromImage(bmp);

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TranslateTransform(50, 350);

            double sx = 700 / mse_list.Count;
            double sy = -300 / mse_list.Max();

            List<PointF> pts = new List<PointF>();

            int r = 2;
            for (int x = 0; x < mse_list.Count; x++)
            {
                double y = mse_list[x];

                pts.Add(new PointF((float)(x * sx), (float)(y * sy)));
                g.FillEllipse(Brushes.Red, (float)(x * sx) - r, (float)(y * sy) - r, r * 2, r * 2);
            }

            g.DrawLines(Pens.Red, pts.ToArray());



            g.DrawLine(Pens.Black, 0, 0, 800, 0);
            g.DrawLine(Pens.Black, 0, 0, 0, -400);
            for (int i = 0; i < list.Count; i++)
            {
                if (i % 2 == 0)
                    g.DrawString(i.ToString(), Font, Brushes.Black, (float)(i * sx) - 10, 0);
                g.FillRectangle(Brushes.Black, (float)(i * sx) - 1, -1, 2, 2);
            }

            for (int i = 0; i < (int)mse_list.Max() + 4; i++)
            {
                if (i == 0) continue;
                if (i % 2 == 0)
                {
                    g.DrawString((i).ToString(), Font, Brushes.Black, -20, (float)(i * sy));
                    g.FillRectangle(Brushes.Black, -1, (float)(i * sy) - 1, 2, 2);
                }
            }

            pictureBox1.Image = bmp;
        }

        double getDist(Data d1, Data d2)
        {
            double dx = d1.math - d2.math;
            double dy = d1.eng - d2.eng;

            return Math.Sqrt(dx * dx + dy * dy);
        }
    }

    class Data
    {
        public double math, eng;
        public int index;
        public Data(double ma, double en)
        {
            math = ma;
            eng = en;
        }
    }
}
