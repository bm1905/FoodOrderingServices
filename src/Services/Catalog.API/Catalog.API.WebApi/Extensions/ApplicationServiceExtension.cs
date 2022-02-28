using Catalog.API.WebApi.Extensions.SwaggerOptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Catalog.API.WebApi.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddSwaggerVersions();
            services.AddHealthChecks(config);
            return services;
        }

        public static void AddHealthChecks(this IServiceCollection services, IConfiguration config)
        {
            services.AddHealthChecks()
                .AddMongoDb(config.GetSection("DatabaseSettings:ConnectionString").Value,
                    "Catalog API Mongo Database Health",
                    HealthStatus.Degraded)
                .AddRedis(config.GetSection("RedisCacheSettings:ConnectionString").Value,
                    "Catalog API Redis Health",
                    HealthStatus.Degraded);
        }

        public static void AddSwaggerVersions(this IServiceCollection services)
        {
            // Swagger extensions
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            services.AddApiVersioning(c =>
            {
                // Specify the default API Version as 1.0
                c.DefaultApiVersion = new ApiVersion(1, 0);
                // If the client hasn't specified the API version in the request, use the default API version number 
                c.AssumeDefaultVersionWhenUnspecified = true;
                // Advertise the API versions supported for the particular endpoint
                c.ReportApiVersions = true;
                // HTTP Header based versions or query based
                //c.ApiVersionReader = ApiVersionReader.Combine(new HeaderApiVersionReader("x-api-version"), new QueryStringApiVersionReader("api-version"));
            });

            services.AddVersionedApiExplorer(o =>
            {
                // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                // note: the specified format code will format the version as "'v'major[.minor][-status]"
                o.GroupNameFormat = "'v'VVV";

                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                // can also be used to control the format of the API version in route templates
                o.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGenNewtonsoftSupport();

            services.AddSwaggerGen(_ =>
            {
                // options
            });
        }
    }
}
