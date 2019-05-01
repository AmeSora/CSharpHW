using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderUI
{

    /// <summary>
    /// Customer class
    /// </summary>
    [Table("customers")]
    public class Customer
    {

        [Required]
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }


        /// <summary>
        /// default constructor
        /// </summary>
        public Customer() { }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="id">customer id</param>
        /// <param name="name">customer name </param>
        public Customer(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }


        public override string ToString()
        {
            return $"customerId:{Id}, CustomerName:{Name}";
        }

        public override bool Equals(object obj)
        {
            var customer = obj as Customer;
            return customer != null &&
                   Id == customer.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }
    }
}
