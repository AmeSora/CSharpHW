using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OrderUI
{
    /// <summary>
    /// OrderService
    /// </summary>
    public class OrderService : IDisposable
    {

        public List<Order> orderList;

        private OrderDB OrderDB;

        public OrderDB db { get => this.OrderDB; }

        /// <summary>
        /// 实现Dispose函数
        /// </summary>
        public void Dispose()
        {
            OrderDB.Dispose();
        }

        /// <summary>
        /// constructor
        /// </summary>
        public OrderService()
        {
            OrderDB = new OrderDB();
        }

        public OrderService(OrderDB orderDb)
        {
            this.OrderDB = orderDb;
        }



        /// <summary>
        /// add new order
        /// </summary>
        /// <param name="order">the order to be added</param>
        public void AddOrder(Order order)
        {
            if (db.orders == null)
            {
                return;
            }
            if (db.orders != null)
            {
                foreach (Order q in db.orders)
                {
                    if (q.Id == order.Id)
                        throw new ApplicationException($"the orderList contains an order with ID {order.Id} !");
                }
                db.orders.Add(order);
            }
            else
            {
                db.orders.Add(order);
            }
            db.SaveChanges();
        }

        /// <summary>
        /// update the order
        /// </summary>
        /// <param name="order">the order to be updated</param>
        public void Update(Order order)
        {
            RemoveOrder(order.Id);
            db.orders.Add(order);
            db.SaveChanges();
        }

        /// <summary>
        /// query by orderId
        /// </summary>
        /// <param name="orderId">id of the order to find</param>
        /// <returns>List<Order></returns> 
        public Order GetById(int orderId)
        {
            return db.orders.Include("Customer").Include("Details").SingleOrDefault(o => o.Id == orderId);
        }

        /// <summary>
        /// remove order
        /// </summary>
        /// <param name="orderId">id of the order which will be removed</param> 
        public void RemoveOrder(int orderId)
        {
            var order = db.orders.Include("Details").SingleOrDefault(o => o.Id == orderId);
            if (order == null)
                throw new ArgumentException($"the orderList doesn't contain an order with ID { orderId } !");
            db.details.RemoveRange(order.Details);
            db.orders.Remove(order);
            db.SaveChanges();
        }

        /// <summary>
        /// query all orders
        /// </summary>
        /// <returns>List<Order>:all the orders</returns> 
        public List<Order> QueryAll()
        {
            return db.orders.Include("Customer").Include("Details").ToList();
        }


        /// <summary>
        /// query by goodsName
        /// </summary>
        /// <param name="goodsName">the name of goods in order's orderDetail</param>
        /// <returns></returns> 
        public List<Order> QueryByGoodsName(string goodsName)
        {
            var query = db.orders.Include("Customer").Include("Details").ToList();
            List<Order> re = query.Where(
              o => o.Details.Exists(
                d => d.Goods.Name == goodsName)).ToList();
            return re;
        }

        /// <summary>
        /// query orders whose totalAmount >= totalAmount
        /// </summary>
        /// <param name="totalAmount">the minimum totalAmount</param>
        /// <returns></returns> 
        public List<Order> QueryByTotalAmount(float totalAmount)
        {
            var query = db.orders.Include("Customer").Include("Details").ToList();
            List<Order> re = new List<Order>();
            foreach (Order o in query)
            {
                if (o.TotalAmount >= totalAmount)
                {
                    re.Add(o);
                }
            }

            return re;
        }

        /// <summary>
        /// query by customerName
        /// </summary>
        /// <param name="customerName">customer name</param>
        /// <returns></returns> 
        public List<Order> QueryByCustomerName(string customerName)
        {
            var query = db.orders
                .Include("Customer").Include("Details").ToList();
            List<Order> re = query.
                Where(o => o.Customer.Name == customerName).ToList();
            return re;
        }

        /// <summary>
        /// Exprot the orders to an xml file.
        /// </summary>
        public void Export(String fileName)
        {
            if (Path.GetExtension(fileName) != ".xml")
                throw new ArgumentException("the exported file must be a xml file!");
            XmlSerializer xs = new XmlSerializer(typeof(List<Order>));
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                xs.Serialize(fs, QueryAll());
            }
        }

        /// <summary>
        /// import from an xml file
        /// </summary>
        public List<Order> Import(string path)
        {
            if (Path.GetExtension(path) != ".xml")
                throw new ArgumentException($"{path} isn't a xml file!");
            XmlSerializer xs = new XmlSerializer(typeof(List<Order>));
            List<Order> result = new List<Order>();
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    List<Order> re = (List<Order>)xs.Deserialize(fs);
                    db.orders.AddRange(re);
                    db.SaveChanges();
                    return re;
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException("import error:" + e.Message);
            }

        }

    }
}
