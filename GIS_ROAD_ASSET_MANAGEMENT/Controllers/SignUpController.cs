using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GIS_ROAD_ASSET_MANAGEMENT.Models;
using System.Web.Mvc;
using Npgsql;
using System.Configuration;
using WebGrease.Css.Ast.Selectors;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics.Contracts;
using System.Web.Helpers;
using System.Xml.Linq;
using System.Security.Policy;
using System.Web.Services.Description;
using System.Security.Cryptography;
using System.Text;

namespace GIS_ROAD_ASSET_MANAGEMENT.Controllers
{
    public class SignUpController : Controller
    {
        // GET: SignUp
        [HttpPost]
        public ActionResult Csignup(string Cname, string Cemail, string Ccontact, int Cward, string Cpassword, int Crole_id)
        {
            string hashedPassword = HashPassword(Cpassword);


            using (NpgsqlConnection conn = DatabaseConnectionManager.OpenConnection())
            {
                if (IsEmailExists(Cemail))
                {
                    return Json(new { Success = false, Message = "Email already exists. Please use a different email." });
                }

                string SignupCitizen = " Select SignupUser(@Name,@Email,@Contact,@Password,@Role,@Ward)";
                using (NpgsqlCommand cmd = new NpgsqlCommand(SignupCitizen, conn))
                {

                    cmd.Parameters.AddWithValue("@Name", Cname);
                    cmd.Parameters.AddWithValue("@Email", Cemail);
                    cmd.Parameters.AddWithValue("@Contact", Ccontact);
                    cmd.Parameters.AddWithValue("@Ward", Cward);
                    cmd.Parameters.AddWithValue("@Password", hashedPassword);
                    cmd.Parameters.AddWithValue("@Role", Crole_id);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        string successMsg = "citizen Register successful! .";
                        return Json(new { Success = true, Message = successMsg });
                    }
                }



            }


        }
        // Method to check if email exists
        [HttpPost]
        public ActionResult Con_signup(string Con_name, string Con_email, string Con_contact, string Con_password, int Con_role_id, int Con_ward, string Con_company, string Con_file)
        {
            string hashedPassword = HashPassword(Con_password);

            using (NpgsqlConnection conn = DatabaseConnectionManager.OpenConnection())
            {

                if (IsEmailExists(Con_email))
                {
                    return Json(new { Success = false, Message = "Email already exists. Please use a different email." });
                }
                string SignupCitizen = " Select contractorsignup(@Name,@Email,@Contact,@Password,@Role,@Ward,@Company,@File)";
                using (NpgsqlCommand cmd = new NpgsqlCommand(SignupCitizen, conn))
                {

                    cmd.Parameters.AddWithValue("@Name", Con_name);
                    cmd.Parameters.AddWithValue("@Email", Con_email);
                    cmd.Parameters.AddWithValue("@Contact", Con_contact);
                    cmd.Parameters.AddWithValue("@Company", Con_company);
                    cmd.Parameters.AddWithValue("@File", Con_file);
                    cmd.Parameters.AddWithValue("@Ward", Con_ward);
                    cmd.Parameters.AddWithValue("@Password", hashedPassword);
                    cmd.Parameters.AddWithValue("@Role", Con_role_id);

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        string successMsg = "Contractor Register successful! .";
                        return Json(new { Success = true, Message = successMsg });
                    }

                }


            }
        }

        [HttpPost]
        public ActionResult Wsignup(string Wname, string Wemail, string Wcontact, int Wward, string Wpassword, int Wrole_id)
        {
            string hashedPassword = HashPassword(Wpassword);

            using (NpgsqlConnection conn = DatabaseConnectionManager.OpenConnection())
            {

                if (IsEmailExists(Wemail))
                {
                    return Json(new { Success = false, Message = "Email already exists. Please use a different email."});
                }
                string SignupWardOfficer = " Select SignupUser(@Name,@Email,@Contact,@Password,@Role,@Ward)";
                using (NpgsqlCommand cmd = new NpgsqlCommand(SignupWardOfficer, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", Wname);
                    cmd.Parameters.AddWithValue("@Email", Wemail);
                    cmd.Parameters.AddWithValue("@Contact", Wcontact);
                    cmd.Parameters.AddWithValue("@Ward", Wward);
                    cmd.Parameters.AddWithValue("@Password", hashedPassword);
                    cmd.Parameters.AddWithValue("@Role", Wrole_id);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        string successMsg = "Ward officer  Register successful! .";
                        TempData["WardSignUp"] = "WardOfficer Added Successfully";
                        return Json(new { Success = true, Message = successMsg });
                    }
                }
            }
        }

        [HttpPost]
        public ActionResult Asignup(string Aname, string Aemail, string Acontact, int Award, string Apassword, int Arole_id)
        {
            string hashedPassword = HashPassword(Apassword);

            using (NpgsqlConnection conn = DatabaseConnectionManager.OpenConnection())
            {

                if (IsEmailExists(Aemail))
                {
                    return Json(new { Success = false, Message = "Email already exists. Please use a different email." });
                }
                string SignUpAdmin = " Select SignupUser(@Name,@Email,@Contact,@Password,@Role,@Ward)";
                using (NpgsqlCommand cmd = new NpgsqlCommand(SignUpAdmin, conn))
                {

                    cmd.Parameters.AddWithValue("@Name", Aname);
                    cmd.Parameters.AddWithValue("@Email", Aemail);
                    cmd.Parameters.AddWithValue("@Contact", Acontact);
                    cmd.Parameters.AddWithValue("@Ward", Award);
                    cmd.Parameters.AddWithValue("@Password", hashedPassword);
                    cmd.Parameters.AddWithValue("@Role", Arole_id);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        string successMsg = "Admin ADD  Register successful! .";
                        TempData["AdminSignUp"] = "Admin Added Successfully";
                        return Json(new { Success = true, Message = successMsg });
                    }
                }

            }


        }

        //method to check exist email id
        private bool IsEmailExists(string email)
        {
            using (NpgsqlConnection conn = DatabaseConnectionManager.OpenConnection())
            {
                string checkEmailQuery = "SELECT check_email_exists(@Email)";
                using (NpgsqlCommand cmd = new NpgsqlCommand(checkEmailQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
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

    }
}
