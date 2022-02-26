using System;
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
        public ICollection<ProductPhoto> ProductPhotos { get; set; }
        public int Rating { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
