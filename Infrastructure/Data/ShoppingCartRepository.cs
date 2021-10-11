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
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly IDatabase _database;

        public ShoppingCartRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<bool> DeleteCartAsync(string cartid)
        {
            return await _database.KeyDeleteAsync(cartid);
        }

        public async Task<ShoppingCart> GetCartAsync(string cartid)
        {
            var data = await _database.StringGetAsync(cartid);
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<ShoppingCart>(data);
        }

        public async Task<ShoppingCart> UpdateCartAsync(ShoppingCart cart)
        {
            var created = await _database.StringSetAsync(cart.SId, JsonSerializer.Serialize(cart), TimeSpan.FromDays(3));

            if (!created) return null;

            return await GetCartAsync(cart.SId);
        }
    }
}
