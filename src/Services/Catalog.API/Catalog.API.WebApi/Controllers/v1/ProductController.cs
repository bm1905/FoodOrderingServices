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

namespace Catalog.API.WebApi.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ValidateModelFilter]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productServices;

        public ProductController(IProductServices productServices)
        {
            _productServices = productServices ?? throw new ArgumentNullException(nameof(productServices));
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        [CacheAttributeFilter(600)]
        [ProducesResponseType(typeof(IEnumerable<ProductResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundException), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(InternalServerErrorException), (int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<PagedResponse<ProductResponse>>> GetProducts([FromQuery]PaginationQuery paginationQuery)
        {
            PagedResponse<ProductResponse> products = await _productServices.ProcessGetProductsAsync(paginationQuery);

            return Ok(products);
        }

        [MapToApiVersion("1.0")]
        //[HttpGet("{productId:length(24)}")]
        [HttpGet("{productId:length(24)}", Name = "GetProductById")]
        [CacheAttributeFilter(600)]
        [ProducesResponseType(typeof(IEnumerable<ProductResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundException), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(InternalServerErrorException), (int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProductById(string productId)
        {
            ProductResponse product = await _productServices.ProcessGetProductByIdAsync(productId);

            return Ok(product);
        }

        [MapToApiVersion("1.0")]
        [HttpGet("[action]/{category}")]
        [CacheAttributeFilter(600)]
        [ProducesResponseType(typeof(IEnumerable<ProductResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundException), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(InternalServerErrorException), (int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<PagedResponse<ProductResponse>>> GetProductsByCategory([FromQuery] PaginationQuery paginationQuery, string category)
        {
            PagedResponse<ProductResponse> products = await _productServices.ProcessGetProductsByCategoryAsync(paginationQuery, category);

            return Ok(products);
        }

        [MapToApiVersion("1.0")]
        [HttpGet("[action]")]
        [ProducesResponseType(typeof(IEnumerable<ProductResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundException), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(InternalServerErrorException), (int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<PagedResponse<ProductResponse>>> GetPopularProducts([FromQuery] PaginationQuery paginationQuery)
        {
            PagedResponse<ProductResponse> popularProducts = await _productServices.ProcessGetPopularProductsAsync(paginationQuery);

            return Ok(popularProducts);
        }

        [MapToApiVersion("1.0")]
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<ProductResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AlreadyExistsException), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(UnauthorizedException), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(InternalServerErrorException), (int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<ProductResponse>> CreateProducts([FromBody] CreateProductRequest product)
        {
            ProductResponse createdProduct = await _productServices.ProcessCreateProductAsync(product);

            return CreatedAtRoute("GetProductById", new { productId = createdProduct.Id }, createdProduct);
        }
    }
}
