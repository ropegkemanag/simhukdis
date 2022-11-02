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
    public class clsSuratMasuk
    {
        [Key]
        public int ID { get; set; }
        [DisplayName("No")]
        public int No { get; set; }
        [DisplayName("No Agenda")]
        public string NoAgenda { get; set; }
        [DisplayName("No Surat")]
        public string NoSurat { get; set; }
        [DisplayName("Asal Surat")]
        public string AsalSurat { get; set; }
        public string TanggalSurat { get; set; }
        [DisplayName("Perihal")]
        public string perihal { get; set; }
        [DisplayName("Satuan Kerja")]
        public string SATUAN_KERJA { get; set; }
        [DisplayName("Kode Satuan Kerja")]
        public string KODE_SATUAN_KERJA { get; set; }
        [DisplayName("Lampiran Surat Pengantar")]
        public string LampiranSurat1 { get; set; }
        [DisplayName("Lampiran BAP")]
        public string LampiranSurat2 { get; set; }
        [DisplayName("Lampiran Surat Tugas Pemeriksaan")]
        public string LampiranSurat3 { get; set; }
        [DisplayName("Lampiran Surat Panggilan")]
        public string LampiranSurat4 { get; set; }
        [DisplayName("Lampiran Surat Dokumen Lainnya")]
        public string LampiranSurat5 { get; set; }
        [DisplayName("Lampiran Putusan Pengadilan")]
        public string LampiranSurat6 { get; set; }
        [DisplayName("Lampiran LHA/STL")]
        public string LampiranSurat_LHA { get; set; }
        [DisplayName("Lampiran Laporan Hasil Pemeriksaan")]
        public string LampiranLHP { get; set; }
        [DisplayName("Lampiran Laporan Surat Pernyataan Tanggung Jawab Mutlak")]
        public string LampiranSPTJM { get; set; }
        [DisplayName("Dokumen yang akan di buat Konseptor")]
        public string Dokumen_Yang_Akan_Dibuat { get; set; }

        public HttpPostedFile LampiranSuratFile { get; set; }
        [DisplayName("Create Date")]
        public string CreateDate { get; set; }
        [DisplayName("Create User")]
        public string CreateUser { get; set; }
        [DisplayName("Update Date")]
        public string UpdateDate { get; set; }
        [DisplayName("Update User")]
        public string UpdatUser { get; set; }
        public string isLampiranSurat6 { get; set; }
        [DisplayName("Catatan Kepala Biro")]
        public string Catatan1 { get; set; }
        [DisplayName("Catatan Koordinator Bagian")]
        public string Catatan2 { get; set; }
        [DisplayName("Catatan Koordinator Sub Bagian")]
        public string Catatan3 { get; set; }

        [DisplayName("Disposisi Kepala Biro oleh")]
        public string DisposisiBy1 { get; set; }
        [DisplayName("Disposisi Koordinator Bagian Oleh")]
        public string DisposisiBy2 { get; set; }
        [DisplayName("Disposisi Koordinator Sub Bagian Oleh")]
        public string DisposisiBy3 { get; set; }

        [DisplayName("Disposisi Status Kepala Biro")]
        public string DisposisiStatus1 { get; set; }
        [DisplayName("Disposisi Status Koordinator Bagian")]
        public string DisposisiStatus2 { get; set; }
        [DisplayName("Disposisi Status Koordinator Sub Bagian")]
        public string DisposisiStatus3 { get; set; }

        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString ="{0:dd/MMM/yyyy}")]
        [DisplayName("Tanggal Disposisi Kepala Biro")]
        public string DisposisiDate1 { get; set; }
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString ="{0:dd/MMM/yyyy}")]
        [DisplayName("Tanggal Disposisi Koordinator Bagian")]
        public string DisposisiDate2 { get; set; }
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString ="{0:dd/MMM/yyyy}")]
        [DisplayName("Tanggal Disposisi Koordinator Sub Bagian")]
        public string DisposisiDate3 { get; set; }
        public int Status { get; set; }
        public string Unit_Kerja { get; set; }
        public string Kode_Unit_Kerja { get; set; }
        public string UsulStatus { get; set; }
    }
    public class clsSPTMJ
    {
        public string id { get; set; }
        public string NIP { get; set; }
        public string NAMA_LENGKAP { get; set; }
        public string GOL_RUANG { get; set; }
        public string LEVEL_JABATAN { get; set; }
        public string Satker { get; set; }
        public string NIP_USUL { get; set; }
        public string NAMA_LENGKAP_USUL { get; set; }
        public string Tanggal_Surat { get; set; }
        public string Created_Date { get; set; }
        public string Created_User { get; set; }
        public string Updated_Date { get; set; }
        public string Updated_User { get; set; }
    }
    public class clsLampiranSurat
    {
        [DisplayName("Lampiran Surat 1")]
        public string LampiranSurat1 { get; set; }
        [DisplayName("Lampiran Surat 2")]
        public string LampiranSurat2 { get; set; }
        [DisplayName("Lampiran Surat 3")]
        public string LampiranSurat3 { get; set; }
        [DisplayName("Lampiran Surat 4")]
        public string LampiranSurat4 { get; set; }
        [DisplayName("Lampiran Surat 5")]
        public string LampiranSurat5 { get; set; }
    }
    public class clsSuratMasukFilter
    {
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public int Status { get; set; }
        public string GroupID { get; set; }
        public string UserLogin { get; set; }
    }
    public class clsMsg
    {
        public string Msg { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string Status { get; set; }
    }
    public class clsUnitKerja
    {
        public string Unit_Kerja { get; set; }
        public string Kode_Unit_Kerja { get; set; }
    }
    public class clsSuratMasukDB
    {
        public List<clsSuratMasuk> SM
        {
            get
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                List<clsSuratMasuk> SuratMasuks = new List<clsSuratMasuk>();
                using (SqlConnection con = new SqlConnection(constr))
                {
                    Int32 pNom = 0;
                    string q ="SIMHUKDIS.sp_SuratMasuk_Display";
                    SqlCommand cmd = new SqlCommand(q, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        clsSuratMasuk data = new clsSuratMasuk();
                        pNom = pNom + 1;
                        data.No = pNom;
                        data.ID = Convert.ToInt32(rd["id"].ToString());
                        data.NoAgenda = rd["nomor_agenda"].ToString();
                        data.NoSurat = rd["nomor_surat"].ToString();
                        data.AsalSurat = rd["asal_surat"].ToString();
                        data.TanggalSurat = rd["tanggal_surat"].ToString();
                        //if (!Convert.IsDBNull(rd["tanggal_surat"]))
                        //{
                        //    data.TanggalSurat = Convert.ToDateTime(rd["tanggal_surat"].ToString());
                        //}
                        data.perihal = rd["perihal"].ToString();
                        data.SATUAN_KERJA = rd["SatuanKerja"].ToString();
                        data.KODE_SATUAN_KERJA = rd["KODE_SATUAN_KERJA"].ToString();
                        data.LampiranSurat1 = rd["LampiranSurat1"].ToString();
                        data.LampiranSurat2 = rd["LampiranSurat2"].ToString();
                        data.LampiranSurat3 = rd["LampiranSurat3"].ToString();
                        data.LampiranSurat4 = rd["LampiranSurat4"].ToString();
                        data.LampiranSurat5 = rd["LampiranSurat5"].ToString();
                        data.LampiranSurat_LHA = rd["Lampiran_LHA"].ToString();
                        data.LampiranSurat6 = rd["LampiranSurat6"].ToString();
                        data.LampiranLHP = rd["LampiranLHP"].ToString();
                        data.LampiranSPTJM = rd["LampiranSPTJM"].ToString();
                        data.CreateDate = rd["created_date"].ToString();
                        //if (!Convert.IsDBNull(rd["created_date"]))
                        //{
                        //    data.CreateDate = Convert.ToDateTime(rd["created_date"].ToString());
                        //}
                        data.CreateUser = rd["create_user"].ToString();
                        data.UpdateDate = rd["update_date"].ToString();
                        //if (!Convert.IsDBNull(rd["update_date"]))
                        //{
                        //    data.UpdateDate = Convert.ToDateTime(rd["update_date"].ToString());
                        //}
                        data.UpdatUser = rd["update_user"].ToString();
                        data.Catatan1 = rd["Disposisi1_Notes"].ToString();
                        data.Catatan2 = rd["Disposisi2_Notes"].ToString();
                        data.Catatan3 = rd["Disposisi3_Notes"].ToString();
                        data.DisposisiStatus1 = rd["Disposisi1_Status"].ToString();
                        data.DisposisiStatus2 = rd["Disposisi2_Status"].ToString();
                        data.DisposisiStatus3 = rd["Disposisi3_Status"].ToString();
                        data.DisposisiBy1 = rd["Disposisi1_By"].ToString();
                        data.DisposisiBy2 = rd["Disposisi2_By"].ToString();
                        data.DisposisiBy3 = rd["Disposisi3_By"].ToString();
                        data.DisposisiDate1 = rd["Disposisi1_Date"].ToString();
                        data.DisposisiDate2 = rd["Disposisi2_Date"].ToString();
                        data.DisposisiDate3 = rd["Disposisi3_Date"].ToString();
                        data.Kode_Unit_Kerja = rd["Kode_Unit_Kerja"].ToString();
                        data.Unit_Kerja = rd["Unit_Kerja"].ToString();
                        data.UsulStatus = rd["UsulStatus"].ToString();
                        SuratMasuks.Add(data);
                    }
                    return SuratMasuks;
                }
            }
        }
        public List<clsSuratMasuk> SuratMasukFilter(clsSuratMasukFilter filter)
        {
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            List<clsSuratMasuk> SuratMasuks = new List<clsSuratMasuk>();
            using (SqlConnection con = new SqlConnection(constr))
            {
                Int32 pNom = 0;
                string q ="sp_SuratMasuk_Filter_Sel";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iStatus", filter.Status);
                cmd.Parameters.AddWithValue("@DateFrom", filter.DateFrom);
                cmd.Parameters.AddWithValue("@DateTo", filter.DateTo);
                cmd.Parameters.AddWithValue("@iGroupID", filter.GroupID);
                cmd.Parameters.AddWithValue("@UserLogin", filter.UserLogin);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    clsSuratMasuk data = new clsSuratMasuk();
                    pNom = pNom + 1;
                    data.No = pNom;
                    data.ID = Convert.ToInt32(rd["id"].ToString());
                    data.Status = Convert.ToInt32(rd["STATUS"].ToString());
                    data.NoAgenda = rd["nomor_agenda"].ToString();
                    data.NoSurat = rd["nomor_surat"].ToString();
                    data.AsalSurat = rd["asal_surat"].ToString();
                    data.TanggalSurat = rd["tanggal_surat"].ToString();
                    //if (!Convert.IsDBNull(rd["tanggal_surat"]))
                    //{
                    //    data.TanggalSurat = Convert.ToDateTime(rd["tanggal_surat"].ToString());
                    //}
                    data.perihal = rd["perihal"].ToString();
                    data.SATUAN_KERJA = rd["SatuanKerja"].ToString();
                    data.KODE_SATUAN_KERJA = rd["KODE_SATUAN_KERJA"].ToString();
                    data.LampiranSurat1 = rd["LampiranSurat1"].ToString();
                    data.LampiranSurat2 = rd["LampiranSurat2"].ToString();
                    data.LampiranSurat3 = rd["LampiranSurat3"].ToString();
                    data.LampiranSurat4 = rd["LampiranSurat4"].ToString();
                    data.LampiranSurat5 = rd["LampiranSurat5"].ToString();
                    data.LampiranSurat_LHA = rd["Lampiran_LHA"].ToString();
                    data.LampiranSurat6 = rd["LampiranSurat6"].ToString();
                    data.LampiranLHP = rd["LampiranLHP"].ToString();
                    data.LampiranSPTJM = rd["LampiranSPTJM"].ToString();

                    data.CreateDate = rd["created_date"].ToString();
                    //if (!Convert.IsDBNull(rd["created_date"]))
                    //{
                    //    data.CreateDate = Convert.ToDateTime(rd["created_date"].ToString());
                    //}
                    data.CreateUser = rd["create_user"].ToString();
                    data.UpdateDate = rd["update_date"].ToString();
                    //if (!Convert.IsDBNull(rd["update_date"]))
                    //{
                    //    data.UpdateDate = Convert.ToDateTime(rd["update_date"].ToString());
                    //}
                    data.UpdatUser = rd["update_user"].ToString();
                    data.Catatan1 = rd["Disposisi1_Notes"].ToString();
                    data.Catatan2 = rd["Disposisi2_Notes"].ToString();
                    data.Catatan3 = rd["Disposisi3_Notes"].ToString();
                    data.DisposisiStatus1 = rd["Disposisi1_Status"].ToString();
                    data.DisposisiStatus2 = rd["Disposisi2_Status"].ToString();
                    data.DisposisiStatus3 = rd["Disposisi3_Status"].ToString();
                    data.DisposisiBy1 = rd["Disposisi1_By"].ToString();
                    data.DisposisiBy2 = rd["Disposisi2_By"].ToString();
                    data.DisposisiBy3 = rd["Disposisi3_By"].ToString();
                    data.DisposisiDate1 = rd["Disposisi1_Date"].ToString();
                    data.DisposisiDate2 = rd["Disposisi2_Date"].ToString();
                    data.DisposisiDate3 = rd["Disposisi3_Date"].ToString();
                    data.Kode_Unit_Kerja = rd["Kode_Unit_Kerja"].ToString();
                    data.Unit_Kerja = rd["Unit_Kerja"].ToString();
                    data.UsulStatus = rd["UsulStatus"].ToString();
                    SuratMasuks.Add(data);
                }
                return SuratMasuks;
            }
        }
        public List<clsSuratMasuk> SuratMasukItjenFilter(clsSuratMasukFilter filter)
        {
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            List<clsSuratMasuk> SuratMasuks = new List<clsSuratMasuk>();
            using (SqlConnection con = new SqlConnection(constr))
            {
                Int32 pNom = 0;
                string q = "SIMHUKDIS.sp_SuratMasuk_Itjen_Sel";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("iStatus", filter.Status);
                cmd.Parameters.AddWithValue("DateFrom", filter.DateFrom);
                cmd.Parameters.AddWithValue("DateTo", filter.DateTo);
                cmd.Parameters.AddWithValue("UserLogin", filter.UserLogin);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    clsSuratMasuk data = new clsSuratMasuk();
                    pNom = pNom + 1;
                    data.No = pNom;
                    data.ID = Convert.ToInt32(rd["id"].ToString());
                    data.Status = Convert.ToInt32(rd["STATUS"].ToString());
                    data.NoAgenda = rd["nomor_agenda"].ToString();
                    data.NoSurat = rd["nomor_surat"].ToString();
                    data.AsalSurat = rd["asal_surat"].ToString();
                    data.TanggalSurat = rd["tanggal_surat"].ToString();
                    //if (!Convert.IsDBNull(rd["tanggal_surat"]))
                    //{
                    //    data.TanggalSurat = Convert.ToDateTime(rd["tanggal_surat"].ToString());
                    //}
                    data.perihal = rd["perihal"].ToString();
                    data.SATUAN_KERJA = rd["SatuanKerja"].ToString();
                    data.KODE_SATUAN_KERJA = rd["KODE_SATUAN_KERJA"].ToString();
                    data.LampiranSurat1 = rd["LampiranSurat1"].ToString();
                    data.LampiranSurat2 = rd["LampiranSurat2"].ToString();
                    data.LampiranSurat3 = rd["LampiranSurat3"].ToString();
                    data.LampiranSurat4 = rd["LampiranSurat4"].ToString();
                    data.LampiranSurat5 = rd["LampiranSurat5"].ToString();
                    data.LampiranSurat_LHA = rd["Lampiran_LHA"].ToString();
                    data.LampiranSurat6 = rd["LampiranSurat6"].ToString();
                    data.LampiranLHP = rd["LampiranLHP"].ToString();
                    data.LampiranSPTJM = rd["LampiranSPTJM"].ToString();

                    data.CreateDate = rd["created_date"].ToString();
                    //if (!Convert.IsDBNull(rd["created_date"]))
                    //{
                    //    data.CreateDate = Convert.ToDateTime(rd["created_date"].ToString());
                    //}
                    data.CreateUser = rd["create_user"].ToString();
                    data.UpdateDate = rd["update_date"].ToString();
                    //if (!Convert.IsDBNull(rd["update_date"]))
                    //{
                    //    data.UpdateDate = Convert.ToDateTime(rd["update_date"].ToString());
                    //}
                    data.UpdatUser = rd["update_user"].ToString();
                    data.Catatan1 = rd["Disposisi1_Notes"].ToString();
                    data.Catatan2 = rd["Disposisi2_Notes"].ToString();
                    data.Catatan3 = rd["Disposisi3_Notes"].ToString();
                    data.DisposisiStatus1 = rd["Disposisi1_Status"].ToString();
                    data.DisposisiStatus2 = rd["Disposisi2_Status"].ToString();
                    data.DisposisiStatus3 = rd["Disposisi3_Status"].ToString();
                    data.DisposisiBy1 = rd["Disposisi1_By"].ToString();
                    data.DisposisiBy2 = rd["Disposisi2_By"].ToString();
                    data.DisposisiBy3 = rd["Disposisi3_By"].ToString();
                    data.DisposisiDate1 = rd["Disposisi1_Date"].ToString();
                    data.DisposisiDate2 = rd["Disposisi2_Date"].ToString();
                    data.DisposisiDate3 = rd["Disposisi3_Date"].ToString();
                    data.Kode_Unit_Kerja = rd["Kode_Unit_Kerja"].ToString();
                    data.Unit_Kerja = rd["Unit_Kerja"].ToString();
                    data.UsulStatus = rd["UsulStatus"].ToString();
                    SuratMasuks.Add(data);
                }
                return SuratMasuks;
            }
        }
        public clsSuratMasuk GetList(int ID)
        {
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            clsSuratMasuk data = new clsSuratMasuk();
            using (SqlConnection con = new SqlConnection(constr))
            {
                Int32 pNom = 0;
                string q ="SIMHUKDIS.sp_SuratMasuk_Sel";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("iid", ID);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    pNom = pNom + 1;
                    data.No = pNom;
                    data.ID = Convert.ToInt32(rd["id"].ToString());
                    data.NoAgenda = rd["nomor_agenda"].ToString();
                    data.NoSurat = rd["nomor_surat"].ToString();
                    data.AsalSurat = rd["asal_surat"].ToString();
                    data.TanggalSurat = rd["tanggal_surat"].ToString();
                    //if (!Convert.IsDBNull(rd["tanggal_surat"]))
                    //{
                    //    data.TanggalSurat = Convert.ToDateTime(rd["tanggal_surat"].ToString());
                    //}
                    data.perihal = rd["perihal"].ToString();
                    data.SATUAN_KERJA = rd["SatuanKerja"].ToString();
                    data.KODE_SATUAN_KERJA = rd["KODE_SATUAN_KERJA"].ToString();
                    data.LampiranSurat1 = rd["LampiranSurat1"].ToString();
                    data.LampiranSurat2 = rd["LampiranSurat2"].ToString();
                    data.LampiranSurat3 = rd["LampiranSurat3"].ToString();
                    data.LampiranSurat4 = rd["LampiranSurat4"].ToString();
                    data.LampiranSurat5 = rd["LampiranSurat5"].ToString();
                    data.LampiranSurat_LHA = rd["Lampiran_LHA"].ToString();
                    data.LampiranSurat6 = rd["LampiranSurat6"].ToString();
                    data.LampiranLHP = rd["LampiranLHP"].ToString();
                    data.LampiranSPTJM = rd["LampiranSPTJM"].ToString();

                    data.CreateDate = rd["created_date"].ToString();
                    //if (!Convert.IsDBNull(rd["created_date"]))
                    //{
                    //    data.CreateDate = Convert.ToDateTime(rd["created_date"].ToString());
                    //}
                    data.CreateUser = rd["create_user"].ToString();
                    data.UpdateDate = rd["update_date"].ToString();
                    //if (!Convert.IsDBNull(rd["update_date"]))
                    //{
                    //    data.UpdateDate = Convert.ToDateTime(rd["update_date"].ToString());
                    //}
                    data.UpdatUser = rd["update_user"].ToString();
                    data.Catatan1 = rd["Disposisi1_Notes"].ToString();
                    data.Catatan2 = rd["Disposisi2_Notes"].ToString();
                    data.Catatan3 = rd["Disposisi3_Notes"].ToString();
                    data.DisposisiStatus1 = rd["Disposisi1_Status"].ToString();
                    data.DisposisiStatus2 = rd["Disposisi2_Status"].ToString();
                    data.DisposisiStatus3 = rd["Disposisi3_Status"].ToString();
                    data.DisposisiBy1 = rd["Disposisi1_By"].ToString();
                    data.DisposisiBy2 = rd["Disposisi2_By"].ToString();
                    data.DisposisiBy3 = rd["Disposisi3_By"].ToString();
                    data.DisposisiDate1 = rd["Disposisi1_Date"].ToString();
                    data.DisposisiDate2 = rd["Disposisi2_Date"].ToString();
                    data.DisposisiDate3 = rd["Disposisi3_Date"].ToString();
                    data.Kode_Unit_Kerja = rd["Kode_Unit_Kerja"].ToString();
                    data.Unit_Kerja = rd["Unit_Kerja"].ToString();
                    data.UsulStatus = rd["UsulStatus"].ToString();
                }
                return data;
                }
        }
        public int Insert(clsSuratMasuk suratMasuk)
        {
            int i;
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("SIMHUKDIS.sp_SuratMasuk_Ins", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //DateTime dy = DateTime.ParseExact(suratMasuk.TanggalSurat,"dd-MM-yyyy",
                //                       System.Globalization.CultureInfo.InvariantCulture);
                cmd.Parameters.AddWithValue("inomor_agenda", "");
                cmd.Parameters.AddWithValue("inomor_surat", suratMasuk.NoSurat);
                cmd.Parameters.AddWithValue("iasal_surat", suratMasuk.AsalSurat);
                cmd.Parameters.AddWithValue("itanggal_surat", Convert.ToDateTime(suratMasuk.TanggalSurat).ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("iperihal", suratMasuk.perihal);
                cmd.Parameters.AddWithValue("iLampiranSurat1", suratMasuk.LampiranSurat1 ?? "");
                cmd.Parameters.AddWithValue("iLampiranSurat2", suratMasuk.LampiranSurat2 ?? "");
                cmd.Parameters.AddWithValue("iLampiranSurat3", suratMasuk.LampiranSurat3 ?? "");
                cmd.Parameters.AddWithValue("iLampiranSurat4", suratMasuk.LampiranSurat4 ?? "");
                cmd.Parameters.AddWithValue("iLampiranSurat5", suratMasuk.LampiranSurat5 ?? "");
                cmd.Parameters.AddWithValue("iLampiranSurat6", suratMasuk.LampiranSurat6 ?? "");
                cmd.Parameters.AddWithValue("iLampiranLHP", suratMasuk.LampiranLHP ?? "");
                cmd.Parameters.AddWithValue("iLampiranSPTJM", suratMasuk.LampiranSPTJM ?? "");
                cmd.Parameters.AddWithValue("iSatuanKerja", suratMasuk.SATUAN_KERJA);
                cmd.Parameters.AddWithValue("icreate_user", suratMasuk.CreateUser);
                cmd.Parameters.AddWithValue("iLampiran_LHA", suratMasuk.LampiranSurat_LHA);
                cmd.Parameters.AddWithValue("iUnit_Kerja", suratMasuk.Kode_Unit_Kerja);
                cmd.Parameters.AddWithValue("iUsulStatus", suratMasuk.UsulStatus);
                con.Open();
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }
        public int InsertSPTJM(clsSPTMJ s)
        {
            int i;
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("sp_SuratMasuk_SPTJM_Ins", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NIP_Atasan", s.NIP);
                cmd.Parameters.AddWithValue("@Nama_Lengkap_Atasan", s.NAMA_LENGKAP);
                cmd.Parameters.AddWithValue("@PangkatGolongan_Atasan", "");
                cmd.Parameters.AddWithValue("@Jabatan_Atasan", s.LEVEL_JABATAN);
                cmd.Parameters.AddWithValue("@Unit_Kerja_Atasan", s.Satker);
                cmd.Parameters.AddWithValue("@Tanggal_Surat", Convert.ToDateTime(s.Tanggal_Surat).ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@NIP_yang_Diusulkan", s.NIP_USUL);
                cmd.Parameters.AddWithValue("@Nama_yang_Diusulkan", s.NAMA_LENGKAP_USUL);
                cmd.Parameters.AddWithValue("@Created_User", s.Created_User);
                con.Open();
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }
        public int Edit(clsSuratMasuk suratMasuk)
        {
            int i;
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("SIMHUKDIS.sp_SuratMasuk_Upd", con);
                cmd.CommandType = CommandType.StoredProcedure;

                //DateTime dy = DateTime.ParseExact(suratMasuk.TanggalSurat,"dd-MM-yyyy",
                //                       System.Globalization.CultureInfo.InvariantCulture);
                cmd.Parameters.AddWithValue("iid", suratMasuk.ID);
                cmd.Parameters.AddWithValue("inomor_agenda", "");
                cmd.Parameters.AddWithValue("inomor_surat", suratMasuk.NoSurat ?? "");
                cmd.Parameters.AddWithValue("iasal_surat", suratMasuk.AsalSurat ?? "");                
                cmd.Parameters.AddWithValue("itanggal_surat", Convert.ToDateTime(suratMasuk.TanggalSurat).ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("iperihal", suratMasuk.perihal ?? "");
                cmd.Parameters.AddWithValue("iLampiranSurat1", suratMasuk.LampiranSurat1 ?? "");
                cmd.Parameters.AddWithValue("iLampiranSurat2", suratMasuk.LampiranSurat2 ?? "");
                cmd.Parameters.AddWithValue("iLampiranSurat3", suratMasuk.LampiranSurat3 ?? "");
                cmd.Parameters.AddWithValue("iLampiranSurat4", suratMasuk.LampiranSurat4 ?? "");
                cmd.Parameters.AddWithValue("iLampiranSurat5", suratMasuk.LampiranSurat5 ?? "");
                cmd.Parameters.AddWithValue("iLampiranSurat6", suratMasuk.LampiranSurat6 ?? "");
                cmd.Parameters.AddWithValue("iLampiranLHP", suratMasuk.LampiranLHP ?? "");
                cmd.Parameters.AddWithValue("iLampiranSPTJM", suratMasuk.LampiranSPTJM ?? "");
                cmd.Parameters.AddWithValue("iSatuanKerja", suratMasuk.SATUAN_KERJA ?? "");
                cmd.Parameters.AddWithValue("iupdate_user", suratMasuk.UpdatUser ?? "");
                cmd.Parameters.AddWithValue("iLampiran_LHA", suratMasuk.LampiranSurat_LHA ?? "");
                cmd.Parameters.AddWithValue("iUnit_Kerja", suratMasuk.Kode_Unit_Kerja ?? "");
                con.Open();
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }
        public int Update(clsSuratMasuk suratMasuk)
        {
            int i;
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("SIMHUKDIS.sp_SuratMasuk_Upd", con);
                cmd.CommandType = CommandType.StoredProcedure;

                //DateTime dy = DateTime.ParseExact(suratMasuk.TanggalSurat,"dd-MM-yyyy",
                //                       System.Globalization.CultureInfo.InvariantCulture);

                cmd.Parameters.AddWithValue("iid", suratMasuk.ID);
                cmd.Parameters.AddWithValue("inomor_agenda", "");
                cmd.Parameters.AddWithValue("inomor_surat", suratMasuk.NoSurat);
                cmd.Parameters.AddWithValue("iasal_surat", suratMasuk.AsalSurat);
                cmd.Parameters.AddWithValue("itanggal_surat", Convert.ToDateTime(suratMasuk.TanggalSurat).ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("iperihal", suratMasuk.perihal);
                cmd.Parameters.AddWithValue("iLampiranSurat1", suratMasuk.LampiranSurat1);
                cmd.Parameters.AddWithValue("iLampiranSurat2", suratMasuk.LampiranSurat2);
                cmd.Parameters.AddWithValue("iLampiranSurat3", suratMasuk.LampiranSurat3);
                cmd.Parameters.AddWithValue("iLampiranSurat4", suratMasuk.LampiranSurat4);
                cmd.Parameters.AddWithValue("iLampiranSurat5", suratMasuk.LampiranSurat5);
                cmd.Parameters.AddWithValue("iLampiranSurat6", suratMasuk.LampiranSurat6);
                cmd.Parameters.AddWithValue("iLampiranLHP", suratMasuk.LampiranLHP);
                cmd.Parameters.AddWithValue("iLampiranSPTJM", suratMasuk.LampiranSPTJM);
                cmd.Parameters.AddWithValue("iSatuanKerja", suratMasuk.SATUAN_KERJA);
                cmd.Parameters.AddWithValue("iupdate_user", suratMasuk.UpdatUser);
                cmd.Parameters.AddWithValue("iLampiran_LHA", suratMasuk.LampiranSurat_LHA);
                cmd.Parameters.AddWithValue("iUnit_Kerja", suratMasuk.Kode_Unit_Kerja ?? "");
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
                SqlCommand cmd = new SqlCommand("SIMHUKDIS.sp_SuratMasuk_Del", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("iid", ID);
                con.Open();
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }
        public int Proses(clsSuratMasuk suratMasuk)
        {
            int i;
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("SIMHUKDIS.sp_SuratMasuk_Proses", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("iid", suratMasuk.ID);
                cmd.Parameters.AddWithValue("iupdate_user", suratMasuk.UpdatUser);
                con.Open();
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }
        public List<clsSuratMasuk> GetListSatker()
        {
            clsSuratMasukDB db = new clsSuratMasukDB();
            List<clsSuratMasuk> User = new List<clsSuratMasuk>();
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("SIMHUKDIS.sp_Satker_Sel", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    clsSuratMasuk s = new clsSuratMasuk();
                    s.SATUAN_KERJA = rd["SATUAN_KERJA"].ToString();
                    s.KODE_SATUAN_KERJA = rd["KODE_SATUAN_KERJA"].ToString();
                    User.Add(s);
                }
                rd.Close();
            }
            return User;
        }
        public List<clsUnitKerja> GetUnit(string KodeSatker)
        {
            clsSuratMasukDB db = new clsSuratMasukDB();
            List<clsUnitKerja> User = new List<clsUnitKerja>();
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("SIMHUKDIS.sp_UnitKerja_Sel", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("iKodeSatker", KodeSatker);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    clsUnitKerja s = new clsUnitKerja();
                    s.Unit_Kerja = rd["Unit_Kerja"].ToString();
                    s.Kode_Unit_Kerja = rd["Kode_Unit_Kerja"].ToString();
                    User.Add(s);
                }
                rd.Close();
            }
            return User;
        }
        public clsUnitKerja GetListUnitker(string KodeSatker)
        {
            clsUnitKerja s = new clsUnitKerja();
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("SIMHUKDIS.sp_UnitKerja_Sel", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("iKodeSatker", KodeSatker);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    s.Unit_Kerja = rd["Unit_Kerja"].ToString();
                    s.Kode_Unit_Kerja = rd["Kode_Unit_Kerja"].ToString();
                }
                rd.Close();
            }
            return s;
        }
    }
}