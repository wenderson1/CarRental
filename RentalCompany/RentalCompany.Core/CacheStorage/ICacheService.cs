

using RentalCompany.Core.Entities;

namespace RentalCompany.Core.CacheStorage
{
    public interface ICacheService
    {
        void SetCache(List<Car> data);
        List<Car> GetCarList();
        void Remove(string key);
    }
}
