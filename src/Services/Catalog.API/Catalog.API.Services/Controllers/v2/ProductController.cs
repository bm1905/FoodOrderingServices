using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Catalog.API.BLL;
using Catalog.API.Helpers.Filters;
using Catalog.API.Model.DTOs;
using Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Services.Controllers.v2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ServiceFilter(typeof(ValidateModelFilter))]
    public class ProductController : ControllerBase
    {
        private readonly IProductServiceBll _productServiceBll;

        public ProductController(IProductServiceBll productServiceBll)
        {
            _productServiceBll = productServiceBll ?? throw new ArgumentNullException(nameof(productServiceBll));
        }

        [MapToApiVersion("2.0")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundException), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProducts()
        {
            IEnumerable<ProductResponse> products = await _productServiceBll.ProcessGetProductsAsync();
            return Ok(products);
        }
    }
}
