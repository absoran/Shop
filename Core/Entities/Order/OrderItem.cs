using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Core.Entities.Order
{
    public class OrderItem : BaseEntity
    {
        public OrderItem()
        {

        }
        public OrderItem(ItemOrdered itemordered,decimal price,int quantity)
        {
            ItemOrdered = itemordered;
            Price = price;
            Quantity = quantity;
        }
        public ItemOrdered ItemOrdered { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
