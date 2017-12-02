using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _101_1
{
    public partial class Form1 : Form
    {
        Pen pen = new Pen(Color.Black, 2);
        int size = 200;
        int gap = 12,       // 線段之間的間距
            offsetGap = 20, // 多出的長度
            offsetX = 100,  // 繪製的左上角座標
            offsetY = 100;  // ...

        public Form1()
        {
            InitializeComponent();
            // 保持置中
            offsetX = (panel1.Width - size) / 2;
            offsetY = (panel1.Height - size) / 2;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int input1 = int.Parse(textBox1.Text);// 被乘數
            int input2 = int.Parse(textBox2.Text);// 乘數

            int[] in1 = new int[] { input1 / 10, input1 % 10 };
            int[] in2 = new int[] { input2 / 10, input2 % 10 };

            Graphics g = panel1.CreateGraphics();
            g.Clear(BackColor);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            //pen.DashPattern = new float[] { 10, 10 }; 設定虛線
            //pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;// 設定實線

            // 先將畫布旋轉為題目指定的角度
            g.TranslateTransform(panel1.Width / 2, panel1.Height / 2);
            g.RotateTransform(-45);
            g.TranslateTransform(-panel1.Width / 2, -panel1.Height / 2);


            // 被乘數的10位數在上
            pen.Color = Color.Red;
            // 先畫虛線
            pen.DashPattern = new float[] { 2, 2 };
            g.DrawLine(pen,
                offsetX, (gap * 0) + offsetY + offsetGap,
                offsetX + size, (gap * 0) + offsetY + offsetGap);
            // 在畫實線
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            for (int i = 0; i < in1[0]; i++)
            {
                g.DrawLine(pen,
                    offsetX, (gap * i) + offsetY + offsetGap,
                    offsetX + size, (gap * i) + offsetY + offsetGap);
            }

            // 被乘數的個位數在下
            pen.DashPattern = new float[] { 2, 2 };
            g.DrawLine(pen,
                offsetX, size - (gap * 0) + offsetY - offsetGap,
                offsetX + size, size - (gap * 0) + offsetY - offsetGap);

            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            for (int i = 0; i < in1[1]; i++)
            {
                g.DrawLine(pen,
                    offsetX, size - (gap * i) + offsetY - offsetGap,
                    offsetX + size, size - (gap * i) + offsetY - offsetGap);
            }

            // 不用判斷是否虛線，因為只要輸入是0，就不會觸發迴圈，當輸入大於0時，就可直接用實線覆蓋
            // 乘數也是一樣

            pen.Color = Color.Blue;

            // 乘數的10位數在左
            pen.DashPattern = new float[] { 2, 2 };
            g.DrawLine(pen,
                offsetX + gap * 0 + offsetGap, offsetY,
                offsetX + gap * 0 + offsetGap, offsetY + size);

            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            for (int i = 0; i < in2[0]; i++)
            {
                g.DrawLine(pen,
                    offsetX + gap * i + offsetGap, offsetY,
                    offsetX + gap * i + offsetGap, offsetY + size);
            }
            // 乘數的個位數在左
            pen.DashPattern = new float[] { 2, 2 };
            g.DrawLine(pen,
                offsetX + size - (gap * 0) - offsetGap, offsetY,
                offsetX + size - (gap * 0) - offsetGap, offsetY + size);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            for (int i = 0; i < in2[1]; i++)
            {
                g.DrawLine(pen,
                    offsetX + size - (gap * i) - offsetGap, offsetY,
                    offsetX + size - (gap * i) - offsetGap, offsetY + size);
            }


        }
    }
}
