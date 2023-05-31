using SIMHUKDIS.Models;
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
    public class clsTelaah
    {
        public Int32 ID { get; set; }
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
        public string PelanggaranDisiplin { get; set; }
        public string PasalPelanggaran { get; set; }
        public string RekomendasiHukdis { get; set; }
        public string AnalisaPertimbangan { get; set; }
        public string KeputusanSidangDPK { get; set; }
        public string FileTelaah { get; set; }
        public string CreatedUser { get; set; }
        public string Status { get; set; }
        public string StatusProses { get; set; }
        public string TelaahNo { get; set; }
        public string Tanggal_Telaah { get; set; }
        public string Karopeg { get; set; }
        public string NIP_Karopeg { get; set; }
        public string Konseptor { get; set; }
        public string Proses { get; set; }
        public string RejectStatus { get; set; }
        public string RejectReason { get; set; }
    }
    public class clsTelaahVerifikasi
    {
        public int ID { get; set; }
        public string UpdateDate { get; set; }
        public string UpdateUser { get; set; }
        public string Catatan { get; set; }
        public string JenisVerifikasi { get; set; }
    }
    public class suratmasuk
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
        [DisplayName("Lampiran LHA")]
        public string LampiranSurat_LHA { get; set; }

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
        [DisplayName("Catata Koordinator Sub Bagian")]
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

        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Tanggal Disposisi Kepala Biro")]
        public string DisposisiDate1 { get; set; }
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Tanggal Disposisi Koordinator Bagian")]
        public string DisposisiDate2 { get; set; }
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Tanggal Disposisi Koordinator Sub Bagian")]
        public string DisposisiDate3 { get; set; }
        public int Status { get; set; }
        public string RejectStatus { get; set; }
        public string RejectReason { get; set; }
        public string Tipe { get; set; }
    }
    public class clsDocument
    {
        public string DocWord { get; set; }
        public string DocPDF { get; set; }
        public string Msg { get; set; }
    }
    public class clsTelaahDB
    {
        public List<clsSuratMasuk> TelaahList(string UserID)
        {
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            List<clsSuratMasuk> SuratMasuks = new List<clsSuratMasuk>();
            using (SqlConnection con = new SqlConnection(constr))
            {
                Int32 pNom = 0;
                string q = "SIMHUKDIS.sp_Telaah_Create";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("iUserID", UserID);
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

                    data.RejectStatus = rd["RejectStatus"].ToString();
                    data.RejectReason = rd["RejectReason"].ToString();
                    SuratMasuks.Add(data);
                }
                return SuratMasuks;
            }
        }

        public List<clsTelaah> ListAll(int ID)
        {
            List<clsTelaah> lst = new List<clsTelaah>();
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SIMHUKDIS.sp_Telaah_Sel", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("iID", ID);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    clsTelaah data = new clsTelaah();
                    data.ID = Convert.ToInt32(rd["id"].ToString());
                    data.NIP = rd["NIP"].ToString();
                    data.NAMA_LENGKAP = rd["Fullname"].ToString();
                    data.DasarBukti = rd["Dasar_dan_Bukti_Penunjang"].ToString();
                    data.PelanggaranDisiplin = rd["Pelanggaran"].ToString();
                    data.PasalPelanggaran = rd["Pasal_Pelanggaran"].ToString();
                    data.RekomendasiHukdis = rd["Rekomendasi_Hukuman"].ToString();
                    data.AnalisaPertimbangan = rd["Analisa_dan_Pertimbangan"].ToString();
                    data.KeputusanSidangDPK = rd["Keputusan_Sidang_DPK"].ToString();
                    data.FileTelaah = rd["FileTelaah"].ToString();
                    data.SATUAN_KERJA = rd["Satker"].ToString();
                    data.Status = "Telaah";
                    data.StatusProses = rd["StatusProses"].ToString();
                    lst.Add(data);
                }
            }
            return lst;
        }
        public List<clsTelaah> ListAtasan()
        {
            List<clsTelaah> lst = new List<clsTelaah>();
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SIMHUKDIS.sp_Telaah_Atasan_Sel", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    clsTelaah data = new clsTelaah();
                    data.ID = Convert.ToInt32(rd["id"].ToString());
                    data.NIP = rd["NIP"].ToString();
                    data.NAMA_LENGKAP = rd["Fullname"].ToString();
                    data.DasarBukti = rd["Dasar_dan_Bukti_Penunjang"].ToString();
                    data.PelanggaranDisiplin = rd["Pelanggaran"].ToString();
                    data.PasalPelanggaran = rd["Pasal_Pelanggaran"].ToString();
                    data.RekomendasiHukdis = rd["Rekomendasi_Hukuman"].ToString();
                    data.AnalisaPertimbangan = rd["Analisa_dan_Pertimbangan"].ToString();
                    data.KeputusanSidangDPK = rd["Keputusan_Sidang_DPK"].ToString();
                    data.FileTelaah = rd["FileTelaah"].ToString();
                    data.SATUAN_KERJA = rd["Satker"].ToString();
                    data.Status = "Telaah";
                    data.StatusProses = rd["StatusProses"].ToString();
                    data.Konseptor = rd["Konseptor"].ToString();
                    lst.Add(data);
                }
            }
            return lst;
        }
        public List<clsTelaah> ListByNIP(int ID, string NIP)
        {
            List<clsTelaah> lst = new List<clsTelaah>();
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SIMHUKDIS.sp_Telaah_Sel_ByIDNIP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("iID", ID);
                cmd.Parameters.AddWithValue("iNIP", NIP);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    clsTelaah data = new clsTelaah();
                    data.ID = Convert.ToInt32(rd["id"].ToString());
                    data.NIP = rd["NIP"].ToString();
                    data.NAMA_LENGKAP = rd["Fullname"].ToString();
                    data.DasarBukti = rd["Dasar_dan_Bukti_Penunjang"].ToString();
                    data.PelanggaranDisiplin = rd["Pelanggaran"].ToString();
                    data.PasalPelanggaran = rd["Pasal_Pelanggaran"].ToString();
                    data.RekomendasiHukdis = rd["Rekomendasi_Hukuman"].ToString();
                    data.AnalisaPertimbangan = rd["Analisa_dan_Pertimbangan"].ToString();
                    data.KeputusanSidangDPK = rd["Keputusan_Sidang_DPK"].ToString();
                    data.FileTelaah = rd["FileTelaah"].ToString();
                    data.SATUAN_KERJA = rd["Satker"].ToString();
                    data.TelaahNo = rd["TelaahNo"].ToString();
                    data.Tanggal_Telaah = rd["Tanggal_Telaah"].ToString();
                    data.Status = "Telaah";
                    lst.Add(data);
                }
            }
            return lst;
        }
        public int Insert(clsTelaah t)
        {
            int i;
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("sp_Telaah_Ins", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("iID", t.ID);
                cmd.Parameters.AddWithValue("iNIP", t.NIP);
                cmd.Parameters.AddWithValue("iDasar_dan_Bukti_Penunjang", t.DasarBukti);
                cmd.Parameters.AddWithValue("iPelanggaran", t.PelanggaranDisiplin);
                cmd.Parameters.AddWithValue("iPasal_Pelanggaran", t.PasalPelanggaran);
                cmd.Parameters.AddWithValue("iRekomendasi_Hukuman", t.RekomendasiHukdis);
                cmd.Parameters.AddWithValue("iAnalisa_dan_Pertimbangan", t.AnalisaPertimbangan);
                cmd.Parameters.AddWithValue("iKeputusan_Sidang_DPK", t.KeputusanSidangDPK);
                cmd.Parameters.AddWithValue("iCreated_User", t.CreatedUser);
                cmd.Parameters.AddWithValue("iFullname", t.NAMA_LENGKAP);
                cmd.Parameters.AddWithValue("iTelaahNo", t.TelaahNo);

                DateTime dParse = DateTime.ParseExact(t.Tanggal_Telaah, "dd MMMM yyyy", null); //Convert.ToDateTime(Data.EstimateDate); 
                t.Tanggal_Telaah = dParse.ToString();

                cmd.Parameters.AddWithValue("iTanggalTelaah", t.Tanggal_Telaah);

                con.Open();
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }
        public int InsertTelaahNo(int No, int Bulan, int Tahun)
        {
            int i;
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("SIMHUKDIS.sp_TelaahNo_Insert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("iTelaahNo", No);
                cmd.Parameters.AddWithValue("iBulan", Bulan);
                cmd.Parameters.AddWithValue("iTahun", Tahun);
                con.Open();
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }
        public int Update(clsTelaah t)
        {
            int i;
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("SIMHUKDIS.sp_Telaah_Upd", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("iID", t.ID);
                cmd.Parameters.AddWithValue("iNIP", t.NIP);
                cmd.Parameters.AddWithValue("iDasar_dan_Bukti_Penunjang", t.DasarBukti);
                cmd.Parameters.AddWithValue("iPelanggaran", t.PelanggaranDisiplin);
                cmd.Parameters.AddWithValue("iPasal_Pelanggaran", t.PasalPelanggaran);
                cmd.Parameters.AddWithValue("iRekomendasi_Hukuman", t.RekomendasiHukdis);
                cmd.Parameters.AddWithValue("iAnalisa_dan_Pertimbangan", t.AnalisaPertimbangan);
                cmd.Parameters.AddWithValue("iKeputusan_Sidang_DPK", t.KeputusanSidangDPK);
                cmd.Parameters.AddWithValue("iCreated_User", t.CreatedUser);
                cmd.Parameters.AddWithValue("iFileTelaah", t.FileTelaah);
                cmd.Parameters.AddWithValue("iFullname", t.NAMA_LENGKAP);
                cmd.Parameters.AddWithValue("iTelaahNo", t.TelaahNo);
                DateTime dParse = DateTime.ParseExact(t.Tanggal_Telaah, "dd-MM-yyyy", null); //Convert.ToDateTime(Data.EstimateDate); 
                t.Tanggal_Telaah = dParse.ToString();
                cmd.Parameters.AddWithValue("iTanggalTelaah", t.Tanggal_Telaah);
                //cmd.Parameters.AddWithValue("iTanggalTelaah", Convert.ToDateTime(t.Tanggal_Telaah).ToString("yyyy-MM-dd"));
                con.Open();
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }
        public int UpdateStatus(int ID, string NIP,string CreatedUser)
        {
            int i;
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("SIMHUKDIS.sp_Telaah_UpdStatus", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("iID", ID);
                cmd.Parameters.AddWithValue("iNIP", NIP);
                cmd.Parameters.AddWithValue("iCreated_User", CreatedUser);
                con.Open();
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }
        public int UpdateTelaahStatus(clsTelaah t)
        {
            int i;
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("SIMHUKDIS.sp_Telaah_UpdTelaahStatus", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("iID", t.ID);
                cmd.Parameters.AddWithValue("iNIP", t.NIP);
                cmd.Parameters.AddWithValue("iCreated_User", t.CreatedUser);
                cmd.Parameters.AddWithValue("iFileTelaah", t.FileTelaah);
                cmd.Parameters.AddWithValue("Proses", t.Proses);
                con.Open();
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }
        public int Delete(int ID, string NIP)
        {
            try
            {
                int i = 0;
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string MySql = "SIMHUKDIS.sp_Telaah_Del";
                    SqlCommand cmd = new SqlCommand(MySql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("iid", ID);
                    cmd.Parameters.AddWithValue("iNIP", NIP);
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
        public suratmasuk GetList(int ID)
        {
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            suratmasuk data = new suratmasuk();
            using (SqlConnection con = new SqlConnection(constr))
            {
                Int32 pNom = 0;
                string q = "SIMHUKDIS.sp_SuratMasuk_Sel";
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
                    DateTime TanggalSurat = DateTime.ParseExact(data.TanggalSurat, "dd-MM-yyyy", null);

                    data.TanggalSurat = TanggalSurat.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"));
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
                    data.RejectStatus = rd["RejectStatus"].ToString();
                    data.RejectReason = rd["RejectReason"].ToString();
                    data.Tipe = rd["TIPE"].ToString();
                }
                return data;
            }
        }
        public List<clsSuratMasuk> SuratMasuks
        {
            get
            {

                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                List<clsSuratMasuk> SuratMasuks = new List<clsSuratMasuk>();
                using (SqlConnection con = new SqlConnection(constr))
                {
                    Int32 pNom = 0;
                    string q = "SIMHUKDIS.sp_Telaah_SM_Sel";
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
                        DateTime TanggalSurat = DateTime.ParseExact(rd["tanggal_surat"].ToString(), "dd-MM-yyyy", null);

                        data.TanggalSurat = TanggalSurat.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"));
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
                        data.RejectStatus = rd["RejectStatus"].ToString();
                        data.RejectReason = rd["RejectReason"].ToString();
                        SuratMasuks.Add(data);
                    }
                    return SuratMasuks;
                }
            }
        }
        public List<clsSuratMasuk> GetSuratMasukByID(int ID)
        {
            try
            {
                List<clsSuratMasuk> sm = new List<clsSuratMasuk>();
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string q = "SIMHUKDIS.sp_SuratMasuk_ByID_Sel";
                    SqlCommand cmd = new SqlCommand(q, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("iid", ID);
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        clsSuratMasuk data = new clsSuratMasuk();
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
                        data.RejectStatus = rd["RejectStatus"].ToString();
                        data.RejectReason = rd["RejectReason"].ToString();
                        sm.Add(data);
                    }
                    rd.Close();
                }
                return sm;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
        public int GetLastTelaahNo (int Bulan, int Tahun)
        {
            int TelaahNo = 0;
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("SIMHUKDIS.sp_TelaahNo_GetNo", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("iBulan", Bulan);
                cmd.Parameters.AddWithValue("iTahun", Tahun);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                int i = 1;
                
                while (rd.Read())
                {
                    if (i == 1)
                    {
                        TelaahNo = Convert.ToInt32(rd["TelaahNo"].ToString());
                    }
                    i = i + 1;
                }
                rd.Close();
            }
            return TelaahNo;
        }
        public int Verifikasi(clsTelaahVerifikasi t)
        {
            int i;
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("sp_Telaah_Verifikasi", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", t.ID);
                cmd.Parameters.AddWithValue("@Reject", t.JenisVerifikasi);
                cmd.Parameters.AddWithValue("@RejectReason", t.Catatan);
                cmd.Parameters.AddWithValue("@UpdateUser", t.UpdateUser);
                con.Open();
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }
    }
}