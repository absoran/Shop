using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IShoppingCartRepository
    {
        Task<ShoppingCart> GetCartAsync(string cartid);
        Task<ShoppingCart> UpdateCartAsync(ShoppingCart cart);
        Task<bool> DeleteCartAsync(string cartid);
    }
}
