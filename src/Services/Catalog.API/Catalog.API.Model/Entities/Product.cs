using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.API.Model.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public double Price { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsPopularProduct { get; set; }
        public string Category { get; set; }
        public ICollection<ProductPhoto> ProductPhotos { get; set; }
        [Range(1, 5,
            ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Rating { get; set; }
    }
}
