using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _98_1
{
    public partial class Form1 : Form
    {
        Random ran = new Random();
        TextBox[] tb;
        Button[] btn;
        int[] types = new int[4];
        string[] statue = { "Ih", "Ld", "En" };
        public Form1()
        {
            InitializeComponent();
            tb = new TextBox[] { textBox1, textBox2, textBox3, textBox4 };
            btn = new Button[] { button1, button2, button3, button4 };

            Reset();

            Array.ForEach(btn, (n) =>
            {
                n.Click += (s, e) =>
                {
                    int index = Array.IndexOf(btn, s);
                    types[index] = (types[index] + 1) % statue.Length;
                    int type = types[index];

                    n.Text = statue[type];
                };
            });
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Array.ForEach(tb, s => s.Text = GetRandom());
            Reset();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int en = Array.IndexOf(types, 2);
            int count = types.Where(s => s == 2).Count();
            if (en == -1)
            {
                MessageBox.Show("沒有En");
                return;
            }
            if (count > 1)
            {
                MessageBox.Show("只能有一個En");
                return;
            }

            for (int i = 0; i < types.Length; i++)
            {
                if (types[i] == 1)
                    tb[i].Text = tb[en].Text;
            }
        }

        string GetRandom()
        {
            return Convert.ToString(ran.Next() & 0xFF, 2).PadLeft(8, '0');
        }
        void Reset()
        {
            Array.ForEach(btn, (n) => n.Text = statue[0]);
            types = new int[4];
        }
    }
}
