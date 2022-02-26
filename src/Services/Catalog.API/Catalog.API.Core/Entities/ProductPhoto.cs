using System;
using Catalog.API.Core.Common;

namespace Catalog.API.Core.Entities
{
    public class ProductPhoto : BaseEntity, IAuditedEntity
    {
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
