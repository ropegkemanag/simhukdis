using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIMHUKDIS.Models
{
    public class clsPekerjaan
    {
        public bool status { get; set; }
        public int code { get; set; }
        public string title { get; set; }
        public clsDataPekerjaan data { get; set; }
    }

    public class clsDataPekerjaan
    {
        public clsPangkat[] pangkat { get; set; }
        public clsJabatan[] jabatan { get; set; }
    }

    public class clsPangkat
    {
        public string NIP { get; set; }
        public int NO_URUT { get; set; }
        public string KODE_PANGKAT { get; set; }
        public string PANGKAT { get; set; }
        public string GOL_RUANG { get; set; }
        public string NO_SK { get; set; }
        public string TGL_SK { get; set; }
        public string TMT_SK { get; set; }
        public int MK_TAHUN { get; set; }
        public int MK_BULAN { get; set; }
        public string KETERANGAN { get; set; }
    }

    public class clsJabatan
    {
        public string NIP { get; set; }
        public int NO_URUT { get; set; }
        public string KODE_JABATAN { get; set; }
        public string JABATAN { get; set; }
        public object KODE_BIDANG_STUDI { get; set; }
        public object BIDANG_STUDI { get; set; }
        public string KODE_SATUAN_KERJA { get; set; }
        public string SATUAN_KERJA { get; set; }
        public string KETERANGAN_SATUAN_KERJA { get; set; }
        public string NO_SK { get; set; }
        public string TGL_SK { get; set; }
        public string TMT_SK { get; set; }
        public object KODE_JABATAN_2 { get; set; }
        public object JABATAN_2 { get; set; }
        public string KODE_SATUAN_KERJA_2 { get; set; }
        public string SATUAN_KERJA_2 { get; set; }
        public string KETERANGAN_SATUAN_KERJA_2 { get; set; }
        public string NO_SK_2 { get; set; }
        public string TGL_SK_2 { get; set; }
        public string TMT_SK_2 { get; set; }
        public string KETERANGAN { get; set; }
        public string KODE_PANGKAT { get; set; }
        public string PANGKAT { get; set; }
        public string GOL_RUANG { get; set; }
    }
}