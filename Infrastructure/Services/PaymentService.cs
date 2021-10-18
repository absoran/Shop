using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.Order;
using Core.Interfaces;
using Core.Spesifications;
using Microsoft.Extensions.Configuration;
using Stripe;
using Order = Core.Entities.Order.Order;
using Product = Core.Entities.Product;

namespace Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;
        public PaymentService(IShoppingCartRepository shoppingCartRepository,IUnitOfWork unitOfWork,IConfiguration config)
        {
            _config = config;
            _shoppingCartRepository = shoppingCartRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ShoppingCart> CreateOrUpdatePaymentIntent(int cartid)
        {
            StripeConfiguration.ApiKey = _config["SuperSecretKey"];
            var cart = await _shoppingCartRepository.GetCartAsync(cartid);
            if (cart == null) return null;
            var shippingprice = 0m;
            if (cart.ShippingID.HasValue)
            {
                var shippingmethod = await _unitOfWork.Repository<Core.Entities.Order.Shipping>().GetByIdAsync((int)cart.ShippingID);
                shippingprice = shippingmethod.ShippingPrice;
            }
            foreach (var item in cart.Items)
            {
                var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                if (item.UnitPrice != productItem.Price)
                {
                    item.UnitPrice = productItem.Price;
                }
            }
            var service = new PaymentIntentService();
            PaymentIntent intent;
            if (string.IsNullOrEmpty(cart.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)cart.Items.Sum(i => i.Quantity * (i.UnitPrice * 100))
                    + (long)shippingprice * 100,
                    Currency = "try",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                intent = await service.CreateAsync(options);
                cart.PaymentIntentId = intent.Id;
                cart.ClientSecret = intent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)cart.Items.Sum(i => i.Quantity * (i.UnitPrice * 100))
                    + (long)shippingprice * 100
                };
                await service.UpdateAsync(cart.PaymentIntentId, options);
            }

            await _shoppingCartRepository.UpdateCartAsync(cart);
            var spec = new OrderByPaymentIntentIdSpecification(cart.PaymentIntentId);
            var order = await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
            order.Status = OrderStatus.Processing;
            return cart;
        }

        public async Task<Order> UpdatePaymentOrderFailed(string paymentIntentId)
        {
            var spec = new OrderByPaymentIntentIdSpecification(paymentIntentId);
            var order = await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
            if (order == null) return null;
            order.Status = OrderStatus.PaymentFailed;
            await _unitOfWork.Commit();
            return order;
        }

        public async Task<Order> UpdatePaymentOrderSucceeded(string paymentIntentId)
        {
            var spec = new OrderByPaymentIntentIdSpecification(paymentIntentId);
            var order = await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
            if (order == null) return null;
            
            order.Status = OrderStatus.PaymentReceived;
            _unitOfWork.Repository<Order>().Update(order);
            await _unitOfWork.Commit();
            return order;
        }

        public async Task<Order> ConfirmOrder(string paymentIntentId)
        {
            var options = new PaymentIntentConfirmOptions
            {
                PaymentMethod = "pm_card_visa"
            };
            var spec = new OrderByPaymentIntentIdSpecification(paymentIntentId);
            var order = await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
            var service = new PaymentIntentService();
            service.Confirm(order.PaymentIntentId, options);
            order.Status = OrderStatus.PaymentReceived;
            return order;
        }
    }
}
