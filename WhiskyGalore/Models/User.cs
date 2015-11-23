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
    public class User
    {
        public enum AccountType
        {
            Personal = 1,
            Business
        }
      

        [Required(ErrorMessage = "*can not be blank!")]
        [StringLength(50, ErrorMessage = "can not exceed 50 characters")]
        [DisplayName("Username*")]
        public string username { get; set; }

        [Required(ErrorMessage = "*can not be blank!")]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "must be between 6-16 chars")]
        [DisplayName("Password*")]
        public string password { get; set; }

        [Required(ErrorMessage = "*can not be blank!")]
        [DisplayName("Type of Account*")]
        public AccountType accountType { get; set; }

        public bool loggedIn { get; set; }
        public bool registered { get; set; }

        public DataTable dt { get; set; }


        private String con_str = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString.ToString();

      

        public void registerUser(User user)
        {
            using (MySqlConnection con = new MySqlConnection(con_str))
            {

                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("registerUser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@username", user.username);
                    cmd.Parameters.AddWithValue("@password", Encryption.Encrypt(user.password));
                    cmd.Parameters.AddWithValue("@accountType", user.accountType.ToString());

                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }
        }

        public Boolean loginUser(User user)
        {
            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                if (checkLogin(user))
                {
                    loggedIn = true;

                    return true;
                }
                else
                    return false;
                
            }
        }

        public Boolean checkLogin(User user)
        {
            this.dt = new DataTable();
            String checkedUsername = "";
            String checkedPassword = "";

            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("getUserDetails", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@username", username);
                    MySqlDataReader reader = null;
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        checkedUsername = reader.GetString("username");
                        checkedPassword = Encryption.Decrypt(reader.GetString("password"));
                        Debug.WriteLine("PASSWORD " + password);
                        string s = reader.GetString("accountType");
                        accountType = (AccountType)Enum.Parse(typeof(AccountType), s);
                    }

                    reader.Close();
                    con.Close();

                }
            }
            if (password.Equals(checkedPassword))
            {
                password = checkedPassword;
                username = checkedUsername;
                
                return true;
            }
            else
                return false;

        }
    }
}
               
            
        

    

    

 
    
