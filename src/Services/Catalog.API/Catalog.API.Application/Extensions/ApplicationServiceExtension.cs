using System.Reflection;
using Catalog.API.Application.Configurations;
using Catalog.API.Application.MappingProfiles;
using Catalog.API.Application.Services.CacheService;
using Catalog.API.Application.Services.PhotoService;
using Catalog.API.Application.Services.ProductService;
using Catalog.API.Application.Services.UriService;
using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Catalog.API.Application.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config, IWebHostEnvironment env)
        {
            services.AddServices();
            services.AddRedis(config);
            services.AddCloudinary(config, env);
            services.RegisterAutoMapper();

            return services;
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IProductServices, ProductServices>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IUriService, UriService>(provider =>
            {
                var accessor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accessor?.HttpContext?.Request;

                // For versions in base url api/v1/....
                return new UriService(string.Concat(request?.Scheme, "://", request?.Host.ToUriComponent(), request?.Path));
            });
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        }

        private static void AddRedis(this IServiceCollection services, IConfiguration config)
        {
            var redisCacheSettings = new RedisCacheSettings();
            config.GetSection(nameof(RedisCacheSettings)).Bind(redisCacheSettings);
            services.AddSingleton(redisCacheSettings);
            if (!redisCacheSettings.Enabled) return;

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisCacheSettings.ConnectionString;
                
            });
            services.AddSingleton<IResponseCacheService, ResponseCacheService>();
        }

        private static void AddCloudinary(this IServiceCollection services, IConfiguration config, IWebHostEnvironment env)
        {
            services.Configure<CloudinarySettings>(options =>
            {
                options.Environment = env.EnvironmentName;
                options.CloudName = config.GetSection("CloudinarySettings:CloudName").Value;
                options.ApiKey = config.GetSection("CloudinarySettings:ApiKey").Value;
                options.ApiSecret = config.GetSection("CloudinarySettings:ApiSecret").Value;

            });
        }

        private static void RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
        }
    }
}
