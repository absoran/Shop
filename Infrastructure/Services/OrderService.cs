using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Order;
using Core.Interfaces;
using Core.Spesifications;
using Core.Entities;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IShoppingCartRepository _cartRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;
        public OrderService(IShoppingCartRepository shoppingCartRepository,IUnitOfWork unitOfWork, IPaymentService paymentService)
        {
            _cartRepository = shoppingCartRepository;
            _unitOfWork = unitOfWork;
            _paymentService = paymentService;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, OrderAddress shippingAddress)
        {
            var spec = new CartWithItemsByCartIdSpecification(basketId);
            var cartWithSpec = await _unitOfWork.Repository<ShoppingCart>().GetEntityWithSpec(spec);
            var cart = await _cartRepository.GetCartAsync(basketId);
            var items = new List<OrderItem>();

            foreach(var item in cartWithSpec.Items)//iterate through cart items
            {
                var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                var itemordered = new ItemOrdered(product.Id, product.Name, product.ImgPath);
                var orderItem = new OrderItem(itemordered, product.Price, item.Quantity);
                items.Add(orderItem);
            }
            var shipping = await _unitOfWork.Repository<Shipping>().GetByIdAsync(deliveryMethodId);
            var subPrice = items.Sum(p => p.Price * p.Quantity);
            var orderspec = new OrderByPaymentIntentIdSpecification(cart.PaymentIntentId);
            var checkorder = await _unitOfWork.Repository<Order>().GetEntityWithSpec(orderspec);
            
            if(checkorder != null)
            {
                _unitOfWork.Repository<Order>().Delete(checkorder);
                await _paymentService.CreateOrUpdatePaymentIntent(cartWithSpec.PaymentIntentId);
            }
            var newOrder = new Order(items, buyerEmail, shippingAddress,shipping,subPrice,cartWithSpec.PaymentIntentId);
            _unitOfWork.Repository<Order>().Add(newOrder);

            var response = await _unitOfWork.Commit();
            if (response <= 0) return null;
            return newOrder;
        }

        public async Task<IReadOnlyList<Shipping>> GetDeliveryMethodsAsync()
        {
            return await _unitOfWork.Repository<Shipping>().ListAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int Id, string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(Id, buyerEmail);
            return await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(buyerEmail);
            return await _unitOfWork.Repository<Order>().ListAsync(spec);
        }
    }
}
