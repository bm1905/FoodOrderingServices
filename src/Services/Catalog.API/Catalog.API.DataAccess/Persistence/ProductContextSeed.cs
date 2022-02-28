using System;
using System.Collections.Generic;
using Catalog.API.Core.Entities;
using MongoDB.Driver;

namespace Catalog.API.DataAccess.Persistence
{
    public static class ProductContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();
            if (!existProduct)
            {
                productCollection.InsertManyAsync(GetPreconfiguredProducts());
            }
        }

        private static IEnumerable<Product> GetPreconfiguredProducts()
        {
            return new List<Product>()
            {
                new()
                {
                    Id = "621c2d222b0f522a70755f17",
                    Name = "Chicken Stew",
                    Detail = "This is the description for Chicken Stew.",
                    Price = 150,
                    IsAvailable = true,
                    IsPopularProduct = true,
                    Category = "Meat",
                    Rating = 5,
                    ProductPhotos = new List<ProductPhoto>()
                    {
                        new()
                        {
                            Url = "https://res.cloudinary.com/bm1905/image/upload/v1646013730/food-service/Development/Meat/itlmsbfqrcrhmttzwgma.jpg",
                            IsMain = true,
                            PublicId = "food-service/Development/Meat/itlmsbfqrcrhmttzwgma",
                            CreatedBy = "System",
                            CreatedOn = DateTime.UtcNow,
                            UpdatedBy = "System",
                            UpdatedOn = DateTime.UtcNow,
                            Id = "621c2d222b0f522a70755f16"
                        }
                    }
                },
                new()
                {
                    Id = "621c2f372b0f522a70755f19",
                    Name = "Fried Okra",
                    Detail = "This is the description for Fried Okra.",
                    Price = 90,
                    IsAvailable = true,
                    IsPopularProduct = true,
                    Category = "Vegetable",
                    Rating = 3,
                    ProductPhotos = new List<ProductPhoto>()
                    {
                        new()
                        {
                            Url = "https://res.cloudinary.com/bm1905/image/upload/v1646014263/food-service/Development/Vegetable/fz8nkv3vwm0gesybv8us.jpg",
                            IsMain = true,
                            PublicId = "food-service/Development/Vegetable/fz8nkv3vwm0gesybv8us",
                            CreatedBy = "System",
                            CreatedOn = DateTime.UtcNow,
                            UpdatedBy = "System",
                            UpdatedOn = DateTime.UtcNow,
                            Id = "621c2f372b0f522a70755f18"
                        }
                    }
                },
            };
        }
    }
}
