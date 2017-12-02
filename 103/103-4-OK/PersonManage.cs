using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _103_4
{
    public class PersonManage
    {
        // 懶得再用一個Class或多重字典
        public int[] indexs_type = new int[] { 3, 2, 2, 2, 2, 1, 1, 1, 1 };
        public string[] indexs_index = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i" };
        public string[] indexs_sports = new string[]
        {
            "大隊接力",
            "一顆球的距離",
            "天旋地轉",
            "滾大球袋鼠跳",
            "牽手同心",
            "100公尺",
            "400公尺接力",
            "800公尺",
            "跳高"
        };

        string fileName;
        public List<Person> list = new List<Person>();
        public PersonManage(string fileName)
        {
            this.fileName = fileName;
        }

        public List<Person> Search(string classN, string number, string name)
        {
            List<Person> result = new List<Person>();

            foreach (var i in list)
            {
                if (i.classN == classN &&
                    i.number == number &&
                    i.name == name)
                {
                    result.Add(i);
                }
            }

            return result;
        }

        public string Add(Person p, int sports_type)
        {
            var result = Search(p.classN, p.number, p.name);

            int[] types = new int[3];// 1單人, 2多人, 3接力
            foreach (var person in result)// 檢查原資料
            {
                for (int i = 0; i < indexs_sports.Length; i++)
                {
                    if (person.sports == indexs_sports[i])
                    {
                        int t = indexs_type[i];

                        types[t - 1]++;
                    }
                }
            }

            // 這段勉強用
            types[sports_type - 1]++;// 加上當前項目
            if (types[2] > 1 && sports_type == 3) return $"{p.ToString()}\t:已報名接力賽";
            if (types[0] > 0 && types[1] > 0 && sports_type != 3) return $"{p.ToString()}\t:個人賽與團體賽只能擇一參加";
            if (types[0] > 2 && sports_type == 1) return $"{p.ToString()}\t:個人賽不得超過兩項";
            if (types[1] > 2 && sports_type == 2) return $"{p.ToString()}\t:團體賽不得超過兩項";

            list.Add(p);
            SaveList();
            return $"{p.sports}報名成功";
        }

        public string Delete(string classN, string number, string name, string sports)
        {
            foreach (var i in list)
            {
                if (i.classN == classN &&
                    i.number == number &&
                    i.name == name &&
                    i.sports == sports)
                {
                    list.Remove(i);
                    SaveList();
                    return i.ToString();
                }
            }
            return "";
        }

        public void InitData()
        {
            string[] input = ReadTxt(fileName).Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            list.Clear();

            foreach (var i in input)
            {
                list.Add(new Person(i, " "));
            }
        }
        public void SaveList()
        {
            string result = "";
            foreach (var i in list)
            {
                result += i.ToString() + "\r\n";
            }
            result.Remove(result.Length - 2, 2);
            Save(fileName, result, 1);
        }

        public void Save(string fileName, string str, int mode)// 0:加入 1:抹寫
        {
            if (File.Exists(fileName) && mode == 1) File.Delete(fileName);

            File.AppendAllText(fileName, str, Encoding.Default);
        }

        public string ReadTxt(string fileName)
        {
            string result = "";

            StreamReader sr = new StreamReader(fileName, Encoding.Default);
            result = sr.ReadToEnd();
            sr.Dispose();

            return result;
        }

    }
    public struct Person
    {
        public string classN, number, name, sex, sports;
        public Person(string classN, string number, string name, string sex, string sports)
        {
            this.classN = classN;
            this.number = number;
            this.name = name;
            this.sex = sex;
            this.sports = sports;
        }
        public Person(string str, string separator)
        {
            string[] input = str.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);
            this.classN = input[0];
            this.number = input[1];
            this.name = input[2];
            this.sex = input[3];
            this.sports = input[4];
        }
        public override string ToString()
        {
            return $"{classN} {number} {name} {sex} {sports}";
        }
        public bool Equals(Person obj)
        {
            return this.ToString() == obj.ToString();
        }
    }
}
