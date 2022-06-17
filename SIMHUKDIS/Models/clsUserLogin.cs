using MySql.Data.MySqlClient;
using SIMHUKDIS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace simhukdis.Models
{
    public class clsUserLogin
    {
        [Required(ErrorMessage = "Please enter your User Name.")]
        [Display(Name = "UserID")]
        public string UserID { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter your Password.")]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "User Status")]
        public string UserStatus { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Display(Name = "NIP")]
        public string NIP { get; set; }
        [Display(Name = "Fullname")]
        public string FullName { get; set; }
        [Display(Name = "Status Admin")]
        public string StatusAdmin { get; set; }
        [Display(Name = "Last Login")]
        public string LastLogin { get; set; }
        [Display(Name = "Group ID")]
        public string GroupID { get; set; }
        [Display(Name = "Group Desc")]
        public string GroupDesc { get; set; }
        [Display(Name = "Last User")]
        public string LastUser { get; set; }
        [Display(Name = "Satuan Kerja")]
        public string Satker { get; set; }
        public string Satuan_Kerja { get; set; }
    }
    public class clsUserLoginDB
    {
        public IEnumerable<clsUserLogin> Users
        {
            get
            {
                Encryption encrypt = new Encryption();
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                List<clsUserLogin> Users = new List<clsUserLogin>();
                using (MySqlConnection con = new MySqlConnection(constr))
                {
                    string q = "sp_User_sel";
                    MySqlCommand cmd = new MySqlCommand(q, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    MySqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        clsUserLogin User = new clsUserLogin();
                        User.UserID = rd["UserID"].ToString();
                        User.UserName = rd["UserName"].ToString();
                        User.FullName = rd["FullName"].ToString();
                        User.Password = encrypt.Decrypt(rd["Password"].ToString(), rd["NIP"].ToString());
                        //User.Password = rd["Password"].ToString();
                        User.GroupID = rd["GroupID"].ToString();
                        User.GroupDesc = rd["GroupDesc"].ToString();
                        User.LastUser = rd["LastUser"].ToString();
                        User.NIP = rd["NIP"].ToString();
                        User.Satker = rd["Satker"].ToString();
                        User.Satuan_Kerja = rd["SATUAN_KERJA"].ToString();
                        if (rd["LastLogin"].ToString() == "")
                        {
                            User.LastLogin = "";
                        }
                        else
                        {
                            User.LastLogin = Convert.ToDateTime(rd["LastLogin"]).ToString("dd/MM/yyyy");
                        }
                        if (rd["StatusAdmin"].ToString() == "1")
                        {
                            User.StatusAdmin = "Yes";
                        }
                        else
                        {
                            User.StatusAdmin = "No";
                        }
                        Users.Add(User);
                    }
                    return Users;
                }
            }
        }
        public List<clsUserLogin> GetKonseptorList()
        {
            clsUserLoginDB db = new clsUserLoginDB();
            List<clsUserLogin> User = new List<clsUserLogin>();
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                MySqlCommand cmd = new MySqlCommand("sp_Konseptor_Sel", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                MySqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    clsUserLogin s = new clsUserLogin();
                    s.FullName = rd["Fullname"].ToString();
                    //s.GroupDesc = rd["GroupDesc"].ToString();
                    User.Add(s);
                }
                rd.Close();
            }
            return User;
        }
        public List<clsSatkerKonseptor> GetListSatker()
        {
            clsSatkerKonseptorDB db = new clsSatkerKonseptorDB();
            List<clsSatkerKonseptor> SK = new List<clsSatkerKonseptor>();
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                MySqlCommand cmd = new MySqlCommand("sp_Satker_Sel", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                MySqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    clsSatkerKonseptor s = new clsSatkerKonseptor();
                    s.SATUAN_KERJA = rd["SATUAN_KERJA"].ToString();
                    s.KODE_SATUAN_KERJA = rd["KODE_SATUAN_KERJA"].ToString();
                    SK.Add(s);
                }
                rd.Close();
            }
            return SK;
        }
        public List<clsUserLogin> GetListUserGroup()
        {
            clsUserLoginDB db = new clsUserLoginDB();
            List<clsUserLogin> User = new List<clsUserLogin>();
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                MySqlCommand cmd = new MySqlCommand("sp_UserGroup_Sel", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                MySqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    clsUserLogin s = new clsUserLogin();
                    s.GroupID = rd["GroupID"].ToString();
                    s.GroupDesc = rd["GroupDesc"].ToString();
                    User.Add(s);
                }
                rd.Close();
            }
            return User;
        }
        public bool GetDataExist(string NIP)
        {
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("sp_User_AlreadyExist", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("iNIP", NIP);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public bool GetAlreadyUse(string UserID)
        {
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("sp_User_AlreadyUse", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("iUserID", UserID);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public int CheckUserLogin(clsUserLogin User)
        {
            Encryption encrypt = new Encryption();
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string q = "select count(*) from tbl_user where NIP = @NIP and Password = @Password";
                MySqlCommand cmd = new MySqlCommand(q, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("NIP", User.NIP);
                cmd.Parameters.AddWithValue("Password", encrypt.Encrypt(User.Password, User.NIP));
                //cmd.Parameters.AddWithValue("Password", User.Password);
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public clsUserLogin GetData(string NIP)
        {
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string q = "sp_User_byuser_sel";
                MySqlCommand cmd = new MySqlCommand(q, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("iNIP", NIP);
                con.Open();
                clsUserLogin user = new clsUserLogin();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    return null;
                }
                else
                {
                    user.NIP = dt.Rows[0]["NIP"].ToString();
                    user.StatusAdmin = Convert.ToInt16(dt.Rows[0]["StatusAdmin"]).ToString();
                    user.FullName = dt.Rows[0]["FullName"].ToString();
                    user.Satker = dt.Rows[0]["Satker"].ToString();
                    user.GroupID = dt.Rows[0]["GroupID"].ToString();
                    user.UserID = dt.Rows[0]["UserID"].ToString();
                    return user;
                }
            }
        }
        public int Insert(clsUserLogin User)
        {
            try
            {
                int i = 0;
                Encryption encrypt = new Encryption();
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (MySqlConnection con = new MySqlConnection(constr))
                {
                    string MySql = "sp_User_Ins";
                    MySqlCommand cmd = new MySqlCommand(MySql, con);
                    cmd.CommandType = CommandType.StoredProcedure;                    
                    cmd.Parameters.AddWithValue("iUserName", User.UserName);
                    cmd.Parameters.AddWithValue("iFullname", User.FullName);
                    cmd.Parameters.AddWithValue("iPassword", encrypt.Encrypt(User.Password,User.NIP));
                    cmd.Parameters.AddWithValue("iStatusAdmin", User.StatusAdmin);
                    cmd.Parameters.AddWithValue("iGroupID", User.GroupID);
                    cmd.Parameters.AddWithValue("iLastUser", User.LastUser);
                    cmd.Parameters.AddWithValue("iNIP", User.NIP);
                    cmd.Parameters.AddWithValue("iSatker", User.Satker);
                    con.Open();
                    i = cmd.ExecuteNonQuery();
                }
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Edit(clsUserLogin User)
        {
            try
            {
                int i = 0;
                Encryption encrypt = new Encryption();
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (MySqlConnection con = new MySqlConnection(constr))
                {
                    string MySql = "sp_User_Upd";
                    MySqlCommand cmd = new MySqlCommand(MySql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("iUserID", User.UserID);
                    cmd.Parameters.AddWithValue("iUserName", User.UserName);
                    cmd.Parameters.AddWithValue("iFullname", User.FullName);
                    cmd.Parameters.AddWithValue("iPassword", encrypt.Encrypt(User.Password, User.NIP));
                    cmd.Parameters.AddWithValue("iStatusAdmin", User.StatusAdmin);
                    cmd.Parameters.AddWithValue("iGroupID", User.GroupID);
                    cmd.Parameters.AddWithValue("iLastUser", User.LastUser);
                    cmd.Parameters.AddWithValue("iNIP", User.NIP);
                    cmd.Parameters.AddWithValue("iSatker", User.Satker);
                    con.Open();
                    i = cmd.ExecuteNonQuery();
                }
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int ChangePassword(clsUserLogin User)
        {
            try
            {
                int i = 0;
                Encryption encrypt = new Encryption();
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (MySqlConnection con = new MySqlConnection(constr))
                {
                    string MySql = "sp_User_ChangePassword";
                    MySqlCommand cmd = new MySqlCommand(MySql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("iUserID", User.UserID);
                    cmd.Parameters.AddWithValue("iPassword", encrypt.Encrypt(User.Password, User.NIP));
                    cmd.Parameters.AddWithValue("iLastUser", User.LastUser);
                    cmd.Parameters.AddWithValue("iNIP", User.NIP);
                    con.Open();
                    i = cmd.ExecuteNonQuery();
                }
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int UpdateLastLogin(string NIP)
        {
            try
            {
                int i = 0;
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (MySqlConnection con = new MySqlConnection(constr))
                {
                    string MySql = "sp_UserLogin_Upd_LastLogin";
                    MySqlCommand cmd = new MySqlCommand(MySql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("iNIP", NIP);
                    con.Open();
                    i = cmd.ExecuteNonQuery();
                }
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Delete(string UserID, string NIP)
        {
            try
            {
                int i = 0;
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (MySqlConnection con = new MySqlConnection(constr))
                {
                    string MySql = "sp_User_Del";
                    MySqlCommand cmd = new MySqlCommand(MySql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("iUserID", UserID);
                    cmd.Parameters.AddWithValue("iNIP", NIP);
                    con.Open();

                    i = cmd.ExecuteNonQuery();
                }
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}