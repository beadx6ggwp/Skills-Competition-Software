using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 許展維_Q2
{
    class Program
    {
        static void Main(string[] args)
        {
            int num = 0;
            do
            {
                num = int.Parse(getInput("請輸入行程數量(MAX 5):"));
            } while (num > 5);

            List<Obj> list = new List<Obj>();

            Console.WriteLine("請輸入行程執行時間:");
            for (int i = 0; i < num; i++)
            {
                int t = int.Parse(getInput($"P{i + 1}:"));
                list.Add(new Obj($"P{i + 1}", t));
            }


            int waitT = int.Parse(getInput("輸入時間配額:"));

            Queue<Obj> queue = new Queue<Obj>();
            string str = "";

            int index = 0;// 0 1 2 0 1 2
            int time = 0;
            while (true)
            {
                Obj item = list[index];

                if (item.time >= waitT)
                {
                    item.start.Add(time);
                    str += $"{time:00}:{item.name}\t";

                    foreach (var it in list)
                    {
                        if (it != item && it.time > 0)
                        {
                            it.wait += waitT;
                        }
                    }

                    time += waitT;
                    item.time -= waitT;

                }
                else if (item.time > 0) // < waitT
                {
                    item.start.Add(time);
                    str += $"{time:00}:{item.name}\t";

                    foreach (var it in list)
                    {
                        if (it != item && it.time > 0)
                        {
                            it.wait += item.time;
                        }
                    }

                    time += item.time;
                    item.time -= item.time;

                }

                queue.Enqueue(item);
                index++;
                index = index % num;

                int count = 0;
                foreach (var it in list)
                {
                    count += it.time;
                }

                if (count == 0) break;
            }

            str += $"{time:00}:{queue.Peek().name}";

            Console.WriteLine(str);

            string str2 = "";
            for (int i = 0; i < list.Count; i++)
            {
                var it = list[i];
                str2 += $"{it.name}等待時間:{it.wait}\t";
            }
            Console.WriteLine();
            Console.WriteLine(str2);


            Console.Read();
        }


        static string getInput(string title)
        {
            Console.Write(title);
            return Console.ReadLine();
        }
    }

    class Obj
    {
        public int maxTime;
        public int time;
        public int wait;
        public List<int> start = new List<int>();
        public string name;
        public Obj(string name, int t)
        {
            time = t;
            maxTime = t;
            wait = 0;

            this.name = name;
        }
    }
}
