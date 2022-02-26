using System;
using Catalog.API.Helpers.Pagination;
using Microsoft.AspNetCore.WebUtilities;

namespace Catalog.API.Helpers.UriService
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetAllProductsUri(PaginationQuery pagination = null)
        {
            var uri = new Uri(_baseUri);
            if (pagination == null)
            {
                return uri;
            }

            var modifiedUri = QueryHelpers.AddQueryString(_baseUri, "pageNumber", pagination.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", pagination.PageSize.ToString());

            return new Uri(modifiedUri);
        }
    }
}
