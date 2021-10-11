using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Order;


namespace Core.Spesifications
{
    public class OrdersWithItemsAndOrderingSpecification : BaseSpecification<Order>
    {
        public OrdersWithItemsAndOrderingSpecification(string email) : base(p => p.BuyerMail == email)
        {
            AddInclude(p => p.OrderItems);
            AddInclude(p => p.Shipping);
            AddInclude(p => p.CreatedDate);
        }
        public OrdersWithItemsAndOrderingSpecification(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.OrderItems);
            AddInclude(p => p.Shipping);
            AddInclude(p => p.CreatedDate);
        }
        public OrdersWithItemsAndOrderingSpecification(int id, string email) : base(o => o.Id == id && o.BuyerMail == email)
        {
            AddInclude(p => p.OrderItems);
            AddInclude(p => p.Shipping);
            AddInclude(p => p.CreatedDate);
        }
    }
}
