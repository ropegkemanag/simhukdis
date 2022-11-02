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
    public class DisposisiController : Controller
    {
        // GET: Disposisi
        clsDisposisiDB db = new clsDisposisiDB();
        public ActionResult Index()
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
                ViewBag.StatusAdmin = StatusAdmin;
                ViewBag.UserID = userlogin;
                ViewBag.SatuanKerja = SatuanKerja;
                ViewBag.UserGroup = UserGroup;

                List<clsDisposisi> sm = db.DP.ToList();
                return View(sm);
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });
            }      
        }

        public JsonResult List()
        {
            return Json(db.ListAll(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetbyID(string ID)
        {
            var Disposisi = db.ListAll().Find(x => x.ID.Equals(ID));
            return Json(Disposisi, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Add(clsDisposisi disp)
        {
            string strMsg = "";
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
                strMsg = ex.Message.ToString();
                return Json(strMsg, JsonRequestBehavior.AllowGet);
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
                clsDisposisiDB x = new clsDisposisiDB();
                ViewBag.Konseptor = new SelectList(x.GetKonseptorList(), "Konseptor", "Konseptor", UGroup.Konseptor);
                return View(UGroup);
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });
            }
            
        }
        [HttpGet]
        public ActionResult Details(int ID)
        {
            if (Session["Fullname"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            string userlogin = Session["Fullname"].ToString();
            string SatuanKerja = Session["Satker"].ToString();
            string StatusAdmin = Session["StatusAdmin"].ToString();
            string UserGroup = Session["UserGroup"].ToString();
            ViewBag.UserID = userlogin;
            ViewBag.SatuanKerja = SatuanKerja;
            ViewBag.UserGroup = UserGroup;
            try
            {
                clsSuratMasukDB db = new clsSuratMasukDB();
                clsSuratMasuk sm = db.GetList(ID);
                return View(sm);
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });
            }            
        }
        public ActionResult Proses(string ID, string StatusDisposisi, string Catatan)
        {
            if (Session["Fullname"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            string userlogin = Session["Fullname"].ToString();
            string SatuanKerja = Session["Satker"].ToString();
            string StatusAdmin = Session["StatusAdmin"].ToString();
            string UserGroup = Session["UserGroup"].ToString();
            ViewBag.UserID = userlogin;
            ViewBag.SatuanKerja = SatuanKerja;
            ViewBag.UserGroup = UserGroup;
            try
            {
                clsDisposisi dp = new clsDisposisi();
                dp.ID = ID;
                dp.Catatan1 = Catatan;
                dp.UpdateUser = userlogin;
                dp.StatusDisposisi = StatusDisposisi;
                clsDisposisiDB db = new clsDisposisiDB();
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