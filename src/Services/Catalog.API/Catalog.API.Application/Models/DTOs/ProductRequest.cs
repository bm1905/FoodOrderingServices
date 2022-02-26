using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Application.Models.DTOs
{
    public abstract class ProductRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Detail { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public bool IsAvailable { get; set; }
        [Required]
        public bool IsPopularProduct { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public int Rating { get; set; }
    }
}
