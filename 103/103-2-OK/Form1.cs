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
using System.Diagnostics;
namespace _103_2
{
    public partial class Form1 : Form
    {
        DataManage source = new DataManage();
        DataManage[] datas = new DataManage[3];
        TextBox[] dataSet_textBox;
        public Form1()
        {
            InitializeComponent();
            dataSet_textBox = new TextBox[] { textBox3, textBox4, textBox5 };

            for (int i = 0; i < datas.Length; i++)
                datas[i] = new DataManage();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            // 載入
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                source.InitData(openFileDialog1.FileName);

            // 顯示資料
            foreach (var i in source.list)
                textBox1.Text += $"{i.index}\t{i.w}\t{i.h}\r\n";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 先初始三堆
            for (int i = 0, index = 0; i < source.list.Count; i++)
            {
                datas[index].list.Add(source.list[i]);
                index = (index + 1) % datas.Length;
            }

            // 執行分堆
            Data[] avg_data = new Data[3];
            for (int times = 0; times < 200; times++)
            {
                // 先取得三堆平均
                avg_data[0] = datas[0].getAvg();
                avg_data[1] = datas[1].getAvg();
                avg_data[2] = datas[2].getAvg();

                // 清除，並根據平均點來重新分堆
                Array.ForEach(datas, item => item.list.Clear());

                for (int i = 0; i < source.list.Count; i++)// 尋覽所有點
                {
                    double dist = 0;
                    double minDist = 999999;
                    Data point = null;
                    int dataSet = -1;

                    for (int j = 0; j < avg_data.Length; j++)// 將每個點與三個堆平均點取距離
                    {
                        dist = getDist(source.list[i], avg_data[j]);
                        if (dist < minDist)
                        {
                            // 只要距離更短,就記下這個點與最近的平均點
                            minDist = dist;
                            point = source.list[i];
                            source.list[i].dataset = j;// 更改原資料的分堆位置
                            dataSet = j;
                        }
                    }
                    // 三堆都比較後，將該點加入最近的一堆
                    datas[dataSet].list.Add(point);
                }
            }

            // 顯示完成結果

            // 從原資料堆中搜尋每個資料分別被丟到哪裡
            for (int i = 0; i < source.list.Count; i++)
                textBox2.Text += $"第{i}筆屬於{source.list[i].dataset}堆\r\n";

            for (int i = 0; i < dataSet_textBox.Length; i++)
            {
                foreach (var obj in datas[i].list)
                    dataSet_textBox[i].Text += $"{obj.index}\t{obj.w}\t{obj.h}\r\n";
            }
        }


        private double getDist(Data d1, Data d2)
        {
            double dw = d1.w - d2.w;
            double dh = d1.h - d2.h;
            return Math.Sqrt(dw * dw + dh * dh);
        }
    }
    public class DataManage
    {
        public List<Data> list = new List<Data>();
        public int dataCount = 0;
        public void InitData(string fileName)// 載入資料
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                dataCount = int.Parse(sr.ReadLine().Split(' ')[0]);
                string[] input = sr.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                string[] temp;
                for (int i = 0; i < input.Length; i++)
                {
                    temp = input[i].Split(' ');
                    list.Add(new Data(i, double.Parse(temp[0]), double.Parse(temp[1])));
                }
            }
        }

        public Data getAvg()
        {
            double avg_w = list.Average(item => item.w);
            double avg_h = list.Average(item => item.h);
            return new Data(0, avg_w, avg_h);
        }
    }
    public class Data
    {
        public double w, h;
        public int index, dataset;
        public Data(int index, double w, double h)
        {
            this.index = index;
            this.w = w;
            this.h = h;
        }
    }
}
