using System;
using aircraft.Components;
using aircraft.Helpers;
using aircraft.Services;

namespace aircraft.Routers
{
    public class router
    {

        public router ()
        {
            new Components.Menu.Menu();
        }

        public static void DirectionMainMenu()
        {
            while (true)
            {
                int choice = Components.Menu.Menu.MainMenu();
                Console.Clear();
                switch (choice)
                {
                    case 1:
                        DirectionTicketMenu();
                        break;
                    case 2:
                        DirectionFlightMenu();
                        break;
                    case 3:
                        DirectionAirplaneMenu();
                        break;
                    case 4:
                        DirectionCustomerMenu();
                        break;
                    case 0:
                        Common.checkIsQuit(0);
                        break;
                    default:
                        break;
                }
            }
        }

        public static void DirectionTicketMenu()
        {
            int choice = 0;

            do
            {
                choice = Components.Menu.Menu.TicketMenu();
                Console.Clear();
                switch (choice)
                {
                    case 1:
                        Services.Ticket.TicketManagement.BookTicket();
                        break;
                    case 2:
                        Services.Ticket.TicketManagement.DetailTicket();
                        break;
                    case 3:
                        Services.Ticket.TicketManagement.TicketList();
                        break;
                    case 4:
                        Services.Ticket.TicketManagement.CancelTicket();
                        break;
                    default:
                        break;
                }
            } while (choice != 0);
        }

        public static void DirectionFlightMenu()
        {
            int choice = 0;

            do
            {
                choice = Components.Menu.Menu.FlightMenu();
                Console.Clear();
                switch (choice)
                {
                    case 1:
                        Services.Airplane.FlightManagement.GetFlightList();
                        break;
                    case 2:
                        Services.Airplane.FlightManagement.GetFlightDetails();
                        break;
                    case 3:
                        Services.Airplane.FlightManagement.FlightCancel();
                        break;
                    case 4:
                        Services.Airplane.FlightManagement.FlightFinal();
                        break;
                    default:
                        break;
                }
            } while (choice != 0);
        }

        public static void DirectionAirplaneMenu()
        {
            int choice = 0;

            do
            {
                choice = Components.Menu.Menu.AirplaneMenu();
                Console.Clear();
                switch (choice)
                {
                    case 1:
                        Services.Airplane.AirplaneManagement.GetAirplaneList();
                        break;
                    case 2:
                        Services.Airplane.AirplaneManagement.GetAirplaneDetails();
                        break;
                    default:
                        break;
                }
            } while (choice != 0);
        }

        public static void DirectionCustomerMenu()
        {
            int choice = 0;

            do
            {
                choice = Components.Menu.Menu.CustomerMenu();
                Console.Clear();
                switch (choice)
                {
                    case 1:
                        Services.Customer.CustomerManagement.GetCustomerList();
                        break;
                    case 2:
                        Services.Customer.CustomerManagement.GetCustomerDetails();
                        break;
                    default:
                        break;
                }
            } while (choice != 0);
        }
    }
}
