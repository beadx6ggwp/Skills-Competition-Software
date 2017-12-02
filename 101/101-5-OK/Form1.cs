using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _101_5
{
    public partial class Form1 : Form
    {
        Timer timer = new Timer();
        Random ran = new Random();
        TextBox[][] tb;// 0:source, 1:goal
        Panel[] panels;

        byte[] goal = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 0 };
        byte[] source;
        public Form1()
        {
            InitializeComponent();

            panels = new Panel[] { panel1, panel2 };
            tb = new TextBox[2][];
            for (int i = 0; i < 2; i++)
            {
                tb[i] = new TextBox[9];
                for (int j = 0; j < 9; j++)
                {
                    tb[i][j] = new TextBox()
                    {
                        Location = new Point(j % 3 * 40, j / 3 * 40),
                        Size = new Size(30, 30),
                        Multiline = true,
                        Font = new Font("consolas", 16),
                        TextAlign = HorizontalAlignment.Center
                    };
                    panels[i].Controls.AddRange(tb[i]);
                    this.Controls.Add(panels[i]);
                }
            }

            for (int i = 0; i < goal.Length; i++)
                tb[1][i].Text = goal[i].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 0 };
            
            for (int i = 0; i < 100; i++)
                swap(ref arr[ran.Next(0, 9)], ref arr[ran.Next(0, 9)]);

            for (int i = 0; i < arr.Length; i++)
                tb[0][i].Text = arr[i].ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            labResult.Text = "計算中...";
            this.Refresh();

            long startTime = Environment.TickCount;
            List<Node> path = Solve(TextBoxToArr(tb[0]), TextBoxToArr(tb[1]));
            double times = (Environment.TickCount - startTime) / 1000.0;

            if (path != null)
                labResult.Text = $"Steps : {path.Count}\nTime : {times:0.##}s";
            else
                labResult.Text = $"無解";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            labResult.Text = "計算中...";
            this.Refresh();

            long startTime = Environment.TickCount;
            List<Node> path = Solve(TextBoxToArr(tb[0]), TextBoxToArr(tb[1]));
            double times = (Environment.TickCount - startTime) / 1000.0;

            if (path != null)
                labResult.Text = $"Steps : {path.Count}\nTime : {times:0.##}s";
            else
            {
                labResult.Text = $"無解";
                return;
            }


            int count = path.Count;

            timer = new Timer();
            timer.Interval = 1000 / 2;

            timer.Start();
            timer.Tick += (s, evt) =>
            {
                for (int i = 0; i < 9; i++)
                {
                    string str = path[0].status[i].ToString();
                    tb[0][i].Text = str == "0" ? "" : str;
                }

                path.RemoveAt(0);
                labCountDown.Text = $"步驟:{path.Count}/{count}";

                if (path.Count <= 0)
                {
                    timer.Stop();
                    int index = Array.IndexOf<byte>(goal, 0);
                    tb[0][index].Text = "0";
                    labCountDown.Text += "\n完成";
                    return;
                }
            };
        }
        byte[] TextBoxToArr(TextBox[] t)
        {
            byte[] arr = new byte[t.Length];
            for (int i = 0; i < 9; i++)
                arr[i] = byte.Parse(t[i].Text);
            return arr;
        }

        // 傳入原本的版面和目標版面，回傳最短路徑
        List<Node> Solve(byte[] source, byte[] goal)
        {
            Queue<Node> queue = new Queue<Node>();
            SortedList<int, bool> book = new SortedList<int, bool>();// 儲存已走過的路徑，防止走回頭路

            Node end = new Node(goal, null);// 終點
            Node start = new Node(source, null);// 起點

            queue.Enqueue(start);// 推入起點
            book.Add(start.ToSequence(), true);// 標示起點已走過

            int endStatus = end.ToSequence();
            while (queue.Count > 0)
            {
                Node now = queue.Dequeue();// 取得當前搜索狀態，並移出佇列

                // 如果抵達終點，那就輸出路徑
                if (now.ToSequence() == endStatus)
                    return PathTrace(now);

                // 取得能走的位置
                List<Node> nextPath = GetNext(now);                                
                foreach (var path in nextPath)
                {
                    int sign = path.ToSequence();
                    // 判斷當前狀態是否走過了
                    if (!book.Keys.Contains(sign))
                    {
                        queue.Enqueue(path);
                        book.Add(sign, true);
                    }
                }
            }

            // 如果窮舉完都沒找到，代表無解
            return null;
        }
        List<Node> GetNext(Node now)// 傳入當前版面，回傳0所有移動後的狀態
        {
            int index = Array.IndexOf<byte>(now.status, 0);
            int col = index % 3;
            int row = index / 3;

            List<Node> nextPush = new List<Node>();
            byte[] next;

            if (row != 0) // Top
            {
                next = (byte[])now.status.Clone();
                swap(ref next[index], ref next[index - 3]);
                nextPush.Add(new Node(next, now));
            }

            if (col != 2) // Right
            {
                next = (byte[])now.status.Clone();
                swap(ref next[index], ref next[index + 1]);
                nextPush.Add(new Node(next, now));
            }

            if (row != 2) // Bottom
            {
                next = (byte[])now.status.Clone();
                swap(ref next[index], ref next[index + 3]);
                nextPush.Add(new Node(next, now));
            }

            if (col != 0) // Left
            {
                next = (byte[])now.status.Clone();
                swap(ref next[index], ref next[index - 1]);
                nextPush.Add(new Node(next, now));
            }
            return nextPush;
        }
        List<Node> PathTrace(Node now)
        {
            // 回朔路徑
            List<Node> path = new List<Node>();
            while (now.father != null)
            {
                path.Add(now);
                now = now.father;
            }
            path.Reverse();
            return path;
        }

        void swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }
    }

    public class Node
    {
        public byte[] status;
        public Node father;// 如果father = null 為父節點
        public Node(byte[] status, Node father)
        {
            this.status = status;
            this.father = father;
        }

        public int ToSequence()// 把陣列轉換成數字序列，較好比對
        {
            int result = 0;
            for (int i = 0; i < status.Length; i++)
                result = result * 10 + status[i];

            return result;
        }
    }
}
