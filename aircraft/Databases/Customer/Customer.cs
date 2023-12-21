using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace aircraft.Databases.Customer
{
    public class Customer
    {
        public const string filePath = "Customers.txt";

        public static void GenerateCustomerFile()
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                for (int i = 0; i < 20; i++)
                {
                    int sequenceNumber = i + 1;
                    string IdentityCardNumber = GenerateIdentityCardNumber();
                    string fullName = GenerateFullName();

                    string customerInfo = $"{sequenceNumber},{IdentityCardNumber},{fullName}";
                    writer.WriteLine(customerInfo);
                }
            }
        }

        private static string GenerateIdentityCardNumber()
        {
            Random random = new Random();
            string cmnd = $"{random.Next(100000000, 999999999)}";
            return cmnd;
        }

        private static string GenerateFullName()
        {
            Random random = new Random();
            string[] firstNames = { "Nguyen", "Tran", "Le", "Pham", "Vo", "Bui" };
            string[] lastNames = { "Phuong", "Khanh", "Thuan", "Huong", "Phuoc", "Trinh" };

            string fullName = $"{firstNames[random.Next(firstNames.Length)]} {lastNames[random.Next(lastNames.Length)]}";
            return fullName;
        }
    }
}
