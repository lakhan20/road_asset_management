using GIS_ROAD_ASSET_MANAGEMENT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GIS_ROAD_ASSET_MANAGEMENT.Controllers
{
    public class CustomBaseController : Controller
    {
        // GET: CustomBase
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Retrieve session variables
            

            var userId = SessionHelper.Get<int>("user_id");
            ViewBag.UserId = userId;
            var roleId = SessionHelper.Get<int>("role_id");
            ViewBag.RoleId = roleId;
            var name = SessionHelper.Get<string>("name");
            ViewBag.Name = name;

            base.OnActionExecuting(filterContext);
        }
    }
}