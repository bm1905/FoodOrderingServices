using Catalog.API.Core.Entities;
using MongoDB.Driver;

namespace Catalog.API.DataAccess.Persistence
{
    public interface IProductContext
    {
        IMongoCollection<Product> Products { get; }
    }
}
