using Catalog.API.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.API.Model.Entities;
using AutoMapper;
using Catalog.API.Helpers.Pagination;
using Catalog.API.Helpers.UriService;
using Catalog.API.Model.DTOs;
using Common.Exceptions;

namespace Catalog.API.BLL
{
    public class ProductServiceBll : IProductServiceBll
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public ProductServiceBll(IProductRepository productRepository, IMapper mapper, IUriService uriService)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _uriService = uriService ?? throw new ArgumentNullException(nameof(uriService));
        }

        public async Task<PagedResponse<ProductResponse>> ProcessGetProductsAsync(PaginationQuery paginationQuery)
        {
            IEnumerable<Product> products;
            PaginationFilter pagination = _mapper.Map<PaginationFilter>(paginationQuery);

            if (pagination != null)
            {
                var skip = (pagination.PageNumber - 1) * pagination.PageSize;
                var take = pagination.PageSize;

                products = await _productRepository.GetPaginatedProducts(skip, take);
            }
            else
            {
                products = await _productRepository.GetAllProducts();
            }

            if (products == null || !products.Any()) throw new NotFoundException("Product not found!");

            IList<ProductResponse> productResponses = _mapper.Map<IList<ProductResponse>>(products);

            if (pagination == null || pagination.PageNumber < 1 || pagination.PageSize < 1)
            {
                return new PagedResponse<ProductResponse>(productResponses);
            }

            return PaginationHelper.CreatePaginatedResponse(_uriService, pagination, productResponses);
        }
    }
}
