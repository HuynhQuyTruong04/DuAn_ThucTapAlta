using DuAn_ThucTapAlta.Data;
using DuAn_ThucTapAlta.DTO.Flights;
using DuAn_ThucTapAlta.Models;
using Microsoft.EntityFrameworkCore;

namespace DuAn_ThucTapAlta.Services
{
    public class FlightService : IFlightService
    {
        private readonly ApplicationDBContext _context;
        public FlightService (ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Flight> GetFlightByIdAsync(int id)
        {
            return await _context.Flights.FirstOrDefaultAsync(s => s.FlightId == id);
        }

        public async Task<IEnumerable<Flight>> GetAllFlightsAsync()
        {
            return await _context.Flights.ToListAsync();
        }

        public async Task<Flight> CreateFlightAsync(Flight flight)
        {
            await _context.Flights.AddAsync(flight);
            await _context.SaveChangesAsync();
            return flight;
        }

        public async Task<Flight> UpdateFlightAsync(int id, UpdateFlightRequestDTO updateDto)
        {
            var existingFlight = await _context.Flights.FirstOrDefaultAsync(x => x.FlightId == id);

            if (existingFlight == null)
            {
                return null;
            }

            existingFlight.FlightNo = updateDto.FlightNo;
            existingFlight.Departure = updateDto.Departure;
            existingFlight.Destination = updateDto.Destination;
            existingFlight.DepartureDate = updateDto.DepartureDate;
            existingFlight.Status = updateDto.Status;
            existingFlight.UserId = updateDto.UserId;

            await _context.SaveChangesAsync();
            return existingFlight;
        }

        public async Task<bool> DeleteFlightAsync(int id)
        {
            var flight = await _context.Flights.FindAsync(id);
            if (flight == null)
            {
                return false;
            }
            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
