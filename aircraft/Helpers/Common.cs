using System;
using System.Threading;

namespace aircraft.Helpers
{
    public class Common
    {
        public static void PrintCenteredText(string text, int height = 10)
        {
            int width = text.Length;
            int left = (Console.WindowWidth - width) / 2;
            int top = (Console.WindowHeight - height) / 2;

            Console.SetCursorPosition(left, top);
            Console.WriteLine(text);
        }

        public static void PrintCenteredTextNoBreak(string text, int height = 10)
        {
            int width = text.Length;
            int left = (Console.WindowWidth - width) / 2;
            int top = (Console.WindowHeight - height) / 2;

            Console.SetCursorPosition(left, top);
            Console.Write(text);
        }

        public static string padSides(string str, int length)
        {
            int spaces = length - str.Length;
            int padLeft = spaces / 2 + str.Length;
            return str.PadLeft(padLeft).PadRight(length);
        }

        public static bool IsNumeric(string input)
        {
            return int.TryParse(input, out _);
        }

        public static int GetMaxLengthString(string[] options)
        {
            int maxLength = 0;
            foreach (var option in options)
            {
                if (option.Length > maxLength)
                {
                    maxLength = option.Length;
                }
            }

            return maxLength;
        }

        public static bool checkIsQuit(int key)
        {
            if (isQuit(key))
            {
                Console.Clear();
                PrintCenteredText("Tam biet, hen gap lai!", 0);
                Thread.Sleep(2000);
                Environment.Exit(0);
                return true;
            }

            return false;
        }

        public static bool checkIsBack(int key)
        {
            if (isQuit(key))
            {
                return true;
            }

            return false;
        }

        public static bool isQuit(int key)
        {
            if (key == Constants.KEY_QUIT)
            {
                return true;
            }

            return false;
        }

        public static void printStringCenterAfterNoBreak (string str, int paddingTop = 2)
        {
            int windowWidth = Console.WindowWidth;
            Console.SetCursorPosition((windowWidth - str.Length) / 2, Console.CursorTop + paddingTop);
            Console.Write(str);
        }

        public static void printStringCenterAfter(string str, int paddingTop = 2)
        {
            int windowWidth = Console.WindowWidth;
            Console.SetCursorPosition((windowWidth - str.Length) / 2, Console.CursorTop + paddingTop);
            Console.WriteLine(str);
        }

        public static void PrintCentered(string title, string[] options, string message = "")
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
        }

        private static string PadSidesMenu(string str)
        {
            str = str.PadRight(25, ' ');
            return $"|{padSides(str, 48)}|";
        }
    }
}
