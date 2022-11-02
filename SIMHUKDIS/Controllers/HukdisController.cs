using SIMHUKDIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIMHUKDIS.Controllers
{
    [SessionExpire]
    public class HukdisController : Controller
    {
        // GET: Hukdis
        clsHukdisDB db = new clsHukdisDB();
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
                List<clsHukdis> sm = db.HukdisList.ToList();
                return View(sm);
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
                ViewBag.UserID = userlogin;
                ViewBag.SatuanKerja = SatuanKerja;
                ViewBag.UserGroup = UserGroup;
                return View();
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });
            }           
        }

        [HttpPost]
        public ActionResult Create(string HukdisID, string HukdisDesc, string Tingkat)
        {
            string strMsg = "";
            string UserLogin = "";
            try
            {
                UserLogin = Session["Fullname"].ToString();
                clsHukdis UHukdis = new clsHukdis();
                UHukdis.HukdisID = HukdisID;
                UHukdis.HukdisDesc = HukdisDesc;
                UHukdis.Tingkat = Tingkat;
                UHukdis.LastUser = UserLogin;

                if (db.GetDataExist(HukdisID) == true)
                {
                    strMsg = "User Group sudah ada !";
                    ModelState.AddModelError(string.Empty, strMsg);
                    ViewBag.Message = string.Format(strMsg, strMsg, DateTime.Now.ToString());
                    return Json(strMsg, JsonRequestBehavior.AllowGet);
                }
                db.Insert(UHukdis);
                strMsg = "Success";
                return Json(strMsg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });
            }
        }

        [HttpGet]
        public ActionResult Edit(int ID)
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
                clsHukdisDB db = new clsHukdisDB();
                clsHukdis UHukdis = db.HukdisList.SingleOrDefault(sub => sub.ID == ID);
                return View(UHukdis);
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });
            }
        }

        [HttpPost]
        public ActionResult Ubah(int ID, string HukdisID, string HukdisDesc, string Tingkat)
        {
            string strMsg = "";
            string UserLogin = "";
            
            try
            {
                UserLogin = Session["Fullname"].ToString();
                clsHukdis UHukdis = new clsHukdis();
                UHukdis.ID = ID;
                UHukdis.HukdisID = HukdisID;
                UHukdis.HukdisDesc = HukdisDesc;
                UHukdis.Tingkat = Tingkat;
                UHukdis.LastUser = UserLogin;

                db.Edit(UHukdis);
                strMsg = "Success";
                return Json(strMsg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });
            }
        }

        [HttpPost]
        public ActionResult Delete(string ID)
        {
            string strMsg = "";
            string UserLogin = Session["Fullname"].ToString();
            try
            {
                clsHukdisDB db = new clsHukdisDB();
                db.Delete(ID);
                strMsg = "Success";
                return Json(strMsg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });
            }
        }
    }
}