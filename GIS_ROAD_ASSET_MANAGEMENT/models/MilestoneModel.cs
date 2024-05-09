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

        public string project_title { get; set; }

        public string milestone_name { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }

        public string milestone_status { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string description { get; set; }
        public string remarks { get; set; }
        public string image_s { get; set; }
        public string document_s { get; set; }
        public HttpPostedFileBase image { get; set; }
        public HttpPostedFileBase document { get; set; }


    }
}