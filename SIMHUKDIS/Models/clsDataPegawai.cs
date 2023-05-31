using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SIMHUKDIS.Models;

namespace SIMHUKDIS.Models
{
    public class clsDataPegawai
    {
        public bool status { get; set; }
        public int code { get; set; }
        public string title { get; set; }
        public clsDataPegawaiDtl data { get; set; }
    }
    public class clsDataPegawaiDtl
    {
        public string TEMBUSAN1 { get; set; }
        public string TEMBUSAN2 { get; set; }
        public string TASPEN { get; set; }
        public string KPPN { get; set; }
        public string NIP { get; set; }
        public string NIP_BARU { get; set; }
        public string NAMA { get; set; }
        public string NAMA_LENGKAP { get; set; }
        public string AGAMA { get; set; }
        public string TEMPAT_LAHIR { get; set; }
        public string TANGGAL_LAHIR { get; set; }
        public string JENIS_KELAMIN { get; set; }
        public string PENDIDIKAN { get; set; }
        public string JENJANG_PENDIDIKAN { get; set; }
        public string KODE_LEVEL_JABATAN { get; set; }
        public string LEVEL_JABATAN { get; set; }
        public string PANGKAT { get; set; }
        public string GOL_RUANG { get; set; }
        public string TMT_CPNS { get; set; }
        public string TMT_PANGKAT { get; set; }
        public string MK_TAHUN { get; set; }
        public string MK_BULAN { get; set; }
        public string GAJI_POKOK { get; set; }
        public string TIPE_JABATAN { get; set; }
        public string KODE_JABATAN { get; set; }
        public string TAMPIL_JABATAN { get; set; }
        public string TMT_JABATAN { get; set; }
        public string KODE_SATUAN_KERJA { get; set; }
        public string SATKER_1 { get; set; }
        public string SATKER_2 { get; set; }
        public string KODE_SATKER_2 { get; set; }
        public string SATKER_3 { get; set; }
        public string KODE_SATKER_3 { get; set; }
        public string SATKER_4 { get; set; }
        public string KODE_SATKER_4 { get; set; }
        public string SATKER_5 { get; set; }
        public string KODE_SATKER_5 { get; set; }
        public string KODE_GRUP_SATUAN_KERJA { get; set; }
        public string GRUP_SATUAN_KERJA { get; set; }
        public string KETERANGAN_SATUAN_KERJA { get; set; }
        public string STATUS_KAWIN { get; set; }
        public string ALAMAT_1 { get; set; }
        public string ALAMAT_2 { get; set; }
        public string TELEPON { get; set; }
        public string NO_HP { get; set; }
        public string EMAIL { get; set; }
        public string KAB_KOTA { get; set; }
        public string PROVINSI { get; set; }
        public string KODE_POS { get; set; }
        public string KODE_LOKASI { get; set; }
        public string ISO { get; set; }
        public string KODE_PANGKAT { get; set; }
        public string KETERANGAN { get; set; }
        public string tmt_pangkat_yad { get; set; }
        public string tmt_kgb_yad { get; set; }
        public string USIA_PENSIUN { get; set; }
        public string TMT_PENSIUN { get; set; }
        public string MK_TAHUN_1 { get; set; }
        public string MK_BULAN_1 { get; set; }
        public string NSM { get; set; }
        public string NPSN { get; set; }
        public string KODE_KUA { get; set; }
        public string KODE_BIDANG_STUDI { get; set; }
        public string BIDANG_STUDI { get; set; }
        public string STATUS_PEGAWAI { get; set; }
        public string LAT { get; set; }
        public string LON { get; set; }
        public string SATKER_KELOLA { get; set; }
    }
    public class clsPegawaiDB
    {
        public clsDataPegawaiDtl GetList(string NIP)
        {
            clsDataPegawaiDtl data = new clsDataPegawaiDtl();
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                Int32 pNom = 0;
                SqlCommand cmd = new SqlCommand("SP_PEGAWAI_SEL", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NIP", NIP);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    
                    data.NIP = rd["NIP"].ToString();
                    data.NIP_BARU = rd["NIP_BARU"].ToString();
                    data.NAMA = rd["NAMA"].ToString();
                    data.NAMA_LENGKAP = rd["NAMA_LENGKAP"].ToString();
                    data.AGAMA = rd["AGAMA"].ToString();
                    data.TEMPAT_LAHIR = rd["TEMPAT_LAHIR"].ToString();

                    data.TANGGAL_LAHIR = rd["TANGGAL_LAHIR"].ToString();
                    string TANGGAL_LAHIR = Convert.ToDateTime(data.TANGGAL_LAHIR).ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"));
                    data.TANGGAL_LAHIR = TANGGAL_LAHIR;

                    data.JENIS_KELAMIN = rd["JENIS_KELAMIN"].ToString();
                    data.PENDIDIKAN = rd["PENDIDIKAN"].ToString();
                    data.JENJANG_PENDIDIKAN = rd["JENJANG_PENDIDIKAN"].ToString();
                    data.KODE_LEVEL_JABATAN = rd["KODE_LEVEL_JABATAN"].ToString();
                    data.LEVEL_JABATAN = rd["LEVEL_JABATAN"].ToString();
                    data.PANGKAT = rd["PANGKAT"].ToString();
                    data.GOL_RUANG = rd["PANGKAT"].ToString() + ", " + rd["GOL_RUANG"].ToString();
                    data.TMT_CPNS = rd["TMT_CPNS"].ToString();
                    data.TMT_PANGKAT = rd["TMT_PANGKAT"].ToString();
                    data.MK_TAHUN = rd["MK_TAHUN"].ToString() + " Tahun " + rd["MK_BULAN"].ToString() + " Bulan";
                    data.MK_BULAN = rd["MK_BULAN"].ToString();
                    data.TAMPIL_JABATAN = rd["TAMPIL_JABATAN"].ToString();
                    data.SATKER_1 = rd["SATKER_1"].ToString();
                    data.SATKER_2 = rd["SATKER_2"].ToString();
                    data.SATKER_3 = rd["SATKER_3"].ToString();
                    data.SATKER_4 = rd["SATKER_4"].ToString();
                    data.SATKER_5 = rd["SATKER_5"].ToString();

                    data.TEMBUSAN1 = rd["TEMBUSAN_1"].ToString();
                    data.TEMBUSAN2 = rd["TEMBUSAN_2"].ToString();
                    data.TASPEN = rd["TASPEN"].ToString();
                    data.KPPN = rd["NAMA_KPPN"].ToString();

                    data.KETERANGAN_SATUAN_KERJA = rd["KETERANGAN_SATUAN_KERJA"].ToString();
                    data.KETERANGAN = rd["KETERANGAN"].ToString();
                    data.TMT_PENSIUN = rd["TMT_PENSIUN"].ToString();
                    string TMT_PENSIUN = Convert.ToDateTime(data.TMT_PENSIUN).ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"));
                    data.TMT_PENSIUN = TMT_PENSIUN;
                }
                return data;
            }
        }
        public List<clsDataPegawaiDtl> ListPegawai (string NIP)
        {
            List<clsDataPegawaiDtl> lst = new List<clsDataPegawaiDtl>();
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_PEGAWAI_SEL", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NIP", NIP);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {

                    clsDataPegawaiDtl data = new clsDataPegawaiDtl();
                    data.NIP = rd["NIP"].ToString();
                    data.NIP_BARU = rd["NIP_BARU"].ToString();
                    data.NAMA = rd["NAMA"].ToString();
                    data.NAMA_LENGKAP = rd["NAMA_LENGKAP"].ToString();
                    data.AGAMA = rd["AGAMA"].ToString();
                    data.TEMPAT_LAHIR = rd["TEMPAT_LAHIR"].ToString();

                    data.TANGGAL_LAHIR = rd["TANGGAL_LAHIR"].ToString();
                    string TANGGAL_LAHIR = Convert.ToDateTime(data.TANGGAL_LAHIR).ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"));
                    data.TANGGAL_LAHIR = TANGGAL_LAHIR;

                    data.JENIS_KELAMIN = rd["JENIS_KELAMIN"].ToString();
                    data.PENDIDIKAN = rd["PENDIDIKAN"].ToString();
                    data.JENJANG_PENDIDIKAN = rd["JENJANG_PENDIDIKAN"].ToString();
                    data.KODE_LEVEL_JABATAN = rd["KODE_LEVEL_JABATAN"].ToString();
                    data.LEVEL_JABATAN = rd["LEVEL_JABATAN"].ToString();
                    data.PANGKAT = rd["PANGKAT"].ToString();
                    data.GOL_RUANG = rd["PANGKAT"].ToString() + ", " + rd["GOL_RUANG"].ToString();
                    data.TMT_CPNS = rd["TMT_CPNS"].ToString();
                    data.TMT_PANGKAT = rd["TMT_PANGKAT"].ToString();
                    data.MK_TAHUN = rd["MK_TAHUN"].ToString() + " Tahun " + rd["MK_BULAN"].ToString() + " Bulan";
                    data.MK_BULAN = rd["MK_BULAN"].ToString();
                    data.TAMPIL_JABATAN = rd["TAMPIL_JABATAN"].ToString();
                    data.SATKER_1 = rd["SATKER_1"].ToString();
                    data.SATKER_2 = rd["SATKER_2"].ToString();
                    data.SATKER_3 = rd["SATKER_3"].ToString();
                    data.SATKER_4 = rd["SATKER_4"].ToString();
                    data.SATKER_5 = rd["SATKER_5"].ToString();
                    data.KODE_SATUAN_KERJA = rd["KODE_SATUAN_KERJA"].ToString();
                    data.TEMBUSAN1 = rd["TEMBUSAN_1"].ToString();
                    data.TASPEN = rd["TASPEN"].ToString();
                    data.KPPN = rd["NAMA_KPPN"].ToString();

                    data.KETERANGAN_SATUAN_KERJA = rd["KETERANGAN_SATUAN_KERJA"].ToString();
                    data.KETERANGAN = rd["KETERANGAN"].ToString();
                    data.TMT_PENSIUN = rd["TMT_PENSIUN"].ToString();
                    string TMT_PENSIUN = Convert.ToDateTime(data.TMT_PENSIUN).ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"));
                    data.TMT_PENSIUN = TMT_PENSIUN;
                    lst.Add(data);
                }
            }
            return lst;
        }
    }

}