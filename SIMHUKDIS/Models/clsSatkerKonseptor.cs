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
    public class clsSatkerKonseptor
    {
        public string KODE_SATUAN_KERJA { get; set; }
        public string SATUAN_KERJA { get; set; }
        public string Fullname { get; set; }
        public string UserID { get; set; }
        public string LastUser { get; set; }
        public string LastUpdate { get; set; }
    }
    public class clsSatkerKonseptorDB
    {
        public List<clsSatkerKonseptor> SK
        {
            get
            {

                string constr = ConfigurationManager.ConnectionStrings["dbHukdis"].ConnectionString;
                List<clsSatkerKonseptor> SK = new List<clsSatkerKonseptor>();
                using (SqlConnection con = new SqlConnection(constr))
                {
                    Int32 pNom = 0;
                    string q = "SIMHUKDIS.sp_Satker_Konseptor_Sel";
                    SqlCommand cmd = new SqlCommand(q, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        clsSatkerKonseptor data = new clsSatkerKonseptor();
                        pNom = pNom + 1;
                        data.KODE_SATUAN_KERJA = rd["KODE_SATUAN_KERJA"].ToString();
                        data.SATUAN_KERJA = rd["SATUAN_KERJA"].ToString();
                        data.Fullname = rd["FullName"].ToString();
                        data.UserID = rd["Konseptor"].ToString();
                        SK.Add(data);
                    }
                    return SK;
                }
            }
        }
        public List<clsSatkerKonseptor> GetListSatker()
        {
            clsSatkerKonseptorDB db = new clsSatkerKonseptorDB();
            List<clsSatkerKonseptor> SK = new List<clsSatkerKonseptor>();
            string constr = ConfigurationManager.ConnectionStrings["dbHukdis"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("SIMHUKDIS.sp_Satker_Sel", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    clsSatkerKonseptor s = new clsSatkerKonseptor();
                    s.SATUAN_KERJA = rd["SATUAN_KERJA"].ToString();
                    s.KODE_SATUAN_KERJA = rd["KODE_SATUAN_KERJA"].ToString();
                    SK.Add(s);
                }
                rd.Close();
            }
            return SK;
        }
        public List<clsSatkerKonseptor> GetKonseptorList()
        {
            clsSatkerKonseptorDB db = new clsSatkerKonseptorDB();
            List<clsSatkerKonseptor> SK = new List<clsSatkerKonseptor>();
            string constr = ConfigurationManager.ConnectionStrings["dbHukdis"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("SIMHUKDIS.sp_Konseptor_Sel", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    clsSatkerKonseptor s = new clsSatkerKonseptor();
                    s.Fullname = rd["Fullname"].ToString();
                    s.UserID = rd["UserID"].ToString();
                    //s.GroupDesc = rd["GroupDesc"].ToString();
                    SK.Add(s);
                }
                rd.Close();
            }
            return SK;
        }
        public int Insert(clsSatkerKonseptor disposisi)
        {
            try
            {
                int i = 0;
                string constr = ConfigurationManager.ConnectionStrings["dbHukdis"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string MySql = "SIMHUKDIS.sp_Satker_Konseptor_Ins";
                    SqlCommand cmd = new SqlCommand(MySql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("iUserID", disposisi.UserID);
                    cmd.Parameters.AddWithValue("iKODE_SATUAN_KERJA", disposisi.KODE_SATUAN_KERJA);
                    cmd.Parameters.AddWithValue("iLastUser", disposisi.LastUser);
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
        public int Delete(clsSatkerKonseptor disposisi)
        {
            try
            {
                int i = 0;
                string constr = ConfigurationManager.ConnectionStrings["dbHukdis"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string MySql = "SIMHUKDIS.sp_Satker_Konseptor_Delete";
                    SqlCommand cmd = new SqlCommand(MySql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("iSATUAN_KERJA", disposisi.KODE_SATUAN_KERJA);
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
        public int Ubah(clsSatkerKonseptor disposisi)
        {
            try
            {
                int i = 0;
                string constr = ConfigurationManager.ConnectionStrings["dbHukdis"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string MySql = "SIMHUKDIS.sp_Satker_Konseptor_Upd";
                    SqlCommand cmd = new SqlCommand(MySql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("iKonseptor", disposisi.UserID);
                    cmd.Parameters.AddWithValue("iSATUAN_KERJA", disposisi.KODE_SATUAN_KERJA);
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
        public bool GetDataExist(string KODE_SATUAN_KERJA, string UserID)
        {
            string constr = ConfigurationManager.ConnectionStrings["dbHukdis"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SIMHUKDIS.sp_Satker_Konseptor_AlreadyExist", con);
                cmd.CommandType = CommandType.StoredProcedure;       
                cmd.Parameters.AddWithValue("iKode_Satuan_Kerja", KODE_SATUAN_KERJA);
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
    }
}