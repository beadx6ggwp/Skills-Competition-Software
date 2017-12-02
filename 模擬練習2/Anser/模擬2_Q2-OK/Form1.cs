using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 模擬2_Q2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int N = int.Parse(textBox1.Text);


            int M = 0;
            do { M++; } while (!Search(N, M, 13));

            textBox2.Text = M.ToString();
        }

        bool Search(int N, int M, int lastCity)
        {
            int[] city = new int[N + 1];

            int index = 1;
            bool flag = false;
            List<int> list = new List<int>();

            while (!flag)
            {
                city[index] = 1;
                list.Add(index);

                for (int i = 0; i < M; i++)
                {
                    index++;
                    if (index >= city.Length) index = 1;

                    if (city[index] != 0) i--;

                    if (city.Sum() == N)
                    {
                        flag = true;
                        break;
                    }
                }
            }

            return list.Last() == lastCity;
        }
    }
}
