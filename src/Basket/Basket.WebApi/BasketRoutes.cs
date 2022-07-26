using Basket.WebApi.Data;
using Basket.WebApi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Basket.WebApi
{
    public static class BasketRoutes
    {
        public static WebApplication AddBasketRoutes(this WebApplication app)
        {
            const string BaseRoute = "/api/v1/Basket";

            app
                .MapGet(BaseRoute + "/{userName}",
                async (string userName, [FromServices] IBasketData data) => await data.GetShoppingCart(userName) ?? new ShoppingCart { User = userName });

            app
                .MapPost(BaseRoute, async ([FromBody] ShoppingCart shoppingCart, [FromServices] IBasketData data) => await data.UpdateShoppingCart(shoppingCart));

            app
                .MapDelete(BaseRoute + "/{userName}", async (string userName, [FromServices] IBasketData data) => await data.Delete(userName));

            return app;
        }
    }
}
