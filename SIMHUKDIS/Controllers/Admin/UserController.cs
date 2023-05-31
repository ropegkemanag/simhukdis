using Newtonsoft.Json;
using SIMHUKDIS.Models;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace SIMHUKDIS.Controllers
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
            try
            {
                string userlogin = Session["Fullname"].ToString();
                string SatuanKerja = Session["Satker"].ToString();
                string StatusAdmin = Session["StatusAdmin"].ToString();
                string UserGroup = Session["UserGroup"].ToString();
                ViewBag.UserID = userlogin;
                ViewBag.SatuanKerja = SatuanKerja;
                ViewBag.UserGroup = UserGroup;

                List<clsUserLogin> sm = db.Users();
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

                clsUserLogin user = new clsUserLogin();
                ViewBag.Satker = new SelectList(db.GetListSatker(), "KODE_SATUAN_KERJA", "SATUAN_KERJA");
                ViewBag.GroupID = new SelectList(db.GetListUserGroup(), "GroupID", "GroupDesc");
                return View();
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message});
            }
        }
        [HttpGet]
        public ActionResult ChangePassword()
        {
            if (Session["Fullname"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            string UserNIP = Session["LogUserID"].ToString();
            string userlogin = Session["Fullname"].ToString();
            string SatuanKerja = Session["Satker"].ToString();
            string StatusAdmin = Session["StatusAdmin"].ToString();
            string UserGroup = Session["UserGroup"].ToString();
            string UserID = Session["UserID"].ToString();

            ViewBag.UserIDx = UserID;
            ViewBag.UserNIP = UserNIP;
            ViewBag.UserID = userlogin;
            ViewBag.SatuanKerja = SatuanKerja;
            ViewBag.UserGroup = UserGroup;
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(string UserID, string Password, string NIP)
        {
            string strMsg = "";
            try
            {
                string UserLogin = Session["Fullname"].ToString();
                clsUserLogin users = new clsUserLogin();
                users.UserID = UserID;
                users.NIP = NIP;
                users.Password = Password;
                users.LastUser = UserLogin;
                db.ChangePassword(users);
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
        public ActionResult Create(string UserName, string Fullname, string Password, string PhoneNo,string Email,
            string StatusAdmin, string GroupID, string NIP, string satker, string LEVEL_JABATAN)
        {
            string strMsg = "";
            try
            {
                string UserLogin = Session["Fullname"].ToString();
                string UserID = Session["UserID"].ToString();
                clsUserLogin users = new clsUserLogin();
                users.UserName = UserName;
                users.FullName = Fullname;
                users.Password = Password;
                users.PhoneNo = PhoneNo;
                users.Email = Email;
                users.LastUser = UserID;
                users.NIP = NIP;
                users.StatusAdmin = StatusAdmin;
                users.Satker = satker;
                users.LEVEL_JABATAN = LEVEL_JABATAN;
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
        public ActionResult Edit(string UserID, string StatusAdmin, string GroupID, string Satker)
        {
            if (Session["Fullname"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            try
            {
                
                string UserNIP = Session["LogUserID"].ToString();
                string userlogin = Session["Fullname"].ToString();
                string SatuanKerja = Session["Satker"].ToString();
                string UserGroup = Session["UserGroup"].ToString();
                string UserIDx = Session["UserID"].ToString();

                ViewBag.UserIDx = UserIDx;
                ViewBag.UserNIP = UserNIP;
                ViewBag.UserID = userlogin;
                ViewBag.SatuanKerja = SatuanKerja;
                ViewBag.UserGroup = UserGroup;
                ViewBag.StatusAdmin = StatusAdmin;

                clsUserLogin users = db.UbahData(UserID);
                ViewBag.GroupID = new SelectList(db.GetListUserGroup(), "GroupID", "GroupDesc", GroupID);
                //users.StatusAdmin = StatusAdmin;
                ViewBag.Satker = new SelectList(db.GetListSatker(), "KODE_SATUAN_KERJA", "SATUAN_KERJA", Satker);

                return View(users);
            }
            catch (Exception ex)
            {
                return View(ex);
            }

        }

        [HttpPost]
        public ActionResult Ubah(string UserID, string UserName, string Fullname, string Password, string PhoneNo, string Email
            ,string StatusAdmin, string GroupID, string NIP, string LEVEL_JABATAN, string satker)
        {
            string strMsg = "";
            string UserLogin = "";
            try
            {
                UserLogin = Session["Fullname"].ToString();
                string UserIDx = Session["UserID"].ToString();
                clsUserLogin users = new clsUserLogin();
                users.UserID = UserID;
                users.UserName = UserName;
                users.FullName = Fullname;
                users.Password = Password;
                users.PhoneNo = PhoneNo;
                users.Email = Email;
                users.NIP = NIP;
                users.StatusAdmin = StatusAdmin;
                users.GroupID = GroupID;
                users.LastUser = UserIDx;
                users.LEVEL_JABATAN = LEVEL_JABATAN;
                users.Satker = satker;
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
                string baseAddress = "https://api.kemenag.go.id/v1/";
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

                    string strMasaKerjaThn = clsDataPegawai.data.MK_TAHUN.ToString();
                    string strMasaKerjaBln = clsDataPegawai.data.MK_BULAN.ToString();
                    string strMasaKerja = strMasaKerjaThn + " Tahun " + strMasaKerjaBln + " Bulan";

                    dtl.MK_TAHUN = strMasaKerja;
                    dtl.MK_BULAN = strMasaKerja;
                    dtl.TIPE_JABATAN = clsDataPegawai.data.TIPE_JABATAN;
                    dtl.KODE_JABATAN = clsDataPegawai.data.KODE_JABATAN;
                    dtl.TAMPIL_JABATAN = clsDataPegawai.data.TAMPIL_JABATAN;
                    dtl.TMT_JABATAN = clsDataPegawai.data.TMT_JABATAN;
                    dtl.KODE_SATUAN_KERJA = clsDataPegawai.data.KODE_SATUAN_KERJA;
                    dtl.SATKER_1 = clsDataPegawai.data.SATKER_1;
                    dtl.KODE_SATKER_2 = clsDataPegawai.data.KODE_SATKER_2;
                    dtl.SATKER_2 = clsDataPegawai.data.SATKER_2;
                    dtl.KODE_SATKER_3 = clsDataPegawai.data.KODE_SATKER_3;
                    dtl.SATKER_3 = clsDataPegawai.data.SATKER_3;
                    dtl.KODE_SATKER_4 = clsDataPegawai.data.KODE_SATKER_4;
                    dtl.SATKER_4 = clsDataPegawai.data.SATKER_4;
                    dtl.KODE_SATKER_5 = clsDataPegawai.data.KODE_SATKER_5;
                    dtl.SATKER_5 = clsDataPegawai.data.SATKER_5;
                    dtl.KETERANGAN_SATUAN_KERJA = clsDataPegawai.data.KETERANGAN_SATUAN_KERJA;
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
                return new JsonResult
                {
                    Data = new { ErrorMessage = ex.Message, Success = false },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.DenyGet
                };
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
        public ActionResult DownloadToExcel2()
        {
            try
            {
                string strMsg = "";
                using (ExcelPackage exl = new ExcelPackage())
                {
                    string sFilename = "DataUser_" + String.Format("{0:yyyyMMdd}", DateTime.Now) + "_" + String.Format("{0:HHmm}", DateTime.Now);
                    List<clsUserLogin> rpt = new List<clsUserLogin>();
                    rpt = db.Users();
                    ExcelWorksheet ws = exl.Workbook.Worksheets.Add("Sheet1");
                    if (rpt.Count == 0)
                    {
                        strMsg = "Data not Found";
                    }
                    else
                    {
                        Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#C0C0C0");

                        //HEADER REPORT
                        //HEADER REPORT
                        ws.Cells["A1"].Value = "DATA USER SIMHUKDIS";
                        ws.Cells["A1:F1"].Merge = true;
                        ws.Cells["A1"].Style.Font.Bold = true;
                        ws.Cells["A1"].Style.Font.Size = 18;
                        ExcelRange rg1 = ws.Cells[1, 1, 1, 6];
                        rg1.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        rg1.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        rg1.Style.Font.Bold = true;

                        ws.Cells["A3"].Value = "Nama Lengkap";
                        ws.Cells["A3"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws.Cells["A3"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                        ws.Cells["B3"].Value = "NIP";
                        ws.Cells["B3"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws.Cells["B3"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                        ws.Cells["C3"].Value = "Password";
                        ws.Cells["C3"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws.Cells["C3"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                        ws.Cells["D3"].Value = "Status Admin";
                        ws.Cells["D3"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws.Cells["D3"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                        ws.Cells["E3"].Value = "Last Login";
                        ws.Cells["E3"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws.Cells["E3"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                        ws.Cells["F3"].Value = "Last User";
                        ws.Cells["F3"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws.Cells["F3"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                        
                        ExcelRange rg2 = ws.Cells[3, 1, 3, 6];
                        rg2.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        rg2.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        rg2.Style.Font.Bold = true;
                        rg2.Style.WrapText = true;

                        int i = 4;
                        foreach (clsUserLogin model in rpt)
                        {
                            ws.Cells[i, 1].Value = model.FullName;
                            ws.Cells[i, 2].Value = model.NIP;
                            ws.Cells[i, 3].Value = model.Password;
                            ws.Cells[i, 4].Value = model.StatusAdmin;
                            ws.Cells[i, 5].Value = model.LastLogin;
                            ws.Cells[i, 6].Value = model.LastUser;
                            i = i + 1;
                        }
                        ExcelRange rg = ws.Cells[3, 1, i - 1, 6];
                        rg.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        rg.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        rg.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        rg.Style.Border.Top.Style = ExcelBorderStyle.Thin;

                        ExcelRange rg3 = ws.Cells[3, 1, i - 1, 5];
                        rg3.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        rg3.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                        ws.Row(3).Height = 50;
                        ws.Column(1).Width = 50;
                        ws.Column(2).Width = 20;
                        ws.Column(3).Width = 20;
                        ws.Column(4).Width = 20;
                        ws.Column(5).Width = 25;
                        ws.Column(6).Width = 25;

                        string fileName = sFilename;
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment; filename=" + fileName + ".xlsx");

                        MemoryStream stream = new MemoryStream(exl.GetAsByteArray());
                        Response.OutputStream.Write(stream.ToArray(), 0, stream.ToArray().Length);
                        Response.Flush();
                        Response.Close();
                    }

                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}