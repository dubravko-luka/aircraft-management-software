using System;
using System.Collections.Generic;
using System.Linq;
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

        public static void PrintCentered(string title, string[] options, string message = "", int top = 0)
        {
            Console.Clear();

            int windowHeight = Console.WindowHeight;
            int windowWidth = Console.WindowWidth;

            int titleTop = top > 0 ? top : windowHeight / 2 - (options.Length + 3) / 2;

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

        public static void DrawTable(string[] headers, string[,] rowData, int padSidesLength = 24, Tuple<int, int, ConsoleColor>[] coloredCells = null)
        {
            int columns = headers.Length;
            int windowWidth = Console.WindowWidth;
            int rows = rowData.GetLength(0);
            int rowLength = (columns * padSidesLength) + (columns + 1);

            string horizontalLine = "+" + new string('-', rowLength - 2) + "+";
            string headerRow = "|";

            foreach (string header in headers)
            {
                headerRow += Common.padSides(header, padSidesLength) + "|";
            }

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Common.printStringCenterAfter(horizontalLine, 0);
            Common.printStringCenterAfter(headerRow, 0);
            Common.printStringCenterAfter(horizontalLine, 0);
            Console.ResetColor();

            for (int i = 0; i < rows; i++)
            {
                Console.SetCursorPosition((windowWidth - horizontalLine.Length) / 2, Console.CursorTop);
                Console.Write("|");
                for (int j = 0; j < columns; j++)
                {
                    bool isColored = coloredCells != null && coloredCells.Any(c => c.Item1 == i && c.Item2 == j);
                    if (isColored)
                    {
                        Tuple<int, int, ConsoleColor> cell = coloredCells.First(c => c.Item1 == i && c.Item2 == j);
                        ConsoleColor cellColor = cell.Item3;
                        Console.ForegroundColor = cellColor;
                    }

                    Console.Write($"{Common.padSides(rowData[i, j], padSidesLength)}");
                    Console.ResetColor();

                    if (j < columns - 1)
                    {
                        Console.Write("|");
                    } else
                    {
                        Console.WriteLine("|");
                    }

                }
                Common.printStringCenterAfter(horizontalLine, 0);
            }
        }
    }
}
