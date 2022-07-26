using Basket.WebApi.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.WebApi.Data
{
    public class RedisBasketData : IBasketData
    {
        private readonly IDistributedCache cache;

        public RedisBasketData(IDistributedCache cache)
        {
            this.cache = cache;
        }

        public async Task Delete(string user) => await cache.RemoveAsync(user);

        public async Task<ShoppingCart?> GetShoppingCart(string user)
        {
            var serializedBasket = await cache.GetStringAsync(user);
            if (string.IsNullOrEmpty(serializedBasket))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<ShoppingCart>(serializedBasket);
        }

        public async Task UpdateShoppingCart(ShoppingCart shoppingCart) => 
            await cache.SetStringAsync(shoppingCart.User, JsonConvert.SerializeObject(shoppingCart));
    }
}
