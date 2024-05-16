using GIS_ROAD_ASSET_MANAGEMENT.Models;
using Microsoft.Ajax.Utilities;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;


namespace GIS_ROAD_ASSET_MANAGEMENT.Controllers
{
    public class LoginController : Controller
    {
        // Generate CAPTCHA image
        public ActionResult GenerateCaptchaImage()
        {
         
                string captchaCode = GenerateRandomCaptchaCode(6); // Generate a random CAPTCHA code
                Session["CaptchaCode"] = captchaCode;
             
            // Return the CAPTCHA image as a file
            // Return the CAPTCHA code as plain text
            return Content(captchaCode);
        }

        // Helper method to generate a random CAPTCHA code
        private string GenerateRandomCaptchaCode(int length)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
        // GET: Login


        [HttpPost]
        public ActionResult Login(string username, string password, string captchaCode)
        {        // Validate CAPTCHA code
            if (!IsCaptchaValid(captchaCode))
            {
               
                return Json(new { Success = false, Message = "Invalid CAPTCHA code. Please try again." });
            }


            int userId = 0;
            string name = "";
            string emailId = "";
            int roleId = 0;
            bool isActive = false;
            string hashedPassword = HashPassword(password);

            // Your existing login logic
            using (NpgsqlConnection conn = DatabaseConnectionManager.OpenConnection())
            {
                // authUse Function call and return data
                string AuthUser = "SELECT * FROM authUser(@Username,@Password);";
                using (NpgsqlCommand cmd = new NpgsqlCommand(AuthUser, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", hashedPassword);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Authentication successful
                            userId = Convert.ToInt32(reader["_user_id"]);
                            name = Convert.ToString(reader["_name"]);
                            emailId = Convert.ToString(reader["_email_id"]);
                            roleId = Convert.ToInt32(reader["_role_id"]);
                            isActive = Convert.ToBoolean(reader["_is_active"]);
                        }
                        else
                        {
                            return Json(new { Success = false, message = "Username and password do not match. Please try again." });
                        }
                    }
                }

                // Additional validation and role-specific checks
                if (isActive == true)
                {
                    if (roleId == 3)
                    {
                        // Check if user is approved by admin
                        string approve = "SELECT is_user FROM public.user WHERE id = @userId";
                        using (NpgsqlCommand cmd2 = new NpgsqlCommand(approve, conn))
                        {
                            cmd2.Parameters.AddWithValue("@userId", userId);
                            using (NpgsqlDataReader reader2 = cmd2.ExecuteReader())
                            {
                                if (reader2.Read())
                                {
                                    bool conApproved = reader2.GetBoolean(0);
                                    if (!conApproved)
                                    {
                                        // User is not approved by admin
                                        return Json(new { Success = false, message = "You are not approved by Admin yet." });
                                    }
                                }
                            }
                        }
                    }

                    // Create response data
                    var responseData = new
                    {
                        UserId = userId,
                        EmailId = emailId,
                        RoleId = roleId,
                    };

                    // Store session data
                    SessionHelper.Set("user_id", userId);
                    SessionHelper.Set("role_id", roleId);
                    SessionHelper.Set("name", name);

                    // Set success message in TempData
                    TempData["SuccessMessage"] = "Login successful! Welcome to the dashboard.";

                    // Return JSON data containing the authentication status and additional data
                    return Json(new { Success = true, Data = responseData });
                }
                else
                {
                    // User account is disabled
                    return Json(new { Success = false, message = "Your account is disabled." });
                }
            }
        }

        [HttpPost]
        public ActionResult UpdateCaptchaSession(string captchaCode)
        {
            Session["CaptchaCode"] = captchaCode; // Update the session with the new CAPTCHA code
            return Json(new { Success = true });
        }

        // CAPTCHA validation method
        private bool IsCaptchaValid(string captchaCode)
        {
            string expectedCaptchaCode = Session["CaptchaCode"] as string; // Retrieve expected CAPTCHA code from session
            return string.Equals(captchaCode, expectedCaptchaCode, StringComparison.OrdinalIgnoreCase); // Compare CAPTCHA codes
        }


        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert byte array to a string
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    stringBuilder.Append(data[i].ToString("x2"));
                }
                return stringBuilder.ToString();
            }
        }

        [HttpPost]
        public ActionResult Logout()
        {
            Console.WriteLine("Session Clearing...");
            HttpContext.Session.Clear();
            if (HttpContext.Session["user_id"] == null)
            {
                Console.WriteLine("Session Clear...");
                return Json(new { Success = true });
            }
            else
            {
                return Json(new { Success = false });
            }

        }
    }


}