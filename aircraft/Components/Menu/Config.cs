using System;
using System.Collections.Generic;

namespace aircraft.Components.Menu
{
    public class Config
    {
        public static List<string> MAIN_MENU_LIST = new List<string>();
        public static List<string> AIRPLANE_MENU_LIST = new List<string>();

        public Config()
        {
            _createMainMenu();
            _createAirplaneMenu();
        }

        private void _createMainMenu()
        {
            MAIN_MENU_LIST.Add("1. Dat ve");
            MAIN_MENU_LIST.Add("2. Huy ve");
            MAIN_MENU_LIST.Add("3. Quan ly chuyen bay");
            MAIN_MENU_LIST.Add("4. Quan ly may bay");
            MAIN_MENU_LIST.Add("5. Quan ly khach hang");
            MAIN_MENU_LIST.Add("6. Thong ke");
            MAIN_MENU_LIST.Add("0. Thoat chuong trinh");
        }

        private void _createAirplaneMenu()
        {
            AIRPLANE_MENU_LIST.Add("1. Xem danh sach");
            AIRPLANE_MENU_LIST.Add("2. Chi tiet");
            AIRPLANE_MENU_LIST.Add("0. Tro ve");
        }
    }
}
