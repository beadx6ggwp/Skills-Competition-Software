using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _96_4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string input = textBox1.Text.Replace(" ", "");

            textBox2.Text = string.Join(" ", postfix(input).ToCharArray());
        }

        string postfix(string input)
        {
            string result = "";

            Stack<string> stack = new Stack<string>();

            for (int i = 0; i < input.Length; i++)
            {
                string now = input[i].ToString();

                switch (now)
                {
                    case "(":
                        stack.Push(now);
                        break;

                    // 右括號輸出優先最高
                    case ")":
                        while (stack.Peek() != "(") result += stack.Pop();
                        stack.Pop();// 推出"("
                        break;

                    case "+":
                    case "-":
                    case "*":
                    case "/":
                        int nowPri = getPriority(now);
                        // 如果堆疊中的優先比較高，先輸出
                        while (stack.Count > 0 && getPriority(stack.Peek()) >= nowPri)
                        {
                            result += stack.Pop();
                        }
                        stack.Push(now);
                        break;

                    // 數字
                    default:
                        result += now;
                        break;
                }
            }

            // 把殘存的輸出
            while (stack.Count > 0)
                result += stack.Pop();


            return result;
        }

        int getPriority(string op)
        {
            if (op == "*" || op == "/") return 2;
            else if (op == "+" || op == "-") return 1;
            return 0;
        }
    }
}
