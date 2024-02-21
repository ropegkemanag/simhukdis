using Newtonsoft.Json;
using SIMHUKDIS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using Syncfusion.XlsIO;
using System.Data.SqlClient;

namespace SIMHUKDIS.Models
{
    public class clsPraDPK
    {
        public int No { get; set; }
        public string ID { get; set; }
        public string FullName { get; set; }
        public string NIP { get; set; }
        public string GOL_RUANG { get; set; }
        public string LEVEL_JABATAN { get; set; }
        public string DasarBukti { get; set; }
        public string PelanggaranDisiplin { get; set; }
        public string PasalPelanggaran { get; set; }
        public string RekomendasiHukdis { get; set; }
        [DisplayName("No Surat")]
        public string NoSurat { get; set; }
        [DisplayName("Asal Surat")]
        public string AsalSurat { get; set; }
        [DisplayName("Tanggal Surat")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime TanggalSurat { get; set; }
        [DisplayName("Perihal")]
        public string perihal { get; set; }
        [DisplayName("Satuan Kerja")]
        public string SATUAN_KERJA { get; set; }
        public string UnitKerja { get; set; }
        public string Catatan { get; set; }
        public string UserLogin { get; set; }
        public string Tanggal_Sidang { get; set; }
        public string JenisPelanggaran { get; set; }
        public string Kode_JenisPelanggaran { get; set; }
    }
    public class clsJenisPelanggaran
    {
        public string Kode_Jenis_Pelanggaran { get; set; }
        public string JenisPelanggaran { get; set; }
    }
    public class clsPejabat
    {
        public string NamaLengkap { get; set; }
        public string NIP { get; set; }
    }
    public class clsPraDPKDB
    {
        public DataTable GetDataTable()
        {
            int a = 0;
            DataTable table = new DataTable();
            string constr = ConfigurationManager.ConnectionStrings["dbHukdis"].ConnectionString;
            List<clsPraDPK> DP = new List<clsPraDPK>();
            using (SqlConnection con = new SqlConnection(constr))
            {
                string q = "SIMHUKDIS.sp_PraDPK_Sel";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                table.Columns.Add("No", typeof(int));
                table.Columns.Add("Nama", typeof(string));
                table.Columns.Add("Pasal Pelanggaran", typeof(string));
                table.Columns.Add("Uraian Pelanggaran", typeof(string));
                table.Columns.Add("Rekomendasi Hukuman", typeof(string));
                while (rd.Read())
                {
                    
                    a = a + 1;
                    table.Rows.Add(a, rd["Fullname"].ToString(), rd["Pasal_Pelanggaran"].ToString(), rd["Pelanggaran"].ToString(), rd["Rekomendasi_Hukuman"].ToString());
                    
                }
                return table;
            }
        }
        public List<clsPraDPK> ListAll()
        {
            int a = 0;
            string constr = ConfigurationManager.ConnectionStrings["dbHukdis"].ConnectionString;
            List<clsPraDPK> DP = new List<clsPraDPK>();
            using (SqlConnection con = new SqlConnection(constr))
            {
                string q = "SIMHUKDIS.sp_PraDPK_Sel";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    clsPraDPK data = new clsPraDPK();
                    a = a + 1;
                    data.No = a;
                    data.ID = rd["id"].ToString();
                    data.NoSurat = rd["nomor_surat"].ToString();
                    data.AsalSurat = rd["asal_surat"].ToString();
                    data.perihal = rd["perihal"].ToString();
                    data.NIP = rd["NIP"].ToString();
                    data.FullName = rd["Fullname"].ToString();
                    data.DasarBukti = rd["Dasar_dan_Bukti_Penunjang"].ToString();
                    data.PelanggaranDisiplin = rd["Pelanggaran"].ToString();
                    data.SATUAN_KERJA = rd["SatuanKerja"].ToString();
                    data.PasalPelanggaran = rd["Pasal_Pelanggaran"].ToString();
                    data.RekomendasiHukdis = rd["Rekomendasi_Hukuman"].ToString();
                    data.Catatan = rd["Catatan"].ToString();
                    data.Tanggal_Sidang = rd["Tanggal_Sidang"].ToString();
                    data.JenisPelanggaran = rd["Jenis_Pelanggaran"].ToString();
                    data.Kode_JenisPelanggaran = rd["Kode_JenisPelanggaran"].ToString();

                    clsPegawaiDB x = new clsPegawaiDB();
                    List<clsDataPegawaiDtl> y = new List<clsDataPegawaiDtl>();
                    y = x.ListPegawai(data.NIP);
                    int z = 1;
                    foreach (var item in y)
                    {
                        if (z==1)
                        {
                            data.GOL_RUANG = item.GOL_RUANG;
                            data.LEVEL_JABATAN = item.TAMPIL_JABATAN;
                            data.UnitKerja = item.KETERANGAN_SATUAN_KERJA;
                        }
                        z = z + 1;
                    }
                    DP.Add(data);
                }
                return DP;
            }
        }
        public List<clsPraDPK> ListFilter(string TanggalSidang)
        {
            int a = 0;
            string constr = ConfigurationManager.ConnectionStrings["dbHukdis"].ConnectionString;
            List<clsPraDPK> DP = new List<clsPraDPK>();
            using (SqlConnection con = new SqlConnection(constr))
            {
                string q = "SIMHUKDIS.sp_PraDPK_Sel";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("iTanggalSidang", TanggalSidang);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    clsPraDPK data = new clsPraDPK();
                    a = a + 1;
                    data.No = a;
                    data.ID = rd["id"].ToString();
                    data.NoSurat = rd["nomor_surat"].ToString();
                    data.AsalSurat = rd["asal_surat"].ToString();
                    data.perihal = rd["perihal"].ToString();
                    data.NIP = rd["NIP"].ToString();
                    data.FullName = rd["Fullname"].ToString();
                    data.DasarBukti = rd["Dasar_dan_Bukti_Penunjang"].ToString();
                    data.PelanggaranDisiplin = rd["Pelanggaran"].ToString();
                    data.SATUAN_KERJA = rd["SatuanKerja"].ToString();
                    data.PasalPelanggaran = rd["Pasal_Pelanggaran"].ToString();
                    data.RekomendasiHukdis = rd["Rekomendasi_Hukuman"].ToString();
                    data.Catatan = rd["Catatan"].ToString();
                    data.Tanggal_Sidang = rd["Tanggal_Sidang"].ToString();
                    data.JenisPelanggaran = rd["Jenis_Pelanggaran"].ToString();
                    data.Kode_JenisPelanggaran = rd["Kode_JenisPelanggaran"].ToString();

                    clsPegawaiDB x = new clsPegawaiDB();
                    List<clsDataPegawaiDtl> y = new List<clsDataPegawaiDtl>();
                    y = x.ListPegawai(data.NIP);
                    int z = 1;
                    foreach (var item in y)
                    {
                        if (z == 1)
                        {
                            data.GOL_RUANG = item.GOL_RUANG;
                            data.LEVEL_JABATAN = item.TAMPIL_JABATAN;
                            data.UnitKerja = item.KETERANGAN_SATUAN_KERJA;
                        }
                        z = z + 1;
                    }
                    DP.Add(data);
                }
                return DP;
            }
        }
        public clsPraDPK ListByID(string id, string nip)
        {
            int a = 0;
            string constr = ConfigurationManager.ConnectionStrings["dbHukdis"].ConnectionString;
            clsPraDPK DP = new clsPraDPK();
            using (SqlConnection con = new SqlConnection(constr))
            {
                string q = "SIMHUKDIS.sp_PraDPK_Sel_ByID";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("iid", id);
                cmd.Parameters.AddWithValue("inip", nip);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    a = a + 1;
                    DP.No = a;
                    DP.ID = rd["id"].ToString();
                    DP.NoSurat = rd["nomor_surat"].ToString();
                    DP.AsalSurat = rd["asal_surat"].ToString();
                    DP.perihal = rd["perihal"].ToString();
                    DP.NIP = rd["NIP"].ToString();
                    DP.FullName = rd["Fullname"].ToString();
                    DP.DasarBukti = rd["Dasar_dan_Bukti_Penunjang"].ToString();
                    DP.PelanggaranDisiplin = rd["Pelanggaran"].ToString();
                    DP.SATUAN_KERJA = rd["SatuanKerja"].ToString();
                    DP.PasalPelanggaran = rd["Pasal_Pelanggaran"].ToString();
                    DP.RekomendasiHukdis = rd["Rekomendasi_Hukuman"].ToString();
                    DP.Catatan = rd["Catatan"].ToString();
                    DP.Tanggal_Sidang = rd["Tanggal_Sidang"].ToString();
                    DP.JenisPelanggaran = rd["Jenis_Pelanggaran"].ToString();
                    DP.Kode_JenisPelanggaran = rd["Kode_JenisPelanggaran"].ToString();
                }
                return DP;
            }
        }
        public string GetPejabatPenandatangan()
        {
            string constr = ConfigurationManager.ConnectionStrings["dbHukdis"].ConnectionString;
            HasilSidangDtl data = new HasilSidangDtl();
            using (SqlConnection con = new SqlConnection(constr))
            {
                string a = "";
                string q = "SIMHUKDIS.sp_PejabatMst_Sel";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    a = rd["Karopeg"].ToString();                    
                }
                return a;
            }
        }
        public clsPejabat GetKaropegSel(string ID)
        {
            string constr = ConfigurationManager.ConnectionStrings["dbHukdis"].ConnectionString;
            clsPejabat x = new clsPejabat();
            HasilSidangDtl data = new HasilSidangDtl();
            using (SqlConnection con = new SqlConnection(constr))
            {
                string a = "";
                string q = "SIMHUKDIS.sp_GetKaropegSel";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("iid", ID);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    x.NamaLengkap = rd["fullname"].ToString();
                    x.NIP = rd["nip"].ToString();
                }
                return x;
            }
        }
        
        public int Insert(clsPraDPK PD)
        {
            try
            {
                int i = 0;
                string constr = ConfigurationManager.ConnectionStrings["dbHukdis"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string MySql = "sp_PraDPK_Ins";
                    string TanggalSidang = Convert.ToDateTime(PD.Tanggal_Sidang).ToString("yyyy-MM-dd");
                    SqlCommand cmd = new SqlCommand(MySql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("iid", PD.ID);
                    cmd.Parameters.AddWithValue("iNIP", PD.NIP);
                    cmd.Parameters.AddWithValue("iCatatan", PD.Catatan);
                    cmd.Parameters.AddWithValue("iCreate_User", PD.UserLogin);
                    cmd.Parameters.AddWithValue("iTanggal_Sidang", TanggalSidang);
                    cmd.Parameters.AddWithValue("iJenisPelanggaran", PD.JenisPelanggaran);
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
        public int Update(clsPraDPK PD)
        {
            try
            {
                int i = 0;
                string constr = ConfigurationManager.ConnectionStrings["dbHukdis"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string MySql = "SIMHUKDIS.sp_PraDPK_Upd";

                    DateTime dParse = DateTime.ParseExact(PD.Tanggal_Sidang, "dd-MM-yyyy", null); //Convert.ToDateTime(Data.EstimateDate); 
                    PD.Tanggal_Sidang = dParse.ToString("yyyy-MM-dd");

                    SqlCommand cmd = new SqlCommand(MySql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("iid", PD.ID);
                    cmd.Parameters.AddWithValue("iNIP", PD.NIP);
                    cmd.Parameters.AddWithValue("iCatatan", PD.Catatan);
                    cmd.Parameters.AddWithValue("iCreate_User", PD.UserLogin);
                    cmd.Parameters.AddWithValue("iTanggal_Sidang", PD.Tanggal_Sidang);
                    cmd.Parameters.AddWithValue("iJenisPelanggaran", PD.JenisPelanggaran);
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
        public int UpdateStatus1(clsPraDPK PD)
        {
            try
            {
                int i = 0;
                string constr = ConfigurationManager.ConnectionStrings["dbHukdis"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string MySql = "SIMHUKDIS.sp_PraDPK_Upd_Status";
                    SqlCommand cmd = new SqlCommand(MySql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("iid", PD.ID);
                    cmd.Parameters.AddWithValue("iNIP", PD.NIP);
                    cmd.Parameters.AddWithValue("iCreate_User", PD.UserLogin);
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
        public int UpdateStatus2(clsPraDPK PD)
        {
            try
            {
                int i = 0;
                string constr = ConfigurationManager.ConnectionStrings["dbHukdis"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string MySql = "SIMHUKDIS.sp_PraDPK_Upd_Status2";
                    SqlCommand cmd = new SqlCommand(MySql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("iid", PD.ID);
                    cmd.Parameters.AddWithValue("iCreate_User", PD.UserLogin);
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
        public bool GetDataExist(string ID,string NIP)
        {
            string constr = ConfigurationManager.ConnectionStrings["dbHukdis"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SIMHUKDIS.sp_PraDPK_Already_Sel", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("iid", ID);
                cmd.Parameters.AddWithValue("iNIP", NIP);
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
        public List<clsJenisPelanggaran> GetListJenisPelanggaran()
        {
            List<clsJenisPelanggaran> JP = new List<clsJenisPelanggaran>();
            string constr = ConfigurationManager.ConnectionStrings["dbHukdis"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("SIMHUKDIS.sp_JenisPelanggaran_Sel", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    clsJenisPelanggaran s = new clsJenisPelanggaran();
                    s.Kode_Jenis_Pelanggaran = rd["id"].ToString();
                    s.JenisPelanggaran = rd["Jenis_Pelanggaran"].ToString();
                    JP.Add(s);
                }
                rd.Close();
            }
            return JP;
        }
    }
}