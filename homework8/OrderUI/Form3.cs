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
    public partial class Form3 : Form
    {
        private OrderService OrderService;

        private Order Order;

        public Form3()
        {
            InitializeComponent();
        }

        public Form3(OrderService orderService)
        {
            InitializeComponent();
            this.OrderService = orderService;
        }
    }
}
