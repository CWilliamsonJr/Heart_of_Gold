﻿using System.Web;
using System.Web.Mvc;

namespace Hearts_of_Gold
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
