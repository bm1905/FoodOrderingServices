using System;
using Catalog.API.Core.Common;
using MongoDB.Bson;

namespace Catalog.API.Core.Entities
{
    public class ProductPhoto : BaseEntity, IAuditedEntity
    {
        public ProductPhoto()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
    }
}
