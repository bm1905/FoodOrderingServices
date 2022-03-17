using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.API.Application.Exceptions;
using Catalog.API.Application.Helpers;
using Catalog.API.Application.Models.DTOs;
using Catalog.API.Application.Models.DTOs.ProductPhotos;
using Catalog.API.Application.Models.DTOs.Products;
using Catalog.API.Application.Models.Pagination;
using Catalog.API.Application.Services.AccountService;
using Catalog.API.Application.Services.PhotoService;
using Catalog.API.Application.Services.UriService;
using Catalog.API.Core.Entities;
using Catalog.API.DataAccess.Repositories;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace Catalog.API.Application.Services.ProductService
{
    public class ProductServices : IProductServices
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private readonly IPhotoService _photoService;
        private readonly IAccountService _accountService;

        public ProductServices(IProductRepository productRepository, IMapper mapper, IUriService uriService,
            IPhotoService photoService, IAccountService accountService)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _uriService = uriService ?? throw new ArgumentNullException(nameof(uriService));
            _photoService = photoService ?? throw new ArgumentNullException(nameof(photoService));
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
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

                if (productsCount - skip <= 0)
                    throw new NotFoundException($"No product exists for page number {pagination.PageNumber}!");

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

        public async Task<PagedResponse<ProductResponse>> ProcessGetProductsByCategoryAsync(
            PaginationQuery paginationQuery, string category)
        {
            IEnumerable<Product> products;
            PaginationFilter pagination = _mapper.Map<PaginationFilter>(paginationQuery);

            if (pagination != null)
            {
                var skip = (pagination.PageNumber - 1) * pagination.PageSize;
                var pageSize = pagination.PageSize;

                var productsCount = await _productRepository.GetProductsCount();

                if (productsCount <= 0) throw new NotFoundException($"No any products found for category {category}!");

                if (productsCount - skip <= 0)
                    throw new NotFoundException(
                        $"No product exists for {category} at page number {pagination.PageNumber}!");

                products = await _productRepository.GetPaginatedProductsByCategory(skip, pageSize, category);
            }
            else
            {
                products = await _productRepository.GetProductsByCategory(category);
            }

            if (products == null || !products.Any())
                throw new NotFoundException($"No any products found for category {category}!");

            IList<ProductResponse> productResponses = _mapper.Map<IList<ProductResponse>>(products);

            if (pagination == null || pagination.PageNumber < 1 || pagination.PageSize < 1)
            {
                return new PagedResponse<ProductResponse>(productResponses);
            }

            return PaginationHelper.CreatePaginatedResponse(_uriService, pagination, productResponses);
        }

        public async Task<PagedResponse<ProductResponse>> ProcessGetPopularProductsAsync(
            PaginationQuery paginationQuery)
        {
            IEnumerable<Product> products;
            PaginationFilter pagination = _mapper.Map<PaginationFilter>(paginationQuery);

            if (pagination != null)
            {
                var skip = (pagination.PageNumber - 1) * pagination.PageSize;
                var pageSize = pagination.PageSize;

                var productsCount = await _productRepository.GetProductsCount();

                if (productsCount <= 0) throw new NotFoundException("No any products found!");

                if (productsCount - skip <= 0)
                    throw new NotFoundException($"No product exists for page number {pagination.PageNumber}!");

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

        public async Task<ProductResponse> ProcessCreateProductAsync(CreateProductRequest product)
        {
            // Get username
            string userName = AccountHelpers.GetUserName(_accountService);

            Product existingProduct = await _productRepository.GetProductByName(product.Name);
            if (existingProduct != null)
                throw new AlreadyExistsException(
                    $"Product name: {product.Name} already exists! Please select different name!");

            Product mappedProduct = _mapper.Map<Product>(product);

            // Include the username in product
            mappedProduct.CreatedBy = userName;

            // Upload to cloudinary and create record in database
            ProductPhoto productPhoto = await UploadPhotoToCloudinaryAndDatabase(product.ProductPhoto, product.Category);

            // This will be the first photo upload, so make it main photo by default
            productPhoto.IsMain = true;

            // Include the username in photo
            productPhoto.CreatedBy = userName;

            // Add the photo to list in product collection
            mappedProduct.ProductPhotos.Add(productPhoto);

            // Create the product
            await _productRepository.CreateProduct(mappedProduct);

            return _mapper.Map<ProductResponse>(mappedProduct);
        }

        public async Task<ProductResponse> ProcessUpdateProductAsync(string productId, UpdateProductRequest product)
        {
            // Get username
            string userName = AccountHelpers.GetUserName(_accountService);

            // Old product
            Product oldProduct = await _productRepository.GetProductById(productId);
            if (oldProduct == null) throw new NotFoundException($"Product with id: {productId} not found!");

            // New product
            Product mappedProduct = _mapper.Map<Product>(product);
            mappedProduct.Id = productId;
            mappedProduct.CreatedBy = oldProduct.CreatedBy;
            mappedProduct.CreatedOn = oldProduct.CreatedOn;
            mappedProduct.ProductPhotos = oldProduct.ProductPhotos;

            // Update the username in product
            mappedProduct.CreatedBy = userName;

            bool isUpdated = await _productRepository.UpdateProduct(mappedProduct);

            if (!isUpdated)
                throw new NotFoundException($"Product with name: {mappedProduct.Name} could not be updated!");

            return _mapper.Map<ProductResponse>(mappedProduct);
        }

        public async Task<ProductPhotoResponse> ProcessAddPhotoAsync(CreateProductPhotoRequest photoRequest)
        {
            // Check if product id exists or not
            Product product = await _productRepository.GetProductById(photoRequest.ProductId);
            if (product == null) throw new NotFoundException($"Product with id: {photoRequest.ProductId} not found!");

            // Upload to cloudinary and create record in database
            var productPhoto = await UploadPhotoToCloudinaryAndDatabase(photoRequest.File, product.Category);

            // Add the photo to list in product collection
            product.ProductPhotos.Add(productPhoto);

            // Update the product with new photo
            bool isUpdated = await _productRepository.UpdateProduct(product);

            // If something went wrong while updating database, delete uploaded photo from cloudinary
            if (!isUpdated) await DeletePhotoFromCloudinary(productPhoto.PublicId);

            // Everything goes fine
            return _mapper.Map<ProductPhotoResponse>(productPhoto);
        }

        public async Task ProcessDeleteProductByIdAsync(string productId)
        {
            // Find the product
            Product product = await _productRepository.GetProductById(productId);
            if (product == null) throw new NotFoundException($"Product with id: {productId} not found!");

            // Delete from database
            bool isDeletedFromDb = await _productRepository.DeleteProduct(productId);
            if (!isDeletedFromDb) throw new InternalServerErrorException($"Product with id: {productId} could not be deleted!");

            // Delete from cloudinary
            try
            {
                if (product.ProductPhotos == null) return;

                foreach (var productPhoto in product.ProductPhotos)
                {
                    // Delete from cloud
                    await DeletePhotoFromCloudinary(productPhoto.PublicId);
                }
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorException(
                    $"Something went wrong while deleting photos. Please clean the cloud manually!, {ex.Message}");
            }
        }

        public async Task ProcessDeleteProductPhotoByIdAsync(DeleteOrUpdateProductPhotoRequest deleteOrUpdateProductPhotoRequest)
        {
            var productId = deleteOrUpdateProductPhotoRequest.ProductId;
            var productPhotoId = deleteOrUpdateProductPhotoRequest.ProductPhotoId;

            // Find the product
            Product product = await _productRepository.GetProductById(productId);
            if (product == null) throw new NotFoundException($"Product with id: {productId} not found!");

            if (product.ProductPhotos == null) return;

            // Get the photo from array
            foreach (var productPhoto in product.ProductPhotos)
            {
                if (productPhoto.Id != productPhotoId) continue;

                if (productPhoto.IsMain) throw new UnauthorizedException("Not authorized to delete main photo!");

                // Delete from cloud
                await DeletePhotoFromCloudinary(productPhoto.PublicId);
            }

            // Find and delete product photo from array
            bool isDeleted = await _productRepository.RemovePhotoFromProduct(productPhotoId, productId);
            if (!isDeleted) throw new InternalServerErrorException($"Photo with id: {productPhotoId} could not be deleted from product id: {productId}!");
        }

        public async Task ProcessSetMainPhotoByIdAsync(DeleteOrUpdateProductPhotoRequest deleteOrUpdateProductPhotoRequest)
        {
            var productId = deleteOrUpdateProductPhotoRequest.ProductId;
            var productPhotoId = deleteOrUpdateProductPhotoRequest.ProductPhotoId;

            // Find the product
            Product product = await _productRepository.GetProductById(productId);
            if (product == null) throw new NotFoundException($"Product with id: {productId} not found!");

            if (product.ProductPhotos == null) return;

            await _productRepository.SetMainPhoto(productPhotoId, productId);
        }

        private async Task<ProductPhoto> UploadPhotoToCloudinaryAndDatabase(IFormFile file, string category)
        {
            // Get username
            string userName = AccountHelpers.GetUserName(_accountService);

            // Insert the photo first
            ImageUploadResult result = await _photoService.AddPhotoAsync(file, category);
            if (result.Error != null)
                throw new InternalServerErrorException("Something went wrong during photo upload!");

            try
            {
                // Construct the photo object
                ProductPhoto productPhoto = new()
                {
                    Url = result.SecureUrl.AbsoluteUri,
                    PublicId = result.PublicId,
                    IsMain = false,
                    CreatedBy = userName,
                    CreatedOn = DateTime.UtcNow,
                    UpdatedBy = userName,
                    UpdatedOn = DateTime.UtcNow
                };

                return productPhoto;
            }
            catch (Exception ex)
            {
                await DeletePhotoFromCloudinary(result.PublicId);
                throw new InternalServerErrorException(
                    $"Something went wrong while updating photo information to database. Error: {ex.Message}");
            }
        }

        private async Task DeletePhotoFromCloudinary(string publicId)
        {
            // Delete uploaded photo from cloudinary
            var deletionResult = await _photoService.DeletePhotoAsync(publicId);

            // Unable to delete photo from cloudinary
            if (deletionResult.Error != null)
                throw new InternalServerErrorException(
                    $"Error during operation. Please manually delete photo from cloud. Error: {deletionResult.Error.Message}");
        }
    }
}
