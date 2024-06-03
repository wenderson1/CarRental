using MongoDB.Driver;
using RentalCompany.Core.Entities;
using RentalCompany.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCompany.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoCollection<Order> _collection;
        public OrderRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<Order>("order");
        }

        public async Task AddAsync(Order order)
        {
            await _collection.InsertOneAsync(order);
        }

        public async Task<List<Order>> GetByCarId(string carId)
        {
            return await _collection.Find(FilterCar(carId)).ToListAsync();
        }

        public async Task<List<Order>> GetByCustomerId(string customerId)
        {
            return await _collection.Find(FilterCustomer(customerId)).ToListAsync();
        }

        public async Task<Order> GetById(string id)
        {
            return await _collection.Find(o => o.Id == id).SingleOrDefaultAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            await _collection.ReplaceOneAsync(m => m.Id == order.Id, order);
        }

        public static FilterDefinition<Order> Filter(string id)
        {
            var filter = Builders<Order>.Filter.And(
             Builders<Order>.Filter.Where(d => d.Id == id));

            return filter;
        }

        public static FilterDefinition<Order> FilterCustomer(string customerId)
        {
            var filter = Builders<Order>.Filter.And(
             Builders<Order>.Filter.Where(d => d.IdCustomer == customerId));

            return filter;
        }
        public static FilterDefinition<Order> FilterCar(string carId)
        {
            var filter = Builders<Order>.Filter.And(
             Builders<Order>.Filter.Where(d => d.IdCar == carId));

            return filter;
        }

    }
}
