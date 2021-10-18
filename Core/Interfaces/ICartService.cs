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
        Task<ShoppingCart> AddItem(int  id, int productId);
        Task<ShoppingCart> RemoveItem(int cartId, int cartItemId);
        Task<ShoppingCart> ClearCart(int cartid);
        Task<ShoppingCart> GetExistingCart(int id);
        Task<ShoppingCart> CreateNewCart();
        public Task ValidateCart(string sid);
        public Task ValidateCart(int id);
    }
}
