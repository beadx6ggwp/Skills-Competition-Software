using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _99_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("請輸入N");
            int n = int.Parse(Console.ReadLine());


            List<int> result = new List<int>() { 0 };

            int m = 1;
            while (true)
            {
                result.Clear();
                int index = 1;

                int[] city = new int[n + 1];

                while (result.Count < n)
                {
                    // 如果該城市沒被停電，停掉他，並標記
                    if (city[index] == 0)
                    {
                        city[index] = 1;
                        result.Add(index);
                    }
                    // 繼續往下找m個城市，如果有某個城市已停電了，就跳過他
                    for (int i = 0; i < m; i++)
                    {
                        index++;
                        if (index >= city.Length) index = 1;
                        // 要多一個city.Sum()確認還有城市沒被停電過，否則無限迴圈
                        if (city[index] == 1 && city.Sum() < n) i--;
                    }
                }
                if (result.Last() == 13) break;
                m++;
            }

            Console.WriteLine($"最小間隔M = {m}");
            Console.Read();
        }
    }
}
