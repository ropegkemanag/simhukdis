using SIMHUKDIS.Models;
using SIMHUKDIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIMHUKDIS.Controllers
{
    [SessionExpire]
    public class PejabatMstController : Controller
    {
        // GET: PejabatMst
        string strMsg = "";
        string UserLogin = "";
        string SatuanKerja = "";
        string StatusAdmin = "";
        string UserGroup = "";
        clsPejabatMstDB db = new clsPejabatMstDB();
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

                clsPejabatMst p = db.GetList();

                ViewBag.karopeg = new SelectList(db.GetKaropegList(), "userid", "nama");
                ViewBag.koordinator = new SelectList(db.GetKoordinatorList(), "userid", "nama");
                ViewBag.subkoordinator = new SelectList(db.GeSubKoordinatorList(), "userid", "nama");
                return View(p);
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });
            }           
        }
        public ActionResult InsertUpdate(string menag, string sekjend, string karopeg, string koordinator, string subkoordinator)
        {
            string strMsg = "";
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
                clsPejabatMst p = new clsPejabatMst();
                p.Menag = menag;
                p.Sekjend = sekjend;
                p.Karopeg = karopeg;
                p.Koordinator = koordinator;
                p.CreateUser = UserLogin;
                p.SubKoordinator = subkoordinator;
                if (db.GetDataExist() == true)
                {
                    db.Update(p);
                }
                else
                {
                    db.Insert(p);
                }
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