using System;
using System.IO;
using System.Linq;
using System.Threading;
using aircraft.Helpers;
using aircraft.Models;

namespace aircraft.Services.Ticket
{
    public class TicketManagement
    {
        public static void BookTicket()
        {
            string flightCode = "";
            string seatNum = "";
            string customerId = "";

            // Flight
            Airplane.FlightManagement._GetFlightList();
            Common.printStringCenterAfterNoBreak("Moi nhap ma chuyen bay: ");

            flightCode = Console.ReadLine();
            Models.Flight flight = Airplane.FlightManagement._GetFlightDetails(flightCode);

            while (flight == null)
            {
                if (flight == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Common.printStringCenterAfter($"Khong tim thay du lieu!", 0);
                    Console.ResetColor();
                }

                Common.printStringCenterAfterNoBreak("Moi nhap ma chuyen bay: ", 0);
                flightCode = Console.ReadLine();
                flight = Airplane.FlightManagement._GetFlightDetails(flightCode);
            }

            // Seat
            Console.Clear();
            int seatCount = Airplane.FlightManagement.GetSeatCount(flight.AircraftCode);
            int[] bookedSeats = Array.ConvertAll(flight.TicketList, int.Parse);

            Airplane.FlightManagement.DrawTableSeat(seatCount, bookedSeats);
            Common.printStringCenterAfter(" ", 2);
            bool validInput = false;
            do
            {
                Common.printStringCenterAfterNoBreak("Moi nhap so ghe trong: ", 0);
                seatNum = Console.ReadLine();

                if (Common.IsNumeric(seatNum))
                {
                    if (int.TryParse(seatNum, out int seatNumber))
                    {
                        if (seatNumber >= 1 && seatNumber <= seatCount && !bookedSeats.Contains(seatNumber))
                        {
                            validInput = true;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Common.printStringCenterAfter($"So ghe khong hop le hoac da co nguoi dat. Vui long nhap lai!", 0);
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Common.printStringCenterAfter($"Vui long nhap so ghe hop le!", 0);
                        Console.ResetColor();
                    }
                } else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Common.printStringCenterAfter($"Vui long nhap so ghe hop le!", 0);
                    Console.ResetColor();
                }

            } while (!validInput);

            // Customer
            Console.Clear();
            Customer.CustomerManagement._GetCustomerList();

            Common.printStringCenterAfterNoBreak("Moi nhap CMND khach hang: ");
            customerId = Console.ReadLine();
            Models.Customer customer = Customer.CustomerManagement._GetCustomerDetails(customerId);

            while (customer == null)
            {
                if (customer == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Common.printStringCenterAfter($"Khong tim thay du lieu!", 0);
                    Console.ResetColor();
                }

                Common.printStringCenterAfterNoBreak("Moi nhap CMND khach hang: ", 0);
                customerId = Console.ReadLine();
                customer = Customer.CustomerManagement._GetCustomerDetails(customerId);
            }

            string ticketCode = $"T{flightCode}-{customerId}-{seatNum}";

            Models.Ticket ticket = new Models.Ticket
            {
                TicketCode = ticketCode,
                FlightCode = flightCode,
                CustomerInfo = new Models.Customer { CMND = customerId, FullName = customer.FullName },
                SeatNumber = int.Parse(seatNum)
            };

            string fileName = $"{ticket.TicketCode}.txt";
            string ticketInfo = $"Ticket Code: {ticket.TicketCode}\n";
            ticketInfo += $"Flight Code: {ticket.FlightCode}\n";
            ticketInfo += $"Customer ID: {ticket.CustomerInfo.CMND}\n";
            ticketInfo += $"Customer Name: {ticket.CustomerInfo.FullName}\n";
            ticketInfo += $"Seat Number: {ticket.SeatNumber}";
            File.WriteAllText(fileName, ticketInfo);

            RemoveCustomerInfo(customerId);

            UpdateFlightSeats(flightCode, int.Parse(seatNum), bookedSeats.Length + 1 == seatCount ? Constants.STATUS_FLIGHT_SOLD_OUT : Constants.STATUS_FLIGHT_AVAILABLE);

            Console.Clear();
            Common.PrintCenteredText("Dat ve thanh cong! Xin cam on!");
            Thread.Sleep(500);
        }

        private static void RemoveCustomerInfo(string customerID)
        {
            const string filePath = Customer.CustomerManagement.filePath;
            var lines = File.ReadAllLines(filePath).ToList();
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].Contains(customerID))
                {
                    lines.RemoveAt(i);
                    break;
                }
            }
            File.WriteAllLines(filePath, lines);
        }

        private static void UpdateFlightSeats(string flightCode, int seatNumber, int status)
        {
            const string filePath = Airplane.FlightManagement.filePath;

            var lines = File.ReadAllLines(filePath).ToList();

            foreach (string line in lines)
            {
                string[] flightInfo = line.Split(',');
                if (flightInfo[0] == flightCode)
                {
                    var seatList = flightInfo[5].Split(';').ToList();

                    if (seatList[0] == "")
                    {
                        seatList[0] = seatNumber.ToString();
                    }
                    else
                    {
                        seatList.Add(seatNumber.ToString());
                    }

                    var emptySeats = flightInfo[6].Split(';').ToList();
                    emptySeats.Remove(seatNumber.ToString());

                    int index = lines.IndexOf(line);
                    flightInfo[4] = status.ToString();
                    flightInfo[5] = string.Join(";", seatList);
                    flightInfo[6] = string.Join(";", emptySeats);
                    lines[index] = string.Join(",", flightInfo);
                    break;
                }
            }

            File.WriteAllLines(filePath, lines);
        }

        public static void CancelTicket()
        {
            do
            {
                Console.Clear();
                Common.PrintCenteredTextNoBreak("Moi nhap CMND: ", 1);
                string customerId = Console.ReadLine();
                Common.printStringCenterAfterNoBreak("Moi nhap ma chuyen bay: ", 1);
                string flightCode = Console.ReadLine();
                string seatNum = "";

                do
                {
                    Common.printStringCenterAfterNoBreak("Moi nhap so ghe: ", 1);
                    seatNum = Console.ReadLine();

                    if (!Common.IsNumeric(seatNum))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Common.printStringCenterAfter($"Vui long nhap so ghe hop le!", 0);
                        Console.ResetColor();
                    }

                } while (!Common.IsNumeric(seatNum));

                string filePath = $"T{flightCode}-{customerId}-{seatNum}.txt";

                if (!File.Exists(filePath))
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Common.PrintCenteredTextNoBreak($"Khong tim thay du lieu!", 0);
                    Console.ResetColor();
                } else
                {
                    _DetailTicket(filePath);
                    if (DoYouWantContinute("Ban co chac chan muon xoa (y/n): "))
                    {
                        _CancelTicket(filePath, flightCode, int.Parse(seatNum));
                        Console.Clear();
                        Common.PrintCenteredText("Da xoa ve thanh cong! Xin cam on!");
                        Thread.Sleep(1000);
                    } else
                    {
                        Console.Clear();
                        Common.PrintCenteredText("Da tu choi xoa! Xin cam on!");
                        Thread.Sleep(1000);
                    }
                }

            } while (DoYouWantContinute());
        }

        private static void _CancelTicket(string ticketFilePath, string flightCode, int seatNumber)
        {
            string flightsFile = Airplane.FlightManagement.filePath;
            var lines = File.ReadAllLines(flightsFile).ToList();

            foreach (string line in lines)
            {
                string[] flightInfo = line.Split(',');
                if (flightInfo[0] == flightCode)
                {
                    var seatList = flightInfo[5].Split(';').ToList();
                    var emptySeats = flightInfo[6].Split(';').ToList();

                    seatList.Remove(seatNumber.ToString());
                    emptySeats.Add(seatNumber.ToString());

                    int index = lines.IndexOf(line);
                    flightInfo[4] = Constants.STATUS_FLIGHT_AVAILABLE.ToString();
                    flightInfo[5] = string.Join(";", seatList);
                    flightInfo[6] = string.Join(";", emptySeats);
                    lines[index] = string.Join(",", flightInfo);
                    break;
                }
            }

            File.WriteAllLines(flightsFile, lines);

            if (File.Exists(ticketFilePath))
            {
                File.Delete(ticketFilePath);
            }
        }


        public static bool DoYouWantContinute(string message = "Ban co muon tiep tuc (y/n): ")
        {
            bool inpubtValid = false;
            bool continute = true;
            do
            {
                Common.printStringCenterAfterNoBreak($"{message}", 2);
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

            return continute;
        }

        public static void DetailTicket()
        {
            do
            {
                Console.Clear();
                Common.PrintCenteredTextNoBreak("Moi nhap CMND: ", 1);
                string customerId = Console.ReadLine();
                Common.printStringCenterAfterNoBreak("Moi nhap ma chuyen bay: ", 1);
                string flightCode = Console.ReadLine();
                string seatNum = "";

                do
                {
                    Common.printStringCenterAfterNoBreak("Moi nhap so ghe: ", 1);
                    seatNum = Console.ReadLine();

                    if (!Common.IsNumeric(seatNum))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Common.printStringCenterAfter($"Vui long nhap so ghe hop le!", 0);
                        Console.ResetColor();
                    }

                } while (!Common.IsNumeric(seatNum));

                string filePath = $"T{flightCode}-{customerId}-{seatNum}.txt";

                if (!File.Exists(filePath))
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Common.PrintCenteredTextNoBreak($"Khong tim thay du lieu!", 0);
                    Console.ResetColor();
                }
                else
                {
                    _DetailTicket(filePath);
                }

            } while (DoYouWantContinute());
        }

        private static void _DetailTicket(string ticketFilePath)
        {
            string[] lines = File.ReadAllLines(ticketFilePath);

            Models.Flight flight = Airplane.FlightManagement._GetFlightDetails(lines[1].Split(':')[1].Trim());

            string[] data = {
                $"Ma ve: {lines[0].Split(':')[1].Trim()}",
                $"So hieu may bay: {lines[1].Split(':')[1].Trim()}",
                $"CMND: {lines[2].Split(':')[1].Trim()}",
                $"Ho va Ten: {lines[3].Split(':')[1].Trim()}",
                $"So ghe: {lines[4].Split(':')[1].Trim()}",
                $"Gio bay: {flight.DepartureDate}",
                $"San bay den: {flight.DestinationAirport}",
            };
            Common.PrintCentered($"Thong tin chi tiet chuyen bay khach hang ", data, "", 3);
        }

        public static void TicketList()
        {
            Airplane.FlightManagement._GetFlightList();
            Common.printStringCenterAfterNoBreak("Moi nhap ma chuyen bay: ");

            string flightCode = Console.ReadLine();
            Models.Flight flight = Airplane.FlightManagement._GetFlightDetails(flightCode);

            if (flight == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Common.printStringCenterAfter($"Khong tim thay du lieu!", 0);
                Console.ResetColor();
            } else
            {
                string directoryPath = Directory.GetCurrentDirectory();

                int[] bookedSeats = Array.ConvertAll(flight.TicketList, int.Parse);
                Console.Clear();
                Common.printStringCenterAfter($"Danh sach ve chuyen bay {flightCode}", 2);
                Common.printStringCenterAfter("", 2);
                string[] headers = { "Ma ve", "So CMND", "Ho va Ten", "So ghe" };
                string[,] rowData = new string[bookedSeats.Length, 4];
                var rowNum = 0;

                foreach (int seatNum in bookedSeats)
                {
                    string[] files = Directory.GetFiles(directoryPath, $"T{flightCode}-*-{seatNum}.txt");

                    foreach (string filePath in files)
                    {
                        string[] lines = File.ReadAllLines(filePath);
                        rowData[rowNum, 0] = lines[0].Split(':')[1].Trim();
                        rowData[rowNum, 1] = lines[2].Split(':')[1].Trim();
                        rowData[rowNum, 2] = lines[3].Split(':')[1].Trim();
                        rowData[rowNum, 3] = lines[4].Split(':')[1].Trim();
                    }
                    rowNum++;
                }

                Common.DrawTable(headers, rowData);
            }

            Console.ReadKey();
        }
    }
}
