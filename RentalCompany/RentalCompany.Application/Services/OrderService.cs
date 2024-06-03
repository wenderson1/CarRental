using RentalCompany.Application.Interfaces;
using RentalCompany.Application.Models.Input;
using RentalCompany.Application.Models.Output;
using RentalCompany.Core.CacheStorage;
using RentalCompany.Core.Entities;
using RentalCompany.Core.Enums;
using RentalCompany.Core.Repositories;
using RentalCompany.Infrastructure.MessageBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCompany.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly ICacheService _cache;
        private readonly IOrderRepository _repository;
        private readonly IMessageBusClient _messageBus;
        private readonly string _exchange = "order-service";

        public OrderService(ICacheService cache, IOrderRepository repository, IMessageBusClient client)
        {
            _cache = cache;
            _repository = repository;
            _messageBus = client;
        }

        public async Task<CreateOrderOutput> CreateOrder(CreateOrderInput input)
        {
            var orderOutput = new CreateOrderOutput
            {
                StartDate = input.StartDate,
                ExpectedReturnDate = input.ExpectedReturnDate,
                Price = RentalPrices.CalculateTotalAmountWithoutPenalty(input.StartDate, input.ExpectedReturnDate)
            };
            var order = new Order(input.IdCustomer, input.IdCar, OrderStatusEnum.Created, input.StartDate, input.ExpectedReturnDate, null, orderOutput.Price);

            var customerOrder = await GetById(order.Id);

            if (customerOrder.Status == OrderStatusEnum.Created)
                throw new Exception("This customer already has a active order.");

            PublishUsedCar(input.IdCar);

            await _repository.AddAsync(order);

            orderOutput.Id = order.Id;
            return orderOutput;
        }

        public async Task<FinishOrderOutput> FinishOrder(string id)
        {
            var result = await _repository.GetById(id);

            if (result.StartDate < DateTime.Now)
                throw new Exception("Cannot finish the order when it's not start.");

            if (result.ExpectedReturnDate < DateTime.Now)
                result.ReturnDate = result.ExpectedReturnDate;

            result.Status = OrderStatusEnum.Finished;
            result.Price = RentalPrices.CalculateTotalAmountWithPenalty(result.StartDate, result.ExpectedReturnDate, result.ReturnDate ?? DateTime.Now);
            result.ReturnDate = DateTime.Now.Date;

            PublishReturnedCar(result.IdCar);

            await _repository.UpdateAsync(result);

            return new FinishOrderOutput
            {
                StartDate = result.StartDate,
                ExpectedReturnDate = result.ExpectedReturnDate,
                ReturnDate = result.ReturnDate ?? DateTime.Now,
                Price = result.Price
            };
        }

        public List<CarOutput?> GetAllCarsAvailable()
        {
            var result = _cache.GetCarList();

            if (result == null) return new List<CarOutput>();

            return result.Select(x => new CarOutput(x.Id, x.Year, x.Model, x.LicensePlate)).ToList();
        }

        public async Task<List<OrderOutput>> GetByCarId(string carId)
        {
            var result = await _repository.GetByCarId(carId);

            if (!result.Any()) return new List<OrderOutput>();

            return result.Select(o => new OrderOutput(o.Id,
                                                      o.IdCustomer, 
                                                      o.IdCar,
                                                      o.Status,
                                                      o.StartDate,
                                                      o.ExpectedReturnDate,
                                                      o.ReturnDate,
                                                      o.Price)).ToList();
        }

        public async Task<List<OrderOutput>> GetByCustomerId(string customerId)
        {
            var result = await _repository.GetByCustomerId(customerId);

            if (!result.Any()) return new List<OrderOutput>();

            return result.Select(o => new OrderOutput(o.Id,
                                                      o.IdCustomer,
                                                      o.IdCar,
                                                      o.Status,
                                                      o.StartDate,
                                                      o.ExpectedReturnDate,
                                                      o.ReturnDate,
                                                      o.Price)).ToList();
        }

        public async Task<OrderOutput?> GetById(string id)
        {
            var result = await _repository.GetById(id);

            if (result == null) return null;

            return new OrderOutput(result.Id,
                                   result.IdCustomer,
                                   result.IdCar,
                                   result.Status,
                                   result.StartDate,
                                   result.ExpectedReturnDate,
                                   result.ReturnDate,
                                   result.Price);
        }

        public RentalPlanOutput GetSimulationPlan(RentalPlanInput input)
        {
            var expectedPrice = RentalPrices.CalculateTotalAmountWithoutPenalty(input.StartDate, input.ExpectedReturnDate);

            return new RentalPlanOutput
            {
                ExpectedPrice = expectedPrice,
                StartDate = input.StartDate,
                ExpectedReturnDate = input.ExpectedReturnDate
            };
        }

        public void PublishUsedCar(string idCar)
        {
            _messageBus.Publish(idCar, "used-car", _exchange);
        }

        public void PublishReturnedCar(string idCar)
        {
            _messageBus.Publish(idCar, "returned-car", _exchange);
        }

    }
}
