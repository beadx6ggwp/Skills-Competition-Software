using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 模擬2_Q5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // input : 1000101110110101000111
        private void button1_Click(object sender, EventArgs e)
        {
            double min = double.Parse(textBox1.Text);
            double max = double.Parse(textBox2.Text);

            double pre = double.Parse(textBox3.Text);

            double val = get2((max - min) * Math.Pow(10, pre));


            string input = textBox4.Text;
            double result = 0;
            if (input.IndexOf(".") != -1) // 有小數點
            {
                result = ((double.Parse(input) - min) * (val - 1.0)) / (max - min);
                textBox5.Text = Convert.ToString((int)Math.Round(result), 2);
            }
            else // 沒有
            {
                long x = Convert.ToInt64(input, 2);
                result = min + x * ((max - min) / (val - 1));
                textBox5.Text = Math.Round(result, (int)pre).ToString();
            }

        }


        double get2(double val)
        {
            int n = 1;
            while (n < val) n <<= 1;

            return n;
        }
    }
}
