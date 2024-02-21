using SIMHUKDIS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace SIMHUKDIS.Controllers.Satker
{
    public class PemberhentianSementaraController : Controller
    {
        // GET: PemberhentianSementara
        clsPemberhentianSementaraDB db = new clsPemberhentianSementaraDB();
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
                List<clsPemberhentianSementara> PS = db.PS(UserID).ToList();
                return View(PS);
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });
            }
        }
        [HttpGet]
        public ActionResult Create()
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

                ViewBag.UserID = userlogin;
                ViewBag.SatuanKerja = SatuanKerja;
                ViewBag.UserGroup = UserGroup;
                ClsSatkerDB x = new ClsSatkerDB();
                ViewBag.SatuanKerjaName = x.GetName(SatuanKerja);
                ViewBag.Pimpinan = x.getPimpinan(SatuanKerja);
                clsSuratMasukDB sm = new clsSuratMasukDB();
                ViewBag.Satker = new SelectList(sm.GetListSatker(), "Kode_Satuan_Kerja", "SATUAN_KERJA", SatuanKerja);
                ViewBag.Unit_Kerja = new SelectList(sm.GetUnit(SatuanKerja), "Kode_Unit_Kerja", "Unit_Kerja");
                return View();
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });
            }
        }
        [HttpPost]
        public ActionResult Create(string NoSurat, string AsalSurat, string TanggalSurat,
            string Perihal, string Kode_Satuan_Kerja, string Kode_Unit_Kerja, HttpPostedFileBase Lampiran_SuratPengantar,
            HttpPostedFileBase Lampiran_DokumenPendukung,
            HttpPostedFileBase Lampiran_SKPemberhentian,
            string UsulStatus)
        {
            try
            {
                if (Session["Fullname"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                else
                {
                    string userlogin = Session["Fullname"].ToString();
                    string SatuanKerja = Session["Satker"].ToString();
                    string StatusAdmin = Session["StatusAdmin"].ToString();
                    string UserGroup = Session["UserGroup"].ToString();
                    string UserID = Session["UserID"].ToString();

                    ViewBag.UserID = userlogin;
                    ViewBag.SatuanKerja = SatuanKerja;
                    ViewBag.UserGroup = UserGroup;

                    string FilePath = "";
                    string FileExt = "";
                    string GetDateTime = "";
                    string Ext = "";
                    string FileNameWithoutExtension = "";
                    string a, b;
                    GetDateTime = DateTime.Now.ToString("ddMMyyyy_hhmmss");
                    string UserLogin = Session["Fullname"].ToString();
                    clsPemberhentianSementara PS = new clsPemberhentianSementara();
                    PS.NoSurat = NoSurat;
                    PS.AsalSurat = AsalSurat;
                    PS.TanggalSurat = TanggalSurat;
                    PS.Perihal = Perihal;
                    PS.Kode_Unit_Kerja = Kode_Unit_Kerja;

                    if (Kode_Satuan_Kerja == null && UserGroup == "03")
                    {
                        PS.Satuan_Kerja = SatuanKerja;
                    }
                    else
                    {
                        PS.Satuan_Kerja = Kode_Satuan_Kerja;
                    }
                    if (Lampiran_SuratPengantar != null)
                    {
                        a = Lampiran_SuratPengantar.FileName;
                        FileExt = Path.GetExtension(a);
                        Ext = Server.MapPath("/Files/Upload/PS/SP/");
                        FileNameWithoutExtension = Path.GetFileNameWithoutExtension(a);
                        FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                        Request.Files[0].SaveAs(FilePath);
                        PS.Lampiran_SuratPengantar = FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                    }
                    else
                    {
                        PS.Lampiran_SuratPengantar = "";
                    }
                    if (Lampiran_DokumenPendukung != null)
                    {
                        b = Lampiran_DokumenPendukung.FileName;
                        FileExt = Path.GetExtension(b);
                        Ext = Server.MapPath("/Files/Upload/PS/DokumenPendukung/");
                        FileNameWithoutExtension = Path.GetFileNameWithoutExtension(b);
                        FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                        Request.Files[1].SaveAs(FilePath);
                        PS.Lampiran_DokumenPendukung = FileNameWithoutExtension + "_" + GetDateTime + FileExt;

                    }
                    else
                    {
                        PS.Lampiran_DokumenPendukung = "";
                    }
                    if (Lampiran_SKPemberhentian != null)
                    {
                        a = Lampiran_SKPemberhentian.FileName;
                        FileExt = Path.GetExtension(a);
                        Ext = Server.MapPath("/Files/Upload/PS/SK_PS/");
                        FileNameWithoutExtension = Path.GetFileNameWithoutExtension(a);
                        FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                        Request.Files[2].SaveAs(FilePath);
                        PS.Lampiran_SKPemberhentian = FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                    }
                    else
                    {
                        PS.Lampiran_SKPemberhentian = "";
                    }
                    PS.Create_User = UserID;
                    PS.UsulStatus = UsulStatus;
                    PS.Status = 0;
                    db.Insert(PS);
                    
                    return RedirectToAction("Index", "PemberhentianSementara");
                }
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });

            }
        }
        [HttpGet]
        public ActionResult Edit(int ID, string Kode_Satuan_Kerja, string Kode_Unit_Kerja, string UsulStatus)
        {            

            if (Session["Fullname"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            try
            {
                clsSuratMasukDB sm = new clsSuratMasukDB();
                clsPemberhentianSementara PS = db.GetList(ID);

                string SatuanKerja = Session["Satker"].ToString();
                string StatusAdmin = Session["StatusAdmin"].ToString();
                string UserGroup = Session["UserGroup"].ToString();
                string userlogin = Session["Fullname"].ToString();
                string UserID = Session["UserID"].ToString();

                ViewBag.UserID = userlogin;
                ViewBag.SatuanKerja = SatuanKerja;
                ViewBag.UserGroup = UserGroup;
                ViewBag.TanggalSurat = PS.TanggalSurat;
                ViewBag.UsulStatus = UsulStatus;


                ViewBag.Satker = new SelectList(sm.GetListSatker(), "Kode_Satuan_Kerja", "SATUAN_KERJA", PS.Kode_Satuan_Kerja);
                ViewBag.Unit_Kerja = new SelectList(sm.GetUnit(Kode_Satuan_Kerja), "Kode_Unit_Kerja", "Unit_Kerja", PS.Kode_Unit_Kerja);

                return View(PS);
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });
            }
        }
        [HttpPost]
        public ActionResult Edit(int ID, string NoSurat, string AsalSurat, string TanggalSurat,
            string perihal, string Kode_Satuan_Kerja, string Kode_Unit_Kerja, HttpPostedFileBase Lampiran_SuratPengantar,
            HttpPostedFileBase Lampiran_DokumenPendukung,
            HttpPostedFileBase Lampiran_SKPemberhentian,
            string UsulStatus)
        {
            string FilePath = "";
            string FileExt = "";
            string GetDateTime = "";
            string Ext = "";
            string FileNameWithoutExtension = "";
            string a, b;

            if (Session["Fullname"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            try
            {
                string SatuanKerja = Session["Satker"].ToString();
                string StatusAdmin = Session["StatusAdmin"].ToString();
                string UserGroup = Session["UserGroup"].ToString();
                string userlogin = Session["Fullname"].ToString();
                string UserID = Session["UserID"].ToString();

                GetDateTime = DateTime.Now.ToString("ddMMyyyy_hhmmss");
                string UserLogin = Session["Fullname"].ToString();
                clsPemberhentianSementara PS = new clsPemberhentianSementara();
                PS.ID = ID;
                PS.NoSurat = NoSurat;
                PS.AsalSurat = AsalSurat;
                PS.TanggalSurat = TanggalSurat;
                PS.Perihal = perihal;
                PS.Kode_Satuan_Kerja = Kode_Satuan_Kerja;
                PS.Kode_Unit_Kerja = Kode_Unit_Kerja;
                PS.UsulStatus = UsulStatus;
                clsPemberhentianSementara xx = db.GetList(ID);
                string FullPath = "";
                if (Lampiran_SuratPengantar != null)
                {
                    a = Lampiran_SuratPengantar.FileName;
                    FileExt = Path.GetExtension(a);
                    Ext = Server.MapPath("/Files/Upload/PS/SP/");
                    FileNameWithoutExtension = Path.GetFileNameWithoutExtension(a);
                    FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                    Request.Files[0].SaveAs(FilePath);
                    PS.Lampiran_SuratPengantar = FileNameWithoutExtension + "_" + GetDateTime + FileExt;

                    FullPath = Ext + xx.Lampiran_SuratPengantar;
                    if (System.IO.File.Exists(FullPath))
                    {
                        System.IO.File.Delete(FullPath);
                    }
                }
                else
                {
                    PS.Lampiran_SuratPengantar = "";
                }
                if (Lampiran_DokumenPendukung != null)
                {
                    b = Lampiran_DokumenPendukung.FileName;
                    FileExt = Path.GetExtension(b);
                    Ext = Server.MapPath("/Files/Upload/PS/DokumenPendukung/");
                    FileNameWithoutExtension = Path.GetFileNameWithoutExtension(b);
                    FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                    Request.Files[0].SaveAs(FilePath);
                    PS.Lampiran_DokumenPendukung = FileNameWithoutExtension + "_" + GetDateTime + FileExt;

                    FullPath = Ext + xx.Lampiran_DokumenPendukung;
                    if (System.IO.File.Exists(FullPath))
                    {
                        System.IO.File.Delete(FullPath);
                    }
                }
                else
                {
                    PS.Lampiran_DokumenPendukung = "";
                }
                if (Lampiran_SKPemberhentian != null)
                {
                    b = Lampiran_SKPemberhentian.FileName;
                    FileExt = Path.GetExtension(b);
                    Ext = Server.MapPath("/Files/Upload/PS/SK_PS/");
                    FileNameWithoutExtension = Path.GetFileNameWithoutExtension(b);
                    FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                    Request.Files[2].SaveAs(FilePath);
                    PS.Lampiran_SKPemberhentian = FileNameWithoutExtension + "_" + GetDateTime + FileExt;

                    FullPath = Ext + xx.Lampiran_SKPemberhentian;
                    if (System.IO.File.Exists(FullPath))
                    {
                        System.IO.File.Delete(FullPath);
                    }
                }
                else
                {
                    PS.Lampiran_SKPemberhentian = "";
                }
                PS.Update_User = UserID;
                db.Update(PS);
                return RedirectToAction("Index", "PemberhentianSementara");
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });

            }
        }
        [HttpPost]
        public ActionResult Delete(int ID, string Status, string TanggalSuratFrom, string TanggalSuratTo)
        {
            string strMsg = "";
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

                ViewBag.UserID = userlogin;
                ViewBag.SatuanKerja = SatuanKerja;
                ViewBag.UserGroup = UserGroup;

                db.Delete(ID);
                strMsg = "Success";
                return Json(strMsg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                strMsg = ex.Message.ToString();
                return Json(strMsg, JsonRequestBehavior.AllowGet);
                //return View();
            }
        }
        public ActionResult Proses(int ID)
        {
            string strMsg = "";
            if (Session["Fullname"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            string userlogin = Session["Fullname"].ToString();
            string SatuanKerja = Session["Satker"].ToString();
            string StatusAdmin = Session["StatusAdmin"].ToString();
            string UserGroup = Session["UserGroup"].ToString();
            string UserID = Session["UserID"].ToString();

            ViewBag.UserID = userlogin;
            ViewBag.SatuanKerja = SatuanKerja;
            ViewBag.UserGroup = UserGroup;
            try
            {
                clsPemberhentianSementara a = new clsPemberhentianSementara();
                a.ID = ID;
                a.Update_User = UserID;
                db.Proses(a);
                SendWaBlas(ID);

                strMsg = "Success";
                return Json(strMsg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                strMsg = ex.Message.ToString();
                return Json(strMsg, JsonRequestBehavior.AllowGet);
                //return View();
            }
        }
        public FileResult DownloadFile1(string fileName)
        {
            try
            {
                //Build the File Path.
                string path = Server.MapPath("/Files/Upload/PS/SP/") + fileName;

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
        public FileResult DownloadFile2(string fileName)
        {
            try
            {
                //Build the File Path.
                string path = Server.MapPath("/Files/Upload/PS/DokumenPendukung/") + fileName;

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
        public ActionResult SendWaBlas(int ID)
        {
            try
            {
                clsSuratMasukMsgInfo Msg = new clsSuratMasukMsgInfo();
                clsSuratMasukDB db = new clsSuratMasukDB();
                int StatusSM = 11;
                Msg = db.GetMsgInfo(ID, StatusSM);
                if (Msg.PhoneNo != "" || Msg.PhoneNo != null)
                {
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
    }
}