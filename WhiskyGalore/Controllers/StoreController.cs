using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhiskyGalore.Models;

namespace WhiskyGalore.Controllers
{
    public class StoreController : Controller
    {
        //
        // GET: /Store/

        public ActionResult Index()
        {
           Category categories = new Category();
            return View(categories);
        }

        //
        // GET: /Store/Browse

        public ActionResult Browse(int categoryId)
        {
            if (Session["loggedIn"] != null)
            {
                string accountType = Session["account"].ToString();
                Category category = new Category(categoryId, accountType);
                return View(category);
            }
            else
            {
                Category category = new Category(categoryId, "Personal");
                return View(category);
            }
               
            
        }

        //
        // GET: /Store/Details

        public ActionResult Details(int productId)
        {
            Product product = new Product(productId);
            return View(product);
        }

    }
}
