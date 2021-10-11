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
        private readonly ILogger<CartService> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public CartService(IShoppingCartRepository shoppingCartRepository, IProductRepository productRepository, ILogger<CartService> logger, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository)); ;
            _shoppingCartRepository = shoppingCartRepository ?? throw new ArgumentNullException(nameof(shoppingCartRepository)); ;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<ShoppingCart> GetCartById(int id)
        {
            var spec = new CartWithItemsByCartIdSpecification(id.ToString());
            return await _unitOfWork.Repository<ShoppingCart>().GetEntityWithSpec(spec);
        }

        public async Task RemoveItem(int cartId, int cartItemId)
        {
            var spec = new CartWithItemsByCartIdSpecification(cartId.ToString());
            var cart = (await _unitOfWork.Repository<ShoppingCart>().GetEntityWithSpec(spec));
            cart.RemoveItem(cartItemId);
            await _shoppingCartRepository.UpdateCartAsync(cart);
        }
        public async Task AddItem(string sid, int productId)
        {
            var cart = await GetExistingOrCreateNewCart(sid);
            var product = await _productRepository.GetProductByIdAsync(productId);
            cart.AddItem(product.Id, 1, product.Price);
            await _shoppingCartRepository.UpdateCartAsync(cart);
        }

        public async Task ClearCart(int cartid)
        {
            var cart = await _shoppingCartRepository.GetCartAsync(cartid.ToString());
            if (cart == null)
            {
                throw new ApplicationException("selected cart is empty or null");
            }
            else
            {
                cart.ClearCartItems();
                await _shoppingCartRepository.UpdateCartAsync(cart);
            }
        }
        public async Task<ShoppingCart> GetExistingOrCreateNewCart(string sid)
        {
            var spec = new CartWithItemsByCartIdSpecification(sid);
            var cart = await _unitOfWork.Repository<ShoppingCart>().GetEntityWithSpec(spec);

            //var cart2 = await _shoppingCartRepository.GetCartAsync(sid); //2. yol

            if (cart != null) { return cart; } //if cart exist
            var newcart = new ShoppingCart
            {
                SId = sid
            };
            await _unitOfWork.Repository<ShoppingCart>().AddAsync(newcart);
            await _unitOfWork.Commit();
            return newcart;
        }
    }
}
