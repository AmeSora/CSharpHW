using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework6
{
    class Order : IComparable
    {
        public uint Oid { get; set; }

        public Customer Customer { get; set; }

        private List<OrderDetail> details = new List<OrderDetail>();

        public List<OrderDetail> Details { get => this.details; }

        /// <summary>
        /// override CompareTo method
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            var o = obj as Order;
            return this.Oid.CompareTo(o.Oid);
        }

        /// <summary>
        /// override ToString method
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"OrderId : {Oid}, " + Customer.ToString() + ", Total Price = " + GetTotalPrice();
        }

        /// <summary>
        /// override Equals method
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var order = obj as Order;
            return obj != null && Oid == order.Oid && Customer.Equals(order.Customer);
        }

        /// <summary>
        /// override GetHashCode method
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Oid.GetHashCode() + Customer.GetHashCode();
        }

        public Order(uint orderId, Customer c)
        {
            Oid = orderId;
            Customer = c;
        }

        public void AddDetails(OrderDetail orderDetail)
        {
            if (this.Details.Contains(orderDetail))
            {
                throw new Exception($"orderDetails-{orderDetail.Id} is already existed!");
            }
            details.Add(orderDetail);
        }

        public double GetTotalPrice()
        {
            double re = 0;

            foreach (OrderDetail order in details)
            {
                re += order.Goods.Price * order.Quantity;
            }

            return re;
        }

    }
}
