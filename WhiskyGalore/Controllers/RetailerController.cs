using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhiskyGalore.Models;

namespace WhiskyGalore.Controllers
{
    public class RetailerController : Controller
    {
        [HttpGet]
        public ActionResult Details()
        {
            if (Session["loggedIn"] != null)
            {
                Retailer r = new Retailer();
                return View(r);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            } 
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(Retailer r)
        {
            if (ModelState.IsValid)
            {
                String username = Session["loginName"].ToString();
                r.completeRetailer(r, username);
            }
            else
            {
                return View(r);
            }
            return RedirectToAction("Index", "Home");
        }


        // GET: /Retailer/UpdateRetailer
        [HttpGet]
        public ActionResult UpdateRetailer()
        {
            if (Session["loggedIn"] != null)
            {
                String username = Session["loginName"].ToString();
                System.Diagnostics.Debug.WriteLine("UpdateRetailer Controller");
                Retailer r = new Retailer();
                r.getRetailer(username);

                return View(r);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            } 
            
        }



        // POST: /Retailer/UpdateRetailer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateRetailer(Retailer r)
        {
            /*  if (ModelState.IsValid)
              { */
            String username = Session["loginName"].ToString();

            r.updateRetailer(r, username);
            /*   }
              else
              {
                  return View(r);
              }  */
            return RedirectToAction("Index", "Checkout");
        }




    }
}