using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using StackExchange.Redis;

namespace Infrastructure.Data
{
    public class ShoppingCartRepository : ShopRepository<ShoppingCart>,IShoppingCartRepository
    {
        private readonly IDatabase _database;
        private readonly ShopDbContext _dbContext;

        public ShoppingCartRepository(IConnectionMultiplexer redis,ShopDbContext context) : base(context)
        {
            _database = redis.GetDatabase();
            _dbContext = context;
        }
        public async Task<ShoppingCart> DeleteCartAsync(int id)
        {
            try
            {
                var cart = await _dbContext.Set<ShoppingCart>().FindAsync(id);
                _dbContext.Set<ShoppingCart>().Remove(cart);
                _dbContext.SaveChanges();
                return cart;
            }
            catch
            {
                throw new ApplicationException("cant find entity");
            }
        }

        public async Task<ShoppingCart> GetCartAsync(int id)
        {
            var data = await _database.StringGetAsync(id.ToString());
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<ShoppingCart>(data);
        }

        public async Task<ShoppingCart> UpdateCartAsync(ShoppingCart cart)
        {
            var created = await _database.StringSetAsync(cart.SId, JsonSerializer.Serialize(cart), TimeSpan.FromDays(3));

            if (!created) return null;

            return await GetCartAsync(cart.Id);
        }
    }
}
