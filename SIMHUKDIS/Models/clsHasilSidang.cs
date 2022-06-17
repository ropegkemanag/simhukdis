using MySql.Data.MySqlClient;
using simhukdis.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
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
    }
    public class HasilSidangDtl
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
    }
    public class clsHasilSidangDB
    {
        public List<clsHasilSidang> ListAll(string UserID)
        {
            int a = 0;
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            List<clsHasilSidang> DP = new List<clsHasilSidang>();
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string q = "sp_HasilSidang_Sel";
                MySqlCommand cmd = new MySqlCommand(q, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("UserID", UserID);
                con.Open();
                MySqlDataReader rd = cmd.ExecuteReader();
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
                            data.SATKER_1  = item.SATKER_1;
                            data.SATKER_2  = item.SATKER_2;
                            data.SATKER_3  = item.SATKER_3;
                            data.SATKER_4  = item.SATKER_4;
                            data.SATKER_5 = item.SATKER_5;
                        }
                        z = z + 1;
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
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string q = "sp_PasalPelanggaran_Sel";
                MySqlCommand cmd = new MySqlCommand(q, con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                MySqlDataReader rd = cmd.ExecuteReader();
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
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                Int32 pNom = 0;
                string q = "sp_HasilSidang_Create";
                MySqlCommand cmd = new MySqlCommand(q, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("iid", ID);
                cmd.Parameters.AddWithValue("iNIP", NIP);
                con.Open();
                MySqlDataReader rd = cmd.ExecuteReader();
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
                    data.DasarBukti = rd["Dasar_dan_Bukti_Penunjang"].ToString();
                    data.PelanggaranDisiplin = rd["Pelanggaran"].ToString();
                    data.PasalPelanggaran = rd["Pasal_Pelanggaran"].ToString();
                    data.RekomendasiHukdis = rd["Rekomendasi_Hukuman"].ToString();
                    data.AnalisaPertimbangan = rd["Analisa_dan_Pertimbangan"].ToString();
                    data.Mengingat = rd["Mengingat"].ToString();
                }
                return data;
            }
        }
        public clsHasilSidang ListAll(int ID, string NIP)
        {
            int a = 1;
            string k = "";
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            clsHasilSidang DP = new clsHasilSidang();
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string q = "sp_HasilSidang_Sel_ByID";
                MySqlCommand cmd = new MySqlCommand(q, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IID", ID);
                cmd.Parameters.AddWithValue("INIP", NIP);
                con.Open();
                MySqlDataReader rd = cmd.ExecuteReader();
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
                        DP.Tembusan = rd["Tembusan"].ToString();
                        clsPraDPKDB x = new clsPraDPKDB();
                        List<clsDataPegawaiDtl> y = new List<clsDataPegawaiDtl>();
                        y = x.GetPegawai(DP.NIP);
                        int z = 1;
                        foreach (var item in y)
                        {
                            if (z == 1)
                            {
                                DP.GOL_RUANG = item.PANGKAT + ", " + item.GOL_RUANG;
                                DP.LEVEL_JABATAN = item.LEVEL_JABATAN;
                                DP.UnitKerja = item.SATUAN_KERJA;
                               DP.SATKER_1 = item.SATKER_1;
                               DP.SATKER_2 = item.SATKER_2;
                               DP.SATKER_3 = item.SATKER_3;
                               DP.SATKER_4 = item.SATKER_4;
                                DP.SATKER_5 = item.SATKER_5;
                                k = item.SATKER_3;
                            }
                            z = z + 1;
                        }
                        clsHasilSidangDB p = new clsHasilSidangDB();
                        DP.Tembusan = p.GetTembusan(DP.SATUAN_KERJA, k);
                    }
                }
                return DP;
            }
        }
        public string GetTembusan(string KodeSatker, string Satker3)
        {
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            HasilSidangDtl data = new HasilSidangDtl();
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                Int32 pNom = 1;
                string a = "";
                string Tembusan = "";
                string q = "sp_Tembusan_sel";
                MySqlCommand cmd = new MySqlCommand(q, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("iKODE_SATUAN_KERJA", KodeSatker);
                cmd.Parameters.AddWithValue("iSATUAN_KERJA", Satker3);
                con.Open();
                MySqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    if (pNom == 1)
                    {
                        a = rd["Tembusan"].ToString();
                        if (a == null || a == "")
                        {
                            if (Tembusan == "")
                            {
                                Tembusan = a;
                            }
                            else
                            {
                                Tembusan = Tembusan + " /n " + a;
                            }
                        }
                    }
                }
                return a;
            }
        }
        public int Insert(clsHasilSidang PD)
        {
            try
            {
                int i = 0;
                Encryption encrypt = new Encryption();
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (MySqlConnection con = new MySqlConnection(constr))
                {
                    string MySql = "sp_DPK_Ins";
                    string TanggalSidang = Convert.ToDateTime(PD.Tanggal_Sidang_DPK).ToString("yyyy-MM-dd");
                    MySqlCommand cmd = new MySqlCommand(MySql, con);
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
        public int Update(clsHasilSidang PD)
        {
            try
            {
                int i = 0;
                Encryption encrypt = new Encryption();
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (MySqlConnection con = new MySqlConnection(constr))
                {
                    string MySql = "sp_DPK_Upd";
                    string TanggalSidang = Convert.ToDateTime(PD.Tanggal_Sidang_DPK).ToString("yyyy-MM-dd");
                    MySqlCommand cmd = new MySqlCommand(MySql, con);
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
        public bool GetDataExist(string ID, string NIP)
        {
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("sp_DPK_Already_Sel", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("iid", ID);
                cmd.Parameters.AddWithValue("iNIP", NIP);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
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