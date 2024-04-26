using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using GIS_ROAD_ASSET_MANAGEMENT.Models;

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
            ViewBag.role = "Admin";
            return View();
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

        public ActionResult suggestionView()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult UserSeggestionHistoryView()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public JsonResult sendEmailToWardOfficer(string Wemail, string Wpassword)
        {
            MyDbContext context = new MyDbContext();
            context.sendEmailToWardOfficer(Wemail, Wpassword);
            return Json(new { Success = true });
        }

        [HttpPost]
        public JsonResult sendEmailToAdmin(string Aemail, string Apassword)
        {
            MyDbContext context = new MyDbContext();
            context.sendEmailToWardOfficer(Aemail,Apassword);
            return Json(new { Success = true });
        }

        [HttpPost]
        public JsonResult sendEmailToContractor(string Wemail)
        {
            MyDbContext context = new MyDbContext();
            context.sendEmailToContractor(Wemail);
            return Json(new { Success = true });
        }

    }
}