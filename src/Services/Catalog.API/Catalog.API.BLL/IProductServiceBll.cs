using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.API.Model.DTOs;

namespace Catalog.API.BLL
{
    public interface IProductServiceBll
    {
        Task<IEnumerable<ProductResponse>> ProcessGetProductsAsync();
    }
}
