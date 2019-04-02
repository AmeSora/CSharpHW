using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework6
{
    class OrderDetail
    {
        public uint Id { get; set; }
        public Goods Goods { get; set; }
        public uint Quantity { get; set; }

        public OrderDetail(uint id, Goods goods, uint quantity)
        {
            Id = id;
            Goods = goods;
            Quantity = quantity;
        }

        public override bool Equals(object obj)
        {
            var detail = obj as OrderDetail;
            return detail != null &&
                   Id == detail.Id &&
                   Goods.Equals(detail.Goods) &&
                   Quantity == detail.Quantity;
        }

        public override int GetHashCode()
        {
            var hashCode = 1087523697;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + Goods.GetHashCode();
            hashCode = hashCode * -1521134295 + Quantity.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return $"OrderDetail : Id = {Id}, " + Goods.ToString() + ", Quantity = {Quantity}";
        }
    }
}