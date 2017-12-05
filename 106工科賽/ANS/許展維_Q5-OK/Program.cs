using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 許展維_Q5
{
    class Program
    {
        static double[] B;
        static double[] price;

        static int N = 10;

        static void Main(string[] args)
        {
            Console.WriteLine("日期系列");
            double[] arrx = Array.ConvertAll(Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries), double.Parse);


            Console.WriteLine("價格系列");
            double[] arry = Array.ConvertAll(Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries), double.Parse);


            B = new double[arrx.Length];
            price = new double[arrx.Length];

            for (int i = N - 1; i < arrx.Length; i++)
            {
                double avgX = 0, avgY = 0;
                for (int j = 0; j < N; j++)
                {
                    avgX += arrx[i - j] / N;
                    avgY += arry[i - j] / N;
                }

                // 計算迴歸係數
                double b = 0, a = 0, dx, dy, up = 0, down = 0;
                for (int j = 0; j < N; j++)
                {
                    dx = arrx[i - j] - avgX;
                    dy = arry[i - j] - avgY;
                    up += (dx * dy);
                    down += (dx * dx);
                }
                b = up / down;
                a = avgY - b * avgX;

                B[i] = b;

                if (i + 1 < arrx.Length)
                    price[i + 1] = a + b * arrx[i + 1];
            }

            Console.WriteLine("直線斜率b");
            Console.WriteLine(string.Join(" ", Array.ConvertAll(B, s => s.ToString("0.0").PadLeft(4, ' '))));

            Console.WriteLine("價格系列");
            Console.WriteLine(string.Join(" ", Array.ConvertAll(price, s => s.ToString("0.0").PadLeft(4,' '))));

            Console.Read();
        }
    }
}
