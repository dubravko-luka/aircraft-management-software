using System;
using aircraft.Components;
namespace aircraft.Routers
{
    public class router
    {

        public static void Direction()
        {
            new Components.Menu.Menu();
            int choice = Components.Menu.Menu.MainMenu();

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
}
