using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SIMHUKDIS.Models
{
    public class clsUploadTelaah
    {
        public Int32 ID { get; set; }
        public string NIP { get; set; }
        public string FileTelaah { get; set; }
        public string NoSurat { get; set; }
        public string AsalSurat { get; set; }
        public string TanggalSurat { get; set; }
        [DisplayName("Perihal")]
        public string perihal { get; set; }
        [DisplayName("Satuan Kerja")]
        public string SATUAN_KERJA { get; set; }
        [DisplayName("Kode Satuan Kerja")]
        public string KODE_SATUAN_KERJA { get; set; }
    }
    public class clsUploadTelaahDB
    {

    }
}