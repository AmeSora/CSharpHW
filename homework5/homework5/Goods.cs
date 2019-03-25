using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework5
{
    class Goods
    {
        public uint Id { get; set; }
        public String Name { get; set; }
        public double Price { get; set; }

        public Goods(uint id, string name, double price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        public override string ToString()
        {
            return $"Goods : Id = {Id}, Name = {Name}, Price = {Price}";
        }

        public override bool Equals(object obj)
        {
            var good = obj as Goods;
            return good != null && Id == good.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
