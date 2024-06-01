using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RentalCompany.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Get details for a plan.
        /// </summary>
        /// <param name="input">Start Date and Expected Return Date</param>
        /// <returns>details about price</returns>
        /// <response code="200">Succes</response>
        /// <response code="400">Bad Request</response>     
        [HttpGet("simulation")]
        public IActionResult GetSimulationPlan([FromQuery] RentalPlanInput input)
        {
            return Ok(_orderService.GetSimulationPlan(input));
        }

        /// <summary>
        /// Get motorcycles availables for create a new order
        /// </summary>
        /// <returns>Motorcycles list</returns>
        /// <response code="200">Succes</response>
        /// <response code="400">Bad Request</response>       
        [HttpGet("motorcycles/available")]
        public IActionResult GetAllMotorcyclesAvailable()
        {
            return Ok(_orderService.GetAllMotorcyclesAvailable());
        }

        /// <summary>
        /// Create a new Order
        /// </summary>
        /// <param name="input">Order Fields</param>
        /// <returns>Simulation order</returns>
        /// <response code="200">Succes</response>
        /// <response code="400">Bad Request</response>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateOrderInput input)
        {
            return Ok(await _orderService.CreateOrder(input));
        }


        /// <summary>
        /// Finish orders
        /// </summary>
        /// <param name="id">Id Order</param>
        /// <returns>Order Details</returns>
        /// <response code="200">Succes</response>
        /// <response code="404">NotFound</response>
        [HttpPut("finish/{id}")]
        public async Task<IActionResult> FinishOrder(string id)
        {
            var result = await _orderService.GetById(id);
            if (result == null) return NotFound();

            var order = await _orderService.FinishOrder(id);

            return Ok(order);
        }

        /// <summary>
        /// Get orders by deliveryManId
        /// </summary>
        /// <param name="deliveryManId">Id Motorcycle</param>
        /// <returns>Order List</returns>
        /// <response code="200">Succes</response>
        [HttpGet("deliveryMan/{deliveryManId}")]
        public async Task<IActionResult> GetByDeliveryManId(string deliveryManId)
        {
            return Ok(await _orderService.GetByDeliveryManId(deliveryManId));
        }

        /// <summary>
        /// Get orders by motorcycleid
        /// </summary>
        /// <param name="motorcycleId">Id Motorcycle</param>
        /// <returns>Order List</returns>
        /// <response code="200">Succes</response>
        [HttpGet("motorcycle/{motorcycleId}")]
        public async Task<IActionResult> GetByMotorcycleId(string motorcycleId)
        {
            return Ok(await _orderService.GetByMotorcycleId(motorcycleId));
        }

    }
}
