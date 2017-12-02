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

namespace 工科101_6
{
    public partial class Form1 : Form
    {
        OpenFileDialog ofd;
        public Form1()
        {
            InitializeComponent();
            ofd = new OpenFileDialog();
            ofd.InitialDirectory = Directory.GetCurrentDirectory();
            ofd.Filter = "txt files|*.txt|All files (*.*)|*.*";
        }

        int[][] maze;
        int[][] book;
        bool finish = false;
        int sx = 0, sy = 0;
        int ex = 7, ey = 7;
        int min = 999;
        List<Point> list = new List<Point>();
        private void FindPath()
        {
            //[r][c]
            book = new int[maze.Length][];
            for (int r = 0; r < maze.Length; r++)
                book[r] = new int[maze[0].Length];

            if (maze[sy][sx] == 1 || maze[ey][ex] == 1)
            {
                textBox3.Text = "無解";
                return;
            }

            book[sy][sx] = 1;
            dfs(sx, sy, 0);

            foreach (var s in list)
            {
                textBox3.Text += $"({s.X},{s.Y})";
            }
        }
        private void dfs(int x,int y,int step)
        {
            //[0]=y, [1]=x
            int[,] next = new int[,]
            {
                    { -1,0 },
                    { -1,1 },
                    { 0,1 },
                    { 1,1 },
                    { 1,0 },
                    { 1,-1 },
                    { 0,-1 },
                    { -1,-1 }
            };
            int tx = 0, ty = 0;
            int row = maze.Length, col = maze[0].Length;
            

            if (x == ex && y == ey)
            {
                if (step < min)
                    min = step;
                finish = true;
                label4.Text = $"輸出:({x},{y}) \n第一條路徑長{min}";
                return;
            }

            for (int way = 0; way < next.GetLength(0); way++)
            {
                ty = y + next[way, 0];
                tx = x + next[way, 1];

                if (ty < 0 || ty > row - 1 || tx < 0 || tx > col - 1) 
                    continue;

                if (maze[ty][tx] == 0 && book[ty][tx] == 0)
                {
                    // 走到第一個路徑就完成
                    if (finish) return;

                    book[ty][tx] = 1;
                    list.Add(new Point(tx, ty));
                    dfs(tx, ty, step + 1);
                    book[ty][tx] = 0;
                }
            }

            return;
        }

        private void OpenFileBtn_Click(object sender, EventArgs e)
        {
            string path = string.Empty;
            if (textBox1.Text != string.Empty)
            {
                MessageBox.Show($"開啟{textBox1.Text}");
                path = textBox1.Text;
            }
            else if (ofd.ShowDialog() == DialogResult.OK)
            {
                path = ofd.FileName;
            }
            textBox1.Text = ofd.FileName;
            textBox2.Text = OpenTxt(path);

            // 以換行符號分隔，並以不規則陣列存取
            string[] text = textBox2.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            maze = new int[text.Length][];
            for (int r = 0; r < maze.Length; r++)
            {
                maze[r] = Array.ConvertAll(text[r].Split(' '), int.Parse);
            }
        }

        private void CalcBtn_Click(object sender, EventArgs e)
        {
            // reset
            list.Clear();
            finish = false;
            textBox3.Clear();
            min = 9999;

            if (textBox4.Text != string.Empty)
            {
                string[] s = textBox4.Text.Split(',');
                sx = int.Parse(s[0]);
                sy = int.Parse(s[1]);
                ex = int.Parse(s[2]);
                ey = int.Parse(s[3]);
            }
            label3.Text = $"設定:({sx},{sy}), ({ex},{ey})";

            FindPath();
        }
        private string OpenTxt(string path)
        {
            string result = string.Empty;
            if (File.Exists(path))
            {
                StreamReader sr = new StreamReader(path, Encoding.UTF8);
                result = sr.ReadToEnd();
                sr.Close();
            }
            return result;
        }
    }
}
