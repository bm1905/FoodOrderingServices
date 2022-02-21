using Catalog.API.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.API.Model.Entities;
using AutoMapper;
using Catalog.API.Model.DTOs;
using Common.Exceptions;

namespace Catalog.API.BLL
{
    public class ProductServiceBll : IProductServiceBll
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductServiceBll(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<ProductResponse>> ProcessGetProductsAsync()
        {
            IEnumerable<Product> products = await _productRepository.GetProducts();
            if (products == null || !products.Any()) throw new NotFoundException("Product not found!");

            return _mapper.Map<IList<ProductResponse>>(products);
        }
    }
}
