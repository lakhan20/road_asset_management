using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GIS_ROAD_ASSET_MANAGEMENT.Filters
{
    public class UserAuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["user_id"] == null)
            {
                // If user is not logged in, redirect to login page with message
                filterContext.Controller.ViewBag.ErrorMessage = "Please login to access this page.";
                filterContext.Result = new RedirectResult("~/Home/Index");
            }
            else
            {
                base.OnActionExecuting(filterContext);
            }
        }


    }
}