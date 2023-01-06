using Newtonsoft.Json;
using SIMHUKDIS.Models;
using Spire.Doc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace SIMHUKDIS.Controllers
{
    [SessionExpire]
    public class SuratMasukController : Controller
    {
        clsSuratMasukDB db = new clsSuratMasukDB();
        string strToken = "vhTYPWC5jyz72sK4VDcjR2re7xPNYnEa516ysMJlpUlKvMgTKNHvdSW9wUDlnTay";
        string baseAddress = "https://kudus.wablas.com/";
        // GET: SuratMasuk
        public ActionResult Index(int Status, string DateFrom, string DateTo)
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
            try
            {
                clsSuratMasukFilter a = new clsSuratMasukFilter();
                a.GroupID = UserGroup;
                a.UserLogin = UserID;
                if (Status != null)
                {
                    a.Status = Status;
                    ViewBag.Status = Status;
                }
                else
                {
                    a.Status = 0;
                    ViewBag.Status = 0;                    
                }
                if (DateFrom == null || DateFrom == "")
                {
                    ViewBag.DateFrom = Convert.ToDateTime(DateTime.Today).ToString("yyyy-01-01");
                    a.DateFrom = Convert.ToDateTime(DateTime.Today).ToString("yyyy-01-01");
                }
                else
                {
                    ViewBag.DateFrom = Convert.ToDateTime(DateFrom).ToString("yyyy-MM-dd");
                    a.DateFrom = Convert.ToDateTime(DateFrom).ToString("yyyy-MM-dd");
                }
                if (DateTo == null || DateTo == "")
                {
                    ViewBag.DateTo = Convert.ToDateTime(DateTime.Today).ToString("yyyy-MM-dd");
                    a.DateTo = Convert.ToDateTime(DateTime.Today).ToString("yyyy-MM-dd");
                }
                else
                {
                    ViewBag.DateTo = Convert.ToDateTime(DateTo).ToString("yyyy-MM-dd");
                    a.DateTo = Convert.ToDateTime(DateTo).ToString("yyyy-MM-dd");
                }
                List<clsSuratMasuk> sm = db.SuratMasukFilter(a).ToList();
                return View(sm);
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });
            }            
        }

        public ActionResult CreateSPTJM()
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

                ViewBag.UserID = UserID;
                ViewBag.SatuanKerja = SatuanKerja;
                ViewBag.UserGroup = UserGroup;
                return View();
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });

            }
        }
        public ActionResult cetakSPTJM(string NIP,
               string NAMA_LENGKAP, string GOL_RUANG, string LEVEL_JABATAN,
               string Satker, string NIP_USUL, string NAMA_LENGKAP_USUL, string Tanggal_Surat)
        {
            string filepath = HttpContext.Request.PhysicalApplicationPath;
            string strTemplate = filepath + "Files/Template/SPTJM Atasan.docx";
            string OutputPath = filepath + "Files/Result/SPTJM/";
            string GetDateTime = DateTime.Now.ToString("ddMMyyyy_hhmmss");
            string strFileNameDoc = "SPTJM Atasan" + NIP + "_" + GetDateTime + ".docx";
            string strFileNamePDF = "SPTJM Atasan" + NIP + "_" + GetDateTime + ".pdf";
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
                string UserLogin = Session["Fullname"].ToString();

                clsSPTMJ a = new clsSPTMJ();
                a.NIP = NIP;
                a.NAMA_LENGKAP = NAMA_LENGKAP;
                a.GOL_RUANG = GOL_RUANG;
                a.LEVEL_JABATAN = LEVEL_JABATAN;
                a.Satker = Satker;
                a.NIP_USUL = NIP_USUL;
                a.NAMA_LENGKAP_USUL = NAMA_LENGKAP_USUL;
                a.Created_User = UserID;
                a.Tanggal_Surat = Tanggal_Surat;

                Document doc = new Document();
                doc.LoadFromFile(strTemplate);
                //get strings to replace
                Dictionary<string, string> dictReplace = ReplaceWord(a);
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
                clsSuratMasukDB c = new clsSuratMasukDB();
                c.InsertSPTJM(a);

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
        public ActionResult CreateTindakPidana()
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
                ClsSatkerDB x = new ClsSatkerDB();
                ViewBag.SatuanKerjaName = x.GetName(SatuanKerja);
                clsSuratMasuk sm = new clsSuratMasuk();
                ViewBag.Satker = new SelectList(db.GetListSatker(), "KODE_SATUAN_KERJA", "SATUAN_KERJA", SatuanKerja);
                return View();
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });
                
            }
        }
        public ActionResult CreatePS()
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
                clsSuratMasuk sm = new clsSuratMasuk();
                ViewBag.Satker = new SelectList(db.GetListSatker(), "KODE_SATUAN_KERJA", "SATUAN_KERJA", SatuanKerja);
                return View();
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
            ClsSatkerDB x = new ClsSatkerDB();
            ViewBag.SatuanKerjaName = x.GetName(SatuanKerja);
            clsSuratMasuk sm = new clsSuratMasuk();
            ViewBag.Satker = new SelectList(db.GetListSatker(), "KODE_SATUAN_KERJA", "SATUAN_KERJA", SatuanKerja);
            ViewBag.Unit_Kerja = new SelectList(db.GetUnit(SatuanKerja), "Kode_Unit_Kerja", "Unit_Kerja");
                return View();
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message});                
            }
        }
        [HttpGet]
        public ActionResult Details(int ID)
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
                ViewBag.ID = ID;

            clsSuratMasukDB db = new clsSuratMasukDB();
            clsSuratMasuk sm = db.GetList(ID);
            return View(sm);
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });
                
            }
        }
        [HttpPost]
        public ActionResult Create(string NoAgenda, string NoSurat, string AsalSurat, string TanggalSurat, 
            string perihal, string KODE_SATUAN_KERJA, string Kode_Unit_Kerja, HttpPostedFileBase LampiranSurat1,
            HttpPostedFileBase LampiranSurat2, HttpPostedFileBase LampiranSurat3, 
            HttpPostedFileBase LampiranSurat4, 
            HttpPostedFileBase LampiranSurat5,
            HttpPostedFileBase LampiranSurat6, 
            HttpPostedFileBase LampiranSurat_LHA,
            HttpPostedFileBase LampiranLHP,
            HttpPostedFileBase LampiranSPTJM,
            string UsulStatus)
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

                String FilePath = "";
                String FileExt = "";
                String GetDateTime = "";
                String Ext = "";
                String FileNameWithoutExtension = "";
                string a, b, c, d, e, f,g,h,i;
                GetDateTime = DateTime.Now.ToString("ddMMyyyy_hhmmss");
                string UserLogin = Session["Fullname"].ToString();
                clsSuratMasuk sm = new clsSuratMasuk();
                sm.NoAgenda = "";
                sm.NoSurat = NoSurat;
                sm.AsalSurat = AsalSurat;
                sm.TanggalSurat = TanggalSurat;
                sm.perihal = perihal;
                sm.Kode_Unit_Kerja = Kode_Unit_Kerja;
                 
                if (KODE_SATUAN_KERJA == null && UserGroup == "03")
                {
                    sm.SATUAN_KERJA = SatuanKerja;
                }
                else
                {
                    sm.SATUAN_KERJA = KODE_SATUAN_KERJA;
                }
                if (LampiranSurat1 != null)
                {
                    a = LampiranSurat1.FileName;
                    FileExt = Path.GetExtension(a);
                    Ext = Server.MapPath("/Files/Upload/Surat Masuk/1.Surat Pengantar/");
                    FileNameWithoutExtension = Path.GetFileNameWithoutExtension(a);
                    FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                    Request.Files[0].SaveAs(FilePath);
                    sm.LampiranSurat1 = FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                }
                else
                {
                    sm.LampiranSurat1 = "";
                }
                if (LampiranSurat2 != null)
                {
                    b = LampiranSurat2.FileName;
                    FileExt = Path.GetExtension(b);
                    Ext = Server.MapPath("/Files/Upload/Surat Masuk/2.BAP/");
                    FileNameWithoutExtension = Path.GetFileNameWithoutExtension(b);
                    FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                    Request.Files[1].SaveAs(FilePath);
                    sm.LampiranSurat2 = FileNameWithoutExtension + "_" + GetDateTime + FileExt;

                }
                else
                {
                    sm.LampiranSurat2 = "";
                }
                if (LampiranSurat3 != null)
                {
                    c = LampiranSurat3.FileName;
                    FileExt = Path.GetExtension(c);
                    Ext = Server.MapPath("/Files/Upload/Surat Masuk/3.Surat Tugas Pemeriksaan/");
                    FileNameWithoutExtension = Path.GetFileNameWithoutExtension(c);
                    FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                    Request.Files[2].SaveAs(FilePath);
                    sm.LampiranSurat3 = FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                }
                else
                {
                    sm.LampiranSurat3 = "";
                }
                if (LampiranSurat4 != null)
                {
                     d = LampiranSurat4.FileName;
                    FileExt = Path.GetExtension(d);
                    Ext = Server.MapPath("/Files/Upload/Surat Masuk/4.Surat Panggilan/");
                    FileNameWithoutExtension = Path.GetFileNameWithoutExtension(d);
                    FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                    Request.Files[3].SaveAs(FilePath);
                    sm.LampiranSurat4 = FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                }
                else
                {
                    sm.LampiranSurat4 = "";
                }
                if (LampiranSurat5 != null)
                {
                     e = LampiranSurat5.FileName;
                    FileExt = Path.GetExtension(e);
                    Ext = Server.MapPath("/Files/Upload/Surat Masuk/5.Dokumen Lainnya/");
                    FileNameWithoutExtension = Path.GetFileNameWithoutExtension(e);
                    FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                    Request.Files[6].SaveAs(FilePath);
                    sm.LampiranSurat5 = FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                }
                else
                {
                    sm.LampiranSurat5 = "";
                }
                if (LampiranSurat6 != null)
                {
                     f = LampiranSurat6.FileName;
                    FileExt = Path.GetExtension(f);
                    Ext = Server.MapPath("/Files/Upload/Surat Masuk/6.Surat Putusan Pengadilan/");
                    FileNameWithoutExtension = Path.GetFileNameWithoutExtension(f);
                    FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                    Request.Files[0].SaveAs(FilePath);
                    sm.LampiranSurat6 = FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                }
                else
                {
                    sm.LampiranSurat6 = "";
                }
                if (LampiranSurat_LHA != null)
                {
                    g = LampiranSurat_LHA.FileName;
                    FileExt = Path.GetExtension(g);
                    Ext = Server.MapPath("/Files/Upload/Surat Masuk/7.LHA/");
                    FileNameWithoutExtension = Path.GetFileNameWithoutExtension(g);
                    FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                    Request.Files[0].SaveAs(FilePath);
                    sm.LampiranSurat_LHA = FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                }
                else
                {
                    sm.LampiranSurat_LHA = "";
                }
                if (LampiranLHP != null)
                {
                    h = LampiranLHP.FileName;
                    FileExt = Path.GetExtension(h);
                    Ext = Server.MapPath("/Files/Upload/Surat Masuk/8.LHP/");
                    FileNameWithoutExtension = Path.GetFileNameWithoutExtension(h);
                    FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                    Request.Files[4].SaveAs(FilePath);
                    sm.LampiranLHP = FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                }
                else
                {
                    sm.LampiranLHP = "";
                }

                if (LampiranSPTJM != null)
                {
                    i = LampiranSPTJM.FileName;
                    FileExt = Path.GetExtension(i);
                    Ext = Server.MapPath("/Files/Upload/Surat Masuk/9.SPTJM/");
                    FileNameWithoutExtension = Path.GetFileNameWithoutExtension(i);
                    FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                    Request.Files[5].SaveAs(FilePath);
                    sm.LampiranSPTJM = FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                }
                else
                {
                    sm.LampiranSPTJM = "";
                }
                sm.CreateUser = UserID;
                sm.UsulStatus = UsulStatus;
                db.Insert(sm);

                int Status = 0;
                string DateFrom = Convert.ToDateTime(DateTime.Today).ToString("yyyy-MM-01");
                string DateTo = Convert.ToDateTime(DateTime.Today).ToString("yyyy-MM-dd"); ;
                return RedirectToAction("Index", "SuratMasuk",new { Status, DateFrom, DateTo });
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });
                
            }
        }

        [HttpPost]
        public ActionResult CreateTindakPidana(string NoAgenda, string NoSurat, string AsalSurat, string TanggalSurat,
            string perihal, string KODE_SATUAN_KERJA, string Kode_Unit_Kerja, HttpPostedFileBase LampiranSurat1,
            HttpPostedFileBase LampiranSurat2, HttpPostedFileBase LampiranSurat3,
            HttpPostedFileBase LampiranSurat4,
            HttpPostedFileBase LampiranSurat5,
            HttpPostedFileBase LampiranSurat6,
            HttpPostedFileBase LampiranSurat_LHA,
            HttpPostedFileBase LampiranLHP,
            HttpPostedFileBase LampiranSPTJM,
            string UsulStatus)
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

                String FilePath = "";
                String FileExt = "";
                String GetDateTime = "";
                String Ext = "";
                String FileNameWithoutExtension = "";
                string a, b, c, d, e, f, g, h, i;
                GetDateTime = DateTime.Now.ToString("ddMMyyyy_hhmmss");
                string UserLogin = Session["Fullname"].ToString();
                clsSuratMasuk sm = new clsSuratMasuk();
                sm.NoAgenda = "";
                sm.NoSurat = NoSurat;
                sm.AsalSurat = AsalSurat;
                sm.TanggalSurat = TanggalSurat;
                sm.perihal = perihal;
                sm.Kode_Unit_Kerja = Kode_Unit_Kerja;

                if (KODE_SATUAN_KERJA == null && UserGroup == "03")
                {
                    sm.SATUAN_KERJA = SatuanKerja;
                }
                else
                {
                    sm.SATUAN_KERJA = KODE_SATUAN_KERJA;
                }
                if (LampiranSurat1 != null)
                {
                    a = LampiranSurat1.FileName;
                    FileExt = Path.GetExtension(a);
                    Ext = Server.MapPath("/Files/Upload/Surat Masuk/1.Surat Pengantar/");
                    FileNameWithoutExtension = Path.GetFileNameWithoutExtension(a);
                    FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                    Request.Files[0].SaveAs(FilePath);
                    sm.LampiranSurat1 = FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                }
                else
                {
                    sm.LampiranSurat1 = "";
                }
                if (LampiranSurat2 != null)
                {
                    b = LampiranSurat2.FileName;
                    FileExt = Path.GetExtension(b);
                    Ext = Server.MapPath("/Files/Upload/Surat Masuk/2.BAP/");
                    FileNameWithoutExtension = Path.GetFileNameWithoutExtension(b);
                    FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                    Request.Files[1].SaveAs(FilePath);
                    sm.LampiranSurat2 = FileNameWithoutExtension + "_" + GetDateTime + FileExt;

                }
                else
                {
                    sm.LampiranSurat2 = "";
                }
                if (LampiranSurat3 != null)
                {
                    c = LampiranSurat3.FileName;
                    FileExt = Path.GetExtension(c);
                    Ext = Server.MapPath("/Files/Upload/Surat Masuk/3.Surat Tugas Pemeriksaan/");
                    FileNameWithoutExtension = Path.GetFileNameWithoutExtension(c);
                    FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                    Request.Files[2].SaveAs(FilePath);
                    sm.LampiranSurat3 = FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                }
                else
                {
                    sm.LampiranSurat3 = "";
                }
                if (LampiranSurat4 != null)
                {
                    d = LampiranSurat4.FileName;
                    FileExt = Path.GetExtension(d);
                    Ext = Server.MapPath("/Files/Upload/Surat Masuk/4.Surat Panggilan/");
                    FileNameWithoutExtension = Path.GetFileNameWithoutExtension(d);
                    FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                    Request.Files[3].SaveAs(FilePath);
                    sm.LampiranSurat4 = FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                }
                else
                {
                    sm.LampiranSurat4 = "";
                }
                if (LampiranSurat5 != null)
                {
                    e = LampiranSurat5.FileName;
                    FileExt = Path.GetExtension(e);
                    Ext = Server.MapPath("/Files/Upload/Surat Masuk/5.Dokumen Lainnya/");
                    FileNameWithoutExtension = Path.GetFileNameWithoutExtension(e);
                    FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                    Request.Files[6].SaveAs(FilePath);
                    sm.LampiranSurat5 = FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                }
                else
                {
                    sm.LampiranSurat5 = "";
                }
                if (LampiranSurat6 != null)
                {
                    f = LampiranSurat6.FileName;
                    FileExt = Path.GetExtension(f);
                    Ext = Server.MapPath("/Files/Upload/Surat Masuk/6.Surat Putusan Pengadilan/");
                    FileNameWithoutExtension = Path.GetFileNameWithoutExtension(f);
                    FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                    Request.Files[1].SaveAs(FilePath);
                    sm.LampiranSurat6 = FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                }
                else
                {
                    sm.LampiranSurat6 = "";
                }
                if (LampiranSurat_LHA != null)
                {
                    g = LampiranSurat_LHA.FileName;
                    FileExt = Path.GetExtension(g);
                    Ext = Server.MapPath("/Files/Upload/Surat Masuk/7.LHA/");
                    FileNameWithoutExtension = Path.GetFileNameWithoutExtension(g);
                    FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                    Request.Files[0].SaveAs(FilePath);
                    sm.LampiranSurat_LHA = FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                }
                else
                {
                    sm.LampiranSurat_LHA = "";
                }
                if (LampiranLHP != null)
                {
                    h = LampiranLHP.FileName;
                    FileExt = Path.GetExtension(h);
                    Ext = Server.MapPath("/Files/Upload/Surat Masuk/8.LHP/");
                    FileNameWithoutExtension = Path.GetFileNameWithoutExtension(h);
                    FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                    Request.Files[4].SaveAs(FilePath);
                    sm.LampiranLHP = FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                }
                else
                {
                    sm.LampiranLHP = "";
                }

                if (LampiranSPTJM != null)
                {
                    i = LampiranSPTJM.FileName;
                    FileExt = Path.GetExtension(i);
                    Ext = Server.MapPath("/Files/Upload/Surat Masuk/9.SPTJM/");
                    FileNameWithoutExtension = Path.GetFileNameWithoutExtension(i);
                    FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                    Request.Files[5].SaveAs(FilePath);
                    sm.LampiranSPTJM = FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                }
                else
                {
                    sm.LampiranSPTJM = "";
                }
                sm.CreateUser = UserID;
                sm.UsulStatus = UsulStatus;
                db.Insert(sm);

                int Status = 0;
                string DateFrom = Convert.ToDateTime(DateTime.Today).ToString("yyyy-MM-01");
                string DateTo = Convert.ToDateTime(DateTime.Today).ToString("yyyy-MM-dd"); ;
                return RedirectToAction("Index", "SuratMasuk", new { Status, DateFrom, DateTo });
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });

            }
        }
        [HttpGet]
        public ActionResult Edit(int ID, string KODE_SATUAN_KERJA, string Kode_Unit_Kerja)
        {
            if (Session["Fullname"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            try
            {
                clsSuratMasukDB db = new clsSuratMasukDB();
                clsSuratMasuk sm = db.GetList(ID);

                string SatuanKerja = Session["Satker"].ToString();
                string StatusAdmin = Session["StatusAdmin"].ToString();
                string UserGroup = Session["UserGroup"].ToString();
                string userlogin = Session["Fullname"].ToString();
                string UserID = Session["UserID"].ToString();
                ViewBag.UserID = userlogin;
                ViewBag.SatuanKerja = SatuanKerja;
                ViewBag.UserGroup = UserGroup;
                ViewBag.TanggalSurat = sm.TanggalSurat;
                
                ViewBag.Satker = new SelectList(db.GetListSatker(), "KODE_SATUAN_KERJA", "SATUAN_KERJA", KODE_SATUAN_KERJA);
                ViewBag.Unit_Kerja = new SelectList(db.GetUnit(KODE_SATUAN_KERJA), "Kode_Unit_Kerja", "Unit_Kerja", Kode_Unit_Kerja);

                return View(sm);
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });

            }

        }

        [HttpGet]
        public ActionResult EditTindakPidana(int ID, string KODE_SATUAN_KERJA, string Kode_Unit_Kerja)
        {
            if (Session["Fullname"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            try
            { 
                clsSuratMasukDB db = new clsSuratMasukDB();
                clsSuratMasuk sm = db.GetList(ID); 

                string SatuanKerja = Session["Satker"].ToString();
                string StatusAdmin = Session["StatusAdmin"].ToString();
                string UserGroup = Session["UserGroup"].ToString();
                string userlogin = Session["Fullname"].ToString();
                ViewBag.UserID = userlogin;
                ViewBag.SatuanKerja = SatuanKerja;
                ViewBag.UserGroup = UserGroup;
                ViewBag.Unit_Kerja = new SelectList(db.GetUnit(SatuanKerja), "Kode_Unit_Kerja", "Unit_Kerja", Kode_Unit_Kerja);
                ViewBag.Satker = new SelectList(db.GetListSatker(), "KODE_SATUAN_KERJA", "SATUAN_KERJA", KODE_SATUAN_KERJA);
                return View(sm);
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });                
            }
        }

        [HttpPost]
        public ActionResult Edit(int ID, string NoAgenda, string NoSurat, string AsalSurat, string TanggalSurat, 
            string perihal, string KODE_SATUAN_KERJA, string Kode_Unit_Kerja, HttpPostedFileBase LampiranSurat1,
            HttpPostedFileBase LampiranSurat2, HttpPostedFileBase LampiranSurat3,
            HttpPostedFileBase LampiranSurat4, 
            HttpPostedFileBase LampiranSurat5, 
            HttpPostedFileBase LampiranSurat6, 
            HttpPostedFileBase LampiranSurat_LHA,
            HttpPostedFileBase LampiranLHP,
            HttpPostedFileBase LampiranSPTJM,
            string UsulStatus)
        {
            string FilePath = "";
            string FileExt = "";
            string GetDateTime = "";
            string Ext = "";
            string FileNameWithoutExtension = "";
            string a, b, c, d, e, f,g,h,i;

            if (Session["Fullname"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            try
            {
                clsSuratMasukDB db = new clsSuratMasukDB();
                GetDateTime = DateTime.Now.ToString("ddMMyyyy_hhmmss");
                string UserLogin = Session["Fullname"].ToString();
                string UserID = Session["UserID"].ToString();

                clsSuratMasuk sm = new clsSuratMasuk();
                sm.ID = ID;
                sm.NoAgenda = NoAgenda;
                sm.NoSurat = NoSurat;
                sm.AsalSurat = AsalSurat;
                sm.TanggalSurat = TanggalSurat;
                sm.perihal = perihal;
                sm.SATUAN_KERJA = KODE_SATUAN_KERJA;
                sm.Kode_Unit_Kerja = Kode_Unit_Kerja;
                sm.UsulStatus = UsulStatus;
                clsSuratMasuk xx = db.GetList(ID);
                string FullPath = "";
                if (LampiranSurat1 != null)
                {
                    a = LampiranSurat1.FileName;
                    FileExt = Path.GetExtension(a);
                    Ext = Server.MapPath("/Files/Upload/Surat Masuk/1.Surat Pengantar/");
                    FileNameWithoutExtension = Path.GetFileNameWithoutExtension(a);
                    FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                    Request.Files[0].SaveAs(FilePath);
                    sm.LampiranSurat1 = FileNameWithoutExtension + "_" + GetDateTime + FileExt;

                    FullPath = Ext + xx.LampiranSurat1;
                    if (System.IO.File.Exists(FullPath))
                    {
                        System.IO.File.Delete(FullPath);
                    }
                }
                else
                {
                    sm.LampiranSurat1 = "";
                }
                if (LampiranSurat2 != null)
                {
                    b = LampiranSurat2.FileName;
                    FileExt = Path.GetExtension(b);
                    Ext = Server.MapPath("/Files/Upload/Surat Masuk/2.BAP/");
                    FileNameWithoutExtension = Path.GetFileNameWithoutExtension(b);
                    FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                    Request.Files[1].SaveAs(FilePath);
                    sm.LampiranSurat2 = FileNameWithoutExtension + "_" + GetDateTime + FileExt;

                    FullPath = Ext + xx.LampiranSurat2;
                    if (System.IO.File.Exists(FullPath))
                    {
                        System.IO.File.Delete(FullPath);
                    }
                }
                else
                {
                    sm.LampiranSurat2 = "";
                }
                if (LampiranSurat3 != null)
                {
                    c = LampiranSurat3.FileName;
                    FileExt = Path.GetExtension(c);
                    Ext = Server.MapPath("/Files/Upload/Surat Masuk/3.Surat Tugas Pemeriksaan/");
                    FileNameWithoutExtension = Path.GetFileNameWithoutExtension(c);
                    FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                    Request.Files[2].SaveAs(FilePath);
                    sm.LampiranSurat3 = FileNameWithoutExtension + "_" + GetDateTime + FileExt;

                    FullPath = Ext + xx.LampiranSurat3;
                    if (System.IO.File.Exists(FullPath))
                    {
                        System.IO.File.Delete(FullPath);
                    }
                }
                else
                {
                    sm.LampiranSurat3 = "";
                }
                if (LampiranSurat4 != null)
                {
                    d = LampiranSurat4.FileName;
                    FileExt = Path.GetExtension(d);
                    Ext = Server.MapPath("/Files/Upload/Surat Masuk/4.Surat Panggilan/");
                    FileNameWithoutExtension = Path.GetFileNameWithoutExtension(d);
                    FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                    Request.Files[3].SaveAs(FilePath);
                    sm.LampiranSurat4 = FileNameWithoutExtension + "_" + GetDateTime + FileExt;

                    FullPath = Ext + xx.LampiranSurat4;
                    if (System.IO.File.Exists(FullPath))
                    {
                        System.IO.File.Delete(FullPath);
                    }
                }
                else
                {
                    sm.LampiranSurat4 = "";
                }
                if (LampiranSurat5 != null)
                {
                    e = LampiranSurat5.FileName;
                    FileExt = Path.GetExtension(e);
                    Ext = Server.MapPath("/Files/Upload/Surat Masuk/5.Dokumen Lainnya/");
                    FileNameWithoutExtension = Path.GetFileNameWithoutExtension(e);
                    FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                    Request.Files[6].SaveAs(FilePath);
                    sm.LampiranSurat5 = FileNameWithoutExtension + "_" + GetDateTime + FileExt;

                    FullPath = Ext + xx.LampiranSurat5;
                    if (System.IO.File.Exists(FullPath))
                    {
                        System.IO.File.Delete(FullPath);
                    }
                }
                else
                {
                    sm.LampiranSurat5 = "";
                }
                if (LampiranSurat6 != null)
                {
                    f = LampiranSurat6.FileName;
                    FileExt = Path.GetExtension(f);
                    Ext = Server.MapPath("/Files/Upload/Surat Masuk/6.Surat Putusan Pengadilan/");
                    FileNameWithoutExtension = Path.GetFileNameWithoutExtension(f);
                    FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                    Request.Files[0].SaveAs(FilePath);
                    sm.LampiranSurat6 = FileNameWithoutExtension + "_" + GetDateTime + FileExt;

                    FullPath = Ext + xx.LampiranSurat6;
                    if (System.IO.File.Exists(FullPath))
                    {
                        System.IO.File.Delete(FullPath);
                    }
                }
                else
                {
                    sm.LampiranSurat6 = "";
                }
                if (LampiranSurat_LHA != null)
                {
                    g = LampiranSurat_LHA.FileName;
                    FileExt = Path.GetExtension(g);
                    Ext = Server.MapPath("/Files/Upload/Surat Masuk/7.LHA/");
                    FileNameWithoutExtension = Path.GetFileNameWithoutExtension(g);
                    FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                    Request.Files[0].SaveAs(FilePath);
                    sm.LampiranSurat_LHA = FileNameWithoutExtension + "_" + GetDateTime + FileExt;

                    FullPath = Ext + xx.LampiranSurat_LHA;
                    if (System.IO.File.Exists(FullPath))
                    {
                        System.IO.File.Delete(FullPath);
                    }
                }
                else
                {
                    sm.LampiranSurat_LHA = "";
                }
                if (LampiranLHP != null)
                {
                    h = LampiranLHP.FileName;
                    FileExt = Path.GetExtension(h);
                    Ext = Server.MapPath("/Files/Upload/Surat Masuk/8.LHP/");
                    FileNameWithoutExtension = Path.GetFileNameWithoutExtension(h);
                    FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                    Request.Files[4].SaveAs(FilePath);
                    sm.LampiranLHP = FileNameWithoutExtension + "_" + GetDateTime + FileExt;

                    FullPath = Ext + xx.LampiranLHP;
                    if (System.IO.File.Exists(FullPath))
                    {
                        System.IO.File.Delete(FullPath);
                    }
                }
                else
                {
                    sm.LampiranLHP = "";
                }
                if (LampiranSPTJM != null)
                {
                    i = LampiranSPTJM.FileName;
                    FileExt = Path.GetExtension(i);
                    Ext = Server.MapPath("/Files/Upload/Surat Masuk/9.SPTJM/");
                    FileNameWithoutExtension = Path.GetFileNameWithoutExtension(i);
                    FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                    Request.Files[5].SaveAs(FilePath);
                    sm.LampiranSPTJM = FileNameWithoutExtension + "_" + GetDateTime + FileExt;

                    FullPath = Ext + xx.LampiranSPTJM;
                    if (System.IO.File.Exists(FullPath))
                    {
                        System.IO.File.Delete(FullPath);
                    }
                }
                else
                {
                    sm.LampiranSPTJM = "";
                }

                sm.UpdatUser = UserID;                
                db.Edit(sm);

                int Status = 0;
                string DateFrom = Convert.ToDateTime(DateTime.Today).ToString("yyyy-01-01");
                string DateTo = Convert.ToDateTime(DateTime.Today).ToString("yyyy-MM-dd"); ;
                return RedirectToAction("Index", "SuratMasuk", new { Status, DateFrom, DateTo });
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message});
                
            }
        }
        [HttpPost]
        public ActionResult EditTindakPidana(int ID, string NoAgenda, string NoSurat, string AsalSurat, string TanggalSurat,
           string perihal, string KODE_SATUAN_KERJA, string Kode_Unit_Kerja, HttpPostedFileBase LampiranSurat1,
           HttpPostedFileBase LampiranSurat2, HttpPostedFileBase LampiranSurat3,
           HttpPostedFileBase LampiranSurat4,
           HttpPostedFileBase LampiranSurat5,
           HttpPostedFileBase LampiranSurat6,
           HttpPostedFileBase LampiranSurat_LHA,
           HttpPostedFileBase LampiranLHP,
           HttpPostedFileBase LampiranSPTJM,
           string UsulStatus)
        {
            string FilePath = "";
            string FileExt = "";
            string GetDateTime = "";
            string Ext = "";
            string FileNameWithoutExtension = "";
            string a, b, c, d, e, f, g, h, i;

            if (Session["Fullname"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            try
            {
                clsSuratMasukDB db = new clsSuratMasukDB();
                GetDateTime = DateTime.Now.ToString("ddMMyyyy_hhmmss");
                string UserLogin = Session["Fullname"].ToString();
                string UserID = Session["UserID"].ToString();

                clsSuratMasuk sm = new clsSuratMasuk();
                sm.ID = ID;
                sm.NoAgenda = NoAgenda;
                sm.NoSurat = NoSurat;
                sm.AsalSurat = AsalSurat;
                sm.TanggalSurat = TanggalSurat;
                sm.perihal = perihal;
                sm.SATUAN_KERJA = KODE_SATUAN_KERJA;
                sm.Kode_Unit_Kerja = Kode_Unit_Kerja;
                sm.UsulStatus = UsulStatus;
                clsSuratMasuk xx = db.GetList(ID);
                string FullPath = "";
                if (LampiranSurat1 != null)
                {
                    a = LampiranSurat1.FileName;
                    FileExt = Path.GetExtension(a);
                    Ext = Server.MapPath("/Files/Upload/Surat Masuk/1.Surat Pengantar/");
                    FileNameWithoutExtension = Path.GetFileNameWithoutExtension(a);
                    FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                    Request.Files[0].SaveAs(FilePath);
                    sm.LampiranSurat1 = FileNameWithoutExtension + "_" + GetDateTime + FileExt;

                    FullPath = Ext + xx.LampiranSurat1;
                    if (System.IO.File.Exists(FullPath))
                    {
                        System.IO.File.Delete(FullPath);
                    }
                }
                else
                {
                    sm.LampiranSurat1 = "";
                }
                if (LampiranSurat2 != null)
                {
                    b = LampiranSurat2.FileName;
                    FileExt = Path.GetExtension(b);
                    Ext = Server.MapPath("/Files/Upload/Surat Masuk/2.BAP/");
                    FileNameWithoutExtension = Path.GetFileNameWithoutExtension(b);
                    FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                    Request.Files[1].SaveAs(FilePath);
                    sm.LampiranSurat2 = FileNameWithoutExtension + "_" + GetDateTime + FileExt;

                    FullPath = Ext + xx.LampiranSurat2;
                    if (System.IO.File.Exists(FullPath))
                    {
                        System.IO.File.Delete(FullPath);
                    }
                }
                else
                {
                    sm.LampiranSurat2 = "";
                }
                if (LampiranSurat3 != null)
                {
                    c = LampiranSurat3.FileName;
                    FileExt = Path.GetExtension(c);
                    Ext = Server.MapPath("/Files/Upload/Surat Masuk/3.Surat Tugas Pemeriksaan/");
                    FileNameWithoutExtension = Path.GetFileNameWithoutExtension(c);
                    FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                    Request.Files[2].SaveAs(FilePath);
                    sm.LampiranSurat3 = FileNameWithoutExtension + "_" + GetDateTime + FileExt;

                    FullPath = Ext + xx.LampiranSurat3;
                    if (System.IO.File.Exists(FullPath))
                    {
                        System.IO.File.Delete(FullPath);
                    }
                }
                else
                {
                    sm.LampiranSurat3 = "";
                }
                if (LampiranSurat4 != null)
                {
                    d = LampiranSurat4.FileName;
                    FileExt = Path.GetExtension(d);
                    Ext = Server.MapPath("/Files/Upload/Surat Masuk/4.Surat Panggilan/");
                    FileNameWithoutExtension = Path.GetFileNameWithoutExtension(d);
                    FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                    Request.Files[3].SaveAs(FilePath);
                    sm.LampiranSurat4 = FileNameWithoutExtension + "_" + GetDateTime + FileExt;

                    FullPath = Ext + xx.LampiranSurat4;
                    if (System.IO.File.Exists(FullPath))
                    {
                        System.IO.File.Delete(FullPath);
                    }
                }
                else
                {
                    sm.LampiranSurat4 = "";
                }
                if (LampiranSurat5 != null)
                {
                    e = LampiranSurat5.FileName;
                    FileExt = Path.GetExtension(e);
                    Ext = Server.MapPath("/Files/Upload/Surat Masuk/5.Dokumen Lainnya/");
                    FileNameWithoutExtension = Path.GetFileNameWithoutExtension(e);
                    FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                    Request.Files[6].SaveAs(FilePath);
                    sm.LampiranSurat5 = FileNameWithoutExtension + "_" + GetDateTime + FileExt;

                    FullPath = Ext + xx.LampiranSurat5;
                    if (System.IO.File.Exists(FullPath))
                    {
                        System.IO.File.Delete(FullPath);
                    }
                }
                else
                {
                    sm.LampiranSurat5 = "";
                }
                if (LampiranSurat6 != null)
                {
                    f = LampiranSurat6.FileName;
                    FileExt = Path.GetExtension(f);
                    Ext = Server.MapPath("/Files/Upload/Surat Masuk/6.Surat Putusan Pengadilan/");
                    FileNameWithoutExtension = Path.GetFileNameWithoutExtension(f);
                    FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                    Request.Files[1].SaveAs(FilePath);
                    sm.LampiranSurat6 = FileNameWithoutExtension + "_" + GetDateTime + FileExt;

                    FullPath = Ext + xx.LampiranSurat6;
                    if (System.IO.File.Exists(FullPath))
                    {
                        System.IO.File.Delete(FullPath);
                    }
                }
                else
                {
                    sm.LampiranSurat6 = "";
                }
                if (LampiranSurat_LHA != null)
                {
                    g = LampiranSurat_LHA.FileName;
                    FileExt = Path.GetExtension(g);
                    Ext = Server.MapPath("/Files/Upload/Surat Masuk/7.LHA/");
                    FileNameWithoutExtension = Path.GetFileNameWithoutExtension(g);
                    FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                    Request.Files[0].SaveAs(FilePath);
                    sm.LampiranSurat_LHA = FileNameWithoutExtension + "_" + GetDateTime + FileExt;

                    FullPath = Ext + xx.LampiranSurat_LHA;
                    if (System.IO.File.Exists(FullPath))
                    {
                        System.IO.File.Delete(FullPath);
                    }
                }
                else
                {
                    sm.LampiranSurat_LHA = "";
                }
                if (LampiranLHP != null)
                {
                    h = LampiranLHP.FileName;
                    FileExt = Path.GetExtension(h);
                    Ext = Server.MapPath("/Files/Upload/Surat Masuk/8.LHP/");
                    FileNameWithoutExtension = Path.GetFileNameWithoutExtension(h);
                    FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                    Request.Files[4].SaveAs(FilePath);
                    sm.LampiranLHP = FileNameWithoutExtension + "_" + GetDateTime + FileExt;

                    FullPath = Ext + xx.LampiranLHP;
                    if (System.IO.File.Exists(FullPath))
                    {
                        System.IO.File.Delete(FullPath);
                    }
                }
                else
                {
                    sm.LampiranLHP = "";
                }
                if (LampiranSPTJM != null)
                {
                    i = LampiranSPTJM.FileName;
                    FileExt = Path.GetExtension(i);
                    Ext = Server.MapPath("/Files/Upload/Surat Masuk/9.SPTJM/");
                    FileNameWithoutExtension = Path.GetFileNameWithoutExtension(i);
                    FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                    Request.Files[5].SaveAs(FilePath);
                    sm.LampiranSPTJM = FileNameWithoutExtension + "_" + GetDateTime + FileExt;

                    FullPath = Ext + xx.LampiranSPTJM;
                    if (System.IO.File.Exists(FullPath))
                    {
                        System.IO.File.Delete(FullPath);
                    }
                }
                else
                {
                    sm.LampiranSPTJM = "";
                }

                sm.UpdatUser = UserID;
                db.Edit(sm);

                int Status = 0;
                string DateFrom = Convert.ToDateTime(DateTime.Today).ToString("yyyy-01-01");
                string DateTo = Convert.ToDateTime(DateTime.Today).ToString("yyyy-MM-dd"); ;
                return RedirectToAction("Index", "SuratMasuk", new { Status, DateFrom, DateTo });
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });

            }
        }
        public FileResult DownloadFileSPTJM(string fileName)
        {
            try
            {
                //Build the File Path.
                string path = Server.MapPath("/Files/Result/SPTJM/") + fileName;

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
        public FileResult DownloadFile1(string fileName)
        {
            try
            {
                //Build the File Path.
                string path = Server.MapPath("/Files/Upload/Surat Masuk/1.Surat Pengantar/") + fileName;

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
        public FileResult DownloadFile2(string fileName)
        {
            try
            {
                //Build the File Path.
                string path = Server.MapPath("/Files/Upload/Surat Masuk/2.BAP/") + fileName;

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
        public FileResult DownloadFile3(string fileName)
        {
            try
            {
                //Build the File Path.
                string path = Server.MapPath("/Files/Upload/Surat Masuk/3.Surat Tugas Pemeriksaan/") + fileName;

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
        public FileResult DownloadFile4(string fileName)
        {
            try
            { 
            //Build the File Path.
            string path = Server.MapPath("/Files/Upload/Surat Masuk/4.Surat Panggilan/") + fileName;

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
        public FileResult DownloadFile5(string fileName)
        {
            try
            { 
            //Build the File Path.
            string path = Server.MapPath("/Files/Upload/Surat Masuk/5.Dokumen Lainnya/") + fileName;

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
        public FileResult DownloadFile6(string fileName)
        {
            try
            {
                //Build the File Path.
                string path = Server.MapPath("/Files/Upload/Surat Masuk/6.Surat Putusan Pengadilan/") + fileName;

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
        public FileResult DownloadFile7(string fileName)
        {
            try
            {
                //Build the File Path.
                string path = Server.MapPath("/Files/Upload/Surat Masuk/7.LHA/") + fileName;

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
        public FileResult DownloadFile8(string fileName)
        {
            try
            {
                //Build the File Path.
                string path = Server.MapPath("/Files/Upload/Surat Masuk/8.LHP/") + fileName;

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
        public FileResult DownloadFile9(string fileName)
        {
            try
            {
                //Build the File Path.
                string path = Server.MapPath("/Files/Upload/Surat Masuk/9.SPTJM/") + fileName;

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
        [HttpPost]
        public ActionResult Delete(int ID, string Status, string TanggalSuratFrom, string TanggalSuratTo)
        {
            string strMsg = "";
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
        public ActionResult Proses(int ID)
        {
            string strMsg = "";
            if (Session["Fullname"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            string userlogin = Session["Fullname"].ToString();
            string SatuanKerja = Session["Satker"].ToString();
            string StatusAdmin = Session["StatusAdmin"].ToString();
            string UserGroup = Session["UserGroup"].ToString();
            string UserID = Session["UserID"].ToString();
            ViewBag.UserID = userlogin;
            ViewBag.SatuanKerja = SatuanKerja;
            ViewBag.UserGroup = UserGroup;
            try
            {
                clsSuratMasuk a = new clsSuratMasuk();
                clsSuratMasukMsgInfo b = new clsSuratMasukMsgInfo();

                a.ID = ID;
                a.UpdatUser = UserID;
                db.Proses(a);

                clsSuratMasuk xx = db.GetList(ID);
                SendWaBlas(ID);

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
        [HttpPost]
        public JsonResult UploadSuratPengantar()
        {
            string userlogin = Session["Fullname"].ToString();
            string SatuanKerja = Session["Satker"].ToString();
            string StatusAdmin = Session["StatusAdmin"].ToString();
            string UserGroup = Session["UserGroup"].ToString();
            ViewBag.UserID = userlogin;
            ViewBag.SatuanKerja = SatuanKerja;
            ViewBag.UserGroup = UserGroup;
            string msg = "";
            try
            {
                String FilePath = "";
                String FileName = "";
                String FileExt = "";
                String GetDateTime = "";
                String Ext = "";
                String FileNameWithoutExtension = "";                

                GetDateTime = DateTime.Now.ToString("ddMMyyyy_hhmmss");
                FileName = Request.Files[0].FileName;
                FileExt = Path.GetExtension(FileName);
                Ext = Server.MapPath("/Files/Upload/Surat Masuk/1.Surat Pengantar/");
                FileNameWithoutExtension = Path.GetFileNameWithoutExtension(FileName);
                FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                Request.Files[0].SaveAs(FilePath);
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GenerateWord(int ID, string NoSurat, string AsalSurat, string TanggalSurat,
            string perihal, string SATUAN_KERJA, string LampiranSurat1,
            string LampiranSurat2, string LampiranSurat3,
            string LampiranSurat4, string LampiranSurat5, string LampiranSurat6, string LampiranSurat_LHA,
            string DisposisiBy1, string DisposisiBy2, string DisposisiBy3,
            string DisposisiStatus1, string DisposisiStatus2, string DisposisiStatus3,
            string DisposisiDate1, string DisposisiDate2, string DisposisiDate3,
            string Catatan1, string Catatan2, string Catatan3)
        {
            string filepath = HttpContext.Request.PhysicalApplicationPath;
            string strTemplate = filepath + "Files/Template/Template_Detail_Surat_Masuk.docx";
            string OutputPath = filepath + "Files/Result/Detail Surat Masuk/";
            string GetDateTime = DateTime.Now.ToString("ddMMyyyy_hhmmss");
            string strFileNameDoc = "Detail_Surat_Masuk_" + GetDateTime + ".docx";
            string strFileNamePDF = "Detail_Surat_Masuk_"+ GetDateTime + ".pdf";
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

                //clsSuratMasuk sm = new clsSuratMasuk();
                clsSuratMasuk sm = db.GetList(ID);
                if (sm.LampiranSurat1 == null || sm.LampiranSurat1 == "")
                {
                    LampiranSurat1 = "×";
                }
                else
                {
                    LampiranSurat1 = "✓";
                }
                if (sm.LampiranSurat2 == null || sm.LampiranSurat2 == "")
                {
                    LampiranSurat2 = "×";
                }
                else
                {
                    LampiranSurat2 = "✓";
                }
                if (sm.LampiranSurat3 == null || sm.LampiranSurat3 == "")
                {
                    LampiranSurat3 = "×";
                }
                else
                {
                    LampiranSurat3 = "✓";
                }
                if (sm.LampiranSurat4 == null || sm.LampiranSurat4 == "")
                {
                    LampiranSurat4 = "×";
                }
                else
                {
                    LampiranSurat4 = "✓";
                }
                if (sm.LampiranSurat5 == null || sm.LampiranSurat5 == "")
                {
                    LampiranSurat5 = "×";
                }
                else
                {
                    LampiranSurat5 = "✓";
                }
                if (sm.LampiranSurat6 == null || sm.LampiranSurat6 == "")
                {
                    LampiranSurat6 = "×";
                }
                else
                {
                    LampiranSurat6 = "✓";
                }
                if (sm.LampiranSurat_LHA == null || sm.LampiranSurat_LHA == "")
                {
                    LampiranSurat_LHA = "×";
                }
                else
                {
                    LampiranSurat_LHA = "✓";
                }

                if (DisposisiBy1 == null )
                {
                    DisposisiBy1 = "";
                }
                if (DisposisiBy2 == null)
                {
                    DisposisiBy2 = "";
                }
                if (DisposisiBy3 == null)
                {
                    DisposisiBy3 = "";
                }
                if (DisposisiDate1 == null)
                {
                    DisposisiDate1 = "";
                }
                if (DisposisiDate2 == null)
                {
                    DisposisiDate2 = "";
                }
                if (DisposisiDate3 == null)
                {
                    DisposisiDate3 = "";
                }

                if (DisposisiStatus1 == null)
                {
                    DisposisiStatus1 = "";
                }
                if (DisposisiStatus2 == null)
                {
                    DisposisiStatus2 = "";
                }
                if (DisposisiStatus3 == null)
                {
                    DisposisiStatus3 = "";
                }
                if (Catatan1 == null)
                {
                    Catatan1 = "";
                }
                if (Catatan2 == null)
                {
                    Catatan2 = "";
                }
                if (Catatan3 == null)
                {
                    Catatan3 = "";
                }
                sm.NoSurat              = NoSurat;
                sm.AsalSurat            = AsalSurat;
                sm.TanggalSurat         = TanggalSurat;
                sm.perihal              = perihal;
                sm.SATUAN_KERJA         = SATUAN_KERJA;
                sm.LampiranSurat1       = LampiranSurat1;
                sm.LampiranSurat2       = LampiranSurat2;
                sm.LampiranSurat3       = LampiranSurat3;
                sm.LampiranSurat4       = LampiranSurat4;
                sm.LampiranSurat5       = LampiranSurat5;
                sm.LampiranSurat6       = LampiranSurat6;
                sm.LampiranSurat_LHA    = LampiranSurat_LHA;
                sm.DisposisiBy1         = DisposisiBy1;
                sm.DisposisiBy2         = DisposisiBy2;
                sm.DisposisiBy3         = DisposisiBy3;
                sm.DisposisiDate1       = DisposisiDate1;
                sm.DisposisiDate2       = DisposisiDate2;
                sm.DisposisiDate3       = DisposisiDate3;
                sm.DisposisiStatus1     = DisposisiStatus1;
                sm.DisposisiStatus2     = DisposisiStatus2;
                sm.DisposisiStatus3     = DisposisiStatus3;
                sm.Catatan1             = Catatan1;
                sm.Catatan2             = Catatan2;
                sm.Catatan3             = Catatan3;
                Document doc = new Document();
                doc.LoadFromFile(strTemplate);
                //get strings to replace
                Dictionary<string, string> dictReplace = GetReplaceDictionary(sm);
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
                
                ToViewFile(OutputPath + strFileNameDoc);
                strMsg = "Success";
                return Json(strMsg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                strMsg = ex.Message.ToString();
                return Json(strMsg, JsonRequestBehavior.AllowGet);
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
        Dictionary<string, string> GetReplaceDictionary(clsSuratMasuk sm)
        {
            Dictionary<string, string> replaceDict = new Dictionary<string, string>();
            replaceDict.Add("*NoSurat*", sm.NoSurat);
            replaceDict.Add("*AsalSurat*", sm.AsalSurat);
            replaceDict.Add("*TanggalSurat*", Convert.ToString(sm.TanggalSurat));
            replaceDict.Add("*Perihal*", sm.perihal);
            replaceDict.Add("*SatuanKerja*", sm.SATUAN_KERJA);
            replaceDict.Add("*LampiranSuratPengantar*", sm.LampiranSurat1);
            replaceDict.Add("*LampiranBAP*", sm.LampiranSurat2);
            replaceDict.Add("*LampiranSuratTugasPemeriksaan*", sm.LampiranSurat3);
            replaceDict.Add("*LampiranSuratPanggilan*", sm.LampiranSurat4);
            replaceDict.Add("*LampiranDokumenLainnya*", sm.LampiranSurat5);
            replaceDict.Add("*LampiranPutusanPengadilan*", sm.LampiranSurat6);
            replaceDict.Add("*LampiranLHA*", sm.LampiranSurat_LHA);
            replaceDict.Add("*Disposisi1Oleh*", sm.DisposisiBy1);
            replaceDict.Add("*TanggalDisposisi1*", sm.DisposisiDate1);
            replaceDict.Add("*StatusDisposisi1*", sm.DisposisiStatus1);
            replaceDict.Add("*CatatanDisposisi1*", sm.Catatan1);
            replaceDict.Add("*Disposisi2Oleh*", sm.DisposisiBy2);
            replaceDict.Add("*TanggalDisposisi2*", sm.DisposisiDate2);
            replaceDict.Add("*Status Disposisi2*", sm.DisposisiStatus2);
            replaceDict.Add("*CatatanDisposisi2*", sm.Catatan2);
            replaceDict.Add("*Disposisi3Oleh*", sm.DisposisiBy3);
            replaceDict.Add("*TanggalDisposisi3*", sm.DisposisiDate3);
            replaceDict.Add("*StatusDisposisi3*", sm.DisposisiStatus3);
            replaceDict.Add("*CatatanDisposisi3*", sm.Catatan3);


            return replaceDict;
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
                clsToken db = new clsToken();
                string fname;
                string strToken = "";
                string Username = "agus@kemenag.go.id";
                string Password = "12345678";
                string baseAddress = "https://api.kemenag.go.id/v1/";
                var client = new HttpClient();
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
        Dictionary<string, string> ReplaceWord(clsSPTMJ a)
        {
            Dictionary<string, string> replaceDict = new Dictionary<string, string>();
            replaceDict.Add("*NAMA*", a.NAMA_LENGKAP);
            replaceDict.Add("*NIP*", a.NIP);
            replaceDict.Add("*PANGKAT*", a.GOL_RUANG);
            replaceDict.Add("*JABATAN*", a.LEVEL_JABATAN);
            replaceDict.Add("*SATUAN_KERJA*", a.Satker);
            replaceDict.Add("*Nama_yang_diusulkan*", a.NAMA_LENGKAP_USUL);
            replaceDict.Add("*NIP_yang_diusulkan*", a.NIP_USUL);
            replaceDict.Add("*Tanggal_pembuatan_surat*", a.Tanggal_Surat);

            return replaceDict;
        }
        public ActionResult SendWaBlas(int ID)
        {
            try
            {
                clsSuratMasukMsgInfo Msg = new clsSuratMasukMsgInfo();
                int StatusSM = 1;
                Msg = db.GetMsgInfo(ID, StatusSM);
                if (Msg.PhoneNo != "" || Msg.PhoneNo != null)
                {
                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage resp = client.GetAsync(baseAddress + "api/send-message?source=postman&phone=" 
                        + WebUtility.UrlEncode(Msg.PhoneNo) + "&message=" + WebUtility.UrlEncode(Msg.Pesan)
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
    }
}