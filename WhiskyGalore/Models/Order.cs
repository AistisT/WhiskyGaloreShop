using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace WhiskyGalore.Models
{
    public class Order
    {
        public ShoppingCart cart { get; set; } 
        public DateTime orderDate { get; set; }
        public string timeStamp = "";
        public Customer customerDetails { get; set; }
        public string country { get; set; }
        public Retailer retailerDetails { get; set; }
        public Payment paymentDetails { get; set; } 
        public decimal shippingIntRate { get; set; }
        public decimal shippingUKRate { get; set; }
        public decimal shippingCost { get; set; }
        public decimal tax { get; set; }
        public decimal totalCost { get; set; }
        [Required(ErrorMessage = "OrderAction *must be selected")]
        public int shippersId { get; set; }
        public DataTable shipTable { get; set; }
        private String con_str = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString.ToString();

        public Order(string username, String accountType, ShoppingCart cart)
        {
            this.cart = (ShoppingCart) cart;

            if (accountType.Equals("Personal"))
            {
                customerDetails = new Customer(username);
                country = customerDetails.country;
            }
            else
            {
                retailerDetails = new Retailer();
                retailerDetails.getRetailer(username);
                country = retailerDetails.country;
            }
               
            paymentDetails = new Payment(username, accountType);
            getShippersDetails();
           
            getTotals();
        }
        public Order() { }

        public void getShippersDetails()
        {
            this.shipTable = new DataTable();

            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("getShippers", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    sda.Fill(shipTable);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

            }
        }

        public void getSingleShippersDetails()
        {


            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("getSingleShippers", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@shippersID", shippersId);
                    MySqlDataReader reader = null;
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        shippingIntRate = reader.GetDecimal("intRate");
                        shippingUKRate =  reader.GetDecimal("ukRate");
                        
                    }

                    reader.Close();
                    con.Close();

                }
            }
        }

        public void getTotals()
        {
            if (country != null)
            {
                if (country.Equals("United Kingdom") || country.Equals("united kingdom") || country.Equals("UK") || country.Equals("uk"))
                {
                    shippingCost = shippingUKRate * Convert.ToDecimal(cart.totalCartWeight);
                    tax = (shippingCost + cart.totalCartCost) / 5;
                    totalCost = shippingCost + tax + cart.totalCartCost;
                }
                else
                    shippingCost = shippingIntRate * Convert.ToDecimal(cart.totalCartWeight);
                tax = (shippingCost + cart.totalCartCost) / 5;
                totalCost = shippingCost + tax + cart.totalCartCost;
            }
            else
            {
                shippingCost = shippingUKRate * Convert.ToDecimal(cart.totalCartWeight);
                tax = (shippingCost + cart.totalCartCost) / 5;
                totalCost = shippingCost + tax + cart.totalCartCost;
            }
            
        }

        public void completeOrderInfo(string username, string accountType)
        {
            using (MySqlConnection con = new MySqlConnection(con_str))
            {

                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("completeOrder", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Get username

                    cmd.Parameters.AddWithValue("@username", username);
                    timeStamp = GetTimestamp(DateTime.Now);
                    //params for orderInfo
                    cmd.Parameters.AddWithValue("@orderStatus", "Pending");
                    cmd.Parameters.AddWithValue("@orderDate", DateTime.Today);
                    cmd.Parameters.AddWithValue("@shippingCost", shippingCost);
                    cmd.Parameters.AddWithValue("@totalPrice", totalCost);
                    cmd.Parameters.AddWithValue("@tax", tax);
                    cmd.Parameters.AddWithValue("@Shippers_shippersId", shippersId);
                    cmd.Parameters.AddWithValue("@accountType", accountType);
                    cmd.Parameters.AddWithValue("@timeStamper", timeStamp);

                    cmd.ExecuteNonQuery();           

                    con.Close();

                    foreach (var item in cart.items)
                    {
                        completeOrderProductContent(item);
                    }
                }
            }
        }
        public void completeOrderProductContent(CartItem item)
        {
            using (MySqlConnection con = new MySqlConnection(con_str))
            {

                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("completeOrderProduct", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Get TIMEstamp

                    cmd.Parameters.AddWithValue("@timeStamper", timeStamp);

                    //params for orderProduct
                    cmd.Parameters.AddWithValue("@product_productId", item.productId);
                    cmd.Parameters.AddWithValue("@units", item.count);
                    cmd.Parameters.AddWithValue("@price", item.totalPrice);
                    cmd.Parameters.AddWithValue("@weight", item.totalWeight);
                    cmd.Parameters.AddWithValue("@Shippers_shippersId", shippersId);
                    

                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }
        }

        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }

    }
}