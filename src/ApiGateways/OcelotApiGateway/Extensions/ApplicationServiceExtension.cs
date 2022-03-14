using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Provider.Consul;

namespace OcelotApiGateway.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddOcelotServices(this IServiceCollection services, IConfiguration config)
        {
            services.ConfigureOcelot();
            services.ConfigureAuthentication(config);
            return services;
        }

        private static void ConfigureAuthentication(this IServiceCollection services, IConfiguration config)
        {
            var authenticationProviderKey = config["IdentityServer:IdentityApiKey"];
            services.AddAuthentication()
                .AddJwtBearer(authenticationProviderKey, options =>
                {
                    options.Authority = config["IdentityServer:BaseUrl"];
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });
        }

        private static void ConfigureOcelot(this IServiceCollection services)
        {
            services.AddOcelot()
                .AddCacheManager(settings => { settings.WithDictionaryHandle(); })
                .AddConsul();
        }
    }
}
