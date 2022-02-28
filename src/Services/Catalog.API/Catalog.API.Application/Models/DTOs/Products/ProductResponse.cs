using System.Collections.Generic;
using Catalog.API.Core.Entities;

namespace Catalog.API.Application.Models.DTOs.Products
{
    public class ProductResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public double Price { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsPopularProduct { get; set; }
        public string Category { get; set; }
        public ICollection<ProductPhoto> ProductPhotos { get; set; }
        public int Rating { get; set; }
    }
}
