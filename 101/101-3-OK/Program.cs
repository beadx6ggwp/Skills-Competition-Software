using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _101_3
{
    class Program
    {
        static void Main(string[] args)
        {
            double r = double.Parse(getInput("請輸入徑向距離(r) = "));
            int n = int.Parse(getInput("請輸入徑向多項式的次數(n) = "));


            for (int m = -n; m <= n; m++)
            {
                if ((n - Math.Abs(m)) % 2 == 0)
                {
                    WL($"計算徑向多項式(radial polynomials) ..., r = {r}, n = {n}, m = {m}");
                    WL($"所求之徑向多項式為 = {getAns(r, n, m)}");
                }
            }


            Console.Read();
        }

        static double getAns(double r, int n, int m)
        {
            double result = 0;

            double temp0, temp1, temp2, temp3, temp4;

            int times = (n - Math.Abs(m)) / 2;
            for (int s = 0; s <= times; s++)
            {
                temp0 = getFactorial(n - s);

                temp1 = getFactorial(s);
                temp2 = getFactorial((n + Math.Abs(m)) / 2 - s);
                temp3 = getFactorial((n - Math.Abs(m)) / 2 - s);

                temp4 = Math.Pow(r, n - 2 * s);

                result += Math.Pow(-1, s) * (temp0 / (temp1 * temp2 * temp3)) * temp4;
            }

            return result;
        }
        static int getFactorial(int n)
        {
            if (n <= 1) return 1;
            else return n * getFactorial(n - 1);
        }
        static string getInput(string title)
        {
            Console.Write($"{title}");
            return RL();
        }
        static void W(string str)
        {
            Console.Write(str);
        }
        static void WL(string str)
        {
            Console.WriteLine(str);
        }
        static string RL()
        {
            return Console.ReadLine();
        }
    }
}
