using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace 許展維_Q4
{
    public partial class Form1 : Form
    {
        int K = 0;// 訓練資料數
        int N = 3;// 訓練參數量
        double n = 0;// 學習率
        double maxTime = 0;// 最大學習次數

        List<double[]> W = new List<double[]>();
        List<double[]> X = new List<double[]>();

        List<double> Y = new List<double>();


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Directory.GetCurrentDirectory();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = File.ReadAllText(ofd.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReadData();

            for (int i = 0; i < maxTime; i++)
            {
                if (learn() == 0) break;
            }

            textBox5.Text = string.Join(";", Array.ConvertAll(W[W.Count - 1], s => $"{s:0.00}"));
        }
        private void button3_Click(object sender, EventArgs e)
        {
            double[] newX = Array.ConvertAll(textBox8.Text.Split(';'), double.Parse);
            double output = O(F(W[W.Count - 1], newX));
            label7.Text = output.ToString() + "," + (output > 0 ? "左" : "右");
        }

        double learn()
        {
            double err = 0;
            for (int k = 1; k <= K; k++)
            {
                double f = F(W[W.Count - 1], X[k]);
                double o = O(f);

                if (o != Y[k])
                {
                    double[] newW = ReW(W[W.Count - 1], X[k], n, Y[k], o);
                    W.Add(newW);
                }
                err = err + Math.Pow(Y[k] - o, 2) * 0.5;
            }

            return err;
        }

        double F(double[] w_arr, double[] x_arr)
        {
            double val = 0;
            for (int i = 0; i < x_arr.Length; i++)
            {
                val += w_arr[i] * x_arr[i];
            }
            return val;
        }
        double O(double val)
        {
            return Math.Sign(val);
        }

        double[] ReW(double[] w_arr, double[] x_arr, double n, double y, double o)
        {
            double[] newW = new double[w_arr.Length];
            double mult = n * (y - o);

            for (int i = 0; i < newW.Length; i++)
            {
                newW[i] = w_arr[i] + mult * x_arr[i];
            }

            return newW;
        }

        void ReadData()
        {
            // 讀取訓練測資
            string[] input = textBox1.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            K = input.Length;
            n = double.Parse(textBox3.Text);
            maxTime = double.Parse(textBox2.Text);

            X.Add(new double[N]);
            Y.Add(0);
            for (int i = 0; i < input.Length; i++)
            {
                double[] temp = Array.ConvertAll(input[i].Split('\t'), double.Parse);
                double[] x = new double[temp.Length - 1];

                for (int j = 0; j < x.Length; j++) x[j] = temp[j];

                X.Add(x);
                Y.Add(temp[temp.Length - 1]);
            }

            // 設定初始權重
            W.Add(Array.ConvertAll(textBox4.Text.Split(';'), double.Parse));
        }

    }
}
