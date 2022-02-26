﻿using System.Collections.Generic;
using System.Linq;
using Catalog.API.Helpers.UriService;
using Catalog.API.Model.DTOs;

namespace Catalog.API.Helpers.Pagination
{
    public class PaginationHelper
    {
        public static PagedResponse<T> CreatePaginatedResponse<T>(IUriService uriService, PaginationFilter pagination, IList<T> response)
        {
            var nextPage = pagination.PageNumber >= 1
                ? uriService.GetAllProductsUri(new PaginationQuery(pagination.PageNumber + 1, pagination.PageSize)).ToString()
                : null;

            var previousPage = pagination.PageNumber - 1 >= 1
                ? uriService.GetAllProductsUri(new PaginationQuery(pagination.PageNumber - 1, pagination.PageSize)).ToString()
                : null;

            var pagedResponse = new PagedResponse<T>
            {
                Data = response,
                PageNumber = pagination.PageNumber >= 1 ? pagination.PageNumber : (int?)null,
                PageSize = pagination.PageSize >= 1 ? pagination.PageSize : (int?)null,
                NextPage = response.Any() ? nextPage : null,
                PreviousPage = previousPage
            };

            return pagedResponse;
        }
    }
}
