using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace 模擬2_Q4
{
    public partial class Form1 : Form
    {
        Button[] btn;
        WebBrowser wb = new WebBrowser();
        public Form1()
        {
            InitializeComponent();
            btn = new Button[] { button1, button2, button3, button4, button5, button6, button7, button8, button9, button10,
                button11, button12, button13, button14, button15, button16, button17 };

            foreach (var item in btn)
            {
                item.Click += (s, e) =>
                {
                    Button b = (Button)s;
                    textBox1.Text += b.Text;
                };
            }


            wb.DocumentText = @"<script>function calc(num){return eval(num)}</script>";
        }

        private void button18_Click(object sender, EventArgs e)
        {
            object result = wb.Document.InvokeScript("calc", new object[] { textBox1.Text });
            if (result != null)
            {
                File.WriteAllText("b.txt", $"{textBox1.Text} = {result}");
            }
            else
            {
                MessageBox.Show("運算式有誤");
            }
        }
    }
}
