using System;
using System.Collections.Generic;
using System.IO;

namespace aircraft.Databases.Airplanes
{
    public class Flight
    {
        public const string filePath = "Flights.txt";

        public static void GenerateFlightFile()
        {
            List<string> airportList = new List<string>
            {
                "HAN", "SGN", "DAD", "VCA", "CXR", "PQC", "VII", "VDH", "PXU", "BMV"
            };

            Random random = new Random();

            string[] airplaneData = File.ReadAllLines("Airplanes.txt");

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                for (int i = 0; i < 10; i++)
                {
                    string flightCode = "F" + (i + 1).ToString().PadLeft(3, '0');

                    string aircraftData = airplaneData[random.Next(airplaneData.Length)];
                    string[] aircraftInfo = aircraftData.Split(',');

                    string aircraftCode = aircraftInfo[0].Trim();
                    int seatCount = int.Parse(aircraftInfo[1].Trim());

                    string departureDate = DateTime.Now.AddDays(random.Next(1, 30)).ToString("yyyy-MM-dd");

                    string destinationAirport = airportList[random.Next(airportList.Count)];

                    int status = random.Next(4); // Status flight: 0 - 3
                    //{ "Canceled", "Available", "Sold out", "Completed" };

                    List<string> ticketList = new List<string>();
                    List<int> emptySeats = new List<int>();
                    for (int j = 1; j <= seatCount; j++)
                    {
                        if (status == 2)
                        {
                            ticketList.Add($"Ticket{j}");
                        }
                        else
                        {
                            emptySeats.Add(j);
                        }
                    }

                    string flightInfo = $"{flightCode},{aircraftCode},{departureDate},{destinationAirport},{status},{string.Join(";", ticketList)},{string.Join(";", emptySeats)},";
                    writer.WriteLine(flightInfo);
                }
            }
        }
    }
}
