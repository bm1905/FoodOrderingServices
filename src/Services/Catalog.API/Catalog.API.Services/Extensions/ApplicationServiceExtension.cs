using Catalog.API.BLL;
using Catalog.API.DAL.Context;
using Catalog.API.DAL.Repository;
using Catalog.API.Helpers.AutoMapper;
using Catalog.API.Helpers.CacheService;
using Catalog.API.Helpers.Filters;
using Catalog.API.Helpers.PhotoService;
using Catalog.API.Helpers.Settings;
using Catalog.API.Helpers.UriService;
using Catalog.API.Services.SwaggerOptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Catalog.API.Services.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            // Cloudinary --> For photos
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));

            // Mongo --> Main database
            services.Configure<MongoSettings>(config.GetSection("DatabaseSettings"));
            services.Configure<MongoSettings>(options =>
            {
                options.ConnectionString = config.GetSection("DatabaseSettings:ConnectionString").Value;
                options.DatabaseName = config.GetSection("DatabaseSettings:DatabaseName").Value;
                options.CollectionName = config.GetSection("DatabaseSettings:CollectionName").Value;
            });

            // Redis --> Caching
            var redisCacheSettings = new RedisCacheSettings();
            config.GetSection(nameof(RedisCacheSettings)).Bind(redisCacheSettings);
            services.AddSingleton(redisCacheSettings);
            if (redisCacheSettings.Enabled)
            {
                services.AddStackExchangeRedisCache(options => options.Configuration = redisCacheSettings.ConnectionString);
                services.AddSingleton<IResponseCacheService, ResponseCacheService>();
            }

            // Swagger extensions
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            // Auto Mapper
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            // Validator
            services.AddScoped<ValidateModelFilter>();

            // DB Context
            services.AddScoped<IProductContext, ProductContext>();

            // Services
            services.AddScoped<IPhotoService, PhotoService>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IUriService, UriService>(provider =>
            {
                var accessor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accessor?.HttpContext?.Request;
                var absoluteUri = string.Concat(request?.Scheme, "://", request?.Host.ToUriComponent(), "/");
                return new UriService(absoluteUri);
            });

            // DAL
            services.AddScoped<IProductRepository, ProductRepository>();

            // BLL
            services.AddScoped<IProductServiceBll, ProductServiceBll>();

            return services;
        }
    }
}
