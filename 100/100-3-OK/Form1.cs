using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _100_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = textBox2.Text = "0";
        }

        int time = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            time = int.Parse(textBox1.Text) * 60 + int.Parse(textBox2.Text);

            timer1.Interval = 1000;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time--;
            textBox1.Text = (time / 60).ToString();
            textBox2.Text = (time % 60).ToString();
            if (time <= 0)
            {
                timer1.Stop();
                MessageBox.Show("時間到");
            }

        }
    }
}
