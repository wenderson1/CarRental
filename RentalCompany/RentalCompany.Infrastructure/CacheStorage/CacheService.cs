using Microsoft.Extensions.Caching.Memory;
using RentalCompany.Core.CacheStorage;
using RentalCompany.Core.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace RentalCompany.Infrastructure.CacheStorage
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        private const string _cacheKey = "CarList";

        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
        }
        public List<Car> GetCarList()
        {
            var result = _cache.Get<List<Car>>(_cacheKey);

            if (result == null) return new List<Car>();

            return result;
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public void SetCache(List<Car> data)
        {
            _cache.Remove(_cacheKey);
            _cache.Set(_cacheKey, data);
        }
    }
}
