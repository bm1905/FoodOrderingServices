using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Catalog.API.Helpers.Exceptions;
using Catalog.API.Model.Models;
using Catalog.API.BLL;

namespace Catalog.API.Services.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductServiceBll _productServiceBll;

        public ProductController(IProductServiceBll productServiceBll)
        {
            _productServiceBll = productServiceBll ?? throw new ArgumentNullException(nameof(productServiceBll));
        }

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
