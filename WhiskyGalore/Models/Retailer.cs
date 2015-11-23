using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace WhiskyGalore.Models
{
    public class Retailer
    {

        public enum Title
        {
            Mr,
            Mrs,
            Miss,
            Ms,
            Dr,
            Prof
        }

        //retail table fields
       
        public int retailerId { get; set; }            

        [StringLength(100, ErrorMessage = "can not exceed 100 characters")]
        [Required(ErrorMessage = "*can not be blank!")]
        [DisplayName("Branch Name*")]
        public string branch { get; set; }


        //contact table fields
        [Required(ErrorMessage = "*can not be blank!")]
        [DisplayName("Title*")]
        public Title title { get; set; }

        [Required(ErrorMessage = "*can not be blank!")]
        [StringLength(100, ErrorMessage = "can not exceed 100 characters")]
        [DisplayName("Company Name*")]
        public string Companyname { get; set; }

       
        [StringLength(100, ErrorMessage = "can not exceed 100 characters")]
        [DisplayName("Company Website")]
        public string Companywebsite { get; set; }

        [Required(ErrorMessage = "*can not be blank!")]
        [StringLength(100, ErrorMessage = "can not exceed 100 characters")]
        [DisplayName("First Name*")]
        public string forename { get; set; }

        [Required(ErrorMessage = "*can not be blank!")]
        [StringLength(100, ErrorMessage = "can not exceed 100 characters")]
        [DisplayName("Last Name*")]
        public string surname { get; set; }

        [Required(ErrorMessage = "*can not be blank!")]
        [StringLength(18, MinimumLength = 11, ErrorMessage = "must be between 11-18 digits")]
        [DisplayName("Primary Phone no.*")]
        public string firstNumber { get; set; }

        [StringLength(18, MinimumLength = 11, ErrorMessage = "must be between 11-18 digits")]
        [DisplayName("Secondary Phone no.")]
        public string secondaryNumber { get; set; }

        [StringLength(100, ErrorMessage = "can not exceed 100 characters")]
        [Required(ErrorMessage = "*can not be blank!")]
        [DisplayName("Email*")]
        public string email { get; set; }

        [StringLength(100, ErrorMessage = "can not exceed 100 characters")]
        [DisplayName("Fax")]
        public string fax { get; set; }


        //address table fields
        [Required(ErrorMessage = "*can not be blank!")]
        [StringLength(100, ErrorMessage = "can not exceed 100 characters")]
        [DisplayName("First Line of Address*")]
        public string firstLine { get; set; }

        [StringLength(100, ErrorMessage = "can not exceed 100 characters")]
        [DisplayName("Second Line of Address")]
        public string secondLine { get; set; }

        [Required(ErrorMessage = "*can not be blank!")]
        [StringLength(100, ErrorMessage = "can not exceed 100 characters")]
        [DisplayName("Town*")]
        public string town { get; set; }

        [Required(ErrorMessage = "*can not be blank!")]
        [StringLength(50, ErrorMessage = "can not exceed 10 characters")]
        [DisplayName("Postcode*")]
        public string postcode { get; set; }

        [Required(ErrorMessage = "*can not be blank!")]
        [StringLength(50, ErrorMessage = "can not exceed 50 characters")]
        [DisplayName("Region*")]
        public string region { get; set; }

        [Required(ErrorMessage = "*can not be blank!")]
        [StringLength(45, ErrorMessage = "can not exceed 100 characters")]
        [DisplayName("Country*")]
        public string country { get; set; }

        public DataTable dt { get; set; }

        private String con_str = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString.ToString();
       

        public void completeRetailer(Retailer r, String username)
        {
            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("completeRetailer", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@username", username);
                   
                    cmd.Parameters.AddWithValue("@branch", r.branch);
                    cmd.Parameters.AddWithValue("@companyName", r.Companyname);
                    
                    if (r.Companywebsite != null)
                    {
                        cmd.Parameters.AddWithValue("@companyWebsite", r.Companywebsite);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@secondaryNumber", null);
                    }
                   
                    //params for insert into contact
                    cmd.Parameters.AddWithValue("@title", r.title.ToString());
                    cmd.Parameters.AddWithValue("@forename", r.forename);
                    cmd.Parameters.AddWithValue("@surname", r.surname);

                    cmd.Parameters.AddWithValue("@firstNumber", r.firstNumber);
                    if (r.secondaryNumber != null)
                    {
                        cmd.Parameters.AddWithValue("@secondaryNumber", r.secondaryNumber);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@secondaryNumber", null);
                    }
                    cmd.Parameters.AddWithValue("@email", r.email);
                    if (r.fax != null)
                    {
                        cmd.Parameters.AddWithValue("@fax", r.fax);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@fax", null);
                    }

                    //params for insert into address
                    cmd.Parameters.AddWithValue("@firstLine", r.firstLine);
                    if (r.secondLine != null)
                    {
                        cmd.Parameters.AddWithValue("@secondLine", r.secondLine);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@secondLine", null);
                    }
                    cmd.Parameters.AddWithValue("@town", r.town);
                    cmd.Parameters.AddWithValue("@postcode", r.postcode);
                    cmd.Parameters.AddWithValue("@region", r.region);
                    cmd.Parameters.AddWithValue("@country", r.country);


                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        public void getRetailer(string username)
        {

            System.Diagnostics.Debug.WriteLine("UpdateRetailer Model");

            this.dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("getRetailerDetails", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@username", username);
                    MySqlDataReader reader = null;
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        firstLine = reader.GetString("firstLine");
                        if (!reader.IsDBNull(reader.GetOrdinal("secondLine")))
                        {
                            secondLine = reader.GetString("secondLine");
                        }
                        else
                        {
                            secondLine = null;
                        }
                        town = reader.GetString("town");
                        postcode = reader.GetString("postcode");
                        region = reader.GetString("region");
                        country = reader.GetString("country");
                        string t = reader.GetString("Title");

                        System.Diagnostics.Debug.WriteLine("reader.GetString(Title) " + t);

                        title = (Title)Enum.Parse(typeof(Title), t);
                        forename = reader.GetString("forename");
                        surname = reader.GetString("surname");
                        firstNumber = reader.GetString("firstNumber");
                        if (!reader.IsDBNull(reader.GetOrdinal("secondaryNumber")))
                        {
                            secondaryNumber = reader.GetString("secondaryNumber");
                        }
                        else
                        {
                            secondaryNumber = null;
                        }
                        email = reader.GetString("email");
                        if (!reader.IsDBNull(reader.GetOrdinal("fax")))
                        {
                            fax = reader.GetString("fax");
                        }
                        else
                        {
                            fax = null;
                        }

                    }

                    reader.Close();
                    con.Close();

                }
            }
        }

        public void updateRetailer(Retailer r, String username)
        {
            try
            {

                using (MySqlConnection con = new MySqlConnection(con_str))
                {

                    con.Open();
                    using (MySqlCommand cmd = new MySqlCommand("updateRetailer", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        //Get username

                        cmd.Parameters.AddWithValue("@username", username);

                        //params for insert into contact
                        cmd.Parameters.AddWithValue("@title", r.title.ToString());
                        cmd.Parameters.AddWithValue("@forename", r.forename);
                        cmd.Parameters.AddWithValue("@surname", r.surname);
                        cmd.Parameters.AddWithValue("@firstNumber", r.firstNumber);
                        if (r.secondaryNumber != null)
                        {
                            cmd.Parameters.AddWithValue("@secondaryNumber", r.secondaryNumber);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@secondaryNumber", null);
                        }
                        cmd.Parameters.AddWithValue("@email", r.email);
                        if (r.fax != null)
                        {
                            cmd.Parameters.AddWithValue("@fax", r.fax);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@fax", null);
                        }


                        //params for insert into address
                        cmd.Parameters.AddWithValue("@firstLine", r.firstLine);
                        if (r.secondLine != null)
                        {
                            cmd.Parameters.AddWithValue("@secondLine", r.secondLine);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@secondLine", null);
                        }
                        cmd.Parameters.AddWithValue("@town", r.town);
                        cmd.Parameters.AddWithValue("@postcode", r.postcode);
                        cmd.Parameters.AddWithValue("@region", r.region);
                        cmd.Parameters.AddWithValue("@country", r.country);


                        cmd.ExecuteNonQuery();

                        con.Close();
                    }
                }
            }

            catch
            {
                System.Diagnostics.Debug.WriteLine("updateRetailer() Fail!");
            }
        }

    }
}