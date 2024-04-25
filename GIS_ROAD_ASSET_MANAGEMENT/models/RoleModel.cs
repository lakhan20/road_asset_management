using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GIS_ROAD_ASSET_MANAGEMENT.Models
{
    public class RoleModel
    {
        public int role_id{ get; set; }

        public string role_name { get; set; }

        public bool is_active { get; set; }
    }
}