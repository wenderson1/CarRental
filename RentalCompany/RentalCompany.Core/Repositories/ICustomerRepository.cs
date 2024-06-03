using RentalCompany.Core.Entities;

namespace RentalCompany.Core.Repositories
{
    public interface ICustomerRepository
    {
        Task AddAsync(Customer customer);
        Task UpdateAsync(Customer customer, string cnhNumber);
        Task DeleteAsync(string cnhNumber);
        Task<Customer> GetByCnhNumber(string cnhNumber);
    }
}
