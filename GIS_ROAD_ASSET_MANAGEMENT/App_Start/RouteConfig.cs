<<<<<<< HEAD
﻿using System;
=======
﻿    using System;
>>>>>>> origin/branch-yashvi
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GIS_ROAD_ASSET_MANAGEMENT
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
<<<<<<< HEAD

=======
            Console.WriteLine("asd");
>>>>>>> origin/branch-yashvi
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
