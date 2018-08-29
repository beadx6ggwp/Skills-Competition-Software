using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _96_4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int A = int.Parse(textBox1.Text);
            int B = int.Parse(textBox2.Text);
            int C = int.Parse(textBox3.Text);

            textBox4.Text = $"{GetMod(A, B, C)}";
        }

        long GetMod(int a, int b, int c)
        {
            string strB2 = Convert.ToString(b, 2);
            int k = strB2.Length;

            long s = 1;// 重要
            for (int i = k - 1; i >= 0; i--)
            {
                s = s * s % c;
                if (strB2[k - 1 - i] == '1')// 因為要對應從最高位元到最低位元
                    s = a * s % c;
            }
            return s;
        }
    }
}
