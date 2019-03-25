using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework5
{
    class Customer
    {
        public String Name { get; set; }
        public uint Id { get; set; }

        public Customer(uint id, string name)
        {
            this.Name = name;
            this.Id = id;
        }

        public override bool Equals(object obj)
        {
            var customer = obj as Customer;
            return customer != null &&
                   Name == customer.Name &&
                   Id == customer.Id;
        }

        public override String ToString()
        {
            return $"Customer : Id = {Id}, Name = {Name}";
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() + Id.GetHashCode();
        }
    }
}
