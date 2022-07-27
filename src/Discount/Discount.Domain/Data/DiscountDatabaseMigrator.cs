using Microsoft.Extensions.Logging;
using Npgsql;
using Polly;
using System.Data;

namespace Discount.Domain.Data
{
    public class DiscountDatabaseMigrator
    {
        private readonly Func<IDbConnection> connectionFactory;
        private readonly ILogger<DiscountDatabaseMigrator> logger;

        public DiscountDatabaseMigrator(Func<IDbConnection> connectionFactory, ILogger<DiscountDatabaseMigrator> logger)
        {
            this.connectionFactory = connectionFactory;
            this.logger = logger;
        }

        public async Task MigrateAsync()
        {
            try
            {
                //if the postgresql server container is not created on run docker compose this
                //migration can't fail for network related exception. The retry options for database operations
                //apply to transient exceptions                    
                await Policy.Handle<NpgsqlException>()
                         .WaitAndRetryAsync(
                             retryCount: 5,
                             sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
                         .ExecuteAsync(async () => await ExecuteMigrations());

                logger.LogInformation("Migrated postresql database.");
            }
            catch (NpgsqlException ex)
            {
                logger.LogError(ex, "An error occurred while migrating the postresql database");
            }
        }

        private async Task ExecuteMigrations()
        {
            using var connection = connectionFactory();
            connection.Open();

            using var command = new NpgsqlCommand
            {
                Connection = connection as NpgsqlConnection
            };

            command.CommandText = "DROP TABLE IF EXISTS Coupon";
            await command.ExecuteNonQueryAsync();

            command.CommandText = @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY, 
                                                                ProductId VARCHAR(24) NOT NULL,
                                                                Description TEXT,
                                                                Amount INT)";
            await command.ExecuteNonQueryAsync();

            command.CommandText = "INSERT INTO Coupon(ProductId, Description, Amount) VALUES('12345', 'IPhone Discount', 150);";
            await command.ExecuteNonQueryAsync();

            command.CommandText = "INSERT INTO Coupon(ProductId, Description, Amount) VALUES('12346', 'Samsung Discount', 100);";
            await command.ExecuteNonQueryAsync();
        }
    }
}
