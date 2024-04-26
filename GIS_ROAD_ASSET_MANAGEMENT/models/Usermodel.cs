using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GIS_ROAD_ASSET_MANAGEMENT.Models
{
    public class Usermodel
    {
        public int id { get; set; }

        public string name { get; set; }

        public string email_id { get; set; }

        public string password { get; set; }
        public string contact_no { get; set; }
        public int role_id { get; set; }

        public DateTime created_at { get; set; }

        public DateTime updated_at { get; set; }

        public bool is_active { get; set; }

        public int ward_id { get; set; }
        public string RoleName { get; set; }
        public string WardName { get; set; }

        public string companyName { get; set; }

        public string proofOfIdentity { get; set; }

        public bool is_approved { get; set; }

        public bool is_user { get; set; }


        //ContractorModel contractor { get; set; }
        //RoleModel role { get; set; }
        //WardModel ward { get; set; }
    }
}