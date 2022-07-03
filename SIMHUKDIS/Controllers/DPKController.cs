using simhukdis.Models;
using SIMHUKDIS.Models;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System.IO;

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
        public ActionResult DownloadToExcel2()
        {
            try
            {
                string strMsg = "";
                using (ExcelPackage exl = new ExcelPackage())
                {
                    string sFilename = "DataSidangDPK_" + String.Format("{0:yyyyMMdd}", DateTime.Now) + "_" + String.Format("{0:HHmm}", DateTime.Now);
                    List<clsDPK> rpt = new List<clsDPK>();
                    rpt = db.ListAll().ToList();
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
                        ws.PrinterSettings.Orientation = eOrientation.Landscape;
                        ws.Cells["A1"].Value = "DATA SIDANG DEWAN PERTIMBANGAN KEPEGAWAIAN";
                        ws.Cells["A2"].Value = "BIRO KEPEGAWAIAN SETJEN KEMENTERIAN AGAMA";
                        ws.Cells["A3"].Value = "TANGGAL";
                        ws.Cells["A1:G1"].Merge = true;
                        ws.Cells["A2:G2"].Merge = true;
                        ws.Cells["A3:G3"].Merge = true;
                        ws.Cells["A1:F3"].Style.Font.Bold = true;
                        ws.Cells["A1:F3"].Style.Font.Size = 12;
                        ExcelRange rg1 = ws.Cells[1, 1, 3, 7];
                        rg1.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        rg1.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        rg1.Style.Font.Bold = true;

                        ws.Cells["A5"].Value = "NO";
                        ws.Cells["A5"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws.Cells["A5"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                        ws.Cells["B5"].Value = "IDENTITAS";
                        ws.Cells["B5"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws.Cells["B5"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                        ws.Cells["C5"].Value = "PASAL PELANGGARAN";
                        ws.Cells["C5"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws.Cells["C5"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                        ws.Cells["D5"].Value = "JENIS PELANGGARAN";
                        ws.Cells["D5"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws.Cells["D5"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                        ws.Cells["E5"].Value = "URAIAN PELANGGARAN";
                        ws.Cells["E5"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws.Cells["E5"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                        ws.Cells["F5"].Value = "REKOMENDASI IRJEN/PIMPINAN SATKER";
                        ws.Cells["F5"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws.Cells["F5"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                        ws.Cells["G5"].Value = "KEPUTUSAN SIDANG";
                        ws.Cells["G5"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws.Cells["G5"].Style.Fill.BackgroundColor.SetColor(colFromHex);

                        ExcelRange rg2 = ws.Cells[5, 1, 5, 7];
                        rg2.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        rg2.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        rg2.Style.Font.Bold = true;
                        rg2.Style.WrapText = true;
                        rg2.Style.Font.Size = 8;
                        int k = 1;
                        int i = 6;
                        foreach (clsDPK model in rpt)
                        {
                            string identitas = model.FullName + "\r" + model.NIP + "\n" + model.GOL_RUANG + "\n" + model.LEVEL_JABATAN + "\n" + model.DasarBukti;
                            ws.Cells[i, 1].Value = k;
                            ws.Cells[i, 2].Value = identitas;
                            ws.Cells[i, 3].Value = model.PasalPelanggaran;
                            ws.Cells[i, 4].Value = model.PelanggaranDisiplin;
                            ws.Cells[i, 5].Value = model.PelanggaranDisiplin;
                            ws.Cells[i, 6].Value = model.RekomendasiHukdis;
                            ws.Cells[i, 7].Value = "";
                            ws.Row(i).Height = 200;
                            i = i + 1;
                            k = k + 1;

                        }
                        ExcelRange rg = ws.Cells[5, 1, i - 1, 7];
                        rg.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        rg.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        rg.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        rg.Style.Border.Top.Style = ExcelBorderStyle.Thin;

                        ExcelRange rg3 = ws.Cells[5, 1, i - 1, 7];
                        rg3.Style.WrapText = true;
                        rg3.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                        rg3.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                        rg3.Style.Font.Size = 8;

                        ws.Row(5).Height = 30;
                        ws.Column(1).Width = 5;
                        ws.Column(2).Width = 20;
                        ws.Column(3).Width = 15;
                        ws.Column(4).Width = 15;
                        ws.Column(5).Width = 35;
                        ws.Column(6).Width = 20;
                        ws.Column(7).Width = 10;

                        string fileName = sFilename;
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment; filename=" + fileName + ".xlsx");
                        //Response.ContentType = "application/pdf";
                        //Response.AddHeader("content-disposition", "attachment; filename=" + fileName + ".pdf");
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
        //public ActionResult DownloadToExcel()
        //{
        //    if (Session["Fullname"] == null)
        //    {
        //        return RedirectToAction("Login", "Home");
        //    }
        //    try
        //    {
        //        List<clsDPK> rpt = new List<clsDPK>();
        //        using (ExcelEngine excelEngine = new ExcelEngine())
        //        {
        //            string sFilename = "PraDPKResult_" + String.Format("{0:yyyyMMdd}", DateTime.Now) + "_" + String.Format("{0:HHmm}", DateTime.Now);

        //            //Initialize Application
        //            IApplication application = excelEngine.Excel;

        //            //Set the default application version as Excel 2016
        //            application.DefaultVersion = ExcelVersion.Excel2016;

        //            //Create a workbook with a worksheet
        //            IWorkbook workbook = application.Workbooks.Create(1);

        //            //Access first worksheet from the workbook instance
        //            IWorksheet worksheet = workbook.Worksheets[0];
        //            clsDPKDB db = new clsDPKDB();
        //            //Export data to Excel
        //            DataTable dataTable = db.GetDataTable();
        //            worksheet.ImportDataTable(dataTable, true, 1, 1);
        //            worksheet.UsedRange.AutofitColumns();

        //            //Save the workbook to disk in xlsx format
        //            workbook.SaveAs(sFilename, ExcelSaveType.SaveAsXLS, HttpContext.ApplicationInstance.Response, ExcelDownloadType.Open);
        //        }
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception ex)
        //    {
        //        return View(ex.Message, JsonRequestBehavior.AllowGet);
        //    }
        //}
    }
}