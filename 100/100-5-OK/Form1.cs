using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _100_5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox4.Text = solve(double.Parse(textBox1.Text), double.Parse(textBox2.Text), double.Parse(textBox3.Text), textBox4.Text);
        }

        private string solve(double min, double max, double r, string input)// bit to val
        {
            double result = 0;

            double range = (max - min) * Math.Pow(10, r);
            double bitRange = 0;
            double powVal = 0;
            while ((powVal = Math.Pow(2, bitRange)) < range) bitRange++;

            double inputVal;
            if (input.IndexOf(".") == -1)
            {
                inputVal = Convert.ToInt32(input, 2);
                result = min + inputVal * (max - min) / (powVal - 1);
                return (Math.Round(result, (int)r)).ToString();
            }
            else
            {
                inputVal = double.Parse(input);
                result = (inputVal - min) * (powVal - 1) / (max - min);
                return Convert.ToString((int)Math.Round(result), 2);
            }

        }
    }
}
