using GIS_ROAD_ASSET_MANAGEMENT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GIS_ROAD_ASSET_MANAGEMENT.Controllers
{
    public class HomeController : Controller
    { 
        MyDbContext context = new MyDbContext();
        public ActionResult Index()
        {
            return View();
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