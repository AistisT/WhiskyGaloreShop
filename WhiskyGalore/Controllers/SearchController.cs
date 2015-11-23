using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhiskyGalore.Models;

namespace WhiskyGalore.Controllers
{
    public class SearchController : Controller
    {
        // Get: Search
        
        public ActionResult Search(string searchTerm)
        {
            if (Session["loggedIn"] != null)
            {
                string accountType = Session["account"].ToString();
                Category category = new Category();
                category.getProductsForSearch(searchTerm, accountType);
                return View(category);
            }
            else
            {
                Category category = new Category();
                category.getProductsForSearch(searchTerm, "Personal");
                return View(category);
            }
        }
    }
}