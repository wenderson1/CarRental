using Microsoft.AspNetCore.Mvc;

namespace RentalCompany.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Create a new Customer
        /// </summary>
        /// <param name="input">Customer Fields</param>
        /// <response code="200">Succes</response>
        /// <response code="400">Bad Request</response>        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerInput input)
        {
            var result = await _customerService.GetByCnhNumber(input.CnhNumber);

            if (result != null) return BadRequest("This CNH Number already exists.");

            await _customerService.CreateCustomer(input);

            return Ok();
        }

        /// <summary>
        /// Update a exists Customer
        /// </summary>
        /// <param name="input">Customer Fields</param>
        /// <response code="200">Succes</response>
        /// <response code="400">Bad Request</response> 
        [HttpPut("{cnhNumber}")]
        public async Task<IActionResult> Put([FromBody]  input, string cnhNumber)
        {
            var result = await _customerService.GetByCnhNumber(cnhNumber);

            if (result == null) return BadRequest("This CNH Number is not found.");

            await _customerService.UpdateAsync(input, cnhNumber);

            return Ok();
        }

        /// <summary>
        /// Upload the CNH Image
        /// </summary>
        /// <param name="input">CNH Number and CNH Image</param>
        /// <response code="200">Succes</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="404">Bad Request</response> 
        [HttpPost("image")]
        public async Task<IActionResult> UploadCnhImage(UploadCnhImageInput input)
        {
            var result = await _customerService.GetByCnhNumber(input.CnhNumber);

            if (result == null) return NotFound();

            await _customerService.UpdateCnhImage(input);

            return Ok();
        }

        /// <summary>
        /// Delete a Customer
        /// </summary>
        /// <param name="cnhNumber">Cnh Number</param>
        /// <response code="200">Succes</response>
        /// <response code="404">NotFound</response> 

        [HttpDelete("{cnhNumber}")]
        public async Task<IActionResult> Delete(string cnhNumber)
        {
            var result = await _customerService.GetByCnhNumber(cnhNumber);

            if (result == null) return NotFound();

            await _customerService.DeleteAsync(cnhNumber);

            return Ok();
        }
        /// <summary>
        /// Get details of Customer
        /// </summary>
        /// <param name="cnhNumber">Cnh Number</param>
        /// <response code="200">Succes</response>
        /// <response code="404">NotFound</response>         
        [HttpGet("{cnhNumber}")]
        public async Task<IActionResult> GetDetailsByCnhNumber(string cnhNumber)
        {
            var result = await _customerService.GetDetailsByCnhNumber(cnhNumber);

            if (result == null) return NotFound();

            return Ok(result);
        }
    }
}
}
