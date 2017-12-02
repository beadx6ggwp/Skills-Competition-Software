using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _105_3
{
    class Program
    {
        static int height;
        static Point p1, p2, p3;
        static void Main(string[] args)
        {
            Console.WindowWidth = 120;
            Console.WindowHeight = 40;
            height = Console.WindowHeight - 2;

            do
            {
                Console.WriteLine("請選擇操作項目:\n" +
                    "\t<1>輸入兩點座標(x1,y1),(x2,y2)繪一線\n" +
                    "\t<2>輸入三點座標(x1,y1),(x2,y2),(x3,y3)繪三角形\n" +
                    "\t<3>上題三角形水平翻轉");
                string input = GetInput("請選擇:");
                switch (input)
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

                Console.SetCursorPosition(0, Console.WindowHeight - 1);
            } while (GetInput("繼續:請按1，結束:請按0 :") == "1");
        }

        static void Func1()
        {
            string p1 = GetInput("(x1,y1):");
            string p2 = GetInput("(x2,y2):");
            Console.Clear();
            DrawLine(new Point(p1), new Point(p2));
        }
        static void Func2()
        {
            p1 = new Point(GetInput("(x1,y1):"));
            p2 = new Point(GetInput("(x2,y2):"));
            p3 = new Point(GetInput("(x3,y3):"));

            Console.Clear();

            DrawTri(p1, p2, p3);
        }
        static void Func3()
        {
            Console.Clear();
            Point[] pts = new Point[] { p1, p2, p3 };
            Array.Sort(pts, (a, b) => a.x.CompareTo(b.x));
            int min = pts[0].x;
            int max = pts[2].x;
            int center = min + (max - min) / 2;

            for (int i = 0; i < pts.Length; i++)
            {
                int diff = center - pts[i].x;
                pts[i].x = center + (diff);
            }

            DrawTri(p1, p2, p3);
        }

        static void DrawLine(Point p1, Point p2)
        {
            int dx = p2.x - p1.x;
            int dy = p2.y - p1.y;
            int steps = Math.Abs(dx) > Math.Abs(dy) ? Math.Abs(dx) : Math.Abs(dy);
            float xi = dx / (float)steps;
            float yi = dy / (float)steps;
            float x = p1.x;
            float y = p1.y;

            SetPoint(p1.x, p1.y);
            SetPoint(p2.x, p2.y);
            for (int i = 0; i < steps; i++)
            {
                x += xi;
                y += yi;
                SetPoint((int)Math.Round(x), (int)Math.Round(y));
            }
        }
        static void DrawTri(Point p1, Point p2, Point p3)
        {
            DrawLine(p1, p2);
            DrawLine(p2, p3);
            DrawLine(p3, p1);
        }
        static void SetPoint(int x1, int y1)
        {
            Console.SetCursorPosition(x1, height - y1);
            Console.Write("*");
        }

        static string GetInput(string str)
        {
            Console.Write(str);
            return Console.ReadLine();
        }

    }

    class Point
    {
        public int x, y;
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public Point(string str)
        {
            string[] temp = str.Split(',');
            x = int.Parse(temp[0]);
            y = int.Parse(temp[1]);
        }
    }
}
