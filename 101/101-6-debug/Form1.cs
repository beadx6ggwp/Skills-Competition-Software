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
using System.Threading;

namespace _101_6
{
    public partial class Form1 : Form
    {
        Graphics g;


        string[] inputs;
        string[][] map;// [r][c]
        bool[,] vis;
        bool flag = false;
        Point start, end;
        List<Point> plist = new List<Point>();
        int nowIndex = 0;

        Point[] dir = new Point[]
            {
                new Point( 0,-1),
                new Point( 1,-1),
                new Point( 1, 0),
                new Point( 1, 1),
                new Point( 0, 1),
                new Point(-1, 1),
                new Point(-1, 0),
                new Point(-1,-1)
            };
        public Form1()
        {
            InitializeComponent();
            g = panel1.CreateGraphics();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamReader sr = File.OpenText(textBox2.Text);
            inputs = sr.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);


            map = new string[inputs.Length][];
            for (int i = 0; i < map.Length; i++)
                map[i] = inputs[i].Split(' ');

            start = new Point(0, 0);
            end = new Point(map[0].Length - 1, map.Length - 1);

            vis = new bool[map[0].Length, map.Length];

            solve(0, 0);
            foreach (var i in plist) textBox1.Text += $"({i.X},{i.Y})";


            timer1.Interval = 500;
            timer1.Start();
        }


        void solve(int c, int r)
        {
            plist.Add(new Point(c, r));

            if (c == end.X && r == end.Y)
            {
                flag = true;
                return;
            }

            for (int i = 0; i < dir.Length; i++)
            {
                int tc = c + dir[i].X;
                int tr = r + dir[i].Y;
                if (tc < 0 || tc > end.X || tr < 0 || tr > end.Y) continue;

                if (map[tr][tc] == "0" && !vis[tr, tc])
                {
                    vis[tr, tc] = true;
                    solve(tc, tr);
                }
                if (flag) return;
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            g.FillRectangle(Brushes.Black, plist[nowIndex].X * 20, plist[nowIndex].Y * 20, 20, 20);
            nowIndex++;

            if (nowIndex < plist.Count)
                g.FillRectangle(Brushes.Red, plist[nowIndex].X * 20, plist[nowIndex].Y * 20, 20, 20);

            if (nowIndex >= plist.Count) timer1.Stop();
        }
    }
}
