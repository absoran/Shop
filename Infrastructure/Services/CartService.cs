using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
using System.Linq;
using Core.Interfaces;
using Core.Spesifications;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class CartService : ICartService
    {
        private readonly IProductRepository _productRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IShopRepository<ShoppingCart> _shopRepository;

        public CartService(IShoppingCartRepository shoppingCartRepository, IProductRepository productRepository, IUnitOfWork unitOfWork, IShopRepository<ShoppingCart> shopRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository)); ;
            _shoppingCartRepository = shoppingCartRepository ?? throw new ArgumentNullException(nameof(shoppingCartRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _shopRepository = shopRepository ?? throw new ArgumentNullException(nameof(shopRepository));
        }
        public async Task<ShoppingCart> GetCartById(int id)
        {
            var spec = new CartWithItemsByCartIdSpecification(id);
            return await _unitOfWork.Repository<ShoppingCart>().GetEntityWithSpec(spec);
        }

        public async Task<ShoppingCart> RemoveItem(int cartId, int cartItemId)
        {
            var spec = new CartWithItemsByCartIdSpecification(cartId.ToString());
            var cart = (await _unitOfWork.Repository<ShoppingCart>().GetEntityWithSpec(spec));
            cart.RemoveItem(cartItemId);
            await _shoppingCartRepository.UpdateCartAsync(cart);
            return cart;
        }
        public async Task<ShoppingCart> AddItem(int cartid, int productId)
        {
            var cart = await GetExistingCart(cartid);
            var product = await _productRepository.GetProductByIdAsync(productId);
            cart.AddItem(product.Id, 1, product.Price);
            await _shopRepository.UpdateAsync(cart); //sıkıntı var test yap
            return cart;
        }

        public async Task<ShoppingCart> ClearCart(int cartid)
        {
            var cart = await _shoppingCartRepository.GetCartAsync(cartid);
            if (cart == null)
            {
                throw new ApplicationException("selected cart is empty or null");
            }
            else
            {
                cart.ClearCartItems();
                await _shoppingCartRepository.UpdateCartAsync(cart);
                return cart;
            }
        }
        public async Task<ShoppingCart> GetExistingCart(int id)
        {
            var spec = new CartWithItemsByCartIdSpecification(id);
            var cart = await _unitOfWork.Repository<ShoppingCart>().GetEntityWithSpec(spec);
            return cart;

        }
        public async Task<ShoppingCart> CreateNewCart()
        {
            var newcart = new ShoppingCart();
            newcart.CreatedDate = DateTime.Now;
            await _unitOfWork.Repository<ShoppingCart>().AddAsync(newcart);
            await _unitOfWork.Commit();
            await ValidateCart(newcart.Id);
            return newcart;
        }
        public async Task ValidateCart(int id)
        {
            var spec = new CartWithItemsByCartIdSpecification(id);
            var cart = await _shopRepository.GetEntityWithSpec(spec);
            //var cart = await _shopRepository.GetByIdAsync(id);
            if (cart == null) { throw new ApplicationException("cart does not exist"); }
        }
        public async Task ValidateCart(string sid)
        {
            var spec = new CartWithItemsByCartIdSpecification(sid);
            var cart = await _unitOfWork.Repository<ShoppingCart>().GetEntityWithSpec(spec);
            if (cart == null) { throw new ApplicationException("cart does not exist"); }
        }

    }
}
