using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.Order
{
    public class Order : BaseEntity
    {
        public Order()
        {

        }
        public Order(IReadOnlyList<OrderItem> orderItems, string buyerEmail, OrderAddress shipToAddress, Shipping Ship, decimal subtotal, string paymentIntentId)
        {
            OrderItems = orderItems;
            BuyerMail = buyerEmail;
            OrderAddress = shipToAddress;
            Shipping = Ship;
            Subtotal = subtotal;
            PaymentIntentId = paymentIntentId;
        }

        public string BuyerMail { get; set; }
        public int OrderAddressId{ get; set; }
        public OrderAddress OrderAddress { get; set; }
        public Shipping Shipping { get; set; }
        public IReadOnlyList<OrderItem> OrderItems { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public decimal Subtotal { get; set; }
        public OrderStatus Status { get; set; }
        public ShippingStatus shippingStatus { get; set; }
        public string PaymentIntentId { get; set; }

        public decimal GetTotal()
        {
            return Subtotal + Shipping.ShippingPrice;
        }
    }
}
