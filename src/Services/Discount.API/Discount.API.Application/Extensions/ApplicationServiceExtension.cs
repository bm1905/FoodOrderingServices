using System.Reflection;
using System.Security.Claims;
using Discount.API.Application.MappingProfiles;
using Discount.API.Application.Services.AccountService;
using Discount.API.Application.Services.DiscountService;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Http;

namespace Discount.API.Application.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddServices();
            services.RegisterAutoMapper();
            return services;
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IDiscountService, DiscountService>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<IAccountService, AccountService>(provider =>
            {
                var accessor = provider.GetRequiredService<IHttpContextAccessor>();
                ClaimsPrincipal claims = accessor?.HttpContext?.User;

                return new AccountService(claims);
            });

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private static void RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
        }
    }
}
