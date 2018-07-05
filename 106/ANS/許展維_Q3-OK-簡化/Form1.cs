using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 許展維_Q3
{
    public partial class Form1 : Form
    {
        Button[] btn;
        int[] nowCheck = new int[7];
        List<LED> list = new List<LED>();

        Random ran = new Random();
        public Form1()
        {
            InitializeComponent();
            btn = new Button[] { button1, button2, button3, button4, button5, button6, button7 };
            foreach (var item in btn)
            {
                item.Text = "";
                item.Click += (s, e) =>
                {
                    int index = Array.IndexOf(btn, (Button)s);
                    nowCheck[index] = (nowCheck[index] + 1) % 2;
                    setLED(nowCheck);
                };
            }

            setLED(nowCheck);

            list.Add(new LED(0, new int[] { 1, 1, 1, 1, 1, 1, 0 }));
            list.Add(new LED(1, new int[] { 0, 1, 1, 0, 0, 0, 0 }));
            list.Add(new LED(1, new int[] { 0, 0, 0, 0, 1, 1, 0 }));
            list.Add(new LED(2, new int[] { 1, 1, 0, 1, 1, 0, 1 }));
            list.Add(new LED(3, new int[] { 1, 1, 1, 1, 0, 0, 1 }));
            list.Add(new LED(4, new int[] { 0, 1, 1, 0, 0, 1, 1 }));
            list.Add(new LED(5, new int[] { 1, 0, 1, 1, 0, 1, 1 }));
            list.Add(new LED(6, new int[] { 1, 0, 1, 1, 1, 1, 1 }));
            list.Add(new LED(6, new int[] { 0, 0, 1, 1, 1, 1, 1 }));
            list.Add(new LED(7, new int[] { 1, 1, 1, 0, 0, 0, 0 }));
            list.Add(new LED(8, new int[] { 1, 1, 1, 1, 1, 1, 1 }));
            list.Add(new LED(9, new int[] { 1, 1, 1, 1, 0, 1, 1 }));
            list.Add(new LED(9, new int[] { 1, 1, 1, 0, 0, 1, 1 }));
        }
        private void button8_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 7; i++) nowCheck[i] = ran.Next(0, 2);
            setLED(nowCheck);
            check();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            check();
        }
        void check()
        {
            foreach (var item in list)
            {
                int count = 0;
                for (int i = 0; i < 7; i++)
                {
                    if (item.arr[i] == nowCheck[i])
                        count++;
                }

                if (count == 7)
                {
                    label2.Text = item.num.ToString();
                    return;
                }
            }
            label2.Text = "非數字";
        }

        void setLED(int[] arr)
        {
            for (int i = 0; i < 7; i++)
                nowCheck[i] = arr[i];

            for (int i = 0; i < 7; i++)
                btn[i].BackColor = (arr[i] == 1) ? Color.Black : Color.White;
        }
    }

    class LED
    {
        public int num;
        public int[] arr = new int[7];
        public LED(int n, int[] _arr)
        {
            num = n;
            for (int i = 0; i < 7; i++)
                arr[i] = _arr[i];
        }
    }
}
