using Catalog.WebApi.Data;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.WebApi.Routes
{
    public static class CatalogRoutes
    {
        public static WebApplication AddCatalogRoutes(this WebApplication application)
        {
            application
                .MapGet("/api/Products", GetAllProducts);

            return application;
        }

        private static async Task GetAllProducts([FromServices] ICatalog catalog) => await catalog.GetProducts();
    }
}
