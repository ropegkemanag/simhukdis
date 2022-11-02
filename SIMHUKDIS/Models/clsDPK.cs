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
    public class clsDPK
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
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime TanggalSurat { get; set; }
        [DisplayName("Perihal")]
        public string perihal { get; set; }
        [DisplayName("Satuan Kerja")]
        public string SATUAN_KERJA { get; set; }
        public string UnitKerja { get; set; }
        public string Catatan { get; set; }
        public string UserLogin { get; set; }
        public string Tanggal_Sidang_Pra_DPK { get; set; }
        public string Tanggal_Sidang_DPK { get; set; }
        public string KeputusanSidang { get; set; }
        public string Catatan_Sidang { get; set; }
        public string hukuman { get; set; }
        public string menag { get; set; }
        public string sekjend { get; set; }
        public string karopeg { get; set; }
        public string nip_karopeg { get; set; }
        public string koor { get; set; }
        public string nip_koor { get; set; }
        public string subkoor { get; set; }
        public string nip_subkoor { get; set; }
        public string konseptor { get; set; }
        public string nip_konseptor { get; set; }
        public string Mengingat { get; set; }
        public string Tembusan { get; set; }
    }
    public class clsDPKDB
    {
        public List<clsDPK> ListAll()
        {
            int a = 0;
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            List<clsDPK> DP = new List<clsDPK>();
            using (SqlConnection con = new SqlConnection(constr))
            {
                string q ="SIMHUKDIS.sp_DPK_Sel";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    clsDPK data = new clsDPK();
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
                    data.Tanggal_Sidang_DPK = rd["tanggal_sidang_dpk"].ToString();
                    data.Tanggal_Sidang_Pra_DPK = rd["tanggal_sidang_pra_dpk"].ToString();
                    data.KeputusanSidang = rd["KeputusanSidang"].ToString();
                    data.hukuman = rd["hukuman"].ToString();
                    data.Catatan_Sidang = rd["Catatan_Sidang"].ToString();

                    clsPraDPKDB x = new clsPraDPKDB();
                    List<clsDataPegawaiDtl> y = new List<clsDataPegawaiDtl>();
                    y = x.GetPegawai(data.NIP);
                    int z = 1;
                    foreach (var item in y)
                    {
                        if (z == 1)
                        {
                            data.GOL_RUANG = item.GOL_RUANG;
                            data.LEVEL_JABATAN = item.LEVEL_JABATAN;
                            data.UnitKerja = item.SATUAN_KERJA;
                        }
                        z = z + 1;
                    }
                    DP.Add(data);
                }
                return DP;
            }
        }
        public List<clsDPK> ListFilter(string TanggalSidang)
        {
            int a = 0;
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            List<clsDPK> DP = new List<clsDPK>();
            using (SqlConnection con = new SqlConnection(constr))
            {
                string q = "SIMHUKDIS.sp_DPK_Sel";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("iTanggalSidang", TanggalSidang);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    clsDPK data = new clsDPK();
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
                    data.Tanggal_Sidang_DPK = rd["tanggal_sidang_dpk"].ToString();
                    data.Tanggal_Sidang_Pra_DPK = rd["tanggal_sidang_pra_dpk"].ToString();
                    data.KeputusanSidang = rd["KeputusanSidang"].ToString();
                    data.hukuman = rd["hukuman"].ToString();
                    data.Catatan_Sidang = rd["Catatan_Sidang"].ToString();

                    clsPraDPKDB x = new clsPraDPKDB();
                    List<clsDataPegawaiDtl> y = new List<clsDataPegawaiDtl>();
                    y = x.GetPegawai(data.NIP);
                    int z = 1;
                    foreach (var item in y)
                    {
                        if (z == 1)
                        {
                            data.GOL_RUANG = item.GOL_RUANG;
                            data.LEVEL_JABATAN = item.LEVEL_JABATAN;
                            data.UnitKerja = item.SATUAN_KERJA;
                        }
                        z = z + 1;
                    }
                    DP.Add(data);
                }
                return DP;
            }
        }
        public bool GetDataExist(string ID, string NIP)
        {
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SIMHUKDIS.sp_DPK_Already_Sel", con);
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
        public int UpdateStatus1(clsDPK PD)
        {
            try
            {
                int i = 0;
                Encryption encrypt = new Encryption();
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string MySql = "SIMHUKDIS.sp_DPK_Upd_Status";
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
        public int UpdateStatus2(clsDPK PD)
        {
            try
            {
                int i = 0;
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string MySql = "SIMHUKDIS.sp_DPK_Upd_Status2";
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
        public int Insert(clsDPK PD)
        {
            try
            {
                int i = 0;
                Encryption encrypt = new Encryption();
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string MySql = "sp_DPK_Ins";
                    string TanggalSidang = Convert.ToDateTime(PD.Tanggal_Sidang_DPK).ToString("yyyy-MM-dd");
                    SqlCommand cmd = new SqlCommand(MySql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("iid", PD.ID);
                    cmd.Parameters.AddWithValue("iNIP", PD.NIP);
                    cmd.Parameters.AddWithValue("iCatatan_Sidang", PD.Catatan_Sidang);
                    cmd.Parameters.AddWithValue("iCreate_User", PD.UserLogin);
                    cmd.Parameters.AddWithValue("iTanggal_Sidang", TanggalSidang);
                    cmd.Parameters.AddWithValue("iKeputusanSidang", PD.KeputusanSidang);
                    cmd.Parameters.AddWithValue("iDasar_Bukti", "");
                    cmd.Parameters.AddWithValue("iPelanggaran", "");
                    cmd.Parameters.AddWithValue("iPasal_Pelanggaran", "");
                    cmd.Parameters.AddWithValue("iMengingat", "");
                    cmd.Parameters.AddWithValue("iTembusan", "");

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
        public int Update(clsDPK PD)
        {
            try
            {
                int i = 0;
                Encryption encrypt = new Encryption();
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string MySql = "SIMHUKDIS.sp_DPK_Upd";
                    string TanggalSidang = Convert.ToDateTime(PD.Tanggal_Sidang_DPK).ToString("yyyy-MM-dd");
                    SqlCommand cmd = new SqlCommand(MySql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("iid", PD.ID);
                    cmd.Parameters.AddWithValue("iNIP", PD.NIP);
                    cmd.Parameters.AddWithValue("iCatatan_Sidang", PD.Catatan_Sidang);
                    cmd.Parameters.AddWithValue("iCreate_User", PD.UserLogin);
                    cmd.Parameters.AddWithValue("iTanggal_Sidang", TanggalSidang);
                    cmd.Parameters.AddWithValue("iKeputusanSidang", PD.KeputusanSidang);
                    cmd.Parameters.AddWithValue("iDasar_Bukti", PD.DasarBukti);
                    cmd.Parameters.AddWithValue("iPelanggaran", PD.PelanggaranDisiplin);
                    cmd.Parameters.AddWithValue("iPasal_Pelanggaran", PD.PasalPelanggaran);
                    cmd.Parameters.AddWithValue("iMengingat", PD.Mengingat);
                    cmd.Parameters.AddWithValue("iTembusan", PD.Tembusan);
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
        public clsDPK ListByID(string ID, string NIP)
        {
            int a = 0;
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            clsDPK DP = new clsDPK();
            using (SqlConnection con = new SqlConnection(constr))
            {
                string q = "sp_DPK_Sel_ByID";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("iid", ID);
                cmd.Parameters.AddWithValue("inip", NIP);
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
                    DP.Catatan = rd["Catatan_Sidang"].ToString();
                    DP.Tanggal_Sidang_DPK = rd["tanggal_sidang_dpk"].ToString();
                    DP.Tanggal_Sidang_Pra_DPK = rd["tanggal_sidang_pra_dpk"].ToString();
                    DP.KeputusanSidang = rd["KeputusanSidang"].ToString();
                    DP.hukuman = rd["hukuman"].ToString();
                }
                return DP;
            }
        }

    }
}