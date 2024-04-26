using GIS_ROAD_ASSET_MANAGEMENT.Models;
using Microsoft.Ajax.Utilities;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
  

namespace GIS_ROAD_ASSET_MANAGEMENT.Controllers
{
    public class LoginController : Controller
    {

        // GET: Login

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            int userId = 0;
            string emailId = "";
            int roleId = 0;
            bool isActive=false;
          
            using (NpgsqlConnection conn = DatabaseConnectionManager.OpenConnection())
            {
                // authUse Function call and return data
                string AuthUser = "SELECT * FROM authUser(@Username,@Password);";
                using (NpgsqlCommand cmd = new NpgsqlCommand(AuthUser, conn))

                {
                 
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Authentication successful
                            userId = Convert.ToInt32(reader["_user_id"]);
                            emailId = reader.GetString(reader.GetOrdinal("_email_id"));
                            roleId = Convert.ToInt32(reader["_role_id"]);
                            isActive = Convert.ToBoolean(reader["_is_active"]);
                        }
                        else
                        {
                            return Json(new { Success = false, message = "Username and password do not match. Please try again." });
                        }

                    }
                   
                }

                if(isActive==true) 
                    {

                    if (roleId == 3)
                    {
                        string approve = "select is_user from public.user where id=@userId";
                        using (NpgsqlCommand cmd2 = new NpgsqlCommand(approve, conn))
                        {
                            cmd2.Parameters.AddWithValue("@userId", userId);
                            using (NpgsqlDataReader reader2 = cmd2.ExecuteReader())
                            {
                                if (reader2.Read())
                                {
                                    bool con_aproved = reader2.GetBoolean(0);
                                    if (con_aproved != true)
                                    {
                                        // Return JSON data containing the authentication status and additional data
                                        return Json(new { Success = false, message = "You are not approved by Admin Yet..!" });
                                    }
                                    else
                                    {
                                        var responseData1 = new
                                        {
                                            UserId = userId,
                                            EmailId = emailId,
                                            RoleId = roleId,
                                        };
<<<<<<< HEAD

=======
                                        SessionHelper.Set("user_id", userId);
>>>>>>> origin/branch-yashvi
                                        // Set success message in TempData
                                        TempData["SuccessMessage"] = "Login successful! Welcome to the dashboard.";
                                        return Json(new { Success = true, Data = responseData1 });
                                    }
                                }
                                else
                                {
                                    return Json(new { Success = false });
                                }
                            }
                        }
                    }
                    var responseData = new
                    {
                        UserId = userId,
                        EmailId = emailId,
                        RoleId = roleId,
                    };
<<<<<<< HEAD

                    // Set success message in TempData
=======
                    SessionHelper.Set("user_id", userId);
                        // Set success message in TempData
>>>>>>> origin/branch-yashvi
                    TempData["SuccessMessage"] = "Login successful! Welcome to the dashboard.";

                    // Return JSON data containing the authentication status and additional data
                    return Json(new { Success = true, Data = responseData });

                }
                else
                {
                    return Json(new { Success = false, message = "You Account is Disable ..!" });
                }

                
            }
        }
<<<<<<< HEAD
=======
        [HttpPost]
        public ActionResult Logout()
        {
            Console.WriteLine("Session Clearing...");
            Session.Clear();
            Console.WriteLine("Session Clear...");
                return Json(new { Success = true });
        }
>>>>>>> origin/branch-yashvi
    }
    
}