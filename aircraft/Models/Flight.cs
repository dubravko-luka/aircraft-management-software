using System;
namespace aircraft.Models
{
    public class Flight
    {
        public string FlightCode { get; set; }
        public string AircraftCode { get; set; }
        public string DepartureDate { get; set; }
        public string DestinationAirport { get; set; }
        public int Status { get; set; }
        public string[] TicketList { get; set; }
        public int[] EmptySeats { get; set; }
    }
}
