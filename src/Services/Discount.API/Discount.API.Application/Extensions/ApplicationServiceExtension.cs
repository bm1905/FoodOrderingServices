using System.Reflection;
using Discount.API.Application.MappingProfiles;
using Discount.API.Application.Services.DiscountService;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        }

        private static void RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
        }
    }
}
