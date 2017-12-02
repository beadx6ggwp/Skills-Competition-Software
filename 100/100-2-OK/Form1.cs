using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _100_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] temp = textBox1.Text.Split('/');
            byte[] ip = Array.ConvertAll(temp[0].Split('.'), byte.Parse);
            int maskSize = int.Parse(temp[1]);

            byte[] network = new byte[4];
            byte[] broadcast = new byte[4];

            int savePos = maskSize / 8;
            int plusPos = maskSize % 8;

            // 計算網路位置

            // 確定保留的欄位
            for (int i = 0; i < savePos; i++) network[i] = ip[i];
            // 需要捨去的欄位，先右移清掉後再左移回去
            network[savePos] = (byte)(ip[savePos] >> (8 - plusPos) << (8 - plusPos));


            // 計算廣播位置

            int range = 32 - maskSize;
            // 確定設為1的欄位
            for (int i = ip.Length - 1; i > savePos; i--) broadcast[i] = 255;
            //剩下用|=處理需要1的個數
            for (int i = 0; i < 8 - plusPos; i++) broadcast[savePos] |= (byte)((2 << i) | 1);
            // 最後合併
            for (int i = 0; i < ip.Length; i++) broadcast[i] |= network[i];


            // 計算可用IP數
            int num = (int)Math.Pow(2, 32 - maskSize) - 2;

            // 結果
            textBox2.Text = String.Join(".", network);
            textBox3.Text = String.Join(".", broadcast);
            textBox4.Text = num.ToString();
        }
    }
}
