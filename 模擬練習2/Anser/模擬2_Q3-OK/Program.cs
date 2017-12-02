using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 模擬2_Q3
{
    class Program
    {
        static int INF = 999;
        static void Main(string[] args)
        {
            Console.WriteLine("請輸入節點數 連通數");
            string[] temp = Console.ReadLine().Split(' ');

            int size = int.Parse(temp[0]);
            int way = int.Parse(temp[1]);

            int[,] map = new int[size + 1, size + 1];
            int[] vis = new int[size + 1];

            for (int i = 0; i < size + 1; i++)
            {
                for (int j = 0; j < size + 1; j++)
                {
                    map[i, j] = INF;
                }
            }

            for (int i = 0; i < way; i++)
            {
                temp = Console.ReadLine().Split(' ');
                int v1 = int.Parse(temp[0]);
                int v2 = int.Parse(temp[1]);
                int w = int.Parse(temp[2]);

                map[v1, v2] = w;
            }


            Queue<Node> queue = new Queue<Node>();

            queue.Enqueue(new Node(1, 0, null));
            int min = INF;
            Node lastNode = null;

            while (queue.Count > 0)
            {
                Node now = queue.Dequeue();

                if (now.v == 7)
                {
                    if (now.allW < min)
                    {
                        min = now.allW;
                        lastNode = now;
                    }
                }

                for (int i = 1; i <= size; i++)
                {
                    int w = map[now.v, i];
                    if (w != INF && now.CanAdd(i))
                    {
                        queue.Enqueue(new Node(i, w, now));
                    }
                }
            }

            string s1 = "", s2 = "";

            Node n = lastNode;
            while (n != null)
            {
                s1 = $"{n.v} {s1}";
                s2 = $"{n.w} {s2}";
                n = n.father;
            }


            Console.WriteLine($"最短成本:{lastNode.allW}");
            Console.WriteLine($"路徑次序:{s1}");
            Console.WriteLine($"連線數值:{s2}");

            Console.ReadLine();
        }


    }

    class Node
    {
        public int v;
        public int w;
        public int allW = 0;
        public Node father;
        public Node(int v, int w, Node father)
        {
            this.v = v;
            this.w = w;
            if (father != null) this.allW = w + father.allW;
            this.father = father;
        }

        public bool CanAdd(int i)
        {
            Node n = this;
            while (n != null)
            {
                if (n.v == i) return false;
                n = n.father;
            }
            return true;
        }
    }
}
