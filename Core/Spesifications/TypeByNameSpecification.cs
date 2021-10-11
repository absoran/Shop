using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
namespace Core.Spesifications
{
    public class TypeByNameSpecification : BaseSpecification<Product>
    {
        public TypeByNameSpecification(string name) : base(p => p.ProductType.Name == name)
        {
            AddInclude(p => p.ProductType);
        }
    }
}
