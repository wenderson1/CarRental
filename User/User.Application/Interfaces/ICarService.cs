using User.Application.Models.Input;
using User.Application.Models.Output;
using User.Core.Enums;

namespace User.Application.Interfaces
{
    public interface ICarService
    {
        Task CreateCarAsync(CarInput input);
        Task<CarDetailsOutput?> GetCarsByLicensePlateAsync(string licensePlate);
        Task UpdateCarAsync(CarDetailsOutput result, CarInput car, string wrongLicensePlate);
        Task DeleteCarAsync(string licensePlate);
        Task<List<CarOutput>> GetAllAsync();
        Task UpdateAvailabilityCar(string id, CarStatusEnum statusEnum);

    }
}
