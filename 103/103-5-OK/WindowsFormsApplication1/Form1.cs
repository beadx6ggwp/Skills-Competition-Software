using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int[] namber = new int[] { 0, 7, 4, 1, 8, 5, 2, 9, 6, 3 };
        int text1, text2, x;
        string text3, y;
        string mail = "@antu.edu.tw";

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string all = null;

            if (textBox1.Text != "" && textBox2.Text != "")
            {
                text1 = int.Parse(textBox1.Text);
                text2 = int.Parse(textBox2.Text);

                for (int i = text1; i <= text2; i++)
                {

                    for (int j = 1; j <= i.ToString().Length; j++)
                    {
                        x += int.Parse(i.ToString().Substring(i.ToString().Length - j, 1)) * (i.ToString().Length - j + 1);
                    }
                    x = (x % 10);
                    y = namber[x].ToString();

                    all += i.ToString() + y + mail + ";" + "\t";
                    x = 0;

                }

            }



            //個別輸入
            if (textBox3.Text != "")
            {
                text3 = textBox3.Text;

                string[] strArray = text3.ToString().Split(' ', ',');

                for (int i = 0; i < strArray.Length; i++)
                {
                    for (int j = 1; j <= strArray[i].ToString().Length; j++)
                    {
                        x += int.Parse(strArray[i].ToString().Substring(strArray[i].ToString().Length - j, 1)) * (strArray[i].ToString().Length - j + 1);
                    }

                    x = (x % 10);
                    y = namber[x].ToString();

                    all += strArray[i] + y + mail + ";" + "\t";
                    x = 0;
                }
            }

            //最後輸出
            textBox4.Text += all.Remove(all.Length - 2, 2);

        }
    }
}
