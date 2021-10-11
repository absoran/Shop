using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public decimal Price { get; set; }
        public int? Unit { get; set; }
        public string Description { get; set; }
        public string ImgPath { get; set; }
        public int ProductTypeID { get; set; }
        public ProductType ProductType { get; set; }
        public int ProductBrandID { get; set; }
        public ProductBrand ProductBrand { get; set; }

    }
}
