using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Model.Models
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
        [Range(1, 5,
            ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Rating { get; set; }
    }
}
