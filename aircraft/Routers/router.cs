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
                        //vehicleService.edit();
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
                //Console.ForegroundColor = ConsoleColor.Red;
                //Common.printStringCenterAfterNoBreak("An phim bat ki de tiep tuc!");
                //Console.ResetColor();
                //Console.ReadKey();
                //Console.Clear();
            } while (choice != 0);
        }
    }
}
