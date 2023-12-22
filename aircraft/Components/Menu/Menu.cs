using System;
using System.Collections.Generic;
using System.Linq;
using aircraft.Routers;
using aircraft.Helpers;
namespace aircraft.Components.Menu
{
    public class Menu
    {
        public Menu ()
        {
            new Config();
        }

        public static int MainMenu(string message = "")
        {
            PrintCenteredMenu("MENU", Config.MAIN_MENU_LIST.ToArray(), message);
            string userInput = Console.ReadLine();
            if (Common.IsNumeric(userInput))
            {
                int choice = int.Parse(userInput);

                if (!Common.checkIsQuit(choice))
                {
                    if (CheckValidSelectMenu(choice, Config.MAIN_MENU_LIST))
                    {
                        return choice;
                    }
                    else
                    {
                        MainMenu("Lua chon khong hop le");
                    }
                }
            }
            else
            {
                MainMenu("Lua chon khong hop le");
            }
            return 1;
        }

        public static int AirplaneMenu(string message = "")
        {
            PrintCenteredMenu("Quan ly may bay", Config.AIRPLANE_MENU_LIST.ToArray(), message);
            string userInput = Console.ReadLine();
            if (Common.IsNumeric(userInput))
            {
                int choice = int.Parse(userInput);

                if (Common.checkIsBack(choice))
                {
                    return 0;
                } else
                {
                    if (CheckValidSelectMenu(choice, Config.AIRPLANE_MENU_LIST))
                    {
                        return choice;
                    }
                    else
                    {
                        AirplaneMenu("Lua chon khong hop le");
                    }
                }
            }
            else
            {
                AirplaneMenu("Lua chon khong hop le");
            }
            return 0;
        }

        public static int CustomerMenu(string message = "")
        {
            PrintCenteredMenu("Quan ly khach hang", Config.CUSTOMER_MENU_LIST.ToArray(), message);
            string userInput = Console.ReadLine();
            if (Common.IsNumeric(userInput))
            {
                int choice = int.Parse(userInput);

                if (Common.checkIsBack(choice))
                {
                    return 0;
                }
                else
                {
                    if (CheckValidSelectMenu(choice, Config.CUSTOMER_MENU_LIST))
                    {
                        return choice;
                    }
                    else
                    {
                        CustomerMenu("Lua chon khong hop le");
                    }
                }
            }
            else
            {
                CustomerMenu("Lua chon khong hop le");
            }
            return 0;
        }

        public static int FlightMenu(string message = "")
        {
            PrintCenteredMenu("Quan ly chuyen bay", Config.FLIGHT_MENU_LIST.ToArray(), message);
            string userInput = Console.ReadLine();
            if (Common.IsNumeric(userInput))
            {
                int choice = int.Parse(userInput);

                if (Common.checkIsBack(choice))
                {
                    return 0;
                } else
                {
                    if (CheckValidSelectMenu(choice, Config.FLIGHT_MENU_LIST))
                    {
                        return choice;
                    }
                    else
                    {
                        FlightMenu("Lua chon khong hop le");
                    }
                }
            }
            else
            {
                FlightMenu("Lua chon khong hop le");
            }
            return 0;
        }

        public static int TicketMenu(string message = "")
        {
            PrintCenteredMenu("Quan ly ve", Config.TICKET_MENU_LIST.ToArray(), message);
            string userInput = Console.ReadLine();
            if (Common.IsNumeric(userInput))
            {
                int choice = int.Parse(userInput);

                if (Common.checkIsBack(choice))
                {
                    return 0;
                }
                else
                {
                    if (CheckValidSelectMenu(choice, Config.TICKET_MENU_LIST))
                    {
                        return choice;
                    }
                    else
                    {
                        TicketMenu("Lua chon khong hop le");
                    }
                }
            }
            else
            {
                TicketMenu("Lua chon khong hop le");
            }
            return 0;
        }

        static void PrintCenteredMenu(string title, string[] options, string message = "")
        {
            Console.Clear();

            int windowHeight = Console.WindowHeight;
            int windowWidth = Console.WindowWidth;

            int titleTop = windowHeight / 2 - (options.Length + 3) / 2;

            string line = "+------------------------------------------------+";

            int linePadLeft = (windowWidth - line.Length) / 2;

            Console.SetCursorPosition(linePadLeft, titleTop - 1);
            Console.WriteLine(line);

            Console.SetCursorPosition(linePadLeft, titleTop);
            Console.WriteLine("|");
            Console.SetCursorPosition((windowWidth - title.Length) / 2, titleTop);
            Console.WriteLine(title);
            Console.SetCursorPosition(linePadLeft + (line.Length - 1), titleTop);
            Console.WriteLine("|");

            Console.SetCursorPosition(linePadLeft, titleTop + 1);
            Console.WriteLine(line);

            int maxLength = Common.GetMaxLengthString(options);

            foreach (var option in options)
            {
                Console.SetCursorPosition(linePadLeft, Console.CursorTop);
                Console.WriteLine(PadSidesMenu($"{option}"));
                Console.SetCursorPosition(linePadLeft, Console.CursorTop);
                Console.WriteLine(line);
            }

            if (message != "")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition((windowWidth - maxLength) / 2, Console.CursorTop + 1);
                Console.Write(message);
                Console.ResetColor();
            }

            Console.SetCursorPosition((windowWidth - maxLength) / 2, Console.CursorTop + (message != "" ? 2 : 1));
            Console.Write("Chon chuc nang: ");
        }

        private static string PadSidesMenu(string str)
        {
            str = str.PadRight(25, ' ');
            return $"|{Common.padSides(str, 48)}|";
        }

        public static bool CheckValidSelectMenu(int key, List<string> list)
        {
            if (key >= 0 && key <= list.Count() - 1)
            {
                return true;
            }
            return false;
        }
    }
}
