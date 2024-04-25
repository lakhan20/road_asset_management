using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        public List<Usermodel> getUserById(int id)
        {
            conn = new NpgsqlConnection(cs);
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


        public List<Usermodel> getContractor()
        {
            conn = new NpgsqlConnection(cs);
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


        public void approvedContractor(int id)
        {
            conn = new NpgsqlConnection(cs);
            conn.Open();
            string approveString = "SELECT approve_contractor_and_insert_user(@id);";
            NpgsqlCommand cmd = new NpgsqlCommand(approveString, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        public List<WardModel> getWardName()
        {
            conn = new NpgsqlConnection(cs);
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
        public List<RoleModel> getRole()
        {
            conn = new NpgsqlConnection(cs);
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
        public bool updateUserDetails(Usermodel user)
        {
            conn = new NpgsqlConnection(cs);
            conn.Open();
            string update = "select * from  update_user(@p_id ,@p_name,@p_email,@p_contact,@p_ward_id,@p_role_id);";
            NpgsqlCommand cmd = new NpgsqlCommand(update, conn);

            // Pass the form field values as parameters to the stored procedure
            cmd.Parameters.AddWithValue("@p_id", user.id);
            cmd.Parameters.AddWithValue("@p_name", user.name);
            cmd.Parameters.AddWithValue("@p_email",user.email_id);
            cmd.Parameters.AddWithValue("@p_contact",user.contact_no);
            cmd.Parameters.AddWithValue("@p_ward_id",user.ward_id);
            cmd.Parameters.AddWithValue("@p_role_id",user.role_id);

            int count = cmd.ExecuteNonQuery();
            if (count > 0) { return true; }
            else { return false; }
        }
        public bool disableUser(int id)
        {
            conn = new NpgsqlConnection(cs);
            conn.Open();
            string update = $"select deactivate_user({id});";
            NpgsqlCommand cmd = new NpgsqlCommand(update, conn);

            // Pass the form field values as parameters to the stored procedure


            int count = cmd.ExecuteNonQuery();
            if (count > 0) { return true; }
            else { return false; }
        }
    }
}