using System;
using aircraft.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aircraft.Services.Airplane
{
    public class AirplaneManagement
    {
        private const string filePath = "Airplanes.txt";

        public List<Models.Airplane> GetAirplaneList()
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

        public Models.Airplane GetAirplaneDetails(string code)
        {
            List<Models.Airplane> airplaneList = GetAirplaneList();
            Models.Airplane airplane = airplaneList.FirstOrDefault(ap => ap.PlaneCode == code);
            return airplane;
        }
    }
}
