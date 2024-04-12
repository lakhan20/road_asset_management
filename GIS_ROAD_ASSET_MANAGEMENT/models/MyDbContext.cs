using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GIS_ROAD_ASSET_MANAGEMENT.Models
{
    public class MyDbContext
    {
        string cs = "Server=localhost;Port=5432;Database=newDb;User Id=postgres;Password=admin123;";
        NpgsqlConnection conn = null;

        public List<Usermodel> getUser(Usermodel user)
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
                usermodel.created_at = Convert.ToDateTime(da.GetValue(6).ToString());
                usermodel.updated_at = Convert.ToDateTime(da.GetValue(7).ToString());
                usermodel.is_active = bool.Parse(da.GetValue(8).ToString());
                WardModel wardmodel = new WardModel();
                wardmodel.ward_name = da.GetValue(9).ToString();
                user1.Add(usermodel);
            }
            return user1;
            conn.Close();
        }

    }
}