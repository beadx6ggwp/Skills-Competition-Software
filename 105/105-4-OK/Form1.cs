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

namespace _105_4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            string s = "1234123412341234";
            s.Insert(1, "q");
        }
        //：
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Directory.GetCurrentDirectory();
            if (ofd.ShowDialog() == DialogResult.OK)
                textBox1.Text = File.ReadAllText(ofd.FileName, Encoding.UTF8);


            string input = textBox1.Text;
            string result = "";

            string[] data = input.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < data.Length; i++)
            {
                if (data[i].Contains("??"))
                    data[i] = data[i].Replace("?", "");

                if (data[i].Contains("：："))// 87美國
                    data[i] = data[i].Remove(data[i].IndexOf("："), 1);

                // 87課程
                if (StrCount(data[i], @"/") == 4)// 如果這個字串有4個/，代表是87課程
                {
                    // 移除課程進度 )
                    data[i] = data[i].Remove(data[i].Length - 1, 1);
                    // 移除課程進度 (
                    int index = data[i].LastIndexOf("(");
                    data[i] = data[i].Remove(index, 1);
                    // 加入換行
                    data[i] = data[i].Insert(index, "\r\n");

                    data[i] = data[i].Replace("/", "\r\n") + "\r\n";
                    result += data[i];
                    continue;
                }

                // 下行有:就換行
                if (i + 1 < data.Length && data[i + 1].Contains("："))
                {
                    result += data[i] + "\r\n";
                    continue;
                }

                result += data[i] + " ";
            }



            textBox2.Text = result;
        }

        int StrCount(string str, string i)// 計數這個字串中有幾個i字元
        {
            int count = 0;
            int index = 0;
            int startIndex = 0;
            while ((index = str.IndexOf(i, startIndex)) != -1)
            {
                count++;
                startIndex = index + i.Length;
            }
            return count;
        }
    }
}
