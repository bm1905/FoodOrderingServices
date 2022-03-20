using System.Collections.Generic;
using Catalog.API.Core.Common;

namespace Catalog.API.Core.Entities
{
    public class Product : BaseEntity, IAuditedEntity
    {
        public string Name { get; set; }
        public string Detail { get; set; }
        public double Price { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsPopularProduct { get; set; }
        public string Category { get; set; }
        public IList<ProductPhoto> ProductPhotos = new List<ProductPhoto>();
        public int Rating { get; set; }
    }
}
