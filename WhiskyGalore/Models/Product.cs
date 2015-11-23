using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace WhiskyGalore.Models
{
    public class Product
    {
        public int productID { get; set; }
        public string productName { get; set; }
        public string productDescription { get; set; }
        public decimal unitPrice { get; set; }
        public decimal mrrp { get; set; }
        public int unitsInStock { get; set; }
        public float unitWeight { get; set; }
        public int quantityPerUnit { get; set; }
        public string picUrl { get; set; }
        public bool featured { get; set; }
        public bool staffPick { get; set; }
        public bool wholesale { get; set; }
        public string categoryName { get; set; }
        public int categoryId { get; set; }

        public DataTable dt { get; set; }
        public DataTable dtcat { get; set; }
        private String con_str = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString.ToString();

        //used to return all details specified by product
        public Product(int productId)
        {
            
            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("getAllProductDetails", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@_productId", productId);
                    MySqlDataReader reader = null;
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                       
                        categoryName = reader.GetString("categoryName");
                        productID = reader.GetInt32("productId");
                        productName = reader.GetString("productName");
                        productDescription = reader.GetString("prodDescription");
                        unitPrice = reader.GetDecimal("unitPrice");
                        mrrp = reader.GetDecimal("mrrp");
                        unitsInStock = reader.GetInt32("unitsInStock");
                        unitWeight = reader.GetFloat("unitWeight");
                        quantityPerUnit = reader.GetInt32("quantityPerUnit");
                        picUrl = reader.GetString("picUrl");
                        featured = reader.GetBoolean("featured");
                        staffPick = reader.GetBoolean("staffPick");
                        wholesale = reader.GetBoolean("wholesale");
                        categoryId = reader.GetInt32("categoryId");
                       

                    }

                    reader.Close();
                    con.Close();

                }
            }
        }



    }
}