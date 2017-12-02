using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _105_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int input = int.Parse(textBox1.Text);
            int baseVal = int.Parse(textBox2.Text);

            string result = "";

            while (input != 0)
            {
                //int mod = input % baseVal;
                int div = input / baseVal;
                int mod = Math.Abs(input % baseVal);
                if (input < 0 && Math.Abs(input) < Math.Abs(baseVal))
                {
                    mod = input - baseVal;
                }

                input = (input - mod) / baseVal;

                if (mod > 9)
                    result = Convert.ToChar(65 + mod - 10) + result;
                else
                    result = mod + result;
            }

            textBox3.Text = result;
        }
    }
}
