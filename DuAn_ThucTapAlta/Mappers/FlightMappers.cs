using DuAn_ThucTapAlta.DTO.Flights;
using DuAn_ThucTapAlta.DTO.Users;
using DuAn_ThucTapAlta.Models;

namespace DuAn_ThucTapAlta.Mappers
{
    public static class FlightMappers
    {
        public static FlightDTO ToFlightDTO(this Flight flightModel)
        {
            return new FlightDTO
            {
                FlightId = flightModel.FlightId,
                FlightNo = flightModel.FlightNo,
                Departure = flightModel.Departure,
                Destination = flightModel.Destination,
                DepartureDate = flightModel.DepartureDate,
                Status = flightModel.Status,
                UserId = flightModel.UserId
            };
        }

        public static Flight ToFlightFromCreateDTO(this CreateFlightRequestDTO flightDto)
        {
            return new Flight
            {
                FlightId = flightDto.FlightId,
                FlightNo = flightDto.FlightNo,
                Departure = flightDto.Departure,
                Destination = flightDto.Destination,
                DepartureDate= flightDto.DepartureDate,
                Status = flightDto.Status,
                UserId = flightDto.UserId
            };
        }
    }
}
