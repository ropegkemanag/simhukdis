using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using simhukdis.Models;
using SIMHUKDIS.Models;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace simhukdis.Controllers
{
    [SessionExpire]
    public class UserController : Controller
    {
        // GET: User
        clsUserLoginDB db = new clsUserLoginDB();
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

            List<clsUserLogin> sm = db.Users.ToList();
            return View(sm);
        }
        [HttpGet]
        public ActionResult Create()
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

            clsUserLogin user = new clsUserLogin();
            ViewBag.Satker = new SelectList(db.GetListSatker(), "KODE_SATUAN_KERJA", "SATUAN_KERJA");
            ViewBag.GroupID = new SelectList(db.GetListUserGroup(), "GroupID", "GroupDesc");
            return View();
        }
        [HttpPost]
        public ActionResult Create(string UserName, string Fullname, string Password, string StatusAdmin, string GroupID, string NIP, string satker)
        {
            string strMsg = "";
            try
            {
                string UserLogin = Session["Fullname"].ToString();
                clsUserLogin users = new clsUserLogin();
                users.UserName = UserName;
                users.FullName = Fullname;
                users.Password = Password;
                users.LastUser = UserLogin;
                users.NIP = NIP;
                users.StatusAdmin = StatusAdmin;
                users.Satker = satker;
                if (db.GetDataExist(NIP) == true)
                {
                    strMsg = "NIP sudah ada !";
                    return Json(strMsg, JsonRequestBehavior.AllowGet);
                }
                users.GroupID = GroupID;
                db.Insert(users);
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
        public ActionResult Edit(string UserID, string StatusAdmin, string GroupID)
        {
            try
            {
                clsUserLogin users = db.Users.SingleOrDefault(sub => sub.UserID == UserID);
                users.StatusAdmin = StatusAdmin;
                ViewBag.GroupID = new SelectList(db.GetListUserGroup(), "GroupID", "GroupDesc", GroupID);
                return View(users);
            }
            catch (Exception ex)
            {
                return View(ex);
            }

        }

        [HttpPost]
        public ActionResult Ubah(string UserID, string UserName, string Fullname, string Password, string StatusAdmin, string GroupID, string NIP)
        {
            string strMsg = "";
            string UserLogin = "";
            try
            {
                UserLogin = Session["Fullname"].ToString();
                clsUserLogin users = new clsUserLogin();
                users.UserID = UserID;
                users.UserName = UserName;
                users.FullName = Fullname;
                users.Password = Password;
                users.NIP = NIP;
                users.StatusAdmin = StatusAdmin;
                users.GroupID = GroupID;
                users.LastUser = UserLogin;
                db.Edit(users);
                strMsg = "Success";
                return Json(strMsg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                strMsg = ex.Message.ToString();
                return Json(strMsg, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Delete(string UserID, string NIP)
        {
            string strMsg = "";
            string UserLogin = Session["Fullname"].ToString();
            try
            {
                if (db.GetAlreadyUse(UserID) == true)
                {
                    strMsg = "Hapus gagal, Karena data sudah digunakan !";
                    return Json(strMsg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.Delete(UserID, NIP);
                    strMsg = "Success";
                    return Json(strMsg, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                strMsg = ex.Message.ToString();
                return Json(strMsg, JsonRequestBehavior.AllowGet);
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
        public JsonResult GetPegawai(string NIP)
        {
            try
            {
                var client = new HttpClient();
                string strToken = "";
                string Username = "agus@kemenag.go.id";
                string Password = "12345678";
                string baseAddress = "https://ropeg.kemenag.go.id/api/v1/";
                clsToken db = new clsToken();
                clsDataPegawaiDtl dtl = new clsDataPegawaiDtl();

                strToken = db.GetAccessToken(Username, Password);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strToken);
                HttpResponseMessage resp = client.GetAsync(baseAddress + "pegawai/profil/" + WebUtility.UrlEncode(NIP)).GetAwaiter().GetResult();
                if (resp.IsSuccessStatusCode)
                {
                    clsDataPegawai clsDataPegawai = new clsDataPegawai();
                    var JsonContent = resp.Content.ReadAsStringAsync().Result;
                    clsDataPegawai = JsonConvert.DeserializeObject<clsDataPegawai>(JsonContent);
                    int x = 0;
                    dtl.NIP = clsDataPegawai.data.NIP;
                    dtl.NIP_BARU = clsDataPegawai.data.NIP_BARU;
                    dtl.NAMA = clsDataPegawai.data.NAMA;
                    dtl.NAMA_LENGKAP = clsDataPegawai.data.NAMA_LENGKAP;
                    dtl.AGAMA = clsDataPegawai.data.AGAMA;
                    dtl.TEMPAT_LAHIR = clsDataPegawai.data.TEMPAT_LAHIR;
                    dtl.TANGGAL_LAHIR = clsDataPegawai.data.TANGGAL_LAHIR.Substring(0, 10);
                    dtl.JENIS_KELAMIN = clsDataPegawai.data.JENIS_KELAMIN;
                    dtl.PENDIDIKAN = clsDataPegawai.data.PENDIDIKAN;
                    dtl.KODE_LEVEL_JABATAN = clsDataPegawai.data.KODE_LEVEL_JABATAN;
                    dtl.LEVEL_JABATAN = clsDataPegawai.data.LEVEL_JABATAN;

                    string strPangkat = clsDataPegawai.data.PANGKAT;
                    string strGolRuang = clsDataPegawai.data.GOL_RUANG;
                    string strPangkatGolRuang = strPangkat + ", " + strGolRuang;


                    dtl.PANGKAT = strPangkatGolRuang;
                    dtl.GOL_RUANG = strPangkatGolRuang;

                    dtl.TMT_CPNS = clsDataPegawai.data.TMT_CPNS;
                    dtl.TMT_PANGKAT = clsDataPegawai.data.TMT_PANGKAT;

                    string strMasaKerjaThn = clsDataPegawai.data.MASAKERJA_TAHUN.ToString();
                    string strMasaKerjaBln = clsDataPegawai.data.MASAKERJA_BULAN.ToString();
                    string strMasaKerja = strMasaKerjaThn + " Tahun " + strMasaKerjaBln + " Bulan";

                    dtl.MASAKERJA_TAHUN = strMasaKerja;
                    dtl.MASAKERJA_BULAN = strMasaKerja;
                    dtl.TIPE_JABATAN = clsDataPegawai.data.TIPE_JABATAN;
                    dtl.KODE_JABATAN = clsDataPegawai.data.KODE_JABATAN;
                    dtl.TAMPIL_JABATAN = clsDataPegawai.data.TAMPIL_JABATAN;
                    dtl.TMT_JABATAN = clsDataPegawai.data.TMT_JABATAN;
                    dtl.KODE_SATKER_1 = clsDataPegawai.data.KODE_SATKER_1;
                    dtl.SATKER_1 = clsDataPegawai.data.SATKER_1;
                    dtl.KODE_SATKER_2 = clsDataPegawai.data.KODE_SATKER_2;
                    dtl.SATKER_2 = clsDataPegawai.data.SATKER_2;
                    dtl.KODE_SATKER_3 = clsDataPegawai.data.KODE_SATKER_3;
                    dtl.SATKER_3 = clsDataPegawai.data.SATKER_3;
                    dtl.KODE_SATKER_4 = clsDataPegawai.data.KODE_SATKER_4;
                    dtl.SATKER_4 = clsDataPegawai.data.SATKER_4;
                    dtl.KODE_SATKER_5 = clsDataPegawai.data.KODE_SATKER_5;
                    dtl.SATKER_5 = clsDataPegawai.data.SATKER_5;
                    dtl.SATUAN_KERJA = clsDataPegawai.data.SATUAN_KERJA;
                    dtl.STATUS_KAWIN = clsDataPegawai.data.STATUS_KAWIN;
                    dtl.ALAMAT_1 = clsDataPegawai.data.ALAMAT_1;
                    dtl.ALAMAT_2 = clsDataPegawai.data.ALAMAT_2;
                    dtl.TELEPON = clsDataPegawai.data.TELEPON;
                    dtl.KAB_KOTA = clsDataPegawai.data.KAB_KOTA;
                    dtl.PROVINSI = clsDataPegawai.data.PROVINSI;
                    dtl.KODE_POS = clsDataPegawai.data.KODE_POS;
                    dtl.KODE_LOKASI = clsDataPegawai.data.KODE_LOKASI;
                    dtl.KODE_PANGKAT = clsDataPegawai.data.KODE_PANGKAT;
                    return Json(dtl, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(null, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult DownloadToExcel()
        {
            try
            {
                Workbook wbToStream = new Workbook();
                Worksheet sheet = wbToStream.Worksheets[0];
                sheet.Range["C10"].Text = "The sample demonstrates how to save an Excel workbook to stream.";
                FileStream file_stream = new FileStream("To_stream.xls", FileMode.Create);
                wbToStream.SaveToStream(file_stream);
                file_stream.Close();
                System.Diagnostics.Process.Start("To_stream.xls");

                //B. Load Excel file from stream
                Workbook wbFromStream = new Workbook();
                FileStream fileStream = new FileStream("sample.xls", FileMode.OpenOrCreate);
                fileStream.Seek(0, SeekOrigin.Begin);
                wbFromStream.LoadFromStream(fileStream);
                wbFromStream.SaveToFile("From_stream.xls", ExcelVersion.Version97to2003);
                fileStream.Dispose();
                System.Diagnostics.Process.Start("From_stream.xls");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ToViewFile(string fileName)
        {
            try
            {
                System.Diagnostics.Process.Start(fileName);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}