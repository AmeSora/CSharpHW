using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace homework6
{
    class OrderService
    {
        private List<Order> orderLists;

        public OrderService()
        {
            this.orderLists = new List<Order>();
        }

        public void AddOrder(Order order)
        {
            orderLists.ForEach((o) => {
                if (o.Equals(order))
                    throw new Exception($"order-{order.Oid} is already existed!");
            });
            orderLists.Add(order);
        }

        /// <summary>
        /// LINQ override 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public Order GetById(uint orderId)
        {

            var result = from order in orderLists where order.Oid == orderId select order;
            return result.Single<Order>();
            
        }

        public void RemoveOrder(uint orderId)
        {
            Order order = GetById(orderId);
            if (order == null) return;
            orderLists.Remove(order);
        }

        public List<Order> SortById()
        {
            orderLists.Sort();
            return orderLists;
        }

        public List<Order> QueryAllOrders()
        {
            return orderLists;
        }

        /// <summary>
        /// LINQ override
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<Order> QueryByGoodsName(String name)
        {
            var re = from order in orderLists from s in order.Details where s.Goods.Name == name select order;
            return re.ToList<Order>();
        }

        /// <summary>
        /// LINQ override
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<Order> QueryByCustomerName(String name)
        {
            var re = from order in orderLists where order.Customer.Name == name select order;
            return re.ToList<Order>();
        }

        public List<Order> SortByTotalPrice()
        {
            //var re = from order in orderLists orderby order.GetTotalPrice() select order;
            //return re.ToList<Order>();

            return orderLists.OrderBy(o => o.GetTotalPrice()).ToList<Order>();
        }

        /// <summary>
        /// export xml document
        /// </summary>
        public void Export()
        {
            XmlDocument xml = new XmlDocument();
            //xml.CreateXmlDeclaration("1.0", "utf-8", null);
            xml.CreateXmlDeclaration("1.0", "UTF-8", null);
            xml.AppendChild(xml.CreateNode(XmlNodeType.Element, "OrderLists", ""));

            foreach (Order order in orderLists)
            {
                uint orderId = order.Oid;
                List<OrderDetail> details = order.Details;
                Customer customer = order.Customer;

                XmlNode root = xml.SelectSingleNode("OrderLists");

                XmlElement Eorder = xml.CreateElement("Order");
                root.AppendChild(Eorder);

                XmlElement Ecustomer = xml.CreateElement("Customer");
                Ecustomer.SetAttribute("Id", customer.Id.ToString());
                Ecustomer.InnerText = customer.Name;
                Eorder.AppendChild(Ecustomer);

                XmlElement Eid = xml.CreateElement("OrderId");
                Eid.InnerText = order.Oid.ToString();
                Eorder.AppendChild(Eid);

                XmlElement EorderDetails = xml.CreateElement("OrderDetails");

                foreach (OrderDetail d in details)
                {
                    XmlElement EorderDetail = xml.CreateElement("OrderDetail");
                    XmlElement EorderDetailId = xml.CreateElement("OrderDetailId");
                    EorderDetailId.InnerText = d.Id.ToString();
                    EorderDetail.AppendChild(EorderDetailId);

                    XmlElement Equantity = xml.CreateElement("Quantity");
                    Equantity.InnerText = d.Quantity.ToString();
                    EorderDetail.AppendChild(Equantity);

                    XmlElement Egoods = xml.CreateElement("Goods");
                    Egoods.SetAttribute("Id", d.Goods.Id.ToString());
                    Egoods.SetAttribute("Price", d.Goods.Price.ToString());
                    Egoods.InnerText = d.Goods.Name;
                    EorderDetail.AppendChild(Egoods);

                    EorderDetails.AppendChild(EorderDetail);

                }

                Eorder.AppendChild(EorderDetails);

                 
            }


            xml.Save(@"D:\OrderLists.xml");
        }

        public void Import(String pathName)
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(pathName);

                XmlNode root = xml.SelectSingleNode("OrderLists");
                for(int i = 0;i<root.ChildNodes.Count;++i)
                {
                    XmlNode NodeOrder = root.ChildNodes[i];

                    uint orderId = uint.Parse(NodeOrder.SelectSingleNode("OrderId").InnerText);
                    //uint orderId = (uint)i;
                    XmlElement Ecustomer = (XmlElement)NodeOrder.SelectSingleNode("Customer");
                    String name = Ecustomer.InnerText;
                    uint customerId = uint.Parse(Ecustomer.GetAttribute("Id"));
                    Customer c = new Customer(customerId,name);
                    
                    Order order = new Order(orderId,c);
                    foreach (XmlNode xnl in NodeOrder.SelectNodes("OrderDetails/OrderDetail"))
                    {
                        uint id = uint.Parse(((XmlElement)xnl).SelectSingleNode("OrderDetailId").InnerText);
                        uint quantity = uint.Parse(((XmlElement)xnl).SelectSingleNode("Quantity").InnerText);
                        XmlElement Ngood = (XmlElement)((XmlElement)xnl).SelectSingleNode("Goods");
                        uint goodId = uint.Parse(Ngood.GetAttribute("Id"));
                        double price = Double.Parse(Ngood.GetAttribute("Price"));
                        Goods g = new Goods(goodId,Ngood.InnerText,price);
                        OrderDetail detail = new OrderDetail(id,g,quantity);

                        order.Details.Add(detail);
                    }

                    orderLists.Add(order);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("读取文件时失败！");
                Console.WriteLine(e.Message);
            }
        }
    }
}
