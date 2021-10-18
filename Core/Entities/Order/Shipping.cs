using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
namespace Core.Entities.Order
{
    public class Shipping : BaseEntity
    {
        public string Name { get; set; }
        public string description { get; set; }
        [Column(TypeName = "decimal(18,5)")]
        public decimal ShippingPrice { get; set; }
        public string EstdeliveryTime { get; set; }
    }
}
