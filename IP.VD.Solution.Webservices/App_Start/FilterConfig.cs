﻿using System.Web;
using System.Web.Mvc;

namespace IP.VD.Solution.Webservices
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
