using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 模擬1_Q3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Random ran = new Random();
        private void button2_Click(object sender, EventArgs e)
        {
            string Ss = textBox1.Text;
            string Es = textBox2.Text;
            string Ms = $"1.{textBox3.Text}";

            int S = int.Parse(Ss) == 1 ? -1 : 1;

            double E = Math.Pow(2, Convert.ToInt32(Es, 2) - 127);

            double M = ToNumber(Ms);


            double result = S * E * M;
            textBox4.Text = result.ToString();
        }

        double ToNumber(string str)
        {
            str = str.Split('.')[1];
            double result = 1;

            for (int i = 0; i < str.Length; i++)
            {
                int s = int.Parse(str[i].ToString());
                result += Math.Pow(2, -(i + 1)) * s;
            }


            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = getRandomBit(1);
            textBox2.Text = getRandomBit(8);
            textBox3.Text = getRandomBit(23);
        }

        string getRandomBit(int num)
        {
            string result = "";
            for (int i = 0; i < num; i++)
            {
                result += ran.Next(2);
            }
            return result;
        }
    }
}
