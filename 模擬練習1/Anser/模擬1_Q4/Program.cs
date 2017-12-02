using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace 模擬1_Q4
{
    class Program
    {
        static double[] up, mid, down, input;
        static double[] result;
        static double avg = 0;
        static void Main(string[] args)
        {
            string choose = "";
            do
            {
                Console.WriteLine("選擇操作項目");
                Console.WriteLine("\t<1>輸入模型資料");
                Console.WriteLine("\t<2>計算平均相似度");
                Console.WriteLine("\t<3>顯示個資料相似度");

                choose = getInput("請選擇:");

                switch (choose)
                {
                    case "1":
                        Func1();
                        break;

                    case "2":
                        Func2();
                        break;

                    case "3":
                        Func3();
                        break;
                }

            } while (getInput("繼續:請按1，結束:請按0:") == "1");
        }

        static string getInput(string title)
        {
            Console.Write($"{title}");
            return Console.ReadLine();
        }

        static void Func1()
        {
            string path = getInput("輸入「資料模型」檔名(Model.txt):");
            string[] temp = File.ReadAllLines(path);
            up = Array.ConvertAll(temp[0].Split(' '), double.Parse);
            mid = Array.ConvertAll(temp[1].Split(' '), double.Parse);
            down = Array.ConvertAll(temp[2].Split(' '), double.Parse);
        }
        static void Func2()
        {
            string path = getInput("輸入「資料串列」檔名(in1.txt):");
            string temp = File.ReadAllText(path);

            input = Array.ConvertAll(temp.Split(' '), double.Parse);

            result = new double[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] <= mid[i])
                    result[i] = getAns(down[i], mid[i], input[i]);
                else
                    result[i] = getAns(up[i], mid[i], input[i]);
            }

            avg = result.Average();

            Console.Write($"平均相似度為:{avg:0.000000}\n");
        }
        static void Func3()
        {
            Console.Write($"資料相似度:{string.Join(" ", result)}\n");
        }
        static double getAns(double min, double max, double val)
        {
            if (Math.Min(min, max) <= val && val <= Math.Max(min, max))
                return (val - min) / (max - min) * 1.0;
            else
                return 0;
        }
    }
}
