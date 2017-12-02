using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _104_4
{
    class Program
    {
        static int[][] dataset;
        static int[] input_data;
        static double[] output_data;
        static int num;
        static void Main(string[] args)
        {
            while (true)
            {
                string choose = GetStartChoose();
                selector(choose);



                if (GetEndChoose() == "0")
                    break;
            }



            WL("按下任鍵結束");
            Console.Read();
        }

        static void solve()
        {
            output_data = new double[num];

            for (int i = 0; i < num; i++)
            {
                int left = dataset[2][i];// min
                int mid = dataset[1][i];// mid
                int right = dataset[0][i];// max

                int val = input_data[i];

                if (val < left || val > right)
                {
                    output_data[i] = 0;
                    continue;
                }

                if (val <= mid)// left
                {
                    output_data[i] = map(left, mid, val);
                }
                else// right
                {
                    output_data[i] = map(right, mid, val);
                }
            }
        }

        static double map(int min, int max, int val)
        {
            return (double)(val - min) / (double)(max - min);

        }

        static void function1()
        {
            W("輸入模型資料總筆數:");
            num = int.Parse(RL());

            dataset = new int[3][];

            W("    序列( x軸):");
            RL();

            W("數值串列(上限):");
            dataset[0] = Array.ConvertAll(RL().Split(' '), int.Parse);

            W("數值串列(中心):");
            dataset[1] = Array.ConvertAll(RL().Split(' '), int.Parse);

            W("數值串列(下限):");
            dataset[2] = Array.ConvertAll(RL().Split(' '), int.Parse);
        }

        static void function2()
        {
            // Debug測試用，手動輸入太拉基
            if (dataset == null)
            {
                string text = readTxt("InputModel.txt");
                string[] data_arr = text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                dataset = new int[data_arr.Length][];
                for (int i = 0; i < data_arr.Length; i++)
                {
                    dataset[i] = Array.ConvertAll(data_arr[i].Split(' '), int.Parse);
                }

                num = dataset[0].Length;
            }


            W("請輸入「資料串列」檔名:");
            string path = RL();

            input_data = Array.ConvertAll(readTxt(path).Split(' '), int.Parse);
            WL($"已開啟「資料串列」檔名:{path}");

            solve();

            double average = Math.Round(output_data.Average(), 6);
            WL($"平均相似度為:{average}");
        }

        static void function3()
        {
            WL("顯示各資料相似度");

        }



        static string GetStartChoose()
        {
            WL("請選擇操作項目");
            WL("\t<1>輸入模型資料:");
            WL("\t<2>計算平均相似度:");
            WL("\t<3>顯示各資料相似度:");
            W("請選擇:");

            return RL();
        }
        static string GetEndChoose()
        {
            WL("");
            W("繼續:請按1，結束:請按0:");

            return RL();
        }

        static void selector(string choose)
        {
            switch (choose)
            {
                case "1":
                    function1();
                    break;

                case "2":
                    function2();
                    break;

                case "3":
                    function3();
                    break;
            }
        }

        static string readTxt(string path)
        {
            string result = "";

            StreamReader sr = new StreamReader(path, Encoding.Default);

            result = sr.ReadToEnd();

            return result;
        }

        static void WL(string str)
        {
            Console.WriteLine(str);
        }
        static void W(string str)
        {
            Console.Write(str);
        }
        static string RL()
        {
            return Console.ReadLine();
        }
    }
}
