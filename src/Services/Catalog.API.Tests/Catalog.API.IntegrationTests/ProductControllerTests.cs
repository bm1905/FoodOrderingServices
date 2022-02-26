using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Catalog.API.Model.DTOs;
using FluentAssertions;
using Xunit;

namespace Catalog.API.IntegrationTests
{
    public class ProductControllerTests : IntegrationTest
    {
        [Fact]
        public async Task GetProducts_WithProducts_ReturnsProductResponse()
        {
            // Arrange


            // Act
            var response = await TestClient.GetAsync($"api/{Version}/Product");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var products = (await response.Content.ReadAsAsync<IEnumerable<ProductResponse>>()).ToList();
            if (products.Any())
            {
                products.Should().NotBeEmpty();
            }
            else
            {
                products.Should().BeEmpty();
            }
        }
    }
}
