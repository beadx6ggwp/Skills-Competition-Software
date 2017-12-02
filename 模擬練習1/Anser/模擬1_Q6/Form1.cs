using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 模擬1_Q6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[] a = new int[40];
            int[] b = new int[40];
            int[] result = new int[40];
            input(textBox1.Text, ref a);
            input(textBox2.Text, ref b);

            int c = 0;
            int sum = 0;
            for (int i = 0; i < 40; i++)
            {
                sum = a[i] + b[i];
                result[i] = sum % 10 + c;
                c = sum / 10;
            }

            textBox3.Text = output(result);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[] a = new int[40];
            int[] b = new int[40];
            int[] result = new int[40];
            input(textBox1.Text, ref a);
            input(textBox2.Text, ref b);

            bool sign = false;

            int lenA = getLength(a);
            int lenB = getLength(b);

            if (lenA <= lenB && a[lenA] < b[lenB])
            {
                sign = true;
                int[] temp = a;
                a = b;
                b = temp;
            }

            bool flag = false;
            int c = 0;
            int sub = 0;
            for (int i = 0; i < 40; i++)
            {
                sub = a[i] - b[i];

                if (sub < 0)// 處理借位
                {
                    sub = 10 + sub;
                    flag = true;
                }
                result[i] = sub + c;
                c = flag ? -1 : 0;
                flag = false;
            }

            textBox3.Text = (sign ? "-" : "") + output(result);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int[] a = new int[40];
            int[] b = new int[40];
            int[] result = new int[40];
            input(textBox1.Text, ref a);
            input(textBox2.Text, ref b);

            int lenA = getLength(a);
            int lenB = getLength(b);
            for (int i = 0; i < lenA + 1; i++)
            {
                for (int j = 0; j < lenB + 1; j++)
                {
                    result[i + j] += a[i] * b[j];
                }
            }

            int c = 0;
            int val = 0;
            for (int i = 0; i < 40; i++)
            {
                val = result[i];
                result[i] = val % 10 + c;
                c = val / 10;
            }

            textBox3.Text = output(result);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }


        string Reverse(string str)
        {
            return string.Join("", str.Reverse().ToArray());
        }

        void input(string str, ref int[] arr)
        {
            str = Reverse(str);
            for (int i = 0; i < str.Length; i++)
            {
                arr[i] = int.Parse(str.Substring(i, 1));
            }
        }
        string output(int[] arr)
        {
            int i = getLength(arr);

            string str = "";
            for (; i >= 0; i--)
            {
                str += arr[i].ToString();
            }
            return str;
        }
        int getLength(int[] arr)// 取得最高位數的陣列位置
        {
            int i = arr.Length;
            while (arr[--i] == 0) ;
            return i;
        }
    }
}
