using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GIS_ROAD_ASSET_MANAGEMENT.Models
{
    public class MonitoringViewModel
    {
        public List<ContractorModel> contractorList { get; set; }
        public List<WardModel> wardList { get; set; }
        public List<ProjectModel> projectList { get; set; }
    }
}