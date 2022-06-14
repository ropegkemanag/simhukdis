using simhukdis.Models;
using SIMHUKDIS.Models;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIMHUKDIS.Controllers
{
    [SessionExpire]
    public class DPKController : Controller
    {
        // GET: DPK
        clsDPKDB db = new clsDPKDB();
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
                ViewBag.StatusAdmin = StatusAdmin;
                ViewBag.UserID = userlogin;
                ViewBag.SatuanKerja = SatuanKerja;
                ViewBag.UserGroup = UserGroup;
                clsHukdisDB x = new clsHukdisDB();
                ViewBag.Hukdis = new SelectList(x.GetListHukdis(), "ID", "HukdisDesc");
                List<clsDPK> PD = db.ListAll().ToList();
                clsDataPegawaiDtl a = new clsDataPegawaiDtl();
                return View(PD);
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });
            }            
        }
        public ActionResult UbahData(string ID, string NIP, string KeputusanSidang, string Tanggal_Sidang, string Catatan_Sidang)
        {
            string strMsg = "";
            if (Session["Fullname"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            try
            {
                string UserLogin = Session["Fullname"].ToString();
                clsDPK PD = new clsDPK();
                PD.ID = ID;
                PD.NIP = NIP;
                PD.KeputusanSidang = KeputusanSidang;
                PD.Catatan_Sidang = Catatan_Sidang;
                PD.UserLogin = UserLogin;
                PD.Tanggal_Sidang_DPK = Tanggal_Sidang;
                PD.DasarBukti = "";
                PD.PelanggaranDisiplin = "";
                PD.PasalPelanggaran = "";
                
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
        public ActionResult SK(string ID, string NIP)
        {
            string strMsg = "";
            if (Session["Fullname"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            try
            {
                string UserLogin = Session["Fullname"].ToString();
                clsDPK PD = new clsDPK();
                PD.ID = ID;
                PD.NIP = NIP;
                PD.UserLogin = UserLogin;

                db.UpdateStatus1(PD);
                db.UpdateStatus2(PD);
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
        public ActionResult DownloadToExcel()
        {
            if (Session["Fullname"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            try
            {
                List<clsPraDPK> rpt = new List<clsPraDPK>();
                using (ExcelEngine excelEngine = new ExcelEngine())
                {
                    string sFilename = "PraDPKResult_" + String.Format("{0:yyyyMMdd}", DateTime.Now) + "_" + String.Format("{0:HHmm}", DateTime.Now);

                    //Initialize Application
                    IApplication application = excelEngine.Excel;

                    //Set the default application version as Excel 2016
                    application.DefaultVersion = ExcelVersion.Excel2016;

                    //Create a workbook with a worksheet
                    IWorkbook workbook = application.Workbooks.Create(1);

                    //Access first worksheet from the workbook instance
                    IWorksheet worksheet = workbook.Worksheets[0];
                    clsPraDPKDB db = new clsPraDPKDB();
                    //Export data to Excel
                    DataTable dataTable = db.GetDataTable();
                    worksheet.ImportDataTable(dataTable, true, 1, 1);
                    worksheet.UsedRange.AutofitColumns();

                    //Save the workbook to disk in xlsx format
                    workbook.SaveAs(sFilename, ExcelSaveType.SaveAsXLS, HttpContext.ApplicationInstance.Response, ExcelDownloadType.Open);
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