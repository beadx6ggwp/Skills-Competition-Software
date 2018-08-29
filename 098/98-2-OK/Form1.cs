using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _98_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            int s = getCount(0, "122233", '1');
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int n = int.Parse(textBox1.Text) + 1;// 1 < n <= 10
            textBox2.Clear();

            List<string> list = new List<string>();

            list.Add("1");
            for (int i = 1; i < n; i++)
            {
                string result = "";
                string str = list[i - 1];

                for (int j = 0; j < str.Length; j++)
                {
                    int count = getCount(j, str, str[j]);// 計算有幾個重複的
                    result += $"{count}{str[j]}";

                    j += count - 1;// 往後加上重複的數量，但要先-1，因為迴圈會再幫我們+1
                }


                list.Add(result);
            }

            textBox2.Text = string.Join("\r\n", list.ToArray());
        }
        int getCount(int index, string str, char c)
        {
            int count = 0;
            for (int i = index; i < str.Length; i++)
            {
                if (str[i] == c) count++;
                else break;
            }
            return count;
        }
    }
}
