using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
using Core.Entities.Order;


namespace Core.Spesifications
{
    public class CartWithItemsByCartIdSpecification : BaseSpecification<ShoppingCart>
    {
        public CartWithItemsByCartIdSpecification(string cartid) : base (o => o.SId == cartid)
        {
            AddInclude(p => p.Items);
        }
    }
}
