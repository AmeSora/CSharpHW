using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hw5_15
{
    public partial class Form1 : Form
    {

        private Graphics graphics;

        public Form1()
        {
            InitializeComponent();
            graphics = CreateGraphics();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                graphics = splitContainer1.Panel2.CreateGraphics();
                graphics.TranslateTransform(AutoScrollPosition.X, AutoScrollPosition.Y);
                double th1 = double.Parse(textBox3.Text) * Math.PI / 180;
                double th2 = double.Parse(textBox4.Text) * Math.PI / 180;
                double per1 = double.Parse(textBox1.Text);
                double per2 = double.Parse(textBox2.Text);
                this.graphics.Clear(Color.AliceBlue);
                DrawCayleyTree(10, 300, 410, 120, new double[] { per1, per2 }, -Math.PI / 2, new double[] { th1, th2 });

            }
            catch (Exception ex)
            {
                MessageBox.Show("输入有误！");
            }
        }

        void DrawCayleyTree(int n, double x0, double y0, double length, double[] per, double th,  double[] th_)
        {
            if (n == 0) { return; }
            double k = 1;
            if (comboBox3.SelectedIndex != -1)
            {
                k = double.Parse(comboBox3.Text);
            }

            double x1 = x0 + length * Math.Cos(th) * k;
            double y1 = y0 + length * Math.Sin(th);
            double x2 = x0 + length * Math.Cos(th) * k;
            double y2 = y0 + length * Math.Sin(th);


            DrawLine(x0, y0, x1, y1);
    
            DrawCayleyTree(n - 1, x1, y1, length * per[0], per, th + th_[0], th_);
            DrawCayleyTree(n - 1, x2, y2, length * per[1], per, th - th_[1], th_);

        }

        void DrawLine(double x0, double y0, double x1, double y1)
        {
            Color c = Color.Empty;
            if (comboBox1.SelectedIndex == -1) c = Color.LightCoral;
            switch (comboBox1.SelectedIndex)
            {
                case 0: c = Color.LightCoral;break;
                case 1: c = Color.Black;break;
                case 2: c = Color.Pink;break;
                case 3: c = Color.LightSkyBlue;break;
            }
            float fl = 2f;
            if (comboBox2.SelectedIndex != -1)
            {
                fl = float.Parse(comboBox2.Text);
            }
            Pen p = new Pen(c, fl);
            graphics.DrawLine(p, (int)x0, (int)y0, (int)x1, (int)y1);
        }

    }
}
