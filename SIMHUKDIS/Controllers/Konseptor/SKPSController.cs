using Newtonsoft.Json;
using SIMHUKDIS.Models;
using Spire.Doc;
using System;
using System.Collections.Generic;
using System.IO;
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
        string strToken = "oaRYeMTcOSI4SM81dsSaos6oSPIltIwxJhybwi2Zd5d26RdmqGghELJQgnDn32K1";
        string baseAddress = "https://kudus.wablas.com/";
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
        [HttpPost]
        public ActionResult UploadFiles()
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

            if (Request.Files.Count > 0)
            {
                try
                {
                    string FilePath = "";
                    string FileExt = "";
                    string GetDateTime = "";
                    string Ext = "";
                    string FileNameWithoutExtension = "";
                    string fname, FileName = "";
                    GetDateTime = DateTime.Now.ToString("ddMMyyyy_hhmmss");
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";
                        string filename = Path.GetFileName(Request.Files[i].FileName);

                        HttpPostedFileBase file = files[i];

                        fname = file.FileName;
                        FileExt = Path.GetExtension(fname);
                        FileNameWithoutExtension = Path.GetFileNameWithoutExtension(fname);
                        FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                        fname = FileNameWithoutExtension + "_" + GetDateTime + FileExt;

                        FileName = fname;
                        fname = Path.Combine(Server.MapPath("/Files/Upload/SK/"), fname);
                        file.SaveAs(fname);
                    }

                    return Json(FileName, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
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
        public ActionResult Selesai(string ID, string FileSK, string NO_SK, string Tanggal_SK)
        {
            if (Session["Fullname"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            string strMsg;
            try
            {
                string userlogin = Session["Fullname"].ToString();
                string UserID = Session["UserID"].ToString();
                string SatuanKerja = Session["Satker"].ToString();
                string StatusAdmin = Session["StatusAdmin"].ToString();
                string UserGroup = Session["UserGroup"].ToString();
                ViewBag.UserID = userlogin;
                ViewBag.SatuanKerja = SatuanKerja;
                ViewBag.UserGroup = UserGroup;
                ViewBag.UserID = userlogin;
                string GetDateTime = "";
                GetDateTime = DateTime.Now.ToString("ddMMyyyy_hhmmss");

                clsSKPS PS = new clsSKPS();
                string UserLogin = Session["Fullname"].ToString();
                PS.Create_User = UserID;
                PS.ID = Convert.ToInt16(ID);
                PS.Tanggal_SK = Tanggal_SK;
                PS.NO_SK = NO_SK;
                if (FileSK == null)
                {
                    PS.FileSK = "";
                }
                else
                {
                    PS.FileSK = FileSK;
                }

                clsSKPSDB db = new clsSKPSDB();
                //db.Update(telaah);
                db.Selesai(PS);

                SendWaBlas(PS.ID,0);
                SendWaBlas(PS.ID, 1);
                SendWaBlas(PS.ID, 2);
                SendWaBlas(PS.ID, 3);
                SendWaBlas(PS.ID, 4);

                strMsg = "Success";
                return Json(strMsg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                strMsg = Error_Message;
                return Json(strMsg, JsonRequestBehavior.AllowGet);
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
        int JenisUsul, string Konseptor, string DasarPemberhentian, string KanregBKN, string PertekBKN)
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
            string strFileNameDoc = "SK_a.n_" + NAMA_LENGKAP + "_NIP " + NIP + ".docx";
            string strFileNamePDF = "SK_a.n_" + NAMA_LENGKAP + "_NIP_" + NIP + ".pdf";

            strFileNameDoc = strFileNameDoc.Replace(" ", "_");
            strFileNamePDF = strFileNamePDF.Replace(" ", "_");

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
                PS.KanregBKN = KanregBKN;
                PS.PERTEKBKN = PertekBKN;
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
            replaceDict.Add("*DasarPS*", PS.DasarPemberhentian?? "");
            replaceDict.Add("*DasarPertimbanganTeknis*", PS.DasarPemberhentian ?? "");
            replaceDict.Add("*TEMBUSAN_1*", PS.TEMBUSAN1);
            replaceDict.Add("*TEMBUSAN_2*", PS.TEMBUSAN2);
            replaceDict.Add("*TASPEN*", PS.TASPEN);
            replaceDict.Add("*KPPN*", PS.KPPN);
            replaceDict.Add("*KanregBKN*", PS.KanregBKN);
            replaceDict.Add("*Nomor_Pertek*", PS.PERTEKBKN);
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
        public ActionResult SendWaBlas(int ID, int StatusSM)
        {
            try
            {
                clsSuratMasukMsgInfo Msg = new clsSuratMasukMsgInfo();
                string path = Server.MapPath("/Files/Upload/SK/");
                Msg = db.GetMsgInfo(ID, StatusSM);
                if (Msg.PhoneNo != "" || Msg.PhoneNo != null)
                {
                    //Msg.PhoneNo = "082172999095";

                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage resp = client.GetAsync(baseAddress + "api/send-message?source=postman&phone="
                        + WebUtility.UrlEncode(Msg.PhoneNo) + "&message=" + Msg.Pesan
                        + "&token=" + WebUtility.UrlEncode(strToken)).GetAwaiter().GetResult();
                    if (resp.IsSuccessStatusCode)
                    {
                        return Json(null, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        return Json(null, JsonRequestBehavior.AllowGet);
                    }

                }
                return Json(null, JsonRequestBehavior.AllowGet);
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
        //public ActionResult SendWaBlas(int ID)
        //{
        //    try
        //    {
        //        clsSuratMasukMsgInfo Msg = new clsSuratMasukMsgInfo();
        //        int StatusSM = 1;
        //        Msg = db.GetMsgInfo(ID, StatusSM);
        //        if (Msg.PhoneNo != "" || Msg.PhoneNo != null)
        //        {
        //            var client = new HttpClient();
        //            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //            HttpResponseMessage resp = client.GetAsync(baseAddress + "api/send-message?source=postman&phone="
        //                + WebUtility.UrlEncode(Msg.PhoneNo) + "&message=" + WebUtility.UrlEncode(Msg.Pesan)
        //                + "&token=" + WebUtility.UrlEncode(strToken)).GetAwaiter().GetResult();
        //            if (resp.IsSuccessStatusCode)
        //            {
        //                return Json(null, JsonRequestBehavior.AllowGet);

        //            }
        //            else
        //            {
        //                return Json(null, JsonRequestBehavior.AllowGet);
        //            }

        //        }
        //        return Json(null, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult
        //        {
        //            Data = new { ErrorMessage = ex.Message, Success = false },
        //            ContentEncoding = System.Text.Encoding.UTF8,
        //            JsonRequestBehavior = JsonRequestBehavior.DenyGet
        //        };
        //    }
        //}
    }
}