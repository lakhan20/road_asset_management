using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GIS_ROAD_ASSET_MANAGEMENT.Models
{
    public class MilestoneModel
    {
        public int milestone_id { get; set; }
        public int project_id { get; set; }
        public string milestone_name { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public decimal milestone_cost { get; set; }
        public string status { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
    }
}