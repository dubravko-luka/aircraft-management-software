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

        public static bool isQuit(int key)
        {
            if (key == Constants.KEY_QUIT)
            {
                return true;
            }

            return false;
        }
    }
}
