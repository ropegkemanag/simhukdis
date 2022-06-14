using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using simhukdis.Models;
using SIMHUKDIS.Models;
using Spire.Doc;
using Syncfusion.XlsIO;

namespace SIMHUKDIS.Controllers
{
    [SessionExpire]
    public class PraDPKController : Controller
    {
        // GET: PraDPK
        string strToken = "";
        string Username = "agus@kemenag.go.id";
        string Password = "12345678";
        string baseAddress = "https://api.kemenag.go.id/v1/";
        clsPraDPKDB db = new clsPraDPKDB();
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

                List<clsPraDPK> PD = db.ListAll().ToList();
                clsDataPegawaiDtl a = new clsDataPegawaiDtl();
                return View(PD);
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });
            }            
        }
        public ActionResult DownloadToExcel()
        {
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
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });
            }
        }
        public ActionResult Proses(string ID, string NIP, string Catatan, string Tanggal_Sidang)
        {
            string strMsg = "";
            try
            {
                string UserLogin = Session["Fullname"].ToString();
                clsPraDPK PD = new clsPraDPK();
                PD.ID = ID;
                PD.NIP = NIP;
                PD.Catatan = Catatan;
                PD.UserLogin = UserLogin;
                PD.Tanggal_Sidang = Tanggal_Sidang;
                if (db.GetDataExist(ID,NIP) == true)
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
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });
            }
        }
        public ActionResult SendToDPK(string ID, string NIP)
        {
            string strMsg = "";
            try
            {
                string UserLogin = Session["Fullname"].ToString();
                clsPraDPK PD = new clsPraDPK();
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
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });
            }
        }
    }
}