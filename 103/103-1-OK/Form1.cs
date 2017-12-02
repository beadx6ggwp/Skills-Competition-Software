using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _103_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double[] arr = getCheck();

            if (arr.Length > 0)
            {
                textBox7.Text = $"在臺北市的上班族遲到的機率是:{solve1(arr)}";
            }
            else textBox7.Text = "無解";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double[] arr = getCheck();

            if (arr.Length > 0)
            {
                textBox7.Text = $"如果已知有人上班遲到，那他是自己開車的機率是:{solve2(arr)}";
            }
            else textBox7.Text = "無解";
        }

        double solve1(double[] arr)
        {
            return arr[0] * arr[3] + arr[1] * arr[4] + arr[2] * arr[5];
        }

        double solve2(double[] arr)
        {
            return (arr[2] * arr[5]) / solve1(arr);
        }

        double[] getCheck()
        {
            double[] arr = new double[] {
                double.Parse(textBox1.Text),
                double.Parse(textBox2.Text),
                double.Parse(textBox3.Text),
                double.Parse(textBox4.Text),
                double.Parse(textBox5.Text),
                double.Parse(textBox6.Text)
            };

            for (int i = 0; i < arr.Length; i++)
                if (arr[i] < 0 || arr[i] > 1) return new double[0];

            return arr;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
