using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ordertest;
using System.Windows.Forms;

namespace OrderUI
{
    /// <summary>
    /// 新增订单的弹出窗口
    /// </summary>
    public partial class Form2 : Form
    {
        private OrderService OrderService;

        private Order Order = new Order();

        public Form2()
        {
            InitializeComponent();
        }

        public Form2(OrderService orderService)
        {
            InitializeComponent();
            this.OrderService = orderService;
            this.button3.Visible = false;
            if (Order.Details.Count == 0)
            {
                button1.Enabled = false;
            }
        }

        /// <summary>
        /// 添加/修改新的Order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == ""
                || textBox5.Text == "" || textBox6.Text == "" || textBox7.Text == "")
            {
                MessageBox.Show("存在空字段！");
            }
            else
            {
                try
                {
                    Order.Id = int.Parse(textBox1.Text);                    
                    Order.Customer = new Customer
                    {
                        Id = uint.Parse(textBox2.Text),
                        Name = textBox3.Text
                    };
                    Goods goods = new Goods(uint.Parse(textBox4.Text), textBox5.Text, float.Parse(textBox6.Text));
                    OrderDetail o = new OrderDetail(goods, uint.Parse(textBox7.Text));
                    

                    if (radioButton1.Checked)
                    {
                        Order.Details.Add(o);
                        OrderService.AddOrder(Order);
                    }
                    else if (radioButton2.Checked)
                    {
                        if (OrderService.GetById(Order.Id) == null)
                            throw new ArgumentNullException();
                        else
                        {
                            Order.Details.Add(o);
                            OrderService.Update(Order);
                        }
                    }
                    else
                    {
                        MessageBox.Show("请选择功能！");
                        return;
                    }
                    

                    this.tableLayoutPanel2.Visible = false;
                    button1.Enabled = true;

                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";
                    textBox7.Text = "";

                    MessageBox.Show("添加/更新成功！");
                }
                catch(ArgumentNullException ex)
                {
                    MessageBox.Show("该订单不存在，无法被更新！");
                }
                catch(ArgumentException ex)
                {
                    MessageBox.Show("订单编号重复！");
                }
                catch(Exception ex)
                {
                    MessageBox.Show("请输入正确的参数！");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.tableLayoutPanel2.Visible = true;
            button3.Visible = true;

        }

        /// <summary>
        /// 添加新的订单明细OrderDetail
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                Goods goods = new Goods(uint.Parse(textBox4.Text), textBox5.Text, float.Parse(textBox6.Text));
                OrderDetail o = new OrderDetail(goods, uint.Parse(textBox7.Text));
                Order.Details.Add(o);
                this.tableLayoutPanel2.Visible = false;
                MessageBox.Show("订单明细提交成功！");
            }
            catch
            {
                MessageBox.Show("请输入正确的订单明细！");
            }
        }
    }
}
