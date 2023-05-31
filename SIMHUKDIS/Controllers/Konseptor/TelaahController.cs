using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SIMHUKDIS.Models;
using Spire.Doc;

namespace SIMHUKDIS.Controllers
{
    //[SessionExpire]
    public class TelaahController : Controller
    {
        // GET: Telaah
        clsToken db = new clsToken();
        string fname;
        string strToken = "";
        string Username = "agus@kemenag.go.id";
        string Password = "12345678";
        string baseAddress = "https://api.kemenag.go.id/v1/";
        string strTokenWA = "vhTYPWC5jyz72sK4VDcjR2re7xPNYnEa516ysMJlpUlKvMgTKNHvdSW9wUDlnTay";
        string baseAddressWA = "https://kudus.wablas.com/";
        public ActionResult Index()
        {
            if (Session["Fullname"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            try
            {
                //strToken = db.GetAccessToken(Username, Password);
                //Session["Token"] = strToken;
                string filepath = HttpContext.Request.PhysicalApplicationPath;
                string userlogin = Session["Fullname"].ToString();
                string UserID = Session["UserID"].ToString();
                string SatuanKerja = Session["Satker"].ToString();
                string StatusAdmin = Session["StatusAdmin"].ToString();
                string UserGroup = Session["UserGroup"].ToString();
                ViewBag.UserID = userlogin;
                ViewBag.SatuanKerja = SatuanKerja;
                ViewBag.UserGroup = UserGroup;
                ViewBag.UserID = userlogin;

                clsTelaahDB x = new clsTelaahDB();
                List<clsSuratMasuk> telaah = x.TelaahList(UserID).ToList();
                return View(telaah);
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });
            }            
        }
        [HttpGet]
        public ActionResult Create(int ID)
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

                clsTelaahDB db = new clsTelaahDB();
                clsSuratMasuk telaah = db.SuratMasuks.SingleOrDefault(sub => sub.ID == ID);
                ViewBag.strTelaahDate = DateTime.Now.ToString("dd-MM-yyyy");
          
                ViewBag.ID = ID;
                string statusAdmin = Session["StatusAdmin"] + "";
                //return View();
                return View(telaah);
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });
                //return RedirectToAction("Error500", "Home");
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
                string UserID = Session["UserID"].ToString();
                string SatuanKerja = Session["Satker"].ToString();
                string StatusAdmin = Session["StatusAdmin"].ToString();
                string UserGroup = Session["UserGroup"].ToString();
                ViewBag.UserID = userlogin;
                ViewBag.SatuanKerja = SatuanKerja;
                ViewBag.UserGroup = UserGroup;
                ViewBag.UserID = userlogin;

                clsTelaahDB db = new clsTelaahDB();
                clsSuratMasuk telaah = db.SuratMasuks.SingleOrDefault(sub => sub.ID == ID);
                return View(telaah);
            }
            catch(Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });
                //return RedirectToAction("Error500", "Home");
            }
        }
        [HttpGet]
        public ActionResult Details2(int ID)
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
                ViewBag.ID = ID;
                dynamic mymodel = new ExpandoObject();
                clsTelaahDB b = new clsTelaahDB();
                mymodel.clsSuratMasuk = b.GetSuratMasukByID(ID);
                mymodel.clsTelaah = b.ListAll(ID);
                return View(mymodel);
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });
                //return RedirectToAction("Error500", "Home");
            }
        }
        [HttpGet]
        public ActionResult DetailAtasan()
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
                clsTelaahDB x = new clsTelaahDB();
                List<clsTelaah> telaah = x.ListAtasan().ToList();
                return View(telaah);
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });
                //return RedirectToAction("Error500", "Home");
            }
        }
        [HttpGet]
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

                clsTelaahDB db = new clsTelaahDB();
                suratmasuk telaah = db.GetList(ID);

                ViewBag.ID = ID;
                ViewBag.NIP = NIP;
                string statusAdmin = Session["StatusAdmin"] + "";
                
                //return View();
                return View(telaah);
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });
                //return RedirectToAction("Error500", "Home");
            }
        }
        [HttpPost]
        public ActionResult Delete(int ID, string NIP)
        {
            string strMsg = "";
            //if (Session["Fullname"] == null)
            //{
            //    return RedirectToAction("Login", "Home");
            //}
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

                clsTelaahDB db = new clsTelaahDB();
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
        [HttpPost]
        public ActionResult Proses(int ID, string NIP, string FileTelaah)
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

                string FilePath = "";
                string FileExt = "";
                string GetDateTime = "";
                string Ext = "";
                string FileNameWithoutExtension = "";
                string a;
                GetDateTime = DateTime.Now.ToString("ddMMyyyy_hhmmss");

                clsTelaah telaah = new clsTelaah();
                string UserLogin = Session["Fullname"].ToString();

                telaah.CreatedUser = UserID;
                telaah.ID = ID;
                telaah.NIP = NIP;
                telaah.Proses = "1";
                telaah.FileTelaah = FileTelaah;
                if (FileTelaah == null)
                {
                    telaah.FileTelaah = "";
                }
                else
                {
                    telaah.FileTelaah = FileTelaah;
                }

                clsTelaahDB db = new clsTelaahDB();
                //db.Update(telaah);
                db.UpdateStatus(ID, NIP,UserID);
                db.UpdateTelaahStatus(telaah);
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
                    String FilePath = "";
                    String FileExt = "";
                    String GetDateTime = "";
                    String Ext = "";
                    String FileNameWithoutExtension = "";
                    string FileName = "";
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
                        fname = Path.Combine(Server.MapPath("/Files/Upload/Telaah/"), fname);
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
        public ActionResult ProsesKeKonseptor(int ID, string NIP, string FileTelaah)
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

                string FilePath = "";
                string FileExt = "";
                string GetDateTime = "";
                string Ext = "";
                string FileNameWithoutExtension = "";
                string a;
                GetDateTime = DateTime.Now.ToString("ddMMyyyy_hhmmss");

                clsTelaah telaah = new clsTelaah();
                string UserLogin = Session["Fullname"].ToString();
                telaah.CreatedUser = UserID;
                telaah.ID = ID;
                telaah.NIP = NIP;
                telaah.Proses = "0";
                if (FileTelaah == null)
                {
                    telaah.FileTelaah = "";
                }
                else
                {
                    telaah.FileTelaah = FileTelaah;
                }

                clsTelaahDB db = new clsTelaahDB();
                //db.Update(telaah);
                db.UpdateStatus(ID, NIP, UserLogin);
                db.UpdateTelaahStatus(telaah);
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
        public ActionResult ProsesKeAtasan(int ID, string NIP, string FileTelaah)
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

                string FilePath = "";
                string FileExt = "";
                string GetDateTime = "";
                string Ext = "";
                string FileNameWithoutExtension = "";
                string a;
                GetDateTime = DateTime.Now.ToString("ddMMyyyy_hhmmss");

                clsTelaah telaah = new clsTelaah();
                string UserLogin = Session["Fullname"].ToString();
                telaah.CreatedUser = UserID;
                telaah.ID = ID;
                telaah.NIP = NIP;
                telaah.Proses = "2";
                if (FileTelaah == null)
                {
                    telaah.FileTelaah = "";
                }
                else
                {
                    telaah.FileTelaah = FileTelaah;
                }

                clsTelaahDB db = new clsTelaahDB();
                //db.Update(telaah);
                db.UpdateStatus(ID, NIP, UserLogin);
                db.UpdateTelaahStatus(telaah);
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
        public ActionResult PraDPK(int ID, string NIP, string FileTelaah)
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

                clsTelaah telaah = new clsTelaah();
                string UserLogin = Session["Fullname"].ToString();
                telaah.CreatedUser = UserID;
                telaah.ID = ID;
                telaah.NIP = NIP;
                telaah.Proses = "3";
                if (FileTelaah == null)
                {
                    telaah.FileTelaah = "";
                }
                else
                {
                    telaah.FileTelaah = FileTelaah;
                }

                clsTelaahDB db = new clsTelaahDB();
                //db.Update(telaah);
                db.UpdateStatus(ID, NIP, UserLogin);
                db.UpdateTelaahStatus(telaah);
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

        public ActionResult UpdateData(int ID, string NIP,
        string NAMA_LENGKAP, string GOL_RUANG, string LEVEL_JABATAN,
        string SATUAN_KERJA, string TEMPAT_LAHIR, string TANGGAL_LAHIR,
        string MK_TAHUN, string TMT_Pensiun, string DasarBukti,
        string PelanggaranDisiplin, string PasalPelanggaran, string RekomendasiHukdis,
        string AnalisaPertimbangan, string KeputusanSidangDPK, string TelaahNo, string Tanggal_Telaah)
        {
            string strMsg = "";
            try
            {
                if (Session["Fullname"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                string UserLogin = Session["Fullname"].ToString();
                string userlogin = Session["Fullname"].ToString();
                string UserID = Session["UserID"].ToString();
                string SatuanKerja = Session["Satker"].ToString();
                string StatusAdmin = Session["StatusAdmin"].ToString();
                string UserGroup = Session["UserGroup"].ToString();
                ViewBag.UserID = userlogin;
                ViewBag.SatuanKerja = SatuanKerja;
                ViewBag.UserGroup = UserGroup;
                ViewBag.UserID = userlogin;

                clsTelaah telaah = new clsTelaah();
                telaah.ID = ID;
                telaah.NIP = NIP;
                telaah.NAMA_LENGKAP = NAMA_LENGKAP;
                telaah.GOL_RUANG = GOL_RUANG;
                telaah.LEVEL_JABATAN = LEVEL_JABATAN;
                telaah.SATUAN_KERJA = SATUAN_KERJA;
                telaah.TEMPAT_LAHIR = TEMPAT_LAHIR;
                telaah.TANGGAL_LAHIR = TANGGAL_LAHIR;
                telaah.MK_TAHUN = MK_TAHUN;
                telaah.TMT_Pensiun = TMT_Pensiun;
                telaah.DasarBukti = DasarBukti;
                telaah.PelanggaranDisiplin = PelanggaranDisiplin;
                telaah.PasalPelanggaran = PasalPelanggaran;
                telaah.RekomendasiHukdis = RekomendasiHukdis;
                telaah.AnalisaPertimbangan = AnalisaPertimbangan;
                telaah.KeputusanSidangDPK = KeputusanSidangDPK;
                telaah.CreatedUser = UserID;
                telaah.FileTelaah = "";
                telaah.TelaahNo = TelaahNo;
                telaah.Tanggal_Telaah = Tanggal_Telaah;
               
                clsTelaahDB a = new clsTelaahDB();
                a.Update(telaah);
                strMsg = "Success";
                return Json(strMsg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                strMsg = ex.Message.ToString();
                return Json(strMsg, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GenerateWord(int ID, string NIP,
        string NAMA_LENGKAP, string GOL_RUANG, string LEVEL_JABATAN,
        string SATUAN_KERJA, string TEMPAT_LAHIR, string TANGGAL_LAHIR,
        string MK_TAHUN, string TMT_Pensiun, string DasarBukti,
        string PelanggaranDisiplin, string PasalPelanggaran, string RekomendasiHukdis,
        string AnalisaPertimbangan, string KeputusanSidangDPK, string Tanggal_Telaah)
        {
            string filepath = HttpContext.Request.PhysicalApplicationPath;
            string strTemplate = filepath + "Files/Template/TEMPLATE TELAAH.docx";
            string OutputPath = filepath + "Files/Result/Telaah/";
            string GetDateTime = DateTime.Now.ToString("ddMMyyyy_hhmmss");
            string strFileNameDoc = "Telaah a.n "+ NAMA_LENGKAP + " NIP " + NIP + "_" + GetDateTime+ ".docx";
            string strFileNamePDF = "Telaah a.n " + NAMA_LENGKAP + " NIP " + NIP + "_" + GetDateTime + ".pdf";
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
                
                clsTelaah telaah = new clsTelaah();
                telaah.ID = ID;
                telaah.NIP = NIP;
                telaah.NAMA_LENGKAP = NAMA_LENGKAP;
                telaah.GOL_RUANG = GOL_RUANG;
                telaah.LEVEL_JABATAN= LEVEL_JABATAN;
                telaah.SATUAN_KERJA= SATUAN_KERJA;
                telaah.TEMPAT_LAHIR= TEMPAT_LAHIR;
                telaah.TANGGAL_LAHIR= TANGGAL_LAHIR;
                telaah.MK_TAHUN = MK_TAHUN;
                telaah.TMT_Pensiun= TMT_Pensiun;
                telaah.DasarBukti= DasarBukti;
                telaah.PelanggaranDisiplin= PelanggaranDisiplin;
                telaah.PasalPelanggaran= PasalPelanggaran;
                telaah.RekomendasiHukdis= RekomendasiHukdis;
                telaah.AnalisaPertimbangan= AnalisaPertimbangan;
                telaah.KeputusanSidangDPK = KeputusanSidangDPK;
                telaah.CreatedUser = UserID;
                telaah.Tanggal_Telaah = Tanggal_Telaah;
                clsTelaahDB i = new clsTelaahDB();
                int Bulan = DateTime.Now.Month;
                int Tahun = DateTime.Now.Year;
                int intTelaahNo = i.GetLastTelaahNo(Bulan,Tahun);
                string strTelaahNo = "";
                strTelaahNo = string.Format(string.Format("{0:000}", intTelaahNo));
                string strBulan = string.Format(string.Format("{0:00}", Bulan));
                strTelaahNo = "R-" + strTelaahNo + "/B.II/2-b/KP.04.1/" + strBulan + "/" + Tahun;

                clsPejabatMst pejabatMst = new clsPejabatMst();
                clsPejabatMstDB pdb = new clsPejabatMstDB();
                pejabatMst = pdb.GetList();

                string Karopeg = pejabatMst.Nama_Karopeg;
                string NIP_Karopeg = pejabatMst.NIP_Karopeg;
                telaah.Karopeg = Karopeg;
                telaah.NIP_Karopeg = NIP_Karopeg;

                telaah.TelaahNo = strTelaahNo;
                Document doc = new Document();
                doc.LoadFromFile(strTemplate);
                //get strings to replace
                Dictionary<string, string> dictReplace = GetReplaceDictionary(telaah);
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
                clsTelaahDB a = new clsTelaahDB();
                a.Insert(telaah);
                a.InsertTelaahNo(intTelaahNo,Bulan,Tahun);
                //ToViewFile(OutputPath+ strFileNameDoc);
                //strMsg = "Success";

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

        public ActionResult GenerateWord2(int ID, string NIP,
        string NAMA_LENGKAP, string GOL_RUANG, string LEVEL_JABATAN,
        string SATUAN_KERJA, string TEMPAT_LAHIR, string TANGGAL_LAHIR,
        string MK_TAHUN, string TMT_Pensiun, string DasarBukti,
        string PelanggaranDisiplin, string PasalPelanggaran, string RekomendasiHukdis,
        string AnalisaPertimbangan, string KeputusanSidangDPK, string Tanggal_Telaah, string Telaah_No)
        {
            string filepath = HttpContext.Request.PhysicalApplicationPath;
            string strTemplate = filepath + "Files/Template/TEMPLATE TELAAH.docx";
            string OutputPath1 = filepath + "Files/Result/Telaah/";
            string OutputPath2 = filepath + "Files/Upload/Telaah/";
            string GetDateTime = DateTime.Now.ToString("ddMMyyyy_hhmmss");
            string strFileNameDoc = "Telaah a.n " + NAMA_LENGKAP + " NIP " + NIP + "_" + GetDateTime + ".docx";
            string strFileNamePDF = "Telaah a.n " + NAMA_LENGKAP + " NIP " + NIP + "_" + GetDateTime + ".PDF";
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

                clsTelaah telaah = new clsTelaah();
                telaah.ID = ID;
                telaah.NIP = NIP;
                telaah.NAMA_LENGKAP = NAMA_LENGKAP;
                telaah.GOL_RUANG = GOL_RUANG;
                telaah.LEVEL_JABATAN = LEVEL_JABATAN;
                telaah.SATUAN_KERJA = SATUAN_KERJA;
                telaah.TEMPAT_LAHIR = TEMPAT_LAHIR;
                telaah.TANGGAL_LAHIR = TANGGAL_LAHIR;
                telaah.MK_TAHUN = MK_TAHUN;
                telaah.TMT_Pensiun = TMT_Pensiun;
                telaah.DasarBukti = DasarBukti;
                telaah.PelanggaranDisiplin = PelanggaranDisiplin;
                telaah.PasalPelanggaran = PasalPelanggaran;
                telaah.RekomendasiHukdis = RekomendasiHukdis;
                telaah.AnalisaPertimbangan = AnalisaPertimbangan;
                telaah.KeputusanSidangDPK = KeputusanSidangDPK;
                telaah.CreatedUser = UserID;
                telaah.Tanggal_Telaah = Tanggal_Telaah;

                clsPejabatMst pejabatMst = new clsPejabatMst();
                clsPejabatMstDB pdb = new clsPejabatMstDB();
                pejabatMst = pdb.GetList();

                string Karopeg = pejabatMst.Nama_Karopeg;
                string NIP_Karopeg = pejabatMst.NIP_Karopeg;
                telaah.Karopeg = Karopeg;
                telaah.NIP_Karopeg = NIP_Karopeg;

                telaah.TelaahNo = Telaah_No;
                Document doc = new Document();
                doc.LoadFromFile(strTemplate);
                //get strings to replace
                Dictionary<string, string> dictReplace = GetReplaceDictionary(telaah);
                //Replace text
                foreach (KeyValuePair<string, string> kvp in dictReplace)
                {
                    doc.Replace(kvp.Key, kvp.Value, true, true);
                }
                doc.SaveToFile(OutputPath1 + strFileNameDoc, FileFormat.Docx);
                doc.SaveToFile(OutputPath1 + strFileNamePDF, FileFormat.PDF);

                doc.SaveToFile(OutputPath2 + strFileNameDoc, FileFormat.Docx);
                doc.SaveToFile(OutputPath2 + strFileNamePDF, FileFormat.PDF);

                //ToViewFile(OutputPath + strFileNameDoc);
                //ToViewFile(OutputPath + strFileNamePDF);
                //ViewBag.strFileNameDoc = strFileNameDoc;
                //strMsg = strFileNamePDF;
                clsDocument a = new clsDocument();
                a.DocPDF = strFileNamePDF;
                a.DocWord = strFileNameDoc;
                a.Msg = "Success";

                ViewBag.NIP = NIP;
                ViewBag.ID = ID;
                ViewBag.DocPDF = strFileNamePDF;
                ViewBag.DocWord = strFileNameDoc;


                strMsg = "Success";
                return Json(a, JsonRequestBehavior.AllowGet);
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
                string path = Server.MapPath("/Files/Upload/Telaah/") + fileName;

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
        public FileResult DownloadResultFile(string fileName)
        {
            try
            {
                //Build the File Path.
                string path = Server.MapPath("/Files/Result/Telaah/") + fileName;
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
            catch(Exception ex)
            {
                throw (ex);
            }
        }
        Dictionary<string, string> GetReplaceDictionary(clsTelaah telaah)
        {
            Dictionary<string, string> replaceDict = new Dictionary<string, string>();
            replaceDict.Add("*NamaPegawai*", telaah.NAMA_LENGKAP);
            replaceDict.Add("*NIP*", telaah.NIP);
            replaceDict.Add("*TTL*", telaah.TEMPAT_LAHIR + " " + telaah.TANGGAL_LAHIR);
            replaceDict.Add("*PangkatGol*", telaah.GOL_RUANG);
            replaceDict.Add("*Jabatan*", telaah.LEVEL_JABATAN);
            replaceDict.Add("*UnitKerja*", telaah.SATUAN_KERJA);
            replaceDict.Add("*MasaKerja*", telaah.MK_TAHUN + ", " + telaah.TMT_Pensiun);
            replaceDict.Add("*DasarBukti*", telaah.DasarBukti);
            replaceDict.Add("*PelanggaranDisiplin*", telaah.PelanggaranDisiplin);
            replaceDict.Add("*PasalPelanggaran*", telaah.PasalPelanggaran);
            replaceDict.Add("*RekomendasiHukdis*", telaah.RekomendasiHukdis);
            replaceDict.Add("*AnalisaPertimbangan*", telaah.AnalisaPertimbangan);
            replaceDict.Add("*KeputusanSidangDPK*", telaah.KeputusanSidangDPK);
            replaceDict.Add("*TelaahNo*", telaah.TelaahNo);
            replaceDict.Add("*Karopeg*", telaah.Karopeg);
            replaceDict.Add("*NIP_Karopeg*", telaah.NIP_Karopeg);

            return replaceDict;
        }
        public JsonResult GetTelaah(int ID, string NIP)
        {
            try
            {
                //var Disposisi = db.ListAll().Find(x => x.ID.Equals(ID));
                //return Json(Disposisi, JsonRequestBehavior.AllowGet);
                clsTelaah dtl = new clsTelaah();
                List<clsTelaah> lists = new List<clsTelaah>();
                clsTelaahDB b = new clsTelaahDB();
                var m = b.ListByNIP(ID,NIP);
                return Json(m, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return new JsonResult
                {
                    Data = new { ErrorMessage = ex.Message, Success = false },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.DenyGet
                };
            }
        }
        public JsonResult GetPegawai(string NIP)
        {
            try
            {
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
                    dtl.AGAMA = clsDataPegawai.data.AGAMA;
                    dtl.TEMPAT_LAHIR = clsDataPegawai.data.TEMPAT_LAHIR;
                    dtl.TANGGAL_LAHIR = clsDataPegawai.data.TANGGAL_LAHIR.Substring(0,10);
                    dtl.TANGGAL_LAHIR = Convert.ToDateTime(dtl.TANGGAL_LAHIR).ToString("d MMMM yyyy", new System.Globalization.CultureInfo("id-ID"));
                    dtl.JENIS_KELAMIN = clsDataPegawai.data.JENIS_KELAMIN;
                    dtl.PENDIDIKAN = clsDataPegawai.data.PENDIDIKAN;
                    dtl.KODE_LEVEL_JABATAN = clsDataPegawai.data.KODE_LEVEL_JABATAN;
                    dtl.LEVEL_JABATAN = clsDataPegawai.data.LEVEL_JABATAN;

                    string Jabatan = clsDataPegawai.data.LEVEL_JABATAN;
                    string dtl_Satker = clsDataPegawai.data.KETERANGAN_SATUAN_KERJA;

                    string strPangkat = clsDataPegawai.data.PANGKAT;
                    string strGolRuang = clsDataPegawai.data.GOL_RUANG;
                    string strPangkatGolRuang = strPangkat + ", " + strGolRuang;


                    dtl.PANGKAT = strPangkatGolRuang;
                    dtl.GOL_RUANG = strPangkatGolRuang;

                    dtl.TMT_CPNS = clsDataPegawai.data.TMT_CPNS;
                    dtl.TMT_PANGKAT = clsDataPegawai.data.TMT_PANGKAT;

                    DateTime TMT_CPNS = DateTime.ParseExact(dtl.TMT_CPNS, "dd-MM-yyyy", null); //Convert.ToDateTime(Data.EstimateDate); 
                    string TahunCPNS = NIP.Substring(8, 4);
                    string BulanCPNS = NIP.Substring(12, 2);
                    int MK_Tahun_CPNS = Convert.ToInt32(TahunCPNS);
                    int MK_Bulan_CPNS = Convert.ToInt32(BulanCPNS);
                    int MK_YearNow = DateTime.Now.Year;
                    int MK_MonthNow = DateTime.Now.Month;
                    int a,b;
                    if(MK_MonthNow < MK_Bulan_CPNS)
                    {
                        a = MK_YearNow - MK_Tahun_CPNS - 1;
                        b = 12 - MK_Bulan_CPNS + MK_MonthNow;
                    }
                    else
                    {
                        a = MK_YearNow - MK_Tahun_CPNS;
                        b = MK_MonthNow - MK_Bulan_CPNS;
                    }


                    string strMasaKerjaThn = clsDataPegawai.data.MK_TAHUN.ToString();
                    string strMasaKerjaBln = clsDataPegawai.data.MK_BULAN.ToString();
                    string strMasaKerja = a + " Tahun " + b + " Bulan";
                    dtl.KETERANGAN_SATUAN_KERJA = clsDataPegawai.data.KETERANGAN_SATUAN_KERJA;
                    dtl.MK_TAHUN = strMasaKerja;
                    dtl.MK_BULAN = strMasaKerja;
                    dtl.TIPE_JABATAN = clsDataPegawai.data.TIPE_JABATAN;
                    dtl.KODE_JABATAN = clsDataPegawai.data.KODE_JABATAN;
                    dtl.TAMPIL_JABATAN = clsDataPegawai.data.TAMPIL_JABATAN + " " + clsDataPegawai.data.SATKER_1;
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
                    dtl.TMT_PENSIUN = clsDataPegawai.data.TMT_PENSIUN.Substring(0, 10);
                    dtl.TMT_PENSIUN = Convert.ToDateTime(dtl.TMT_PENSIUN).ToString("d MMMM yyyy", new System.Globalization.CultureInfo("id-ID"));


                    dtl.NO_HP = clsDataPegawai.data.NO_HP;
                    dtl.EMAIL = clsDataPegawai.data.EMAIL;
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
        public JsonResult Verifikasi(clsTelaahVerifikasi TV)
        {
            string strMsg = "";
            string UserLogin = Session["Fullname"].ToString();
            try
            {
                //clsTelaahVerifikasi TV = new clsTelaahVerifikasi();
                clsTelaahDB x = new clsTelaahDB();
                string UserID = Session["UserID"].ToString();
                //TV.ID = ID;
                //TV.Catatan = Catatan;
                //TV.JenisVerifikasi = JenisVerifikasi;
                TV.UpdateUser = UserID;

                x.Verifikasi(TV);
                if (TV.JenisVerifikasi=="1")
                {
                    SendWaBlas(Convert.ToInt16(TV.ID));
                }
                
                strMsg = "Success";
                return Json(strMsg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                strMsg = ex.Message.ToString();
                return Json(strMsg, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SendWaBlas(int ID)
        {
            try
            {
                clsSuratMasukMsgInfo Msg = new clsSuratMasukMsgInfo();
                clsSuratMasukDB r = new clsSuratMasukDB();
                int StatusSM = 5;

                Msg = r.GetMsgInfo(ID, StatusSM);

                if (Msg.PhoneNo != "" || Msg.PhoneNo != null)
                {
                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    
                    HttpResponseMessage resp = client.GetAsync(baseAddressWA + "api/send-message?source=postman&phone=" 
                        + WebUtility.UrlEncode(Msg.PhoneNo) + "&message=" + WebUtility.UrlEncode(Msg.Pesan)
                        + "&token=" + WebUtility.UrlEncode(strTokenWA)).GetAwaiter().GetResult();
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

        //public JsonResult GetPekerjaan(string NIP)
        //{
        //    try
        //    {
        //        var client = new HttpClient();
        //        clsDataPekerjaan dtl = new clsDataPekerjaan();
        //        strToken = db.GetAccessToken(Username, Password);
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strToken);
        //        HttpResponseMessage resp = client.GetAsync(baseAddress + "pegawai/pekerjaan/" + WebUtility.UrlEncode(NIP)).GetAwaiter().GetResult();
        //        if (resp.IsSuccessStatusCode)
        //        {
        //            clsPekerjaan clsPekerjaan = new clsPekerjaan();
        //            var JsonContent = resp.Content.ReadAsStringAsync().Result;
        //            clsPekerjaan = JsonConvert.DeserializeObject<clsPekerjaan>(JsonContent);
        //            int x = 0;
        //            dtl.jabatan = clsPekerjaan.data.jabatan[];
        //            dtl.pangkat = clsPekerjaan.data.pangkat;

        //            clsJabatan jab = new clsJabatan();
        //            jab.KETERANGAN = dtl.pangkat[x].KETERANGAN;
        //            return Json(dtl, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            return Json(null, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult
        //        {
        //            Data = new { ErrorMessage = ex.Message, Success = false },
        //            ContentEncoding = System.Text.Encoding.UTF8,
        //            JsonRequestBehavior = JsonRequestBehavior.DenyGet
        //        };
        //    }
        //}
    }
}