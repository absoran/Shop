using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
namespace Core.Spesifications
{
    public class BrandByNameSpecification : BaseSpecification<Product>
    {
        public BrandByNameSpecification(string name) : base(p => p.ProductBrand.Name == name)
        {
            AddInclude(p => p.ProductBrand);
        }
    }
}
