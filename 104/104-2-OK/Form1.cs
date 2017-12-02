using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _104_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


        }

        private void button1_Click(object sender, EventArgs e)// go
        {
            string[] inputs = textBox1.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            string[] outputs = new string[inputs.Length];

            for (int i = 0; i < inputs.Length; i++)
            {
                outputs[i] = convert(inputs[i]);
            }

            foreach (var i in outputs)
                textBox2.Text += i + "\r\n";
        }

        string convert(string str)
        {
            string result = "";
            string process = "";
            string last = "";

            string first = str[0].ToString();

            for (int i = 0; i < str.Length; i++)
            {
                string current = getCode(str.Substring(i, 1));

                process += (current == last) ? "0" : current;

                last = current;
            }
            process = process.Remove(0, 1);
            process = process.Insert(0, first);

            process = process.Replace("0", "");

            if (process.Length <= 4)
                process = process.PadRight(4, '0');
            else
                process = process.Substring(0, 4);

            result = process;
            return result;
        }

        string getCode(string str)
        {
            string result = "";

            switch (str)
            {
                case "B":
                case "P":
                case "F":
                case "V":
                    result = "1";
                    break;

                case "C":
                case "S":
                case "K":
                case "G":
                case "J":
                case "Q":
                case "X":
                case "Z":
                    result = "2";
                    break;

                case "D":
                case "T":
                    result = "3";
                    break;

                case "L":
                    result = "4";
                    break;

                case "M":
                case "N":
                    result = "5";
                    break;

                case "R":
                    result = "6";
                    break;
                default:
                    result = "0";
                    break;
            }

            return result;
        }
    }
}
