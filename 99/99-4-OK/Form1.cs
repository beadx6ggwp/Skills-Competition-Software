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
        bool reverse = false;
        bool empty = false;
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            int n = int.Parse(textBox2.Text);
            int lv = (n + 1) / 2;

            List<string> str = new List<string>();
            string test = new string('c', 8);
            string s = "";
            for (int i = 1; i <= n; i += 2)
            {
                s = "";
                s = s.PadLeft((n - i) / 2, ' ');
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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            reverse = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            reverse = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            empty = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            empty = false;
        }
    }
}
