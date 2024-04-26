using GIS_ROAD_ASSET_MANAGEMENT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

<<<<<<< HEAD

=======
>>>>>>> origin/branch-yashvi
namespace GIS_ROAD_ASSET_MANAGEMENT.Controllers
{
    public class HomeController : Controller
    { 
        MyDbContext context = new MyDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult Navbar()
        {

<<<<<<< HEAD
            WardModel ward = new WardModel();
=======
            //WardModel ward = new WardModel();
>>>>>>> origin/branch-yashvi
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