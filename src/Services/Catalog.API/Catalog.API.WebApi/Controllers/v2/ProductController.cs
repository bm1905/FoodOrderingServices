using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Catalog.API.Application.Exceptions;
using Catalog.API.Application.Models.DTOs;
using Catalog.API.Application.Models.Pagination;
using Catalog.API.Application.Services;
using Catalog.API.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.WebApi.Controllers.v2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ValidateModelFilter]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productServices;

        public ProductController(IProductServices productServices)
        {
            _productServices = productServices ?? throw new ArgumentNullException(nameof(productServices));
        }

        [MapToApiVersion("2.0")]
        [HttpGet]
        [CacheAttributeFilter(600)]
        [ProducesResponseType(typeof(IEnumerable<ProductResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundException), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<PagedResponse<ProductResponse>>> GetProducts([FromQuery] PaginationQuery paginationQuery)
        {
            PagedResponse<ProductResponse> products = await _productServices.ProcessGetProductsAsync(paginationQuery);

            return Ok(products);
        }
    }
}