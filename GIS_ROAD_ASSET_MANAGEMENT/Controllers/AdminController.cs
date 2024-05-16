using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using GIS_ROAD_ASSET_MANAGEMENT.Filters;
using GIS_ROAD_ASSET_MANAGEMENT.Models;
using Newtonsoft.Json;
using Npgsql;

namespace GIS_ROAD_ASSET_MANAGEMENT.Controllers
{
    [UserAuthentication]
    public class AdminController : CustomBaseController
    {
        Temp temp = new Temp();

        public PartialViewResult NavbarAdmin()
        {
            return PartialView("_NavbarAdmin", "Admin");

        }
        public PartialViewResult SidebarAdmin()
        {
            return PartialView("_sidebarAdmin", "Admin");
        }



        public ActionResult getAllProject()
        {

            List<ProjectModel> projectList = new List<ProjectModel>();
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
                    projectModel.s_latitude = Convert.ToString(readerProject["s_latitude"]);
                    projectModel.s_longitude = Convert.ToString(readerProject["s_longitude"]);
                    projectModel.e_latitude = Convert.ToString(readerProject["e_latitude"]);
                    projectModel.e_longitude = Convert.ToString(readerProject["e_longitude"]);

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

                    var rd = readerProject["road_data"].ToString();
                    //projectModel.road_data_j = JsonConvert.DeserializeObject(rd);
                    projectModel.road_data = readerProject["road_data"].ToString();
                    //complete the code

                    projectList.Add(projectModel);

                    //viewModel.contractorList.Add(contractorModel);
                }
                readerProject.Close(); // Close the reader after reading the data
            }

            return Json(new { success = "hello", project = projectList }, JsonRequestBehavior.AllowGet);


        }




        public ActionResult HomeView()
        {
            //var viewModel = new MonitoringViewModel();

            //viewModel.projectList = new List<ProjectModel>();
            //List<ProjectModel> projectList = new List<ProjectModel>();
            //using (NpgsqlConnection connection = new NpgsqlConnection())
            //{
            //    connection.ConnectionString = ConfigurationManager.ConnectionStrings["MyconnectionString"].ToString();
            //    connection.Open();
            //    string projectQuery = "SELECT * FROM public.project join public.contractor on public.project.allocated_contractor_id=public.contractor.contractor_id join public.user on public.contractor.user_id=public.user.id join ward on public.project.ward_id=public.ward.ward_id  where public.project.is_active=true";

            //    NpgsqlCommand cmdProject = new NpgsqlCommand(projectQuery, connection);
            //    NpgsqlDataReader readerProject = cmdProject.ExecuteReader();

            //    while (readerProject.Read())
            //    {
            //        var projectModel = new ProjectModel();

            //        projectModel.project_id = Convert.ToInt32(readerProject["project_id"]);
            //        projectModel.project_title = readerProject["project_title"].ToString();
            //        projectModel.work_order_no = readerProject["work_order_no"].ToString();
            //        projectModel.start_date = Convert.ToDateTime(readerProject["start_date"]);
            //        projectModel.end_date = Convert.ToDateTime(readerProject["end_date"]);
            //        projectModel.status = readerProject["status"].ToString();
            //        int status_temp = Convert.ToInt16(projectModel.status);
            //        if (status_temp == 1)
            //        {
            //            projectModel.status = "Pending";
            //        }
            //        else if (status_temp == 2)
            //        {
            //            projectModel.status = "On Going";

            //        }
            //        else
            //        {
            //            projectModel.status = "Completed";
            //        }

            //        int projectType_temp = Convert.ToInt16(readerProject["project_type"]);

            //        if (projectType_temp == 1)
            //        {
            //            projectModel.project_type = "foot bridge";


            //        }
            //        else if (projectType_temp == 2)
            //        {
            //            projectModel.project_type = "road cc";
            //        }
            //        else if (projectType_temp == 3)
            //        {
            //            projectModel.project_type = "subway/ underpass";
            //        }
            //        else if (projectType_temp == 4)
            //        {
            //            projectModel.project_type = "track and footpath";
            //        }


            //        projectModel.approval_letter_s = readerProject["approval_letter"].ToString();
            //        projectModel.tender_doc_s = readerProject["tender_doc"].ToString();
            //        projectModel.project_alloted_name = readerProject["name"].ToString();
            //        projectModel.project_alloted_contact = readerProject["contact_no"].ToString();
            //        projectModel.ward_id = Convert.ToInt16(readerProject["ward_id"]);
            //        projectModel.ward_name = readerProject["ward_name"].ToString();
            //        var rd = readerProject["road_data"].ToString();
            //        projectModel.road_data_j = JsonConvert.DeserializeObject(rd);
            //        //complete the code

            //        projectList.Add(projectModel);

            //        //viewModel.contractorList.Add(contractorModel);
            //    }
            //    readerProject.Close(); // Close the reader after reading the data
            //}


            return View();
        }
        public ActionResult DashboardView()
        {
            MyDbContext db = new MyDbContext();

            Dictionary<int, int> projectCounts = db.GetProjectCountsByType(); // Assuming you have a method to retrieve project counts

            return View(projectCounts);
        }


        public ActionResult Project_MonitoringView()
        {
            var viewModel = new MonitoringViewModel();
            viewModel.contractorList = new List<ContractorModel>();
            viewModel.wardList = new List<WardModel>();
            viewModel.projectList = new List<ProjectModel>();
            int userId = (int)Session["user_id"];
            List<ProjectModel> projects = new List<ProjectModel>();
            using (NpgsqlConnection connection = new NpgsqlConnection())
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["MyconnectionString"].ToString();
                connection.Open();
                string contractorQuery = "SELECT * FROM contractor join public.\"user\" ON \"user\".id = contractor.user_id WHERE is_approved=true;";
                string wardQuery = "SELECT * FROM ward";
                string projectQuery = "SELECT * FROM public.project JOIN public.contractor ON public.project.allocated_contractor_id = public.contractor.contractor_id JOIN public.user ON public.contractor.user_id = public.user.id JOIN ward ON public.project.ward_id = ward.ward_id WHERE public.project.is_active = true ORDER BY COALESCE(public.project.updated_at, public.project.created_at, '1900-01-01') DESC, public.project.project_id DESC;";

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

        public ActionResult updateMilestone(MilestoneModel milestonemodel)
        {
            if (milestonemodel.document != null)
            {
                try
                {
                    // Ensure directories exist
                    string documentDirectory = Server.MapPath("~/document");

                    Directory.CreateDirectory(documentDirectory);

                    // Save tender document
                    string documentFileName = Path.GetFileName(milestonemodel.document.FileName);
                    string documentPath = Path.Combine(documentDirectory, documentFileName);
                    milestonemodel.document.SaveAs(documentPath);
                    milestonemodel.document_s = documentFileName.Trim();
                    // Save approval letter

                }
                catch (Exception ex)
                {
                    return Json(new { success = false, error = ex.Message });
                }
            }
            if (milestonemodel.image != null)
            {
                try
                {
                    // Ensure directories exist
                    string imageDirectory = Server.MapPath("~/image");

                    Directory.CreateDirectory(imageDirectory);

                    // Save tender document
                    string imageFileName = Path.GetFileName(milestonemodel.image.FileName);
                    string imagePath = Path.Combine(imageDirectory, imageFileName);
                    milestonemodel.image.SaveAs(imagePath);
                    milestonemodel.image_s = imageFileName.Trim();
                    // Save approval letter
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, error = ex.Message });
                }
            }
            int count = 0;
            using (NpgsqlConnection connection = new NpgsqlConnection())
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["MyconnectionString"].ToString();
                connection.Open();
                string updateMilestone = "";
                //updateProject = "UPDATE public.project SET project_title='" + projectModel.project_title + "',work_order_no='" + projectModel.work_order_no + "',description='" + projectModel.description + "',start_date='" + projectModel.start_date + "',end_date='" + projectModel.end_date + "',s_latitude='" + projectModel.s_latitude + "',s_longitude='" + projectModel.s_longitude + "',e_latitude='" + projectModel.e_latitude + "',e_longitude='" + projectModel.e_longitude + "',updated_at='" + DateTime.Now + "',allocated_contractor_id='" + projectModel.allocated_contractor_id + "',budget_cost='" + projectModel.budget_cost + "',ward_id='" + projectModel.ward_id + "',approval_letter='" + projectModel.approval_letter_s + "',tender_doc='" + projectModel.tender_doc_s + "',project_type='" + projectModel.project_type + "',status='" + projectModel.status + "' WHERE project_id=" + projectModel.project_id;
                updateMilestone = "UPDATE public.milestone SET  project_id='" + milestonemodel.project_id + "', milestone_name='" + milestonemodel.milestone_name + "', start_date='" + milestonemodel.start_date + "', end_date='" + milestonemodel.end_date + "', updated_at='" + DateTime.Now + "', description='" + milestonemodel.description + "', image='" + milestonemodel.image_s + "', document='" + milestonemodel.document_s + "', remarks='" + milestonemodel.remarks + "', milestone_status='" + milestonemodel.milestone_status + "' WHERE milestone_id ='" + milestonemodel.milestone_id + "'";
                NpgsqlCommand cmd = new NpgsqlCommand(updateMilestone, connection);

                cmd.Connection = connection;
                count = cmd.ExecuteNonQuery();
            }
            if (count > 0)
            {
                return Json(new { success = true });
            }
            return Json(new { error = true });


        }


        public ActionResult addMilestone(MilestoneModel milestoneModel)
        {
            if (milestoneModel.image != null && milestoneModel.document != null)
            {
                try
                {
                    // Ensure directories exist
                    string imageDirectory = Server.MapPath("~/images");
                    string documentDirectory = Server.MapPath("~/document");
                    Directory.CreateDirectory(imageDirectory);
                    Directory.CreateDirectory(documentDirectory);

                    // Save tender document
                    string imageFileName = Path.GetFileName(milestoneModel.image.FileName);
                    string imagePath = Path.Combine(imageDirectory, imageFileName);
                    milestoneModel.image.SaveAs(imagePath);

                    // Save approval letter
                    string documentFileName = Path.GetFileName(milestoneModel.document.FileName);
                    string documentPath = Path.Combine(documentDirectory, documentFileName); //filename
                    milestoneModel.document.SaveAs(documentPath);

                    //var created_by = SessionHelper.Get<int>("user_id");

                    using (NpgsqlConnection connection = new NpgsqlConnection())
                    {


                        connection.ConnectionString = ConfigurationManager.ConnectionStrings["MyconnectionString"].ToString();
                        connection.Open();
                        string insertMilestone = "";
                        insertMilestone = "INSERT INTO milestone(project_id,milestone_name,start_date,end_date,created_at,description,image,document,remarks,milestone_status) VALUES('" + milestoneModel.project_id + "','" + milestoneModel.milestone_name.Trim().ToString() + "','" + milestoneModel.start_date + "','" + milestoneModel.end_date + "','" + DateTime.Now + "','" + milestoneModel.description.Trim().ToString() + "','" + imageFileName.Trim().ToString() + "','" + documentFileName.Trim().ToString() + "','" + milestoneModel.remarks + "','" + milestoneModel.milestone_status + "' )";

                        //Print the SQL command
                        //    Console.WriteLine("SQL Command: " + insertProject);

                        NpgsqlCommand cmd = new NpgsqlCommand(insertMilestone, connection);

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

                    var created_by = SessionHelper.Get<int>("user_id");

                    using (NpgsqlConnection connection = new NpgsqlConnection())
                    {


                        connection.ConnectionString = ConfigurationManager.ConnectionStrings["MyconnectionString"].ToString();
                        connection.Open();
                        string insertProject = "";
                        insertProject = "INSERT INTO project(project_title,work_order_no,status,description,start_date,end_date,created_by,created_at,s_latitude,s_longitude,e_latitude,e_longitude,project_type,budget_cost,allocated_contractor_id,ward_id,approval_letter,tender_doc,road_data) VALUES('" + projectModel.project_title.Trim().ToString() + "','" + projectModel.work_order_no.Trim().ToString() + "','1','" + projectModel.description.Trim().ToString() + "','" + projectModel.start_date + "','" + projectModel.end_date + "','" + created_by + "','" + DateTime.Now + "','" + projectModel.s_latitude.Trim().ToString() + "','" + projectModel.s_longitude.Trim().ToString() + "','" + projectModel.e_latitude.Trim().ToString() + "','" + projectModel.e_longitude.Trim().ToString() + "','" + projectModel.project_type + "','" + projectModel.budget_cost + "','" + projectModel.allocated_contractor_id + "','" + projectModel.ward_id + "','" + approvalLetterFileName.Trim().ToString() + "','" + tenderDocFileName.Trim().ToString() + "','" + projectModel.road_data + "')";

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
            var viewModel = new MilestoneViewModel();
            viewModel.projectList = new List<ProjectModel>();

            viewModel.milestoneList = new List<MilestoneModel>();

            using (NpgsqlConnection connection = new NpgsqlConnection())
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["MyconnectionString"].ToString();
                connection.Open();
                string projectQuery = "SELECT * FROM public.project where is_active=true  ORDER BY project_id ASC";

                NpgsqlCommand cmdProject = new NpgsqlCommand(projectQuery, connection);
                NpgsqlDataReader readerProject = cmdProject.ExecuteReader();
                while (readerProject.Read())
                {
                    var projectModel = new ProjectModel();

                    projectModel.project_id = Convert.ToInt32(readerProject["project_id"]);
                    projectModel.project_title = readerProject["project_title"].ToString();

                    viewModel.projectList.Add(projectModel);

                }
                readerProject.Close();


                string monitoringQuery = "SELECT * FROM public.milestone join public.project on public.milestone.project_id=public.project.project_id  ORDER BY COALESCE(public.milestone.updated_at, '1900-01-01') DESC, public.milestone.milestone_id DESC";

                NpgsqlCommand cmdMilestone = new NpgsqlCommand(monitoringQuery, connection);
                NpgsqlDataReader readerMilestone = cmdMilestone.ExecuteReader();
                while (readerMilestone.Read())
                {
                    var milestoneModel = new MilestoneModel();

                    milestoneModel.milestone_id = Convert.ToInt32(readerMilestone["milestone_id"]);
                    milestoneModel.project_id = Convert.ToInt32(readerMilestone["project_id"]);
                    milestoneModel.project_title = readerProject["project_title"].ToString();
                    milestoneModel.milestone_name = readerProject["milestone_name"].ToString();
                    milestoneModel.start_date = Convert.ToDateTime(readerProject["start_date"]);
                    milestoneModel.end_date = Convert.ToDateTime(readerProject["end_date"]);
                    milestoneModel.milestone_status = readerProject["milestone_status"].ToString();
                    int tmp_status = Convert.ToInt16(readerProject["milestone_status"]);
                    if (tmp_status == 1)
                    {
                        milestoneModel.milestone_status = "In progress";
                    }
                    else if (tmp_status == 2)
                    {
                        milestoneModel.milestone_status = "Pending";

                    }
                    else if (tmp_status == 3)
                    {
                        milestoneModel.milestone_status = "Completed";

                    }
                    else if (tmp_status == 4)
                    {
                        milestoneModel.milestone_status = "Not Yet Started";

                    }
                    milestoneModel.created_at = Convert.ToDateTime(readerProject["created_at"]);
                    milestoneModel.description = readerProject["description"].ToString();
                    milestoneModel.remarks = readerProject["remarks"].ToString();
                    milestoneModel.image_s = readerProject["image"].ToString();
                    milestoneModel.document_s = readerProject["document"].ToString();





                    viewModel.milestoneList.Add(milestoneModel);
                }
                readerMilestone.Close();


            }
            var viewModelList = new List<MilestoneViewModel> { viewModel };



            return View(viewModelList);
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

        public ActionResult SuggestionView()
        {
            MyDbContext db = new MyDbContext();
            List<Usermodel> users = db.getUserSuggestion();
            temp.usermodel = users;
            return View(temp);
        }
        [HttpPost]
        public ActionResult SuggestionView(Usermodel user)
        {
            MyDbContext db = new MyDbContext();
            var userId = SessionHelper.Get<int>("user_id");

            List<Usermodel> users = db.getUserSuggestion();
            temp.usermodel = users;
            bool result = db.addSuggestion(user, userId);
            TempData["addSuggestion"] = "Feedback Added Successfully";

            if (result)
            {
                string message = "Suggestion Added Successfully";
                return Json(new { success = true, Message = message });
            }
            else
            {
                string error = "Suggestion not send..!";
                return Json(new { success = false, error = error });
            }
        }

        public ActionResult SuggestionHistoryView()
        {
            MyDbContext db = new MyDbContext();
            List<Usermodel> suggestionHistory = db.displayCitizenSuggestion();
            return View(suggestionHistory);
        }

        [HttpPost]
        public ActionResult SuggestionHistoryView(SuggestionModel suggestion)
        {
            MyDbContext db = new MyDbContext();

            db.editCitizenSuggestion(suggestion);
            TempData["EditSuggestion"] = "Suggestion Updated Successfully";
            return Json(new { success = true });
        }

        [HttpPost]
        public JsonResult removeSuggestion(SuggestionModel suggestion)
        {
            MyDbContext context = new MyDbContext();
            context.removeSuggestion(suggestion);
            TempData["RemoveSuggestion"] = "Suggestion Removed Successfully";
            return Json(new { Success = true });
        }

        public ActionResult UserView()
        {
            MyDbContext context = new MyDbContext();

            List<Usermodel> user1 = context.getUser();
            List<WardModel> wards = context.getWardName();
            List<RoleModel> role = context.getRole();

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
            return Json(new { Success = true });
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
        public ActionResult GetProjectById(int id)
        {
            MyDbContext context = new MyDbContext();

            List<ProjectModel> project = new List<ProjectModel>();

            project = context.getProjectById(id);
            return Json(new { sucess = true, project = project });
        }
        public ActionResult disableProject(string projectId)
        {

            int projId = Convert.ToInt32(projectId);

            int count = 0;
            using (NpgsqlConnection connection = new NpgsqlConnection())
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["MyconnectionString"].ToString();
                connection.Open();
                string updateProject = "";
                updateProject = "UPDATE public.project SET is_active=false where project_id=" + projId;

                NpgsqlCommand cmd = new NpgsqlCommand(updateProject, connection);

                cmd.Connection = connection;
                count = cmd.ExecuteNonQuery();
            }
            if (count > 0)
            {
                return Json(new { success = true });
            }
            return Json(new { error = true });

        }
        public ActionResult updateProject(ProjectModel projectModel)
        {

            Console.WriteLine(projectModel);

            if (projectModel.tender_doc != null)
            {
                try
                {
                    // Ensure directories exist
                    string tenderDocDirectory = Server.MapPath("~/tenderDocs");

                    Directory.CreateDirectory(tenderDocDirectory);

                    // Save tender document
                    string tenderDocFileName = Path.GetFileName(projectModel.tender_doc.FileName);
                    string tenderDocPath = Path.Combine(tenderDocDirectory, tenderDocFileName);
                    projectModel.tender_doc.SaveAs(tenderDocPath);
                    projectModel.tender_doc_s = tenderDocFileName.Trim();
                    // Save approval letter

                }
                catch (Exception ex)
                {
                    return Json(new { success = false, error = ex.Message });
                }
            }
            if (projectModel.approval_letter != null)
            {
                try
                {
                    string approvalLetterDirectory = Server.MapPath("~/approvalLetters");
                    Directory.CreateDirectory(approvalLetterDirectory);
                    string approvalLetterFileName = Path.GetFileName(projectModel.approval_letter.FileName);
                    string approvalLetterPath = Path.Combine(approvalLetterDirectory, approvalLetterFileName); //filename
                    projectModel.approval_letter.SaveAs(approvalLetterPath);
                    projectModel.approval_letter_s = approvalLetterFileName.Trim().ToString();
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, error = ex.Message });
                }

            }

            int count = 0;
            using (NpgsqlConnection connection = new NpgsqlConnection())
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["MyconnectionString"].ToString();
                connection.Open();
                string updateProject = "";
                updateProject = "UPDATE public.project SET project_title='" + projectModel.project_title + "',work_order_no='" + projectModel.work_order_no + "',description='" + projectModel.description + "',start_date='" + projectModel.start_date + "',end_date='" + projectModel.end_date + "',s_latitude='" + projectModel.s_latitude + "',s_longitude='" + projectModel.s_longitude + "',e_latitude='" + projectModel.e_latitude + "',e_longitude='" + projectModel.e_longitude + "',updated_at='" + DateTime.Now + "',allocated_contractor_id='" + projectModel.allocated_contractor_id + "',budget_cost='" + projectModel.budget_cost + "',ward_id='" + projectModel.ward_id + "',approval_letter='" + projectModel.approval_letter_s + "',tender_doc='" + projectModel.tender_doc_s + "',project_type='" + projectModel.project_type + "',status='" + projectModel.status + "' WHERE project_id=" + projectModel.project_id;

                NpgsqlCommand cmd = new NpgsqlCommand(updateProject, connection);

                cmd.Connection = connection;
                count = cmd.ExecuteNonQuery();
            }
            if (count > 0)
            {
                return Json(new { success = true });
            }
            return Json(new { error = true });

        }
        [HttpPost]
        public JsonResult sendEmailToWardOfficer(string Wemail, string Wpassword)
        {
            MyDbContext context = new MyDbContext();
            context.sendEmailToWardOfficer(Wemail, Wpassword);
            return Json(new { success = true });
        }

        [HttpPost]
        public JsonResult sendEmailToAdmin(string Aemail, string Apassword)
        {
            MyDbContext context = new MyDbContext();
            context.sendEmailToWardOfficer(Aemail, Apassword);
            return Json(new { Success = true });
        }

        [HttpPost]
        public JsonResult sendEmailToContractor(string Wemail)
        {
            MyDbContext context = new MyDbContext();
            context.sendEmailToContractor(Wemail);
            return Json(new { Success = true });
        }
        // for fetch data from db and display on chart
        [HttpGet]
        public ActionResult PendingProjectsData()
        {
            MyDbContext db = new MyDbContext();
            // Fetch pending projects data from the database
            Dictionary<string, int> pendingData = db.PendingProjects(); /* code to fetch data from database */;
            return Json(pendingData, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult OngoingProjectsData()
        {
            MyDbContext db = new MyDbContext();
            // Fetch pending projects data from the database
            Dictionary<string, int> pendingData = db.OngoingProjects(); /* code to fetch data from database */;
            return Json(pendingData, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CompletedProjectsData()
        {
            MyDbContext db = new MyDbContext();
            // Fetch pending projects data from the database
            Dictionary<string, int> pendingData = db.CompletedProjects(); /* code to fetch data from database */;
            return Json(pendingData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ProfileView()
        {
            MyDbContext db = new MyDbContext();
            List<Usermodel> profiledata = db.displayProfileData();
            List<WardModel> wards = db.getWardName();
            temp.wardmoel = wards;
            temp.usermodel = profiledata;
            
            return View(temp);
        }
        [HttpPost]
       public ActionResult ProfileView(Usermodel user)
        {
            MyDbContext db = new MyDbContext();

            if(user.password != null)
            {
                db.updateProfileDetails(user);
                return Json(new { Success = true });
            }
            else
            {
                db.updateProfileDetailsWithOutPassword(user);
                return Json(new { Success = true });
            }
           
        }

    }
}