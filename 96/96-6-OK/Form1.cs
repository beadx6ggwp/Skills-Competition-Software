using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _96_6
{
    public partial class Form1 : Form
    {
        /*
            解密
            9907 68269
            18407627003178645118
            output=> 公開金鑰

            9907 68269
            2854212172437551217228542
            output=> 12321
         */
        Encoding encoding = Encoding.UTF8;
        Encoding big5 = Encoding.GetEncoding("big5");
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int input1 = int.Parse(textBox1.Text),
                input2 = int.Parse(textBox2.Text);

            if (sender == button1) textBox4.Text = Encrypt(textBox3.Text, input1, input2);
            if (sender == button2) textBox4.Text = Decrypt(textBox3.Text, input1, input2);
        }
        string Encrypt(string data, int E, int N)
        {
            string result = "";
            // 僅限UniCode(UTF16)，這邊是BIG5，剛好可以用來驗證
            // 當輸入是中文時，test的平均數值就會>127(ASCII範圍)，因為Unicode相容ASCII，代表只要沒有對應到ASCII就是輸入中文
            // 因為中文是2Byte，Convert.ToInt32(s)時，會超過65535(2Byte);
            // 因為題目沒有處理中英合併，暫時這樣就夠，等有空在來處理中英交錯的狀況
            int[] test = Array.ConvertAll(data.ToCharArray(), s => Convert.ToInt32(s));
            byte[] arr = Encoding.Default.GetBytes(data.ToCharArray());
            double check = test.Average();

            long c = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (check < 127)
                    c = GetMod(arr[i], E, N);
                else
                {
                    c = GetMod(arr[i] << 8 | arr[i + 1], E, N);
                    i++;
                }

                result += $"{c:00000}";//補位的位數應該是對應N的位數
            }

            return result;
        }
        string Decrypt(string data, int D, int N)
        {
            string result = "";

            for (int i = 0; i < data.Length; i += 5)
            {
                long m = GetMod(int.Parse(data.Substring(i, 5)), D, N);
                if (m < 127)
                {
                    result += Convert.ToChar(m);
                }
                else
                {
                    result += big5.GetString(new byte[] { (byte)(m >> 8), (byte)m });
                }
            }

            return result;
        }

        long GetMod(int a, int b, int c)
        {
            string strB2 = Convert.ToString(b, 2);
            int k = strB2.Length;

            long s = 1;// 用long防止溢位
            for (int i = k - 1; i >= 0; i--)
            {
                s = s * s % c;
                if (strB2[k - 1 - i] == '1')
                    s = a * s % c;
            }
            return s;
        }
    }
}
