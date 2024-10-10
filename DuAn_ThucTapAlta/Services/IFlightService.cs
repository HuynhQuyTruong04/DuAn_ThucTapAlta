using DuAn_ThucTapAlta.DTO.Flights;
using DuAn_ThucTapAlta.Models;

namespace DuAn_ThucTapAlta.Services
{
    public interface IFlightService
    {
        Task<Flight> GetFlightByIdAsync(int flightId);
        Task<IEnumerable<Flight>> GetAllFlightsAsync();
        Task<Flight> CreateFlightAsync(Flight flight);
        Task<Flight> UpdateFlightAsync(int id, UpdateFlightRequestDTO updateDto);
        Task<bool> DeleteFlightAsync(int flightId);
    }
}
