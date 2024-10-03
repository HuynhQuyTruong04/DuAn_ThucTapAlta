using System;

namespace DuAn_ThucTapAlta.Models
{
    public class Flight
    {
        public int FlightId { get; set; }
        public string FlightNo { get; set; } //so hieu chuyen bay
        public string Departure {  get; set; } //noi khoi hanh
        public string Destination { get; set; } //noi den
        public DateTime DepartureDate { get; set; } //ngay khoi hanh
        public string Status { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Document> Documents { get; set; }
    }
}
