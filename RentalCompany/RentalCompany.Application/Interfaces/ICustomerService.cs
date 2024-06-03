using RentalCompany.Application.Models.Input;
using RentalCompany.Application.Models.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCompany.Application.Interfaces
{
    public interface ICustomerService
    {
        Task CreateCustomer(CustomerInput customer);
        Task UpdateAsync(CustomerUpdateInput customer, string cnhNumber);
        Task UpdateCnhImage(UploadCnhImageInput image);
        Task DeleteAsync(string cnhNumber);
        Task<CustomerOutput?> GetByCnhNumber(string cnh);
        Task<CustomerDetailsOutput?> GetDetailsByCnhNumber(string cnhNumber);
    }
}
