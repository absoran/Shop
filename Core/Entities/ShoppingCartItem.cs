using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class ShoppingCartItem : BaseEntity
    {
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public decimal TotalPrice { get; set; }
        [Column(TypeName = "decimal(18,5)")]
        public decimal UnitPrice { get; set; }
        public int Productid { get; set; }
        public Product Product { get; set; }

    }
}
        ////public string ProductName { get; set; }
        ////public ICollection<Product> Products { get; set; }

        //[Column(TypeName = "decimal(18,5)")]
        //public decimal Price { get; set; }
        //public string PictureUrl { get; set; }
        //public string Brand { get; set; }
        //public string Type { get; set; }