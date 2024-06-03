using MongoDB.Driver;
using RentalCompany.Core.Entities;
using RentalCompany.Core.Repositories;

namespace RentalCompany.Infrastructure.Persistence.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IMongoCollection<Customer> _collection;
        public CustomerRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<Customer>("customer");
        }

        public async Task AddAsync(Customer customer)
        {
            await _collection.InsertOneAsync(customer);
        }

        public async Task DeleteAsync(string cnhNumber)
        {
            await _collection.FindOneAndDeleteAsync(Filter(cnhNumber));
        }

        public async Task<Customer> GetByCnhNumber(string cnhNumber)
        {
            return await _collection.Find(Filter(cnhNumber)).SingleOrDefaultAsync();
        }

        public async Task UpdateAsync(Customer customer, string cnhNumber)
        {
            await _collection.ReplaceOneAsync(Filter(cnhNumber), customer);
        }

        public static FilterDefinition<Customer> Filter(string cnhNumber)
        {
            var filter = Builders<Customer>.Filter.And(
             Builders<Customer>.Filter.Where(d => d.CnhNumber == cnhNumber));

            return filter;
        }
    }
}
