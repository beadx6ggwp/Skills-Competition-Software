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

namespace _98_4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string[] input;

        private void button1_Click(object sender, EventArgs e)
        {
            string[] temp = new string[0];
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Directory.GetCurrentDirectory();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                temp = File.ReadAllText(ofd.FileName).Split(' ');
            }

            input = new string[35];//34+1
            Array.Copy(temp, 0, input, 1, temp.Length);// 讓資料從第一個開始
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double[] src = Array.ConvertAll(input, n => n == null ? 0 : double.Parse(n));
            List<Data> data = new List<Data>();
            for (int i = 20; i < src.Length; i++)
            {
                Data d = new Data(src[i]);
                for (int j = 0; j < 5; j++) d.a5 += src[i - j] / 5.0;
                for (int j = 0; j < 20; j++) d.a20 += src[i - j] / 20.0;
                d.sub = d.a5 - d.a20;
                d.x = (4 * src[i - 4] - src[i - 19]) / 3.0;

                if (d.val == 0) d.a5 = d.a20 = d.sub = 0;

                data.Add(d);
            }

            foreach (var item in data)
            {
                textBox1.Text += $"{item.val:00.00} ";
                textBox2.Text += $"{item.a5:00.00} ";
                textBox3.Text += $"{item.a20:00.00} ";
                textBox4.Text += $"{item.sub:00.00} ";
                textBox5.Text += $"{item.x:00.00} ";
            }
        }

        class Data
        {
            public double val, a5, a20, sub, x;
            public Data(double val) { this.val = val; }
        }
    }
}
