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
    public class clsPemberhentianSementara
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
    }
    public class clsPemberhentianSementaraDB
    {
        public List<clsPemberhentianSementara> PS (string userid)
        {
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            List<clsPemberhentianSementara> PemberhentianSementara = new List<clsPemberhentianSementara>();
            using (SqlConnection con = new SqlConnection(constr))
            {
                string q = "sp_PemberhentianSementara_Display";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", userid);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    clsPemberhentianSementara data = new clsPemberhentianSementara();
                    data.ID = Convert.ToInt32(rd["id"].ToString());
                    data.NoSurat = rd["nomor_surat"].ToString();
                    data.AsalSurat = rd["asal_surat"].ToString();
                    data.TanggalSurat = rd["tanggal_surat"].ToString();
                    data.Perihal = rd["perihal"].ToString();
                    data.Satuan_Kerja = rd["Satuan_Kerja"].ToString();
                    data.Kode_Satuan_Kerja = rd["Kode_Satuan_Kerja"].ToString();
                    data.Kode_Unit_Kerja = rd["Kode_Unit_Kerja"].ToString();
                    data.Unit_Kerja = rd["Unit_Kerja"].ToString();

                    data.Lampiran_SuratPengantar = rd["Lampiran_SuratPengantar"].ToString();
                    data.Lampiran_DokumenPendukung = rd["Lampiran_DokumenPendukung"].ToString();

                    data.Create_Date = rd["Created_Date"].ToString();
                    data.Create_User = rd["Created_User"].ToString();
                    data.Update_Date = rd["Update_Date"].ToString();
                    data.Update_User = rd["Update_User"].ToString();


                    data.UsulStatus = rd["Usul_Status"].ToString();
                    PemberhentianSementara.Add(data);
                }
                return PemberhentianSementara;
            }
        }
        public int Insert(clsPemberhentianSementara PS)
        {
            int i;
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("sp_PemberhentianSementara_Ins", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nomor_Surat", PS.NoSurat ?? "");
                cmd.Parameters.AddWithValue("@Asal_Surat", PS.AsalSurat ?? "");
                cmd.Parameters.AddWithValue("@Tanggal_Surat", Convert.ToDateTime(PS.TanggalSurat).ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@Perihal", PS.Perihal ?? "");
                cmd.Parameters.AddWithValue("@Satuan_Kerja", PS.Satuan_Kerja ?? "");
                cmd.Parameters.AddWithValue("@Unit_Kerja", PS.Kode_Unit_Kerja ?? "");
                cmd.Parameters.AddWithValue("@Usul_Status", PS.UsulStatus);
                cmd.Parameters.AddWithValue("@Status", PS.Status);
                cmd.Parameters.AddWithValue("@Lampiran_SuratPengantar", PS.Lampiran_SuratPengantar ?? "");
                cmd.Parameters.AddWithValue("@Lampiran_DokumenPendukung", PS.Lampiran_DokumenPendukung ?? "");
                cmd.Parameters.AddWithValue("@Created_User", PS.Create_User);
                con.Open();
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }
        public int Update(clsPemberhentianSementara PS)
        {
            int i;
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("sp_PemberhentianSementara_Upd", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", PS.ID);
                cmd.Parameters.AddWithValue("@Nomor_Surat", PS.NoSurat ?? "");
                cmd.Parameters.AddWithValue("@Asal_Surat", PS.AsalSurat ?? "");
                cmd.Parameters.AddWithValue("@Tanggal_Surat", Convert.ToDateTime(PS.TanggalSurat).ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@Perihal", PS.Perihal ?? "");
                cmd.Parameters.AddWithValue("@Satuan_Kerja", PS.Kode_Satuan_Kerja);
                cmd.Parameters.AddWithValue("@Unit_Kerja", PS.Kode_Unit_Kerja);
                cmd.Parameters.AddWithValue("@Usul_Status", PS.UsulStatus);
                cmd.Parameters.AddWithValue("@Status", PS.Status);
                cmd.Parameters.AddWithValue("@Lampiran_SuratPengantar", PS.Lampiran_SuratPengantar ?? "");
                cmd.Parameters.AddWithValue("@Lampiran_DokumenPendukung", PS.Lampiran_DokumenPendukung ?? "");
                cmd.Parameters.AddWithValue("@Update_User", PS.Update_User);
                con.Open();
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }
        public int Proses(clsPemberhentianSementara PS)
        {
            int i;
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("sp_PemberhentianSementara_Proses", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", PS.ID);
                cmd.Parameters.AddWithValue("@Update_User", PS.Update_User);
                con.Open();
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }
        public int Delete(int ID)
        {
            int i;
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("sp_PemberhentianSementara_Del", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);
                con.Open();
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }
        public clsPemberhentianSementara GetList(int ID)
        {
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            clsPemberhentianSementara data = new clsPemberhentianSementara();
            using (SqlConnection con = new SqlConnection(constr))
            {
                Int32 pNom = 0;
                string q = "sp_PemberhentianSementara_Sel";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    pNom = pNom + 1;
                    data.ID = Convert.ToInt32(rd["id"].ToString());
                    data.NoSurat = rd["nomor_surat"].ToString();
                    data.AsalSurat = rd["asal_surat"].ToString();
                    data.TanggalSurat = rd["tanggal_surat"].ToString();
                    data.Perihal = rd["perihal"].ToString();
                    data.Satuan_Kerja = rd["Satuan_Kerja"].ToString();
                    data.Kode_Satuan_Kerja = rd["Kode_Satuan_Kerja"].ToString();
                    data.Kode_Unit_Kerja = rd["Kode_Unit_Kerja"].ToString();
                    data.Unit_Kerja = rd["Unit_Kerja"].ToString();

                    data.Lampiran_SuratPengantar = rd["Lampiran_SuratPengantar"].ToString();
                    data.Lampiran_DokumenPendukung = rd["Lampiran_DokumenPendukung"].ToString();

                    data.Create_Date = rd["Created_Date"].ToString();
                    data.Create_User = rd["Created_UserName"].ToString();
                    data.Update_Date = rd["Update_Date"].ToString();
                    data.Update_User = rd["Update_UserName"].ToString();
                }
                return data;
            }
        }
    }
}