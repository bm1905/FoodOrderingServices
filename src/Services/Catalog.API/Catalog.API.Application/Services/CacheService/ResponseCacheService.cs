using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Catalog.API.Application.Services.CacheService
{
    public class ResponseCacheService : IResponseCacheService
    {
        private readonly IDistributedCache _distributedCache;

        public ResponseCacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive)
        {
            if (response == null)
            {
                return;
            }

            var serializedResponse = JsonConvert.SerializeObject(response);

            try
            {
                await _distributedCache.SetStringAsync(cacheKey, serializedResponse, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = timeToLive
                });
            }
            catch (Exception)
            {
                // Redis is down
            }
        }

        public async Task<string> GetCachedResponseAsync(string cacheKey)
        {
            try
            {
                var cachedResponse = await _distributedCache.GetStringAsync(cacheKey);
                return string.IsNullOrEmpty(cachedResponse) ? null : cachedResponse;
            }
            catch (Exception)
            {
                // Redis is down
                return null;
            }
        }
    }
}
