using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Order;
using System.Threading.Tasks;
namespace Core.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, OrderAddress shippingAddress);
        Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);
        Task<Order> GetOrderByIdAsync(int Id, string buyerEmail);
        Task<IReadOnlyList<Shipping>> GetDeliveryMethodsAsync();
    }
}
