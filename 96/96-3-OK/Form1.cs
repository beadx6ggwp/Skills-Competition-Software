using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _96_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double R = double.Parse(textBox1.Text);
            double G = double.Parse(textBox2.Text);
            double B = double.Parse(textBox3.Text);

            double r = R / (R + G + B);
            double g = G / (R + G + B);
            double b = B / (R + G + B);

            double h, s, i;
            double theta = Math.Acos((0.5 * ((r - g) + (r - b))) / Math.Pow((Math.Pow(r - g, 2) + (r - b) * (g - b)), 0.5));

            if (b <= g) h = theta;
            else h = Math.PI * 2 - theta;

            s = 1 - 3 * Math.Min(r, Math.Min(g, b));

            i = (R + G + B) / (3 * 255);

            textBox4.Text = $"{h * 180 / Math.PI}";
            textBox5.Text = $"{s * 255}";
            textBox6.Text = $"{i * 255}";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double H = double.Parse(textBox4.Text);
            double S = double.Parse(textBox5.Text);
            double I = double.Parse(textBox6.Text);

            double h = H * Math.PI / 180;
            double s = S / 255;
            double i = I / 255;

            if (Math.PI * 2 / 3 <= h && h < Math.PI * 4 / 3) h = h - Math.PI * 2;
            if (Math.PI * 4 / 3 <= h && h < Math.PI * 2) h = h - Math.PI * 4 / 3;

            double x, y, z, r = 0, g = 0, b = 0;

            x = i * (1 - s);
            y = i * (1 + (s * Math.Cos(h)) / (Math.Cos(Math.PI / 3 - h)));
            z = 3 * i - (x + y);

            if (h < Math.PI * 2 / 3)
            {
                b = x;
                r = y;
                g = z;
            }
            else if (h < Math.PI * 4 / 3)
            {
                r = x;
                g = y;
                b = z;
            }
            else if (h < Math.PI * 2)
            {
                g = x;
                b = y;
                r = z;
            }

            textBox1.Text = $"{Math.Round(r * 255)}";
            textBox2.Text = $"{Math.Round(g * 255)}";
            textBox3.Text = $"{Math.Round(b * 255)}";
        }
    }
}
