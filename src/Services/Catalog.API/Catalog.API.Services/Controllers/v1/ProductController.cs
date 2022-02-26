using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Catalog.API.BLL;
using Catalog.API.Helpers.Filters;
using Catalog.API.Helpers.Pagination;
using Catalog.API.Model.DTOs;
using Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Services.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ValidateModelFilter]
    public class ProductController : ControllerBase
    {
        private readonly IProductServiceBll _productServiceBll;

        public ProductController(IProductServiceBll productServiceBll)
        {
            _productServiceBll = productServiceBll ?? throw new ArgumentNullException(nameof(productServiceBll));
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        [CacheAttributeFilter(600)]
        [ProducesResponseType(typeof(IEnumerable<ProductResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundException), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<PagedResponse<ProductResponse>>> GetProducts([FromQuery]PaginationQuery paginationQuery)
        {
            PagedResponse<ProductResponse> products = await _productServiceBll.ProcessGetProductsAsync(paginationQuery);

            return Ok(products);
        }

        [MapToApiVersion("1.0")]
        [HttpGet("{productId:length(24)}")]
        [CacheAttributeFilter(600)]
        [ProducesResponseType(typeof(IEnumerable<ProductResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundException), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProductById(string productId)
        {
            ProductResponse product = await _productServiceBll.ProcessGetProductByIdAsync(productId);

            return Ok(product);
        }

        [MapToApiVersion("1.0")]
        [HttpGet("[action]/{category}")]
        [CacheAttributeFilter(600)]
        [ProducesResponseType(typeof(IEnumerable<ProductResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundException), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<PagedResponse<ProductResponse>>> GetProductsByCategory([FromQuery] PaginationQuery paginationQuery, string category)
        {
            PagedResponse<ProductResponse> products = await _productServiceBll.ProcessGetProductsByCategoryAsync(paginationQuery, category);

            return Ok(products);
        }
    }
}
