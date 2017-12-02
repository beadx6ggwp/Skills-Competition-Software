using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _104_1
{
    public partial class Form1 : Form
    {
        int lab_size = 4;
        int lab_w = 60, lab_h = 60;
        Point startPos = new Point(100, 50);
        Label[,] lab_arr;
        List<Button> btn_list = new List<Button>();

        Label select_lab;
        public Form1()
        {
            InitializeComponent();

            // name label1 ~ label16
            lab_arr = new Label[lab_size, lab_size];

            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    lab_arr[r, c] = new Label()
                    {
                        Name = $"{c},{r}",
                        Size = new Size(lab_w, lab_h),
                        AutoSize = false,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = new Font("consolas", 10),
                        BorderStyle = BorderStyle.FixedSingle,
                        Location = new Point(c * lab_w + startPos.X, r * lab_w + startPos.Y)
                    };
                    lab_arr[r, c].Click += new EventHandler(lab_click);
                    this.Controls.Add(lab_arr[r, c]);
                }
            }

            // name button1 ~ button6
            foreach (var item in panel2.Controls)
            {
                Button btn = item as Button;

                btn.Click += new EventHandler(btn_click);
                btn_list.Add(btn);
            }
        }

        void lab_click(object sender, EventArgs e)
        {
            select_lab = sender as Label;



            this.Text = select_lab.Name;
        }
        void btn_click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (select_lab != null)
            {
                select_lab.Text = btn.Text;
                select_lab = null;
            }
        }

        private void button5_Click(object sender, EventArgs e)// 提示
        {
            for (int r = 0; r < lab_size; r++)
            {
                for (int c = 0; c < lab_size; c++)
                {
                    var curr_lab = lab_arr[r, c];
                    List<string> str_list = new List<string>() { "1", "2", "3", "4" };

                    // 塞選已輸入
                    if (str_list.IndexOf(curr_lab.Text) != -1) continue;

                    int tx = c, ty = r;
                    
                    // 橫排
                    for (int x = -lab_size; x <= lab_size; x++)
                    {
                        tx = c + x;
                        if (tx < 0 || tx > lab_size - 1 || x == 0) continue;
                        if (str_list.IndexOf(lab_arr[r, tx].Text) != -1) //問題
                        {
                            string temp = lab_arr[r, tx].Text;                            
                            str_list.Remove(temp);
                        }
                    }
                    
                    // 直排
                    for (int y = -lab_size; y <= lab_size; y++)
                    {
                        ty = r + y;
                        if (ty < 0 || ty > lab_size - 1 || y == 0) continue;
                        if (str_list.IndexOf(lab_arr[ty, c].Text) != -1)
                        {
                            string temp = lab_arr[ty, c].Text;
                            str_list.Remove(temp);
                        }
                    }
                    
                    curr_lab.Text = $"({string.Join(",", str_list.ToArray())})";
                }
            }

        }

        private void button6_Click(object sender, EventArgs e)// check
        {
            bool gg = false, flag = false; ;

            for (int r = 0; r < lab_size; r++)
            {
                for (int c = 0; c < lab_size; c++)
                {
                    if (cheak(c, r)&& !flag)
                    {
                        gg = true;
                        flag = true;
                        break;
                    }
                }
            }

            if (gg) MessageBox.Show("GG");
        }

        bool cheak(int c, int r)
        {

            var curr_lab = lab_arr[c, r];
            int tx = c, ty = r;

            // 塞選空白
            if (curr_lab.Text == "") return false;

            // 橫排
            for (int x = -lab_size; x <= lab_size; x++)
            {
                tx = c + x;
                if (tx < 0 || tx > lab_size - 1 || x == 0) continue;

                if (curr_lab.Text == lab_arr[r, tx].Text)
                {
                    return true;
                }
            }

            // 直排
            for (int y = -lab_size; y <= lab_size; y++)
            {
                ty = c + y;
                if (ty < 0 || ty > lab_size - 1 || y == 0) continue;

                if (curr_lab.Text == lab_arr[ty, c].Text)
                {
                    return true;
                }
            }


            return false;
        }
    }
}
