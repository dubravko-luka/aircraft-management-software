﻿using System;
using aircraft.Databases;
using aircraft.Helpers;
using System.Threading;

namespace aircraft.Services.Auth
{
    public class AdminLogin
    {

        public static bool Login(string message = "", int loginFail = 1)
        {
            
            Console.Clear();
            const int width = 50;
            const int height = 2;
            Console.ForegroundColor = ConsoleColor.Red;
            Common.PrintCenteredText(message, -20);
            Console.ResetColor();
            Common.PrintCenteredText("Dang nhap he thong", 8);
            if (loginFail - 1 > 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Common.PrintCenteredText($"Ban da nhap sai {loginFail - 1} lan", -22);
                Console.ResetColor();
            }

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
                    if (loginFail == 3)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        int s = 5;
                        do
                        {
                            Common.PrintCenteredText("Ban da nhap sai qua 3 lan", 0);
                            Common.printStringCenterAfterNoBreak($"He thong the dong sau 0{s} giay", 1);
                            s--;
                            Thread.Sleep(1000);
                        } while (s >= 0);
                        Console.ResetColor();
                        Common.checkIsQuit(0);
                    } else
                    {
                        Login($"Tai khoan hoac mat khau khong chinh xac!", loginFail + 1);
                    }
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
