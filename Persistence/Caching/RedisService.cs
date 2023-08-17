using Application.Abstractions.IServices;
using Application.Dtos;

namespace Persistence.Caching;

public class RedisService : IRedisService
{
    private readonly RedisCacheService _redisCacheService;
    private readonly RedisModule _redisModule;


    public RedisService(RedisCacheService redisCacheService)
    {
        _redisCacheService = redisCacheService;
    }

    public string GetCachedDataAsync(string key)
    {
         string cachedData = _redisCacheService.Get(key);

            if (string.IsNullOrEmpty(cachedData))
            {
                // If data not found in cache, fetch it from the original source (e.g., database)
                // and store it in the cache for future use.
                // FetchDataFromOriginalSource()
                // string data = _redisModule.();
                // _redisCacheService.Set(key, data, TimeSpan.FromMinutes(80));
            }

            return cachedData;
    }
}
