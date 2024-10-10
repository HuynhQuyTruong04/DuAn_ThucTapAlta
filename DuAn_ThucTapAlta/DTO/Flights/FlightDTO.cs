using DuAn_ThucTapAlta.Models;

namespace DuAn_ThucTapAlta.DTO.Flights
{
    public class FlightDTO
    {
        public int FlightId { get; set; }
        public string FlightNo { get; set; }
        public string Departure { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureDate { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }
    }
}
