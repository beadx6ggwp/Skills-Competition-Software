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

namespace 模擬1_Q2
{
    public partial class Form1 : Form
    {
        List<Data> list = new List<Data>();
        TextBox[] tb;
        public Form1()
        {
            InitializeComponent();
            tb = new TextBox[] { textBox3, textBox4, textBox5 };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Directory.GetCurrentDirectory();

            string[] arr = new string[0];
            if (ofd.ShowDialog() == DialogResult.OK)
                arr = File.ReadAllLines(ofd.FileName);

            string[] temp;
            Data d;
            for (int i = 1; i < arr.Length; i++)
            {
                temp = arr[i].Split(' ');
                d = new Data(double.Parse(temp[0]), double.Parse(temp[1])) { index = i - 1 };
                list.Add(d);
                textBox1.Text += d.ToString() + "\r\n";
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<Data>[] three = new List<Data>[] { new List<Data>(), new List<Data>(), new List<Data>() };

            for (int i = 0; i < list.Count; i++)// 隨便初始3堆
                three[i % 3].Add(list[i]);

            for (int i = 0; i < 200; i++)
            {
                Data[] avg3 = new Data[3];// 取得三堆平均
                for (int j = 0; j < 3; j++)
                {
                    double avgW = three[j].Average(item => item.w);
                    double avgH = three[j].Average(item => item.h);
                    avg3[j] = new Data(avgW, avgH);
                }

                for (int j = 0; j < 3; j++) three[j].Clear();

                // 分三堆
                for (int j = 0; j < list.Count; j++)
                {
                    Data now = list[j];
                    // 先找出距離最近的一堆
                    int minIndex = 0;
                    double min = 999999;
                    for (int k = 0; k < 3; k++)
                    {
                        double dist = getDist(now, avg3[k]);
                        if (dist < min)
                        {
                            minIndex = k;
                            min = dist;
                        }
                    }
                    now.newIndex = minIndex;
                    three[minIndex].Add(now);
                }
            }

            // 分配完畢，顯示資料

            for (int i = 0; i < list.Count; i++)
            {
                Data d = list[i];
                textBox2.Text += $"第{d.index}筆屬於{d.newIndex}堆" + "\r\n";
            }
            for (int i = 0; i < 3; i++)
            {
                var li = three[i];
                for (int j = 0; j < li.Count; j++)
                {
                    tb[i].Text += li[j].ToString() + "\r\n";
                }
            }
        }


        double getDist(Data d1, Data d2)
        {
            double dx = d1.w - d2.w;
            double dy = d1.h - d2.h;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }

    class Data
    {
        public int index;
        public int newIndex;
        public double w, h;
        public Data(double w, double h)
        {
            this.w = w;
            this.h = h;
        }
        public override string ToString()
        {
            return $"{index}\t{w}\t{h}";
        }
    }
}
