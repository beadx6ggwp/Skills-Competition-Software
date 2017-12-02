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

namespace _102_4
{
    public partial class Form1 : Form
    {
        Point[] dir = new Point[]
        {
            new Point( 0, -1),
            new Point( 1, -1),
            new Point( 1,  0),
            new Point( 1,  1),
            new Point( 0,  1),
            new Point(-1,  1),
            new Point(-1,  0),
            new Point(-1, -1)
        };
        public Form1()
        {
            InitializeComponent();
            button2.Enabled = false;
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                button2.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap sourceBmp = new Bitmap(pictureBox1.Image);
            Bitmap newBmp = new Bitmap(pictureBox1.Image);

            for (int y = 0; y < sourceBmp.Height; y++)
            {
                for (int x = 0; x < sourceBmp.Width; x++)
                {
                    Color currColor = sourceBmp.GetPixel(x, y);
                    if ((currColor.R + currColor.G + currColor.B) / 3 > 0)
                    {
                        newBmp.SetPixel(x, y, putColor(sourceBmp, x, y));
                    }
                }
            }

            pictureBox1.Image = newBmp;
        }

        Color putColor(Bitmap bmp, int x, int y)// 已知該點為白
        {
            foreach (var p in dir)
            {
                int tx = x + p.X, ty = y + p.Y;

                if (tx < 0 || tx >= bmp.Width || ty < 0 || ty >= bmp.Height) continue;

                Color nc = bmp.GetPixel(tx, ty);
                if ((nc.R + nc.G + nc.B) / 3 < 255)
                    return Color.Black;
            }

            return Color.White;
        }
    }
}
