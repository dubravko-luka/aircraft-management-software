using System;
using aircraft.Databases;
using aircraft.Services;
using aircraft.Routers;

namespace aircraft
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Databases.Init.generateData();

            //if (Services.Auth.AdminLogin.Login())
            //{
                Routers.router.Direction();
            //}
        }
    }
}
