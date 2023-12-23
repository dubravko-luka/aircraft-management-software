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

        public static int MainMenu()
        {
            try
            {
                string message = "";
                int choice = -1;
                bool validInput = false;
                do
                {
                    PrintCenteredMenu("MENU", Config.MAIN_MENU_LIST.ToArray(), message);
                    string userInput = Console.ReadLine();

                    if (Common.IsNumeric(userInput))
                    {
                        choice = int.Parse(userInput);

                        if (Common.checkIsBack(choice))
                        {
                            validInput = true;
                        }
                        else
                        {
                            if (CheckValidSelectMenu(choice, Config.MAIN_MENU_LIST))
                            {
                                validInput = true;
                            }
                            else
                            {
                                message = "Lua chon khong hop le";
                                validInput = false;
                            }
                        }
                    }
                    else
                    {
                        message = "Lua chon khong hop le";
                        validInput = false;
                    }

                } while (!validInput);

                return choice;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static int AirplaneMenu()
        {
            try
            {
                string message = "";
                int choice = -1;
                bool validInput = false;
                do
                {
                    PrintCenteredMenu("Quan ly may bay", Config.AIRPLANE_MENU_LIST.ToArray(), message);
                    string userInput = Console.ReadLine();

                    if (Common.IsNumeric(userInput))
                    {
                        choice = int.Parse(userInput);

                        if (Common.checkIsBack(choice))
                        {
                            validInput = true;
                        }
                        else
                        {
                            if (CheckValidSelectMenu(choice, Config.AIRPLANE_MENU_LIST))
                            {
                                validInput = true;
                            }
                            else
                            {
                                message = "Lua chon khong hop le";
                                validInput = false;
                            }
                        }
                    }
                    else
                    {
                        message = "Lua chon khong hop le";
                        validInput = false;
                    }

                } while (!validInput);

                return choice;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static int CustomerMenu()
        {
            try
            {
                string message = "";
                int choice = -1;
                bool validInput = false;
                do
                {
                    PrintCenteredMenu("Quan ly khach hang", Config.CUSTOMER_MENU_LIST.ToArray(), message);
                    string userInput = Console.ReadLine();

                    if (Common.IsNumeric(userInput))
                    {
                        choice = int.Parse(userInput);

                        if (Common.checkIsBack(choice))
                        {
                            validInput = true;
                        }
                        else
                        {
                            if (CheckValidSelectMenu(choice, Config.CUSTOMER_MENU_LIST))
                            {
                                validInput = true;
                            }
                            else
                            {
                                message = "Lua chon khong hop le";
                                validInput = false;
                            }
                        }
                    }
                    else
                    {
                        message = "Lua chon khong hop le";
                        validInput = false;
                    }

                } while (!validInput);

                return choice;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static int FlightMenu()
        {
            try
            {
                string message = "";
                int choice = -1;
                bool validInput = false;
                do
                {
                    PrintCenteredMenu("Quan ly chuyen bay", Config.FLIGHT_MENU_LIST.ToArray(), message);
                    string userInput = Console.ReadLine();

                    if (Common.IsNumeric(userInput))
                    {
                        choice = int.Parse(userInput);

                        if (Common.checkIsBack(choice))
                        {
                            validInput = true;
                        }
                        else
                        {
                            if (CheckValidSelectMenu(choice, Config.FLIGHT_MENU_LIST))
                            {
                                validInput = true;
                            }
                            else
                            {
                                message = "Lua chon khong hop le";
                                validInput = false;
                            }
                        }
                    }
                    else
                    {
                        message = "Lua chon khong hop le";
                        validInput = false;
                    }

                } while (!validInput);

                return choice;

            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static int TicketMenu()
        {
            try
            {
                string message = "";
                int choice = -1;
                bool validInput = false;
                do
                {
                    PrintCenteredMenu("Quan ly ve", Config.TICKET_MENU_LIST.ToArray(), message);
                    string userInput = Console.ReadLine();

                    if (Common.IsNumeric(userInput))
                    {
                        choice = int.Parse(userInput);

                        if (Common.checkIsBack(choice))
                        {
                            validInput = true;
                        }
                        else
                        {
                            if (CheckValidSelectMenu(choice, Config.TICKET_MENU_LIST))
                            {
                                validInput = true;
                            }
                            else
                            {
                                message = "Lua chon khong hop le";
                                validInput = false;
                            }
                        }
                    }
                    else
                    {
                        message = "Lua chon khong hop le";
                        validInput = false;
                    }

                } while (!validInput);

                return choice;
            }
            catch (Exception ex)
            {
                return 0;
            }
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
