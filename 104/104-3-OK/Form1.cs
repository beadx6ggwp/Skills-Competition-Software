using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _104_3
{
    public partial class Form1 : Form
    {
        // Convert.ToInt32("0101",2).ToString(); => 5
        // Convert.ToString(10, 2); => "1010"
        public Form1()
        {
            InitializeComponent();
        }
        Random ran = new Random();
        private void button1_Click(object sender, EventArgs e)
        {
            textBox4.Clear();
            textBox1.Text = getNumber(1);
            textBox2.Text = Convert.ToString(ran.Next(0, 256), 2).PadLeft(8, '0');// 0~255
            textBox3.Text = getNumber(23);

        }
        private void button2_Click(object sender, EventArgs e)
        {
            string[] text = new string[] { textBox1.Text, textBox2.Text, textBox3.Text };

            text[2] = $"1.{text[2].Substring(0, text[2].Length - 1)}";

            double t0 = text[0] == "1" ? -1.0 : 1.0;
            double t1 = Math.Pow(2, (Convert.ToInt32(text[1], 2) - 127));
            double t2 = toDecimal(text[2]);

            double result = t0 * t1 * t2;

            this.Text = $"{t0},{t1},{t2}";
            textBox4.Text = result.ToString();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        double toDecimal(string str)// 二進位小數轉十進位
        {
            str = str.Split('.')[1];
            double result = 1;
            
            for (int i = 1; i <= str.Length; i++)
            {
                result += (1 / Math.Pow(2, i)) * int.Parse(str.Substring(i - 1, 1));
            }
            return result;
        }


        string getNumber(int size)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < size; i++)
            {
                result.Append(ran.Next(0, 2));
            }

            return result.ToString();
        }
    }
}
