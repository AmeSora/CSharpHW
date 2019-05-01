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
    /// OrderDetail class : a entry of an order object
    /// to record the goods and its quantity
    /// </summary>
    [Table("orderdetails")]
    public class OrderDetail
    {


        /// <summary>
        /// default constructor
        /// </summary>
        public OrderDetail() { }

        /// <summary>
        /// OrderDetail constructor
        /// </summary>
        /// <param name="Num">orderDetail's num</param>
        /// <param name="goods">orderDetail's goods</param>
        /// <param name="quantity">goods' quantity</param>
        public OrderDetail(Goods goods, int quantity)
        {
            this.Goods = goods;
            this.Quantity = quantity;
        }

        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        /// <summary>
        /// orderDetail's goods
        /// </summary>
        public virtual Goods Goods { get; set; }

        /// <summary>
        /// goods quantity
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// the price amount of the orderDetail
        /// </summary>
        public float Amount
        {
            get => Goods != null ? Goods.Price * Quantity : 0;
        }

        public override bool Equals(object obj)
        {
            var detail = obj as OrderDetail;
            return detail != null &&
                   EqualityComparer<Goods>.Default.Equals(Goods, detail.Goods);
        }

        public override int GetHashCode()
        {
            return 785010553 + EqualityComparer<Goods>.Default.GetHashCode(Goods);
        }





        /// <summary>
        /// override ToString
        /// </summary>
        /// <returns>string:message of the OrderDetail object</returns>
        public override string ToString()
        {
            return $"orderDetail:{Goods},{Quantity}";
        }


    }
}
