using Catalog.API.Application.CacheService;
using Catalog.API.Application.Configurations;
using Catalog.API.Application.MappingProfiles;
using Catalog.API.Application.Services;
using Catalog.API.Application.UriService;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Catalog.API.Application.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddServices();
            services.AddRedis(config);
            services.RegisterAutoMapper();

            return services;
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IProductServices, ProductServices>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IUriService, UriService.UriService>(provider =>
            {
                var accessor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accessor?.HttpContext?.Request;

                // For versions in base url api/v1/....
                return new UriService.UriService(string.Concat(request?.Scheme, "://", request?.Host.ToUriComponent(), request?.Path));
            });
        }
        
        private static void AddRedis(this IServiceCollection services, IConfiguration config)
        {
            var redisCacheSettings = new RedisCacheSettings();
            config.GetSection(nameof(RedisCacheSettings)).Bind(redisCacheSettings);
            services.AddSingleton(redisCacheSettings);
            if (redisCacheSettings.Enabled)
            {
                services.AddStackExchangeRedisCache(options => options.Configuration = redisCacheSettings.ConnectionString);
                services.AddSingleton<IResponseCacheService, ResponseCacheService>();
            }
        }

        private static void RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
        }
    }
}
