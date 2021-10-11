using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Order;

namespace Core.Spesifications
{
    public class OrderByUserMailSpecification : BaseSpecification<Order>
    {
        public OrderByUserMailSpecification(string email) : base (p => p.BuyerMail == email)
        {

        }
    }
}
