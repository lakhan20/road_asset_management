using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GIS_ROAD_ASSET_MANAGEMENT.Models;
using Npgsql;

namespace GIS_ROAD_ASSET_MANAGEMENT.Controllers
{
    public class AdminController : Controller
    {
        Temp temp = new Temp();
        public ActionResult HomeView()
        {
            return View();
        }
        public ActionResult DashboardView()
        {
            return View();
        }

        public ActionResult Project_MonitoringView()
        {
            var viewModel = new MonitoringViewModel();
            viewModel.contractorList = new List<ContractorModel>();
            viewModel.wardList = new List<WardModel>();
            viewModel.projectList = new List<ProjectModel>();
            //int userId = (int)Session["user_id"];

            using (NpgsqlConnection connection = new NpgsqlConnection())
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["MyconnectionString"].ToString();
                connection.Open();
                string contractorQuery = "SELECT * FROM contractor join public.\"user\" ON \"user\".id = contractor.user_id WHERE is_approved=true;";
                string wardQuery = "SELECT * FROM ward";
                string projectQuery = "SELECT * FROM public.project join public.contractor on public.project.allocated_contractor_id=public.contractor.contractor_id join public.user on public.contractor.user_id=public.user.id";

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
                    projectModel.approval_letter_s = readerProject["approval_letter"].ToString();
                    projectModel.tender_doc_s = readerProject["tender_doc"].ToString();
                    projectModel.project_alloted_name = readerProject["name"].ToString();
                    projectModel.project_alloted_contact = readerProject["contact_no"].ToString();


                    //complete the code

                    viewModel.projectList.Add(projectModel);

                    //viewModel.contractorList.Add(contractorModel);
                }
                readerProject.Close(); // Close the reader after reading the data

                NpgsqlCommand cmdContractor = new NpgsqlCommand(contractorQuery, connection);
                NpgsqlDataReader readerContractor = cmdContractor.ExecuteReader();

                while (readerContractor.Read())
                {
                    var contractorModel = new ContractorModel();
                    contractorModel.contractor_id = Convert.ToInt32(readerContractor["contractor_id"]);
                    contractorModel.user_id = Convert.ToInt32(readerContractor["user_id"]);
                    contractorModel.company_name = readerContractor["company_name"].ToString();
                    contractorModel.proof_of_identity = readerContractor["proof_of_identity"].ToString();
                    contractorModel.email_id = readerContractor["email_id"].ToString();
                    viewModel.contractorList.Add(contractorModel);
                }
                readerContractor.Close(); // Close the reader after reading the data

                NpgsqlCommand cmdWard = new NpgsqlCommand(wardQuery, connection);
                NpgsqlDataReader readerWard = cmdWard.ExecuteReader();

                while (readerWard.Read())
                {
                    var wardModel = new WardModel();
                    wardModel.ward_id = Convert.ToInt32(readerWard["ward_id"]);
                    wardModel.ward_name = readerWard["ward_name"].ToString();
                    viewModel.wardList.Add(wardModel);
                }
                readerWard.Close(); // Close the reader after reading the data
            }

            // Wrap the single instance of MonitoringViewModel in a list
            var viewModelList = new List<MonitoringViewModel> { viewModel };
            ViewBag.role = "Admin";

            return View(viewModelList); // Pass the list containing the single instance to the view
          
        }
        [HttpPost]
        public ActionResult addProject(ProjectModel projectModel)
        {
            if (projectModel.tender_doc != null && projectModel.approval_letter != null)
            {
                try
                {
                    // Ensure directories exist
                    string tenderDocDirectory = Server.MapPath("~/tenderDocs");
                    string approvalLetterDirectory = Server.MapPath("~/approvalLetters");
                    Directory.CreateDirectory(tenderDocDirectory);
                    Directory.CreateDirectory(approvalLetterDirectory);

                    // Save tender document
                    string tenderDocFileName = Path.GetFileName(projectModel.tender_doc.FileName);
                    string tenderDocPath = Path.Combine(tenderDocDirectory, tenderDocFileName);
                    projectModel.tender_doc.SaveAs(tenderDocPath);

                    // Save approval letter
                    string approvalLetterFileName = Path.GetFileName(projectModel.approval_letter.FileName);
                    string approvalLetterPath = Path.Combine(approvalLetterDirectory, approvalLetterFileName); //filename
                    projectModel.approval_letter.SaveAs(approvalLetterPath);


                    using (NpgsqlConnection connection = new NpgsqlConnection())
                    {
                      

                        connection.ConnectionString = ConfigurationManager.ConnectionStrings["MyconnectionString"].ToString();
                        connection.Open();
                        string insertProject = "";
                        insertProject = "INSERT INTO project(project_title,work_order_no,status,description,start_date,end_date,created_at,s_latitude,s_longitude,e_latitude,e_longitude,project_type,budget_cost,allocated_contractor_id,ward_id,approval_letter,tender_doc) VALUES('" + projectModel.project_title.Trim().ToString() + "','" + projectModel.work_order_no.Trim().ToString() + "','1','" + projectModel.description.Trim().ToString() + "','" + projectModel.start_date + "','" + projectModel.end_date + "','" + DateTime.Now + "','" + projectModel.s_latitude.Trim().ToString() + "','" + projectModel.s_longitude.Trim().ToString() + "','" + projectModel.e_latitude.Trim().ToString() + "','" + projectModel.e_longitude.Trim().ToString() + "','" + projectModel.project_type + "','" + projectModel.budget_cost + "','" + projectModel.allocated_contractor_id + "','" + projectModel.ward_id + "','" + approvalLetterFileName.Trim().ToString() + "','" + tenderDocFileName.Trim().ToString() + "')";

                        //Print the SQL command
                        //    Console.WriteLine("SQL Command: " + insertProject);

                        NpgsqlCommand cmd = new NpgsqlCommand(insertProject, connection);

                        cmd.Connection = connection;
                        cmd.ExecuteNonQuery();
                    }


                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, error = ex.Message });
                }
            }
            else
            {
                return Json(new { success = false, error = "Tender document or approval letter is missing." });
            }
        }

        public ActionResult Project_MilestoneView()
        {
            return View();
        }

        public ActionResult AboutView()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult DemoView()
        {
            return View();
        }
        public ActionResult ContactView()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult suggestionView()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult UserView()
        {
            MyDbContext context = new MyDbContext();

            List<Usermodel> user1 = context.getUser();
            List<WardModel> wards = context.getWardName();
            List<RoleModel>  role = context.getRole();

            temp.wardmoel = wards;
            temp.usermodel = user1;
            temp.rolemodel = role;
            
            return View(temp);
        }
        public ActionResult ContractorView()
        {
            MyDbContext context = new MyDbContext();
            List<Usermodel> user1 = context.getContractor();
            return View(user1);
        }

        [HttpPost]
        public JsonResult ContractorView(int id)
        {
            MyDbContext context = new MyDbContext();
            context.approvedContractor(id);
            return Json(new { Success = true});
        }
        [HttpPost]
        public JsonResult updateUser(Usermodel user)
        {
            MyDbContext context = new MyDbContext();
            context.updateUserDetails(user);
            return Json(new { Success = true });
        }
        [HttpPost]
        public JsonResult disableUser(int id)
        {
            MyDbContext context = new MyDbContext();
            context.disableUser(id);
            return Json(new { Success = true });
        }
        [HttpPost]
        public JsonResult getUserById(int id)
        {
            MyDbContext context = new MyDbContext();
            context.approvedContractor(id);
            return Json(new { Success = true });
        }



    }
}