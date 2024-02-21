using SIMHUKDIS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace SIMHUKDIS.Controllers.Konseptor
{
    
    [SessionExpire]
    public class SKHukdisTanpaSidangController : Controller
    {
        string strToken = "oaRYeMTcOSI4SM81dsSaos6oSPIltIwxJhybwi2Zd5d26RdmqGghELJQgnDn32K1";
        string baseAddress = "https://kudus.wablas.com/";
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
        public ActionResult SendWaBlas(int ID, string NIP,int tipe)
        {
            try
            {
                clsSuratMasukMsgInfo Msg = new clsSuratMasukMsgInfo();
                clsSuratMasukDB r = new clsSuratMasukDB();
                Msg = db.GetMsgInfo(ID, tipe, NIP);
                if (Msg.PhoneNo != "" || Msg.PhoneNo != null)
                {
                    //Msg.PhoneNo = "082172999095";
                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage resp = client.GetAsync(baseAddress + "api/send-message?source=postman&phone="
                        + WebUtility.UrlEncode(Msg.PhoneNo) + "&message=" + Msg.Pesan
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
        public ActionResult SendtoMenag(string ID, string NIP, string NO_SK, string Tanggal_SK)
        {
            string strMsg = "";
            try
            {
                string UserLogin = Session["Fullname"].ToString();
                clsSKHukdisTanpaSidang PD = new clsSKHukdisTanpaSidang();
                PD.ID = ID;
                PD.NIP = NIP;
                PD.NO_SK = NO_SK;
                PD.Tanggal_SK = Tanggal_SK;
                db.Update(PD);
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
        public ActionResult UploadFiles()
        {
            if (Session["Fullname"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            string userlogin = Session["Fullname"].ToString();
            string UserID = Session["UserID"].ToString();
            string SatuanKerja = Session["Satker"].ToString();
            string StatusAdmin = Session["StatusAdmin"].ToString();
            string UserGroup = Session["UserGroup"].ToString();
            ViewBag.UserID = userlogin;
            ViewBag.SatuanKerja = SatuanKerja;
            ViewBag.UserGroup = UserGroup;
            ViewBag.UserID = userlogin;

            if (Request.Files.Count > 0)
            {
                try
                {
                    string FilePath = "";
                    string FileExt = "";
                    string GetDateTime = "";
                    string Ext = "";
                    string FileNameWithoutExtension = "";
                    string fname, FileName = "";
                    GetDateTime = DateTime.Now.ToString("ddMMyyyy_hhmmss");
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";
                        string filename = Path.GetFileName(Request.Files[i].FileName);

                        HttpPostedFileBase file = files[i];

                        fname = file.FileName;
                        FileExt = Path.GetExtension(fname);
                        FileNameWithoutExtension = Path.GetFileNameWithoutExtension(fname);
                        FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                        fname = FileNameWithoutExtension + "_" + GetDateTime + FileExt;

                        FileName = fname.Replace(" ","_");
                        fname = Path.Combine(Server.MapPath("/Files/Upload/SK/"), FileName);
                        file.SaveAs(fname);
                    }

                    return Json(FileName, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public ActionResult Selesai(string ID, string FileSK, string NIP, string NO_SK, string Tanggal_SK)
        {
            if (Session["Fullname"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            string strMsg;
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
                string GetDateTime = "";
                GetDateTime = DateTime.Now.ToString("ddMMyyyy_hhmmss");

                clsSKHukdisTanpaSidang PS = new clsSKHukdisTanpaSidang();
                string UserLogin = Session["Fullname"].ToString();
                PS.Create_User = UserID;
                PS.ID = ID;
                PS.NIP = NIP;
                PS.NO_SK = NO_SK;
                PS.Tanggal_SK = Tanggal_SK;
                if (FileSK == null)
                {
                    PS.FILE_SK = "";
                }
                else
                {
                    PS.FILE_SK = FileSK;
                }

                clsSKHukdisTanpaSidangDB db = new clsSKHukdisTanpaSidangDB();
                //db.Update(telaah);
                db.Selesai(PS);

                SendWaBlas(Convert.ToInt16(ID),NIP,0);
                SendWaBlas(Convert.ToInt16(ID),NIP,1);
                SendWaBlas(Convert.ToInt16(ID),NIP,2);
                SendWaBlas(Convert.ToInt16(ID),NIP,3);
                SendWaBlas(Convert.ToInt16(ID),NIP,4);
                SendWaBlas(Convert.ToInt16(ID),NIP,5);

                strMsg = "Success";
                return Json(strMsg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                strMsg = Error_Message;
                return Json(strMsg, JsonRequestBehavior.AllowGet);
            }
        }

    }
}