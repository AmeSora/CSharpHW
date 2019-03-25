using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework5
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

        public Order GetById(uint orderId)
        {
            foreach(Order o in orderLists)
            {
                if (o.Oid == orderId)
                    return o;
            }
            return null;
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

        public List<Order> QueryByGoodsName(String name)
        {
            var re = from order in orderLists from s in order.Details where s.Goods.Name == name select order;
            return re.ToList<Order>();
        }

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
    }
}
