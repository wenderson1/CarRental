using RentalCompany.Application.Models.Input;
using RentalCompany.Application.Models.Output;

namespace RentalCompany.Application.Interfaces
{
    public interface IOrderService
    {
        RentalPlanOutput GetSimulationPlan(RentalPlanInput input);
        Task<CreateOrderOutput> CreateOrder(CreateOrderInput input);
        List<CarOutput?> GetAllCarsAvailable();
        Task<FinishOrderOutput> FinishOrder(string id);
        Task<OrderOutput?> GetById(string id);
        Task<List<OrderOutput>> GetByCarId(string carId);
        Task<List<OrderOutput>> GetByCustomerId(string customerId);
    }
}
