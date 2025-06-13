using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Caching.Memory;

namespace dev_connect.Server.Services.Cache
{
    public interface ICacheService
    {
        T GetCache<T>(string cachekey) where T : class;
        T GetOrAddCache<T>(string cachekey, Func<T> get, CacheSetting cacheSetting = null) where T : class;
        void SetCache<T>(string cacheKey, T item, CacheSetting cacheSetting = null) where T : class;
        void ClearCache(string cachekey);
    }
    public class CacheService(IMemoryCache memoryCache) : ICacheService
    {
        public readonly IMemoryCache _memoryCache = memoryCache;

        public void ClearCache(string cachekey)
        {
            _memoryCache.Remove(cachekey);
        }

        public T GetCache<T>(string cacheKey) where T : class
        {
            if (_memoryCache.TryGetValue(cacheKey, out T cachedValue))
            {
                return cachedValue;
            }
            return null;
        }

        public T GetOrAddCache<T>(string cachekey, Func<T> get, CacheSetting cacheSetting = null) where T : class
        {
            var cachedValue = GetCache<T>(cachekey);

            if(cachedValue != null)
            {
                return cachedValue;
            }

            var newValue = get();

            if(newValue != null)
            {
                if(cachedValue != null)
                {
                    var cacheEntryOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = cacheSetting.AbsoluteExpiration,
                        SlidingExpiration = cacheSetting.SlidingExpiration
                    };

                    _memoryCache.Set(cachekey, newValue, cacheEntryOptions);

                }
                else
                {
                    SetCache<T>(cachekey, newValue, CacheSetting.cacheForThreeHourAndExtendByHalfAndHour);
                }
              
            }
            return newValue;
        }
        public async Task<T> GetOrAddCacheAsync<T>(string cacheKey, Func<Task<T>> getAsync, CacheSetting cacheSetting = null) where T : class
        {
            var cachedValue = GetCache<T>(cacheKey);
            if(cachedValue != null)
            {
                return cachedValue;
            }

            var newValue = await getAsync();

            if(newValue != null)
            {
                if(cacheSetting != null)
                {
                    var cacheEntryOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = cacheSetting.AbsoluteExpiration,
                        SlidingExpiration = cacheSetting.SlidingExpiration
                    };

                    _memoryCache.Set(cacheKey, newValue, cacheEntryOptions);
                }
                else
                {
                    SetCache<T>(cacheKey, newValue, CacheSetting.cacheForThreeHourAndExtendByHalfAndHour);
                }
            }
            return newValue;
        }

        public void SetCache<T>(string cacheKey, T item, CacheSetting cacheSetting = null) where T : class
        {
            if(cacheSetting != null)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = cacheSetting.AbsoluteExpiration,
                    SlidingExpiration = cacheSetting.SlidingExpiration
                };

                _memoryCache.Set(cacheKey, item, cacheEntryOptions);
            }
            else
            {
                _memoryCache.Set(cacheKey, item);
            }
        }
    }
}
