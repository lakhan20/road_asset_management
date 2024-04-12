using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GIS_ROAD_ASSET_MANAGEMENT.Models
{
    public class ContractorModel : RoleModel
    {
        public int contractor_id { get; set; }

        public int user_id { get; set; }
        public string company_name { get; set; }
        public string proof_of_identity { get; set; }
        public bool is_approved { get; set; }

    }
}
