using System;
using System.Collections.Generic;
using System.Text;
using Core.Interfaces;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopDbContext dbContext;

        public ProductRepository(ShopDbContext context)
        {
            dbContext = context;
        }
        /*
         * include'lar system-null-referance-exception fırlatmasın,eager loading için eklendi
         */
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await dbContext.Products
                .Include(p => p.ProductBrand)
                .Include(p => p.ProductType)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await dbContext.Products
                .Include(p => p.ProductBrand)
                .Include(p => p.ProductType)
                .ToListAsync();
        }
        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await dbContext.ProductTypes.ToArrayAsync();
        }
        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await dbContext.ProductBrands.ToArrayAsync();
        }
    }
}
