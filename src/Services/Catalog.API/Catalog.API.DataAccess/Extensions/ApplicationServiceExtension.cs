using Catalog.API.DataAccess.Persistence;
using Catalog.API.DataAccess.Persistence.Configurations;
using Catalog.API.DataAccess.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.API.DataAccess.Extensions
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
            services.AddScoped<IProductRepository, ProductRepository>();
        }

        // Mongo Database
        private static void AddDatabase(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<MongoSettings>(options =>
            {
                options.ConnectionString = config.GetSection("DatabaseSettings:ConnectionString").Value;
                options.DatabaseName = config.GetSection("DatabaseSettings:DatabaseName").Value;
                options.CollectionName = config.GetSection("DatabaseSettings:CollectionName").Value;
            });

            services.AddScoped<IProductContext, ProductContext>();
        }
    }
}
