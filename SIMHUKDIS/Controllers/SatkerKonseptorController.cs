using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using simhukdis.Models;
using SIMHUKDIS.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace SIMHUKDIS.Controllers
{
    [SessionExpire]
    public class SatkerKonseptorController : Controller
    {
        // GET: SatkerKonseptor
        clsSatkerKonseptorDB db = new clsSatkerKonseptorDB();

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
            ViewBag.UserID = userlogin;
            ViewBag.SatuanKerja = SatuanKerja;
            ViewBag.UserGroup = UserGroup;
            try
            {
                ViewBag.UserID = userlogin;
                List<clsSatkerKonseptor> sm = db.SK.ToList();
                return View(sm);
            }
            catch (Exception)
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            string statusAdmin = Session["StatusAdmin"] + "";
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            ViewBag.Satker = new SelectList(db.GetListSatker(), "KODE_SATUAN_KERJA", "SATUAN_KERJA");
            ViewBag.Konseptor = new SelectList(db.GetKonseptorList(), "UserID", "Fullname");
            //using (MySqlConnection con = new MySqlConnection(constr))
            //{
            //    MySqlDataAdapter da = new MySqlDataAdapter("sp_Konseptor_Sel",con);
            //    DataTable dt = new DataTable();
            //    da.Fill(dt);
            //    ViewBag.Konseptor = ToSelectList(dt, "UserID", "Fullname");

            //    da = new MySqlDataAdapter("sp_Satker_Sel", con);
            //    DataTable dt1 = new DataTable();
            //    da.Fill(dt1);
            //    ViewBag.Satker = ToSelectList(dt1, "KODE_SATUAN_KERJA", "SATUAN_KERJA");
            //}            
            //clsSatkerKonseptor sm = new clsSatkerKonseptor();
            //ViewBag.Satker = new SelectList(db.GetListSatker(), "KODE_SATUAN_KERJA", "SATUAN_KERJA");
            return View();
        }
        [HttpPost]
        public ActionResult Create(string KODE_SATUAN_KERJA, string UserID)
        {
            string strMsg = "";
            try
            {
                String userlogin = Session["Fullname"].ToString();
                clsSatkerKonseptor SK = new clsSatkerKonseptor();
                SK.KODE_SATUAN_KERJA = KODE_SATUAN_KERJA;
                SK.UserID = UserID;
                SK.LastUser = userlogin;
                if (db.GetDataExist(KODE_SATUAN_KERJA, UserID) == true)
                {
                    strMsg = "Satuan Kerja sudah ada";
                    ModelState.AddModelError(string.Empty, strMsg);
                    ViewBag.Message = string.Format(strMsg, strMsg, DateTime.Now.ToString());
                    return Json(strMsg, JsonRequestBehavior.AllowGet);
                }
                db.Insert(SK);
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
        [HttpGet]
        public ActionResult Edit(string KODE_SATUAN_KERJA, string UserID)
        {
            clsSatkerKonseptor SK = db.SK.SingleOrDefault(sub => sub.KODE_SATUAN_KERJA == KODE_SATUAN_KERJA);
            ViewBag.Satker = new SelectList(db.GetListSatker(), "KODE_SATUAN_KERJA", "SATUAN_KERJA", KODE_SATUAN_KERJA);
            ViewBag.Konseptor = new SelectList(db.GetKonseptorList(), "UserID", "Fullname",UserID);
            return View(SK);
        }
        [HttpPost]
        public ActionResult Ubah(string KODE_SATUAN_KERJA, string UserID)
        {
            string strMsg = "";
            try
            {
                String userlogin = Session["Fullname"].ToString();
                clsSatkerKonseptor SK = new clsSatkerKonseptor();
                SK.KODE_SATUAN_KERJA = KODE_SATUAN_KERJA;
                SK.UserID = UserID;
                SK.LastUser = userlogin;

                db.Ubah(SK);
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
        public ActionResult Delete(string Satker)
        {
            string strMsg = "";
            string UserLogin = Session["Fullname"].ToString();
            try
            {
                clsSatkerKonseptorDB db = new clsSatkerKonseptorDB();
                clsSatkerKonseptor SK = new clsSatkerKonseptor();
                SK.KODE_SATUAN_KERJA = Satker;
                db.Delete(SK);
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