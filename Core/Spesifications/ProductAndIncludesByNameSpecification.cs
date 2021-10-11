using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
namespace Core.Spesifications
{
    public class ProductAndIncludesByNameSpecification : BaseSpecification<Product>
    {
        public ProductAndIncludesByNameSpecification(string name) : base (p=> p.Name==name)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }
    }
}
