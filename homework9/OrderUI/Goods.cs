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
    /// Goods class
    /// </summary>
    [Table("goods")]
    public class Goods
    {

        [Required]
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price
        {
            get { return price; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("value must >= 0!");
                price = value;
            }
        }

        private float price;

        /// <summary>
        /// default constructor
        /// </summary>
        public Goods() { }

        /// <summary>
        /// Goods constuctor
        /// </summary>
        /// <param name="id">goods' id</param>
        /// <param name="name">goods' name</param>
        /// <param name="price">>goods' price</param>
        public Goods(int id, string name, float price)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
        }



        public override bool Equals(object obj)
        {
            var goods = obj as Goods;
            return goods != null &&
                   Id == goods.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }


        /// <summary>
        /// override ToString
        /// </summary>
        /// <returns>string:message of the Goods object</returns>
        public override string ToString()
        {
            return $"Id:{Id}, Name:{Name}, Value:{Price}";
        }
    }
}
