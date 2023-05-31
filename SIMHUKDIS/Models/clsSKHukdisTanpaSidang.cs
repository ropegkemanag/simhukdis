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
    public class clsSKHukdisTanpaSidang
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
        public string Catatan_Pra_DPK { get; set; }
        public string UserLogin { get; set; }
        public string Tanggal_Sidang_Pra_DPK { get; set; }
        public string Tanggal_Sidang_DPK { get; set; }
        public string KeputusanSidang { get; set; }
        public string Catatan_Sidang { get; set; }
        public string hukuman { get; set; }
        public string Keputusan_Menteri { get; set; }
        public string Mengingat { get; set; }
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
        public string Tembusan { get; set; }
        public string SATKER_1 { get; set; }
        public string SATKER_2 { get; set; }
        public string SATKER_3 { get; set; }
        public string SATKER_4 { get; set; }
        public string SATKER_5 { get; set; }
        public string Unit_Kerja { get; set; }
        public string Kode_Unit_Kerja { get; set; }
        public string TEMPAT_LAHIR { get; set; }
        public string TANGGAL_LAHIR { get; set; }
        public string MK_TAHUN { get; set; }
        public string TMT_PENSIUN { get; set; }
        public string Jabatan_Konseptor { get; set; }
        public string Jabatan_subkoor { get; set; }
        public string Jabatan_koor { get; set; }
        public string Tanggal_SK { get; set; }
        public string NO_SK { get; set; }
        public string FILE_SK { get; set; }
        public string Keterangan_SK { get; set; }
        public string Tanggal_Penyampaian_Ke_Satker { get; set; }
        public string Penerima_Satker { get; set; }
        public string BAP_Satker { get; set; }
        public string Keterangan_Satker { get; set; }
        public string Tanggal_Penyampaian_Ke_YBS { get; set; }
        public string Penerima_YBS { get; set; }
        public string BAP_Penerima { get; set; }
        public string Keterangan_Penerima { get; set; }
    }
    public class clsSKHukdisTanpaSidangDB
    {
        public List<clsHasilSidang> ListTanpaSidang(string UserID)
        {
            int a = 0;
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            List<clsHasilSidang> DP = new List<clsHasilSidang>();
            using (SqlConnection con = new SqlConnection(constr))
            {
                string q = "[sp_TanpaSidangDPK_Sel]";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("UserID", UserID);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    clsHasilSidang data = new clsHasilSidang();
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
                    data.Catatan_Pra_DPK = rd["Catatan"].ToString();
                    data.Tanggal_Sidang_DPK = rd["tanggal_sidang_dpk"].ToString();
                    data.Tanggal_Sidang_Pra_DPK = rd["tanggal_sidang_pra_dpk"].ToString();
                    data.KeputusanSidang = rd["KeputusanSidang"].ToString();
                    data.hukuman = rd["hukuman"].ToString();
                    data.Catatan_Sidang = rd["Catatan_Sidang"].ToString();
                    data.Tembusan = rd["Tembusan"].ToString();
                    data.Catatan_Sidang = rd["Catatan_Sidang"].ToString();
                    data.Tembusan = rd["Tembusan"].ToString();
                    data.Kode_Unit_Kerja = rd["Kode_Unit_Kerja"].ToString();
                    data.Unit_Kerja = rd["Unit_Kerja"].ToString();
                    data.NO_SK = rd["NO_SK"].ToString();
                    data.Tanggal_SK = rd["Tanggal_SK"].ToString();
                    data.FILE_SK = rd["FILE_SK"].ToString();
                    data.Keterangan_SK = rd["Keterangan_SK"].ToString();
                    data.Tanggal_Penyampaian_Ke_Satker = rd["Tanggal_Penyampaian_Ke_Satker"].ToString();
                    data.Penerima_Satker = rd["Penerima_Satker"].ToString();
                    data.BAP_Satker = rd["BAP_Satker"].ToString();
                    data.Keterangan_Satker = rd["Keterangan_Satker"].ToString();
                    data.Tanggal_Penyampaian_Ke_YBS = rd["Tanggal_Penyampaian_Ke_YBS"].ToString();
                    data.Penerima_YBS = rd["Penerima_YBS"].ToString();
                    data.BAP_Penerima = rd["BAP_Penerima"].ToString();
                    data.Keterangan_Penerima = rd["Keterangan_Penerima"].ToString();
                    clsPraDPKDB x = new clsPraDPKDB();
                    List<clsDataPegawaiDtl> y = new List<clsDataPegawaiDtl>();
                    if (data.NIP != "")
                    {
                        //y = x.GetPegawai(data.NIP);
                        //int z = 1;
                        //foreach (var item in y)
                        //{
                        //    if (z == 1)
                        //    {
                        //        data.GOL_RUANG = item.GOL_RUANG;
                        //        data.LEVEL_JABATAN = item.LEVEL_JABATAN;
                        //        data.UnitKerja = item.KETERANGAN_SATUAN_KERJA;
                        //        data.TEMPAT_LAHIR = item.TEMPAT_LAHIR;
                        //        data.TANGGAL_LAHIR = item.TANGGAL_LAHIR;
                        //        data.MK_TAHUN = item.MK_TAHUN;
                        //        data.TMT_PENSIUN = item.TMT_PENSIUN;
                        //        data.SATKER_1 = item.SATKER_1;
                        //        data.SATKER_2 = item.SATKER_2;
                        //        data.SATKER_3 = item.SATKER_3;
                        //        data.SATKER_4 = item.SATKER_4;
                        //        data.SATKER_5 = item.SATKER_5;
                        //    }
                        //    z = z + 1;
                        //}
                    }

                    DP.Add(data);
                }
                return DP;
            }
        }
        public int Delete(int ID, string NIP)
        {
            try
            {
                int i = 0;
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string MySql = "SP_SKTANPASIDANGDPK_DEL";
                    SqlCommand cmd = new SqlCommand(MySql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@NIP", NIP);
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

        public clsHasilSidang ListTanpaSidangByID(string UserID, int ID, string NIP)
        {
            int a = 0;
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            clsHasilSidang data = new clsHasilSidang();
            using (SqlConnection con = new SqlConnection(constr))
            {
                string q = "sp_TanpaSidangDPKByID_Sel";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@NIP", NIP);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    
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
                    data.Catatan_Pra_DPK = rd["Catatan"].ToString();
                    data.Tanggal_Sidang_DPK = rd["tanggal_sidang_dpk"].ToString();
                    data.Tanggal_Sidang_Pra_DPK = rd["tanggal_sidang_pra_dpk"].ToString();
                    data.KeputusanSidang = rd["KeputusanSidang"].ToString();
                    data.hukuman = rd["hukuman"].ToString();
                    data.Catatan_Sidang = rd["Catatan_Sidang"].ToString();
                    data.Tembusan = rd["Tembusan"].ToString();
                    data.Catatan_Sidang = rd["Catatan_Sidang"].ToString();
                    data.Tembusan = rd["Tembusan"].ToString();
                    data.Kode_Unit_Kerja = rd["Kode_Unit_Kerja"].ToString();
                    data.Unit_Kerja = rd["Unit_Kerja"].ToString();
                    data.NO_SK = rd["NO_SK"].ToString();
                    data.Tanggal_SK = rd["Tanggal_SK"].ToString();
                    data.FILE_SK = rd["FILE_SK"].ToString();
                    data.Keterangan_SK = rd["Keterangan_SK"].ToString();
                    data.Tanggal_Penyampaian_Ke_Satker = rd["Tanggal_Penyampaian_Ke_Satker"].ToString();
                    data.Penerima_Satker = rd["Penerima_Satker"].ToString();
                    data.BAP_Satker = rd["BAP_Satker"].ToString();
                    data.Keterangan_Satker = rd["Keterangan_Satker"].ToString();
                    data.Tanggal_Penyampaian_Ke_YBS = rd["Tanggal_Penyampaian_Ke_YBS"].ToString();
                    data.Penerima_YBS = rd["Penerima_YBS"].ToString();
                    data.BAP_Penerima = rd["BAP_Penerima"].ToString();
                    data.Keterangan_Penerima = rd["Keterangan_Penerima"].ToString();  
                    data.TMTDate = rd["TMTDate"].ToString();
                }
                return data;
            }
        }
    }
}