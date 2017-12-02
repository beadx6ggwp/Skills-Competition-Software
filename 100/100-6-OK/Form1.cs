using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _100_6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string[] UNIT_STRING = new string[] { "KB", "MB", "GB", "TB" };
        int[] UNIT_POW = new int[] { 10, 20, 30, 40 };
        Random ran = new Random();
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = ran.Next(16, 53).ToString();
            textBox2.Text = ran.Next(1, 9).ToString() + "B";
            textBox3.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox3.Text = Fconv(int.Parse(textBox1.Text), int.Parse(textBox2.Text.Replace("B", "")));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox4.Text = Rconv(int.Parse(textBox5.Text.Replace("B", "")), textBox6.Text).ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox4.Text = "";
            textBox5.Text = ran.Next(1, 9).ToString();
            textBox6.Text = Fconv(ran.Next(16, 53), ran.Next(1, 9));
        }

        string Fconv(int bus, int bpa)
        {
            int max = UNIT_POW[UNIT_POW.Length - 1];
            int quotient = bus / 10;
            int remainder = bus % 10;
            // 超過換算上限( > 2^40)
            if (bus >= max) remainder = bus - max;

            return (Math.Pow(2, remainder) * bpa).ToString() + UNIT_STRING[Math.Min(quotient, UNIT_STRING.Length) - 1];
        }

        int Rconv(int bpa, string ms)
        {
            string unit = ms.Substring(ms.Length - 2);
            int val = int.Parse(ms.Substring(0, ms.Length - 2));
            int pow = 0;

            for (int i = 0; i < UNIT_STRING.Length; i++)
            {
                if (UNIT_STRING[i] == unit)
                    pow = UNIT_POW[i];
            }
            // 相除後，他是2的幾次方 (val/bpa) < 2^n
            // pow + n 就是答案
            int quotient = val / bpa;
            while (quotient > 0)
            {
                quotient >>= 1;
                pow++;
            }
            return pow;
        }
    }
}
