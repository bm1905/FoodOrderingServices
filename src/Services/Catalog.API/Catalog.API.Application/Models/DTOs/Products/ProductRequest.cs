namespace Catalog.API.Application.Models.DTOs.Products
{
    public abstract class ProductRequest
    {
        public string Name { get; set; }
        public string Detail { get; set; }
        public double Price { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsPopularProduct { get; set; }
        public string Category { get; set; }
        public int Rating { get; set; }
    }
}
