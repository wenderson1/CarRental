using Microsoft.AspNetCore.Mvc;
using User.Application.Interfaces;
using User.Application.Models.Input;

namespace User.Api.Controllers
{
    [ApiController]
    [Route("api/Car")]
    public class CarController : ControllerBase
    {
        private readonly ICarService _service;

        public CarController(ICarService service)
        {
            _service = service;
        }

        /// <summary>
        /// Create a new Car
        /// </summary>
        /// <remarks>
        /// {
        /// "Year": 2014
        /// "Model": "Jetta"
        /// "LicensePlate":"ABC123"
        /// }
        /// </remarks>
        /// <param name="input">Car Fields</param>
        /// <response code="200">Succes</response>
        /// <response code="400">Bad Request</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CarInput input)
        {
            var result = await _service.GetCarsByLicensePlateAsync(input.LicensePlate);

            if (result != null)
                return BadRequest("This Car already exists.");

            await _service.CreateCarAsync(input);

            return Created();
        }

        /// <summary>
        /// Get all Cars.
        /// </summary>
        /// <returns>list of Cars</returns>
        /// <response code="200">Succes</response>
        [HttpGet]
        public async Task<IActionResult> GetAllCars()
        {
            return Ok(await _service.GetAllAsync());
        }


        /// <summary>
        /// Get a Car by license plate.
        /// </summary>
        /// <param name="licensePlate">license plate</param>
        /// <returns>details about the Car</returns>
        /// <response code="200">Succes</response>
        /// <response code="404">NotFound</response>    
        [HttpGet("{licensePlate}")]
        public async Task<IActionResult> GetByCarByLicensePlate(string licensePlate)
        {
            var result = await _service.GetCarsByLicensePlateAsync(licensePlate);

            if (result != null) return Ok(result);

            return NotFound();
        }

        /// <summary>
        /// Update a exists Car.
        /// </summary>
        /// <param name="Car">Car fields</param>
        /// <returns>details about price</returns>
        /// <response code="200">Succes</response>
        /// <response code="404">NotFound</response>    
        [HttpPut("{wrongLicensePlate}")]
        public async Task<IActionResult> UpdateCar(string wrongLicensePlate, [FromBody] CarInput car)
        {
            var result = await _service.GetCarsByLicensePlateAsync(wrongLicensePlate);

            if (result == null) return NotFound();

            await _service.UpdateCarAsync(result, car, wrongLicensePlate);

            return Ok();
        }

        /// <summary>
        /// Delete a Car
        /// </summary>
        /// <param name="licensePlate">License Plate of Car</param>
        /// <response code="200">Succes</response>
        /// <response code="404">NotFound</response>
        [HttpDelete("{licensePlate}")]
        public async Task<IActionResult> Delete(string licensePlate)
        {
            var result = await _service.GetCarsByLicensePlateAsync(licensePlate);

            if (result == null) return NotFound("Car Not Found.");

            await _service.DeleteCarAsync(licensePlate);

            return Ok();
        }
    }
}
