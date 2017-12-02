using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _97_6
{
    public partial class Form1 : Form
    {
        Button[] btn_num, btn_action;
        string symbol = "";
        double num = 0;

        public Form1()
        {
            InitializeComponent();

            textBox1.Text = "0";

            btn_num = new Button[] { button1, button2, button3, button4, button5, button6, button7, button8, button9, button10 };
            btn_action = new Button[] { button15, button16, button17, button18 };

            foreach (var item in btn_num)
            {
                item.Click += (s, e) =>
                {
                    if (textBox1.Text == "0") textBox1.Clear();
                    textBox1.Text += (s as Button).Text;
                };
            }
            foreach (var item in btn_action)
            {
                item.Click += Action_Click;
            }
        }


        private void button11_Click(object sender, EventArgs e)// +/-
        {
            if (double.Parse(textBox1.Text) > 0) textBox1.Text = "-" + textBox1.Text;
            else if (double.Parse(textBox1.Text) < 0) textBox1.Text = textBox1.Text.Remove(0, 1);
        }

        private void button12_Click(object sender, EventArgs e)// .
        {
            if (textBox1.Text.IndexOf(".") == -1)
                textBox1.Text += ".";
        }

        private void button13_Click(object sender, EventArgs e)// log
        {
            textBox1.Text = Math.Log10(double.Parse(textBox1.Text)).ToString();
        }
        private void button14_Click(object sender, EventArgs e)// AC
        {
            symbol = "";
            num = 0;
            textBox1.Text = "0";
        }

        private void button19_Click(object sender, EventArgs e)// = 
        {
            textBox1.Text = Calc(symbol, num, double.Parse(textBox1.Text)).ToString();
            num = 0;
        }

        private void Action_Click(object sender, EventArgs e)// +-x/
        {
            symbol = (sender as Button).Text;
            num += double.Parse(textBox1.Text);
            textBox1.Clear();
        }


        double Calc(string s, double a, double b)
        {
            switch (s)
            {
                case "+": return a + b;
                case "-": return a - b;
                case "x": return a * b;
                case "/": return a / b;
            }
            return 0;
        }
    }
}
