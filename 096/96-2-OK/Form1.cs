using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _96_2
{
    public partial class Form1 : Form
    {
        Random ran = new Random();

        List<Label> labList = new List<Label>();
        int labSize = 30;
        Point pos = new Point(20, 50);

        int[] queue;
        int head;
        int count;
        public Form1()
        {
            InitializeComponent();
            queue = new int[6];
            head = ran.Next(0, 6);

            for (int i = 0; i < 6; i++)
            {
                labList.Add(CreateLabel(pos.X + i * labSize, pos.Y));
            }
            this.Controls.AddRange(labList.ToArray());
            this.Size = new Size(pos.X + (labList.Count + 1) * labSize, this.Height);

            count = 0;
            Add();
            Add();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //ADD
            textBox1.Text = Add();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //REMOVE
            textBox1.Text = Remove();
        }


        string Add()
        {
            if (count >= queue.Length)
            {
                int[] arr = new int[queue.Length * 2];

                // 更新資料
                for (int i = 0; i < count; i++)
                    arr[head + i] = queue[(head + i) % queue.Length];

                // 更新顯示
                for (int i = labList.Count; i < arr.Length; i++)
                {
                    Label lab = CreateLabel(pos.X + i * labSize, pos.Y);
                    labList.Add(lab);
                    this.Controls.Add(lab);
                }
                // 調整大小
                this.Size = new Size(pos.X + (labList.Count + 1) * labSize, this.Height);
                queue = arr;
            }

            int next = (head + count) % queue.Length;
            queue[next] = getNumber();
            count++;

            View();

            return $"Added {queue[next].ToString()}";
        }

        string Remove()
        {
            if (count == 0) return "Queue is empty";

            int removeEle = queue[head];

            queue[head] = 0;
            head = (head + 1) % queue.Length;
            count--;

            View();

            return$"Remover {removeEle.ToString()}";
        }

        void View()
        {

            for (int i = 0; i < queue.Length; i++)
            {
                labList[i].Text = queue[i] == 0 ? "" : queue[i].ToString();
            }
        }

        int getNumber()
        {
            return ran.Next(1, 1000);
        }

        Label CreateLabel(int x, int y)
        {
            Label lab = new Label()
            {
                Location = new Point(x, y),
                Size = new Size(labSize, labSize),
                AutoSize = false,
                BorderStyle = BorderStyle.FixedSingle,
                TextAlign = ContentAlignment.MiddleCenter
            };
            return lab;
        }
    }
}
