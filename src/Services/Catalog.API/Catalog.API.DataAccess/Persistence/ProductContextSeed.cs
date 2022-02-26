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
                    Id = "602d2149e773f2a3990b47f5",
                    Name = "Chicken Stew",
                    Detail = "This is the description for Chicken Stew.",
                    Price = 150.00,
                    IsAvailable = true,
                    IsPopularProduct = true,
                    Category = "Meat",
                    Rating = 5
                },
                new()
                {
                    Id = "602d2149e773f2a3990b47f6",
                    Name = "Fried Okra",
                    Detail = "This is the description for Fried Okra.",
                    Price = 90.00,
                    IsAvailable = true,
                    IsPopularProduct = false,
                    Category = "Vegetable",
                    Rating = 3
                },
                new()
                {
                    Id = "602d2149e773f2a3990b47f7",
                    Name = "Fried Chicken",
                    Detail = "This is the description for Fried Chicken.",
                    Price = 190.00,
                    IsAvailable = true,
                    IsPopularProduct = false,
                    Category = "Meat",
                    Rating = 3
                },
                new()
                {
                    Id = "602d2149e773f2a3990b47f8",
                    Name = "Vegetable Stew",
                    Detail = "This is the description for Vegetable Stew.",
                    Price = 40.00,
                    IsAvailable = true,
                    IsPopularProduct = false,
                    Category = "Vegetable",
                    Rating = 3
                },
                new()
                {
                    Id = "602d2149e773f2a3990b47f9",
                    Name = "Chicken Stew 1",
                    Detail = "This is the description for Chicken Stew.",
                    Price = 150.00,
                    IsAvailable = true,
                    IsPopularProduct = true,
                    Category = "Meat",
                    Rating = 5
                },
                new()
                {
                    Id = "602d2149e773f2a3990b47f0",
                    Name = "Fried Okra 1",
                    Detail = "This is the description for Fried Okra.",
                    Price = 90.00,
                    IsAvailable = true,
                    IsPopularProduct = false,
                    Category = "Vegetable",
                    Rating = 3
                },
                new()
                {
                    Id = "602d2149e773f2a3990b47f1",
                    Name = "Fried Chicken 12",
                    Detail = "This is the description for Fried Chicken.",
                    Price = 190.00,
                    IsAvailable = true,
                    IsPopularProduct = false,
                    Category = "Meat",
                    Rating = 3
                },
                new()
                {
                    Id = "602d2149e773f2a3990b47f2",
                    Name = "Vegetable Stew 2",
                    Detail = "This is the description for Vegetable Stew.",
                    Price = 40.00,
                    IsAvailable = true,
                    IsPopularProduct = false,
                    Category = "Vegetable",
                    Rating = 3
                }
            };
        }
    }
}
