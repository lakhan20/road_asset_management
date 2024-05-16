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
using System.Security.Cryptography;
using System.Text;

namespace GIS_ROAD_ASSET_MANAGEMENT.Models
{
    public class MyDbContext
    {

        public List<Usermodel> getUser()
        {
            using (NpgsqlConnection conn = DatabaseConnectionManager.OpenConnection())
            {
                List<Usermodel> user1 = new List<Usermodel>();
                string select = "select * from display_users();";
                using (NpgsqlCommand cmd = new NpgsqlCommand(select, conn))
                {
                    using (NpgsqlDataReader da = cmd.ExecuteReader())
                    {
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


                }

            }

        }
        public List<Usermodel> getUserSuggestion()
        {
            using (NpgsqlConnection conn = DatabaseConnectionManager.OpenConnection())
            {
                List<Usermodel> user1 = new List<Usermodel>();
                string select = "select * from get_suggestion_details();";
                using (NpgsqlCommand cmd = new NpgsqlCommand(select, conn))
                {
                    using (NpgsqlDataReader da = cmd.ExecuteReader())
                    {
                        while (da.Read())
                        {
                            Usermodel usermodel = new Usermodel();
                            usermodel.id = Convert.ToInt32(da.GetValue(0).ToString());

                            SuggestionModel suggestionmodel = new SuggestionModel();
                            suggestionmodel.suggestion_id = Convert.ToInt32(da.GetValue(1).ToString());
                            usermodel.suggestion_id = suggestionmodel.suggestion_id;

                            usermodel.name = da.GetValue(2).ToString();
                            suggestionmodel.comment = da.GetValue(3).ToString();
                            usermodel.comment = suggestionmodel.comment;

                            ProjectModel projectModel = new ProjectModel();
                            projectModel.project_id = Convert.ToInt32(da.GetValue(4).ToString());
                            usermodel.project_id = projectModel.project_id;

                            projectModel.project_title = da.GetValue(5).ToString();
                            usermodel.project_title = projectModel.project_title;

                            usermodel.project_location = projectModel.project_location;

                            projectModel.status = da.GetValue(7).ToString();
                            usermodel.status = projectModel.status;

                            projectModel.project_alloted_name = da.GetValue(8).ToString();
                            usermodel.project_alloted_name = projectModel.project_alloted_name;

                            projectModel.project_alloted_contact = da.GetValue(9).ToString();
                            usermodel.project_alloted_contact = projectModel.project_alloted_contact;

                            suggestionmodel.created_at = Convert.ToDateTime(da.GetValue(10).ToString());
                            usermodel.created_at = suggestionmodel.created_at;

                            suggestionmodel.updated_at = Convert.ToDateTime(da.GetValue(11).ToString());
                            usermodel.updated_at = suggestionmodel.updated_at;
                            user1.Add(usermodel);
                        }
                        return user1;
                    }


                }

            }
        }

        // ADD SUGGETION ON PROJECT
        public bool addSuggestion(Usermodel user, int userId)
        {
            using (NpgsqlConnection conn = DatabaseConnectionManager.OpenConnection())
            {
                List<Usermodel> user1 = new List<Usermodel>();
                string select = "select insert_suggestion(@_user_id,@_comment,@_project_id);";
                using (NpgsqlCommand cmd = new NpgsqlCommand(select, conn))
                {
                    cmd.Parameters.AddWithValue("@_user_id", userId);
                    cmd.Parameters.AddWithValue("@_comment", user.comment);
                    cmd.Parameters.AddWithValue("@_project_id", user.project_id);
                    
                    int result = Convert.ToInt32(cmd.ExecuteScalar());
                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

            }
        }

        public List<Usermodel> getUserById(int id)
        {
            using (NpgsqlConnection conn = DatabaseConnectionManager.OpenConnection())
            {
                List<Usermodel> user1 = new List<Usermodel>();
                string select = "select * from public.user where id=@id;";
                using (NpgsqlCommand cmd = new NpgsqlCommand(select, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (NpgsqlDataReader da = cmd.ExecuteReader())
                    {
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

                }

            }

        }


        public List<Usermodel> getContractor()
        {
            using (NpgsqlConnection conn = DatabaseConnectionManager.OpenConnection())
            {
                List<Usermodel> contractor = new List<Usermodel>();
                string select = "select * from display_contractors();";
                using (NpgsqlCommand cmd = new NpgsqlCommand(select, conn))
                {
                    using (NpgsqlDataReader da = cmd.ExecuteReader())
                    {
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
                            contractor.Add(usermodel);
                        }
                        return contractor;

                    }
                }

            }



        }


        public void approvedContractor(int id)
        {
            using (NpgsqlConnection conn = DatabaseConnectionManager.OpenConnection())
            {
                string approveString = "SELECT approve_contractor_and_insert_user(@id);";
                using (NpgsqlCommand cmd = new NpgsqlCommand(approveString, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<WardModel> getWardName()
        {
            using (NpgsqlConnection conn = DatabaseConnectionManager.OpenConnection())
            {
                List<WardModel> user1 = new List<WardModel>();
                string select = "select * from display_ward();";
                using (NpgsqlCommand cmd = new NpgsqlCommand(select, conn))
                {
                    using (NpgsqlDataReader da = cmd.ExecuteReader())
                    {
                        while (da.Read())
                        {
                            WardModel ward = new WardModel();
                            ward.ward_id = Convert.ToInt32(da.GetValue(0).ToString());
                            ward.ward_name = da.GetValue(1).ToString();
                            user1.Add(ward);
                        }
                        return user1;
                    }

                }

            }

        }
        public List<RoleModel> getRole()
        {
            using (NpgsqlConnection conn = DatabaseConnectionManager.OpenConnection())
            {
                List<RoleModel> user1 = new List<RoleModel>();
                string select = "select * from display_role();";
                using (NpgsqlCommand cmd = new NpgsqlCommand(select, conn))
                {
                    using (NpgsqlDataReader da = cmd.ExecuteReader())
                    {
                        while (da.Read())
                        {
                            RoleModel role = new RoleModel();
                            role.role_id = Convert.ToInt32(da.GetValue(0).ToString());
                            role.role_name = da.GetValue(1).ToString();
                            user1.Add(role);
                        }
                        return user1;

                    }

                }

            }


        }
        public bool updateUserDetails(Usermodel user)
        {
            using (NpgsqlConnection conn = DatabaseConnectionManager.OpenConnection())
            {
                string update = "select * from  update_user(@p_id ,@p_name,@p_email,@p_contact,@p_ward_id,@p_role_id);";
                using (NpgsqlCommand cmd = new NpgsqlCommand(update, conn))
                { // Pass the form field values as parameters to the function
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
            }
        }

        public List<Usermodel> displayCitizenSuggestion()
        {
            List<Usermodel> userModels = new List<Usermodel>();

            using (NpgsqlConnection conn = DatabaseConnectionManager.OpenConnection())
            {
                string select = "SELECT * FROM displaySuggestionHistory(@userId)";

                using (NpgsqlCommand cmd = new NpgsqlCommand(select, conn))
                {
                    int userId = SessionHelper.Get<int>("user_id");
                    cmd.Parameters.AddWithValue("@userId", userId);

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Usermodel usermodel = new Usermodel();
                            usermodel.id = Convert.ToInt32(reader.GetValue(0).ToString());

                            SuggestionModel suggestionmodel = new SuggestionModel();
                            suggestionmodel.suggestion_id = Convert.ToInt32(reader.GetValue(1).ToString());
                            usermodel.suggestion_id = suggestionmodel.suggestion_id;

                            usermodel.name = reader.GetValue(2).ToString();
                            suggestionmodel.comment = reader.GetValue(3).ToString();
                            usermodel.comment = suggestionmodel.comment;

                            ProjectModel projectModel = new ProjectModel();
                            projectModel.project_id = Convert.ToInt32(reader.GetValue(4).ToString());
                            usermodel.project_id = projectModel.project_id;

                            projectModel.project_title = reader.GetValue(5).ToString();
                            usermodel.project_title = projectModel.project_title;

                            usermodel.project_location = projectModel.project_location;

                            projectModel.status = reader.GetValue(7).ToString();
                            usermodel.status = projectModel.status;

                            projectModel.project_alloted_name = reader.GetValue(8).ToString();
                            usermodel.project_alloted_name = projectModel.project_alloted_name;

                            projectModel.project_alloted_contact = reader.GetValue(9).ToString();
                            usermodel.project_alloted_contact = projectModel.project_alloted_contact;

                            suggestionmodel.created_at = Convert.ToDateTime(reader.GetValue(10).ToString());
                            usermodel.created_at = suggestionmodel.created_at;

                            suggestionmodel.updated_at = Convert.ToDateTime(reader.GetValue(11).ToString());
                            usermodel.updated_at = suggestionmodel.updated_at;
                            userModels.Add(usermodel);
                        }
                        return userModels;
                    }
                }
            }
        }
        public bool  editCitizenSuggestion(SuggestionModel suggestion)
        {
            try 
            {
                using (NpgsqlConnection conn = DatabaseConnectionManager.OpenConnection())
                {
                    string select = "SELECT * FROM public.update_suggestion(@p_suggestionId,@p_comment);";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(select, conn))
                    {
                        cmd.Parameters.AddWithValue("@p_suggestionId", suggestion.suggestion_id);
                        cmd.Parameters.AddWithValue("@p_comment", suggestion.comment);
                        int count = cmd.ExecuteNonQuery();
                        if (count > 0) { return true; }
                        else { return false; }
                    }
                }
            }
            catch(Exception ex) 
            {
                return false;
            }
            
        }

        public bool disableUser(int id)
        {
            using (NpgsqlConnection conn = DatabaseConnectionManager.OpenConnection())
            {
                string update = $"select deactivate_user({id});";
                using (NpgsqlCommand cmd = new NpgsqlCommand(update, conn))
                {
                    // Pass the form field values as parameters to the function
                    int count = cmd.ExecuteNonQuery();
                    if (count > 0) { return true; }
                    else { return false; }
                }
            }
        }

        public void removeSuggestion(SuggestionModel suggestion)
        {
            using (NpgsqlConnection conn = DatabaseConnectionManager.OpenConnection())
            {
                string update = "select public.remove_suggestion(@p_suggestion_id);";
                using (NpgsqlCommand cmd = new NpgsqlCommand(update, conn))
                {
                    cmd.Parameters.AddWithValue("@p_suggestion_id", suggestion.suggestion_id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void sendEmailToWardOfficer(string email_id, string password)
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
        public List<ProjectModel> getProjectById(int id)
        {
            string cs = "Server=10.195.202.141;Port=5432;Database=postgres;User Id=postgres;Password=Admin123;";
            NpgsqlConnection conn = new NpgsqlConnection(cs);
            conn.Open();
            List<ProjectModel> project = new List<ProjectModel>();
            string projectQuery = "SELECT * FROM public.project WHERE project_id=@id";
            NpgsqlCommand cmd = new NpgsqlCommand(projectQuery, conn);
            cmd.Parameters.AddWithValue("@id", id);
            NpgsqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                ProjectModel projectModel = new ProjectModel();
                projectModel.project_title = da["project_title"].ToString();
                projectModel.work_order_no = da["work_order_no"].ToString();
                projectModel.description = da["description"].ToString();
                projectModel.status = da["status"].ToString();
                projectModel.start_date = Convert.ToDateTime(da["start_date"]);
                projectModel.end_date = Convert.ToDateTime(da["end_date"]);

                projectModel.s_latitude = da["s_latitude"].ToString();
                projectModel.s_longitude = da["s_longitude"].ToString();
                projectModel.created_at = Convert.ToDateTime((DateTime)da["created_at"]);

                projectModel.allocated_contractor_id = Convert.ToInt32(da["allocated_contractor_id"]);
                projectModel.budget_cost = Convert.ToInt64(da["budget_cost"]);

                projectModel.ward_id = Convert.ToInt32(da["ward_id"]);
                projectModel.approval_letter_s = da["approval_letter"].ToString();
                projectModel.tender_doc_s = da["tender_doc"].ToString();

                projectModel.e_latitude = da["e_latitude"].ToString();
                projectModel.e_longitude = da["e_longitude"].ToString();
                projectModel.project_type = da["project_type"].ToString();
                project.Add(projectModel);

            }
            return project;


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
        //display project count
        public Dictionary<int, int> GetProjectCountsByType()
        {
            Dictionary<int, int> projectCounts = new Dictionary<int, int>();

            using (NpgsqlConnection conn = DatabaseConnectionManager.OpenConnection())
            {
                string query = "SELECT project_type, COUNT(project_id) AS project_count FROM project GROUP BY project_type";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int projectType = reader.GetInt32(0); // Assuming project_type is stored as a string
                            int count = reader.GetInt32(1); // Assuming project_count is stored as an integer
                            projectCounts.Add(projectType, count);
                        }
                    }
                }
            }

            return projectCounts;
        }

        //for fetching pending data
        public Dictionary<string, int> PendingProjects()
        {
            Dictionary<string, int> PendingData = new Dictionary<string, int>();

            using (NpgsqlConnection conn = DatabaseConnectionManager.OpenConnection())
            {
                string query = "SELECT TO_CHAR(start_date, 'Mon') AS month_name,COUNT(*) AS pending_projects_count FROM public.project WHERE status = 1 AND start_date IS NOT NULL GROUP BY TO_CHAR(start_date, 'Mon') ORDER BY EXTRACT(MONTH FROM MIN(start_date));";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string month = reader.GetString(0);
                            int projectdata = reader.GetInt32(1);
                            PendingData.Add(month, projectdata);
                        }
                    }
                }
            }

            return PendingData;
        }
        public Dictionary<string, int> OngoingProjects()
        {
            Dictionary<string, int> OngoingData = new Dictionary<string, int>();

            using (NpgsqlConnection conn = DatabaseConnectionManager.OpenConnection())
            {
                string query = "SELECT TO_CHAR(start_date, 'Mon') AS month_name,COUNT(*) AS ongoing_projects_count FROM public.project WHERE status = 2 AND start_date IS NOT NULL GROUP BY TO_CHAR(start_date, 'Mon') ORDER BY EXTRACT(MONTH FROM MIN(start_date));";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string projectType = reader.GetString(0); // Assuming project_type is stored as a string
                            int count = reader.GetInt32(1); // Assuming project_count is stored as an integer
                            OngoingData.Add(projectType, count);
                        }
                    }
                }
            }

            return OngoingData;
        }
        public Dictionary<string, int> CompletedProjects()
        {
            Dictionary<string, int> CompleteData = new Dictionary<string, int>();

            using (NpgsqlConnection conn = DatabaseConnectionManager.OpenConnection())
            {
                string query = "SELECT TO_CHAR(end_date, 'Mon') AS month_name,COUNT(*) AS completed_projects_count FROM public.project WHERE status = 3 AND end_date IS NOT NULL GROUP BY TO_CHAR(end_date, 'Mon') ORDER BY EXTRACT(MONTH FROM MIN(end_date));";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string projectType = reader.GetString(0); // Assuming project_type is stored as a string
                            int count = reader.GetInt32(1); // Assuming project_count is stored as an integer
                            CompleteData.Add(projectType, count);
                        }
                    }
                }
            }

            return CompleteData;
        }
        public List<Usermodel> displayProfileData()
        {
            List<Usermodel> userModels = new List<Usermodel>();

            using (NpgsqlConnection conn = DatabaseConnectionManager.OpenConnection())
            {
                string select = "SELECT * FROM display_Profile_Data(@_userid)";

                using (NpgsqlCommand cmd = new NpgsqlCommand(select, conn))
                {
                    int userId = SessionHelper.Get<int>("user_id");
                    cmd.Parameters.AddWithValue("@_userid", userId);

                    using (NpgsqlDataReader da = cmd.ExecuteReader())
                    {
                        while (da.Read())
                        {
                            Usermodel usermodel = new Usermodel();
                            usermodel.id = Convert.ToInt32(da.GetValue(0).ToString());
                            usermodel.name = da.GetValue(1).ToString();
                            usermodel.email_id = da.GetValue(2).ToString();
                            usermodel.contact_no = da.GetValue(3).ToString();
                            WardModel wardmodel = new WardModel();
                            wardmodel.ward_name = da.GetValue(4).ToString();
                            usermodel.WardName = wardmodel.ward_name;
                            usermodel.password = da.GetValue(5).ToString();
                      
                            userModels.Add(usermodel);
                        }
                        return userModels;
                    }
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

        public bool updateProfileDetails(Usermodel user)
        {
            using (NpgsqlConnection conn = DatabaseConnectionManager.OpenConnection())
            {
                string update = "SELECT public.update_Profile_Data( @_userid,@_name ,@_email_id,@_contact_no,@_ward_id,@_password);";
                string hashedPassword = HashPassword(user.password);

                using (NpgsqlCommand cmd = new NpgsqlCommand(update, conn))
                {
                    cmd.Parameters.AddWithValue("@_userid", user.id);
                    cmd.Parameters.AddWithValue("@_name", user.name);
                    cmd.Parameters.AddWithValue("@_email_id", user.email_id);
                    cmd.Parameters.AddWithValue("@_contact_no", user.contact_no);
                    cmd.Parameters.AddWithValue("@_ward_id", user.ward_id);
                    cmd.Parameters.AddWithValue("@_password", hashedPassword);

                    int count = cmd.ExecuteNonQuery();

                    if (count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public bool updateProfileDetailsWithOutPassword(Usermodel user)
        {
            using (NpgsqlConnection conn = DatabaseConnectionManager.OpenConnection())
            {
                string update = "SELECT  public.update_profile_data_without_password( @_userid,@_name ,@_email_id,@_contact_no,@_ward_id);";

                using (NpgsqlCommand cmd = new NpgsqlCommand(update, conn))
                {
                    cmd.Parameters.AddWithValue("@_userid", user.id);
                    cmd.Parameters.AddWithValue("@_name", user.name);
                    cmd.Parameters.AddWithValue("@_email_id", user.email_id);
                    cmd.Parameters.AddWithValue("@_contact_no", user.contact_no);
                    cmd.Parameters.AddWithValue("@_ward_id", user.ward_id);
                    int count = cmd.ExecuteNonQuery();

                    if (count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
    }
}

