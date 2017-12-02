using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {
            int num, n, s;
            string str = "";
            Console.WriteLine("輸入10進位數");
            num = int.Parse(Console.ReadLine());
            Console.WriteLine("輸入基底");
            n = int.Parse(Console.ReadLine());

            while (num != 0)
            {
                if (num < 0 && n < num)
                    s = num - n;
                else
                    s = Math.Abs(num % n);
                num = (num - s) / n;

                if (s > 9)
                    str = Convert.ToChar(s + 55).ToString() + str;
                else
                    str = s + str;
                Console.WriteLine(num+","+str);
            }
            Console.WriteLine(str);
            Console.Read();
        }
    }
}
