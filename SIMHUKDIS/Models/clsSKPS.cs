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
    public class clsSKPS
    {
        [Key]
        public int ID { get; set; }
        [DisplayName("No Surat")]
        public string NoSurat { get; set; }
        [DisplayName("Asal Surat")]
        public string AsalSurat { get; set; }
        [DisplayName("Tanggal Surat")]
        public string TanggalSurat { get; set; }
        [DisplayName("Perihal")]
        public string Perihal { get; set; }
        [DisplayName("Satuan Kerja")]
        public string Satuan_Kerja { get; set; }
        [DisplayName("Kode Satuan Kerja")]
        public string Kode_Satuan_Kerja { get; set; }
        public string Unit_Kerja { get; set; }
        public string Kode_Unit_Kerja { get; set; }
        [DisplayName("Lampiran Surat Pengantar")]
        public string Lampiran_SuratPengantar { get; set; }
        [DisplayName("Lampiran Dokumen Pendukung")]
        public string Lampiran_DokumenPendukung { get; set; }
        [DisplayName("Create Date")]
        public string Create_Date { get; set; }
        [DisplayName("Create User")]
        public string Create_User { get; set; }
        [DisplayName("Update Date")]
        public string Update_Date { get; set; }
        [DisplayName("Update User")]
        public string Update_User { get; set; }
        public int Status { get; set; }
        public string UsulStatus { get; set; }
        public string Reject { get; set; }
        public string RejectReason { get; set; }
        public string RejectDate { get; set; }
        public string Catatan1 { get; set; }
        public string Catatan2 { get; set; }
        public string Catatan3 { get; set; }

        public string Disposisi1_By { get; set; }
        public string Disposisi2_By { get; set; }
        public string Disposisi3_By { get; set; }
        public string Disposisi1_Date { get; set; }
        public string Disposisi2_Date { get; set; }
        public string Disposisi3_Date { get; set; }
        public string FileSK { get; set; }

        public string NIP { get; set; }
        public string NAMA_LENGKAP { get; set; }
        public string GOL_RUANG { get; set; }
        public string LEVEL_JABATAN { get; set; }
        public string SATUAN_KERJA { get; set; }
        public string TEMPAT_LAHIR { get; set; }
        public string TANGGAL_LAHIR { get; set; }
        public string MK_TAHUN { get; set; }
        public string TMT_Pensiun { get; set; }
        public string DasarBukti { get; set; }
        public string PSReason { get; set; }
        public string SKDate { get; set; }
        public string TMTDate { get; set; }
        public string FileSKPS { get; set; }
        public String Menag { get; set; }
        public string Karopeg { get; set; }
        public string NIP_Karopeg { get; set; }
        public string Konseptor { get; set; }
        public string NIP_Konseptor { get; set; }
        public string NIP_Koordinator { get; set; }
        public string Koordinator { get; set; }
        public string NIP_SubKoordinator { get; set; }
        public string SubKoordinator { get; set; }
        public string tembusan { get; set; }
        public string Bulan { get; set; }
        public string Tahun { get; set; }
        public string JenisUsul { get; set; }
        public string Jabatan_Karopeg { get; set; }
        public string Jabatan_Koordinator { get; set; }
        public string Jabatan_SubKoordinator { get; set; }
        public string Jabatan_Konseptor { get; set; }
        public string DasarPemberhentian { get; set; }
        public string TEMBUSAN1 { get; set; }
        public string TEMBUSAN2 { get; set; }
        public string TASPEN { get; set; }
        public string KPPN { get; set; }
        public string NO_SK { get; set; }
        public string Tanggal_SK { get; set; }
        public string DasarPS { get; set; }
        public string PERTEKBKN { get; set; }
        public string KanregBKN { get; set; }

    }
    public class clsSKPSDB
    {
        public List<clsSKPS> PSList(string UserID)
        {
            string constr = ConfigurationManager.ConnectionStrings["dbHukdis"].ConnectionString;
            List<clsSKPS> PS = new List<clsSKPS>();
            using (SqlConnection con = new SqlConnection(constr))
            {
                Int32 pNom = 0;
                string q = "sp_PS_Create";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iUserID", UserID);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    clsSKPS data = new clsSKPS();
                    pNom = pNom + 1;
                    data.ID = Convert.ToInt32(rd["id"].ToString());
                    data.NoSurat = rd["nomor_surat"].ToString();
                    data.AsalSurat = rd["asal_surat"].ToString();
                    data.TanggalSurat = rd["tanggal_surat"].ToString();
                    data.Perihal = rd["perihal"].ToString();
                    data.Satuan_Kerja = rd["SatuanKerja"].ToString();
                    data.Kode_Satuan_Kerja = rd["KODE_SATUAN_KERJA"].ToString();
                    data.Lampiran_SuratPengantar = rd["Lampiran_SuratPengantar"].ToString();
                    data.Lampiran_DokumenPendukung = rd["Lampiran_DokumenPendukung"].ToString();
                    data.Create_Date = rd["created_date"].ToString();
                    data.Create_User = rd["create_user"].ToString();
                    data.Update_Date = rd["update_date"].ToString();
                    data.Update_User = rd["update_user"].ToString();
                    data.Catatan1 = rd["Disposisi1_Notes"].ToString();
                    data.Catatan2 = rd["Disposisi2_Notes"].ToString();
                    data.Catatan3 = rd["Disposisi3_Notes"].ToString();
                    data.Disposisi1_By = rd["Disposisi1_By"].ToString();
                    data.Disposisi2_By = rd["Disposisi2_By"].ToString();
                    data.Disposisi3_By = rd["Disposisi3_By"].ToString();
                    data.Disposisi1_Date = rd["Disposisi1_Date"].ToString();
                    data.Disposisi2_Date = rd["Disposisi2_Date"].ToString();
                    data.Disposisi3_Date = rd["Disposisi3_Date"].ToString();

                    data.Reject = rd["RejectStatus"].ToString();
                    data.RejectDate = rd["RejectDate"].ToString();
                    data.RejectReason = rd["RejectReason"].ToString();

                    data.NIP = rd["NIP"].ToString();
                    data.DasarBukti = rd["DasarBukti"].ToString();
                    data.PSReason = rd["PSReason"].ToString();
                    data.tembusan = rd["tembusan"].ToString();
                    data.SKDate = rd["SKDate"].ToString();
                    data.TMTDate = rd["TMTDate"].ToString();

                    data.Jabatan_Konseptor = rd["Jabatan_Konseptor"].ToString();
                    data.Konseptor = rd["Konseptor"].ToString();

                    PS.Add(data);
                }
                return PS;
            }
        }
        public clsSKPS GetList(int ID)
        {
            string constr = ConfigurationManager.ConnectionStrings["dbHukdis"].ConnectionString;
            clsSKPS data = new clsSKPS();
            using (SqlConnection con = new SqlConnection(constr))
            {
                Int32 pNom = 0;
                string q = "sp_SKPSDtl_Sel";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", ID);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    
                    pNom = pNom + 1;
                    data.ID = Convert.ToInt32(rd["id"].ToString());
                    data.NoSurat = rd["nomor_surat"].ToString();
                    data.DasarPS = rd["DasarPS"].ToString();
                    data.PERTEKBKN = rd["PERTEKBKN"].ToString();
                    data.KanregBKN = rd["Kanreg_BKN"].ToString();
                    data.AsalSurat = rd["asal_surat"].ToString();
                    data.TanggalSurat = rd["tanggal_surat"].ToString();
                    data.Perihal = rd["perihal"].ToString();
                    data.Satuan_Kerja = rd["SatuanKerja"].ToString();
                    data.Kode_Satuan_Kerja = rd["KODE_SATUAN_KERJA"].ToString();
                    data.Unit_Kerja = rd["Unit_Kerja"].ToString();
                    data.Kode_Unit_Kerja = rd["Kode_Unit_Kerja"].ToString();
                    data.Lampiran_SuratPengantar = rd["Lampiran_SuratPengantar"].ToString();
                    data.Lampiran_DokumenPendukung = rd["Lampiran_DokumenPendukung"].ToString();
                    data.Create_Date = rd["created_date"].ToString();
                    data.Create_User = rd["create_user"].ToString();
                    data.Update_Date = rd["update_date"].ToString();
                    data.Update_User = rd["update_user"].ToString();
                    data.Catatan1 = rd["Disposisi1_Notes"].ToString();
                    data.Catatan2 = rd["Disposisi2_Notes"].ToString();
                    data.Catatan3 = rd["Disposisi3_Notes"].ToString();
                    data.Disposisi1_By = rd["Disposisi1_By"].ToString();
                    data.Disposisi2_By = rd["Disposisi2_By"].ToString();
                    data.Disposisi3_By = rd["Disposisi3_By"].ToString();
                    data.Disposisi1_Date = rd["Disposisi1_Date"].ToString();
                    data.Disposisi2_Date = rd["Disposisi2_Date"].ToString();
                    data.Disposisi3_Date = rd["Disposisi3_Date"].ToString();

                    data.Reject = rd["RejectStatus"].ToString();
                    data.RejectDate = rd["RejectDate"].ToString();
                    data.RejectReason = rd["RejectReason"].ToString();

                    data.NIP = rd["NIP"].ToString();
                    data.DasarBukti = rd["DasarBukti"].ToString();
                    data.PSReason = rd["PSReason"].ToString();
                    data.tembusan = rd["tembusan"].ToString();
                    data.SKDate = rd["SKDate"].ToString();
                    data.TMTDate = rd["TMTDate"].ToString();
                    data.JenisUsul = rd["JenisUsul"].ToString();
                    data.NIP_Konseptor = rd["NIP_Konseptor"].ToString();
                    data.Konseptor = rd["Nama_Konseptor"].ToString();
                }
                return data;
            }
        }
        public int Selesai(clsSKPS a)
        {
            int i;
            string constr = ConfigurationManager.ConnectionStrings["dbHukdis"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("[sp_tbl_PemberhentianSementara_SK_Selesai]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", a.ID);
                cmd.Parameters.AddWithValue("@FileSK", a.FileSK);
                cmd.Parameters.AddWithValue("@UserLogin", a.Create_User);
                cmd.Parameters.AddWithValue("@NO_SK", a.NO_SK);
                cmd.Parameters.AddWithValue("@Tanggal_SK", Convert.ToDateTime(a.Tanggal_SK).ToString("yyyy-MM-dd"));
                con.Open();
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }
        public clsSuratMasukMsgInfo GetMsgInfo(int ID, int status)
        {
            string constr = ConfigurationManager.ConnectionStrings["dbHukdis"].ConnectionString;
            clsSuratMasukMsgInfo data = new clsSuratMasukMsgInfo();
            using (SqlConnection con = new SqlConnection(constr))
            {
                string q = "SP_MESSAGEINFO_PS_SELESAI";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@STATUS", status);
                cmd.Parameters.AddWithValue("@ID", ID);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    data.PhoneNo = rd["PhoneNo"].ToString();
                    data.Pesan = rd["Pesan"].ToString();
                }
                return data;
            }
        }

        public int Update (clsSKPS a)
        {
            int i;
            string constr = ConfigurationManager.ConnectionStrings["dbHukdis"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("sp_tbl_PemberhentianSementara_SK_Upd", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", a.ID);
                con.Open();
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }
        public int Insert(clsSKPS t)
        {
            int i;
            string constr = ConfigurationManager.ConnectionStrings["dbHukdis"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("sp_tbl_PemberhentianSementara_SK_Insert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", t.ID);
                cmd.Parameters.AddWithValue("@NIP", t.NIP);
                cmd.Parameters.AddWithValue("@DasarBukti", t.DasarBukti);
                DateTime dParse = DateTime.ParseExact(t.SKDate, "dd MMMM yyyy", null);
                t.SKDate = dParse.ToString("yyyy-MM-dd");
                //t.SKDate = Convert.ToDateTime(t.SKDate).ToString("dd-MM-yyyy");
                //DateTime dParse = DateTime.ParseExact(t.SKDate, "dd MMMM yyyy", null); //Convert.ToDateTime(Data.EstimateDate); 
                //t.SKDate = dParse.ToString();

                cmd.Parameters.AddWithValue("@SKDate", t.SKDate);
                dParse = DateTime.ParseExact(t.TMTDate, "dd MMMM yyyy", null);
                t.TMTDate = dParse.ToString("yyyy-MM-dd");
                //DateTime dParse = DateTime.ParseExact(t.TMTDate, "dd MMMM yyyy", null); //Convert.ToDateTime(Data.EstimateDate); 
                //t.TMTDate = dParse.ToString();

                cmd.Parameters.AddWithValue("@TMTDate", t.TMTDate);
                cmd.Parameters.AddWithValue("@PSReason", t.PSReason);
                cmd.Parameters.AddWithValue("@tembusan", t.tembusan);
                cmd.Parameters.AddWithValue("@Created_User", t.Create_User);
                cmd.Parameters.AddWithValue("@JenisUsul", t.JenisUsul);
                cmd.Parameters.AddWithValue("@DASAR_PS", t.DasarPemberhentian);
                cmd.Parameters.AddWithValue("@PERTEKBKN", t.PERTEKBKN);
                con.Open();
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }

    }
}