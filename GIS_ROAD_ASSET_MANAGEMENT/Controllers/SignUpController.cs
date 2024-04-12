using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GIS_ROAD_ASSET_MANAGEMENT.Models;
using System.Web.Mvc;

namespace GIS_ROAD_ASSET_MANAGEMENT.Controllers
{
    public class SignUpController : Controller
    {
        // GET: SignUp
        public JsonResult signUp(Usermodel user)
        {

            return Json("true",JsonRequestBehavior.AllowGet);
        }
    }
}