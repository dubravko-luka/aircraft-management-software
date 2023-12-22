using System;

namespace aircraft.Models
{
    public class Ticket
    {
        public string TicketCode { get; set; }
        public string FlightCode { get; set; }
        public Customer CustomerInfo { get; set; }
        public int SeatNumber { get; set; }
    }
}
