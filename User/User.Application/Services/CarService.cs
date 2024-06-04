using User.Application.Interfaces;
using User.Application.Models.Input;
using User.Application.Models.Output;
using User.Core.Entities;
using User.Core.Enums;
using User.Core.Repositories;
using User.Infrastructure.MessageBus;

namespace User.Application.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _repository;
        private readonly IMessageBusClient _messageBus;
        private readonly string _exchange = "car-service";
        private readonly string _routingKey = "available-cars";

        public CarService(ICarRepository carRepository
                         ,IMessageBusClient messageBusClient)
        {
            _repository = carRepository;
            _messageBus = messageBusClient;
        }

        public async Task CreateCarAsync(CarInput input)
        {
            await _repository.AddAsync(new Car(Guid.NewGuid().ToString(), input.Year, input.Model, input.LicensePlate, 0));
            PublishAvailableCars();
        }

        public async Task DeleteCarAsync(string licensePlate)
        {
            await _repository.DeleteAsync(licensePlate);
            PublishAvailableCars();
        }

        public async Task<List<CarOutput>> GetAllAsync()
        {
            var cars = await _repository.GetAllAsync();

            return cars.Select(c => new CarOutput(c.Id, c.Year, c.Model, c.LicensePlate, c.Status)).ToList();
        }

        public async Task<CarDetailsOutput?> GetCarsByLicensePlateAsync(string licensePlate)
        {
            var car = await _repository.GetByLicensePlateAsync(licensePlate) ?? null;

            if (car == null) return null;

            CarDetailsOutput? carDetails = new CarDetailsOutput(car.Id, car.Year, car.Model, car.LicensePlate, car.Status);

            return carDetails;
        }

        public async Task UpdateAvailabilityCar(string id, CarStatusEnum statusEnum)
        {
            await _repository.UpdateAvailabilityCar(id, statusEnum);
            PublishAvailableCars();
        }

        public async Task UpdateCarAsync(CarDetailsOutput result, CarInput car, string wrongLicensePlate)
        {
            var carUpdate = new Car(result.Id, car.Year, car.Model, car.LicensePlate, 0);
            await _repository.UpdateAsync(carUpdate, wrongLicensePlate);
            PublishAvailableCars();
        }
  
        public async void PublishAvailableCars()
        {
            var car = (await _repository.GetAllAsync())
                                        .Where(m => m.Status == CarStatusEnum.Available);

            _messageBus.Publish(car, _routingKey, _exchange);
        }
    }
}
