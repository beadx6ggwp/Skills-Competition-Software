using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _102_5
{
    public partial class Form1 : Form
    {
        TextBox[] tb;
        Random ran = new Random();
        int m = 0, n = 0;
        public Form1()
        {
            InitializeComponent();
            tb = new TextBox[] { textBox1, textBox2, textBox3 };// m, n, result
            makeAns();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //check
            int input;
            if (!Int32.TryParse(tb[2].Text, out input))
            {
                MessageBox.Show("答案不可為空");
                return;
            }

            if (m * n == input)
            {
                label1.Text = "Very Good";
                return;
            }

            m = int.Parse(tb[0].Text);
            n = int.Parse(tb[1].Text);

            int m0 = m % 10;// 取個位數
            int m1 = m / 10;// 取十位數
            int n0 = n % 10;
            int n1 = n / 10;

            // 剩下就照題目要求給答案
            int a1 = m + n0;
            int a2 = a1 * m1 * 10;
            int a3 = m0 * n0;
            int a4 = a2 + a3;

            StringBuilder str = new StringBuilder();
            str.AppendLine($"(1) {m}+{n0}={a1}");
            str.AppendLine($"(2) {a1}x{m1 * 10}={a2}");
            str.AppendLine($"(3) {m0}x{n0}={a3}");
            str.AppendLine($"(4) {a2}+{a3}={a4}");
            label1.Text = str.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // clear
            Array.ForEach(tb, item => item.Clear());
            makeAns();
        }
        void makeAns()
        {
            // 題目只有兩種11x11~19x19 || 21x21~29x29
            m = ran.Next(11, 30);
            n = m < 20 ? ran.Next(11, 20) : ran.Next(20, 30);

            tb[0].Text = m.ToString();
            tb[1].Text = n.ToString();
        }
    }
}
