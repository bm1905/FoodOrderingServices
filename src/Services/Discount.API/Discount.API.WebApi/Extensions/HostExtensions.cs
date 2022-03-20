using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Discount.API.WebApi.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, int? retry = 0)
        {
            retry ??= 0;
            var retryForAvailability = retry.Value;

            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var configuration = services.GetRequiredService<IConfiguration>();
            var logger = services.GetRequiredService<ILogger<TContext>>();

            try
            {
                logger.LogInformation("Migrating postgres database.");

                using var connection = new NpgsqlConnection
                    (configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
                connection.Open();

                using var command = new NpgsqlCommand
                {
                    Connection = connection
                };

                try
                {
                    logger.LogInformation("Checking if table exists!");
                    command.CommandText = @"SELECT COUNT(*) FROM DiscountCoupon;";
                    var result = command.ExecuteScalar();
                    if (result != null && (long)result > 0) return host;
                }
                catch (Exception)
                {
                    logger.LogInformation("Table or database does not exist! Attempting to re-create table!");
                    // Do nothing
                }

                command.CommandText = "DROP TABLE IF EXISTS DiscountCoupon";
                command.ExecuteNonQuery();

                command.CommandText = @"CREATE TABLE DiscountCoupon(Id SERIAL PRIMARY KEY, 
                                                                CouponCode VARCHAR(5) NOT NULL,
                                                                ProductName VARCHAR(24) NOT NULL,
                                                                Description TEXT NOT NULL,
                                                                Amount INT NOT NULL,
                                                                ExpiresOn DATE, 
                                                                CreatedBy TEXT, 
                                                                CreatedOn DATE,
                                                                UpdatedBy TEXT,
                                                                UpdatedOn DATE)";

                command.ExecuteNonQuery();

                command.CommandText = @"INSERT INTO DiscountCoupon(CouponCode, ProductName, Description, Amount, ExpiresOn, CreatedBy, CreatedOn) 
                                                        VALUES('DIS20', 'Test A', 'Discount for Test A', 15, '2022-05-05', 'system', '2022-02-02');";
                command.ExecuteNonQuery();

                command.CommandText = @"INSERT INTO DiscountCoupon(CouponCode, ProductName, Description, Amount, ExpiresOn, CreatedBy, CreatedOn) 
                                                        VALUES('DIS21', 'Test B', 'Discount for Test B', 45, '2022-05-05', 'system', '2022-02-02');";
                command.ExecuteNonQuery();

                logger.LogInformation("Migrated postgres database.");
            }
            catch (NpgsqlException ex)
            {
                logger.LogError(ex, $"An error occurred while migrating the postgres database. Retrying for {retryForAvailability} times.");

                if (retryForAvailability < 50)
                {
                    retryForAvailability++;
                    System.Threading.Thread.Sleep(2000);
                    MigrateDatabase<TContext>(host, retryForAvailability);
                }
            }
            return host;
        }
    }
}
