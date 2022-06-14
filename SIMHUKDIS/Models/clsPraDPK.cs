using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using simhukdis.Models;
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
    }
    public class clsPraDPKDB
    {
        public DataTable GetDataTable()
        {
            int a = 0;
            DataTable table = new DataTable();
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            List<clsPraDPK> DP = new List<clsPraDPK>();
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string q = "sp_PraDPK_Sel";
                MySqlCommand cmd = new MySqlCommand(q, con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                MySqlDataReader rd = cmd.ExecuteReader();
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
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            List<clsPraDPK> DP = new List<clsPraDPK>();
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string q = "sp_PraDPK_Sel";
                MySqlCommand cmd = new MySqlCommand(q, con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                MySqlDataReader rd = cmd.ExecuteReader();
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

                    clsPraDPKDB x = new clsPraDPKDB();
                    List<clsDataPegawaiDtl> y = new List<clsDataPegawaiDtl>();
                    y = x.GetPegawai(data.NIP);
                    int z = 1;
                    foreach (var item in y)
                    {
                        if (z==1)
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
        public List<clsDataPegawaiDtl> GetPegawai(string NIP)
        {
            string strToken = "";
            string Username = "agus@kemenag.go.id";
            string Password = "12345678";
            string baseAddress = "https://ropeg.kemenag.go.id/api/v1/";
            try
            {
                var client = new HttpClient();
                List<clsDataPegawaiDtl> pegawai = new List<clsDataPegawaiDtl>();
                clsDataPegawaiDtl dtl = new clsDataPegawaiDtl();
                clsToken db = new clsToken();
                strToken = db.GetAccessToken(Username, Password);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strToken);
                HttpResponseMessage resp = client.GetAsync(baseAddress + "pegawai/profil/" + WebUtility.UrlEncode(NIP)).GetAwaiter().GetResult();
                if (resp.IsSuccessStatusCode)
                {
                    clsDataPegawai clsDataPegawai = new clsDataPegawai();
                    var JsonContent = resp.Content.ReadAsStringAsync().Result;
                    clsDataPegawai = JsonConvert.DeserializeObject<clsDataPegawai>(JsonContent);
                    int x = 0;
                    dtl.NIP = clsDataPegawai.data.NIP;
                    dtl.NIP_BARU = clsDataPegawai.data.NIP_BARU;
                    dtl.NAMA = clsDataPegawai.data.NAMA;
                    dtl.NAMA_LENGKAP = clsDataPegawai.data.NAMA_LENGKAP;
                    dtl.AGAMA = clsDataPegawai.data.AGAMA;
                    dtl.TEMPAT_LAHIR = clsDataPegawai.data.TEMPAT_LAHIR;
                    dtl.TANGGAL_LAHIR = clsDataPegawai.data.TANGGAL_LAHIR.Substring(0, 10);
                    dtl.JENIS_KELAMIN = clsDataPegawai.data.JENIS_KELAMIN;
                    dtl.PENDIDIKAN = clsDataPegawai.data.PENDIDIKAN;
                    dtl.KODE_LEVEL_JABATAN = clsDataPegawai.data.KODE_LEVEL_JABATAN;
                    dtl.LEVEL_JABATAN = clsDataPegawai.data.LEVEL_JABATAN;

                    string strPangkat = clsDataPegawai.data.PANGKAT;
                    string strGolRuang = clsDataPegawai.data.GOL_RUANG;
                    string strPangkatGolRuang = strPangkat + ", " + strGolRuang;


                    dtl.PANGKAT = strPangkatGolRuang;
                    dtl.GOL_RUANG = strPangkatGolRuang;

                    dtl.TMT_CPNS = clsDataPegawai.data.TMT_CPNS;
                    dtl.TMT_PANGKAT = clsDataPegawai.data.TMT_PANGKAT;

                    string strMasaKerjaThn = clsDataPegawai.data.MASAKERJA_TAHUN.ToString();
                    string strMasaKerjaBln = clsDataPegawai.data.MASAKERJA_BULAN.ToString();
                    string strMasaKerja = strMasaKerjaThn + " Tahun " + strMasaKerjaBln + " Bulan";

                    dtl.MASAKERJA_TAHUN = strMasaKerja;
                    dtl.MASAKERJA_BULAN = strMasaKerja;
                    dtl.TIPE_JABATAN = clsDataPegawai.data.TIPE_JABATAN;
                    dtl.KODE_JABATAN = clsDataPegawai.data.KODE_JABATAN;
                    dtl.TAMPIL_JABATAN = clsDataPegawai.data.TAMPIL_JABATAN;
                    dtl.TMT_JABATAN = clsDataPegawai.data.TMT_JABATAN;
                    dtl.KODE_SATKER_1 = clsDataPegawai.data.KODE_SATKER_1;
                    dtl.SATKER_1 = clsDataPegawai.data.SATKER_1;
                    dtl.KODE_SATKER_2 = clsDataPegawai.data.KODE_SATKER_2;
                    dtl.SATKER_2 = clsDataPegawai.data.SATKER_2;
                    dtl.KODE_SATKER_3 = clsDataPegawai.data.KODE_SATKER_3;
                    dtl.SATKER_3 = clsDataPegawai.data.SATKER_3;
                    dtl.KODE_SATKER_4 = clsDataPegawai.data.KODE_SATKER_4;
                    dtl.SATKER_4 = clsDataPegawai.data.SATKER_4;
                    dtl.KODE_SATKER_5 = clsDataPegawai.data.KODE_SATKER_5;
                    dtl.SATKER_5 = clsDataPegawai.data.SATKER_5;
                    dtl.SATUAN_KERJA = clsDataPegawai.data.SATUAN_KERJA;
                    dtl.STATUS_KAWIN = clsDataPegawai.data.STATUS_KAWIN;
                    dtl.ALAMAT_1 = clsDataPegawai.data.ALAMAT_1;
                    dtl.ALAMAT_2 = clsDataPegawai.data.ALAMAT_2;
                    dtl.TELEPON = clsDataPegawai.data.TELEPON;
                    dtl.KAB_KOTA = clsDataPegawai.data.KAB_KOTA;
                    dtl.PROVINSI = clsDataPegawai.data.PROVINSI;
                    dtl.KODE_POS = clsDataPegawai.data.KODE_POS;
                    dtl.KODE_LOKASI = clsDataPegawai.data.KODE_LOKASI;
                    dtl.KODE_PANGKAT = clsDataPegawai.data.KODE_PANGKAT;
                    //return Json(dtl, JsonRequestBehavior.AllowGet);
                    pegawai.Add(dtl);
                }
                else
                {
                    return null;
                }
                return pegawai;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Insert(clsPraDPK PD)
        {
            try
            {
                int i = 0;
                Encryption encrypt = new Encryption();
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (MySqlConnection con = new MySqlConnection(constr))
                {
                    string MySql = "sp_PraDPK_Ins";
                    string TanggalSidang = Convert.ToDateTime(PD.Tanggal_Sidang).ToString("yyyy-MM-dd");
                    MySqlCommand cmd = new MySqlCommand(MySql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("iid", PD.ID);
                    cmd.Parameters.AddWithValue("iNIP", PD.NIP);
                    cmd.Parameters.AddWithValue("iCatatan", PD.Catatan);
                    cmd.Parameters.AddWithValue("iCreate_User", PD.UserLogin);
                    cmd.Parameters.AddWithValue("iTanggal_Sidang", TanggalSidang);
                    
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
                Encryption encrypt = new Encryption();
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (MySqlConnection con = new MySqlConnection(constr))
                {
                    string MySql = "sp_PraDPK_Upd";
                    string TanggalSidang = Convert.ToDateTime(PD.Tanggal_Sidang).ToString("yyyy-MM-dd");
                    MySqlCommand cmd = new MySqlCommand(MySql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("iid", PD.ID);
                    cmd.Parameters.AddWithValue("iNIP", PD.NIP);
                    cmd.Parameters.AddWithValue("iCatatan", PD.Catatan);
                    cmd.Parameters.AddWithValue("iCreate_User", PD.UserLogin);
                    cmd.Parameters.AddWithValue("iTanggal_Sidang", TanggalSidang);
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
                Encryption encrypt = new Encryption();
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (MySqlConnection con = new MySqlConnection(constr))
                {
                    string MySql = "sp_PraDPK_Upd_Status";
                    MySqlCommand cmd = new MySqlCommand(MySql, con);
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
                Encryption encrypt = new Encryption();
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (MySqlConnection con = new MySqlConnection(constr))
                {
                    string MySql = "sp_PraDPK_Upd_Status2";
                    MySqlCommand cmd = new MySqlCommand(MySql, con);
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