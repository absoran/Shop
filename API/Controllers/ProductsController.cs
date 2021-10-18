using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Spesifications;
using AutoMapper;
using API.Errors;
using API.DTOs;
namespace API.Controllers
{
    [Route("api/products/")]
    public class ProductsController : BaseController
    {
        private readonly IShopRepository<Product> _productRepo;
        private readonly IShopRepository<ProductBrand> _productBrandRepo;
        private readonly IShopRepository<ProductType> _productTypeRepo;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        
        public ProductsController(IShopRepository<Product> productsRepo, IShopRepository<ProductBrand> productBrandRepo,
        IShopRepository<ProductType> productTypeRepo, IMapper mapper,IProductService productService)
        {
            _productRepo = productsRepo;
            _productBrandRepo = productBrandRepo;
            _productTypeRepo = productTypeRepo;
            _mapper = mapper;
            _productService = productService;
        }
        //[HttpPut]
        //[Route("test/")]
        //public async Task<Product> updatetest(ProductToReturnDTO product)
        //{
        //    await _productService.ValidateProductById(product.id);
        //    var spec = new ProductWithTypeAndBrandByIdSpecification(product.id);
        //    var productToUpdate = await _productRepo.GetEntityWithSpec(spec);
        //    productToUpdate.Description = product.Description;
        //    productToUpdate.ImgPath = product.ImgUrl;
        //    productToUpdate.Price = product.Price;
        //    productToUpdate.Name = product.Name;
        //    productToUpdate.CreatedDate = DateTime.Now;
        //    productToUpdate.Unit = product.Unit;
        //    if (productToUpdate.ProductBrand.Name != product.ProductBrand)
        //    {
        //        var newbrand = await _productService.GetOrCreateProductBrandByName(product.Name);
        //        productToUpdate.ProductBrand = newbrand;
        //        productToUpdate.ProductBrandID = newbrand.Id;
        //    }
        //    if(productToUpdate.ProductType.Name != product.ProductType)
        //    {
        //        var newtype = await _productService.GetOrCreateProductTypeByName(product.ProductType);
        //        productToUpdate.ProductType = newtype;
        //        productToUpdate.ProductTypeID = newtype.Id;
        //    }
        //    return await _productRepo.UpdateAsync(productToUpdate);
            
        //}

        [HttpGet]
        [Route("getbyid/{id}")]
        public async Task<ActionResult<ProductToReturnDTO>> GetProductByID(int id)
        {
            var spec = new ProductWithTypeAndBrandByIdSpecification(id);
            var product = await _productRepo.GetEntityWithSpec(spec);
            if (product == null)
            {
                return NotFound(new APIResponse(404));
            }
            else
            {
                return _mapper.Map<Product, ProductToReturnDTO>(product);
            }
        }
        [HttpGet]
        [Route("getbyname/{name}")]
        public async Task<ActionResult<ProductToReturnDTO>> GetProductByName(string name)
        {
            var spec = new ProductAndIncludesByNameSpecification(name);
            var product = await _productRepo.GetEntityWithSpec(spec);
            if (product == null)
            {
                return NotFound(new APIResponse(404));
            }
            else
            {
                return _mapper.Map<Product, ProductToReturnDTO>(product);
            }
        }

        [HttpGet]
        [Route("getall")]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDTO>>> GetProducts()
        {
            var spec = new ProductsWithTypeAndBrandSpecification();
            var productlist = await _productRepo.ListAsync(spec);
            var mappedlist = new List<ProductToReturnDTO>();
            foreach(var product in productlist)
            {
                var mapped =_mapper.Map<Product,ProductToReturnDTO>(product);
                mappedlist.Add(mapped);
            }
            return mappedlist;
        }
        [HttpGet]
        [Route("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _productBrandRepo.ListAllAsync());
        }
        [HttpGet]
        [Route("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await _productTypeRepo.ListAllAsync());
        }

        [HttpPost]
        [Route("addproduct")]
        public async Task<ActionResult<Product>> AddProduct(ProductToAddDTO dtoproduct)
        {
            var newproduct = new Product();
            newproduct.CreatedDate = DateTime.Now;
            newproduct.Name = dtoproduct.Name;
            newproduct.Price = dtoproduct.Price;
            newproduct.Unit = dtoproduct.Unit;
            newproduct.ImgPath = dtoproduct.ImgUrl;
            newproduct.Description = dtoproduct.Description;
            var productforincludes = await _productService.ProductIfExistWithName(dtoproduct.Name);
            if (productforincludes == null)
            {
                var type = await _productService.GetOrCreateProductTypeByName(dtoproduct.ProductType);
                var brand = await _productService.GetOrCreateProductBrandByName(dtoproduct.ProductBrand);
                newproduct.ProductBrand = brand;
                newproduct.ProductBrandID = brand.Id;
                newproduct.ProductType = type;
                newproduct.ProductTypeID = type.Id;
                return await _productService.CreateProduct(newproduct);

            }
            else
            {
                newproduct.ProductType = productforincludes.ProductType;
                newproduct.ProductTypeID = productforincludes.ProductTypeID;
                newproduct.ProductBrand = productforincludes.ProductBrand;
                newproduct.ProductBrandID = productforincludes.ProductBrandID;      
                await _productService.CreateProduct(newproduct);
                return newproduct;
            }           
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<Product> Delete(int id)
        {
            var spec = new ProductWithTypeAndBrandByIdSpecification(id);
            var productTodelete = await _productRepo.GetEntityWithSpec(spec);
            return await _productRepo.DeleteAsync(productTodelete);
        }
        [HttpPut]
        [Route("updateproduct/")]
        public async Task<Product> Update(ProductToReturnDTO product)
        {
            await _productService.ValidateProductById(product.id);
            var spec = new ProductWithTypeAndBrandByIdSpecification(product.id);
            var productToUpdate = await _productRepo.GetEntityWithSpec(spec);
            productToUpdate.Description = product.Description;
            productToUpdate.ImgPath = product.ImgUrl;
            productToUpdate.Price = product.Price;
            productToUpdate.Name = product.Name;
            productToUpdate.CreatedDate = DateTime.Now;
            productToUpdate.Unit = product.Unit;
            if (productToUpdate.ProductBrand.Name != product.ProductBrand)
            {
                var newbrand = await _productService.GetOrCreateProductBrandByName(product.Name);
                productToUpdate.ProductBrand = newbrand;
                productToUpdate.ProductBrandID = newbrand.Id;
            }
            if (productToUpdate.ProductType.Name != product.ProductType)
            {
                var newtype = await _productService.GetOrCreateProductTypeByName(product.ProductType);
                productToUpdate.ProductType = newtype;
                productToUpdate.ProductTypeID = newtype.Id;
            }
            return await _productRepo.UpdateAsync(productToUpdate);

        }
    }
}
