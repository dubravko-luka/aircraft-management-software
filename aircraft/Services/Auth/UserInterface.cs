using System;
namespace aircraft.Services.Auth
{
    public class UserInterface
    {
        public static void DrawInputField(int left, int top, int width, int height)
        {
            Console.SetCursorPosition(left, top);
            for (int i = 0; i < width; i++)
            {
                Console.Write("+");
            }

            for (int i = 1; i < height; i++)
            {
                Console.SetCursorPosition(left, top + i);
                Console.Write("+   ");
                Console.SetCursorPosition(left + width - 1, top + i);
                Console.Write("+");
            }

            Console.SetCursorPosition(left, top + height);
            for (int i = 0; i < width; i++)
            {
                Console.Write("+");
            }
        }

        public static string GetUsername(int left, int top)
        {
            Console.SetCursorPosition(left + 3, top + 1);
            return Console.ReadLine();
        }

        public static string GetMaskedPassword(int left, int top)
        {
            string password = "";
            Console.SetCursorPosition(left + 3, top + 1);
            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = Console.ReadKey(true);

                if (!char.IsControl(keyInfo.KeyChar))
                {
                    password += keyInfo.KeyChar;
                    Console.Write("*");
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, (password.Length - 1));
                    Console.Write("\b \b");
                }
            } while (keyInfo.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return password;
        }

        public static void DrawCenteredInputFields(int width, int height)
        {
            int left = (Console.WindowWidth - width) / 2;
            int top = (Console.WindowHeight - (2 * height + 1)) / 2;

            Console.SetCursorPosition(left, top);
            Console.WriteLine("Admin Login");

            DrawInputField(left, top, width, height);
            DrawInputField(left, top + height + 1, width, height);
        }
    }
}
