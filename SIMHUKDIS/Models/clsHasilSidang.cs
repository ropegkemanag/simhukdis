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
    public class clsHasilSidang
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
        public string TMTDate { get; set; }
        public string TEMBUSAN1 { get; set; }
        public string TASPEN { get; set; }
        public string KPPN { get; set; }
        public string Kode_Satker1 { get; set; }
    }
    public class HasilSidangDtl
    {
        [Key]
        public string ID { get; set; }
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
        public string TMTDate { get; set; }
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

        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        [DisplayName("Tanggal Disposisi Kepala Biro")]
        public string DisposisiDate1 { get; set; }
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        [DisplayName("Tanggal Disposisi Koordinator Bagian")]
        public string DisposisiDate2 { get; set; }
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        [DisplayName("Tanggal Disposisi Koordinator Sub Bagian")]
        public string DisposisiDate3 { get; set; }
        public int Status { get; set; }
        public string FullName { get; set; }
        public string NIP { get; set; }
        public string GOL_RUANG { get; set; }
        public string LEVEL_JABATAN { get; set; }
        public string DasarBukti { get; set; }
        public string PelanggaranDisiplin { get; set; }
        public string PasalPelanggaran { get; set; }
        public string RekomendasiHukdis { get; set; }
        public string AnalisaPertimbangan { get; set; }
        public string UnitKerja { get; set; }
        public string Catatan_Pra_DPK { get; set; }
        public string UserLogin { get; set; }
        public string Tanggal_Sidang_Pra_DPK { get; set; }
        public string Tanggal_Sidang_DPK { get; set; }
        public string KeputusanSidang { get; set; }
        public string Catatan_Sidang { get; set; }
        public string hukuman { get; set; }
        public string Keputusan_Menteri { get; set; }
        public string Tanggal_Telaah { get; set; }
        public string Mengingat { get; set; }
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
    public class clsKirimSurat
    {
        public string ID { get; set; }
        public string NIP { get; set; }
        public string FileSKS { get; set; }
        public string NoSurat { get; set; }
        public string TanggalSurat { get; set; }
        public string UserID { get; set; }
        public string Tipe { get; set; }
    }
    public class clsHasilSidangDB
    {
        public List<clsHasilSidang> ListAll(string UserID)
        {
            int a = 0;
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            List<clsHasilSidang> DP = new List<clsHasilSidang>();
            using (SqlConnection con = new SqlConnection(constr))
            {
                string q = "SIMHUKDIS.sp_HasilSidang_Sel";
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
        
        public string GetPasalPelanggaran()
        {
            int a = 0;
            string pasalpelanggaran = "";
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string q = "SIMHUKDIS.sp_PasalPelanggaran_Sel";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    if (pasalpelanggaran == "")
                    {
                        pasalpelanggaran = rd["Pasal_Pelanggaran"].ToString();
                    }
                    else
                    {
                        pasalpelanggaran = pasalpelanggaran + "\n" + rd["Pasal_Pelanggaran"].ToString();
                    }
                    
                }
                return pasalpelanggaran;
            }
        }
        public HasilSidangDtl GetListCreate(int ID, string NIP)
        {
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            HasilSidangDtl data = new HasilSidangDtl();
            using (SqlConnection con = new SqlConnection(constr))
            {
                Int32 pNom = 0;
                string k = "";
                string q = "SIMHUKDIS.sp_HasilSidang_Create";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("iid", ID);
                cmd.Parameters.AddWithValue("iNIP", NIP);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    pNom = pNom + 1;
                    data.No = pNom;
                    data.ID = rd["id"].ToString();
                    data.NoAgenda = rd["nomor_agenda"].ToString();
                    data.NoSurat = rd["nomor_surat"].ToString();
                    data.AsalSurat = rd["asal_surat"].ToString();
                    data.TanggalSurat = rd["tanggal_surat"].ToString();
                    
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
                    data.DisposisiDate3 = rd["Disposisi3_Date"].ToString();
                    data.NIP = rd["NIP"].ToString();
                    data.Tanggal_Sidang_DPK = rd["tanggal_sidang_dpk"].ToString();
                    data.Tanggal_Telaah = rd["Tanggal_Telaah"].ToString();
                    data.KeputusanSidang = rd["KeputusanSidang"].ToString();
                    data.Catatan_Sidang = rd["Catatan_Sidang"].ToString();
                    string DasarBukti = rd["Dasar_dan_Bukti_Penunjang"].ToString();
                    DasarBukti = DasarBukti.Replace("\n"," ");
                    data.DasarBukti = DasarBukti;
                    data.PelanggaranDisiplin = rd["Pelanggaran"].ToString();
                    data.PasalPelanggaran = rd["Pasal_Pelanggaran"].ToString();
                    data.RekomendasiHukdis = rd["Rekomendasi_Hukuman"].ToString();
                    data.AnalisaPertimbangan = rd["Analisa_dan_Pertimbangan"].ToString();
                    data.Mengingat = rd["Mengingat"].ToString();
                    data.Kode_Unit_Kerja = rd["Kode_Unit_Kerja"].ToString();
                    data.Unit_Kerja = rd["Unit_Kerja"].ToString();
                    data.Jabatan_Konseptor = rd["Jabatan_Konseptor"].ToString();
                    data.Tembusan = rd["Tembusan"].ToString();
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


                    clsPegawaiDB x = new clsPegawaiDB();
                    List<clsDataPegawaiDtl> y = new List<clsDataPegawaiDtl>();
                    y = x.ListPegawai(data.NIP);
                    int z = 1;
                    if (y != null)
                    {
                        foreach (var item in y)
                        {
                            if (z == 1)
                            {
                                data.FullName = item.NAMA_LENGKAP;
                                data.GOL_RUANG = item.PANGKAT;
                                data.LEVEL_JABATAN = item.TAMPIL_JABATAN;
                                data.SATUAN_KERJA = item.KETERANGAN_SATUAN_KERJA;
                                data.UnitKerja = item.KETERANGAN_SATUAN_KERJA;
                                data.TEMPAT_LAHIR = item.TEMPAT_LAHIR;
                                data.TANGGAL_LAHIR = item.TANGGAL_LAHIR;
                                data.MK_TAHUN = item.MK_TAHUN;
                                data.TMT_PENSIUN = item.TMT_PENSIUN;
                                data.SATKER_1 = item.SATKER_1;
                                data.SATKER_2 = item.SATKER_2;
                                data.SATKER_3 = item.SATKER_3;
                                data.SATKER_4 = item.SATKER_4;
                                data.SATKER_5 = item.SATKER_5;
                                k = item.SATKER_3;
                            }
                            z = z + 1;
                        }
                    }                    
                    clsHasilSidangDB p = new clsHasilSidangDB();
                    //if (data.Tembusan == "" || data.Tembusan == null)
                    //{
                    //    data.Tembusan = p.GetTembusan(data.KODE_SATUAN_KERJA, data.Kode_Unit_Kerja);
                    //}                    
                }
                return data;
            }
        }
        public clsHasilSidang DBList(int ID, string NIP)
        {
            int a = 1;
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            clsHasilSidang DP = new clsHasilSidang();
            using (SqlConnection con = new SqlConnection(constr))
            {
                string q = "SP_SK_TanpaSidangDPK_SEL";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@NIP", NIP);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    if(a==1)
                    {
                        DP.No = a;
                        DP.ID = rd["id"].ToString();
                        DP.AsalSurat = rd["asal_surat"].ToString();
                        DP.perihal = rd["perihal"].ToString();
                        DP.NIP = rd["NIP"].ToString();
                        DP.DasarBukti = rd["Mengingat"].ToString();
                        DP.KeputusanSidang = rd["KeputusanSidang"].ToString();
                        DP.menag = rd["Menag"].ToString();
                        DP.sekjend = rd["Sekjend"].ToString();
                        DP.karopeg = rd["karopeg"].ToString();
                        DP.nip_karopeg = rd["nip_karopeg"].ToString();
                        DP.koor = rd["koor"].ToString();
                        DP.nip_koor = rd["nip_koor"].ToString();
                        DP.subkoor = rd["subkoor"].ToString();
                        DP.nip_subkoor = rd["nip_subkoor"].ToString();
                        DP.konseptor = rd["konseptor"].ToString();
                        DP.nip_konseptor = rd["nip_konseptor"].ToString();
                        DP.Kode_Unit_Kerja = rd["Kode_Unit_Kerja"].ToString();
                        DP.Unit_Kerja = rd["Unit_Kerja"].ToString();
                        DP.TMTDate = rd["TMTDate"].ToString();
                        DP.Tembusan = rd["tembusan"].ToString();
                        clsPegawaiDB x = new clsPegawaiDB();
                        List<clsDataPegawaiDtl> y = new List<clsDataPegawaiDtl>();
                        y = x.ListPegawai(NIP);
                        int z = 1;
                        if (y != null)
                        {
                            foreach (var item in y)
                            {
                                if (z == 1)
                                {
                                    DP.FullName = item.NAMA_LENGKAP;
                                    DP.GOL_RUANG = item.GOL_RUANG;
                                    DP.LEVEL_JABATAN = item.TAMPIL_JABATAN;
                                    DP.UnitKerja = item.KETERANGAN_SATUAN_KERJA;
                                    DP.TEMPAT_LAHIR = item.TEMPAT_LAHIR;
                                    DP.TANGGAL_LAHIR = item.TANGGAL_LAHIR;
                                    DP.MK_TAHUN = item.MK_TAHUN;
                                    DP.TMT_PENSIUN = item.TMT_PENSIUN;
                                    DP.SATKER_1 = item.SATKER_1;
                                    DP.SATKER_2 = item.SATKER_2;
                                    DP.SATKER_3 = item.SATKER_3;
                                    DP.SATKER_4 = item.SATKER_4;
                                    DP.SATKER_5 = item.SATKER_5;
                                    DP.TEMBUSAN1 = item.TEMBUSAN1;
                                    DP.KPPN = item.KPPN;
                                    DP.TASPEN = item.TASPEN;
                                }
                                z = z + 1;
                            }
                        }

                        z = 1;
                        y = x.ListPegawai(DP.nip_subkoor);
                        foreach (var item in y)
                        {
                            DP.Jabatan_subkoor = item.TAMPIL_JABATAN;
                        }
                        y = x.ListPegawai(DP.nip_koor);
                        foreach (var item in y)
                        {
                            DP.Jabatan_koor = item.TAMPIL_JABATAN;
                        }
                        y = x.ListPegawai(DP.nip_konseptor);
                        foreach (var item in y)
                        {
                            DP.Jabatan_Konseptor = item.TAMPIL_JABATAN;
                        }
                        //clsHasilSidangDB p = new clsHasilSidangDB();
                        //DP.Tembusan = p.GetTembusan(DP.SATUAN_KERJA, k);
                    }
                }
                return DP;
            }
        }
        public clsHasilSidang ListAll(int ID, string NIP)
        {
            int a = 1;
            string k = "";
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            clsHasilSidang DP = new clsHasilSidang();
            using (SqlConnection con = new SqlConnection(constr))
            {
                string q = "SIMHUKDIS.sp_HasilSidang_Sel_ByID";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IID", ID);
                cmd.Parameters.AddWithValue("INIP", NIP);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    if (a == 1 )
                    {
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
                        DP.Catatan_Pra_DPK = rd["Catatan"].ToString();
                        DP.Tanggal_Sidang_DPK = rd["tanggal_sidang_dpk"].ToString();
                        DP.Tanggal_Sidang_Pra_DPK = rd["tanggal_sidang_pra_dpk"].ToString();
                        DP.KeputusanSidang = rd["KeputusanSidang"].ToString();
                        DP.hukuman = rd["hukuman"].ToString();
                        DP.Catatan_Sidang = rd["Catatan_Sidang"].ToString();
                        DP.menag = rd["Menag"].ToString();
                        DP.sekjend = rd["Sekjend"].ToString();
                        DP.karopeg = rd["karopeg"].ToString();
                        DP.nip_karopeg = rd["nip_karopeg"].ToString();
                        DP.koor = rd["koor"].ToString();
                        DP.nip_koor = rd["nip_koor"].ToString();
                        DP.subkoor = rd["subkoor"].ToString();
                        DP.nip_subkoor = rd["nip_subkoor"].ToString();
                        DP.konseptor = rd["konseptor"].ToString();
                        DP.nip_konseptor = rd["nip_konseptor"].ToString();
                        DP.Mengingat = rd["Mengingat"].ToString();
                        DP.Kode_Unit_Kerja = rd["Kode_Unit_Kerja"].ToString();
                        DP.Unit_Kerja = rd["Unit_Kerja"].ToString();
                        DP.Tembusan = rd["Tembusan"].ToString();
                        DP.Jabatan_Konseptor = rd["Jabatan_Konseptor"].ToString();
                        DP.Jabatan_koor = rd["Jabatan_Koor"].ToString();
                        DP.Jabatan_subkoor = rd["Jabatan_Subkoor"].ToString();
                        DP.NO_SK = rd["NO_SK"].ToString();
                        DP.Tanggal_SK = rd["Tanggal_SK"].ToString();
                        DP.FILE_SK = rd["FILE_SK"].ToString();
                        DP.Keterangan_SK = rd["Keterangan_SK"].ToString();
                        DP.Tanggal_Penyampaian_Ke_Satker = rd["Tanggal_Penyampaian_Ke_Satker"].ToString();
                        DP.Penerima_Satker = rd["Penerima_Satker"].ToString();
                        DP.BAP_Satker = rd["BAP_Satker"].ToString();
                        DP.Keterangan_Satker = rd["Keterangan_Satker"].ToString();
                        DP.Tanggal_Penyampaian_Ke_YBS = rd["Tanggal_Penyampaian_Ke_YBS"].ToString();
                        DP.Penerima_YBS = rd["Penerima_YBS"].ToString();
                        DP.BAP_Penerima = rd["BAP_Penerima"].ToString();
                        DP.Keterangan_Penerima = rd["Keterangan_Penerima"].ToString();
                        clsPegawaiDB x = new clsPegawaiDB();
                        List<clsDataPegawaiDtl> y = new List<clsDataPegawaiDtl>();
                        y = x.ListPegawai(NIP);
                        int z = 1;
                        foreach (var item in y)
                        {
                            if (z == 1)
                            {
                                DP.FullName = item.NAMA_LENGKAP;
                                DP.GOL_RUANG = item.GOL_RUANG;
                                DP.LEVEL_JABATAN = item.TAMPIL_JABATAN + " " + item.SATKER_1;
                                DP.UnitKerja = item.SATKER_4;
                                DP.TEMPAT_LAHIR = item.TEMPAT_LAHIR;
                                DP.TANGGAL_LAHIR = item.TANGGAL_LAHIR;
                                DP.MK_TAHUN = item.MK_TAHUN;
                                DP.TMT_PENSIUN = item.TMT_PENSIUN;
                                DP.SATKER_1 = item.SATKER_1;
                                DP.SATKER_2 = item.SATKER_2;
                                DP.SATKER_3 = item.SATKER_3;
                                DP.SATKER_4 = item.SATKER_4;
                                DP.SATKER_5 = item.SATKER_5;
                                DP.TEMBUSAN1 = item.TEMBUSAN1;
                                DP.KPPN = item.KPPN;
                                DP.TASPEN = item.TASPEN;
                                DP.Kode_Satker1 = item.KODE_SATUAN_KERJA;
                            }
                            z = z + 1;
                        }
                        y = x.ListPegawai(DP.nip_subkoor);
                        //y = x.GetPegawai(DP.nip_subkoor);
                        foreach (var item in y)
                        {
                            if (z == 1)
                            {
                                DP.Jabatan_subkoor = item.TAMPIL_JABATAN;
                            }
                        }
                        y = x.ListPegawai(DP.nip_koor);
                        foreach (var item in y)
                        {
                            if (z == 1)
                            {
                                DP.Jabatan_koor = item.TAMPIL_JABATAN;
                            }
                        }
                        y = x.ListPegawai(DP.nip_konseptor);
                        foreach (var item in y)
                        {
                            if (z == 1)
                            {
                                DP.Jabatan_Konseptor = item.TAMPIL_JABATAN;
                            }
                        }
                        //clsHasilSidangDB p = new clsHasilSidangDB();
                        //DP.Tembusan = p.GetTembusan(DP.SATUAN_KERJA, k);
                    }
                }
                return DP;
            }
        }
        public List<clsKirimSurat> GetSuratNo(String UserID)
        {
            List<clsKirimSurat> SuratNo = new List<clsKirimSurat>();
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("sp_GetNoSurat_Sel", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", UserID);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    clsKirimSurat s = new clsKirimSurat();
                    s.ID = rd["ID"].ToString();
                    s.NoSurat = rd["NoSurat"].ToString();
                    SuratNo.Add(s);
                }
                rd.Close();
            }
            return SuratNo;
        }
        public string GetTembusan(string KodeSatker, string Satker3)
        {
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            HasilSidangDtl data = new HasilSidangDtl();
            using (SqlConnection con = new SqlConnection(constr))
            {
                Int32 pNom = 1;
                string a,b,c,d,e,f,g,h,i,j = "";
                string Tembusan = "";
                string q = "SIMHUKDIS.sp_Tembusan_sel";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("iKODE_SATUAN_KERJA", KodeSatker);
                cmd.Parameters.AddWithValue("iKode_Unit_Kerja", Satker3);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    a = rd["Tembusan_1"].ToString();
                    b = rd["Tembusan_2"].ToString();
                    c = rd["Tembusan_3"].ToString();
                    d = rd["Tembusan_4"].ToString();
                    e = rd["Tembusan_5"].ToString();
                    f = rd["Tembusan_6"].ToString();
                    g = rd["Tembusan_7"].ToString();
                    h = rd["Tembusan_8"].ToString();
                    i = rd["Tembusan_9"].ToString();
                    j = rd["Tembusan_10"].ToString();

                    if (a != null || a != "")
                    {
                        if (Tembusan == "")
                        {
                            Tembusan = a;
                        }
                        else
                        {
                            Tembusan = Tembusan + "\n" + a;
                        }
                    }
                    if (b != null || b != "")
                    {
                        if (Tembusan == "")
                        {
                            Tembusan = b;
                        }
                        else
                        {
                            Tembusan = Tembusan + "\n" + b;
                        }
                    }
                    if (c != null || c != "")
                    {
                        if (Tembusan == "")
                        {
                            Tembusan = c;
                        }
                        else
                        {
                            Tembusan = Tembusan + "\n" + c;
                        }
                    }
                    if (d != null || d != "")
                    {
                        if (Tembusan == "")
                        {
                            Tembusan = d;
                        }
                        else
                        {
                            Tembusan = Tembusan + "\n" + d;
                        }
                    }
                    if (e != null || e != "")
                    {
                        if (Tembusan == "")
                        {
                            Tembusan = e;
                        }
                        else
                        {
                            Tembusan = Tembusan + "\n" + e;
                        }
                    }
                    if (f != null || f != "")
                    {
                        if (Tembusan == "")
                        {
                            Tembusan = f;
                        }
                        else
                        {
                            Tembusan = Tembusan + "\n" + f;
                        }
                    }
                    if (g != null || g != "")
                    {
                        if (Tembusan == "")
                        {
                            Tembusan = g;
                        }
                        else
                        {
                            Tembusan = Tembusan + "\n" + g;
                        }
                    }
                    if (h != null || h != "")
                    {
                        if (Tembusan == "")
                        {
                            Tembusan = h;
                        }
                        else
                        {
                            Tembusan = Tembusan + "\n" + h;
                        }
                    }
                    if (i != null || i != "")
                    {
                        if (Tembusan == "")
                        {
                            Tembusan = i;
                        }
                        else
                        {
                            Tembusan = Tembusan + "\n" + i;
                        }
                    }
                    if (j != null || j != "")
                    {
                        if (Tembusan == "")
                        {
                            Tembusan = j;
                        }
                        else
                        {
                            Tembusan = Tembusan + "\n" + j;
                        }
                    }
                }
                return Tembusan;
            }
        }
        public int Insert(clsHasilSidang PD)
        {
            try
            {
                int i = 0;
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string MySql = "sp_DPK_Ins";
                    string TanggalSidang = Convert.ToDateTime(PD.Tanggal_Sidang_DPK).ToString("yyyy-MM-dd");
                    SqlCommand cmd = new SqlCommand(MySql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("iid", PD.ID);
                    cmd.Parameters.AddWithValue("iNIP", PD.NIP);
                    cmd.Parameters.AddWithValue("iCatatan_Sidang", PD.Catatan_Sidang ?? "");
                    cmd.Parameters.AddWithValue("iCreate_User", PD.UserLogin ?? "");
                    cmd.Parameters.AddWithValue("iTanggal_Sidang", TanggalSidang ?? "");
                    cmd.Parameters.AddWithValue("iKeputusanSidang", PD.KeputusanSidang ?? "");
                    cmd.Parameters.AddWithValue("iDasar_Bukti", PD.DasarBukti ?? "");
                    cmd.Parameters.AddWithValue("iPelanggaran", PD.PelanggaranDisiplin ?? "");
                    cmd.Parameters.AddWithValue("iPasal_Pelanggaran", PD.PasalPelanggaran ?? "");
                    cmd.Parameters.AddWithValue("iMengingat", PD.Mengingat ?? ""); 
                    cmd.Parameters.AddWithValue("iTembusan", PD.Tembusan ?? "");
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
        public int HasilSidang_Insert(HasilSidangDtl PD)
        {
            try
            {
                int i = 0;
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string MySql = "SIMHUKDIS.sp_HasilSidang_Ins";
                    string Tanggal_SK = Convert.ToDateTime(PD.Tanggal_SK).ToString("yyyy-MM-dd");
                    string Tanggal_Penyampaian_Ke_Satker = Convert.ToDateTime(PD.Tanggal_Penyampaian_Ke_Satker).ToString("yyyy-MM-dd");
                    string Tanggal_Penyampaian_Ke_YBS = Convert.ToDateTime(PD.Tanggal_Penyampaian_Ke_YBS).ToString("yyyy-MM-dd");
                    SqlCommand cmd = new SqlCommand(MySql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("IID", PD.ID);
                    cmd.Parameters.AddWithValue("iNIP", PD.NIP);
                    cmd.Parameters.AddWithValue("INO_SK", PD.NO_SK);
                    cmd.Parameters.AddWithValue("ITanggal_SK", Tanggal_SK);
                    cmd.Parameters.AddWithValue("IFILE_SK", PD.FILE_SK);
                    cmd.Parameters.AddWithValue("IKeterangan_SK", PD.Keterangan_SK);
                    cmd.Parameters.AddWithValue("ITanggal_Penyampaian_Ke_Satker", Tanggal_Penyampaian_Ke_Satker);
                    cmd.Parameters.AddWithValue("IPenerima_Satker", PD.Penerima_Satker);
                    cmd.Parameters.AddWithValue("IBAP_Satker", PD.BAP_Satker);
                    cmd.Parameters.AddWithValue("IKeterangan_Satker", PD.Keterangan_Satker);
                    cmd.Parameters.AddWithValue("ITanggal_Penyampaian_Ke_YBS", Tanggal_Penyampaian_Ke_YBS);
                    cmd.Parameters.AddWithValue("IPenerima_YBS", PD.Penerima_YBS);
                    cmd.Parameters.AddWithValue("IBAP_Penerima", PD.BAP_Penerima);
                    cmd.Parameters.AddWithValue("IKeterangan_Penerima", PD.Keterangan_Penerima);
                    cmd.Parameters.AddWithValue("ICreate_User", PD.CreateUser);
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

        public int KirimSKS(clsKirimSurat SKS)
        {
            try
            {
                int i = 0;
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string MySql = "SP_HASILSIDANG_INS";
                    SqlCommand cmd = new SqlCommand(MySql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", SKS.ID);
                    cmd.Parameters.AddWithValue("@NIP", SKS.NIP);
                    cmd.Parameters.AddWithValue("@NOSK", SKS.NoSurat);
                    cmd.Parameters.AddWithValue("@TANGGALSK", SKS.TanggalSurat);
                    cmd.Parameters.AddWithValue("@FILESK", SKS.FileSKS);
                    cmd.Parameters.AddWithValue("@Tipe", SKS.Tipe);
                    cmd.Parameters.AddWithValue("@USERID", SKS.UserID);
                    
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
        public int Ubah(HasilSidangDtl PD)
        {
            try
            {
                int i = 0;
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string MySql = "SIMHUKDIS.sp_HasilSidang_Upd";
                    string Tanggal_SK = Convert.ToDateTime(PD.Tanggal_SK).ToString("yyyy-MM-dd");
                    string Tanggal_Penyampaian_Ke_Satker = Convert.ToDateTime(PD.Tanggal_Penyampaian_Ke_Satker).ToString("yyyy-MM-dd");
                    string Tanggal_Penyampaian_Ke_YBS = Convert.ToDateTime(PD.Tanggal_Penyampaian_Ke_YBS).ToString("yyyy-MM-dd");
                    SqlCommand cmd = new SqlCommand(MySql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("IID", PD.ID);
                    cmd.Parameters.AddWithValue("iNIP", PD.NIP);
                    cmd.Parameters.AddWithValue("INO_SK", PD.NO_SK);
                    cmd.Parameters.AddWithValue("ITanggal_SK", Tanggal_SK);
                    cmd.Parameters.AddWithValue("IFILE_SK", PD.FILE_SK);
                    cmd.Parameters.AddWithValue("IKeterangan_SK", PD.KeputusanSidang);
                    cmd.Parameters.AddWithValue("ITanggal_Penyampaian_Ke_Satker", Tanggal_Penyampaian_Ke_Satker);
                    cmd.Parameters.AddWithValue("IPenerima_Satker", PD.Penerima_Satker);
                    cmd.Parameters.AddWithValue("IBAP_Satker", PD.BAP_Satker);
                    cmd.Parameters.AddWithValue("IKeterangan_Satker", PD.Keterangan_Satker);
                    cmd.Parameters.AddWithValue("ITanggal_Penyampaian_Ke_YBS", Tanggal_Penyampaian_Ke_YBS);
                    cmd.Parameters.AddWithValue("IPenerima_YBS", PD.Penerima_YBS);
                    cmd.Parameters.AddWithValue("IBAP_Penerima", PD.BAP_Penerima);
                    cmd.Parameters.AddWithValue("IKeterangan_Penerima", PD.Keterangan_Penerima);
                    cmd.Parameters.AddWithValue("ICreate_User", PD.CreateUser);
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
        public int InsertSK(clsHasilSidang PD)
        {
            try
            {
                int i = 0;
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string MySql = "SP_SK_TanpaSidangDPK_Ins";
                    SqlCommand cmd = new SqlCommand(MySql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", PD.ID);
                    cmd.Parameters.AddWithValue("@NIP", PD.NIP);
                    cmd.Parameters.AddWithValue("@Dasar_Bukti", PD.Mengingat ?? "");
                    cmd.Parameters.AddWithValue("@Keputusan_Sidang", PD.KeputusanSidang ?? "");
                    cmd.Parameters.AddWithValue("@UserID", PD.UserLogin ?? "");

                    DateTime dParse = DateTime.ParseExact(PD.TMTDate, "dd MMMM yyyy", null);
                    PD.TMTDate = dParse.ToString("yyyy-MM-dd");
                    cmd.Parameters.AddWithValue("@TMTDate", PD.TMTDate ?? "");
                    cmd.Parameters.AddWithValue("@tembusan", PD.Tembusan ?? "");

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
        public int Update(clsHasilSidang PD)
        {
            try
            {
                int i = 0;
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string MySql = "SIMHUKDIS.sp_DPK_Upd";

                    string TanggalSidang = Convert.ToDateTime(PD.Tanggal_Sidang_DPK).ToString("yyyy-MM-dd");
                    SqlCommand cmd = new SqlCommand(MySql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("iid", PD.ID);
                    cmd.Parameters.AddWithValue("iNIP", PD.NIP);
                    cmd.Parameters.AddWithValue("iCatatan_Sidang", PD.Catatan_Sidang ?? "");
                    cmd.Parameters.AddWithValue("iCreate_User", PD.UserLogin ?? "" );
                    cmd.Parameters.AddWithValue("iTanggal_Sidang", TanggalSidang);
                    cmd.Parameters.AddWithValue("iKeputusanSidang", PD.KeputusanSidang ?? "");
                    cmd.Parameters.AddWithValue("iDasar_Bukti", PD.DasarBukti ?? "");
                    cmd.Parameters.AddWithValue("iPelanggaran", PD.PelanggaranDisiplin ?? "");
                    cmd.Parameters.AddWithValue("iPasal_Pelanggaran", PD.PasalPelanggaran ?? "");
                    cmd.Parameters.AddWithValue("iMengingat", PD.Mengingat ?? "");
                    cmd.Parameters.AddWithValue("iTembusan", PD.Tembusan ?? "");
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
        public bool cek(string ID, string NIP)
        {
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SIMHUKDIS.sp_HasilSidang_AlreadyExist", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IID", ID);
                cmd.Parameters.AddWithValue("INIP", NIP);
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
    }
}