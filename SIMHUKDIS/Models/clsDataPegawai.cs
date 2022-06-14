using System;
using System.Collections.Generic;
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
        public string NIP { get; set; }
        public string NIP_BARU { get; set; }
        public string NAMA { get; set; }
        public string NAMA_LENGKAP { get; set; }
        public string AGAMA { get; set; }
        public string TEMPAT_LAHIR { get; set; }
        public string TANGGAL_LAHIR { get; set; }
        public int JENIS_KELAMIN { get; set; }
        public string PENDIDIKAN { get; set; }
        public string KODE_LEVEL_JABATAN { get; set; }
        public string LEVEL_JABATAN { get; set; }
        public string PANGKAT { get; set; }
        public string GOL_RUANG { get; set; }
        public string TMT_CPNS { get; set; }
        public string TMT_PANGKAT { get; set; }
        public string MASAKERJA_TAHUN { get; set; }
        public string MASAKERJA_BULAN { get; set; }
        public string TIPE_JABATAN { get; set; }
        public string KODE_JABATAN { get; set; }
        public string TAMPIL_JABATAN { get; set; }
        public string TMT_JABATAN { get; set; }
        public string KODE_SATKER_1 { get; set; }
        public string SATKER_1 { get; set; }
        public string KODE_SATKER_2 { get; set; }
        public string SATKER_2 { get; set; }
        public string KODE_SATKER_3 { get; set; }
        public string SATKER_3 { get; set; }
        public string KODE_SATKER_4 { get; set; }
        public string SATKER_4 { get; set; }
        public string KODE_SATKER_5 { get; set; }
        public string SATKER_5 { get; set; }
        public string SATUAN_KERJA { get; set; }
        public string STATUS_KAWIN { get; set; }
        public object ALAMAT_1 { get; set; }
        public object ALAMAT_2 { get; set; }
        public object TELEPON { get; set; }
        public object KAB_KOTA { get; set; }
        public object PROVINSI { get; set; }
        public object KODE_POS { get; set; }
        public string KODE_LOKASI { get; set; }
        public string KODE_PANGKAT { get; set; }
    }
}