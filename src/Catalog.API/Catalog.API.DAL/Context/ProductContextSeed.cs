﻿using System.Collections.Generic;
using Catalog.API.Model.Entities;
using MongoDB.Driver;

namespace Catalog.API.DAL.Context
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
                }
            };
        }
    }
}
