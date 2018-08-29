using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace _97_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string input = textBox1.Text.Replace("-", "");
            int i;
            if (!int.TryParse(input, out i))
            {
                MessageBox.Show("輸入錯誤");
                return;
            }

            textBox2.Text = GetISBN10(input);
            textBox3.Text = GetISBN13(input);
        }

        string GetISBN10(string input)
        {
            string result = input;

            int[] arr = Array.ConvertAll(input.ToCharArray(), s => s - '0');
            int val = 0;
            for (int i = 0; i < 9; i++)
                val += arr[i] * (10 - i);

            int N = 11 - val % 11;
            if (N == 10) result += "X";
            else if (N == 11) result += "0";
            else result += N;

            result = result.Substring(0, 3) + "-" +
                     result.Substring(3, 4) + "-" +
                     result.Substring(7, 2) + "-" +
                     result.Substring(9, 1);

            return result;
        }
        string GetISBN13(string input)
        {
            string result = "978" + input;

            int[] arr = Array.ConvertAll(result.ToCharArray(), s => s - '0');

            int val = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                int n = i % 2 == 0 ? 1 : 3;
                val += arr[i] * n;
            }
            result += 10 - val % 10;

            result = result.Substring(0, 3) + "-" +
                     result.Substring(3, 3) + "-" +
                     result.Substring(6, 4) + "-" +
                     result.Substring(10, 2) + "-" +
                     result.Substring(12, 1);

            return result;
        }
    }
}
