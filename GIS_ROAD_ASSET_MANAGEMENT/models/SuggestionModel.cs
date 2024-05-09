using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GIS_ROAD_ASSET_MANAGEMENT.Models
{
    public class SuggestionModel
    {
        public int suggestion_id { get; set; }
        public string comment { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        public int projectId { get; set; }
        public int userId {  get; set; }

    }
}