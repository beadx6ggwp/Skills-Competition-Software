using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _105_5
{
    public partial class Form1 : Form
    {
        TextBox[] text, times;
        int[] num = new int[4];
        Random ran = new Random();
        public Form1()
        {
            InitializeComponent();
            text = new TextBox[] { textBox1, textBox2, textBox3, textBox4 };
            times = new TextBox[] { textBox5, textBox6, textBox7, textBox8 };
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int max = times.Sum(s => int.Parse(s.Text)) * 2;

            for (int i = 0; i < times.Length; i++) num[i] = int.Parse(times[i].Text);

            Array.Sort(num);
            int newEncoding = num[0] * 3 + num[1] * 3 + num[2] * 2 + num[3] * 1;

            label5.Text = max.ToString();
            label6.Text = newEncoding.ToString();
            label7.Text = $"{max / (double)newEncoding:0.####}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[] n = getRandom(0, 25);
            int[] n2 = getRandom(1, 999);
            for (int i = 0; i < text.Length; i++)
            {
                text[i].Text = Convert.ToChar(n[i] + 97).ToString();
                times[i].Text = n2[i].ToString();
            }
        }

        int[] getRandom(int min, int max)
        {
            int[] arr = new int[] { -1, -1, -1, -1 };
            int val;
            for (int i = 0; i < arr.Length; i++)
            {
                val = ran.Next(min, max + 1);
                if (arr.Contains(val)) i--;
                else arr[i] = val;
            }
            return arr;
        }
    }
}
