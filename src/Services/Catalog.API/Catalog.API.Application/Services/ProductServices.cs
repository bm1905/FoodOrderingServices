using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.API.Application.Exceptions;
using Catalog.API.Application.Helpers;
using Catalog.API.Application.Models.DTOs;
using Catalog.API.Application.Models.Pagination;
using Catalog.API.Application.UriService;
using Catalog.API.Core.Entities;
using Catalog.API.DataAccess.Repositories;

namespace Catalog.API.Application.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public ProductServices(IProductRepository productRepository, IMapper mapper, IUriService uriService)
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
                var pageSize = pagination.PageSize;

                var productsCount = await _productRepository.GetProductsCount();

                if (productsCount <= 0) throw new NotFoundException("No any products found!");

                if (productsCount - skip <= 0) throw new NotFoundException($"No product exists for page number {pagination.PageNumber}!");

                products = await _productRepository.GetPaginatedProducts(skip, pageSize);
            }
            else
            {
                products = await _productRepository.GetAllProducts();
            }

            if (products == null || !products.Any()) throw new NotFoundException("No any products found!");

            IList<ProductResponse> productResponses = _mapper.Map<IList<ProductResponse>>(products);

            if (pagination == null || pagination.PageNumber < 1 || pagination.PageSize < 1)
            {
                return new PagedResponse<ProductResponse>(productResponses);
            }

            return PaginationHelper.CreatePaginatedResponse(_uriService, pagination, productResponses);
        }

        public async Task<ProductResponse> ProcessGetProductByIdAsync(string productId)
        {
            Product product = await _productRepository.GetProductById(productId);

            if (product == null) throw new NotFoundException($"Product with id: {productId} not found!");

            return _mapper.Map<ProductResponse>(product);
        }

        public async Task<PagedResponse<ProductResponse>> ProcessGetProductsByCategoryAsync(PaginationQuery paginationQuery, string category)
        {
            IEnumerable<Product> products;
            PaginationFilter pagination = _mapper.Map<PaginationFilter>(paginationQuery);

            if (pagination != null)
            {
                var skip = (pagination.PageNumber - 1) * pagination.PageSize;
                var pageSize = pagination.PageSize;

                var productsCount = await _productRepository.GetProductsCount();

                if (productsCount <= 0) throw new NotFoundException($"No any products found for category {category}!");

                if (productsCount - skip <= 0) throw new NotFoundException($"No product exists for {category} at page number {pagination.PageNumber}!");

                products = await _productRepository.GetPaginatedProductsByCategory(skip, pageSize, category);
            }
            else
            {
                products = await _productRepository.GetProductsByCategory(category);
            }

            if (products == null || !products.Any()) throw new NotFoundException($"No any products found for category {category}!");

            IList<ProductResponse> productResponses = _mapper.Map<IList<ProductResponse>>(products);

            if (pagination == null || pagination.PageNumber < 1 || pagination.PageSize < 1)
            {
                return new PagedResponse<ProductResponse>(productResponses);
            }

            return PaginationHelper.CreatePaginatedResponse(_uriService, pagination, productResponses);
        }

        public async Task<PagedResponse<ProductResponse>> ProcessGetPopularProductsAsync(PaginationQuery paginationQuery)
        {
            IEnumerable<Product> products;
            PaginationFilter pagination = _mapper.Map<PaginationFilter>(paginationQuery);

            if (pagination != null)
            {
                var skip = (pagination.PageNumber - 1) * pagination.PageSize;
                var pageSize = pagination.PageSize;

                var productsCount = await _productRepository.GetProductsCount();

                if (productsCount <= 0) throw new NotFoundException("No any products found!");

                if (productsCount - skip <= 0) throw new NotFoundException($"No product exists for page number {pagination.PageNumber}!");

                products = await _productRepository.GetPaginatedPopularProducts(skip, pageSize);
            }
            else
            {
                products = await _productRepository.GetPopularProducts();
            }

            if (products == null || !products.Any()) throw new NotFoundException("No any products found!");

            IList<ProductResponse> productResponses = _mapper.Map<IList<ProductResponse>>(products);

            if (pagination == null || pagination.PageNumber < 1 || pagination.PageSize < 1)
            {
                return new PagedResponse<ProductResponse>(productResponses);
            }

            return PaginationHelper.CreatePaginatedResponse(_uriService, pagination, productResponses);
        }
    }
}
