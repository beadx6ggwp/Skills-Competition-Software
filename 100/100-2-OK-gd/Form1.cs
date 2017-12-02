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
            byte[] ip_arr = Array.ConvertAll(temp[0].Split('.'), byte.Parse);
            Array.Reverse(ip_arr);
            int maskSize = int.Parse(temp[1]);

            int ip = 0, mask = 0, network = 0, broadcast = 0;
            int saveRange = 32 - maskSize;

            // 先轉為32 bit
            for (int i = 0; i < ip_arr.Length; i++) ip |= ip_arr[i] << (i * 8);
            // 計算遮罩
            mask = Int32.MaxValue << saveRange;

            // 計算網路位置
            network = ip & mask;

            // 計算廣播位置
            broadcast = network | (~mask);

            //  計算IP數量
            int num = (int)Math.Pow(2, 32 - maskSize) - 2;

            // 將IP轉回字串後輸出
            textBox2.Text = String.Join(".", Array.ConvertAll(getIPArray(network), s => Convert.ToString(s, 10)));
            textBox3.Text = String.Join(".", Array.ConvertAll(getIPArray(broadcast), s => Convert.ToString(s, 10)));
            textBox4.Text = num.ToString();

        }
        int[] getIPArray(int ip)
        {
            int[] ip_arr = new int[4];
            for (int i = 0; i < 4; i++) ip_arr[i] = ip >> (8 * i) & 0xFF;
            Array.Reverse(ip_arr);// 位置相反所以需要反轉
            return ip_arr;
        }
    }
}
