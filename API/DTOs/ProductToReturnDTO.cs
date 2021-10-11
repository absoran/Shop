using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace API.DTOs
{
    public class ProductToReturnDTO
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Unit{ get; set; }
        public string ImgUrl { get; set; }
        public string ProductBrand { get; set; }
        public string ProductType { get; set; }
    }
}
