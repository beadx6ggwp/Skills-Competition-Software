using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace _103_4
{
    class Program
    {
        public delegate void func();
        static PersonManage pm;

        static void Main(string[] args)
        {
            pm = new PersonManage("data.txt");

            pm.InitData();

            func[] func_arr = new func[] { Func1, Func2, Func3, Func4, Func5 };
            while (true)
            {
                string choose = getMenuChoose();

                func_arr[int.Parse(choose) - 1]();

                if (getInput("繼續:請按1，結束:請按0 :") == "0")
                    break;
            }


            WL("press any key to continue");
            Console.Read();
        }

        static void Func1()
        {
            WL("批次輸入");
            string fileName = getInput("輸入檔名:");
            string[] inputs = pm.ReadTxt(fileName).Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);


            foreach (var i in inputs)
            {
                int index = Array.IndexOf(pm.indexs_sports, i.Split(' ')[4]);
                int sprots_type = pm.indexs_type[index];
                string result = pm.Add(new Person(i," "), sprots_type);
                WL(result);
            }
        }

        static void Func2()
        {
            WL("選手查詢");
            string input = getInput("請輸入 班級、學號、姓名:");
            string[] temp = input.Split(' ');

            var result = pm.Search(temp[0], temp[1], temp[2]);
            foreach (var i in result)
            {
                WL(i.ToString());
            }
        }

        static void Func3()
        {
            WL("刪除資料");
            string input = getInput("請輸入 班級、學號、姓名、報名項目:");
            string[] temp = input.Split(' ');

            string result = pm.Delete(temp[0], temp[1], temp[2], temp[3]);
            if (result != "")
            {
                WL($"被刪除的選手:{result}");
            }
        }

        static void Func4()
        {
            WL("逐筆輸入");
            string input = getInput("請輸入:班級 學號 姓名 性別:");
            WL("報名項目");
            for (int i = 0; i < pm.indexs_index.Length; i++)
            {
                WL($"{pm.indexs_index[i]}:{pm.indexs_sports[i]}");
            }

            int index = Array.IndexOf(pm.indexs_index, getInput("請選擇:"));

            string input2 = pm.indexs_sports[index];// getsports
            int sprots_type = pm.indexs_type[index];

            WL($"輸入:{input} {input2}");

            string result = pm.Add(new Person($"{input} {input2}", " "), sprots_type);
            WL(result);
        }

        static void Func5()
        {
            WL("顯示所有資料");

            foreach (var i in pm.list)
            {
                WL(i.ToString());
            }
        }



        static string getMenuChoose()
        {
            WL("請選擇操作項目:");
            WL("\t<1>批次輸入:");
            WL("\t<2>選手查詢:");
            WL("\t<3>刪除:");
            WL("\t<4>逐筆輸入:");
            WL("\t<5>顯示所有資料:");
            W("請選擇 :");

            return RL();
        }

        static string getInput(string title)
        {
            if (title != "") W(title);
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
