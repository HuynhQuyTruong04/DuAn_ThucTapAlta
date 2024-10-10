using DuAn_ThucTapAlta.Data;
using DuAn_ThucTapAlta.DTO.Flights;
using DuAn_ThucTapAlta.Mappers;
using DuAn_ThucTapAlta.Models;
using DuAn_ThucTapAlta.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DuAn_ThucTapAlta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IFlightService _flightService;

        public FlightController(IFlightService flightService, ApplicationDBContext context)
        {
            _context = context;
            _flightService = flightService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFlight(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var flight = await _flightService.GetFlightByIdAsync(id);

            if (flight == null)
            {
                return NotFound("Không tìm thấy Flight!");
            }

            return Ok(flight.ToFlightDTO());
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFlights()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var flights = await _flightService.GetAllFlightsAsync();

            var flightDto = flights.Select(s => s.ToFlightDTO()).ToList();

            return Ok(flightDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFlight([FromBody] CreateFlightRequestDTO flightDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (flightDto == null)
            {
                return BadRequest("Chuyến bay không hợp lệ!");
            }

            var flightModel = flightDto.ToFlightFromCreateDTO();

            await _flightService.CreateFlightAsync(flightModel);

            return CreatedAtAction(nameof(GetFlight), new { id = flightModel.FlightId }, flightModel.ToFlightDTO());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFlight(int id, [FromBody] UpdateFlightRequestDTO updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var flightModel = await _flightService.UpdateFlightAsync(id, updateDto);

            if (flightModel == null)
            {
                return NotFound("Chuyến bay không tồn tại.");
            }

            return Ok(flightModel.ToFlightDTO());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlight(int id)
        {
            var isDeleted = await _flightService.DeleteFlightAsync(id);

            if (!isDeleted)
            {
                return NotFound("Chuyến bay không tồn tại.");
            }

            return NoContent();
        }
    }
}
