using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductList();
        Task<Product> GetProductById(int id);
        Task<Product> GetProductByName(string name);
        Task<Product> CreateProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(Product product);
        Task<Product> ProductIfExistWithId(int id);
        Task<Product> ProductIfExistWithName(string name);
        Task ValidateProductIfNotExist(Product product);
        Task ValidateProductByName(string name);
        Task ValidateProductById(int id);
        Task<ProductBrand> GetOrCreateProductBrandByName(string name);
        Task<ProductType> GetOrCreateProductTypeByName(string name);

    }
}
