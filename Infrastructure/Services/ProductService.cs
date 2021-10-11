using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Entities;
using Core.Spesifications;

namespace Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IShopRepository<Product> _shoprepo;

        public ProductService(IUnitOfWork unitOfWork, IShopRepository<Product> shopRepository)
        {
            _unitOfWork = unitOfWork;
            _shoprepo = shopRepository;
        }
        public async Task<Product> CreateProduct(Product product)
        {
            return await _shoprepo.AddAsync(product);
        }

        public async Task<Product> GetProductById(int id)
        {
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(id);
            return product;
        }

        public async Task<Product> GetProductByName(string name)
        {
            var product = await _unitOfWork.Repository<Product>().GetByNameAsync(name);
            return product;
        }

        public async Task<IEnumerable<Product>> GetProductList()
        {
            var productlist = await _unitOfWork.Repository<Product>().ListAllAsync();
            return productlist;
        }

        public async Task UpdateProduct(Product product)
        {
            await ValidateProductIfNotExist(product);

            var productToEdit = await _unitOfWork.Repository<Product>().GetByIdAsync(product.Id);
            if (productToEdit == null)
            {
                throw new ApplicationException($"entity could not loaded or entity not exist");
            }
            await _unitOfWork.Repository<Product>().UpdateAsync(product);
        }
        public async Task DeleteProduct(Product product)
        {
            await ValidateProductIfNotExist(product);
            await _unitOfWork.Repository<Product>().DeleteAsync(product);
        }
        public async Task<ProductBrand> GetOrCreateProductBrandByName(string name)
        {
            var spec = new BrandByNameSpecification(name);
            var existingprod = await _shoprepo.GetEntityWithSpec(spec);

            if (existingprod == null)
            {
                var newbrand = new ProductBrand();
                newbrand.Name = name.ToLower();
                newbrand.CreatedDate = DateTime.Now;
                await _unitOfWork.Repository<ProductBrand>().AddAsync(newbrand);
                return newbrand;
            }
            else
            {
                return existingprod.ProductBrand;
            }
        }
        public async Task<ProductType> GetOrCreateProductTypeByName(string name)
        {
            var spec = new TypeByNameSpecification(name);
            var existingprod = await _shoprepo.GetEntityWithSpec(spec);

            if (existingprod == null)
            {
                var newtype = new ProductType();
                newtype.Name = name.ToLower();
                newtype.CreatedDate = DateTime.Now;
                await _unitOfWork.Repository<ProductType>().AddAsync(newtype);
                return newtype;
            }
            else
            {
                return existingprod.ProductType;
            }
        }
        public async Task<ProductType> GetOrCreateProductType(Product product)
        {
            var existingProduct = await _unitOfWork.Repository<Product>().GetByIdAsync(product.Id);
            if (existingProduct != null)
            {
                return existingProduct.ProductType;
            }
            else
            {
                await _unitOfWork.Repository<ProductType>().AddAsync(product.ProductType);
                return product.ProductType;
            }
        }
        public async Task ValidateProductIfNotExist(Product product)
        {
            var existingEntity = await _unitOfWork.Repository<Product>().GetByIdAsync(product.Id);
            if (existingEntity == null)
                throw new ApplicationException($"{product.ToString()} with this id is not exists");
        }

        public async Task<Product> ProductIfExistWithId(int id)
        {
            var spec = new ProductWithTypeAndBrandByIdSpecification(id);
            var existingEntity = await _shoprepo.GetEntityWithSpec(spec);
            if (existingEntity == null)
            {
                throw new ApplicationException("product with this id not exists");
            }
            else { return existingEntity; }

        }
        public async Task<Product> ProductIfExistWithName(string name)
        {
            var spec = new ProductAndIncludesByNameSpecification(name);
            var existingEntity = await _shoprepo.GetEntityWithSpec(spec);
            if (existingEntity == null)
            {
                return null;
            }
            else { return existingEntity; }         
        }
        public async Task ValidateProductByName(string name)
        {
            var spec = new ProductAndIncludesByNameSpecification(name);
            var existingEntity = await _shoprepo.GetEntityWithSpec(spec);
            if (existingEntity == null)
                throw new ApplicationException($"{existingEntity.ToString()} with this name is not exists");
        }
        public async Task ValidateProductById(int id)
        {
            var spec = new ProductWithTypeAndBrandByIdSpecification(id);
            var existingEntity = await _shoprepo.GetEntityWithSpec(spec);
            if (existingEntity == null)
                throw new ApplicationException($"{existingEntity.ToString()} with this name is not exists");
        }
    }
}
