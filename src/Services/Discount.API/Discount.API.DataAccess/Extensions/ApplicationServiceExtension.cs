using Discount.API.DataAccess.Persistence;
using Discount.API.DataAccess.Persistence.Configurations;
using Discount.API.DataAccess.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Discount.API.DataAccess.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddRepositories();
            services.AddDatabase(config);

            return services;
        }

        // Repositories
        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IDiscountRepository, DiscountRepository>();
        }

        // Postgres Database
        private static void AddDatabase(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<NpgsqlSettings>(options =>
            {
                options.ConnectionString = config.GetSection("DatabaseSettings:ConnectionString").Value;
            });

            services.AddScoped<IDiscountContext, DiscountContext>();
        }
    }
}
