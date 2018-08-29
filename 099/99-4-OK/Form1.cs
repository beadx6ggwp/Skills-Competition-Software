using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _99_4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            int n = (int)numericUpDown1.Value;
            int lv = (n + 1) / 2;// 先取得當前有幾層，因為假設 n = 5，代表是 1、3、5 三層，剛好是 (n+1) / 2

            bool reverse = radioButton2.Checked;
            bool empty = radioButton4.Checked;

            List<string> str = new List<string>();
            //string test = new string('c', 8);
            string s = "";
            for (int i = 1; i <= n; i += 2)// 第一層1個,第二層3個...
            {
                /*
                 假設n = 5，從第一層(i = 1)開始，想像空白也有印出來，就會像這樣
                 --*--
                 -***-
                 *****
                 
                //  為什麼要 (n-i) 是因為n是我最後一排，i則是我當前星星有機個，所以只要(n-i)就可以知道第一排有4個空白，但我們只要取左邊兩個，所以在除2，這樣就可以知道我們要補2個空白。
                */
                s = "";
                s = s.PadLeft((n - i) / 2, ' ');

                /*
                 而這邊的迴圈就是單存看這是第幾層，就印出幾個星星
                 如果我想要空心，那我就只在每一行的第一個跟最後一個放星星，其他都放空白，這段(j==1 || j == i)? "*" : " ";的意思就是，在第一個跟最後一個放星星，其他都放空白
                 加上我第一排跟最後一排不能是空心，所以我多加一個if (empty && i > 1 && i < n)判斷，這樣。
                 */
                for (int j = 1; j <= i; j++)
                {
                    if (empty && i > 1 && i < n)
                        s += j == 1 || j == i ? "*" : " ";
                    else
                        s += "*";
                }
                str.Add(s);
            }

            if (reverse) str.Reverse();

            textBox1.Text = string.Join("\r\n", str.ToArray());

            /*
             x:空格(PadLeft)
             *:星星
             
             x x x * x x x
             x x * * * x x
             x * * * * * x
             * * * * * * *
             */
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }
    }
}
