using GIS_ROAD_ASSET_MANAGEMENT.Models;
using Newtonsoft.Json;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
namespace GIS_ROAD_ASSET_MANAGEMENT.Controllers
{
    public class HomeController : Controller
    { 
        MyDbContext context = new MyDbContext();
        public ActionResult Index() {
            var viewModel = new MonitoringViewModel();

            viewModel.projectList = new List<ProjectModel>();

            using (NpgsqlConnection connection = new NpgsqlConnection())
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["MyconnectionString"].ToString();
                connection.Open();
                string projectQuery = "SELECT * FROM public.project join public.contractor on public.project.allocated_contractor_id=public.contractor.contractor_id join public.user on public.contractor.user_id=public.user.id join ward on public.project.ward_id=public.ward.ward_id  where public.project.is_active=true";

                NpgsqlCommand cmdProject = new NpgsqlCommand(projectQuery, connection);
                NpgsqlDataReader readerProject = cmdProject.ExecuteReader();

                while (readerProject.Read())
                {
                    var projectModel = new ProjectModel();

                    projectModel.project_id = Convert.ToInt32(readerProject["project_id"]);
                    projectModel.project_title = readerProject["project_title"].ToString();
                    projectModel.work_order_no = readerProject["work_order_no"].ToString();
                    projectModel.start_date = Convert.ToDateTime(readerProject["start_date"]);
                    projectModel.end_date = Convert.ToDateTime(readerProject["end_date"]);
                    projectModel.status = readerProject["status"].ToString();
                    int status_temp = Convert.ToInt16(projectModel.status);
                    if (status_temp == 1)
                    {
                        projectModel.status = "Pending";
                    }
                    else if (status_temp == 2)
                    {
                        projectModel.status = "On Going";

                    }
                    else
                    {
                        projectModel.status = "Completed";
                    }

                    int projectType_temp = Convert.ToInt16(readerProject["project_type"]);

                    if (projectType_temp == 1)
                    {
                        projectModel.project_type = "foot bridge";


                    }
                    else if (projectType_temp == 2)
                    {
                        projectModel.project_type = "road cc";
                    }
                    else if (projectType_temp == 3)
                    {
                        projectModel.project_type = "subway/ underpass";
                    }
                    else if (projectType_temp == 4)
                    {
                        projectModel.project_type = "track and footpath";
                    }


                    projectModel.approval_letter_s = readerProject["approval_letter"].ToString();
                    projectModel.tender_doc_s = readerProject["tender_doc"].ToString();
                    projectModel.project_alloted_name = readerProject["name"].ToString();
                    projectModel.project_alloted_contact = readerProject["contact_no"].ToString();
                    projectModel.ward_id = Convert.ToInt16(readerProject["ward_id"]);
                    projectModel.ward_name = readerProject["ward_name"].ToString();
                    var rd= readerProject["road_data"].ToString();
                    projectModel.road_data_j = JsonConvert.DeserializeObject(rd);
                    //complete the code

                    viewModel.projectList.Add(projectModel);

                    //viewModel.contractorList.Add(contractorModel);
                }
                readerProject.Close(); // Close the reader after reading the data
            }


                return View();

        }
        public PartialViewResult Navbar()
        {

            WardModel ward = new WardModel();
            List<WardModel> wards = context.getWardName();

            // Pass the wards data to the partial view
            return PartialView("_Navbar", wards);

        }
        public ActionResult About()
        {

            return View();
        }

        public ActionResult Contact()
        {

            return View();
        }
        public ActionResult Services()
        {

            return View();
        }
       

    }
}