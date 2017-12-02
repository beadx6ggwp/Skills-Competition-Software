using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _101_2
{
    class Program
    {

        static void Main(string[] args)
        {
            while (solve()) ;



            WL("press any key to exit");
            Console.Read();
        }

        static bool solve()
        {
            WL("輸入比值，空白分割");
            double[] input1 = Array.ConvertAll(getInput("請輸入「專業能力」對「通識素養」的指標比值 : ").Split(' '), double.Parse);
            double[] input2 = Array.ConvertAll(getInput("請輸入「專業能力」對「合群性」的指標比值   : ").Split(' '), double.Parse);
            double[] input3 = Array.ConvertAll(getInput("請輸入「通識素養」對「合群性」的指標比值   : ").Split(' '), double.Parse);

            int n = 3;
            double[][] a = new double[][] { new double[n], new double[n], new double[n] };// row, col
            double[][] b = new double[][] { new double[n], new double[n], new double[n] };
            double[] w = new double[n];
            double max = 0;
            double CR = 0;
            // 初始化並給定資料
            for (int i = 0; i < n; i++) a[i][i] = 1;
            a[0][1] = input1[0] / input1[1];
            a[1][0] = input1[1] / input1[0];

            a[0][2] = input2[0] / input2[1];
            a[2][0] = input2[1] / input2[0];

            a[1][2] = input3[0] / input3[1];
            a[2][1] = input3[1] / input3[0];

            // 計算b
            for (int r = 0; r < n; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    double val = 0;
                    for (int t = 0; t < n; t++) val += a[t][c];

                    b[r][c] = a[r][c] / val;
                }
            }
            //計算w
            for (int r = 0; r < n; r++)
            {
                double val = 0;
                for (int t = 0; t < n; t++) val += b[r][t];

                w[r] = val / n;
            }

            // 計算max
            for (int j = 0; j < n; j++)
            {
                double val = 0;
                for (int i = 0; i < n; i++)
                {
                    val += a[i][j];
                }
                max += w[j] * val;
            }

            // 計算CR
            CR = (max - n) / ((n - 1) * 0.58);

            // 顯示該矩陣
            for (int r = 0; r < n; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    W($"{a[r][c]:0.000} ");
                }
                WL("");
            }

            W("顯示指標的權重 : ");
            for (int i = 0; i < n; i++) W($"{w[i]:0.000} ");
            WL("");

            WL($"顯示最大特徵比值 : {max:0.000}");

            WL($"顯示一致性比率 : {CR:0.000}");

            if (CR < 0.1)
                WL("符合一致性，為有效問卷");
            else
                WL("不符合一致性，不採用結果");

            if (getInput("是否繼續?(y or n) : ") == "y") return true;
            else return false;
        }
        static string getInput(string title)
        {
            W(title);
            return RL();
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
