using System;
using System.Threading.Tasks;

namespace Catalog.API.Application.Services.CacheService
{
    public interface IResponseCacheService
    {
        Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive);
        Task<string> GetCachedResponseAsync(string cacheKey);
    }
}
