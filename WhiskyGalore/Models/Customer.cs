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
    public class Customer
    {
        public enum Title
        {
            Mr = 1,
            Mrs,
            Miss,
            Ms,
            Dr,
            Prof
        }

        [DisplayName("Date of Birth*")]
        public DateTime dob { get; set; }

        //contact table fields
        [Required(ErrorMessage = "*can not be blank!")]
        [DisplayName("Title*")]
        public Title title { get; set; }
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
        [Required(ErrorMessage = "*can not be blank!")]
        [StringLength(100, ErrorMessage = "can not exceed 100 characters")]
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
        [StringLength(10, ErrorMessage = "can not exceed 10 characters")]
        [DisplayName("Postcode*")]
        public string postcode { get; set; }
        [Required(ErrorMessage = "*can not be blank!")]
        [StringLength(50, ErrorMessage = "can not exceed 50 characters")]
        [DisplayName("Region*")]
        public string region { get; set; }
        [Required(ErrorMessage = "*can not be blank!")]
        [StringLength(100, ErrorMessage = "can not exceed 100 characters")]
        [DisplayName("Country*")]
        public string country { get; set; }

        public DataTable dt { get; set; }

        private String con_str = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString.ToString();

        public void updateConsumer(Customer cust, String username)
        {
            using (MySqlConnection con = new MySqlConnection(con_str))
            {

                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("updateConsumer", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Get username

                    cmd.Parameters.AddWithValue("@username", username);

                    //params for insert into contact
                    cmd.Parameters.AddWithValue("@title", cust.title.ToString());
                    cmd.Parameters.AddWithValue("@forename", cust.forename);
                    cmd.Parameters.AddWithValue("@surname", cust.surname);
                    cmd.Parameters.AddWithValue("@firstNumber", cust.firstNumber);
                    if (cust.secondaryNumber != null)
                    {
                        cmd.Parameters.AddWithValue("@secondaryNumber", cust.secondaryNumber);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@secondaryNumber", null);
                    }
                    cmd.Parameters.AddWithValue("@email", cust.email);
                    if (cust.fax != null)
                    {
                        cmd.Parameters.AddWithValue("@fax", cust.fax);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@fax", null);
                    }
                    if (cust.dob != null)
                    {
                        cmd.Parameters.AddWithValue("@dob", cust.dob.ToString("yyyy-MM-dd"));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@dob", null);
                    }


                    //params for insert into address
                    cmd.Parameters.AddWithValue("@firstLine", cust.firstLine);
                    if (cust.secondLine != null)
                    {
                        cmd.Parameters.AddWithValue("@secondLine", cust.secondLine);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@secondLine", null);
                    }
                    cmd.Parameters.AddWithValue("@town", cust.town);
                    cmd.Parameters.AddWithValue("@postcode", cust.postcode);
                    cmd.Parameters.AddWithValue("@region", cust.region);
                    cmd.Parameters.AddWithValue("@country", cust.country);


                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }
        }

        public void completeConsumer(Customer cust, String username)
        {
            using (MySqlConnection con = new MySqlConnection(con_str))
            {

                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("completeConsumer", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Get username

                    cmd.Parameters.AddWithValue("@username", username);

                    //params for insert into contact
                    cmd.Parameters.AddWithValue("@title", cust.title.ToString());
                    cmd.Parameters.AddWithValue("@forename", cust.forename);
                    cmd.Parameters.AddWithValue("@surname", cust.surname);
                    cmd.Parameters.AddWithValue("@firstNumber", cust.firstNumber);
                    if (cust.secondaryNumber != null)
                    {
                        cmd.Parameters.AddWithValue("@secondaryNumber", cust.secondaryNumber);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@secondaryNumber", null);
                    }
                    cmd.Parameters.AddWithValue("@email", cust.email);
                    if (cust.fax != null)
                    {
                        cmd.Parameters.AddWithValue("@fax", cust.fax);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@fax", null);
                    }
                    if (cust.dob != null)
                    {
                        cmd.Parameters.AddWithValue("@dob", cust.dob.ToString("yyyy-MM-dd"));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@dob", null);
                    }


                    //params for insert into address
                    cmd.Parameters.AddWithValue("@firstLine", cust.firstLine);
                    if (cust.secondLine != null)
                    {
                        cmd.Parameters.AddWithValue("@secondLine", cust.secondLine);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@secondLine", null);
                    }
                    cmd.Parameters.AddWithValue("@town", cust.town);
                    cmd.Parameters.AddWithValue("@postcode", cust.postcode);
                    cmd.Parameters.AddWithValue("@region", cust.region);
                    cmd.Parameters.AddWithValue("@country", cust.country);


                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }
        }

        public Customer() { }

         public Customer (string username)
        {
            this.dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("getConsumerDetails", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@username", username);
                    MySqlDataReader reader = null;
                    reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                       
                        dob = reader.GetDateTime("dob");
                       
                        
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
                        title = (Title) Enum.Parse(typeof(Title), t);
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

    }
}