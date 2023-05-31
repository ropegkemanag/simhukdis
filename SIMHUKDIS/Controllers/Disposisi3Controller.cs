using Newtonsoft.Json;
using SIMHUKDIS.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace SIMHUKDIS.Controllers
{
    [SessionExpire]
    public class Disposisi3Controller : Controller
    {
        // GET: Disposisi
        clsDisposisi3DB db = new clsDisposisi3DB();
        string strToken = "vhTYPWC5jyz72sK4VDcjR2re7xPNYnEa516ysMJlpUlKvMgTKNHvdSW9wUDlnTay";
        string baseAddress = "https://kudus.wablas.com/";
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
                string UserID = Session["UserID"].ToString();

                ViewBag.StatusAdmin = StatusAdmin;
                ViewBag.UserID = userlogin;
                ViewBag.SatuanKerja = SatuanKerja;
                ViewBag.UserGroup = UserGroup;

                ViewBag.Satker = new SelectList(db.GetListSatker(), "KODE_SATUAN_KERJA", "SATUAN_KERJA");
                ViewBag.Konseptor = new SelectList(db.GetKonseptorList(), "UserID", "Konseptor");
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
            ViewBag.Konseptor = new SelectList(db.GetKonseptorList(), "UserID", "Konseptor", Disposisi.Konseptor);
            return Json(Disposisi, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Add(clsDisposisi disp)
        {
            string strMsg = "";
            string UserLogin = Session["Fullname"].ToString();
            string UserID = Session["UserID"].ToString();
            try
            {
                disp.UpdateUser = UserID;
                db.Edit(disp);
                SendWaBlas(Convert.ToInt16(disp.ID), Convert.ToInt16(disp.tipe));
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
                string userlogin = Session["Fullname"].ToString();
                string SatuanKerja = Session["Satker"].ToString();
                string StatusAdmin = Session["StatusAdmin"].ToString();
                string UserGroup = Session["UserGroup"].ToString();
                ViewBag.StatusAdmin = StatusAdmin;
                ViewBag.UserID = userlogin;
                ViewBag.SatuanKerja = SatuanKerja;
                ViewBag.UserGroup = UserGroup;
                clsDisposisi UGroup = db.DP.SingleOrDefault(sub => sub.ID == ID);
                clsDisposisi3DB x = new clsDisposisi3DB();
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
            string Msg = "";
            try
            {
                string UserLogin = Session["Fullname"].ToString();
                string UserID = Session["UserID"].ToString();

                clsDisposisi dp = new clsDisposisi();

                if(Catatan == "")
                {
                    Msg = "Catatan tidak boleh Kosong";
                    return Json(Msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    dp.ID = ID;
                    dp.Catatan1 = Catatan;
                    dp.UpdateUser = UserID;
                    dp.StatusDisposisi = StatusDisposisi;
                    clsDisposisi3DB db = new clsDisposisi3DB();
                    db.Edit(dp);
                    SendWaBlas(Convert.ToInt16(dp.ID), Convert.ToInt16(dp.tipe));

                    Msg = "Success";
                    return Json(Msg, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Msg = ex.Message.ToString();
                return Json(Msg, JsonRequestBehavior.AllowGet);
            }
        }
        [NonAction]
        public SelectList ToSelectList(DataTable table, string valueField, string textField)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (DataRow row in table.Rows)
            {
                list.Add(new SelectListItem()
                {
                    Text = row[textField].ToString(),
                    Value = row[valueField].ToString()
                });
            }

            return new SelectList(list, "Value", "Text");
        }
        public FileResult DownloadFile1(string fileName, string tipe)
        {
            try
            {
                string path;
                //Build the File Path.
                if (tipe == "1")
                {
                    path = Server.MapPath("/Files/Upload/Surat Masuk/1.Surat Pengantar/") + fileName;
                }
                else
                {
                    path = Server.MapPath("/Files/Upload/PS/SP/") + fileName;
                }


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
        public FileResult DownloadFile2(string fileName, string tipe)
        {
            try
            {
                string path;
                //Build the File Path.
                if (tipe == "1")
                {
                    path = Server.MapPath("/Files/Upload/Surat Masuk/2.BAP/") + fileName;
                }
                else
                {
                    path = Server.MapPath("/Files/Upload/PS/DokumenPendukung/") + fileName;
                }
                //Build the File Path.

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
        public ActionResult SendWaBlas(int ID, int tipe)
        {
            try
            {
                clsSuratMasukMsgInfo Msg = new clsSuratMasukMsgInfo();
                clsSuratMasukDB r = new clsSuratMasukDB();
                int StatusSM = 4;

                if (tipe == 1)
                {
                    Msg = r.GetMsgInfo(ID, StatusSM);
                }
                else
                {
                    Msg = r.GetPSMsgInfo(ID, StatusSM);
                }

                if (Msg.PhoneNo != "" || Msg.PhoneNo != null)
                {
                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    
                    HttpResponseMessage resp = client.GetAsync(baseAddress + "api/send-message?source=postman&phone=" 
                        + WebUtility.UrlEncode(Msg.PhoneNo) + "&message=" + WebUtility.UrlEncode(Msg.Pesan)
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