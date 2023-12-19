using System;
using aircraft.Models;
using System.IO;
using System.Collections.Generic;

namespace aircraft.Databases.Airplanes.Airplane
{
    public class Airplane
    {
        // Airplanes
        private const string filePath = "Airplanes.txt";

        public static void GenerateRandomAirplanes()
        {
            Random random = new Random();
            List<Models.Airplane> airplaneList = new List<Models.Airplane>();

            for (int i = 0; i < 20; i++)
            {
                Models.Airplane airplane = new Models.Airplane
                {
                    PlaneCode = $"MB{i + 1}",
                    SeatCount = random.Next(100, 300) // Số chỗ từ 100 đến 299
                };
                airplaneList.Add(airplane);
            }

            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (Models.Airplane airplane in airplaneList)
                    {
                        writer.WriteLine($"{airplane.PlaneCode},{airplane.SeatCount}");
                    }
                }
                Console.WriteLine("Successfully generated and saved 20 airplanes in Airplanes.txt");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
