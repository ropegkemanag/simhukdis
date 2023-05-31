using SIMHUKDIS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIMHUKDIS.Controllers.Konseptor
{
    public class PenyampaianSKBiroController : Controller
    {
        clsPenyampaianSKBiroDB db = new clsPenyampaianSKBiroDB();
        // GET: PenyampaianSKBiro
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
            string UserID = Session["UserID"].ToString();
            ViewBag.UserID = UserID;
            ViewBag.SatuanKerja = SatuanKerja;
            ViewBag.UserGroup = UserGroup;
            List<clsKumpulanSK> PS = db.PS(UserID, UserGroup, SatuanKerja).ToList();
            return View(PS);
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
                string UserID = Session["UserID"].ToString();

                ViewBag.UserID = userlogin;
                ViewBag.SatuanKerja = SatuanKerja;
                ViewBag.UserGroup = UserGroup;
                ClsSatkerDB x = new ClsSatkerDB();
                ViewBag.SatuanKerjaName = x.GetName(SatuanKerja);
                clsSuratMasukDB A = new clsSuratMasukDB();
                ViewBag.Satker = new SelectList(A.GetListSatker(), "Kode_Satuan_Kerja", "SATUAN_KERJA", SatuanKerja);
                ViewBag.Unit_Kerja = new SelectList(A.GetUnit(SatuanKerja), "Kode_Unit_Kerja", "Unit_Kerja");
                return View();
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });
            }
        }
        [HttpPost]
        public ActionResult Create(string NO_SK, string TANGGAL_SK, string NIP,
            string NAMA_LENGKAP, string JENIS_SK, string TMT_SK, string KODE_SATUAN_KERJA,
            string Kode_Unit_Kerja, string KETERANGAN, HttpPostedFileBase FILE_SK)
        {
            try
            {
                if (Session["Fullname"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                else
                {
                    string userlogin = Session["Fullname"].ToString();
                    string SatuanKerja = Session["Satker"].ToString();
                    string StatusAdmin = Session["StatusAdmin"].ToString();
                    string UserGroup = Session["UserGroup"].ToString();
                    string UserID = Session["UserID"].ToString();

                    ViewBag.UserID = userlogin;
                    ViewBag.SatuanKerja = SatuanKerja;
                    ViewBag.UserGroup = UserGroup;

                    String FilePath = "";
                    String FileExt = "";
                    String GetDateTime = "";
                    String Ext = "";
                    String FileNameWithoutExtension = "";
                    string a, b;
                    GetDateTime = DateTime.Now.ToString("ddMMyyyy_hhmmss");
                    string UserLogin = Session["Fullname"].ToString();
                    clsKumpulanSK PS = new clsKumpulanSK();
                    PS.NO_SK = NO_SK;
                    //PS.TANGGAL_SK = TANGGAL_SK;
                    PS.NIP = NIP;
                    PS.NAMA_LENGKAP = NAMA_LENGKAP;
                    PS.JENIS_SK = JENIS_SK;
                    //PS.TMT_SK = TMT_SK;
                    PS.KODE_SATUAN_KERJA = KODE_SATUAN_KERJA;
                    PS.KODE_UNIT_KERJA = Kode_Unit_Kerja;
                    if (TANGGAL_SK == null | TANGGAL_SK == "")
                    {
                        PS.TANGGAL_SK = "01 January 1900";
                    }
                    else
                    {
                        PS.TANGGAL_SK = TANGGAL_SK;
                    }

                    if (TMT_SK == null | TMT_SK == "")
                    {
                        PS.TMT_SK = "01 January 1900";
                    }
                    else
                    {
                        PS.TMT_SK = TMT_SK;
                    }
                    if (FILE_SK != null)
                    {
                        a = FILE_SK.FileName;
                        FileExt = Path.GetExtension(a);
                        Ext = Server.MapPath("/Files/Upload/INVENTARIS SURAT/");
                        FileNameWithoutExtension = Path.GetFileNameWithoutExtension(a);
                        FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                        Request.Files[0].SaveAs(FilePath);
                        PS.FILE_SK = FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                    }
                    else
                    {
                        PS.FILE_SK = "";
                    }

                    PS.CREATED_USER = UserID;
                    PS.KETERANGAN = KETERANGAN;
                    db.Insert(PS);

                    return RedirectToAction("Index", "PenyampaianSKBiro");
                }
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });

            }
        }
        [HttpGet]
        public ActionResult Edit(string ID, string Kode_Satuan_Kerja, string Kode_Unit_Kerja, string KODE_JENIS_SK)
        {

            if (Session["Fullname"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            try
            {
                clsPenyampaianSKBiroDB sm = new clsPenyampaianSKBiroDB();
                clsKumpulanSK PS = db.GetList(ID);

                string SatuanKerja = Session["Satker"].ToString();
                string StatusAdmin = Session["StatusAdmin"].ToString();
                string UserGroup = Session["UserGroup"].ToString();
                string userlogin = Session["Fullname"].ToString();
                string UserID = Session["UserID"].ToString();

                ViewBag.UserID = userlogin;
                ViewBag.SatuanKerja = SatuanKerja;
                ViewBag.UserGroup = UserGroup;
                ViewBag.TanggalSurat = PS.TANGGAL_SK;
                ViewBag.TMT_SK = PS.TMT_SK;
                ViewBag.JENIS_SK = KODE_JENIS_SK;

                clsSuratMasukDB A = new clsSuratMasukDB();
                ViewBag.Satker = new SelectList(A.GetListSatker(), "Kode_Satuan_Kerja", "SATUAN_KERJA", PS.KODE_SATUAN_KERJA);
                ViewBag.Unit_Kerja = new SelectList(A.GetUnit(Kode_Satuan_Kerja), "Kode_Unit_Kerja", "Unit_Kerja", PS.KODE_UNIT_KERJA);

                return View(PS);
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });
            }
        }
        [HttpPost]
        public ActionResult Edit(string ID, string NO_SK, string TANGGAL_SK, string NIP,
            string NAMA_LENGKAP, string KODE_JENIS_SK, string TMT_SK, string KODE_SATUAN_KERJA,
            string Kode_Unit_Kerja, string KETERANGAN, HttpPostedFileBase FILE_SK)
        {
            string FilePath = "";
            string FileExt = "";
            string GetDateTime = "";
            string Ext = "";
            string FileNameWithoutExtension = "";
            string a;

            if (Session["Fullname"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            try
            {
                string SatuanKerja = Session["Satker"].ToString();
                string StatusAdmin = Session["StatusAdmin"].ToString();
                string UserGroup = Session["UserGroup"].ToString();
                string userlogin = Session["Fullname"].ToString();
                string UserID = Session["UserID"].ToString();

                GetDateTime = DateTime.Now.ToString("ddMMyyyy_hhmmss");
                string UserLogin = Session["Fullname"].ToString();
                clsKumpulanSK PS = new clsKumpulanSK();
                PS.ID = ID;
                PS.NO_SK = NO_SK;
                //PS.TANGGAL_SK = TANGGAL_SK;
                PS.NIP = NIP;
                PS.NAMA_LENGKAP = NAMA_LENGKAP;
                PS.KODE_JENIS_SK = KODE_JENIS_SK;
                //PS.TMT_SK = TMT_SK;
                PS.KODE_SATUAN_KERJA = KODE_SATUAN_KERJA;
                PS.KODE_UNIT_KERJA = Kode_Unit_Kerja;

                if (TANGGAL_SK == null | TANGGAL_SK == "")
                {
                    PS.TANGGAL_SK = "01 January 1900";
                }
                else
                {
                    PS.TANGGAL_SK = TANGGAL_SK;
                }

                if (TMT_SK == null | TMT_SK == "")
                {
                    PS.TMT_SK = "01 January 1900";
                }
                else
                {
                    PS.TMT_SK = TMT_SK;
                }

                clsKumpulanSK xx = db.GetList(ID);
                string FullPath = "";
                if (FILE_SK != null)
                {
                    a = FILE_SK.FileName;
                    FileExt = Path.GetExtension(a);
                    Ext = Server.MapPath("/Files/Upload/INVENTARIS SURAT/");
                    FileNameWithoutExtension = Path.GetFileNameWithoutExtension(a);
                    FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                    Request.Files[0].SaveAs(FilePath);
                    PS.FILE_SK = FileNameWithoutExtension + "_" + GetDateTime + FileExt;

                    FullPath = Ext + xx.FILE_SK;
                    if (System.IO.File.Exists(FullPath))
                    {
                        System.IO.File.Delete(FullPath);
                    }
                }
                else
                {
                    PS.FILE_SK = "";
                }

                PS.CREATED_USER = UserID;
                PS.KETERANGAN = KETERANGAN;
                db.Update(PS);
                return RedirectToAction("Index", "PenyampaianSKBiro");
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });

            }
        }
        [HttpPost]
        public ActionResult Delete(int ID)
        {
            string strMsg = "";
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

                ViewBag.UserID = userlogin;
                ViewBag.SatuanKerja = SatuanKerja;
                ViewBag.UserGroup = UserGroup;

                db.Delete(ID);
                strMsg = "Success";
                return Json(strMsg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                strMsg = ex.Message.ToString();
                return Json(strMsg, JsonRequestBehavior.AllowGet);
                //return View();
            }
        }

        public FileResult DownloadFile1(string fileName)
        {
            try
            {
                //Build the File Path.
                string path = Server.MapPath("/Files/Upload/INVENTARIS SURAT/") + fileName;

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
    }
}