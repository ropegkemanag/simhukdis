using simhukdis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace simhukdis.Controllers
{
    [SessionExpire]
    public class UserGroupController : Controller
    {
        // GET: UserGroup
        clsUserGroupDB db = new clsUserGroupDB();
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
            List<clsUserGroup> sm = db.UserGroupList.ToList();
            return View(sm);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(string GroupID, string GroupDesc)
        {            
            try
            {
                string UserLogin = Session["Fullname"].ToString();
                string strMsg = "";
                clsUserGroup UGroup = new clsUserGroup();
                UGroup.GroupID = GroupID;
                UGroup.GroupDesc = GroupDesc;
                UGroup.LastUser = UserLogin;

                if (db.GetDataExist(GroupDesc) == true)
                {
                    strMsg = "User Group sudah ada !";
                    ModelState.AddModelError(string.Empty, strMsg);
                    ViewBag.Message = string.Format(strMsg, strMsg, DateTime.Now.ToString());
                    return Json(strMsg, JsonRequestBehavior.AllowGet);
                    //return View();
                }
                db.Insert(UGroup);
                strMsg = "Success";
                return Json(strMsg, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                string strMsg = ex.Message.ToString();
                ModelState.AddModelError(string.Empty, strMsg);
                ViewBag.Message = string.Format(strMsg, strMsg, DateTime.Now.ToString());
                return Json(strMsg, JsonRequestBehavior.AllowGet);
                //return View();
            }
        }
        [HttpGet]
        public ActionResult Edit(string GroupID)
        {
            clsUserGroupDB db = new clsUserGroupDB();
            clsUserGroup UGroup = db.UserGroupList.SingleOrDefault(sub => sub.GroupID == GroupID);
            return View(UGroup);
        }

        [HttpPost]
        public ActionResult Edit(string GroupID, string GroupDesc)
        {
            string strMsg = "";
            string UserLogin = Session["Fullname"].ToString();
            try
            {                
                clsUserGroup UGroup = new clsUserGroup();
                UGroup.GroupID = GroupID;
                UGroup.GroupDesc = GroupDesc;
                UGroup.LastUser = UserLogin;
                db.Edit(UGroup);
                strMsg = "Success";
                return Json(strMsg, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Index");
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
        
        //[HttpGet]
        //public ActionResult Delete(string GroupID)
        //{
        //    clsUserGroupDB db = new clsUserGroupDB();
        //    clsUserGroup UGroup = db.UserGroupList.SingleOrDefault(sub => sub.GroupID == GroupID);
        //    return View(UGroup);
        //}

        [HttpPost]
        public ActionResult Delete(string GroupID)
        {
            string strMsg = "";
            string UserLogin = Session["Fullname"].ToString();
            try { 
                db.Delete(GroupID);
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
    }
}