using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _102_3
{
    class Program
    {
        static int[,] map;
        static int inf = 999;
        static int size, way, start, end;
        static void Main(string[] args)
        {
            Console.WriteLine("請輸入程式數量、路徑數量EX 7 9");
            string[] temp = Console.ReadLine().Split(' ');
            size = int.Parse(temp[0]);
            way = int.Parse(temp[1]);

            start = 1;
            end = size;

            map = new int[size + 1, size + 1];
            for (int r = 1; r <= size; r++)
                for (int c = 1; c <= size; c++)
                    map[r, c] = r == c ? 0 : inf;

            Console.WriteLine("輸入節點1 節點2 路徑長 EX:1 2 35");

            int p1, p2, len;
            for (int i = 0; i < way; i++)
            {
                temp = Console.ReadLine().Split(' ');
                p1 = int.Parse(temp[0]);
                p2 = int.Parse(temp[1]);
                len = int.Parse(temp[2]);

                map[p1, p2] = len;
            }
            Console.WriteLine("預覽");
            for (int r = 1; r <= size; r++)
            {
                for (int c = 1; c <= size; c++)
                {
                    Console.Write(map[r, c] + "\t");
                }
                Console.Write("\n");
            }

            //-----開始計算Dijkstra----
            int[] dist = new int[size + 1];
            int[] vis = new int[size + 1];
            int[] path = new int[size + 1];
            int min, u = 0, v = 0;// 兩節點最小路徑,搜索點,搜索點的擴展點

            for (int i = 1; i <= size; i++)
            {
                dist[i] = map[start, i];
                path[i] = (dist[i] == inf) ? 0 : start;
                /*
                 * 如果該點距離起點距離不是無限，代表他跟起點接通
                 * 所以要先設定這些點的源頭是起點，否則會指到空白處
                 * 因為離起點最近的節點代表他是最短的，所以在下面更新距離的判斷中不會成立條件
                 * 那麼這些點就沒辦法被設定源頭了，這就是先行設定的用意
                 * 
                 */
            }

            vis[start] = 1;
            path[start] = start;

            for (int i = 1; i <= size - 1; i++)
            {
                min = inf;
                for (int j = 1; j <= size; j++)
                {
                    if (vis[j] == 0 && dist[j] < min)
                    {
                        min = dist[j];
                        u = j;
                    }
                }
                vis[u] = 1;
                for (v = 1; v <= size; v++)
                {
                    int newdist = dist[u] + map[u, v];
                    // 為甚麼不能用 >= 
                    // 因為當u==v時，newdist的值會跟dist[v]一樣，這邊會指到相同節點，
                    // 所以節點就會紀錄源頭為自己，導致無法輸出路徑
                    // 上面預設起點周圍的節點就是因為這個判斷式，因為這判斷式不能動
                    if (dist[v] > newdist)
                    {
                        dist[v] = newdist;
                        path[v] = u;// 紀錄父節點
                    }
                }
            }
            List<int> shortPath = new List<int>();
            List<int> pointLen = new List<int>();

            shortPath.Add(size);//先加入終點
            int next = size;
            while (next != start)//再一一回朔
            {
                next = path[next];
                shortPath.Add(next);
            }

            string str_len = "", resultPath = "";
            str_len += map[start, start] + " ";//先加入起點本身的路徑長度
            for (int i = shortPath.Count - 1; i >= 0; i--)//從起點開始，所以倒過來輸出
            {
                resultPath += shortPath[i] + " ";//取得路徑順序
                if (i > 0)
                {
                    var note = shortPath[i];
                    var next_note = shortPath[i - 1];
                    str_len += map[note, next_note] + " ";//加入起點到下個點的距離
                }
            }
            Console.WriteLine($"最低成本值總和:{dist[size]}");
            Console.WriteLine($"路徑順序:{resultPath}");
            Console.WriteLine($"連線數值:{str_len}");
            Console.Read();
        }
    }
}
