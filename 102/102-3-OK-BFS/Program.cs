using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _102_3
{
    class Program
    {
        static int size, way;
        static int[,] map;
        static int start = 1, end = 7;
        static List<Node> queue = new List<Node>();
        static void Main(string[] args)
        {
            Console.WriteLine("輸入節點數、連通數");
            string[] temp = Console.ReadLine().Split(' ');
            size = int.Parse(temp[0]);
            way = int.Parse(temp[1]);

            map = new int[size + 1, size + 1];
            for (int i = 0; i < way; i++)
            {
                temp = Console.ReadLine().Split(' ');
                int v = int.Parse(temp[0]);
                int e = int.Parse(temp[1]);
                int len = int.Parse(temp[2]);
                map[v, e] = len;
            }

            // 計算路徑
            int head = 0, tail = 0;// 初始化佇列

            queue.Add(new Node() { pos = start, way = 0, f = 0 });
            tail++;

            while (head < tail)
            {
                Node cur = queue[head];

                for (int i = 1; i <= size; i++)
                {
                    // 因為本題沒有環狀圖，所以只要下有路通往下個頂點，就能加入佇列
                    if (map[cur.pos, i] > 0)
                    {
                        // 探索新節點，並記錄當前路徑長與前一個節點在佇列中的位置
                        queue.Add(new Node()
                        {
                            pos = i,
                            way = cur.way + map[cur.pos, i],
                            f = head// 要記住，父節點要存佇列中的位置，不能存頂點編號，不然回朔時會失敗
                        });
                        tail++;
                    }
                }
                head++;
            }

            // 擷取出有抵達終點的路徑，並從中挑選最短的回朔
            var item = from node in queue
                       where node.pos == end
                       orderby node.way
                       select node;
            Node n = item.ToArray()[0];

            int minLen = n.way;
            List<Node> result = new List<Node>();

            while (n.pos != start)
            {
                result.Add(n);
                n = queue[n.f];
            }
            result.Add(new Node() { pos = start, way = 0, f = 0 });// 最後加入起點

            // 輸出
            
            string str_way = $"{map[start, start]}\t";// 先加入起點本身的長度
            string str_path = "";
            // 因為回朔時，是從終點開始，所以顯示時，要從回朔結果再倒回去
            for (int i = result.Count - 1; i >= 0; i--)
            {
                str_path += $"{result[i].pos}\t";
                if (i > 0)// 取得該點到下個點的距離
                    str_way += $"{map[result[i].pos, result[i - 1].pos]}\t";
            }

            Console.WriteLine($"最低成本總和 : {minLen}");
            Console.WriteLine($"路徑次序 : {str_path}");
            Console.WriteLine($"連線數值 : {str_way}");

            Console.ReadLine();
        }
    }

    struct Node
    {
        public int pos, way, f;
    }
}
