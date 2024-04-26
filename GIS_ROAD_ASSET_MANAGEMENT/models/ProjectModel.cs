using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GIS_ROAD_ASSET_MANAGEMENT.Models
{
    public class ProjectModel
    {
        public int project_id { get; set; }
        public string project_title { get; set; }
        public string work_order_no { get; set; }
        public string description { get; set; }
        public string project_location { get; set; }
        public string status { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public string created_by { get; set; }
        public string s_latitude { get; set; }
        public string s_longitude { get; set; }

        public string e_latitude { get; set; }
        public string e_longitude { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public int allocated_contractor_id { get; set; }
        public decimal budget_cost { get; set; }
        public string project_type { get; set; }
        public int ward_id { get; set; }
        public HttpPostedFileBase approval_letter { get; set; }
        public HttpPostedFileBase tender_doc { get; set; }

        public string approval_letter_s { get; set; }
        public string tender_doc_s { get; set; }

        public string project_alloted_name { get; set; }

        public string project_alloted_contact { get; set; }

    }
}