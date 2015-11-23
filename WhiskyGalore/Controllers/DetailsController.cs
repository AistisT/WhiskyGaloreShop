using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhiskyGalore.Models;

namespace WhiskyGalore.Controllers
{
    public class DetailsController : Controller
    {

        // GET: /Details/ConsumerDetails
        public ActionResult ConsumerDetails()
        {
            if (Session["loggedIn"] != null)
            {
                return View(new Customer());
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
           
        }

        // POST: /Details/ConsumerDetails
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConsumerDetails(Customer cust)
        {
            if (ModelState.IsValid)
            {
                String username = Session["loginName"].ToString();
                cust.completeConsumer(cust, username);
            }
            else
            {

                return View(cust);
            }
            return RedirectToAction("Index", "Checkout");
        }

        // GET: /Details/PaymentDetails
        public ActionResult PaymentDetails()
        {

            if (Session["loggedIn"] != null)
            {
                return View(new Payment());
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        // POST: /Details/PaymentDetails
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PaymentDetails(Payment p)
        {
            if (ModelState.IsValid)
            {
                String username = Session["loginName"].ToString();
                String accountType = Session["account"].ToString();
                p.completePayment(p, username, accountType);
            }
            else
            {

                return View(p);
            }
            return RedirectToAction("Index", "Checkout");
        }

        // GET: /Details/UpdateConsumer
        public ActionResult UpdateConsumer()
        {
            if (Session["loggedIn"] != null)
            {
                String username = Session["loginName"].ToString();
                Customer cust = new Customer(username);
                return View(cust);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            } 
                      
        }


        // POST: /Details/UpdateConsumer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateConsumer(Customer cust)
        {
            if (ModelState.IsValid)
            {
                String username = Session["loginName"].ToString();
                cust.updateConsumer(cust, username);
            }
            else
            {

                return View(cust);
            }
            return RedirectToAction("Index", "Checkout");
        }

        // GET: /Details/UpdatePayment
        public ActionResult UpdatePayment()
        {
            if (Session["loggedIn"] != null)
            {
                ModelState.Clear();
                String username = Session["loginName"].ToString();
                String accountType = Session["account"].ToString();
                Payment pay = new Payment(username, accountType);

                return View(pay);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            } 
           
        }

        // POST: /Details/UpdatePayment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdatePayment(Payment p)
        {
            if (ModelState.IsValid)
            {
                String username = Session["loginName"].ToString();
                String accountType = Session["account"].ToString();
                p.updatePayment(p, username, accountType);
            }
            else
            {

                return View(p);
            }
            return RedirectToAction("Index", "Checkout");
        }
    }
}