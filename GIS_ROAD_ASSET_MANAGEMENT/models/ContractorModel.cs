using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace GIS_ROAD_ASSET_MANAGEMENT.Models
{
    public class ContractorModel
    {

        public int contractor_id { get; set; }
        public int user_id { get; set; }
        public string company_name { get; set; } = "";

        public string proof_of_identity { get; set; } = "";
        public bool is_approved { get; set; }

        //user details from user table
        public string name { get; set; } = "";
        public string email_id { get; set; } = "";
        public string contact_no { get; set; } = "";
        public string password { get; set; } = "";

        public int role_id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public bool is_active { get; set; }
        public int ward_id { get; set; }
        public bool is_user { get; set; }

    }
}