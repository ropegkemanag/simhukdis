using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SIMHUKDIS.Models
{
    public class clsPejabatMst
    {
        public string Menag { get; set; }
        public string Sekjend { get; set; }
        public string Karopeg { get; set; }
        public string Koordinator { get; set; }
        public string SubKoordinator { get; set; }
        public string Konseptor { get; set; }

        public string NIP_Karopeg { get; set; }
        public string NIP_Koordinator { get; set; }
        public string NIP_SubKoordinator { get; set; }
        public string NIP_Konseptor { get; set; }

        public string Nama_Karopeg { get; set; }
        public string Nama_Koordinator { get; set; }
        public string Nama_SubKoordinator { get; set; }
        public string Nama_Konseptor { get; set; }

        public string Jabatan_Karopeg { get; set; }
        public string Jabatan_Koordinator { get; set; }
        public string Jabatan_SubKoordinator { get; set; }
        public string Jabatan_Konseptor { get; set; }

        [DisplayName("Create Date")]
        public string CreateDate { get; set; }
        [DisplayName("Create User")]
        public string CreateUser { get; set; }
        [DisplayName("Update Date")]
        public string UpdateDate { get; set; }
        [DisplayName("Update User")]
        public string UpdatUser { get; set; }
    }
    public class clsPejabatName
    {
        public string nama { get; set; }
        public string userid { get; set; }
    }
    public class clsPejabatMstDB
    {
        public clsPejabatMst GetList()
        {
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            clsPejabatMst data = new clsPejabatMst();
            using (SqlConnection con = new SqlConnection(constr))
            {
                Int32 pNom = 0;
                string q = "SIMHUKDIS.sp_PejabatMst_Sel";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    pNom = pNom + 1;
                    data.Menag = rd["Menag"].ToString();
                    data.Sekjend = rd["Sekjend"].ToString();
                    data.Koordinator = rd["Koordinator"].ToString();
                    data.Karopeg = rd["Karopeg"].ToString();
                    data.SubKoordinator = rd["SubKoordinator"].ToString();

                    data.NIP_Koordinator = rd["NIP_Koordinator"].ToString();
                    data.NIP_Karopeg = rd["NIP_Karopeg"].ToString();
                    data.NIP_SubKoordinator = rd["NIP_SubKoordinator"].ToString();
                    
                    data.Nama_Koordinator = rd["Nama_Koordinator"].ToString();
                    data.Nama_Karopeg = rd["Nama_Karopeg"].ToString();
                    data.Nama_SubKoordinator = rd["Nama_SubKoordinator"].ToString();

                    data.Jabatan_Koordinator = rd["Jabatan_Koordinator"].ToString();
                    data.Jabatan_Karopeg = rd["Jabatan_Karopeg"].ToString();
                    data.Jabatan_SubKoordinator = rd["Jabatan_SubKoordinator"].ToString();
                }
                return data;
            }
        }
        public int Insert(clsPejabatMst t)
        {
            int i;
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("SIMHUKDIS.sp_PejabatMst_Ins", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("iMenag", t.Menag);
                cmd.Parameters.AddWithValue("iSekjend", t.Sekjend);
                cmd.Parameters.AddWithValue("iKaropeg", t.Karopeg);
                cmd.Parameters.AddWithValue("iKoordinator", t.Koordinator);
                cmd.Parameters.AddWithValue("iSubKoordinator", t.SubKoordinator);
                cmd.Parameters.AddWithValue("iCreated_User", t.CreateUser);
                con.Open();
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }
        public int Update(clsPejabatMst t)
        {
            int i;
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("SIMHUKDIS.sp_PejabatMst_Upd", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("iMenag", t.Menag);
                cmd.Parameters.AddWithValue("iSekjend", t.Sekjend);
                cmd.Parameters.AddWithValue("iKaropeg", t.Karopeg);
                cmd.Parameters.AddWithValue("iKoordinator", t.Koordinator);
                cmd.Parameters.AddWithValue("iSubKoordinator", t.SubKoordinator);
                cmd.Parameters.AddWithValue("iCreated_User", t.CreateUser);
                con.Open();
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }
        public bool GetDataExist()
        {
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SIMHUKDIS.sp_PejabatMst_Sel", con);
                cmd.CommandType = CommandType.StoredProcedure;
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
        public List<clsPejabatName> GetKaropegList()
        {
            List<clsPejabatName> SK = new List<clsPejabatName>();
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("SIMHUKDIS.sp_Karopeg_Sel", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    clsPejabatName s = new clsPejabatName();
                    s.nama = rd["Fullname"].ToString();
                    s.userid = rd["UserID"].ToString();
                    //s.GroupDesc = rd["GroupDesc"].ToString();
                    SK.Add(s);
                }
                rd.Close();
            }
            return SK;
        }
        public List<clsPejabatName> GetKoordinatorList()
        {
            List<clsPejabatName> SK = new List<clsPejabatName>();
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("SIMHUKDIS.sp_Koordinator_Sel", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    clsPejabatName s = new clsPejabatName();
                    s.nama = rd["Fullname"].ToString();
                    s.userid = rd["UserID"].ToString();
                    //s.GroupDesc = rd["GroupDesc"].ToString();
                    SK.Add(s);
                }
                rd.Close();
            }
            return SK;
        }
        public List<clsPejabatName> GeSubKoordinatorList()
        {
            List<clsPejabatName> SK = new List<clsPejabatName>();
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("SIMHUKDIS.sp_SubKoordinator_Sel", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    clsPejabatName s = new clsPejabatName();
                    s.nama = rd["Fullname"].ToString();
                    s.userid = rd["UserID"].ToString();
                    //s.GroupDesc = rd["GroupDesc"].ToString();
                    SK.Add(s);
                }
                rd.Close();
            }
            return SK;
        }
    }
}