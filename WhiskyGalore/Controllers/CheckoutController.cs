using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhiskyGalore.Models;


namespace WhiskyGalore.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: /Checkout/
        public ActionResult Index()
        {
            if (Session["CartSessionKey"] == null)
            {
                if (Session["loggedIn"] != null)
                {
                    Session["CartSessionKey"] = Session["loginName"].ToString();
                    ShoppingCart cart = new ShoppingCart(Session["loginName"].ToString());
                    String accountType = Session["account"].ToString();
                    Session["cart"] = cart;
                    Order order = new Order(Session["loginName"].ToString(), accountType, cart);
                    View(order);
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    // Send tempCartId back to client as a cookie
                    Session["CartSessionKey"] = tempCartId.ToString();
                    ShoppingCart cart = new ShoppingCart(tempCartId.ToString());
                    String accountType = "Personal";
                    Session["cart"] = cart;
                    Order order = new Order(null, accountType, cart);
                    View(order);
                }
            }
            else
            {
                if (Session["loggedIn"] != null)
                {
                    ShoppingCart cart = (ShoppingCart)Session["cart"];
                    String accountType = Session["account"].ToString();
                    Order order = new Order(Session["loginName"].ToString(), accountType, cart);
                    return View(order);
                }
                else
                {
                    ShoppingCart cart = (ShoppingCart)Session["cart"];
                    String accountType = "Personal";
                    Order order = new Order(null, accountType, cart);
                    return View(order);
                }
            }

            return View();
        }

    
        // Post: /Checkout/Complete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Complete(Order order, decimal shippingCost)
        {
            if (ModelState.IsValid)
            {
                ShoppingCart cart = (ShoppingCart)Session["cart"];
                String accountType = Session["account"].ToString();
                Order newOrder = new Order(Session["loginName"].ToString(),accountType, cart);
                newOrder.shippersId = order.shippersId;
                
                return RedirectToAction("CompleteCheckout", newOrder);
            }
            else
            {
                return View(order);
            }
        }

        // Post: /Checkout/CompleteOrderProcess
       
        public ActionResult CompleteOrderProcess(Order order, int shippersID)
        {
            if (ModelState.IsValid)
            {
                ShoppingCart cart = (ShoppingCart)Session["cart"];
                String accountType = Session["account"].ToString();
                Order newOrder = new Order(Session["loginName"].ToString(), accountType, cart);
                newOrder.shippersId = Convert.ToInt32(shippersID);
                newOrder.getSingleShippersDetails();
                newOrder.getTotals();
                newOrder.completeOrderInfo(Session["loginName"].ToString(), Session["account"].ToString());

                ShoppingCart newCart = new ShoppingCart(Session["loginName"].ToString());
                newCart.setTotals();
                Session["itemCount"] = newCart.totalItemCount;
                Session["cart"] = newCart;

                return RedirectToAction("CompleteOrder");
            }
            else
            {
                return View(order);
            }
        }

        // GET: /Checkout/CompleteCheckout
        public ActionResult CompleteCheckout(Order order)
        {
            ShoppingCart cart = (ShoppingCart)Session["cart"];
            String accountType = Session["account"].ToString();
            Order newOrder = new Order(Session["loginName"].ToString(), accountType, cart);
            newOrder.shippersId = order.shippersId;
            newOrder.getSingleShippersDetails();
            newOrder.getTotals();
            return View(newOrder);
        }

         // GET: /Checkout/CompleteOrder
        public ActionResult CompleteOrder()
        {
           
            
            return View();
        }
    }
}