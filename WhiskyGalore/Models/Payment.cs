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
    public class Payment
    {

        public enum Card
        {
            Visa = 1,
            Mastercard,
            Maestro,
            AmericanExpress
        }

        //paymentdetails table fields
        [Required(ErrorMessage = "*can not be blank!")]
        [DisplayName("Card Type*")]
        public Card cardType { get; set; }
        [Required(ErrorMessage = "*can not be blank!")]
        [StringLength(100, ErrorMessage = "can not exceed 100 characters")]
        [DisplayName("First Name on card*")]
        public string cardForename { get; set; }
        [Required(ErrorMessage = "*can not be blank!")]
        [StringLength(100, ErrorMessage = "can not exceed 100 characters")]
        [DisplayName("Surname on card*")]
        public string cardSurname { get; set; }
        [Required(ErrorMessage = "can not be blank")]
        [DisplayName("Card No.*")]
        public string cardNo { get; set; }
        [Required(ErrorMessage = "*can not be blank!")]
        [DisplayName("Start Date*")]
        public DateTime startDate { get; set; }
        [Required(ErrorMessage = "*can not be blank!")]
        [DisplayName("End Date*")]
        public DateTime endDate { get; set; }
        [Required(ErrorMessage = "can not be blank")]
        [DisplayName("Issue No.*")]
        public int issueNo { get; set; }

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

        public Payment(){}
         public Payment (string username, string accountType)
        {
            this.dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("getPayment", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@accountType", accountType);
                    MySqlDataReader reader = null;
                    reader = cmd.ExecuteReader();

                    while(reader.Read())
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

                        string c = reader.GetString("creditCardType");
                        cardType = (Card)Enum.Parse(typeof(Card), c);
                        startDate = reader.GetDateTime("startDate");
                        endDate = reader.GetDateTime("expiryDate");
                        issueNo = reader.GetInt32("issueNumber");
                        cardNo = reader.GetString("cardNumber");
                        cardForename = reader.GetString("fName");
                        cardSurname = reader.GetString("lName");
                       
                        
                    }

                    reader.Close();
                    con.Close();
                    
                }
            }
        }

        public void completePayment(Payment p, String username, String accountType)
        {
            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("completePayment", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                   
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@accountType", accountType);                    

                    //params for address
                    cmd.Parameters.AddWithValue("@firstLine", p.firstLine);
                    if (p.secondLine != null)
                    {
                        cmd.Parameters.AddWithValue("@secondLine", p.secondLine);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@secondLine", null);
                    }
                    cmd.Parameters.AddWithValue("@town", p.town);
                    cmd.Parameters.AddWithValue("@postcode", p.postcode);
                    cmd.Parameters.AddWithValue("@region", p.region);
                    cmd.Parameters.AddWithValue("@country", p.country);           

                    //params for insert into paymentdetails
                    cmd.Parameters.AddWithValue("@creditCardType", p.cardType.ToString());
                    cmd.Parameters.AddWithValue("@fName", p.cardForename);
                    cmd.Parameters.AddWithValue("@lName", p.cardSurname);
                    cmd.Parameters.AddWithValue("@cardNumber", p.cardNo);
                    cmd.Parameters.AddWithValue("@startDate", p.startDate);
                    cmd.Parameters.AddWithValue("@expiryDate", p.endDate);
                    cmd.Parameters.AddWithValue("@issueNumber", p.issueNo);

                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }
        }

        public void updatePayment(Payment p, String username, String accountType)
        {
            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("updatePayment", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@accountType", accountType); 

                    //params for address
                    cmd.Parameters.AddWithValue("@firstLine", p.firstLine);
                    if (p.secondLine != null)
                    {
                        cmd.Parameters.AddWithValue("@secondLine", p.secondLine);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@secondLine", null);
                    }
                    cmd.Parameters.AddWithValue("@town", p.town);
                    cmd.Parameters.AddWithValue("@postcode", p.postcode);
                    cmd.Parameters.AddWithValue("@region", p.region);
                    cmd.Parameters.AddWithValue("@country", p.country);

                    //params for insert into paymentdetails
                    cmd.Parameters.AddWithValue("@creditCardType", p.cardType.ToString());
                    cmd.Parameters.AddWithValue("@fName", p.cardForename);
                    cmd.Parameters.AddWithValue("@lName", p.cardSurname);
                    cmd.Parameters.AddWithValue("@cardNumber", p.cardNo);
                    cmd.Parameters.AddWithValue("@startDate", p.startDate);
                    cmd.Parameters.AddWithValue("@expiryDate", p.endDate);
                    cmd.Parameters.AddWithValue("@issueNumber", p.issueNo);

                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }
        }
    }
}