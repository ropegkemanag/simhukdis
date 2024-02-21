using SIMHUKDIS.Models;
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

namespace SIMHUKDIS.Controllers.Konseptor
{
    public class UploadTelaahController : Controller
    {
        // GET: UploadTelaah
        public ActionResult Index()
        {
            return View();
        }
    }
}