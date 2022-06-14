using simhukdis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace simhukdis.Controllers
{
    public class HomeController : Controller
    {
        clsUserLoginDB UserLogin = new clsUserLoginDB();
        public ActionResult Index()
        {
            string userlogin = Session["Fullname"].ToString();
            string SatuanKerja = Session["Satker"].ToString();
            string StatusAdmin = Session["StatusAdmin"].ToString();
            string UserGroup = Session["UserGroup"].ToString();
            ViewBag.StatusAdmin = StatusAdmin;
            ViewBag.UserID = userlogin;
            ViewBag.SatuanKerja = SatuanKerja;
            ViewBag.UserGroup = UserGroup;
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(clsUserLogin User)
        {
            string message = "";
            try
            {
                if (User.NIP == "")
                {
                    message = "NIP is empty!";
                }
                if (User.Password == "")
                {
                    message = "Password is empty!";
                }

                if (UserLogin.CheckUserLogin(User) > 0)
                {
                    message = "Success";
                    Session["LogUserID"] = User.NIP.ToString();
                    
                    clsUserLoginDB udb = new clsUserLoginDB();
                    udb.UpdateLastLogin(User.NIP);
                    clsUserLogin cUser = udb.GetData(User.NIP.ToString());
                    Session["StatusAdmin"] = cUser.StatusAdmin.ToString();
                    Session["UserID"] = cUser.UserID.ToString();
                    Session["Fullname"] = cUser.FullName.ToString();
                    Session["Satker"] = cUser.Satker.ToString();
                    Session["UserGroup"] = cUser.GroupID.ToString();
                    ViewBag.Fullname = cUser.FullName.ToString();
                }
                else
                {
                    message = "NIP or Password is Incorrect!";
                }

                //if (message != "Success")
                //{
                //    ViewBag.Message = message;
                //    return View(User);
                //}
                //else
                //{
                //    return RedirectToAction("Index", "Home");
                //}
            }
            catch (Exception ex)
            {
                message = ex.Message.ToString().Trim();
                return View(User);
            }
            ViewBag.Message = message;
            return Json(message, JsonRequestBehavior.AllowGet);

        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public ActionResult Error500(string Error_Message)
        {
            ViewBag.Error_Message = Error_Message;
            return View();
        }
    }
}