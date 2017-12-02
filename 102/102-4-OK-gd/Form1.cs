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

namespace 侵蝕膨脹
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Image img;
        Graphics g;
        OpenFileDialog ofd;
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "點擊空白處加入開啟圖片   z擴散,x侵蝕,c二值化";
            ofd = new OpenFileDialog();
            ofd.Filter = "PNG;JPG;JPEG|*.png;*.jpg;*.jpeg";
            ofd.InitialDirectory = Directory.GetCurrentDirectory();
        }
        private void Form1_Click(object sender, EventArgs e)
        {
            
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                img = Image.FromFile(ofd.FileName);
                if (img.Width > 480 && img.Height > 380)
                {
                    this.Location = new Point(0, 0);
                    this.Size = new Size(img.Width, img.Height);
                }
                g = this.CreateGraphics();
                this.Text += ", W = " + img.Width + " H = " + img.Height;
                draw(img);
            }
        }
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (img == null) return;
            if (e.KeyChar == 'z')
            {
                img = expend(new Bitmap(img));
                draw(img);
            }
            if (e.KeyChar == 'x')
            {
                img = corrode(binarization(new Bitmap(img)));
                draw(img);
            }
            if (e.KeyChar == 'c')
            {
                img = binarization(new Bitmap(img));
                draw(img);
            }
        }

        void draw(Image pic){ g.DrawImage(pic, 0, 0, this.Width, this.Height); }

        public Bitmap binarization(Bitmap bitmap)
        {
            Bitmap bitImage = bitmap;//二值化
            Color c;
            int height = bitmap.Height;
            int width = bitmap.Width;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    c = bitImage.GetPixel(j, i);
                    int r = c.R;
                    int g = c.G;
                    int b = c.B;
                    if ((r + g + b) / 3 >= 127)
                    {
                        bitImage.SetPixel(j, i, Color.FromArgb(255, 255, 255));
                    }
                    else
                    {
                        bitImage.SetPixel(j, i, Color.FromArgb(0, 0, 0));
                    }
                }
            }
            return bitImage;
        }

        public bool[] getRoundPixel(Bitmap bitmap, int x, int y)//傳回(x,y)附近的像素，黑色=True
        {
            bool[] pixels = new bool[8];
            Color c;
            int num = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    c = bitmap.GetPixel(x + i, y + j);
                    if (i != 0 || j != 0)
                    {
                        if (255 == c.G)//圖為黑白，所以只要检查RGB中一个属性的值(如原圖為彩色，需先二值化)
                        {
                            pixels[num] = false;//白色false
                            num++;
                        }
                        else if (0 == c.G)
                        {
                            pixels[num] = true;//黑色true
                            num++;
                        }
                    }
                }
            }
            return pixels;
        }
        public Bitmap expend(Bitmap bitmap)
        {
            Bitmap bitImage = new Bitmap(bitmap);//處理前
            Bitmap bitImage1 = new Bitmap(bitmap);//處理後
            int height = bitmap.Height;
            int width = bitmap.Width;
            bool[] pixels;
            for (int i = 1; i < width - 1; i++)
            {
                for (int j = 1; j < height - 1; j++)
                {

                    if (bitImage.GetPixel(i, j).R != 0)
                    {
                        pixels = getRoundPixel(bitImage, i, j);
                        for (int k = 0; k < pixels.Length; k++)
                        {
                            if (pixels[k] == true)
                            {
                                //set this piexl's color to black
                                bitImage1.SetPixel(i, j, Color.FromArgb(0, 0, 0));
                                break;
                            }
                        }
                    }
                }
            }
            return bitImage1;

        }
        public Bitmap corrode(Bitmap bitmap)
        {
            Bitmap bitImage = new Bitmap(bitmap);//處理前
            Bitmap bitImage1 = new Bitmap(bitmap);//處理後
            int height = bitmap.Height;
            int width = bitmap.Width;
            Color c;
            bool[] pixels;
            for (int i = 1; i < width - 1; i++)
            {
                for (int j = 1; j < height - 1; j++)
                {
                    c = bitImage.GetPixel(i, j);
                    if (bitImage.GetPixel(i, j).R == 0)
                    {
                        pixels = getRoundPixel(bitImage, i, j);
                        for (int k = 0; k < pixels.Length; k++)
                        {
                            if (pixels[k] == false)
                            {
                                //set black
                                bitImage1.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                                break;
                            }
                        }
                    }
                }
            }
            return bitImage1;
        }

    }
}
