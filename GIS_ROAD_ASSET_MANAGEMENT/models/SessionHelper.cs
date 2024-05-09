using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GIS_ROAD_ASSET_MANAGEMENT.Models
{
    public class SessionHelper
    {
        public static void Set<T>(string key, T value)
        {
            HttpContext.Current.Session[key] = value;
        }

        // Method to get a session value
        public static T Get<T>(string key)
        {
            object value = HttpContext.Current.Session[key];
            if (value != null)
            {
                return (T)value;
            }
            else
            {
                return default(T);
            }
        }
    }
}