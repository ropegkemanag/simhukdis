using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SIMHUKDIS.Models
{
    public class ClsSatker
    {
        public string KODE_SATUAN_KERJA { get; set; }
        public string SATUAN_KERJA { get; set; }
        public string UserLogin { get; set; }
        public string LastUpdate { get; set; }
    }
    public class ClsSatkerDB
    {
        public List<ClsSatker> SatkerList
        {
            get
            {

                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                List<ClsSatker> Satker = new List<ClsSatker>();
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string q = "SIMHUKDIS.sp_Satker_Sel";
                    SqlCommand cmd = new SqlCommand(q, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        ClsSatker data = new ClsSatker();
                        data.KODE_SATUAN_KERJA = rd["KODE_SATUAN_KERJA"].ToString();
                        data.SATUAN_KERJA = rd["SATUAN_KERJA"].ToString();
                        data.UserLogin = rd["LastUser"].ToString();
                        data.LastUpdate = rd["LastUpdate"].ToString();
                        Satker.Add(data);
                    }
                    return Satker;
                }
            }
        }
        public int Insert(ClsSatker User)
        {
            try
            {
                int i = 0;

                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string MySql = "SIMHUKDIS.sp_Satker_Ins";
                    SqlCommand cmd = new SqlCommand(MySql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("iSATUAN_KERJA", User.SATUAN_KERJA);
                    cmd.Parameters.AddWithValue("iUserID", User.UserLogin);
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
        public bool GetDataExist(string SATUAN_KERJA)
        {
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SIMHUKDIS.sp_Satker_AlreadyExists", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("iSATUAN_KERJA", SATUAN_KERJA);
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
        public int Edit(ClsSatker Hukdis)
        {
            try
            {
                int i = 0;
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string MySql = "SIMHUKDIS.sp_Satker_Upd";
                    SqlCommand cmd = new SqlCommand(MySql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("iKODE_SATUAN_KERJA", Hukdis.KODE_SATUAN_KERJA);
                    cmd.Parameters.AddWithValue("iSATUAN_KERJA", Hukdis.SATUAN_KERJA);
                    cmd.Parameters.AddWithValue("iUserID", Hukdis.UserLogin);
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
        public int Delete(string KODE_SATUAN_KERJA)
        {
            try
            {
                int i = 0;
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string MySql = "SIMHUKDIS.sp_Satker_Del";
                    SqlCommand cmd = new SqlCommand(MySql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("iKODE_SATUAN_KERJA", KODE_SATUAN_KERJA);
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
        public string GetName(string id)
        {
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                string Satker = "";
                int i = 0;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("SIMHUKDIS.sp_Satker_GetName", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("IID", id);
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        if (i == 0)
                        {
                            Satker = rd["SATUAN_KERJA"].ToString();
                        }
                        i = i + 1;
                    }
                    rd.Close();
                }
                return Satker;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}