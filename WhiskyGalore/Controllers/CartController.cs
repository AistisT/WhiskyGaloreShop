using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhiskyGalore.Models;

namespace WhiskyGalore.Controllers
{
    public class CartController : Controller
    {
        // GET: /Cart/
        public ActionResult Index()
        {
            
            
            if (Session["CartSessionKey"] == null)
            {
                if (Session["loggedIn"] != null)
                {
                    Session["CartSessionKey"] = Session["loginName"].ToString();
                    ShoppingCart cart = new ShoppingCart(Session["loginName"].ToString()); 
                    Session["cart"] = cart;
                    View(cart);
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    // Send tempCartId back to client as a cookie
                    Session["CartSessionKey"] = tempCartId.ToString();
                    ShoppingCart cart = new ShoppingCart(tempCartId.ToString());                    
                    Session["cart"] = cart;
                    View(cart);
                }
            }
            else
            {
                ShoppingCart cart = (ShoppingCart)Session["cart"];
                cart.setTotals();
                Session["itemCount"] = cart.totalItemCount;
                return View(cart);
            }

            return View();
        }

        //
        // GET: /Store/AddToCart
        public ActionResult AddToCart(int productId, decimal unitPrice, string productName, float productWeight)
        {
            if (Session["CartSessionKey"] == null)
            {
               

                if (Session["loggedIn"] != null)
                {
                    Session["CartSessionKey"] = Session["loginName"].ToString();
                    ShoppingCart cart = new ShoppingCart(Session["loginName"].ToString());
                    cart.addToCart(productId, unitPrice, productName, productWeight);
                    Session["cart"] = cart;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    // Send tempCartId back to client as a cookie
                    Session["CartSessionKey"] = tempCartId.ToString();
                    ShoppingCart cart = new ShoppingCart(tempCartId.ToString());
                    cart.addToCart(productId, unitPrice, productName, productWeight);
                    Session["cart"] = cart;
                }
            }
            else
            {
                
                ShoppingCart cart = (ShoppingCart) Session["cart"];
                cart.addToCart(productId, unitPrice, productName, productWeight);
            }
            return RedirectToAction("Index");
        }

        // POST: /Store/RemoveFromCart
        public ActionResult RemoveFromCart(int productId)
        {
            ShoppingCart cart = (ShoppingCart)Session["cart"];
            cart.removeFromCart(productId);
            Session["cart"] = cart;
            cart.setTotals();
            Session["itemCount"] = cart.totalItemCount;
            return View("Index", cart);
        }

    }
}