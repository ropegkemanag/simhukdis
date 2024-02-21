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
    public class clsFormatPenomoran
    {
        public string No_Telaah { get; set; }
        public string No_SK { get; set; }

        [DisplayName("Create Date")]
        public string CreateDate { get; set; }
        [DisplayName("Create User")]
        public string CreateUser { get; set; }
        [DisplayName("Update Date")]
        public string UpdateDate { get; set; }
        [DisplayName("Update User")]
        public string UpdatUser { get; set; }
    }
    public class clsFormatPenomoranDB
    {
        public clsFormatPenomoran GetList()
        {
            string constr = ConfigurationManager.ConnectionStrings["dbHukdis"].ConnectionString;
            clsFormatPenomoran data = new clsFormatPenomoran();
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
                    data.No_Telaah = rd["Menag"].ToString();
                    data.No_SK = rd["Sekjend"].ToString();
                }
                return data;
            }
        }
        //public int Insert(clsFormatPenomoran t)
        //{
        //    int i;
        //    string constr = ConfigurationManager.ConnectionStrings["dbHukdis"].ConnectionString;
        //    using (SqlConnection con = new SqlConnection(constr))
        //    {
        //        SqlCommand cmd = new SqlCommand("SIMHUKDIS.sp_PejabatMst_Ins", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("iMenag", t.Menag);
        //        cmd.Parameters.AddWithValue("iSekjend", t.Sekjend);
        //        cmd.Parameters.AddWithValue("iKaropeg", t.Karopeg);
        //        cmd.Parameters.AddWithValue("iKoordinator", t.Koordinator);
        //        cmd.Parameters.AddWithValue("iSubKoordinator", t.SubKoordinator);
        //        cmd.Parameters.AddWithValue("iCreated_User", t.CreateUser);
        //        con.Open();
        //        i = cmd.ExecuteNonQuery();
        //    }
        //    return i;
        //}
        //public int Update(clsFormatPenomoran t)
        //{
        //    int i;
        //    string constr = ConfigurationManager.ConnectionStrings["dbHukdis"].ConnectionString;
        //    using (SqlConnection con = new SqlConnection(constr))
        //    {
        //        SqlCommand cmd = new SqlCommand("SIMHUKDIS.sp_PejabatMst_Upd", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("iMenag", t.Menag);
        //        cmd.Parameters.AddWithValue("iSekjend", t.Sekjend);
        //        cmd.Parameters.AddWithValue("iKaropeg", t.Karopeg);
        //        cmd.Parameters.AddWithValue("iKoordinator", t.Koordinator);
        //        cmd.Parameters.AddWithValue("iSubKoordinator", t.SubKoordinator);
        //        cmd.Parameters.AddWithValue("iCreated_User", t.CreateUser);
        //        con.Open();
        //        i = cmd.ExecuteNonQuery();
        //    }
        //    return i;
        //}
        //public bool GetDataExist()
        //{
        //    string constr = ConfigurationManager.ConnectionStrings["dbHukdis"].ConnectionString;
        //    using (SqlConnection con = new SqlConnection(constr))
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("SIMHUKDIS.sp_PejabatMst_Sel", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        if (dt.Rows.Count == 0)
        //        {
        //            return false;
        //        }
        //        else
        //        {
        //            return true;
        //        }
        //    }
        //}
        //public List<clsPejabatName> GetKaropegList()
        //{
        //    List<clsPejabatName> SK = new List<clsPejabatName>();
        //    string constr = ConfigurationManager.ConnectionStrings["dbHukdis"].ConnectionString;
        //    using (SqlConnection con = new SqlConnection(constr))
        //    {
        //        SqlCommand cmd = new SqlCommand("SIMHUKDIS.sp_Karopeg_Sel", con);
        //        cmd.CommandType = CommandType.Text;
        //        con.Open();
        //        SqlDataReader rd = cmd.ExecuteReader();
        //        while (rd.Read())
        //        {
        //            clsPejabatName s = new clsPejabatName();
        //            s.nama = rd["Fullname"].ToString();
        //            s.userid = rd["UserID"].ToString();
        //            //s.GroupDesc = rd["GroupDesc"].ToString();
        //            SK.Add(s);
        //        }
        //        rd.Close();
        //    }
        //    return SK;
        //}
        //public List<clsPejabatName> GetKoordinatorList()
        //{
        //    List<clsPejabatName> SK = new List<clsPejabatName>();
        //    string constr = ConfigurationManager.ConnectionStrings["dbHukdis"].ConnectionString;
        //    using (SqlConnection con = new SqlConnection(constr))
        //    {
        //        SqlCommand cmd = new SqlCommand("SIMHUKDIS.sp_Koordinator_Sel", con);
        //        cmd.CommandType = CommandType.Text;
        //        con.Open();
        //        SqlDataReader rd = cmd.ExecuteReader();
        //        while (rd.Read())
        //        {
        //            clsPejabatName s = new clsPejabatName();
        //            s.nama = rd["Fullname"].ToString();
        //            s.userid = rd["UserID"].ToString();
        //            //s.GroupDesc = rd["GroupDesc"].ToString();
        //            SK.Add(s);
        //        }
        //        rd.Close();
        //    }
        //    return SK;
        //}
        //public List<clsPejabatName> GeSubKoordinatorList()
        //{
        //    List<clsPejabatName> SK = new List<clsPejabatName>();
        //    string constr = ConfigurationManager.ConnectionStrings["dbHukdis"].ConnectionString;
        //    using (SqlConnection con = new SqlConnection(constr))
        //    {
        //        SqlCommand cmd = new SqlCommand("SIMHUKDIS.sp_SubKoordinator_Sel", con);
        //        cmd.CommandType = CommandType.Text;
        //        con.Open();
        //        SqlDataReader rd = cmd.ExecuteReader();
        //        while (rd.Read())
        //        {
        //            clsPejabatName s = new clsPejabatName();
        //            s.nama = rd["Fullname"].ToString();
        //            s.userid = rd["UserID"].ToString();
        //            //s.GroupDesc = rd["GroupDesc"].ToString();
        //            SK.Add(s);
        //        }
        //        rd.Close();
        //    }
        //    return SK;
        //}
    }
}