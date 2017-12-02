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
namespace _104_6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)// Open
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "text|*.txt";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    richTextBox1.Text = readText(ofd.FileName);
                }
            }
        }
        string readText(string path)
        {
            string result = "";
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                result = sr.ReadToEnd();
            }
            return result;
        }

        private void button2_Click(object sender, EventArgs e)// Go
        {
            if (textBox1.Text.Length == 0 || textBox1.Text == string.Empty)
            {
                MessageBox.Show("請輸入");
                return;
            }

            int index = 0, count = 0;
            string str = richTextBox1.Text,
                   search = textBox1.Text;

            // 清除顏色
            richTextBox1.Select(0, str.Length);
            richTextBox1.SelectionBackColor = Color.White;

            // 直到找到最後一個
            while (index <= str.LastIndexOf(search))
            {
                // 記錄當前找到的索引
                index = str.IndexOf(search, index);

                // 選取當前索引往後搜尋字的長度，並著色
                richTextBox1.Select(index, search.Length);
                richTextBox1.SelectionBackColor = Color.Yellow;

                // 往後搜尋
                index += +search.Length;

                count++;
            }

            // 輸出個數
            resultNum.Text = count + "";
        }
    }
}
