using Microsoft.VisualStudio.TestTools.UnitTesting;
using ordertest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordertest.Tests
{
    [TestClass()]
    public class OrderServiceTests
    {
        [TestMethod()]
        public void OrderServiceTest()
        {
            OrderService order = new OrderService();
            Assert.IsNotNull(order);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void AddOrderTest()
        {
            Customer customer = new Customer(3, "Mike");
            Order order1 = new Order(1, customer);
            Order order2 = new Order(1, customer);
            OrderService orderService = new OrderService();

            orderService.AddOrder(order1);
            orderService.AddOrder(order2);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            OrderService order = new OrderService();
            Customer c1 = new Customer(1, "Mike");
            Customer c2 = new Customer(2, "Mary");
            Order order1 = new Order(1, c1);
            order.AddOrder(order1);
            Order order2 = new Order(1, c2);
            order.Update(order2);

            Assert.AreEqual(order.GetById(1).Customer, c2);
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            OrderService order = new OrderService();
            Order o1 = new Order(1, new Customer(1, "Mike"));
            order.AddOrder(o1);
            Assert.AreEqual(order.GetById(1), o1);
        }

        [TestMethod()]
        public void RemoveOrderTest()
        {
            OrderService order = new OrderService();
            Order o = new Order(1, new Customer(1, "mike"));
            order.AddOrder(o);
            order.RemoveOrder(1);
            Assert.IsNull(order.GetById(1));
        }

        [TestMethod()]
        public void QueryAllTest()
        {
            OrderService order = new OrderService();
            List<Order> list = order.QueryAll();
            Assert.AreEqual(list.Count, 0);
            order.AddOrder(new Order());
            Assert.AreEqual(order.QueryAll().Count, 1);
        }

        [TestMethod()]
        public void QueryByGoodsNameTest()
        {
            Goods milk = new Goods(1, "Milk", 69.9f);
            Customer customer1 = new Customer(1, "Customer1");
            Order order1 = new Order(1, customer1);
            order1.AddDetails(new OrderDetail(milk, 10));
            OrderService order = new OrderService();
            order.AddOrder(order1);
            List<Order> re = order.QueryByGoodsName("Milk");
            Assert.AreEqual(order1, re[0]);
        }

        [TestMethod()]
        public void QueryByTotalAmountTest()
        {
            Goods milk = new Goods(1, "Milk", 69.9f);
            Customer customer1 = new Customer(1, "Customer1");
            Order order1 = new Order(1, customer1);
            order1.AddDetails(new OrderDetail(milk, 10));
            OrderService order = new OrderService();
            order.AddOrder(order1);
            List<Order> re = order.QueryByTotalAmount(50f);
            Assert.AreEqual(order1, re[0]);
        }

        [TestMethod()]
        public void QueryByCustomerNameTest()
        {
            Goods milk = new Goods(1, "Milk", 69.9f);
            Customer customer1 = new Customer(1, "Customer1");
            Order order1 = new Order(1, customer1);
            order1.AddDetails(new OrderDetail(milk, 10));
            OrderService order = new OrderService();
            order.AddOrder(order1);
            List<Order> re = order.QueryByCustomerName("Customer1");
            Assert.AreEqual(order1, re[0]);
            List<Order> re1 = order.QueryByCustomerName("Cust");
            Assert.AreEqual(re1.Count, 0);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SortTest()
        {
            OrderService order = new OrderService();
            Order o1 = new Order(1, new Customer());
            Order o2 = new Order(2, new Customer());
            Order o3 = new Order(3, new Customer());
            order.AddOrder(o1);
            order.AddOrder(o2);
            order.AddOrder(o3);
            order.Sort((o_1, o_2) => o_1.Id.CompareTo(o_2.Id));
            List<Order> re = order.QueryAll();
            Assert.AreEqual(re[0].Id, 1);
            Assert.AreEqual(re[1].Id, 2);
            Assert.AreEqual(re[2].Id, 3);
            order.Sort(null);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ExportTest()
        {
            Goods milk = new Goods(1, "Milk", 69.9f);
            Customer customer1 = new Customer(1, "Customer1");
            Order order1 = new Order(1, customer1);
            order1.AddDetails(new OrderDetail(milk, 10));
            OrderService order = new OrderService();
            order.AddOrder(order1);

            order.Export("test.xml");
            order.Export("text.txt");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ImportTest()
        {
            OrderService order = new OrderService();
            List<Order> re = order.Import("test.xml");
            Assert.AreEqual(re[0].Id, 1);
            order.Import("test.txt");
        }
    }
}