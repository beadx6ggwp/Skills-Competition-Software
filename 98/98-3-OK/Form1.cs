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

namespace _98_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Bitmap bmp;
        int curr, max;
        double time;
        private void button1_Click(object sender, EventArgs e)
        {
            FileStream fs = null;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Directory.GetCurrentDirectory();

            if (ofd.ShowDialog() == DialogResult.OK)
                fs = File.Open(ofd.FileName, FileMode.Open);
            else
                return;
            

            byte[] riff = GetData(fs, 0, 4);
            byte[] WAVEfmt = GetData(fs, 0x8, 7);
            byte[] PCM = GetData(fs, 0x14, 2);
            byte[] NOC = GetData(fs, 0x16, 2);
            byte[] SR = GetData(fs, 0x18, 4);
            byte[] BPS = GetData(fs, 0x22, 2);
            byte[] data = GetData(fs, 0x24, 4);
            byte[] NOS = GetData(fs, 0x28, 4);

            int sr = BitConverter.ToInt32(SR, 0);
            int nos = BitConverter.ToInt32(NOS, 0);// 陣列第0組是最低位元
            byte[] sound = GetData(fs, 0x2C, nos);

            if (ByteToStr(riff) != "RIFF" || ByteToStr(WAVEfmt) != "WAVEfmt" || BitConverter.ToInt16(PCM, 0) != 1 ||
                BitConverter.ToInt16(NOC, 0) != 1 || BitConverter.ToInt16(BPS, 0) != 8 ||
                ByteToStr(data) != "data")
            {
                MessageBox.Show("輸入的檔案不是RIFF、WAVEfmt、PCM格式、8位元及單聲道");
                return;
            }

            bmp = new Bitmap(nos, pictureBox1.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.Black);

            for (int i = 0; i < nos; i++)
            {
                int diff = Math.Abs(sound[i] - 0x80);
                g.DrawLine(Pens.Green, i, 150 + diff, i, 150 - diff);
            }

            hScrollBar1.Maximum = max = nos;
            hScrollBar1.Minimum = 0;
            hScrollBar1.Value = curr = 0;

            pictureBox1.Image = bmp;

            time = (double)nos / sr;

            label1.Text = $"目前位置:{curr}";
            label2.Text = $"聲音長度:{time}";
        }

        byte[] GetData(FileStream fs, int start, int count)
        {
            byte[] arr = new byte[count];
            fs.Seek(start, SeekOrigin.Begin);
            fs.Read(arr, 0, count);
            return arr;
        }
        string ByteToStr(byte[] arr)
        {
            string s = "";
            foreach (var item in arr)
                s += Convert.ToChar(item);
            return s;
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                curr = hScrollBar1.Value;
                pictureBox1.CreateGraphics().DrawImage(bmp, -curr, 0);

                label1.Text = $"目前位置{(double)curr / max * time}";
            }
        }
    }
}
