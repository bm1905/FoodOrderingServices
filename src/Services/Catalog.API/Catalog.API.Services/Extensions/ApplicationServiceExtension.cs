using Catalog.API.BLL;
using Catalog.API.DAL.Context;
using Catalog.API.DAL.Repository;
using Catalog.API.Helpers.AutoMapper;
using Catalog.API.Helpers.Filters;
using Catalog.API.Helpers.PhotoService;
using Catalog.API.Helpers.Settings;
using Catalog.API.Services.SwaggerOptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Catalog.API.Services.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            services.Configure<MongoSettings>(config.GetSection("DatabaseSettings"));

            services.Configure<MongoSettings>(options =>
            {
                options.Connection = config.GetSection("DatabaseSettings:Connection").Value;
                options.DatabaseName = config.GetSection("DatabaseSettings:DatabaseName").Value;
                options.CollectionName = config.GetSection("DatabaseSettings:CollectionName").Value;
            });

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            services.AddScoped<IProductContext, ProductContext>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductServiceBll, ProductServiceBll>();
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            services.AddScoped<ValidateModelFilter>();

            return services;
        }
    }
}
