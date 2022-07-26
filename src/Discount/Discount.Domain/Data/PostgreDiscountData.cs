using Discount.Domain.Entities;
using System.Data;
using Dapper;

namespace Discount.Domain.Data
{
    public class PostgreDiscountData : IDiscountData
    {
        private readonly Func<IDbConnection> connectionFactory;

        public PostgreDiscountData(Func<IDbConnection> connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public async Task CreateDiscount(Coupon coupon)
        {
            using var connection = connectionFactory();

            await connection.ExecuteAsync("INSERT INTO Coupon (ProductId, Description, Amount) VALUES (@ProductId, @Description, @Amount)",
                            coupon);
        }

        public async Task<Coupon> GetDiscount(string productId)
        {
            using var connection = connectionFactory();

            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
                ("SELECT * FROM Coupon WHERE ProductId = @ProductId", new { ProductId = productId });

            return coupon ?? Coupon.CreateWithNoDiscount(productId);
        }

        public async Task DeleteDiscount(string productId)
        {
            using var connection = connectionFactory();

            await connection.ExecuteAsync("DELETE FROM Coupon WHERE ProductId = @ProductId", new { ProductId = productId });
        }

        public async Task UpdateDiscount(Coupon coupon)
        {
            using var connection = connectionFactory();

            await connection.ExecuteAsync("UPDATE Coupon SET ProductName=@ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id", coupon);
        }
    }
}
