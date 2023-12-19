﻿using System;
using System.IO;
using aircraft.Helpers;
using System.Threading;

namespace aircraft.Databases
{
    public class Init
    {
        public static void generateData()
        {
            checkAdminData();
            checkAirplaneData();
            //GenerateData();
        }

        private static void checkAdminData ()
        {
            string filePath = Auth.Admin.Admin.FilePathAdmin;
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                bool validData = false;

                foreach (string line in lines)
                {
                    string[] parts = line.Split('=');
                    if (parts.Length == 2 && parts[0].Trim() == "username" && parts[1].Trim() == "admin")
                    {
                        validData = true;
                        break;
                    }
                }
                if (!validData)
                {
                    File.Delete(filePath);
                    Auth.Admin.Admin.GenerateAdminFile();
                }
            }
            else
            {
                Auth.Admin.Admin.GenerateAdminFile();
            }
        }

        private static void checkAirplaneData()
        {
            string filePath = Airplanes.Airplane.Airplane.filePath;
            if (!File.Exists(filePath))
            {
                Airplanes.Airplane.Airplane.GenerateRandomAirplanes();
            }
        }

        static void GenerateData()
        {
            int count = 1;
            int maxCount = 3;
            int delay = 500;

            while (count <= maxCount)
            {
                int dotCount = 1;
                int maxDotCount = 3;
                Console.Clear();
                Common.PrintCenteredTextNoBreak("Generating Data", 0);
                do
                {
                    Console.Write(".");
                    Thread.Sleep(delay);
                    dotCount++;
                } while (dotCount <= maxDotCount);
                count++;
            }

            Console.WriteLine();
            Console.WriteLine("Data generated!");
        }
    }
}
