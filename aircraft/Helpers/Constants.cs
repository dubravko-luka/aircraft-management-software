using System;
namespace aircraft.Helpers
{
    public class Constants
    {
        public static int KEY_QUIT = 0;
        public static int STATUS_FLIGHT_CANCELED = 0;
        public static int STATUS_FLIGHT_AVAILABLE = 1;
        public static int STATUS_FLIGHT_SOLD_OUT = 2;
        public static int STATUS_FLIGHT_COMPLETED = 3;
        public static string[] STATUS_FLIGHT = { "Canceled", "Available", "Sold out", "Completed" };
    }
}
