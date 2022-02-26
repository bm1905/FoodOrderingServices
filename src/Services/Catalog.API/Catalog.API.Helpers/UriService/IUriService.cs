using System;
using Catalog.API.Helpers.Pagination;

namespace Catalog.API.Helpers.UriService
{
    public interface IUriService
    {
        Uri GetProductUri(string postId);
        Uri GetAllProductsUri(PaginationQuery pagination = null);
    }
}
