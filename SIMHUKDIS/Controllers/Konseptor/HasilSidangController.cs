using SIMHUKDIS.Models;
using Spire.Doc;
using System;
using System.Collections.Generic;
using System.IO;
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
        string strTemplate;
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
                ViewBag.Hukdis = new SelectList(x.GetListHukdis(), "ID", "HukdisDesc");
                ViewBag.SuratNo = new SelectList(db.GetSuratNo(UserID), "ID", "NoSurat");
                return View();
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
                
                clsHasilSidangDB db = new clsHasilSidangDB();
                HasilSidangDtl hasil = db.GetListCreate(ID, NIP);
                ViewBag.Hukdis = new SelectList(x.GetListHukdis(), "ID", "HukdisDesc", hasil.KeputusanSidang);
                //if (hasil.Mengingat == null || hasil.Mengingat == "")
                //{
                //    hasil.Mengingat = db.GetPasalPelanggaran();
                //}
                //if (hasil.Tembusan == null || hasil.Tembusan == "")
                //{
                //    hasil.Tembusan = db.GetTembusan(hasil.KODE_SATUAN_KERJA, hasil.Kode_Unit_Kerja);
                //}
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
        [HttpGet]
        public ActionResult KirimData(int ID, string NIP)
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

                ViewBag.StatusAdmin = StatusAdmin;
                ViewBag.UserID = userlogin;
                ViewBag.SatuanKerja = SatuanKerja;
                ViewBag.UserGroup = UserGroup;

                clsHasilSidangDB db = new clsHasilSidangDB();
                HasilSidangDtl hasil = db.GetListCreate(ID, NIP);

                return View(hasil);
            }
        }
        [HttpPost]
        public ActionResult KirimData(string ID, string NIP, string NO_SK, string Tanggal_SK, HttpPostedFileBase FILE_SK, string Keterangan_SK,
         string Tanggal_Penyampaian_Ke_Satker, string Penerima_Satker, HttpPostedFileBase BAP_Satker, string Keterangan_Satker,
         string Tanggal_Penyampaian_Ke_YBS, string Penerima_YBS, HttpPostedFileBase BAP_Penerima, string Keterangan_Penerima)
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
                String FilePath = "";
                String FileExt = "";
                String GetDateTime = "";
                String Ext = "";
                String FileNameWithoutExtension = "";
                string a, b, c, d, e, f, g;
                GetDateTime = DateTime.Now.ToString("ddMMyyyy_hhmmss");
                string UserLogin = Session["Fullname"].ToString();
                HasilSidangDtl sm = new HasilSidangDtl();
                sm.ID = ID;
                sm.NIP = NIP;
                sm.NO_SK = NO_SK;
                sm.Tanggal_SK = Tanggal_SK;
                sm.Keterangan_SK = Keterangan_SK;
                sm.Penerima_Satker = Penerima_Satker;
                sm.Tanggal_Penyampaian_Ke_Satker = Tanggal_Penyampaian_Ke_Satker;
                sm.Tanggal_Penyampaian_Ke_YBS = Tanggal_Penyampaian_Ke_YBS;
                sm.Penerima_Satker = Penerima_Satker;
                sm.Keterangan_Satker = Keterangan_Satker;
                sm.Keterangan_Satker = Keterangan_Satker;
                sm.Penerima_YBS = Penerima_YBS; 
                sm.Keterangan_Penerima = Keterangan_Penerima; 
                
                if (FILE_SK != null)
                {
                    a = FILE_SK.FileName;
                    FileExt = Path.GetExtension(a);
                    Ext = Server.MapPath("/Files/Upload/SK/");
                    FileNameWithoutExtension = Path.GetFileNameWithoutExtension(a);
                    FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                    Request.Files[0].SaveAs(FilePath);
                    sm.FILE_SK = FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                }
                else
                {
                    sm.FILE_SK = "";
                }
                if (BAP_Satker != null)
                {
                    b = BAP_Satker.FileName;
                    FileExt = Path.GetExtension(b);
                    Ext = Server.MapPath("/Files/Upload/BAP SATKER/");
                    FileNameWithoutExtension = Path.GetFileNameWithoutExtension(b);
                    FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                    Request.Files[0].SaveAs(FilePath);
                    sm.BAP_Satker = FileNameWithoutExtension + "_" + GetDateTime + FileExt;

                }
                else
                {
                    sm.BAP_Satker = "";
                }
                if (BAP_Penerima != null)
                {
                    c = BAP_Penerima.FileName;
                    FileExt = Path.GetExtension(c);
                    Ext = Server.MapPath("/Files/Upload/BAP YBS/");
                    FileNameWithoutExtension = Path.GetFileNameWithoutExtension(c);
                    FilePath = Ext + FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                    Request.Files[0].SaveAs(FilePath);
                    sm.BAP_Penerima = FileNameWithoutExtension + "_" + GetDateTime + FileExt;
                }
                else
                {
                    sm.BAP_Penerima = "";
                }
                
                sm.CreateUser = UserLogin;
                if (db.cek(ID, NIP) == true)
                {
                    db.Ubah(sm);
                }
                else
                {
                    db.HasilSidang_Insert(sm);
                }
                

                int Status = 0;
                string DateFrom = Convert.ToDateTime(DateTime.Today).ToString("yyyy-MM-dd");
                string DateTo = Convert.ToDateTime(DateTime.Today).ToString("yyyy-MM-dd"); ;
                return RedirectToAction("Index", "HasilSidang", new { Status, DateFrom, DateTo });
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });

            }
        }
        public ActionResult UbahData(string ID, string NIP, string KeputusanSidang, string Tanggal_Sidang, string Catatan_Sidang,
            string DasarBukti, string Pelanggaran, string PasalPelanggaran, string Mengingat, string Tembusan, string NoSurat, string NAMA_LENGKAP)
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
                if (ID == null)
                {
                    PD.ID = NoSurat;
                }
                else
                {
                    PD.ID = ID;
                }
                
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
                if (Tanggal_Sidang != null) 
                
                {
                    DateTime dParse = DateTime.ParseExact(Tanggal_Sidang, "dd-MM-yyyy", null);
                    PD.Tanggal_Sidang_DPK = dParse.ToString();
                }

                if (db.GetDataExist(PD.ID, NIP) == true)
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

        public ActionResult Insert(string NIP, string KeputusanSidang,
            string Mengingat, string ID, string NAMA_LENGKAP, string TMTDate, string Tembusan)
        {
            string strMsg = "";
            try
            {
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
                PD.UserLogin = UserID;
                PD.Mengingat = Mengingat;
                PD.FullName = NAMA_LENGKAP;
                PD.TMTDate = TMTDate;
                PD.Tembusan = Tembusan;
                db.Insert(PD);
                
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

        [HttpPost]
        public ActionResult KirimSurat(string ID, string NIP, string NoSurat, string TanggalSurat, string FileSKS)
        {
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
                GetDateTime = DateTime.Now.ToString("dMMyyyy");

                clsKirimSurat a = new clsKirimSurat();
                a.UserID = UserID;
                a.ID = ID;
                a.NIP = NIP;
                a.NoSurat = NoSurat;
                a.TanggalSurat = TanggalSurat;
                a.Tipe = "2";
                DateTime tglSurat = DateTime.ParseExact(a.TanggalSurat, "dd MMMM yyyy", null);
                a.TanggalSurat = tglSurat.ToString();
                if (FileSKS == null)
                {
                    a.FileSKS = "";
                }
                else
                {
                    a.FileSKS = FileSKS;
                }

                clsHasilSidangDB db = new clsHasilSidangDB();
                db.KirimSKS(a);
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
        [HttpPost]
        public ActionResult UploadFiles()
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

            if (Request.Files.Count > 0)
            {
                try
                {
                    string FilePath = "";
                    string FileExt = "";
                    string GetDateTime = "";
                    string Ext = "";
                    string FileNameWithoutExtension = "";
                    string FileName = "";
                    string fname;
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

                        FileName = fname;
                        fname = Path.Combine(Server.MapPath("/Files/Upload/SK/"), fname);
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

        [HttpPost]
        public ActionResult UploadFilesKewenanganSatker()
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

            if (Request.Files.Count > 0)
            {
                try
                {
                    string FilePath = "";
                    string FileExt = "";
                    string GetDateTime = "";
                    string Ext = "";
                    string FileNameWithoutExtension = "";
                    string FileName = "";
                    string fname;
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

                        FileName = fname;
                        fname = Path.Combine(Server.MapPath("/Files/Upload/Kewenangan Satker/"), fname);
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

        public ActionResult GenerateSK(int ID,string NIP, string NAMA_LENGKAP, string KeputusanSidang, string GOL_RUANG, string LEVEL_JABATAN, string Satker,
            string Tembusan)
        {
            string filepath = HttpContext.Request.PhysicalApplicationPath;
            if (KeputusanSidang == "1")
            {
                strTemplate = filepath + "Files/Template/TEMPLATE TEGURAN LISAN.docx";
            }
            else if (KeputusanSidang == "2")
            {
                strTemplate = filepath + "Files/Template/TEMPLATE TEGURAN TERTULIS.docx";
            }
            else if (KeputusanSidang == "3")
            {
                strTemplate = filepath + "Files/Template/TEMPLATE PERNYATAAN TIDAK PUAS SECARA TERTULIS.docx";
            }
            else if (KeputusanSidang == "4")
            {
                strTemplate = filepath + "Files/Template/Template_Hukuman_Dengan_Sidang_DPK.docx";
            }
            else if (KeputusanSidang == "5")
            {
                strTemplate = filepath + "Files/Template/Template_Hukuman_Dengan_Sidang_DPK.docx";
            }
            else if (KeputusanSidang == "6")
            {
                strTemplate = filepath + "Files/Template/Template_Hukuman_Dengan_Sidang_DPK.docx";
            }
            else if (KeputusanSidang == "7")
            {
                strTemplate = filepath + "Files/Template/Template_Hukuman_Dengan_Sidang_DPK.docx";
            }
            else if (KeputusanSidang == "8")
            {
                strTemplate = filepath + "Files/Template/TEMPLATE PEMBEBASAN DARI JABATANNYA MENJADI JABATAN PELAKSANA SELAMA 12 BULAN.docx";
            }
            else if (KeputusanSidang == "9")
            {
                strTemplate = filepath + "Files/Template/TEMPLATE PEMBERHENTIAN DENGAN HORMAT TIDAK ATAS PERMINTAAN SENDIRI.docx";
            }
            else if (KeputusanSidang == "10")
            {
                strTemplate = filepath + "Files/Template/Template_Hukuman_Dengan_Sidang_DPK.docx";
            }
            else if (KeputusanSidang == "11")
            {
                strTemplate = filepath + "Files/Template/Template_Hukuman_Dengan_Sidang_DPK.docx";
            }
            else if (KeputusanSidang == "12")
            {
                strTemplate = filepath + "Files/Template/Template_Hukuman_Dengan_Sidang_DPK.docx";
            }
            else if (KeputusanSidang == "13")
            {
                strTemplate = filepath + "Files/Template/Template_Hukuman_Dengan_Sidang_DPK.docx";
            }
            else if (KeputusanSidang == "14")
            {
                strTemplate = filepath + "Files/Template/Template_Hukuman_Dengan_Sidang_DPK.docx";
            }
            else if (KeputusanSidang == "15")
            {
                strTemplate = filepath + "Files/Template/TEMPLATE PTDH TINDAK PIDANA.docx";
            }

            string OutputPath = filepath + "Files/Result/SK/";
            string GetDateTime = DateTime.Now.ToString("ddMMyyyy_hhmmss");
            string strFileNameDoc = "SK a.n " + NAMA_LENGKAP + " NIP " + NIP + "_" + GetDateTime + ".docx";
            string strFileNamePDF = "SK a.n " + NAMA_LENGKAP + " NIP " + NIP + "_" + GetDateTime + ".pdf";
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
                a = db.DBList(ID, NIP);

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
        public ActionResult GenerateWord(int ID, string NIP, string NAMA_LENGKAP, string KeputusanSidang)
        {
            
            string filepath = HttpContext.Request.PhysicalApplicationPath;
            if (KeputusanSidang == "1")
            {
                strTemplate = filepath + "Files/Template/TEMPLATE TEGURAN LISAN.docx";
            }
            else if (KeputusanSidang == "2")
            {
                strTemplate = filepath + "Files/Template/TEMPLATE TEGURAN TERTULIS.docx";
            }
            else if (KeputusanSidang == "3")
            {
                strTemplate = filepath + "Files/Template/TEMPLATE PERNYATAAN TIDAK PUAS SECARA TERTULIS.docx";
            }
            else if (KeputusanSidang == "4")
            {
                strTemplate = filepath + "Files/Template/Template_Hukuman_Dengan_Sidang_DPK.docx";
            }
            else if (KeputusanSidang == "5")
            {
                strTemplate = filepath + "Files/Template/Template_Hukuman_Dengan_Sidang_DPK.docx";
            }
            else if (KeputusanSidang == "6")
            {
                strTemplate = filepath + "Files/Template/Template_Hukuman_Dengan_Sidang_DPK.docx";
            }
            else if (KeputusanSidang == "7")
            {
                strTemplate = filepath + "Files/Template/TEMPLATE PJ 12 BULAN.docx";
            }
            else if (KeputusanSidang == "8")
            {
                strTemplate = filepath + "Files/Template/TEMPLATE PDJ.docx";
            }
            else if (KeputusanSidang == "9")
            {
                strTemplate = filepath + "Files/Template/TEMPLATE PEMBERHENTIAN DENGAN HORMAT TIDAK ATAS PERMINTAAN SENDIRI.docx";
            }
            else if (KeputusanSidang == "10")
            {
                strTemplate = filepath + "Files/Template/Template_Hukuman_Dengan_Sidang_DPK.docx";
            }
            else if (KeputusanSidang == "11")
            {
                strTemplate = filepath + "Files/Template/TEMPLATE PKP 1 TAHUN.docx";
            }
            else if (KeputusanSidang == "12")
            {
                strTemplate = filepath + "Files/Template/Template_Hukuman_Dengan_Sidang_DPK.docx";
            }
            else if (KeputusanSidang == "13")
            {
                strTemplate = filepath + "Files/Template/Template_Hukuman_Dengan_Sidang_DPK.docx";
            }
            else if (KeputusanSidang == "14")
            {
                strTemplate = filepath + "Files/Template/Template_Hukuman_Dengan_Sidang_DPK.docx";
            }
            else if(KeputusanSidang == "15")
            {
                strTemplate = filepath + "Files/Template/TEMPLATE PTDH TINDAK PIDANA.docx";
            }
            
            string OutputPath = filepath + "Files/Result/SK/";
            string GetDateTime = DateTime.Now.ToString("ddMMyyyy_hhmmss");
            string strFileNameDoc = "SK a.n "+ NAMA_LENGKAP + " NIP " + NIP + "_" + GetDateTime + ".docx";
            string strFileNamePDF = "SK a.n " + NAMA_LENGKAP + " NIP " + NIP + "_" + GetDateTime + ".pdf";
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
                a.NIP = NIP;

                string s_KODE_SATKER = a.Kode_Satker1.Substring(0, 2);
                //s_KODE_RAHASIA = lq.KODE_RAHASIA.ToString();
                string s_Ditjen = "";
                switch (s_KODE_SATKER)
                {
                    case "11":
                    case "04":
                    case "05":
                        s_Ditjen = "Direktur Jenderal Pendidikan Islam Kementerian Agama Jakarta";
                        break;
                    case "15":
                    case "06":
                        s_Ditjen = "Direktur Jenderal Bimbingan Masyarakat Kristen Kementerian Agama Jakarta";
                        break;
                    case "14":
                        s_Ditjen = "Direktur Jenderal Bimbingan Masyarakat Katolik Kementerian Agama Jakarta";
                        break;
                    case "12":
                    case "16":
                    case "07":
                        s_Ditjen = "Direktur Jenderal Bimbingan Masyarakat Hindu Kementerian Agama Jakarta";
                        break;
                    case "08":
                        s_Ditjen = "Direktur Jenderal Bimbingan Masyarakat Buddha Kementerian Agama Jakarta";
                        break;
                }

                Document doc = new Document();
                doc.LoadFromFile(strTemplate);
                //get strings to replace
                Dictionary<string, string> dictReplace = GetReplaceDictionary(a);
                //Replace text
                foreach (KeyValuePair<string, string> kvp in dictReplace)
                {
                    doc.Replace(kvp.Key, kvp.Value, true, true);
                }
                doc.SaveToFile(OutputPath + strFileNameDoc, FileFormat.Docx);
                doc.SaveToFile(OutputPath + strFileNamePDF, FileFormat.PDF);
                clsDocument b = new clsDocument();
                b.DocPDF = strFileNamePDF;
                b.DocWord = strFileNameDoc;
                b.Msg = "Success";
                System.Diagnostics.Process.Start(OutputPath + strFileNameDoc);
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
                string path = Server.MapPath("/Files/Result/SK/") + fileName;

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
            int Bulan = DateTime.Now.Month;
            int Tahun = DateTime.Now.Year;
            string strBulan = string.Format(string.Format("{0:00}", Bulan));
            string strTahun = Tahun.ToString();

            replaceDict.Add("*DasarBukti*", a.DasarBukti ?? "");
            replaceDict.Add("*Nama_Lengkap*", a.FullName ?? "");
            replaceDict.Add("*NIP*", a.NIP ?? "");
            replaceDict.Add("*Alasan*", a.DasarBukti ?? "");
            replaceDict.Add("*Pelanggaran*", a.PelanggaranDisiplin ?? "");
            string TTL = a.TEMPAT_LAHIR + ", " + a.TANGGAL_LAHIR;
            replaceDict.Add("*TTL*", TTL ?? "");
            replaceDict.Add("*Pasal_Pelanggaran*", a.PasalPelanggaran ?? "");
            replaceDict.Add("*hukuman*", a.hukuman ?? "");
            replaceDict.Add("*Keputusan_Sidang*", a.hukuman ?? "");
            replaceDict.Add("*PangkatGol*", a.GOL_RUANG ?? "");
            replaceDict.Add("*Jabatan*", a.LEVEL_JABATAN ?? "");
            replaceDict.Add("*UnitKerja*", a.UnitKerja ?? "");
            replaceDict.Add("*MENAG*", a.menag ?? "");
            replaceDict.Add("*Konseptor*", a.konseptor ?? "");
            replaceDict.Add("*Kasubag*", a.subkoor ?? "");
            replaceDict.Add("*Koordinator*", a.koor ?? "");
            replaceDict.Add("*Nama_Menteri*", a.menag ?? "");
            replaceDict.Add("*Mengingat*", a.Mengingat ?? "");
            replaceDict.Add("*Tembusan*", a.Tembusan ?? "");
            replaceDict.Add("*Jabatan_Konseptor*", a.Jabatan_Konseptor ?? "");
            replaceDict.Add("*Jabatan_Kasubag*", a.Jabatan_subkoor ?? "");
            replaceDict.Add("*Jabatan_Koordinator*", a.Jabatan_koor ?? "");

            replaceDict.Add("*TEMBUSAN_1*", a.TEMBUSAN1 ?? "");
            replaceDict.Add("*TASPEN*", a.TASPEN ?? "");
            replaceDict.Add("*KPPN*", a.KPPN ?? "");


            replaceDict.Add("*TAHUN*", strTahun ?? "");
            //DateTime TMTDate = DateTime.ParseExact(a.TMTDate, "dd-MM-yyyy", null); ;
            //a.TMTDate = TMTDate.ToString("d MMMM yyyy", new System.Globalization.CultureInfo("id-ID"));
            //replaceDict.Add("*TMT*", a.TMTDate);
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