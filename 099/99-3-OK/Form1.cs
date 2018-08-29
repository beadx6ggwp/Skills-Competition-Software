using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _99_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            listView1.View = View.Details;
            listView1.Columns.Add("資料筆數", 100);
            listView1.Columns.Add("輸入資料", 100);
            listView1.Columns.Add("算術平均數", 100);
            listView1.Columns.Add("標準差", 100);
            listView1.Columns.Add("幾何平均數", 100);
            listView1.Columns.Add("均方根平均數", 100);
            listView1.Columns.Add("調和平均數", 100);

            label1.Text = $"輸入第{count}筆資料";

        }

        int count = 0;
        List<double> data = new List<double>();
        List<double> avg = new List<double>();
        private void button1_Click(object sender, EventArgs e)
        {
            double input = double.Parse(textBox1.Text);
            data.Add(input);

            double x = data.Average();
            avg.Add(x);

            double N = data.Count;

            double s = Math.Sqrt((N * data.Sum(n => n * n) - Math.Pow(data.Sum(), 2)) / (N * (N - 1)));
            if (N == 1) s = 0;

            double gm = Math.Pow(mul(data), 1 / N);

            double rmsa = Math.Sqrt(data.Sum(n => n * n) / N);

            double hm = N / data.Sum(n => 1 / n);


            string[] arr = Array.ConvertAll(new double[] { count, input, x, s, gm, rmsa, hm }, n => $"{(Math.Floor(n * 1000) / 1000):0.000}");
            ListViewItem listViewItem = new ListViewItem(arr);
            listView1.Items.Add(listViewItem);

            count++;
            label1.Text = $"輸入第{count}筆資料";
        }

        double mul(List<double> list)
        {
            double n = list[0];
            for (int i = 1; i < list.Count; i++)
                n *= list[i];
            return n;
        }
    }
}
