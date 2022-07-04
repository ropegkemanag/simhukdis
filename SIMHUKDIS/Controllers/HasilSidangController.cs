using simhukdis.Models;
using SIMHUKDIS.Models;
using Spire.Doc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIMHUKDIS.Controllers
{
    [SessionExpire]
    public class HasilSidangController : Controller
    {
        // GET: HasilSidang
        clsHasilSidangDB db = new clsHasilSidangDB();
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
                string UserID = Session["UserID"].ToString();

                ViewBag.StatusAdmin = StatusAdmin;
                ViewBag.UserID = userlogin;
                ViewBag.SatuanKerja = SatuanKerja;
                ViewBag.UserGroup = UserGroup;
                //clsHukdisDB x = new clsHukdisDB();
                //ViewBag.Hukdis = new SelectList(x.GetListHukdis(), "ID", "HukdisDesc");
                List<clsHasilSidang> PD = db.ListAll(UserID).ToList();
                clsDataPegawaiDtl a = new clsDataPegawaiDtl();
                return View(PD);
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });
            }            
        }
        [HttpGet]
        public ActionResult Create(int ID, string NIP)
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
                ViewBag.Hukdis = new SelectList(x.GetListHukdis(), "ID", "HukdisDesc");
                clsHasilSidangDB db = new clsHasilSidangDB();
                HasilSidangDtl hasil = db.GetListCreate(ID, NIP);
                if (hasil.Mengingat == null || hasil.Mengingat == "")
                {
                    hasil.Mengingat = db.GetPasalPelanggaran();
                }
                if (hasil.Tembusan == null || hasil.Tembusan == "")
                {
                    hasil.Tembusan = db.GetTembusan(hasil.KODE_SATUAN_KERJA, hasil.UnitKerja);
                }
                ViewBag.ID = ID;
                ViewBag.NIP = NIP;
                string statusAdmin = Session["StatusAdmin"] + "";
                //return View();
                return View(hasil);
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });
                //return RedirectToAction("Error500", "Home");
            }
        }
        public ActionResult UbahData(string ID, string NIP, string KeputusanSidang, string Tanggal_Sidang, string Catatan_Sidang,
            string DasarBukti, string Pelanggaran, string PasalPelanggaran, string Mengingat, string Tembusan)
        {
            string strMsg = "";
            try
            {
                if (Session["Fullname"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                string UserLogin = Session["Fullname"].ToString();
                string SatuanKerja = Session["Satker"].ToString();
                string StatusAdmin = Session["StatusAdmin"].ToString();
                string UserGroup = Session["UserGroup"].ToString();
                string UserID = Session["UserID"].ToString();

                ViewBag.StatusAdmin = StatusAdmin;
                ViewBag.UserID = UserLogin;
                ViewBag.SatuanKerja = SatuanKerja;
                ViewBag.UserGroup = UserGroup;

                clsHasilSidang PD = new clsHasilSidang();
                PD.ID = ID;
                PD.NIP = NIP;
                PD.KeputusanSidang = KeputusanSidang;
                PD.Catatan_Sidang = Catatan_Sidang;
                PD.UserLogin = UserLogin;
                PD.Tanggal_Sidang_DPK = Tanggal_Sidang;
                PD.DasarBukti = DasarBukti;
                PD.PelanggaranDisiplin = Pelanggaran;
                PD.PasalPelanggaran = PasalPelanggaran;
                PD.Mengingat = Mengingat;
                PD.Tembusan = Tembusan;
                if (db.GetDataExist(ID, NIP) == true)
                {
                    db.Update(PD);
                }
                else
                {
                    db.Insert(PD);
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
        public ActionResult GenerateWord(int ID, string NIP)
        {
            string filepath = HttpContext.Request.PhysicalApplicationPath;
            string strTemplate = filepath + "Files/Template/Template_Hukuman_Dengan_Sidang_DPK.docx";
            string OutputPath = filepath + "Files/Result/SK/";
            string GetDateTime = DateTime.Now.ToString("ddMMyyyy_hhmmss");
            string strFileNameDoc = "SK_" + NIP + "_" + GetDateTime + ".docx";
            string strFileNamePDF = "SK_" + NIP + "_" + GetDateTime + ".pdf";
            string strMsg = "";
            try
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
                string UserLogin = Session["Fullname"].ToString();

                clsHasilSidang a = new clsHasilSidang();
                a = db.ListAll(ID, NIP);

                Document doc = new Document();
                doc.LoadFromFile(strTemplate);
                //get strings to replace
                Dictionary<string, string> dictReplace = GetReplaceDictionary(a);
                //Replace text
                foreach (KeyValuePair<string, string> kvp in dictReplace)
                {
                    doc.Replace(kvp.Key, kvp.Value, true, true);
                }
                //Save doc file.
                doc.SaveToFile(OutputPath + strFileNameDoc, FileFormat.Docx);
                //Convert to PDF
                doc.SaveToFile(OutputPath + strFileNamePDF, FileFormat.PDF);
                //Insert Table
                //ToViewFile(OutputPath + strFileNameDoc);
                //strMsg = "Success";
                //return Json(strMsg, JsonRequestBehavior.AllowGet);
                clsDocument b = new clsDocument();
                b.DocPDF = strFileNamePDF;
                b.DocWord = strFileNameDoc;
                b.Msg = "Success";

                return Json(b, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                strMsg = ex.Message.ToString();
                return Json(strMsg, JsonRequestBehavior.AllowGet);
            }
        }
        public FileResult DownloadFile(string fileName)
        {
            try
            {
                //Build the File Path.
                string path = Server.MapPath("~/Files/Result/SK/") + fileName;

                //Read the File data into Byte Array.
                byte[] bytes = System.IO.File.ReadAllBytes(path);

                //Send the File to Download.
                return File(bytes, "application/octet-stream", fileName);
            }
            catch (Exception ex)
            {
                string strMsg = ex.Message.ToString();
                return null;
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
        Dictionary<string, string> GetReplaceDictionary(clsHasilSidang a)
        {
            Dictionary<string, string> replaceDict = new Dictionary<string, string>();
            replaceDict.Add("*DasarBukti*", a.DasarBukti);
            replaceDict.Add("*Nama_Lengkap*", a.FullName);
            replaceDict.Add("*NIP*", a.NIP);
            replaceDict.Add("*Pelanggaran*", a.PelanggaranDisiplin);
            replaceDict.Add("*Pasal_Pelanggaran*", a.PasalPelanggaran);
            replaceDict.Add("*hukuman*", a.hukuman);
            replaceDict.Add("*Keputusan_Sidang*", a.hukuman);
            replaceDict.Add("*Pangkat_Gol*", a.GOL_RUANG);
            replaceDict.Add("*Jabatan*", a.LEVEL_JABATAN);
            replaceDict.Add("*UnitKerja*", a.UnitKerja);
            replaceDict.Add("*Konseptor*", a.konseptor);
            replaceDict.Add("*Kasubag*", a.subkoor);
            replaceDict.Add("*Koordinator*", a.koor);
            replaceDict.Add("*Nama_Menteri*", a.menag);
            replaceDict.Add("*Mengingat*", a.Mengingat);
            replaceDict.Add("*Tembusan*", a.Tembusan);
            replaceDict.Add("*Jabatan_Konseptor*", a.Jabatan_Konseptor);
            return replaceDict;
        }
        public JsonResult GetPasalPelanggaran(string NIP)
        {
            try
            {
                string Pasal = db.GetPasalPelanggaran();
                return Json(Pasal, JsonRequestBehavior.AllowGet);
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
        public JsonResult GetTembusan(string Satker, string Satker3)
        {
            try
            {
                string Pasal = db.GetPasalPelanggaran();
                return Json(Pasal, JsonRequestBehavior.AllowGet);
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