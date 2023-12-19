using System;
using aircraft.Models;
using aircraft.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aircraft.Services.Airplane
{
    public class AirplaneManagement
    {
        private const string filePath = "Airplanes.txt";

        public static void GetAirplaneList()
        {

            _GetAirplaneList();
            Common.printStringCenterAfterNoBreak("An phim bat ki de tro ve!");
            Console.ReadKey();
        }

        private static void _GetAirplaneList()
        {
            List<Models.Airplane> airplaneList = __GetAirplaneList();

            if (airplaneList != null && airplaneList.Count > 0)
            {
                string[] headers = { "So hieu may bay", "So cho ngoi" };
                string[,] rowData = new string[airplaneList.Count, 2];

                for (int i = 0; i < airplaneList.Count; i++)
                {
                    rowData[i, 0] = airplaneList[i].PlaneCode;
                    rowData[i, 1] = airplaneList[i].SeatCount.ToString();
                }

                DrawTable(headers, rowData);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Khong co du lieu may bay.");
                Console.ResetColor();
            }
        }

        private static List<Models.Airplane> __GetAirplaneList()
        {
            List<Models.Airplane> airplaneList = new List<Models.Airplane>();

            try
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] items = line.Split(',');
                    if (items.Length == 2)
                    {
                        Models.Airplane airplane = new Models.Airplane
                        {
                            PlaneCode = items[0],
                            SeatCount = int.Parse(items[1])
                        };
                        airplaneList.Add(airplane);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Airplanes.txt not found");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return airplaneList;
        }

        public static void GetAirplaneDetails()
        {
            bool continute = true;
            int windowHeight = Console.WindowHeight;
            int windowWidth = Console.WindowWidth;

            do
            {
                Console.Clear();
                _GetAirplaneList();
                Common.printStringCenterAfterNoBreak("Nhap ma may bay: ");
                string code = Console.ReadLine();
                Models.Airplane airplane = _GetAirplaneDetails(code);

                Console.Clear();
                Console.SetCursorPosition(windowWidth / 2, windowHeight / 2);
                Console.WriteLine(" ");

                if (airplane != null)
                {
                    string[] data = { $"So hieu may bay: {airplane.PlaneCode}", $"So cho ngoi: {airplane.SeatCount}" };
                    Common.PrintCentered($"Thong tin chi tiet may bay ma so {code}", data);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Common.printStringCenterAfter($"Khong tim thay du lieu!", 0);
                    Console.ResetColor();
                }
                bool inpubtValid = false;
                do
                {
                    Common.printStringCenterAfterNoBreak($"Tiep tuc (y/n): ", 2);
                    string choice = Console.ReadLine();
                    if (choice == "Y" || choice == "y")
                    {
                        inpubtValid = true;
                    }
                    if (choice == "N" || choice == "n")
                    {
                        inpubtValid = true;
                        continute = false;
                    }
                } while (!inpubtValid);

            } while (continute);
        }

        private static Models.Airplane _GetAirplaneDetails(string code)
        {
            List<Models.Airplane> airplaneList = __GetAirplaneList();
            Models.Airplane airplane = airplaneList.FirstOrDefault(ap => ap.PlaneCode == code);
            return airplane;
        }

        static void DrawTable(string[] headers, string[,] rowData)
        {
            int columns = headers.Length;
            int rows = rowData.GetLength(0);
            int rowLength = (columns * 24) + (columns + 1);

            string horizontalLine = "+" + new string('-', rowLength - 2) + "+";
            string headerRow = "|";

            foreach (string header in headers)
            {
                headerRow += Common.padSides(header, 24) + "|";
            }

            Common.printStringCenterAfter(horizontalLine, 0);
            Common.printStringCenterAfter(headerRow, 0);
            Common.printStringCenterAfter(horizontalLine, 0);

            for (int i = 0; i < rows; i++)
            {
                string dataRow = "|";
                for (int j = 0; j < columns; j++)
                {
                    dataRow += Common.padSides(rowData[i, j], 24) + "|";
                }
                Common.printStringCenterAfter(dataRow, 0);
                Common.printStringCenterAfter(horizontalLine, 0);
            }
        }
    }
}
