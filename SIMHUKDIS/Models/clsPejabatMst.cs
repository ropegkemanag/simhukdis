using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
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
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                Int32 pNom = 0;
                string q = "sp_PejabatMst_Sel";
                MySqlCommand cmd = new MySqlCommand(q, con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                MySqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    pNom = pNom + 1;
                    data.Menag = rd["Menag"].ToString();
                    data.Sekjend = rd["Sekjend"].ToString();
                    data.Koordinator = rd["Koordinator"].ToString();
                    data.Karopeg = rd["Karopeg"].ToString();
                    data.SubKoordinator = rd["SubKoordinator"].ToString();
                }
                return data;
            }
        }
        public int Insert(clsPejabatMst t)
        {
            int i;
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                MySqlCommand cmd = new MySqlCommand("sp_PejabatMst_Ins", con);
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
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                MySqlCommand cmd = new MySqlCommand("sp_PejabatMst_Upd", con);
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
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("sp_PejabatMst_Sel", con);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
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
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                MySqlCommand cmd = new MySqlCommand("sp_Karopeg_Sel", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                MySqlDataReader rd = cmd.ExecuteReader();
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
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                MySqlCommand cmd = new MySqlCommand("sp_Koordinator_Sel", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                MySqlDataReader rd = cmd.ExecuteReader();
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
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                MySqlCommand cmd = new MySqlCommand("sp_SubKoordinator_Sel", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                MySqlDataReader rd = cmd.ExecuteReader();
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