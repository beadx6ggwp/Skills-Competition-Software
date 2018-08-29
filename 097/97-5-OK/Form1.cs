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

namespace _97_5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] input = File.ReadAllLines(textBox1.Text);

            int start = 9 - 1;
            // 從陣列從0開始
            double[] high = Array.ConvertAll(input[0].Split(' '), double.Parse);
            double[] low = Array.ConvertAll(input[1].Split(' '), double.Parse);
            double[] final = Array.ConvertAll(input[2].Split(' '), double.Parse);
            double[] K = new double[12];
            double[] D = new double[12];

            K[7] = double.Parse(textBox3.Text);
            D[7] = double.Parse(textBox4.Text);


            for (int n = start; n < high.Length; n++)
            {
                double min = 999, max = 0;
                for (int i = 0; i < 9; i++)
                {
                    min = Math.Min(min, low[n - i]);
                    max = Math.Max(max, high[n - i]);
                }
                double RSV = (final[n] - min) / (max - min) * 100;
                // 盡量不要動到原資料，這邊只是為了方便
                K[n] = Math.Round(K[n - 1] * 2 / 3 + RSV / 3, 2);
                D[n] = Math.Round(D[n - 1] * 2 / 3 + K[n] / 3, 2);
            }


            string result = string.Join(" ", K) + "\r\n" + string.Join(" ", D);

            File.WriteAllText(textBox2.Text, result);

            MessageBox.Show("成功執行");
        }
    }
}
