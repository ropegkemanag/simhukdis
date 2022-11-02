using SIMHUKDIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIMHUKDIS.Controllers.Admin
{
    [SessionExpire]
    public class MonitoringAdminController : Controller
    {
        
        // GET: MonitoringAdmin
        public ActionResult Index()
        {
            return View();
        }
    }
}