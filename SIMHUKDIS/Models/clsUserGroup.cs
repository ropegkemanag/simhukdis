using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SIMHUKDIS.Models
{
    public class clsUserGroup
    {
        [DisplayName("Group ID")]
        [Required]
        public string GroupID { get; set; }
        [DisplayName("Group Description")]
        public string GroupDesc { get; set; }
        [DisplayName("Last User")]
        public string LastUser { get; set; }
        [DisplayName("Last Update")]
        public string LastUpdate { get; set; }
    }
    public class clsUserGroupDB
    {
        public List<clsUserGroup> UserGroupList
        {
            get
            {

                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                List<clsUserGroup> ug = new List<clsUserGroup>();
                using (SqlConnection con = new SqlConnection(constr))
                {
                    Int32 pNom = 0;
                    string q = "SIMHUKDIS.sp_UserGroup_Sel";
                    SqlCommand cmd = new SqlCommand(q, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        clsUserGroup data = new clsUserGroup();
                        pNom = pNom + 1;
                       
                        data.GroupID = rd["GroupID"].ToString();
                        data.GroupDesc = rd["GroupDesc"].ToString();
                        data.LastUser = rd["LastUser"].ToString();
                        data.LastUpdate = rd["LastUpdate"].ToString();
                        ug.Add(data);
                    }
                    return ug;
                }
            }
        }
        public List<clsUserGroup> GetListUserGroup()
        {
            List<clsUserGroup> User = new List<clsUserGroup>();
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("SIMHUKDIS.sp_UserGroup_Sel", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    clsUserGroup s = new clsUserGroup();
                    s.GroupID = rd["GroupID"].ToString();
                    //s.GroupDesc = rd["GroupDesc"].ToString();
                    User.Add(s);
                }
                rd.Close();
            }
            return User;
        }
        public bool GetDataExist(string GroupID)
        {
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SIMHUKDIS.sp_UserGroup_AlreadyExist", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("iGroupID", GroupID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public int Insert(clsUserGroup User)
        {
            try
            {
                int i = 0;

                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string MySql = "SIMHUKDIS.sp_UserGroup_Ins";
                    SqlCommand cmd = new SqlCommand(MySql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("iGroupID", User.GroupID);
                    cmd.Parameters.AddWithValue("iGroupDesc", User.GroupDesc);
                    cmd.Parameters.AddWithValue("iUserLogin", User.LastUser);
                    con.Open();
                    i = cmd.ExecuteNonQuery();
                }
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Edit(clsUserGroup User)
        {
            try
            {
                int i = 0;
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string MySql = "SIMHUKDIS.sp_UserGroup_Upd";
                    SqlCommand cmd = new SqlCommand(MySql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("iGroupID", User.GroupID);
                    cmd.Parameters.AddWithValue("iGroupDesc", User.GroupDesc);
                    cmd.Parameters.AddWithValue("iLastUser", User.LastUser);
                    con.Open();
                    i = cmd.ExecuteNonQuery();
                }
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Delete(string GroupID)
        {
            try
            {
                int i = 0;
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string MySql = "SIMHUKDIS.sp_UserGroup_Del";
                    SqlCommand cmd = new SqlCommand(MySql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("iGroupID", GroupID);
                    con.Open();

                    i = cmd.ExecuteNonQuery();
                }
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}