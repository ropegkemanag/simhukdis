using Newtonsoft.Json;
using simhukdis.Models;
using SIMHUKDIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace SIMHUKDIS.Controllers
{
    [SessionExpire]
    public class SatkerController : Controller
    {
        private static string Username = string.Empty;
        private static string Password = string.Empty;
        private static string baseAddress = "https://ropeg.kemenag.go.id/api/v1/";
        clsDataAPI db = new clsDataAPI();

        // GET: DataAPI
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
                //Token token = null;
                //Username = "agus@kemenag.go.id";
                //Password = "12345678";
                //token = GetAccessToken(Username, Password);
                //GetMaster(token.token);
                List<Datum> sm = db.ListAll();
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
            if (Session["Fullname"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Create(string SATUAN_KERJA)
        {
            string strMsg = "";
            string UserLogin = "";
            try
            {
                UserLogin = Session["Fullname"].ToString();
                ClsSatker Satker = new ClsSatker();
                Satker.SATUAN_KERJA = SATUAN_KERJA;
                Satker.UserLogin = UserLogin;
                ClsSatkerDB db = new ClsSatkerDB();
                if (db.GetDataExist(SATUAN_KERJA) == true)
                {
                    strMsg = "Satuan Kerja sudah ada !";
                    return Json(strMsg, JsonRequestBehavior.AllowGet);
                }
                db.Insert(Satker);
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
        public ActionResult Edit(string ID)
        {
            if (Session["Fullname"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ClsSatkerDB db = new ClsSatkerDB();
            ClsSatker Satker = db.SatkerList.SingleOrDefault(sub => sub.KODE_SATUAN_KERJA == ID);
            return View(Satker);
        }

        [HttpPost]
        public ActionResult Ubah(string KODE_SATUAN_KERJA, string SATUAN_KERJA)
        {
            string strMsg = "";
            string UserLogin = "";

            try
            {
                UserLogin = Session["Fullname"].ToString();
                ClsSatker Satker = new ClsSatker();
                Satker.KODE_SATUAN_KERJA = KODE_SATUAN_KERJA;
                Satker.SATUAN_KERJA = SATUAN_KERJA;
                Satker.UserLogin = UserLogin;
                ClsSatkerDB db = new ClsSatkerDB();
                db.Edit(Satker);
                strMsg = "Success";
                return Json(strMsg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                strMsg = ex.Message.ToString();
                return Json(strMsg, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Delete(string ID)
        {
            string strMsg = "";
            string UserLogin = Session["Fullname"].ToString();
            try
            {
                ClsSatkerDB db = new ClsSatkerDB();
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
        public static Token GetAccessToken(string username, string password)
        {
            Token token = new Token();
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            var RequestBody = new Dictionary<string, string>
                {
                {"email", username},
                {"password", password},
                };
            var tokenResponse = client.PostAsync(baseAddress + "auth/login", new FormUrlEncodedContent(RequestBody)).Result;

            if (tokenResponse.IsSuccessStatusCode)
            {
                var JsonContent = tokenResponse.Content.ReadAsStringAsync().Result;
                token = JsonConvert.DeserializeObject<Token>(JsonContent);
            }
            else
            {
                token = null;
            }
            return token;
        }
        public Satker GetMaster(string token)
        {

            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage resp = client.GetAsync(baseAddress + "master/satker").GetAwaiter().GetResult();
            if (resp.IsSuccessStatusCode)
            {
                Satker Satker = new Satker();
                var JsonContent = resp.Content.ReadAsStringAsync().Result;
                Satker = JsonConvert.DeserializeObject<Satker>(JsonContent);
                foreach (Datum i in Satker.data)
                {
                    db.InsertUpdate(i.KODE_SATUAN_KERJA, i.SATUAN_KERJA);
                }
                Console.WriteLine(Satker.data[0].KODE_SATUAN_KERJA);
                return Satker;
            }
            else
            {
                return null;
            }
        }
    }
}