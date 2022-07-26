using Catalog.WebApi.Entities;
using MongoDB.Driver;

namespace Catalog.WebApi.Data
{
    public class MongoCatalog : ICatalog
    {
        public MongoCatalog(IMongoDatabase mongoDatabase)
        {
            Products = mongoDatabase.GetCollection<Product>(nameof(Products));
        }

        private IMongoCollection<Product> Products { get; }

        public async Task CreateProduct(Product product) => await Products.InsertOneAsync(product);

        public async Task DeleteProduct(string id) => await Products.DeleteOneAsync(p => p.Id == id);

        public async Task<Product> GetProduct(string id) => await (await Products.FindAsync(p => p.Id == id)).SingleOrDefaultAsync();

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName) => await
            (await Products.FindAsync(p => p.Category == categoryName))
            .ToListAsync();

        public async Task<IEnumerable<Product>> GetProductByName(string name) => await
            (await Products.FindAsync(p => p.Name == name))
            .ToListAsync();

        public async Task<IEnumerable<Product>> GetProducts() => await (await Products.FindAsync(p => true)).ToListAsync();

        public async Task UpdateProduct(Product product) => await Products.ReplaceOneAsync(p => p.Id == product.Id, product);
    }
}
