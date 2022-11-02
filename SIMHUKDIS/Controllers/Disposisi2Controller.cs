using Newtonsoft.Json;
using SIMHUKDIS.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;


namespace SIMHUKDIS.Controllers
{
    [SessionExpire]
    public class Disposisi2Controller : Controller
    {
        // GET: Disposisi
        string strMsg = "";
        string UserLogin = "";
        string SatuanKerja = "";
        string StatusAdmin = "";
        string UserGroup = "";
        clsDisposisi2DB db = new clsDisposisi2DB();
        public ActionResult Index()
        {
            if (Session["Fullname"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            try
            {
                UserLogin = Session["Fullname"].ToString();
                SatuanKerja = Session["Satker"].ToString();
                StatusAdmin = Session["StatusAdmin"].ToString();
                UserGroup = Session["UserGroup"].ToString();

                ViewBag.StatusAdmin = StatusAdmin;
                ViewBag.UserID = UserLogin;
                ViewBag.SatuanKerja = SatuanKerja;
                ViewBag.UserGroup = UserGroup;

                List<clsDisposisi> sm = db.DP.ToList();
                return View(sm);
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });
                //return RedirectToAction("Error500", "Home");
            }
        }
        public JsonResult List()
        {
            try
            {
                return Json(db.ListAll(), JsonRequestBehavior.AllowGet);
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
        public JsonResult GetbyID(string ID)
        {
            try
            {
                var Disposisi = db.ListAll().Find(x => x.ID.Equals(ID));
                return Json(Disposisi, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                strMsg = ex.Message.ToString();
                return Json(strMsg, JsonRequestBehavior.AllowGet);
            }
            
        }
        public JsonResult Add(clsDisposisi disp)
        {
            string UserLogin = Session["Fullname"].ToString();
            try
            {
                disp.UpdateUser = UserLogin;
                db.Edit(disp);
                strMsg = "Success";
                return Json(strMsg, JsonRequestBehavior.AllowGet);
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
        [HttpGet]
        public ActionResult Proses(string ID)
        {
            if (Session["Fullname"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            try
            {
                clsDisposisi UGroup = db.DP.SingleOrDefault(sub => sub.ID == ID);
                clsDisposisi2DB x = new clsDisposisi2DB();
                ViewBag.Konseptor = new SelectList(x.GetKonseptorList(), "Konseptor", "Konseptor", UGroup.Konseptor);
                return View(UGroup);
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });
                //return RedirectToAction("Error500", "Home");
            }
        }

        [HttpGet]
        public ActionResult Details(int ID)
        {
            if (Session["Fullname"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            try
            {
                UserLogin = Session["Fullname"].ToString();
                SatuanKerja = Session["Satker"].ToString();
                StatusAdmin = Session["StatusAdmin"].ToString();
                UserGroup = Session["UserGroup"].ToString();

                ViewBag.StatusAdmin = StatusAdmin;
                ViewBag.UserID = UserLogin;
                ViewBag.SatuanKerja = SatuanKerja;
                ViewBag.UserGroup = UserGroup;
                string statusAdmin = Session["StatusAdmin"] + "";
                clsSuratMasukDB db = new clsSuratMasukDB();
                clsSuratMasuk sm = db.GetList(ID);
                return View(sm);
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });
                //return RedirectToAction("Error500", "Home");
            }
        }
        public ActionResult Proses(string ID, string StatusDisposisi, string Catatan)
        {
            if (Session["Fullname"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            try
            {
                UserLogin = Session["Fullname"].ToString();
                SatuanKerja = Session["Satker"].ToString();
                StatusAdmin = Session["StatusAdmin"].ToString();
                UserGroup = Session["UserGroup"].ToString();

                ViewBag.StatusAdmin = StatusAdmin;
                ViewBag.UserID = UserLogin;
                ViewBag.SatuanKerja = SatuanKerja;
                ViewBag.UserGroup = UserGroup;

                clsDisposisi dp = new clsDisposisi();
                dp.ID = ID;
                dp.Catatan1 = Catatan;
                dp.UpdateUser = UserLogin;
                dp.StatusDisposisi = StatusDisposisi;

                clsDisposisi2DB db = new clsDisposisi2DB();
                db.Edit(dp);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                string strMsg = ex.Message.ToString();
                ModelState.AddModelError(string.Empty, strMsg);
                ViewBag.Message = string.Format(strMsg, strMsg, DateTime.Now.ToString());
                return View();
            }
        }
        public FileResult DownloadFile1(string fileName)
        {
            try
            {
                //Build the File Path.
                string path = Server.MapPath("/Files/Upload/Surat Masuk/1.Surat Pengantar/") + fileName;

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
                string path = Server.MapPath("/Files/Upload/Surat Masuk/2.BAP/") + fileName;

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
        public FileResult DownloadFile3(string fileName)
        {
            try
            {
                //Build the File Path.
                string path = Server.MapPath("/Files/Upload/Surat Masuk/3.Surat Tugas Pemeriksaan/") + fileName;

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
        public FileResult DownloadFile4(string fileName)
        {
            try
            {
                //Build the File Path.
                string path = Server.MapPath("/Files/Upload/Surat Masuk/4.Surat Panggilan/") + fileName;

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
        public FileResult DownloadFile5(string fileName)
        {
            try
            {
                //Build the File Path.
                string path = Server.MapPath("/Files/Upload/Surat Masuk/5.Dokumen Lainnya/") + fileName;

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
        public FileResult DownloadFile6(string fileName)
        {
            try
            {
                //Build the File Path.
                string path = Server.MapPath("/Files/Upload/Surat Masuk/6.Surat Putusan Pengadilan/") + fileName;

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
        public FileResult DownloadFile7(string fileName)
        {
            try
            {
                //Build the File Path.
                string path = Server.MapPath("/Files/Upload/Surat Masuk/7.LHA/") + fileName;

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