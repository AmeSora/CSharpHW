using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderUI
{
    public partial class Form1 : Form
    {


        public OrderService orderService = new OrderService();

        public Form1()
        {
            InitializeComponent();

            Customer customer1 = new Customer(1, "Customer1");
            Customer customer2 = new Customer(2, "Customer2");

            Goods milk = new Goods(1, "Milk", 69.9f);
            Goods eggs = new Goods(2, "eggs", 4.99f);
            Goods apple = new Goods(3, "apple", 5.59f);

            Order order1 = new Order(1, customer1);
            order1.AddDetails(new OrderDetail(apple, 8));
            order1.AddDetails(new OrderDetail(eggs, 10));
            order1.AddDetails(new OrderDetail(milk, 10));

            Order order2 = new Order(2, customer2);
            order2.AddDetails(new OrderDetail(eggs, 10));
            order2.AddDetails(new OrderDetail(milk, 10));

            Order order3 = new Order(3, customer2);
            order3.AddDetails(new OrderDetail(milk, 100));

            //orderService = new OrderService();
            orderService.AddOrder(order1);
            orderService.AddOrder(order2);
            orderService.AddOrder(order3);

            UpdateDataSource1();
            UpdateDataSource2();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2(orderService);
            form.ShowDialog();
            UpdateDataSource1();
            UpdateDataSource2();
        }


        /// <summary>
        /// 根据鼠标选中行改变子表的内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.CurrentCell == null) return;
            int row = dataGridView1.CurrentCell.RowIndex;
            int id = int.Parse(dataGridView1[0, row].Value.ToString());
            Order order = orderService.GetById(id);
            dataGridView2.DataSource = order.Details.Select(o => new {
                o.Amount,
                o.Quantity,
                g_id = o.Goods.Id,
                g_name = o.Goods.Name,
                g_price = o.Goods.Price
            }).ToList();
        }

        /// <summary>
        /// 查询功能按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            int select = comboBox1.SelectedIndex;
            List<Order> query = new List<Order>();
            String text = textBox2.Text;
            bool flag = int.TryParse(text, out int result);
            switch (select)
            {
                case 0:
                    if (flag)
                    {
                        Order order = orderService.GetById(result);
                        if (order != null)
                            query.Add(order);
                        break;
                    }
                    else
                    {
                        MessageBox.Show("输入查询参数有误！");
                        return;
                    }

                case 1:
                    if (flag)
                    {
                        int amount = int.Parse(text);
                        List<Order> q = orderService.QueryByTotalAmount(amount);
                        query.AddRange(q);
                        break;
                    }
                    else
                    {
                        MessageBox.Show("输入查询参数有误！");
                        return;
                    }

                case 2:
                    {
                        List<Order> q = orderService.QueryByGoodsName(text);
                        query.AddRange(q);
                        break;
                    }

                case 3:
                    {
                        List<Order> q = orderService.QueryByCustomerName(text);
                        query.AddRange(q);
                        break;
                    }

                case 4:
                    {
                        query = orderService.QueryAll();
                        break;
                    }
            }

            if (query.Count == 0)
            {
                MessageBox.Show("查询结果为空！");
            }
            else
            {
                var list = query.Select(o => new {
                    o.Id,
                    o.TotalAmount,
                    c_id = o.Customer.Id,
                    c_Name = o.Customer.Name
                }).ToList();
                dataGridView1.DataSource = list;
                UpdateDataSource2();
            }
        }

        /// <summary>
        /// 删除功能按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            bool flag = int.TryParse(textBox1.Text, out int id);
            if (flag)
            {
                Order order = orderService.GetById(id);
                if (order == null)
                {
                    MessageBox.Show("该订单不存在！");
                }
                else
                {
                    orderService.RemoveOrder(id);

                    UpdateDataSource1();
                    UpdateDataSource2();

                    MessageBox.Show("删除成功！");
                }
            }
            else
            {
                MessageBox.Show("请输入正确的编号！");
            }
        }

        /// <summary>
        /// 更新表格1的数据源
        /// </summary>
        private void UpdateDataSource1()
        {
            var list = orderService.QueryAll().Select(o => new {
                o.Id,
                o.TotalAmount,
                c_id = o.Customer.Id,
                c_Name = o.Customer.Name
            }).ToList();

            this.dataGridView1.DataSource = list;
        }

        /// <summary>
        /// 更新表格2的数据源
        /// </summary>
        private void UpdateDataSource2()
        {
            if (this.dataGridView1.RowCount != 0)
            {
                int Id = 0;
                try
                {
                    Id = int.Parse(dataGridView1[0, 0].Value.ToString());
                }
                catch (Exception e)
                {
                    MessageBox.Show("error!");
                }
                var query = orderService.GetById(Id);
                var re = query.Details.Select(o => new {
                    o.Amount,
                    o.Quantity,
                    g_id = o.Goods.Id,
                    g_name = o.Goods.Name,
                    g_price = o.Goods.Price
                }).ToList();
                this.dataGridView2.DataSource = re;
            }
        }

        /// <summary>
        /// 导出为xml文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.RestoreDirectory = true;
            save.Filter = "订单文件(*.xml)|*.xml";
            save.FileName = "Order";
            if (save.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    orderService.Export(save.FileName.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("保存失败！");
                }
            }
        }

        /// <summary>
        /// 导入xml文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.RestoreDirectory = true;
            open.Filter = "订单文件(*.xml)|*.xml";
            if (open.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    orderService.orderList = orderService.Import(open.FileName.ToString());
                    UpdateDataSource1();
                    UpdateDataSource2();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("导入失败！");
                }
            }
        }


    }


}
