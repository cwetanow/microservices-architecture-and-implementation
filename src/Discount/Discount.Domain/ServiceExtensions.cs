using Discount.Domain.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System.Data;

namespace Discount.Domain
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddDiscounts(this IServiceCollection services, string connectionString) => services
            .AddScoped<IDiscountData, PostgreDiscountData>()
            .AddScoped<Func<IDbConnection>>(provider => () => new NpgsqlConnection(connectionString));
    }
}
