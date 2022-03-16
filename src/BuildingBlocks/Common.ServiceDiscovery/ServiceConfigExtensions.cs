using Microsoft.Extensions.Configuration;

namespace Common.ServiceDiscovery
{
    public static class ServiceConfigExtensions
    {
        public static ServiceConfig GetServiceConfig(this IConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var serviceConfig = new ServiceConfig
            {
                ServiceDiscoveryAddress = configuration.GetValue<Uri>("ServiceConfig:ServiceDiscoveryAddress"),
                ServiceAddress = configuration.GetValue<Uri>("ServiceConfig:ServiceAddress"),
                ServiceName = configuration.GetValue<string>("ServiceConfig:ServiceName"),
                ServiceId = configuration.GetValue<string>("ServiceConfig:ServiceId")
            };

            return serviceConfig;
        }
    }
}
