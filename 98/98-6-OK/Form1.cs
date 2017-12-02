using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace _98_6
{
    public partial class Form1 : Form
    {
        List<Data> data = new List<Data>();
        public Form1()
        {
            InitializeComponent();

            comboBox1.SelectedIndex = 0;
            for (int i = 1; i <= 32; i++) comboBox2.Items.Add(i);
            comboBox2.SelectedIndex = 23;

            string[] input = File.ReadAllLines("in1.txt");
            foreach (var item in input)
            {
                var temp = item.Split(',');
                data.Add(new Data(temp[0], temp[1], temp[2]));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string inputIP = comboBox1.Text;
            int maskCount = int.Parse(comboBox2.Text);

            int mask = int.MaxValue << (32 - maskCount);

            Debug.WriteLine(Convert.ToString(mask, 2));
            Debug.WriteLine(Convert.ToString(ConvertToIP(inputIP), 2));

            foreach (var item in data)
            {
                if (SameNat(mask, ConvertToIP(inputIP), ConvertToIP(item.ip_str)))
                {
                    textBox1.Text += item.GetMessage() + "\r\n";
                }
            }
        }

        int ConvertToIP(string ip)
        {
            string[] temp = ip.Split('.');
            int[] arr = Array.ConvertAll(temp, int.Parse);
            return arr[0] << 24 | arr[1] << 16 | arr[2] << 8 | arr[3];
        }
        bool SameNat(int mask, int ip1, int ip2)
        {
            return (ip1 & mask) == (ip2 & mask);
        }
    }
    class Data
    {
        Dictionary<string, string> table = new Dictionary<string, string>() { { "A", "000" }, { "B", "100" }, { "C", "110" } };
        public string t, ip_str, text;
        public Data(string t, string ip_str, string text)
        {
            this.t = t;
            this.ip_str = ip_str;
            this.text = text;
            Fix();
        }

        void Fix()
        {
            string s = table[t];
            string[] temp = Array.ConvertAll(ip_str.Split('.'), str => Convert.ToString(int.Parse(str), 2));
            temp[0] = s + temp[0].PadLeft(8, '0').Remove(0, 3);
            ip_str = string.Join(".", Array.ConvertAll(temp, str => Convert.ToInt32(str, 2)));
        }
        public string GetMessage()
        {
            return $"{ip_str}, {text}";
        }
    }
}
