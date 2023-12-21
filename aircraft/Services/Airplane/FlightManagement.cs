using System;
using System.Collections.Generic;
using System.IO;
using aircraft.Models;
using aircraft.Helpers;
using System.Linq;

namespace aircraft.Services.Airplane
{
    public class FlightManagement
    {
        private const string filePath = Databases.Airplanes.Flight.filePath;

        public static void GetFlightList()
        {
            _GetFlightList();

            Common.printStringCenterAfterNoBreak("An phim bat ki de tro ve!");
            Console.ReadKey();
        }

        private static void _GetFlightList(string[] headers = null)
        {
            List<Flight> flights = __GetFlightList();

            if (flights != null && flights.Count > 0)
            {

                if (headers == null)
                {
                    headers = new string[] { "Ma", "So hieu", "Ngay khoi hanh", "San bay den", "Trang thai", "So luong ve", "So ghe trong" };
                }

                string[,] rowData = new string[flights.Count, 7];

                for (int i = 0; i < flights.Count; i++)
                {
                    rowData[i, 0] = flights[i].FlightCode;
                    rowData[i, 1] = flights[i].AircraftCode;
                    rowData[i, 2] = flights[i].DepartureDate;
                    rowData[i, 3] = flights[i].DestinationAirport;
                    rowData[i, 4] = flights[i].Status.ToString();
                    rowData[i, 5] = flights[i].TicketList.Length.ToString();
                    rowData[i, 6] = flights[i].EmptySeats.Length.ToString();
                }

                Common.DrawTable(headers, rowData);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Khong co du lieu may bay.");
                Console.ResetColor();
            }
        }

        public static List<Flight> __GetFlightList()
        {
            List<Flight> flights = new List<Flight>();

            try
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    string[] data = line.Split(',');

                    Flight flight = new Flight
                    {
                        FlightCode = data[0],
                        AircraftCode = data[1],
                        DepartureDate = data[2],
                        DestinationAirport = data[3],
                        Status = int.Parse(data[4]),
                        TicketList = !string.IsNullOrEmpty(data[5]) ? data[5].Split(';') : new string[0],
                        EmptySeats = !string.IsNullOrEmpty(data[6]) ? Array.ConvertAll(data[6].Split(';'), int.Parse) : new int[0]
                    };

                    flights.Add(flight);
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Loi he thong!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Loi he thong: {ex.Message}");
            }

            return flights;
        }

        public static void GetFlightDetails()
        {
            bool continute = true;
            int windowHeight = Console.WindowHeight;
            int windowWidth = Console.WindowWidth;

            do
            {
                Console.Clear();
                _GetFlightList(new string[] { "Ma", "So hieu", "Ngay khoi hanh", "San bay den", "Trang thai" });
                Common.printStringCenterAfterNoBreak("Nhap ma chuyen bay: ");
                string code = Console.ReadLine();
                Models.Flight flight = _GetFlightDetails(code);

                Console.Clear();
                Console.SetCursorPosition(windowWidth / 2, 0);
                Console.WriteLine(" ");

                if (flight != null)
                {
                    int seatCount = GetSeatCount(flight.AircraftCode);

                    string[] data = {
                        $"Ma chuyen bay: {flight.FlightCode}",
                        $"So hieu may bay: {flight.AircraftCode}",
                        $"Ngay khoi hanh: {flight.DepartureDate}",
                        $"San bay den: {flight.DestinationAirport}",
                        $"Trang thai chuyen bay: {flight.Status}",
                        $"So luong ghe: {seatCount}",
                    };
                    Common.PrintCentered($"Thong tin chi tiet may bay ma so {code}", data, "", 3);
                    //DisplayTicketList(flight.TicketList);
                    DrawTable(seatCount);

                    //if (!string.IsNullOrEmpty(flight.EmptySeats))
                    //{
                    //List<string[]> ticketChunks = SplitList(flight.TicketList);
                    //for (int index = 0; index < ticketChunks.Count; index++)
                    //{
                    //    var ticketChunk = ticketChunks[index];

                    //    string[,] rowData = new string[ticketChunk.Length, 10];

                    //    rowData[index, 0] = "1";
                    //    rowData[index, 1] = "1";
                    //    rowData[index, 2] = "1";
                    //    rowData[index, 3] = "1";
                    //    rowData[index, 4] = "1";
                    //    rowData[index, 5] = "1";
                    //    rowData[index, 6] = "1";
                    //    rowData[index, 7] = "1";
                    //    rowData[index, 8] = "1";
                    //    rowData[index, 9] = "1";

                    //    Common.DrawTableNoHeader(rowData, 9);
                    //}
                    //}

                    //if (!string.IsNullOrEmpty(flight.EmptySeats))
                    //{
                    //    string[,] emptySeatChunks = SplitList(flight.EmptySeats);
                    //    Common.DrawTableNoHeader(emptySeatChunks);
                    //}

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

        public static void DisplayTicketList(string[] ticketList)
        {
            const int columnCount = 10;
            int ticketCount = ticketList.Length;
            int rowCount = (int)Math.Ceiling((double)ticketCount / columnCount);

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    int index = i * columnCount + j;
                    if (index < ticketCount)
                    {
                        Console.Write($"|{ticketList[index],-6}");
                    }
                    else
                    {
                        Console.Write($"|{' ',-6}");
                    }
                }
                Console.WriteLine("|");
            }
        }

        private static Models.Flight _GetFlightDetails(string code)
        {
            List<Models.Flight> flightList = __GetFlightList();
            Models.Flight flight = flightList.FirstOrDefault(ap => ap.FlightCode == code);
            return flight;
        }

        public static int GetSeatCount(string aircraftCode)
        {
            string[] lines = File.ReadAllLines("Airplanes.txt");

            foreach (string line in lines)
            {
                string[] data = line.Split(',');

                if (data.Length >= 2 && data[0].Trim() == aircraftCode)
                {
                    if (int.TryParse(data[1].Trim(), out int seatCount))
                    {
                        return seatCount;
                    }
                }
            }
            return 0;
        }

        public static void DrawTable(int seatCount = 100)
        {
            string[] headers = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O" };

            int rows = (int)Math.Ceiling((double)seatCount / 15);

            string[,] rowData = new string[rows, headers.Length];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < headers.Length; j++)
                {
                    int seatNumber = (i * headers.Length) + j + 1;
                    if (seatNumber <= seatCount)
                    {
                        rowData[i, j] = seatNumber.ToString();
                    }
                    else
                    {
                        rowData[i, j] = " ";
                    }
                }
            }


            int windowWidth = Console.WindowWidth;
            string title = "Danh sach ghe ngoi";
            Console.SetCursorPosition((windowWidth - title.Length) / 2, Console.CursorTop + 1);
            Console.WriteLine(title);

            Common.DrawTable(headers, rowData, 7);
        }
    }
}
