using Newtonsoft.Json;
using SIMHUKDIS.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SIMHUKDIS.Controllers
{
    public class HomeController : Controller
    {
        clsUserLoginDB UserLogin = new clsUserLoginDB();
        clsHomeDB x = new clsHomeDB();
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
                ViewBag.JumlahKasus = x.GetGrafik1(UserGroup, SatuanKerja, UserID);
                ViewBag.JumlahKasus2 = x.GetGrafik2(UserGroup, SatuanKerja, UserID);
                ViewBag.JumlahKasus3 = x.GetGrafik3(UserGroup, SatuanKerja, UserID);
                return View();
            }
            catch (Exception ex)
            {
                var Error_Message = "Error Catch ! (" + ex.Message + ")";
                return RedirectToAction("Error500", "Home", new { Error_Message });
            }            
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "Home");
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(clsUserLogin User)
        {
            string message = "";
            try
            {
                if (User.NIP == "" || User.NIP == null)
                {
                    message = "NIP is empty!";
                }
                else if (User.Password == "" || User.Password == null)
                {
                    message = "Password is empty!";
                }
                else
                {
                    if (UserLogin.CheckUserLogin(User) > 0)
                    {
                        message = "Success";
                        Session["LogUserID"] = User.NIP.ToString();
                        UserLogin.UpdateLastLogin(User.NIP);
                        clsUserLogin cUser = UserLogin.GetData(User.NIP.ToString());
                        Session["StatusAdmin"] = cUser.StatusAdmin.ToString();
                        Session["UserID"] = cUser.UserID.ToString();
                        Session["Fullname"] = cUser.FullName.ToString();
                        Session["Satker"] = cUser.Satker.ToString();
                        Session["UserGroup"] = cUser.GroupID.ToString();
                        ViewBag.Fullname = cUser.FullName.ToString();
                    }
                    else
                    {
                        message = "NIP or Password is Incorrect!";
                    }
                }

                //if (message != "Success")
                //{
                //    ViewBag.Message = message;
                //    return View(User);
                //}
                //else
                //{
                //    return RedirectToAction("Index", "Home");
                //}
            }
            catch (Exception ex)
            {
                message = ex.Message.ToString().Trim();
                //return View(User);
            }

            ViewBag.Message = message;
            return Json(message, JsonRequestBehavior.AllowGet);

        }       
        [HttpGet]
        public ActionResult Error500(string Error_Message)
        {
            string userlogin = Session["Fullname"].ToString();
            string SatuanKerja = Session["Satker"].ToString();
            string StatusAdmin = Session["StatusAdmin"].ToString();
            string UserGroup = Session["UserGroup"].ToString();
            ViewBag.StatusAdmin = StatusAdmin;
            ViewBag.UserID = userlogin;
            ViewBag.SatuanKerja = SatuanKerja;
            ViewBag.UserGroup = UserGroup;
            ViewBag.Error_Message = Error_Message;
            return View();
        }
        [HttpPost]
        public ContentResult AjaxMethod(string country)
        {
            StringBuilder sb = new StringBuilder();
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("sp_Grafik_Dashboard", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                sb.Append("[");
                while (rd.Read())
                {
                    sb.Append("{");
                    System.Threading.Thread.Sleep(50);
                    string color = String.Format("#{0:X6}", new Random().Next(0x1000000));
                    sb.Append(string.Format("text :'{0}', value:{1}, color: '{2}'", rd["JML"].ToString(), rd["STATUS"].ToString(), color));
                    sb.Append("},");
                    //clsHome s = new clsHome();
                    //s.Nilai = rd["JML"].ToString();
                    //s.Keterangan = rd["STATUS"].ToString();
                    ////s.GroupDesc = rd["GroupDesc"].ToString();
                    //s.Add(s);
                }
                sb = sb.Remove(sb.Length - 1, 1);
                sb.Append("]");
                rd.Close();
            }

            return Content(sb.ToString());
        }
       public JsonResult Dashboardcount()
        {
            try
            {
                string Satker = Session["Satker"].ToString();
                string GroupUser = Session["UserGroup"].ToString();
                
                

                string[] Dashboardcount = new string[2];
                clsHome x = new clsHome();
                clsHomeDB y = new clsHomeDB();
                x = y.GetNilaiGrafik(Satker, GroupUser);
                Dashboardcount[0] = x.BELUM_PROSES.ToString();
                Dashboardcount[1] = x.SUDAH_PROSES.ToString();
                return Json(new { Dashboardcount }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                throw ex;
                //var Error_Message = "Error Catch ! (" + ex.Message + ")";
                //return RedirectToAction("Error500", "Home", new { Error_Message });
            }
        }
        public ContentResult ActualChart()
        {
            try
            {
                DataChart chartData = new DataChart();
                string Satker = Session["Satker"].ToString();
                string GroupUser = Session["UserGroup"].ToString();
                string pcolor = "";

                clsHomeDB db = new clsHomeDB();
                List<DashboardChart> data = db.Disp.ToList();

                string[] tmplabel = new string[10];
                decimal[] tmpVal = new decimal[10];
                string[] tmpColor = new string[10];

                int tmpCount = 0;
                int counter = 0;

                if (data.Count != 0)
                {
                    for (int x = 0; x < data.Count; x++)
                    {
                        tmplabel[counter] = data[x].LabelName;
                        tmpVal[counter] = Convert.ToDecimal(data[x].LabelValue.ToString());

                        if (Convert.ToDecimal(tmpVal[counter]) >= 5)
                        {
                            pcolor = "#ffffff";

                        }
                        else 
                        {
                            pcolor = "#ff8c00";
                        }

                        tmpColor[counter] = pcolor.Trim();

                        counter++;
                        tmpCount++;
                    }
                }
                string[] clabel = new string[tmpCount];
                string[] cColor = new string[tmpCount];
                if (tmpCount > 0)
                {
                    tmpCount = tmpCount - 1;
                    for (int i = 0; i <= tmpCount; i++)
                    {
                        clabel[i] = tmplabel[i];
                        cColor[i] = tmpColor[i];
                    }
                }

                chartData.labels = clabel;

                tmpCount++;
                decimal[] series1 = new decimal[tmpCount];
                if (tmpCount > 0)
                {
                    tmpCount--;
                    for (int i = 0; i <= tmpCount; i++)
                    {
                        series1[i] = tmpVal[i];
                    }
                }
                chartData.datasets = new List<DataSetChart>();
                List<DataSetChart> _dataSet = new List<DataSetChart>();

                _dataSet.Add(new DataSetChart()
                {
                    fill = false,
                    data = series1,
                    borderColor = cColor,
                    backgroundColor = cColor,
                    borderWidth = "1",
                });
                chartData.datasets = _dataSet;

                JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
                return Content(JsonConvert.SerializeObject(chartData, _jsonSetting), "application/json");
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
                //var Error_Message = "Error Catch ! (" + ex.Message + ")";
                //return RedirectToAction("Error500", "Home", new { Error_Message });
            }
        }
    }
}