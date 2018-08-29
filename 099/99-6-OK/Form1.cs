using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _99_6
{
    public partial class Form1 : Form
    {
        Label[] labSum;
        Label[][] lab;
        PictureBox[][] pic;

        int people = 0;// A, B
        int count = 0;
        int[][] price = new int[][] {
            new int[] { 75, 70, 65, 80 },
            new int[] { 45, 40, 50, 40 },
            new int[] { 45, 40, 40, 50 },
            new int[] { 40, 45, 40, 35 }
        };
        int[,] select = new int[4, 4];
        public Form1()
        {
            InitializeComponent();
            labSum = new Label[] { label9, label10 };
            lab = new Label[][]
            {
                new Label[]{ label1,label2,label3,label4},
                new Label[]{ label5,label6,label7,label8}
            };
            pic = new PictureBox[][]
            {
                new PictureBox[]{ pictureBox1,pictureBox2,pictureBox3,pictureBox4},
                new PictureBox[]{ pictureBox5, pictureBox6, pictureBox7, pictureBox8 }
            };
        }
        void Calc(int type, int index, string name)
        {
            if (people > 2) return;

            lab[people][type].Text = $"{name}({price[type][index]})";

            select[type, index] = 1;

            int sum = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (select[i, j] == 1) sum += price[i][j];
                }
            }

            labSum[people].Text = $"合計:{sum}";

            count++;
            if (count == 4)
            {
                people++;
                select = new int[4, 4];
            }
        }

        void ToolStripMenuItem0_click(object sender, EventArgs e)
        {
            ToolStripMenuItem Sender = sender as ToolStripMenuItem;
            int index = int.Parse(Sender.Tag.ToString());
            Calc(0, index, Sender.Text);
        }
        void ToolStripMenuItem1_click(object sender, EventArgs e)
        {
            ToolStripMenuItem Sender = sender as ToolStripMenuItem;
            int index = int.Parse(Sender.Tag.ToString());
            Calc(1, index, Sender.Text);
        }
        void ToolStripMenuItem2_click(object sender, EventArgs e)
        {
            ToolStripMenuItem Sender = sender as ToolStripMenuItem;
            int index = int.Parse(Sender.Tag.ToString());
            Calc(2, index, Sender.Text);
        }
        void ToolStripMenuItem3_click(object sender, EventArgs e)
        {
            ToolStripMenuItem Sender = sender as ToolStripMenuItem;
            int index = int.Parse(Sender.Tag.ToString());
            Calc(3, index, Sender.Text);
        }
    }
}
