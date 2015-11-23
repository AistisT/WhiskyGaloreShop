using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhiskyGalore.Models;

namespace WhiskyGalore.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            Category cat = new Category();
            cat.getProductsForFeatured();
            cat.getProductsForStaff();
            return View(cat);
        }

    }
}
