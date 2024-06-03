using RentalCompany.Application.Interfaces;
using RentalCompany.Application.Models.Input;
using RentalCompany.Application.Models.Output;
using RentalCompany.Core.Entities;
using RentalCompany.Core.Repositories;

namespace RentalCompany.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly string _basePath = @"C:\CnhImages\";
        
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task CreateCustomer(CustomerInput input)
        {
            await _customerRepository.AddAsync(new Customer(input.Name, input.CNPJ, input.BirthDate, input.CnhNumber, input.CnhType));
        }

        public async Task DeleteAsync(string cnhNumber)
        {
            await _customerRepository.DeleteAsync(cnhNumber);
        }

        public async Task<CustomerOutput?> GetByCnhNumber(string cnh)
        {
            var result = await _customerRepository.GetByCnhNumber(cnh);

            if (result == null) return null;

            return new CustomerOutput(result.Id, result.Name, result.CNPJ, result.BirthDate, result.CnhNumber, result.CnhType);
        }

        public async Task<CustomerDetailsOutput?> GetDetailsByCnhNumber(string cnhNumber)
        {
            var result = await _customerRepository.GetByCnhNumber(cnhNumber);

            if (result == null) return null;

            var image = this.GetLocalImage(cnhNumber);

            return new CustomerDetailsOutput(result.Id,
                                             result.Name,
                                             result.CNPJ,
                                             result.BirthDate,
                                             result.CnhNumber,
                                             result.CnhType,
                                             image);
        }

        public async Task UpdateAsync(CustomerUpdateInput input, string cnhNumber)
        {
            var customerUpdate = new Customer(input.Name, input.CNPJ, input.BirthDate, input.CnhType);
            await _customerRepository.UpdateAsync(customerUpdate, cnhNumber);
        }

        public  async Task UpdateCnhImage(UploadCnhImageInput image)
        { 
            await SaveLocalImage(image);
        }

        public byte[] GetLocalImage(string cnhNumber)
        {
            var imagePath = $"{_basePath}{cnhNumber}";

            if (File.Exists($"{imagePath}.png"))
            {
                byte[] imageBytes = File.ReadAllBytes($"{imagePath}.png");
                return imageBytes;
            }
            if (File.Exists($"{imagePath}.bmp"))
            {
                byte[] imageBytes = File.ReadAllBytes($"{imagePath}.bmp");
                return imageBytes;
            }
            return [];
        }
        public async Task SaveLocalImage(UploadCnhImageInput input)
        {
            string fileName = $"{input.CnhNumber}{input.CnhImage.FileName.Substring(input.CnhImage.FileName.Length - 4)}";
            string filePath = Path.Combine(_basePath, fileName);


            if (!Directory.Exists(_basePath))
            {
                Directory.CreateDirectory(_basePath);
            }

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await input.CnhImage.CopyToAsync(fileStream);
            }
        }

    }
}

