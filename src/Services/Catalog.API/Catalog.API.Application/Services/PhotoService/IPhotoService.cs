using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace Catalog.API.Application.Services.PhotoService
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file, string category);
        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}
