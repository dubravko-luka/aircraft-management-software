using System;
using aircraft.Databases;
using aircraft.Helpers;
using System.Threading;

namespace aircraft.Services.Auth
{
    public class AdminLogin
    {
        public static bool Login(string message = "")
        {

            Console.Clear();
            const int width = 50;
            const int height = 2;
            Console.ForegroundColor = ConsoleColor.Red;
            Common.PrintCenteredText(message, -10);
            Console.ResetColor();
            Common.PrintCenteredText("Login Admin", 8);
            UserInterface.DrawCenteredInputFields(width, height);

            int left = (Console.WindowWidth - width) / 2;
            int top = (Console.WindowHeight - (2 * height + 1)) / 2;

            Console.SetCursorPosition(left + 1, top + 1);
            string username = UserInterface.GetUsername(left, top);

            Console.SetCursorPosition(left + 1, top + height + 2);
            string password = UserInterface.GetMaskedPassword(left, top + height + 1);

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                if (!Authenticate(username, password))
                {
                    Login("Tai khoan hoac mat khau khong chinh xac!");
                }
            } else
            {
                Login("Vui long nhap day du thong tin!");
            }
            Console.Clear();
            Common.PrintCenteredText("Dang nhap thanh cong", 0);
            Thread.Sleep(1000);
            Console.Clear();
            return true;
        }

        private static bool Authenticate(string username, string password)
        {
            string[] adminCredentials = Databases.Auth.Admin.Admin.GetAdminCredentials();

            if (adminCredentials != null)
            {
                foreach (string cred in adminCredentials)
                {
                    string[] split = cred.Split('=');
                    if (split.Length == 2 && split[0].Trim().Equals("username") && split[1].Trim().Equals(username))
                    {
                        string[] passSplit = adminCredentials[1].Split('=');
                        if (passSplit.Length == 2 && passSplit[0].Trim().Equals("password") && passSplit[1].Trim().Equals(password))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
