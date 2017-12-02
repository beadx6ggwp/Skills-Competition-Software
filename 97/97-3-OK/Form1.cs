using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _97_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double input = double.Parse(textBox1.Text);
            int sign = input > 0 ? 0 : 1;
            int integer = Math.Abs((int)(input));
            double fraction = Math.Abs(input % 1);

            string intStr = Convert.ToString(integer, 2);
            string fStr = DoubleToStr(fraction);

            if (1 + intStr.Length + fStr.Length <= 24)
            {
                textBox2.Text = string.Format("{0}{1}.{2}", sign, intStr.PadLeft(15, '0'), fStr.PadRight(8, '0'));
            }
            else
                textBox2.Text = "overflow";
        }
        string DoubleToStr(double val)
        {
            string result = "";
            for (int i = 0; i < 25; i++)
            {
                val *= 2;
                if (val >= 1)
                {
                    val = val % 1;
                    result += "1";
                }
                else
                    result += "0";

                if (val == 0) break;
            }
            return result;
        }
    }
}
