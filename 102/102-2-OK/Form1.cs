using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _102_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Random ran = new Random();
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = ((ran.NextDouble() * 10000)).ToString("#.######");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] input = textBox1.Text.Split('.');
            int val = int.Parse(input[0]);
            int point = input.Length > 1 ? int.Parse(input[1]) : 0;

            if (val < 0 || val >= 10000)
            {
                MessageBox.Show("請輸入正確範圍");
                return;
            }

            string val_result = Convert.ToString(val, 2);
            string point_result = ".";

            int size = (int)Math.Pow(10, point.ToString().Length);// 取得該數的進位值 ex: 561 => 1000
            int count = 0;
            while (point != 0 && count < 10)
            {
                point *= 2;
                if (point >= size)
                {
                    point -= size;
                    point_result += "1";
                }
                else
                {
                    point_result += "0";
                }
                count++;
            }

            // 如果沒有小數點就刪掉"."
            point_result = point_result.Length == 1 ? "" : point_result;

            string fixP = point_result;
            if (fixP.Length > 0)
            {
                while (fixP.Substring(fixP.Length - 1, 1) == "0")
                    fixP = fixP.Remove(fixP.Length - 1, 1);
            }


            // 輸出
            textBox2.Text = val_result + point_result;
            textBox3.Text = val_result + fixP;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
