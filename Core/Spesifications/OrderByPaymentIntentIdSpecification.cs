using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Order;
namespace Core.Spesifications
{
    public class OrderByPaymentIntentIdSpecification : BaseSpecification<Order>
    {
        public OrderByPaymentIntentIdSpecification(string paymentintendid) : base (p => p.PaymentIntentId == paymentintendid)
        {

        }
    }
}
