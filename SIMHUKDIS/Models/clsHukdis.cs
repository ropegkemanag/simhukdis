using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SIMHUKDIS.Models
{
    public class clsHukdis
    {
        public int ID { get; set; }
        [DisplayName("Hukdis ID")]
        [Required]
        public string HukdisID { get; set; }
        [DisplayName("Hukdis Description")]
        public string HukdisDesc { get; set; }
        public string Tingkat { get; set; }
        [DisplayName("Last User")]
        public string LastUser { get; set; }
        [DisplayName("Last Update")]
        public string LastUpdate { get; set; }
    }
    public class clsHukdisDB
    {
        public List<clsHukdis> HukdisList
        {
            get
            {

                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                List<clsHukdis> Hukdis = new List<clsHukdis>();
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string q = "SIMHUKDIS.sp_Hukdis_Sel";
                    SqlCommand cmd = new SqlCommand(q, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        clsHukdis data = new clsHukdis();
                        data.ID = Convert.ToInt32(rd["id"].ToString());
                        //data.HukdisID = rd["HukdisID"].ToString();
                        data.Tingkat = rd["Tingkat"].ToString();
                        data.HukdisDesc = rd["hukuman"].ToString();
                        data.LastUser = rd["LastUser"].ToString();
                        data.LastUpdate = rd["LastUpdate"].ToString();
                        //if (!Convert.IsDBNull(rd["LastUpdate"]))
                        //{
                        //    data.LastUpdate = Convert.ToDateTime(rd["LastUpdate"].ToString());
                        //}
                        Hukdis.Add(data);
                    }
                    return Hukdis;
                }
            }
        }
        public List<clsHukdis> GetListHukdis()
        {
            List<clsHukdis> Hukdis = new List<clsHukdis>();
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("SIMHUKDIS.sp_Hukdis_Sel", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    clsHukdis s = new clsHukdis();
                    //s.HukdisID = rd["HukdisID"].ToString();
                    s.ID = Convert.ToInt32(rd["ID"].ToString());
                    s.HukdisDesc = rd["hukuman"].ToString();
                    Hukdis.Add(s);
                }
                rd.Close();
            }
            return Hukdis;
        }
        public bool GetDataExist(string GroupID)
        {
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SIMHUKDIS.sp_Hukdis_AlreadyExist", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("ihukuman", GroupID);
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
        public int Insert(clsHukdis Hukdis)
        {
            try
            {
                int i = 0;
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string MySql = "SIMHUKDIS.sp_Hukdis_Ins";
                    SqlCommand cmd = new SqlCommand(MySql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("iHukdisID", Hukdis.HukdisID);
                    cmd.Parameters.AddWithValue("iHukuman", Hukdis.HukdisDesc);
                    cmd.Parameters.AddWithValue("iTingkat", Hukdis.Tingkat);
                    cmd.Parameters.AddWithValue("iLastUser", Hukdis.LastUser);                    
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
        public int Edit(clsHukdis Hukdis)
        {
            try
            {
                int i = 0;
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string MySql = "SIMHUKDIS.sp_Hukdis_Upd";
                    SqlCommand cmd = new SqlCommand(MySql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("iid", Hukdis.ID);
                    cmd.Parameters.AddWithValue("iHukuman", Hukdis.HukdisDesc);
                    cmd.Parameters.AddWithValue("iTingkat", Hukdis.Tingkat);
                    cmd.Parameters.AddWithValue("iLastUser", Hukdis.LastUser);
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
        public int Delete(string ID)
        {
            try
            {
                int i = 0;
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string MySql = "SIMHUKDIS.sp_Hukdis_Del";
                    SqlCommand cmd = new SqlCommand(MySql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("iid", ID);
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