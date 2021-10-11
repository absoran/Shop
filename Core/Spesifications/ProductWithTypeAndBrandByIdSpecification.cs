using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
using Core.Entities.Order;

namespace Core.Spesifications
{
    public class ProductWithTypeAndBrandByIdSpecification : BaseSpecification<Product>
    {            
        public ProductWithTypeAndBrandByIdSpecification(int id) : base(o => o.Id == id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }
    }
}
