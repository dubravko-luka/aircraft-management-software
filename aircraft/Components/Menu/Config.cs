using System;
using System.Collections.Generic;

namespace aircraft.Components.Menu
{
    public class Config
    {
        public static List<string> TICKET_MENU_LIST = new List<string>();
        public static List<string> MAIN_MENU_LIST = new List<string>();
        public static List<string> AIRPLANE_MENU_LIST = new List<string>();
        public static List<string> FLIGHT_MENU_LIST = new List<string>();
        public static List<string> CUSTOMER_MENU_LIST = new List<string>();

        public Config()
        {
            _createMainMenu();
            _creareTicketMenu();
            _createAirplaneMenu();
            _createFlightMenu();
            _customerMenu();
        }

        private void _createMainMenu()
        {
            MAIN_MENU_LIST.Add("1. Quan ly ve");
            MAIN_MENU_LIST.Add("2. Quan ly chuyen bay");
            MAIN_MENU_LIST.Add("3. Quan ly may bay");
            MAIN_MENU_LIST.Add("4. Quan ly khach hang");
            MAIN_MENU_LIST.Add("0. Thoat chuong trinh");
        }

        private void _creareTicketMenu()
        {
            TICKET_MENU_LIST.Add("1. Dat ve");
            TICKET_MENU_LIST.Add("2. Chi tiet ve");
            TICKET_MENU_LIST.Add("3. Danh sach ve");
            TICKET_MENU_LIST.Add("4. Huy ve");
            TICKET_MENU_LIST.Add("0. Tro ve");
        }

        private void _createAirplaneMenu()
        {
            AIRPLANE_MENU_LIST.Add("1. Xem danh sach");
            AIRPLANE_MENU_LIST.Add("2. Chi tiet");
            AIRPLANE_MENU_LIST.Add("0. Tro ve");
        }

        private void _createFlightMenu()
        {
            FLIGHT_MENU_LIST.Add("1. Xem danh sach");
            FLIGHT_MENU_LIST.Add("2. Chi tiet");
            FLIGHT_MENU_LIST.Add("3. Huy chuyen bay");
            FLIGHT_MENU_LIST.Add("4. Hoan tat chuyen bay");
            FLIGHT_MENU_LIST.Add("0. Tro ve");
        }

        private void _customerMenu()
        {
            CUSTOMER_MENU_LIST.Add("1. Xem danh sach");
            CUSTOMER_MENU_LIST.Add("2. Chi tiet");
            CUSTOMER_MENU_LIST.Add("0. Tro ve");
        }
    }
}
