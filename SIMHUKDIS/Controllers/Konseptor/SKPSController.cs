using Newtonsoft.Json;
using SIMHUKDIS.Models;
using Spire.Doc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace SIMHUKDIS.Controllers.Konseptor
{
    public class SKPSController : Controller
    {
        // GET: SKPS
        clsSKPSDB db = new clsSKPSDB();
        string Username = "agus@kemenag.go.id";
        string Password = "12345678";
        string strToken = "";
        string baseAddress = "https://api.kemenag.go.id/v1/";
        public ActionResult Index()
        {
            if (Session["Fullname"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            string userlogin = Session["Fullname"].ToString();
            string SatuanKerja = Session["Satker"].ToString();
            string StatusAdmin = Session["StatusAdmin"].ToString();
            string UserGroup = Session["UserGroup"].ToString();
            string UserID = Session["UserID"].ToString();
            ViewBag.UserID = UserID;
            ViewBag.SatuanKerja = SatuanKerja;
            ViewBag.UserGroup = UserGroup;
            try
            {
                List<clsSKPS> PS = db.PSList(UserID).ToList();
                return View(PS);
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });
            }
        }
        public ActionResult Details(int ID)
        {
            if (Session["Fullname"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            try
            {
                string userlogin = Session["Fullname"].ToString();
                string SatuanKerja = Session["Satker"].ToString();
                string StatusAdmin = Session["StatusAdmin"].ToString();
                string UserGroup = Session["UserGroup"].ToString();
                string UserID = Session["UserID"].ToString();
                ViewBag.UserID = UserID;
                ViewBag.SatuanKerja = SatuanKerja;
                ViewBag.UserGroup = UserGroup;
                clsSKPS sm = db.GetList(ID);
                DateTime SKDate;
                if (sm.TMTDate != "")
                {
                    SKDate = DateTime.ParseExact(sm.SKDate, "dd-MM-yyyy", null);
                }
                else
                {
                    SKDate = Convert.ToDateTime("1900-01-01");
                } 
                
                DateTime TMTDate;
                if (sm.TMTDate != "")
                {
                    TMTDate = DateTime.ParseExact(sm.TMTDate, "dd-MM-yyyy", null);
                }
                else
                {
                    TMTDate = Convert.ToDateTime("1900-01-01");
                }
                

                sm.SKDate = SKDate.ToString("dd MMMM yyyy");
                sm.TMTDate = TMTDate.ToString("dd MMMM yyyy");
                ViewBag.JenisUsul = sm.JenisUsul;
                ViewBag.SKDate = sm.SKDate;
                ViewBag.TMTDate = sm.TMTDate;
                DateTime TanggalSurat = DateTime.ParseExact(sm.TanggalSurat, "dd-MM-yyyy", null);
                sm.TanggalSurat = TanggalSurat.ToString("d MMMM yyyy", new System.Globalization.CultureInfo("id-ID"));
                return View(sm);
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });
            }
        }


        public ActionResult SendtoMenag(string ID)
        {
            string strMsg = "";
            try
            {
                string UserLogin = Session["Fullname"].ToString();
                clsSKPS PD = new clsSKPS();
                PD.ID = Convert.ToInt16(ID);

                db.Update(PD);
                strMsg = "Success";
                return Json(strMsg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });
            }
        }

        public ActionResult GenerateWord(int ID, string NIP,
        string NAMA_LENGKAP, string GOL_RUANG, string LEVEL_JABATAN,
        string SATUAN_KERJA, string TEMPAT_LAHIR, string TANGGAL_LAHIR,
        string MK_TAHUN, string TMT_Pensiun, string DasarBukti,
        string SKDate, string PSReason, string TMTDate, string tembusan, 
        int JenisUsul, string Konseptor, string DasarPemberhentian)
        {
            string filepath = HttpContext.Request.PhysicalApplicationPath;
            string strTemplate = "";
            if (JenisUsul == 1)
            {
                strTemplate = filepath + "Files/Template/TEMPLATE PEMBERHENTIAN SEMENTARA KARENA MENJADI ANGGOTA.docx";
            }
            if (JenisUsul == 2)
            {
                strTemplate = filepath + "Files/Template/TEMPLATE PEMBERHENTIAN SEMENTARA KARENA TINDAK PIDANA" +
                    ".docx";
            }
            if (JenisUsul == 3)
            {
                strTemplate = filepath + "Files/Template/TEMPLATE PENGAKTIFAN KEMBALI KARENA SELESAI MENJADI ANGGOTA.docx";
            }
            if (JenisUsul == 4)
            {
                strTemplate = filepath + "Files/Template/TEMPLATE PENGAKTIFAN KEMBALI KARENA TINDAK PIDANA.docx";
            }
            string OutputPath = filepath + "Files/Result/PS/";
            string GetDateTime = DateTime.Now.ToString("dd MMMM yyyy");
            string strFileNameDoc = "SK a.n " + NAMA_LENGKAP + "_NIP " + NIP+ "_" + GetDateTime + ".docx";
            string strFileNamePDF = "SK Pemberhentian Sementara a.n " + NAMA_LENGKAP + "_NIP_" + NIP + "_" + GetDateTime + ".pdf";
            string strMsg = "";
            try
            {
                if (Session["Fullname"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                string userlogin = Session["Fullname"].ToString();
                string UserID = Session["UserID"].ToString();
                string SatuanKerja = Session["Satker"].ToString();
                string StatusAdmin = Session["StatusAdmin"].ToString();
                string UserGroup = Session["UserGroup"].ToString();
                ViewBag.UserID = userlogin;
                ViewBag.SatuanKerja = SatuanKerja;
                ViewBag.UserGroup = UserGroup;
                ViewBag.UserID = userlogin;
                string UserLogin = Session["Fullname"].ToString();

                clsSKPS PS = new clsSKPS();
                PS.ID = ID;
                PS.NIP = NIP;

                clsDataPegawaiDtl p = new clsDataPegawaiDtl();
                clsPegawaiDB q = new clsPegawaiDB();
                p = q.GetList(NIP);

                PS.TEMBUSAN1 = p.TEMBUSAN1;
                PS.TEMBUSAN2 = p.TEMBUSAN2;
                PS.KPPN = p.KPPN;
                PS.TASPEN = p.TASPEN;

                PS.NAMA_LENGKAP = p.NAMA_LENGKAP;
                PS.GOL_RUANG = p.GOL_RUANG;
                PS.LEVEL_JABATAN = p.TAMPIL_JABATAN + " " + p.SATKER_1 + " "+ p.SATKER_2 ;
                PS.SATUAN_KERJA = p.SATKER_4;
                PS.TEMPAT_LAHIR = p.TEMPAT_LAHIR;
                PS.TANGGAL_LAHIR = p.TANGGAL_LAHIR;
                PS.MK_TAHUN = MK_TAHUN;
                PS.TMT_Pensiun = TMT_Pensiun;
                PS.DasarBukti = DasarBukti;
                PS.SKDate = SKDate;
                PS.PSReason = PSReason;
                PS.TMTDate = TMTDate;
                PS.tembusan = tembusan;
                PS.DasarPemberhentian = DasarPemberhentian;
                clsTelaahDB i = new clsTelaahDB();
                int Bulan = DateTime.Now.Month;
                string Tahun = DateTime.Now.Year.ToString();
                
                string strBulan = string.Format(string.Format("{0:00}", Bulan));

                clsPejabatMst pejabatMst = new clsPejabatMst();
                clsPejabatMstDB pdb = new clsPejabatMstDB();
                pejabatMst = pdb.GetList();

                string Karopeg = pejabatMst.Nama_Karopeg;
                string NIP_Karopeg = pejabatMst.NIP_Karopeg;
                PS.Bulan = strBulan;
                PS.Tahun = Tahun;
                PS.Menag = pejabatMst.Menag;
                PS.Karopeg = Karopeg;
                PS.NIP_Karopeg = NIP_Karopeg;
                PS.Koordinator = pejabatMst.Nama_Koordinator;
                PS.Jabatan_Koordinator = pejabatMst.Jabatan_Koordinator;
                PS.SubKoordinator = pejabatMst.Nama_SubKoordinator;
                PS.Jabatan_SubKoordinator = pejabatMst.Jabatan_SubKoordinator;

                p = q.GetList(Konseptor);

                PS.Konseptor = p.NAMA_LENGKAP;
                PS.NIP_Konseptor = Konseptor;
                PS.Jabatan_Konseptor = p.TAMPIL_JABATAN;
                PS.Create_User = UserID;
                PS.JenisUsul = Convert.ToString(JenisUsul);
                Document doc = new Document();
                doc.LoadFromFile(strTemplate);
                //get strings to replace
                Dictionary<string, string> dictReplace = GetReplaceDictionary(PS);
                //Replace text
                foreach (KeyValuePair<string, string> kvp in dictReplace)
                {
                    doc.Replace(kvp.Key, kvp.Value, true, true);
                }
                //Save doc file.
                doc.SaveToFile(OutputPath + strFileNameDoc, FileFormat.Docx);
                //Convert to PDF
                doc.SaveToFile(OutputPath + strFileNamePDF, FileFormat.PDF);
                //Insert Table
                clsTelaahDB a = new clsTelaahDB();
               db.Insert(PS);

                clsDocument b = new clsDocument();
                b.DocPDF = strFileNamePDF;
                b.DocWord = strFileNameDoc;
                b.Msg = "Success";
                //DownloadFile(strFileNamePDF);
                return Json(b, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                strMsg = ex.Message.ToString();
                return Json(strMsg, JsonRequestBehavior.AllowGet);
            }
        }
        public string ToUpperEveryWord(string s)
        {
            // Check for empty string.  
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            var words = s.Split(' ');

            string t = "";
            foreach (var word in words)
            {
                t += char.ToUpper(word[0]) + word.Substring(1) + ' ';
            }
            return t;
        }

        public JsonResult GetPegawai(string NIP)
        {
            try
            {
                var client = new HttpClient();
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
                    dtl.TANGGAL_LAHIR = Convert.ToDateTime(dtl.TANGGAL_LAHIR).ToString("d MMMM yyyy", new System.Globalization.CultureInfo("id-ID"));
                    dtl.JENIS_KELAMIN = clsDataPegawai.data.JENIS_KELAMIN;
                    dtl.PENDIDIKAN = clsDataPegawai.data.PENDIDIKAN;
                    dtl.KODE_LEVEL_JABATAN = clsDataPegawai.data.KODE_LEVEL_JABATAN;
                    dtl.LEVEL_JABATAN = clsDataPegawai.data.LEVEL_JABATAN;

                    string Jabatan = clsDataPegawai.data.LEVEL_JABATAN;
                    string dtl_Satker = clsDataPegawai.data.KETERANGAN_SATUAN_KERJA;

                    string strPangkat = clsDataPegawai.data.PANGKAT;
                    string strGolRuang = clsDataPegawai.data.GOL_RUANG;
                    string strPangkatGolRuang = strPangkat + ", " + strGolRuang;


                    dtl.PANGKAT = strPangkatGolRuang;
                    dtl.GOL_RUANG = strPangkatGolRuang;

                    dtl.TMT_CPNS = clsDataPegawai.data.TMT_CPNS;
                    dtl.TMT_PANGKAT = clsDataPegawai.data.TMT_PANGKAT;

                    string strMasaKerjaThn = clsDataPegawai.data.MK_TAHUN.ToString();
                    string strMasaKerjaBln = clsDataPegawai.data.MK_BULAN.ToString();
                    string strMasaKerja = strMasaKerjaThn + " Tahun " + strMasaKerjaBln + " Bulan";
                    dtl.KETERANGAN_SATUAN_KERJA = clsDataPegawai.data.KETERANGAN_SATUAN_KERJA;
                    dtl.MK_TAHUN = strMasaKerja;
                    dtl.MK_BULAN = strMasaKerja;
                    dtl.TIPE_JABATAN = clsDataPegawai.data.TIPE_JABATAN;
                    dtl.KODE_JABATAN = clsDataPegawai.data.KODE_JABATAN;
                    dtl.TAMPIL_JABATAN = clsDataPegawai.data.TAMPIL_JABATAN + " " + clsDataPegawai.data.SATKER_1;
                    dtl.KETERANGAN = clsDataPegawai.data.KETERANGAN;
                    dtl.TMT_JABATAN = clsDataPegawai.data.TMT_JABATAN;
                    dtl.KODE_SATUAN_KERJA = clsDataPegawai.data.KODE_SATUAN_KERJA;
                    dtl.SATKER_1 = clsDataPegawai.data.SATKER_1;
                    dtl.KODE_SATKER_2 = clsDataPegawai.data.KODE_SATKER_2;
                    dtl.SATKER_2 = clsDataPegawai.data.SATKER_2;
                    dtl.KODE_SATKER_3 = clsDataPegawai.data.KODE_SATKER_3;
                    dtl.SATKER_3 = clsDataPegawai.data.SATKER_3;
                    dtl.KODE_SATKER_4 = clsDataPegawai.data.KODE_SATKER_4;
                    dtl.SATKER_4 = clsDataPegawai.data.SATKER_4;
                    dtl.KODE_SATKER_5 = clsDataPegawai.data.KODE_SATKER_5;
                    dtl.SATKER_5 = clsDataPegawai.data.SATKER_5;

                    dtl.STATUS_KAWIN = clsDataPegawai.data.STATUS_KAWIN;
                    dtl.ALAMAT_1 = clsDataPegawai.data.ALAMAT_1;
                    dtl.ALAMAT_2 = clsDataPegawai.data.ALAMAT_2;
                    dtl.TELEPON = clsDataPegawai.data.TELEPON;
                    dtl.KAB_KOTA = clsDataPegawai.data.KAB_KOTA;
                    dtl.PROVINSI = clsDataPegawai.data.PROVINSI;
                    dtl.KODE_POS = clsDataPegawai.data.KODE_POS;
                    dtl.KODE_LOKASI = clsDataPegawai.data.KODE_LOKASI;
                    dtl.KODE_PANGKAT = clsDataPegawai.data.KODE_PANGKAT;
                    dtl.TMT_PENSIUN = clsDataPegawai.data.TMT_PENSIUN.Substring(0, 10);
                    dtl.TMT_PENSIUN = Convert.ToDateTime(dtl.TMT_PENSIUN).ToString("d MMMM yyyy", new System.Globalization.CultureInfo("id-ID"));
                    dtl.NO_HP = clsDataPegawai.data.NO_HP;
                    dtl.EMAIL = clsDataPegawai.data.EMAIL;
                    return Json(dtl, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(null, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return new JsonResult
                {
                    Data = new { ErrorMessage = ex.Message, Success = false },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.DenyGet
                };
            }
        }
        Dictionary<string, string> GetReplaceDictionary(clsSKPS PS)
        {
            Dictionary<string, string> replaceDict = new Dictionary<string, string>();
            string nama = ToUpperEveryWord(PS.NAMA_LENGKAP);
            replaceDict.Add("*Nama_Lengkap*", PS.NAMA_LENGKAP);
            replaceDict.Add("*NIP*", PS.NIP);
            replaceDict.Add("*TTL*", PS.TEMPAT_LAHIR + ", " + PS.TANGGAL_LAHIR);
            replaceDict.Add("*PangkatGol*", PS.GOL_RUANG);
            replaceDict.Add("*Jabatan*", PS.LEVEL_JABATAN);
            replaceDict.Add("*UnitKerja*", PS.SATUAN_KERJA);
            DateTime dParse = DateTime.ParseExact(PS.TMTDate, "dd MMMM yyyy", null);
            string TMTDate = dParse.ToString("d MMMM yyyy", new System.Globalization.CultureInfo("id-ID"));
            replaceDict.Add("*TMT*", TMTDate);
            replaceDict.Add("*DasarBukti*", PS.DasarBukti);
            replaceDict.Add("*Alasan*", PS.PSReason);
            replaceDict.Add("*MENAG*", PS.Menag);
            replaceDict.Add("*tembusan*", PS.tembusan);
            replaceDict.Add("*BULAN*", PS.Bulan);
            replaceDict.Add("*TAHUN*", PS.Tahun);
            replaceDict.Add("*Jabatan_Konseptor*", PS.Jabatan_Konseptor);
            replaceDict.Add("*Konseptor*", PS.Konseptor);
            replaceDict.Add("*Jabatan_Kasubag*", PS.Jabatan_SubKoordinator);
            replaceDict.Add("*Kasubag*", PS.SubKoordinator);
            replaceDict.Add("*Jabatan_Koordinator*", PS.Jabatan_Koordinator);
            replaceDict.Add("*Koordinator*", PS.Koordinator);

            replaceDict.Add("*DasarPS*", PS.DasarPemberhentian);
            replaceDict.Add("*TEMBUSAN_1*", PS.TEMBUSAN1);
            replaceDict.Add("*TEMBUSAN_2*", PS.TEMBUSAN2);
            replaceDict.Add("*TASPEN*", PS.TASPEN);
            replaceDict.Add("*KPPN*", PS.KPPN);

            return replaceDict;
        }
        public FileResult DownloadFile(string fileName)
        {
            try

            {
                //Build the File Path.
                string path = Server.MapPath("/Files/Result/PS/") + fileName;

                //Read the File data into Byte Array.
                byte[] bytes = System.IO.File.ReadAllBytes(path);

                //Send the File to Download.
                return File(bytes, "application/octet-stream", fileName);
            }
            catch (Exception ex)
            {
                string strMsg = ex.Message.ToString();
                return null;
            }

        }
    }
}