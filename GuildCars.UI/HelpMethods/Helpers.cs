using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuildCars.UI.HelpMethods
{
    public static class Helpers
    {
        public static string GetImageFilePath(int id)
        {
            return "http://localhost:56259/Images/Inventory-" + id + ".jpg";
        }
    }
}