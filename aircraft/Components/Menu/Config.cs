using System;
using System.Collections.Generic;

namespace aircraft.Components.Menu
{
    public class Config
    {
        public static List<string> MAIN_MENU_LIST = new List<string>();

        public Config()
        {
            _createMainMenu();
        }

        private void _createMainMenu()
        {
            MAIN_MENU_LIST.Add("1. Dat ve");
            MAIN_MENU_LIST.Add("2. Huy ve");
            MAIN_MENU_LIST.Add("3. Danh sach chuyen bay");
            MAIN_MENU_LIST.Add("4. Danh sach may bay");
            MAIN_MENU_LIST.Add("5. Danh sach khach hang");
            MAIN_MENU_LIST.Add("6. Thong ke");
            MAIN_MENU_LIST.Add("0. Thoat");
        }
    }
}
