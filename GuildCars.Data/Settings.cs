using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data
{
    public static class Settings
    {
        private static string _connection = "Server = localhost; Database=GuildCars;Integrated Security = True;";
        private static string _mode = "ADO";
        public static string GetConnectionString()
        {
            return _connection;
        }

        public static string GetMode()
        {
            return _mode;
        }
    }
}
