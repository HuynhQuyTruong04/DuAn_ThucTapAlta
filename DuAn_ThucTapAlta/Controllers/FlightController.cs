using DuAn_ThucTapAlta.Models;
using DuAn_ThucTapAlta.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DuAn_ThucTapAlta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService _flightService;

        public FlightController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFlight(int id)
        {
            var flight = await _flightService.GetFlightByIdAsync(id);

            if (flight == null)
            {
                return NotFound("Chuyến bay không tồn tại!");
            }

            return Ok(flight);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFlights()
        {
            var flights = await _flightService.GetAllFlightsAsync();
            return Ok(flights);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFlight([FromBody] Flight flight)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (flight == null)
            {
                return BadRequest("Chuyến bay không hợp lệ!");
            }

            var createFlight = await _flightService.CreateFlightAsync(flight);

            return CreatedAtAction(nameof(GetFlight), new { id = createFlight.FlightId }, createFlight);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFlight(int id, [FromBody] Flight flight)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != flight.FlightId)
            {
                return BadRequest("ID không hợp lệ!");
            }

            var updateFlight = await _flightService.UpdateFlightAsync(flight);

            if (updateFlight == null)
            {
                return NotFound("Chuyến bay không tồn tại!");
            }

            return Ok(updateFlight);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlight(int id)
        {
            var isDelete = await _flightService.DeleteFlightAsync(id);

            if (!isDelete)
            {
                return NotFound("Chuyến bay không tồn tại!");
            }

            return NoContent();
        }
    }
}
