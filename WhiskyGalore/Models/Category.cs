using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Configuration;
using WhiskyGalore.Libs;
using System.Diagnostics;
using System.ComponentModel;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace WhiskyGalore.Models
{
    public class Category
    {
        public string categoryName { get; set; }
        public int categoryID { get; set; }
        public string categoryDescription { get; set; }
        public DataTable dtc { get; set; }
        public DataTable dtstaff { get; set; }
        public DataTable dtfea { get; set; }
        public DataTable dts { get; set; }
         public DataTable dtp { get; set; }
        private String con_str = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString.ToString();


        //Get all category details
        public Category()
        {
            this.dtc = new DataTable();

            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("getMinCategoryDetails", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    sda.Fill(dtc);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

            }
           
        }

        //used to return all details specified by category
        public Category(int categoryID, string accountType)
        {
            this.dtc = new DataTable();
            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("getAllCategoryDetails", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@_categoryId", categoryID);
                    MySqlDataReader reader = null;
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        categoryID = reader.GetInt32("categoryId");
                        categoryName = reader.GetString("categoryName");
                        categoryDescription = reader.GetString("catDescription");
                        

                    }

                    reader.Close();
                    con.Close();

                }
            }
            getProductsForCategories(categoryID, accountType);
        }

        public void getProductsForCategories(int categoryID, string accountType)
        {
            this.dtp = new DataTable();

            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("getProductsForCategory", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@categoryID", categoryID);
                    cmd.Parameters.AddWithValue("@accountType", accountType);
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    sda.Fill(dtp);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

            }
        }

             public void getProductsForSearch(string searchTerm, string accountType)
        {
            this.dts = new DataTable();

            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("getProductsForSearch", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@searchTerm", searchTerm);
                    cmd.Parameters.AddWithValue("@accountType", accountType);
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    sda.Fill(dts);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

            }
        }

             public void getProductsForStaff()
             {
                 this.dtstaff = new DataTable();

                 using (MySqlConnection con = new MySqlConnection(con_str))
                 {
                     con.Open();
                     using (MySqlCommand cmd = new MySqlCommand("getProductsForStaffPick", con))
                     {
                         cmd.CommandType = CommandType.StoredProcedure;
                         MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                         sda.Fill(dtstaff);
                         cmd.ExecuteNonQuery();
                         con.Close();
                     }

                 }
             }

             public void getProductsForFeatured()
             {
                 this.dtfea = new DataTable();

                 using (MySqlConnection con = new MySqlConnection(con_str))
                 {
                     con.Open();
                     using (MySqlCommand cmd = new MySqlCommand("getProductsForFeatured", con))
                     {
                         cmd.CommandType = CommandType.StoredProcedure;
                         MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                         sda.Fill(dtfea);
                         cmd.ExecuteNonQuery();
                         con.Close();
                     }

                 }
             }
    }

}