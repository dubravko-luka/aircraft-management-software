using System;
using System.Collections.Generic;
using System.IO;
using aircraft.Models;
using aircraft.Helpers;
using System.Linq;
using System.Threading;

namespace aircraft.Services.Airplane
{
    public class FlightManagement
    {
        public const string filePath = Databases.Airplanes.Flight.filePath;

        public static void GetFlightList()
        {
            _GetFlightList();

            Common.printStringCenterAfterNoBreak("An phim bat ki de tro ve!");
            Console.ReadKey();
        }

        public static void _GetFlightList(string[] headers = null)
        {
            List<Flight> flights = __GetFlightList();

            if (flights != null && flights.Count > 0)
            {

                if (headers == null)
                {
                    headers = new string[] { "Ma", "So hieu", "Ngay khoi hanh", "San bay den", "Trang thai", "So luong ve", "So ghe trong" };
                }

                string[,] rowData = new string[flights.Count, 7];

                var coloredCells = new List<Tuple<int, int, ConsoleColor>>();

                for (int i = 0; i < flights.Count; i++)
                {
                    rowData[i, 0] = flights[i].FlightCode;
                    rowData[i, 1] = flights[i].AircraftCode;
                    rowData[i, 2] = flights[i].DepartureDate;
                    rowData[i, 3] = flights[i].DestinationAirport;
                    rowData[i, 4] = Constants.STATUS_FLIGHT[flights[i].Status].ToString();
                    rowData[i, 5] = flights[i].TicketList.Length.ToString();
                    rowData[i, 6] = flights[i].EmptySeats.Length.ToString();

                    if (flights[i].Status == 0)
                    {
                        coloredCells.Add(Tuple.Create(i, 0, ConsoleColor.Red));
                        coloredCells.Add(Tuple.Create(i, 1, ConsoleColor.Red));
                        coloredCells.Add(Tuple.Create(i, 2, ConsoleColor.Red));
                        coloredCells.Add(Tuple.Create(i, 3, ConsoleColor.Red));
                        coloredCells.Add(Tuple.Create(i, 4, ConsoleColor.Red));
                        coloredCells.Add(Tuple.Create(i, 5, ConsoleColor.Red));
                        coloredCells.Add(Tuple.Create(i, 6, ConsoleColor.Red));
                    }

                    if (flights[i].Status == 1)
                    {
                        coloredCells.Add(Tuple.Create(i, 0, ConsoleColor.Green));
                        coloredCells.Add(Tuple.Create(i, 1, ConsoleColor.Green));
                        coloredCells.Add(Tuple.Create(i, 2, ConsoleColor.Green));
                        coloredCells.Add(Tuple.Create(i, 3, ConsoleColor.Green));
                        coloredCells.Add(Tuple.Create(i, 4, ConsoleColor.Green));
                        coloredCells.Add(Tuple.Create(i, 5, ConsoleColor.Green));
                        coloredCells.Add(Tuple.Create(i, 6, ConsoleColor.Green));
                    }

                    if (flights[i].Status == 2)
                    {
                        coloredCells.Add(Tuple.Create(i, 0, ConsoleColor.Yellow));
                        coloredCells.Add(Tuple.Create(i, 1, ConsoleColor.Yellow));
                        coloredCells.Add(Tuple.Create(i, 2, ConsoleColor.Yellow));
                        coloredCells.Add(Tuple.Create(i, 3, ConsoleColor.Yellow));
                        coloredCells.Add(Tuple.Create(i, 4, ConsoleColor.Yellow));
                        coloredCells.Add(Tuple.Create(i, 5, ConsoleColor.Yellow));
                        coloredCells.Add(Tuple.Create(i, 6, ConsoleColor.Yellow));
                    }

                    if (flights[i].Status == 3)
                    {
                        coloredCells.Add(Tuple.Create(i, 0, ConsoleColor.Blue));
                        coloredCells.Add(Tuple.Create(i, 1, ConsoleColor.Blue));
                        coloredCells.Add(Tuple.Create(i, 2, ConsoleColor.Blue));
                        coloredCells.Add(Tuple.Create(i, 3, ConsoleColor.Blue));
                        coloredCells.Add(Tuple.Create(i, 4, ConsoleColor.Blue));
                        coloredCells.Add(Tuple.Create(i, 5, ConsoleColor.Blue));
                        coloredCells.Add(Tuple.Create(i, 6, ConsoleColor.Blue));
                    }
                }

                Common.DrawTable(headers, rowData, 24, coloredCells.ToArray());
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
                        $"Trang thai chuyen bay: {Constants.STATUS_FLIGHT[flight.Status]}",
                        $"So luong ghe: {seatCount}",
                    };
                    Common.PrintCentered($"Thong tin chi tiet chuyen bay so hieu: {code}", data, "", 3);
                    int[] bookedSeats = Array.ConvertAll(flight.TicketList, int.Parse);
                    DrawTableSeat(seatCount, bookedSeats);
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

        public static Models.Flight _GetFlightDetails(string code)
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

        public static void DrawTableSeat(int seatCount = 100, int[] bookedSeats = null)
        {
            string[] headers = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O" };

            int rows = (int)Math.Ceiling((double)seatCount / 15);

            string[,] rowData = new string[rows, headers.Length];

            var coloredCells = new List<Tuple<int, int, ConsoleColor>>();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < headers.Length; j++)
                {
                    int seatNumber = (i * headers.Length) + j + 1;
                    if (seatNumber <= seatCount)
                    {
                        rowData[i, j] = seatNumber.ToString();
                        if (Array.IndexOf(bookedSeats, seatNumber) != -1)
                        {
                            coloredCells.Add(Tuple.Create(i, j, ConsoleColor.Red));
                        }
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

            string selled = "Ghe khong co san   ";
            string available = "Ghe co san";
            Console.SetCursorPosition((windowWidth - (selled.Length + available.Length)) / 2, Console.CursorTop + 1);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(selled);
            Console.ResetColor();
            Console.WriteLine(available);
            Console.WriteLine(" ");

            Common.DrawTable(headers, rowData, 7, coloredCells.ToArray());
        }

        public static void FlightCancel()
        {
            bool continute = true;

            do
            {
                Console.Clear();
                Airplane.FlightManagement._GetFlightList();
                Common.printStringCenterAfterNoBreak("Moi nhap ma chuyen bay: ");

                string flightCode = Common.InputString();
                if (flightCode == Constants.EscapeString)
                {
                    break;
                }

                Models.Flight flight = Airplane.FlightManagement._GetFlightDetails(flightCode);

                bool flightCodeValid = true;

                if (flight == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Common.printStringCenterAfter($"Khong tim thay du lieu!", 0);
                    Console.ResetColor();
                    flightCodeValid = false;
                }

                if (flight.Status == Constants.STATUS_FLIGHT_CANCELED)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Common.printStringCenterAfter($"Chuyen bay da huy!", 0);
                    Console.ResetColor();
                    flightCodeValid = false;
                }

                if (flight.Status == Constants.STATUS_FLIGHT_COMPLETED)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Common.printStringCenterAfter($"Chuyen bay da hoan thanh!", 0);
                    Console.ResetColor();
                    flightCodeValid = false;
                }

                if (flightCodeValid)
                {
                    if (Common.DoYouWantContinute("Ban co chac chan muon cap nhat (y/n): "))
                    {
                        string filePath = Databases.Airplanes.Flight.filePath;
                        string[] lines = File.ReadAllLines(filePath);

                        for (int i = 0; i < lines.Length; i++)
                        {
                            string[] data = lines[i].Split(',');

                            if (data[0] == flight.FlightCode)
                            {
                                data[4] = Constants.STATUS_FLIGHT_CANCELED.ToString();

                                lines[i] = string.Join(",", data);
                                break;
                            }
                        }

                        File.WriteAllLines(filePath, lines);

                        Console.Clear();
                        Airplane.FlightManagement._GetFlightList();
                        Common.printStringCenterAfter("Cap nhat thanh cong!");
                    }
                    else
                    {
                        Console.Clear();
                        Common.printStringCenterAfter("Da tu choi cap nhat! Xin cam on!");
                    }
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

        public static void FlightFinal()
        {
            bool continute = true;

            do
            {
                Console.Clear();
                Airplane.FlightManagement._GetFlightList();
                Common.printStringCenterAfterNoBreak("Moi nhap ma chuyen bay: ");

                string flightCode = Common.InputString();

                if (flightCode == Constants.EscapeString)
                {
                    break;
                }

                Models.Flight flight = Airplane.FlightManagement._GetFlightDetails(flightCode);

                bool flightCodeValid = true;

                if (flight == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Common.printStringCenterAfter($"Khong tim thay du lieu!", 0);
                    Console.ResetColor();
                    flightCodeValid = false;
                }

                if (flight.Status == Constants.STATUS_FLIGHT_CANCELED)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Common.printStringCenterAfter($"Chuyen bay da bi huy!", 0);
                    Console.ResetColor();
                    flightCodeValid = false;
                }

                if (flight.Status == Constants.STATUS_FLIGHT_COMPLETED)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Common.printStringCenterAfter($"Chuyen bay da hoan thanh!", 0);
                    Console.ResetColor();
                    flightCodeValid = false;
                }

                if (flightCodeValid)
                {
                    if (Common.DoYouWantContinute("Ban co chac chan muon cap nhat (y/n): "))
                    {
                        string filePath = Databases.Airplanes.Flight.filePath;
                        string[] lines = File.ReadAllLines(filePath);

                        for (int i = 0; i < lines.Length; i++)
                        {
                            string[] data = lines[i].Split(',');

                            if (data[0] == flight.FlightCode)
                            {
                                data[4] = Constants.STATUS_FLIGHT_COMPLETED.ToString();

                                lines[i] = string.Join(",", data);
                                break;
                            }
                        }

                        File.WriteAllLines(filePath, lines);

                        Console.Clear();
                        Airplane.FlightManagement._GetFlightList();
                        Common.printStringCenterAfter("Cap nhat thanh cong!");
                    }
                    else
                    {
                        Console.Clear();
                        Common.printStringCenterAfter("Da tu choi cap nhat! Xin cam on!");
                    }
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
    }
}
