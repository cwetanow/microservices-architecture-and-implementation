using Catalog.WebApi.Data;
using Catalog.WebApi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.WebApi.Routes
{
    public static class CatalogRoutes
    {
        public static WebApplication AddCatalogRoutes(this WebApplication application)
        {
            const string BaseRoute = "/api/Products";

            application
                .MapGet(BaseRoute,
                async ([FromServices] ICatalog catalog) => await catalog.GetProducts());

            application
                .MapGet(BaseRoute + "/{id}",
                async (string id, [FromServices] ICatalog catalog) => await catalog.GetProduct(id) is Product product ?
                Results.Ok(product) : Results.NotFound());

            application
                .MapGet(BaseRoute + "/ByCategory/{category}",
                async (string category, [FromServices] ICatalog catalog) => await catalog.GetProductByCategory(category));

            application
                .MapGet(BaseRoute + "/ByName/{name}",
                async (string name, [FromServices] ICatalog catalog) => await catalog.GetProductByName(name));

            application
                .MapPost(BaseRoute,
                async ([FromBody] Product product, [FromServices] ICatalog catalog) => await catalog.CreateProduct(product));

            application
                .MapPut(BaseRoute,
                async ([FromBody] Product updatedProduct, [FromServices] ICatalog catalog) => await catalog.UpdateProduct(updatedProduct));

            application
                .MapDelete(BaseRoute + "/{id}",
                async (string id, [FromServices] ICatalog catalog) => await catalog.DeleteProduct(id));

            return application;
        }
    }
}
