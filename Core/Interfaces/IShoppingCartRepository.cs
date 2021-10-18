using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IShoppingCartRepository
    {
        //Task<ShoppingCart> CreateOrGetCart(string cartid);
        Task<ShoppingCart> GetCartAsync(int id);
        Task<ShoppingCart> UpdateCartAsync(ShoppingCart cart);
        Task<ShoppingCart> DeleteCartAsync(int id);
    }
}
