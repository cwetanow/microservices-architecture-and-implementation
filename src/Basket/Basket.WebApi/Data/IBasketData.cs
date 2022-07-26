using Basket.WebApi.Entities;

namespace Basket.WebApi.Data
{
    public interface IBasketData
    {
        Task<ShoppingCart?> GetShoppingCart(string user);
        Task UpdateShoppingCart(ShoppingCart shoppingCart);
        Task Delete(string user);
    }
}
