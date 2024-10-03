using DuAn_ThucTapAlta.Data;
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

        public async Task<Flight> GetFlightByIdAsync(int flightId)
        {
            return await _context.Flights.FindAsync(flightId);
        }

        public async Task<IEnumerable<Flight>> GetAllFlightsAsync()
        {
            return await _context.Flights.ToListAsync();
        }

        public async Task<Flight> CreateFlightAsync(Flight flight)
        {
            _context.Flights.Add(flight);
            await _context.SaveChangesAsync();
            return flight;
        }

        public async Task<Flight> UpdateFlightAsync(Flight flight)
        {
            _context.Entry(flight).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return flight;
        }

        public async Task<bool> DeleteFlightAsync(int flightId)
        {
            var flight = await _context.Flights.FindAsync(flightId);
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
