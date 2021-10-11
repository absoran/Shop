using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;


namespace Core.Interfaces
{
    public interface ICartService
    {
        Task<ShoppingCart> GetCartById(int id);
        Task AddItem(string sid, int productId);
        Task RemoveItem(int cartId, int cartItemId);
        Task ClearCart(int cartid);
        Task<ShoppingCart> GetExistingOrCreateNewCart(string sid);
    }
}
