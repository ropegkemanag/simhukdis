using simhukdis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace simhukdis.Controllers
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
        [HttpGet]
        public ActionResult Create()
        {
            if (Session["Fullname"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
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
                strMsg = ex.Message.ToString();
                return Json(strMsg, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Edit(int ID)
        {
            if (Session["Fullname"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            clsHukdisDB db = new clsHukdisDB();
            clsHukdis UHukdis = db.HukdisList.SingleOrDefault(sub => sub.ID == ID);
            return View(UHukdis);
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
                strMsg = ex.Message.ToString();
                return Json(strMsg, JsonRequestBehavior.AllowGet);
            }
        }
        
        //[HttpGet]
        //public ActionResult Delete(int ID)
        //{
        //    clsHukdisDB db = new clsHukdisDB();
        //    clsHukdis UHukdis = db.HukdisList.SingleOrDefault(sub => sub.ID == ID);
        //    return View(UHukdis);
        //}

        [HttpPost]
        //[ActionName("Delete")]
        //[ValidateAntiForgeryToken]
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
                strMsg = ex.Message.ToString();
                ModelState.AddModelError(string.Empty, strMsg);
                ViewBag.Message = string.Format(strMsg, strMsg, DateTime.Now.ToString());
                return Json(strMsg, JsonRequestBehavior.AllowGet);
            }
        }
    }
}