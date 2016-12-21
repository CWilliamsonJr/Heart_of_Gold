using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hearts_of_Gold.Controllers
{
    public class HomeController : Controller
    {
        // GET: Homess
        public ActionResult Index()
        {
            return View();
        }
    }
}