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
            int lv = (n + 1) / 2;

            bool reverse = radioButton2.Checked;
            bool empty = radioButton4.Checked;

            List<string> str = new List<string>();
            //string test = new string('c', 8);
            string s = "";
            for (int i = 1; i <= n; i += 2)// 第一層1個,第二層3個...
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
