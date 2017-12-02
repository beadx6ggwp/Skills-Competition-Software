using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _103_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Random ran = new Random();
        sbyte b1, b2, b3;
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = getBit(8);
            textBox2.Text = getBit(8);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            b1 = (sbyte)Convert.ToInt32(textBox1.Text, 2);
            b2 = (sbyte)Convert.ToInt32(textBox2.Text, 2);

            b3 = (sbyte)(b1 + b2);

            this.Text = $"(b1, b2, b1+b2, b3),{b1},{b2},{b1 + b2},{b3}";

            string result = Convert.ToString(b3, 2).PadLeft(8, '0');
            textBox3.Text = result.Substring(result.Length - 8, 8);

            if ((int)b3 != b1 + b2)
                textBox4.Text = "overflow";
            else
                textBox4.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox5.Text = Convert.ToInt32(b1).ToString();
            textBox6.Text = Convert.ToInt32(b2).ToString();
            textBox7.Text = Convert.ToInt32(b3).ToString();

            if (textBox4.Text != "")
                textBox8.Text = "不足位";
            else
                textBox8.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        string getBit(int size)
        {
            string result = "";
            for (int i = 0; i < size; i++)
                result += ran.Next(0, 2);
            return result;
        }
    }
}
