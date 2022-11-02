using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace SIMHUKDIS.Models
{
    public class Token
    {
        [JsonProperty("access_token")]
        public Boolean status { get; set; }
        public int code { get; set; }
        public string token { get; set; }
    }
    public class Satker
    {
        public bool status { get; set; }
        public int code { get; set; }
        public string title { get; set; }
        public Datum[] data { get; set; }
    }
    public class Datum
    {
        public string KODE_SATUAN_KERJA { get; set; }
        public string SATUAN_KERJA { get; set; }
    }
}