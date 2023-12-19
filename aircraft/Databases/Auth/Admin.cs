using System;
using System.IO;

namespace aircraft.Databases.Auth.Admin
{
    public class Admin
    {
        // Admin
        public const string FilePathAdmin = "Admin.txt";

        public static void GenerateAdminFile()
        {
            using (StreamWriter writer = new StreamWriter(FilePathAdmin))
            {
                writer.WriteLine("username=admin");
                writer.WriteLine("password=123456");
            }
        }

        public static string[] GetAdminCredentials()
        {
            if (File.Exists(FilePathAdmin))
            {
                return File.ReadAllLines(FilePathAdmin);
            }
            return null;
        }
    }
}
