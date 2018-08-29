using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _99_1
{
    public partial class Form1 : Form
    {
        Random ran = new Random();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string input = textBox1.Text;
            List<string> data = new List<string>();


            int count = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '1')
                {
                    data.Add(Convert.ToString(count, 2));
                    count = 0;
                    continue;
                }

                count++;
            }
            if (input[input.Length - 1] != '1')
            {
                count = input.Length - 1 - input.LastIndexOf("1");
                data.Add(Convert.ToString(count, 2));
            }

            string result = string.Join(" ", data.ToArray());
            textBox2.Text = result;
            label4.Text = $"{(double)result.Length / (double)input.Length * 100}";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[] arr = new int[40];
            arr[0] = arr[1] = arr[2] = arr[3] = 1;

            for (int i = 0; i < 100; i++)
            {
                int a = ran.Next(0, 40), b = ran.Next(0, 40);
                int t = arr[a];
                arr[a] = arr[b];
                arr[b] = t;
            }

            textBox1.Text = string.Join("", arr);
        }
    }
}
