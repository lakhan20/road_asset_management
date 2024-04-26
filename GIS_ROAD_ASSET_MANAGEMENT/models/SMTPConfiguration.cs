using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GIS_ROAD_ASSET_MANAGEMENT.Models
{
    public class SMTPConfiguration
    {
        public string server { get; set; }
        public string hostName { get; set; }
        public string password { get; set; }
        public int port { get; set; }
    }
}