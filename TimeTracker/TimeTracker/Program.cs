using System;
using System.Data.SqlClient;

namespace TimeTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            MenuManager menu = new MenuManager();
            menu.Menu();
           
        }
    }
}
