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
        int text1, text2, modNum, checkLen = 8;
        string text3, checkNum;
        string mail = "@antu.edu.tw";
        private void button1_Click(object sender, EventArgs e)
        {
            textBox4.Clear();
            string all = null, str = "";
            int getNumber = 0;

            if (textBox1.Text != "" && textBox2.Text != "")
            {
                text1 = int.Parse(textBox1.Text);
                text2 = int.Parse(textBox2.Text);

                for (int i = text1; i <= text2; i++)
                {
                    str = i.ToString();
                    for (int j = 1; j <= checkLen; j++)
                    {
                        getNumber = str[checkLen - j] - '0';
                        modNum += getNumber * (checkLen - j + 1);
                    }
                    modNum = (modNum % 10);
                    checkNum = namber[modNum].ToString();

                    all += i.ToString() + checkNum + mail + ";" + "\t";
                    modNum = 0;

                }

            }



            //個別輸入
            if (textBox3.Text != "")
            {
                text3 = textBox3.Text;

                string[] strArray = text3.ToString().Split(' ', ',');

                for (int i = 0; i < strArray.Length; i++)
                {
                    str = strArray[i].ToString();
                    for (int j = 1; j <= checkLen; j++)
                    {
                        getNumber = str[checkLen - j] - '0';
                        modNum += getNumber * (checkLen - j + 1);
                    }

                    modNum = (modNum % 10);
                    checkNum = namber[modNum].ToString();

                    all += strArray[i] + checkNum + mail + ";" + "\t";
                    modNum = 0;
                }
            }

            //最後輸出
            textBox4.Text += all.Remove(all.Length - 2, 2);

        }
    }
}
