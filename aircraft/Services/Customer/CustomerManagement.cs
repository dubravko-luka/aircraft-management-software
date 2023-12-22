using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using aircraft.Helpers;

namespace aircraft.Services.Customer
{
    public class CustomerManagement
    {
        public const string filePath = Databases.Customer.Customer.filePath;

        public static void GetCustomerList()
        {

            _GetCustomerList();
            Common.printStringCenterAfterNoBreak("An phim bat ki de tro ve!");
            Console.ReadKey();
        }

        public static void _GetCustomerList()
        {
            List<Models.Customer> CustomerList = __GetCustomerList();

            if (CustomerList != null && CustomerList.Count > 0)
            {
                string[] headers = { "So thu tu", "So CMND", "Ho va Ten" };
                string[,] rowData = new string[CustomerList.Count, 3];

                for (int i = 0; i < CustomerList.Count; i++)
                {
                    rowData[i, 0] = CustomerList[i].SequenceNumber.ToString();
                    rowData[i, 1] = CustomerList[i].CMND.ToString();
                    rowData[i, 2] = CustomerList[i].FullName.ToString();
                }

                Common.DrawTable(headers, rowData);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Khong co du lieu khach hang.");
                Console.ResetColor();
            }
        }

        private static List<Models.Customer> __GetCustomerList()
        {
            List<Models.Customer> CustomerList = new List<Models.Customer>();

            try
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] items = line.Split(',');
                    if (items.Length == 3)
                    {
                        Models.Customer Customer = new Models.Customer
                        {
                            SequenceNumber = int.Parse(items[0]),
                            CMND = items[1],
                            FullName = items[2]
                        };
                        CustomerList.Add(Customer);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Customers.txt not found");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return CustomerList;
        }

        public static void GetCustomerDetails()
        {
            bool continute = true;
            int windowHeight = Console.WindowHeight;
            int windowWidth = Console.WindowWidth;

            do
            {
                Console.Clear();
                _GetCustomerList();
                Common.printStringCenterAfterNoBreak("Nhap chung minh nhan dan: ");
                string code = Console.ReadLine();
                Models.Customer customer = _GetCustomerDetails(code);

                Console.Clear();
                Console.SetCursorPosition(windowWidth / 2, windowHeight / 2);
                Console.WriteLine(" ");

                if (customer != null)
                {
                    string[] data = { $"So thu tu: {customer.SequenceNumber}", $"So CMND: {customer.CMND}", $"Ho va Ten: {customer.FullName}" };
                    Common.PrintCentered($"Khach hang co CMND: {code}", data);
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

        public static Models.Customer _GetCustomerDetails(string code)
        {
            List<Models.Customer> CustomerList = __GetCustomerList();
            Models.Customer Customer = CustomerList.FirstOrDefault(ap => ap.CMND == code);
            return Customer;
        }
    }
}
