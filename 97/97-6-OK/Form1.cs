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
        bool status = false;
        string symbol = "";
        string num = "";

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
                    if (status)// 用來確認是否再輸入第二個值
                    {
                        num = textBox1.Text;// 將原本數值保存
                        textBox1.Text = "";
                        status = false;
                    }
                    textBox1.Text += (s as Button).Text;
                };
            }

            foreach (var item in btn_action) item.Click += Action_Click;
        }

        private void button11_Click(object sender, EventArgs e)// +/-
        {
            textBox1.Text = (double.Parse(textBox1.Text) * -1).ToString();
        }

        private void button12_Click(object sender, EventArgs e)// .
        {
            if (textBox1.Text.IndexOf(".") == -1) textBox1.Text += ".";
        }

        private void button13_Click(object sender, EventArgs e)// log
        {
            textBox1.Text = Math.Log10(double.Parse(textBox1.Text)).ToString();
        }
        private void button14_Click(object sender, EventArgs e)// AC
        {
            textBox1.Text = "0";

            symbol = "";
            num = "";
            status = false;
        }

        private void button19_Click(object sender, EventArgs e)// = 
        {
            textBox1.Text = Calc(symbol, num, textBox1.Text).ToString();
            label1.Text = "=";

            symbol = "";
            num = "";
            status = false;
        }

        private void Action_Click(object sender, EventArgs e)// +-x/
        {
            label1.Text = symbol = (sender as Button).Text;
            // 如果以儲存一個數字,則下次按下操作鍵時,先將之前的值進行運算,達到連續運算的效果
            if (num != "" && textBox1.Text != "")
                textBox1.Text = Calc(symbol, num, textBox1.Text).ToString();

            status = true;
        }


        double Calc(string s, string a, string b)
        {
            double a1 = double.Parse(a), b1 = double.Parse(b);
            switch (s)
            {
                case "+": return a1 + b1;
                case "-": return a1 - b1;
                case "x": return a1 * b1;
                case "/": return a1 / b1;
            }
            return 0;
        }
    }
}
