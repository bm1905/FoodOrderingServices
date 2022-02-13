using System.IO;
using System.Threading.Tasks;
using Catalog.API.Helpers.Settings;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Catalog.API.Helpers.PhotoService
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;

        public PhotoService(IOptions<CloudinarySettings> config)
        {
            Account acc = new(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            ImageUploadResult uploadResult = new();
            if (file.Length <= 0) return uploadResult;

            await using Stream stream = file.OpenReadStream();
            ImageUploadParams uploadParams = new()
            {
                File = new FileDescription(file.FileName, stream),
                Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
            };
            uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            DeletionParams deleteParams = new(publicId);
            DeletionResult result = await _cloudinary.DestroyAsync(deleteParams);
            return result;
        }
    }
}
