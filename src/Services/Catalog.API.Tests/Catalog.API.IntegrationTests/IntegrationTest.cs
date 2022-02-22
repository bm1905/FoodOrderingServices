using System.Net.Http;
using Catalog.API.Services;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.API.IntegrationTests
{
    public class IntegrationTest
    {
        protected readonly HttpClient TestClient;
        protected readonly string Version;

        public IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            //Version = appFactory.Services.GetRequiredService<IApiVersionDescriptionProvider>().ApiVersionDescriptions.Single().GroupName;

            Version = appFactory.Services.GetRequiredService<IApiVersionDescriptionProvider>().ApiVersionDescriptions[0].GroupName;

            TestClient = appFactory.CreateClient();
        }

    }
}
