﻿using System.Collections.Generic;
using Catalog.API.WebApi.Extensions.SwaggerOptions;
using Common.ServiceDiscovery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Catalog.API.WebApi.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddSwaggerVersions();
            services.AddSecurity(config);
            services.AddServiceDiscovery(config);
            services.AddHealthChecks(config);
            return services;
        }

        // Health Check
        private static void AddHealthChecks(this IServiceCollection services, IConfiguration config)
        {
            services.AddHealthChecks()
                .AddMongoDb(config.GetSection("DatabaseSettings:ConnectionString").Value,
                    "Catalog API Mongo Database Health",
                    HealthStatus.Degraded)
                .AddRedis(config.GetSection("RedisCacheSettings:ConnectionString").Value,
                    "Catalog API Redis Health",
                    HealthStatus.Degraded);
        }

        // Security
        private static void AddSecurity(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = config.GetSection("IdentityServerSettings:IdentityServerUrl").Value;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                    options.RequireHttpsMetadata = false;
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "Catalog.API");
                });
            });
        }
        
        // Service Discovery
        private static void AddServiceDiscovery(this IServiceCollection services, IConfiguration config)
        {
            var serviceConfig = config.GetServiceConfig();
            services.RegisterConsulServices(serviceConfig);
        }

        // Swagger
        private static void AddSwaggerVersions(this IServiceCollection services)
        {
            // Swagger extensions
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            services.AddApiVersioning(options =>
            {
                // Specify the default API Version as 1.0
                options.DefaultApiVersion = new ApiVersion(1, 0);
                // If the client hasn't specified the API version in the request, use the default API version number 
                options.AssumeDefaultVersionWhenUnspecified = true;
                // Advertise the API versions supported for the particular endpoint
                options.ReportApiVersions = true;
                // HTTP Header based versions or query based
                // c.ApiVersionReader = ApiVersionReader.Combine(new HeaderApiVersionReader("x-api-version"),
                // new QueryStringApiVersionReader("api-version"));
            });

            services.AddVersionedApiExplorer(options =>
            {
                // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                // note: the specified format code will format the version as "'v'major[.minor][-status]"
                options.GroupNameFormat = "'v'VVV";

                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                // can also be used to control the format of the API version in route templates
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGenNewtonsoftSupport();

            services.AddSwaggerGen(options =>
            {
                options.EnableAnnotations();
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"Enter 'Bearer' [space] and your token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                
                options.AddSecurityRequirement(new OpenApiSecurityRequirement 
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });
        }
    }
}
