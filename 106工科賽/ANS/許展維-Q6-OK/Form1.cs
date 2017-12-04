using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 許展維_Q6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            for (int i = 1; i < 5000; i++)
            {
                if (isNum(i))
                {
                    table.Add(i);
                }
            }
        }
        List<int> table = new List<int>();
        private void button1_Click(object sender, EventArgs e)
        {
            int input = int.Parse(textBox1.Text);

            long[] result = Solve(input);

            string str1 = "";
            int len = getLen(result);
            for (int i = 0; i <= len; i++)
            {
                str1 += result[i].ToString();
            }
            var carr = str1.ToCharArray();
            Array.Reverse(carr);
            str1 = string.Join("", carr);//
            textBox2.Text = str1;

            long count = 0;//
            for (int i = 0; i < carr.Length; i++)
            {
                count += carr[i] - '0';
            }
            textBox3.Text = count.ToString();

            // 分解input
            int[] arr = new int[input];


            int all = 0;
            for (int i = 1; i <= input; i++)
            {
                if (input == 1) { all = 1; break; }
                for (int j = 0; j < table.Count; j++)
                {
                    int num = table[j];
                    if (num > i) break;

                    if (i % num == 0)
                    {
                        int t = i;
                        do
                        {
                            t /= num;
                            all++;
                        } while (t > 1 && t % num == 0);
                    }
                }
            }
            textBox4.Text = all.ToString();
        }

        bool isNum(int n)//植樹判斷
        {
            if (n == 1) return false;
            if (n == 2) return true;
            for (int i = 2; i < n; i++)
            {
                if (n % i == 0) return false;
            }

            return true;
        }

        long[] Solve(int n)
        {

            long[] m1 = toArr(1);
            for (int i = 1; i <= n; i++)
            {
                long[] m2 = toArr(i);

                m1 = mult(m1, m2);
            }


            return m1;
        }

        long[] mult(long[] m1, long[] m2)
        {
            long[] ret = new long[600];

            for (int i = 0; i < 300; i++)
            {
                for (int j = 0; j < 300; j++)
                {
                    ret[i + j] += m1[i] * m2[j];
                }
            }

            long c = 0;
            for (int i = 0; i < 600; i++)
            {
                ret[i] += c;
                c = ret[i] / 10;
                ret[i] = ret[i] % 10;
            }

            return ret;
        }
        long[] toArr(int n)
        {
            long[] arr = new long[300];

            int i = 0;
            while (n != 0)
            {
                arr[i] = n % 10;
                n /= 10;
                i++;
            }

            return arr;
        }

        int getLen(long[] arr)
        {
            int len = arr.Length - 1;

            while (arr[len] == 0) len--;


            return len;
        }
    }
}
