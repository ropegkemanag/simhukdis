using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using simhukdis.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
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
                string statusAdmin = Session["StatusAdmin"] + "";
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
                    dp.UpdateUser = UserLogin;
                    dp.StatusDisposisi = StatusDisposisi;
                    clsDisposisi3DB db = new clsDisposisi3DB();
                    db.Edit(dp);
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
    }
}