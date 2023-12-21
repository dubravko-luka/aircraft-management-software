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
                        //vehicleService.create();
                        break;
                    case 2:
                        //vehicleService.delete();
                        break;
                    case 3:
                        DirectionFlightMenu();
                        break;
                    case 4:
                        DirectionAirplaneMenu();
                        break;
                    case 5:
                        //vehicleService.edit();
                        break;
                    case 6:
                        //vehicleService.read();
                        break;
                    default:
                        break;
                }
            }
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
                        Services.Airplane.FlightManagement.GetFlightList(); ;
                        break;
                    case 2:
                        Services.Airplane.FlightManagement.GetFlightDetails();
                        break;
                    case 3:
                        //Services.Airplane.AirplaneManagement.GetAirplaneDetails();
                        break;
                    case 4:
                        //Services.Airplane.AirplaneManagement.GetAirplaneDetails();
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
    }
}
