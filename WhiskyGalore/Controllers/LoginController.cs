using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhiskyGalore.Models;


namespace WhiskyGalore.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login
        public ActionResult Index()
        {
            return View(new User());
        }


        //
        // POST: /Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {

                if (user.loginUser(user))
                {
                    String loginName = user.username;
                    String account = user.accountType.ToString();
                    String loggedIn = user.loggedIn.ToString();
                    

                    //Store in session
                    Session["loginName"] = loginName;
                    Session["account"] = account;
                    Session["loggedIn"] = loggedIn;

                    return RedirectToAction("Index", "Store");
                }

                else
                {

                    ModelState.AddModelError("", "The user name or password provided is incorrect." );
                    return View(user);
                }
            }else
            {
                
                return View(user);
            }

           
        }
        //
        // GET: /LogOut


        public ActionResult LogOut()
        {
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Index", "Home");
        }
    }
}

