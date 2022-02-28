using System;
using Catalog.API.Application.Models.Pagination;

namespace Catalog.API.Application.Services.UriService
{
    public interface IUriService
    {
        Uri GetAllProductsUri(PaginationQuery pagination = null);
    }
}
