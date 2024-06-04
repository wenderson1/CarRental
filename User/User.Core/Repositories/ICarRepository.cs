using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Core.Entities;
using User.Core.Enums;

namespace User.Core.Repositories
{
    public interface ICarRepository
    {
        Task<List<Car>> GetAllAsync();
        Task<Car> GetByLicensePlateAsync(string licensePlate);
        Task AddAsync(Car car);
        Task UpdateAsync(Car newLicensePlate, string wrongLicensePlate);
        Task DeleteAsync(string licensePlate);
        Task UpdateAvailabilityCar(string id, CarStatusEnum statusEnum);
    }
}
