using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Catalog.API.Application.Configurations;
using CloudinaryDotNet;
using Microsoft.Extensions.Options;

namespace Catalog.API.Application.Services.PhotoService
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;
        private readonly string _environment;

        public PhotoService(IOptions<CloudinarySettings> config)
        {
            _environment = config.Value.Environment;

            var acc = new Account
            (
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file, string category)
        {
            var uploadResult = new ImageUploadResult();
            if (file.Length <= 0) return uploadResult;

            await using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                Folder = $"/food-service/{_environment}/{category}",
                File = new FileDescription(file.FileName, stream),
                Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
            };
            uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);
            return result;
        }
    }
}
