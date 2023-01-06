using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SIMHUKDIS.Models
{
    public class clsHome
    {
        public string SUDAH_PROSES { get; set; }
        public string BELUM_PROSES { get; set; }
    }
    public class DashboardChart
    {
        public string LabelName { get; set; }
        public string LabelValue { get; set; }
    }
    public class DataChart
    {
        public string[] labels { get; set; }
        public List<DataSetChart> datasets { get; set; }
    }

    public class DataSetChart
    {
        public string type { get; set; }
        public string label { get; set; }
        public bool fill { get; set; }
        public string[] backgroundColor { get; set; }
        public string[] borderColor { get; set; }
        public string borderWidth { get; set; }
        public decimal[] data { get; set; }
        public decimal lineTension { get; set; }
        public int[] borderDash { get; set; }
        public string pointBorderColor { get; set; }
        public string pointBackgroundColor { get; set; }
        public int pointRadius { get; set; }
        public int pointHoverRadius { get; set; }
        public int pointHitRadius { get; set; }
        public int pointBorderWidth { get; set; }
        public string pointStyle { get; set; }

    }
    public class clsHomeDB
    {
        public List<clsHome> GetDashboard()
        {
            List<clsHome> JP = new List<clsHome>();
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("SIMHUKDIS.sp_Grafik_Dashboard", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    clsHome s = new clsHome();
                    s.BELUM_PROSES = rd["JML"].ToString();
                    s.SUDAH_PROSES = rd["STATUS"].ToString();
                    JP.Add(s);
                }
                rd.Close();
            }
            return JP;
        }
        public List<DashboardChart> Disp
        {
            get
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                List<DashboardChart> Disp = new List<DashboardChart>();
                using (SqlConnection con = new SqlConnection(constr))
                {
                    Int32 pNom = 0;
                    string q = "SIMHUKDIS.sp_DashboardChart_Sel";
                    SqlCommand cmd = new SqlCommand(q, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        DashboardChart data = new DashboardChart();
                        pNom = pNom + 1;
                        data.LabelName = rd["SATUAN_KERJA"].ToString();
                        data.LabelValue = rd["hitung"].ToString();

                        Disp.Add(data);
                    }
                    return Disp;
                }
            }
            
        }
        public clsHome GetNilaiGrafik(string Satker, string GroupUser)
        {
            int a = 0;
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            clsHome DP = new clsHome();
            using (SqlConnection con = new SqlConnection(constr))
            {
                string q = "sp_Grafik_Dashboard";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("iSatker", Satker);
                cmd.Parameters.AddWithValue("iGroupUser", GroupUser);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    DP.BELUM_PROSES = rd["BELUM_PROSES"].ToString();
                    DP.SUDAH_PROSES = rd["SUDAH_PROSES"].ToString();
                }
                return DP;
            }
        }
        public int GetGrafik1 (string UserGroup, string SatuanKerja, string UserID)
        {
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            int a = 0;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string q = "sp_Dashboard_Grafik1_Sel";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iSatker", SatuanKerja);
                cmd.Parameters.AddWithValue("@iGroupUser", UserGroup);
                cmd.Parameters.AddWithValue("@UserLogin", UserID);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    a = Convert.ToInt16(rd["JUMLAH"]);
                }
                return a;
            }
        }
        public int GetGrafik2(string UserGroup, string SatuanKerja, string UserID)
        {
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            int a = 0;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string q = "sp_Dashboard_Grafik2_Sel";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("@iSatker", SatuanKerja);
                cmd.Parameters.AddWithValue("@iGroupUser", UserGroup);
                cmd.Parameters.AddWithValue("@UserLogin", UserID);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    a = Convert.ToInt16(rd["JUMLAH"]);
                }
                return a;
            }
        }
        public int GetGrafik3(string UserGroup, string SatuanKerja, string UserID)
        {
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            int a = 0;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string q = "sp_Dashboard_Grafik3_Sel";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("@iSatker", SatuanKerja);
                cmd.Parameters.AddWithValue("@iGroupUser", UserGroup);
                cmd.Parameters.AddWithValue("@UserLogin", UserID);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    a = Convert.ToInt16(rd["JUMLAH"]);
                }
                return a;
            }
        }
    }
}