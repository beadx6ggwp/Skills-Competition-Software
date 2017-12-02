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

namespace 模擬2_Q6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string[][] maze;
        bool flag = false;

        Point start = new Point(0, 0);
        Point End = new Point(7, 7);
        Point[] next = new Point[]
        {
            new Point(0,-1),
            new Point(1,-1),
            new Point(1,0),
            new Point(1,1),
            new Point(0,1),
            new Point(-1,1),
            new Point(-1,0),
            new Point(-1,-1)
        };
        private void button1_Click(object sender, EventArgs e)
        {
            string[] text = File.ReadAllLines(textBox1.Text);

            maze = new string[text.Length][];
            for (int i = 0; i < maze.Length; i++)
            {
                maze[i] = text[i].Split(' ');
            }

            searchPath(start.X, start.Y);
        }


        void searchPath(int x, int y)
        {
            if (flag) return;

            textBox2.Text += $"({y},{x})";

            if (x == End.X && y == End.Y)
            {
                MessageBox.Show("到了");
                flag = true;
                return;
            }

            foreach (var n in next)
            {
                int nx = x + n.X;
                int ny = y + n.Y;
                if (nx < 0 || nx >= maze.Length || ny < 0 || ny >= maze.Length) continue;

                if (maze[ny][nx] == "0")
                {
                    maze[ny][nx] = "2";
                    searchPath(nx, ny);
                }
            }
        }
    }
}
