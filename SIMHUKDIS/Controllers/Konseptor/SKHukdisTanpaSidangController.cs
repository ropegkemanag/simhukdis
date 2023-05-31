using SIMHUKDIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIMHUKDIS.Controllers.Konseptor
{
    [SessionExpire]
    public class SKHukdisTanpaSidangController : Controller
    {
        clsSKHukdisTanpaSidangDB db = new clsSKHukdisTanpaSidangDB();
        public ActionResult Index()
        {            
            try
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

                ViewBag.StatusAdmin = StatusAdmin;
                ViewBag.UserID = userlogin;
                ViewBag.SatuanKerja = SatuanKerja;
                ViewBag.UserGroup = UserGroup;
                List<clsHasilSidang> PD = db.ListTanpaSidang(UserID).ToList();
                return View(PD);
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });
            }
        }
        [HttpGet]
        public ActionResult BuatSK()
        {
            if (Session["Fullname"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
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

                clsHukdisDB x = new clsHukdisDB();
                clsHasilSidangDB y = new clsHasilSidangDB();
                ViewBag.Hukdis = new SelectList(x.GetListHukdis(), "ID", "HukdisDesc");
                ViewBag.SuratNo = new SelectList(y.GetSuratNo(UserID), "ID", "NoSurat");
                return View();
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });
            }
        }
        public ActionResult Delete(int ID, string NIP)
        {
            string strMsg = "";
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

                clsSKHukdisTanpaSidangDB db = new clsSKHukdisTanpaSidangDB();
                db.Delete(ID, NIP);
                strMsg = "Success";
                return Json(strMsg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                strMsg = ex.Message.ToString();
                ModelState.AddModelError(string.Empty, strMsg);
                ViewBag.Message = string.Format(strMsg, strMsg, DateTime.Now.ToString());
                return Json(strMsg, JsonRequestBehavior.AllowGet);
                //return View();
            }
            //return RedirectToAction("Index");
        }
        public ActionResult Edit(int ID, string NIP)
        {
            if (Session["Fullname"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
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

                clsHukdisDB x = new clsHukdisDB();
                clsHasilSidangDB y = new clsHasilSidangDB();
                
                clsHasilSidang PD = db.ListTanpaSidangByID(UserID, ID, NIP);
                DateTime TMTDate = DateTime.ParseExact(PD.TMTDate, "dd-MM-yyyy", null);
                PD.TMTDate = TMTDate.ToString("dd MMMM yyyy");
                string Hukdis = PD.KeputusanSidang;
                string NoSurat = PD.ID;
                ViewBag.Hukdis = new SelectList(x.GetListHukdis(), "ID", "HukdisDesc", Hukdis);
                //List<clsHasilSidang> PD = db.ListTanpaSidangByID(UserID, ID, NIP).ToList();
                return View(PD);
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });
            }
            
        }
        public JsonResult GetDataPegawai(string NIP)
        {
            try
            {
                clsPegawaiDB b = new clsPegawaiDB();
                var m = b.ListPegawai(NIP);
                return Json(m, JsonRequestBehavior.AllowGet);
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