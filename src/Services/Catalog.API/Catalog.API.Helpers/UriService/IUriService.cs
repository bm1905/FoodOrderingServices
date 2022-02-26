using System;
using Catalog.API.Helpers.Pagination;

namespace Catalog.API.Helpers.UriService
{
    public interface IUriService
    {
        Uri GetAllProductsUri(PaginationQuery pagination = null);
    }
}
