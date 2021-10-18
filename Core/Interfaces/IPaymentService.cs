using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Order;
using Core.Entities;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IPaymentService
    {
        Task<ShoppingCart> CreateOrUpdatePaymentIntent(int cartid);
        Task<Order> UpdatePaymentOrderSucceeded(string paymentIntentId);
        Task<Order> UpdatePaymentOrderFailed(string paymentIntentId);
        Task<Order> ConfirmOrder(string paymentIntentId);
    }
}
