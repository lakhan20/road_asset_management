using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Security;
using System.Xml.Linq;

namespace GIS_ROAD_ASSET_MANAGEMENT.Models
{
    public class MyDbContext
    {
        string cs = "Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=admin123;";
        NpgsqlConnection conn = null;

        public List<Usermodel> getUser()
        {
            conn = new NpgsqlConnection(cs);
            try
            {
                conn.Open();
                List<Usermodel> user1 = new List<Usermodel>();
                string select = "select * from display_users();";
                NpgsqlCommand cmd = new NpgsqlCommand(select, conn);
                NpgsqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    Usermodel usermodel = new Usermodel();
                    usermodel.id = Convert.ToInt32(da.GetValue(0).ToString());
                    usermodel.name = da.GetValue(1).ToString();
                    usermodel.email_id = da.GetValue(2).ToString();
                    usermodel.contact_no = da.GetValue(3).ToString();
                    usermodel.password = da.GetValue(4).ToString();
                    RoleModel rolemodel = new RoleModel();
                    rolemodel.role_name = da.GetValue(5).ToString();
                    usermodel.RoleName = rolemodel.role_name;

                    usermodel.created_at = Convert.ToDateTime(da.GetValue(6).ToString());
                    usermodel.updated_at = Convert.ToDateTime(da.GetValue(7).ToString());
                    usermodel.is_active = bool.Parse(da.GetValue(8).ToString());
                    WardModel wardmodel = new WardModel();
                    wardmodel.ward_name = da.GetValue(9).ToString();
                    usermodel.WardName = wardmodel.ward_name;
                    user1.Add(usermodel);
                }
                return user1;
            }
            catch (NpgsqlException ex)
            {
                // Handle the exception, log it, etc.
                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public List<Usermodel> getUserById(int id)
        {
            conn = new NpgsqlConnection(cs);
            try 
            {
                conn.Open();
                List<Usermodel> user1 = new List<Usermodel>();
                string select = "select * from public.user where id=@id;";
                NpgsqlCommand cmd = new NpgsqlCommand(select, conn);
                cmd.Parameters.AddWithValue("@id", id);
                NpgsqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    Usermodel usermodel = new Usermodel();
                    usermodel.id = Convert.ToInt32(da.GetValue(0).ToString());
                    usermodel.name = da.GetValue(1).ToString();
                    usermodel.email_id = da.GetValue(2).ToString();
                    usermodel.contact_no = da.GetValue(3).ToString();
                    usermodel.password = da.GetValue(4).ToString();
                    RoleModel rolemodel = new RoleModel();
                    rolemodel.role_name = da.GetValue(5).ToString();
                    usermodel.RoleName = rolemodel.role_name;

                    usermodel.created_at = Convert.ToDateTime(da.GetValue(6).ToString());
                    usermodel.updated_at = Convert.ToDateTime(da.GetValue(7).ToString());
                    usermodel.is_active = bool.Parse(da.GetValue(8).ToString());
                    WardModel wardmodel = new WardModel();
                    wardmodel.ward_name = da.GetValue(9).ToString();
                    usermodel.WardName = wardmodel.ward_name;
                    user1.Add(usermodel);
                }
                return user1;
            }
            catch (NpgsqlException ex)
            {
                // Handle the exception, log it, etc.
                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }


        public List<Usermodel> getContractor()
        {
            conn = new NpgsqlConnection(cs);
            try
            {
                conn.Open();
                List<Usermodel> user1 = new List<Usermodel>();
                string select = "select * from display_contractors();";
                NpgsqlCommand cmd = new NpgsqlCommand(select, conn);
                NpgsqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    Usermodel usermodel = new Usermodel();
                    ContractorModel contractormodel = new ContractorModel();

                    usermodel.id = Convert.ToInt32(da.GetValue(0).ToString());
                    usermodel.name = da.GetValue(1).ToString();
                    usermodel.email_id = da.GetValue(2).ToString();
                    usermodel.contact_no = da.GetValue(3).ToString();
                    usermodel.password = da.GetValue(4).ToString();
                    RoleModel rolemodel = new RoleModel();
                    rolemodel.role_name = da.GetValue(5).ToString();
                    usermodel.RoleName = rolemodel.role_name;

                    usermodel.created_at = Convert.ToDateTime(da.GetValue(6).ToString());
                    usermodel.updated_at = Convert.ToDateTime(da.GetValue(7).ToString());
                    contractormodel.is_approved = bool.Parse(da.GetValue(8).ToString());
                    usermodel.is_approved = contractormodel.is_approved;
                    WardModel wardmodel = new WardModel();
                    wardmodel.ward_name = da.GetValue(9).ToString();
                    usermodel.WardName = wardmodel.ward_name;

                    contractormodel.company_name = da.GetValue(10).ToString();
                    contractormodel.proof_of_identity = da.GetValue(11).ToString();
                    usermodel.companyName = contractormodel.company_name;
                    usermodel.proofOfIdentity = contractormodel.proof_of_identity;
                    user1.Add(usermodel);
                }
                return user1;
            }
            catch (NpgsqlException ex)
            {
                // Handle the exception, log it, etc.
                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }


        public void approvedContractor(int id)
        {
            conn = new NpgsqlConnection(cs);
            try
            {
                conn.Open();
                string approveString = "SELECT approve_contractor_and_insert_user(@id);";
                NpgsqlCommand cmd = new NpgsqlCommand(approveString, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
            catch (NpgsqlException ex)
            {
                // Handle the exception, log it, etc.
                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

        }

        public List<WardModel> getWardName()
        {
            conn = new NpgsqlConnection(cs);
            try
            {
                conn.Open();
                List<WardModel> user1 = new List<WardModel>();
                string select = "select * from display_ward();";
                NpgsqlCommand cmd = new NpgsqlCommand(select, conn);
                NpgsqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    WardModel ward = new WardModel();
                    ward.ward_id = Convert.ToInt32(da.GetValue(0).ToString());
                    ward.ward_name = da.GetValue(1).ToString();
                    user1.Add(ward);
                }
                return user1;
            }
            catch (NpgsqlException ex)
            {
                // Handle the exception, log it, etc.
                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }



        }
        public List<RoleModel> getRole()
        {
            conn = new NpgsqlConnection(cs);
            try
            {
                conn.Open();
                List<RoleModel> user1 = new List<RoleModel>();
                string select = "select * from display_role();";
                NpgsqlCommand cmd = new NpgsqlCommand(select, conn);
                NpgsqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    RoleModel role = new RoleModel();
                    role.role_id = Convert.ToInt32(da.GetValue(0).ToString());
                    role.role_name = da.GetValue(1).ToString();
                    user1.Add(role);
                }
                return user1;
            }
            catch (NpgsqlException ex)
            {
                // Handle the exception, log it, etc.
                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

        }
        public bool updateUserDetails(Usermodel user)
        {
            conn = new NpgsqlConnection(cs);
            try 
            {
                conn.Open();
                string update = "select * from  update_user(@p_id ,@p_name,@p_email,@p_contact,@p_ward_id,@p_role_id);";
                NpgsqlCommand cmd = new NpgsqlCommand(update, conn);

                // Pass the form field values as parameters to the stored procedure
                cmd.Parameters.AddWithValue("@p_id", user.id);
                cmd.Parameters.AddWithValue("@p_name", user.name);
                cmd.Parameters.AddWithValue("@p_email", user.email_id);
                cmd.Parameters.AddWithValue("@p_contact", user.contact_no);
                cmd.Parameters.AddWithValue("@p_ward_id", user.ward_id);
                cmd.Parameters.AddWithValue("@p_role_id", user.role_id);

                int count = cmd.ExecuteNonQuery();
                if (count > 0) { return true; }
                else { return false; }
            }
            catch (NpgsqlException ex)
            {
                // Handle the exception, log it, etc.
                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

        }
        public bool disableUser(int id)
        {
            conn = new NpgsqlConnection(cs);
            try 
            {
                conn.Open();
                string update = $"select deactivate_user({id});";
                NpgsqlCommand cmd = new NpgsqlCommand(update, conn);

                // Pass the form field values as parameters to the stored procedure


                int count = cmd.ExecuteNonQuery();
                if (count > 0) { return true; }
                else { return false; }
            }
            catch (NpgsqlException ex)
            {
                // Handle the exception, log it, etc.
                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public void sendEmailToWardOfficer(string email_id,string password)
        {

            SMTPConfiguration sMTPConfiguration = new SMTPConfiguration
            {
                server = "smtp.gmail.com",
                hostName = "bhavymunjani1418@gmail.com",
                password = "bqqwhpqlxlvhnwuk",
                port = 587
            };

            MailMessage message = new MailMessage();
            /*
                        Random r=new Random();
                        int num = r.Next();*/

            message.From = new MailAddress(sMTPConfiguration.hostName);
            message.Subject = "Welcome to Amnex Technology 🎉";

            message.To.Add(new MailAddress(email_id.Trim().ToLower().ToString()));
            message.Body = $@"
                        <!DOCTYPE html>
                        <html lang='en'>
                        <head>
                            <meta charset='UTF-8'>
                            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                        </head>
                        <body style='font-family: Arial, sans-serif; background-color: transparent; margin: 0; padding: 0;  color:white;'>
                            <div class='container' style='width: 80%; margin: 20px auto; max-width: 600px;background-color: #0a3d62; border-radius: 10px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); padding: 20px;'>
                                <img src='https://storage.googleapis.com/nextgen_technology/Assets/logos/logo4-light.png' alt='Company Logo' style='max-width: 100px;height: auto; display: block; margin: 0 auto 20px;'>
                                <div class='content' style='text-align: center;'>
                                    <p>Login With this Email Or PassWord </p> </br>
                                        <span class='name' style='font-weight: bold;'><b>Email_Id:{email_id}</b></span> </br>
                                        <span class='name' style='font-weight: bold;'><b>PassWord:{password}</b></span> </br>
                                    <p>Do Not Shares With Others</p>
                                </div>
                            </div>
                        </body>
                        </html>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient(sMTPConfiguration.server)
            {
                Port = sMTPConfiguration.port,
                Credentials = new NetworkCredential(sMTPConfiguration.hostName, sMTPConfiguration.password),
                EnableSsl = true,
            };

            smtpClient.Send(message);
        }

        public void sendEmailToContractor(string email_id)
        {

            SMTPConfiguration sMTPConfiguration = new SMTPConfiguration
            {
                server = "smtp.gmail.com",
                hostName = "bhavymunjani1418@gmail.com",
                password = "bqqwhpqlxlvhnwuk",
                port = 587
            };

            MailMessage message = new MailMessage();
            /*
                        Random r=new Random();
                        int num = r.Next();*/

            message.From = new MailAddress(sMTPConfiguration.hostName);
            message.Subject = "Welcome to Amnex Technology 🎉";

            message.To.Add(new MailAddress(email_id.Trim().ToLower().ToString()));
            message.Body = $@"
                        <!DOCTYPE html>
                        <html lang='en'>
                        <head>
                            <meta charset='UTF-8'>
                            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                        </head>
                        <body style='font-family: Arial, sans-serif; background-color: transparent; margin: 0; padding: 0;  color:white;'>
                            <div class='container' style='width: 80%; margin: 20px auto; max-width: 600px;background-color: #0a3d62; border-radius: 10px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); padding: 20px;'>
                                <img src='https://storage.googleapis.com/nextgen_technology/Assets/logos/logo4-light.png' alt='Company Logo' style='max-width: 100px;height: auto; display: block; margin: 0 auto 20px;'>
                                <div class='content' style='text-align: center;'>
                                        <span class='name' style='font-weight: bold;'><b>Your Registerd Email Id is : {email_id}</b></span> </br>
                                        <span class='name' style='font-weight: bold;'><b>,You are Approved By Admin</b></span> </br>
                                    <p>Now You Can Go For The Login</p>
                                </div>
                            </div>
                        </body>
                        </html>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient(sMTPConfiguration.server)
            {
                Port = sMTPConfiguration.port,
                Credentials = new NetworkCredential(sMTPConfiguration.hostName, sMTPConfiguration.password),
                EnableSsl = true,
            };

            smtpClient.Send(message);
        }
    }
}