using MongoDB.Driver;
using User.Core.Entities;
using User.Core.Enums;
using User.Core.Repositories;

namespace User.Infrastructure.Persistence.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly IMongoCollection<Car> _collection;
        public async Task AddAsync(Car car)
        {
            await _collection.InsertOneAsync(car);
        }

        public async Task DeleteAsync(string licensePlate)
        {
            var filter = Builders<Car>.Filter.And(
                         Builders<Car>.Filter.Where(m => m.LicensePlate == licensePlate));

            await _collection.FindOneAndDeleteAsync(filter);
        }

        public async Task<List<Car>> GetAllAsync()
        {
            return await _collection.Find(m => true).ToListAsync();
        }

        public async Task<Car> GetByLicensePlateAsync(string licensePlate)
        {
            return await _collection.Find(m => m.LicensePlate == licensePlate).SingleOrDefaultAsync();
        }

        public async Task UpdateAsync(Car newLicensePlate, string wrongLicensePlate)
        {
            await _collection.ReplaceOneAsync(m => m.LicensePlate == wrongLicensePlate, newLicensePlate);
        }

        public async Task UpdateAvailabilityCar(string id, CarStatusEnum statusEnum)
        {
            var filter = Builders<Car>.Filter.Eq("_id", id);

            var update = Builders<Car>.Update.Set("Status", statusEnum);

            await _collection.UpdateOneAsync(filter, update);
        }
    }
}
