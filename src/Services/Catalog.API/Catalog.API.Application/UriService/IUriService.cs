using System;
using Catalog.API.Application.Models.Pagination;

namespace Catalog.API.Application.UriService
{
    public interface IUriService
    {
        Uri GetAllProductsUri(PaginationQuery pagination = null);
    }
}
