using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace SIMHUKDIS.Models
{
    public class clsDisposisi
    {
        [Key]
        public string ID { get; set; }
        [DisplayName("No")]
        public int No { get; set; }
        [DisplayName("No Surat")]
        public string NoSurat { get; set; }
        [DisplayName("Asal Surat")]
        public string AsalSurat { get; set; }
        [DisplayName("Tanggal Surat")]
        public string TanggalSurat { get; set; }
        [DisplayName("Perihal")]
        public string perihal { get; set; }
        [DisplayName("Satuan Kerja")]
        public string KODE_SATUAN_KERJA { get; set; }
        public string SATUAN_KERJA { get; set; }
        [DisplayName("Konseptor")]
        public string Konseptor { get; set; }
        public string UserID { get; set; }
        [DisplayName("Status")]
        public string StatusDisposisi { get; set; }
        [DisplayName("Catatan")]
        public string Catatan1 { get; set; }
        [DisplayName("Catatan")]
        public string Catatan2 { get; set; }
        [DisplayName("Catatan")]
        public string Catatan3 { get; set; }
        public string Dokumen_Yang_Akan_Dibuat { get; set; }

        [DisplayName("Disposisi By 1")]
        public string DisposisiBy1 { get; set; }
        [DisplayName("Disposisi By 2")]
        public string DisposisiBy2 { get; set; }
        [DisplayName("Disposisi By 3")]
        public string DisposisiBy3 { get; set; }

        [DisplayName("Disposisi Status 1")]
        public string DisposisiStatus1 { get; set; }
        [DisplayName("Disposisi Status 2")]
        public string DisposisiStatus2 { get; set; }
        [DisplayName("Disposisi Status 3")]
        public string DisposisiStatus3 { get; set; }

        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Disposisi Date 1")]
        public string DisposisiDate1 { get; set; }
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Disposisi Date 2")]
        public string DisposisiDate2 { get; set; }
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Disposisi Date 3")]
        public string DisposisiDate3 { get; set; }
        [DisplayName("Update User")]
        public string UpdateUser { get; set; }
        public string Kode_Unit_Kerja { get; set; }
        public string Unit_Kerja { get; set; }
        public string UsulStatus { get; set; }
        public string tipe { get; set; }
    }
    public class clsDisposisiDB
    {
        public List<clsDisposisi> ListAll()
        {
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            List<clsDisposisi> DP = new List<clsDisposisi>();
            using (SqlConnection con = new SqlConnection(constr))
            {
                Int32 pNom = 0;
                string q = "SIMHUKDIS.sp_Disposisi1_Sel";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    clsDisposisi data = new clsDisposisi();
                    pNom = pNom + 1;
                    data.No = pNom;
                    data.ID = rd["id"].ToString();
                    data.NoSurat = rd["nomor_surat"].ToString();
                    data.AsalSurat = rd["asal_surat"].ToString();
                    data.TanggalSurat = rd["tanggal_surat"].ToString();
                    //if (!Convert.IsDBNull(rd["tanggal_surat"]))
                    //{
                    //    data.TanggalSurat = Convert.ToDateTime(rd["tanggal_surat"].ToString());
                    //}
                    data.perihal = rd["perihal"].ToString();
                    data.Konseptor = rd["konseptor"].ToString();
                    data.UserID = rd["UserID"].ToString();                    
                    data.KODE_SATUAN_KERJA = rd["KODE_SATUAN_KERJA"].ToString();
                    data.SATUAN_KERJA = rd["SatuanKerja"].ToString();
                    data.Catatan1 = rd["Disposisi1_Notes"].ToString();
                    data.Catatan2 = rd["Disposisi2_Notes"].ToString();
                    data.Catatan3 = rd["Disposisi3_Notes"].ToString();
                    data.DisposisiStatus1 = rd["Disposisi1_Status"].ToString();
                    data.DisposisiStatus2 = rd["Disposisi2_Status"].ToString();
                    data.DisposisiStatus3 = rd["Disposisi3_Status"].ToString();
                    data.DisposisiBy1 = rd["Disposisi1_By"].ToString();
                    data.DisposisiBy2 = rd["Disposisi2_By"].ToString();
                    data.DisposisiBy3 = rd["Disposisi3_By"].ToString();
                    data.DisposisiDate1 = rd["Disposisi1_Date"].ToString();
                    data.DisposisiDate2 = rd["Disposisi2_Date"].ToString();
                    data.DisposisiDate3 = rd["Disposisi3_Date"].ToString();
                    data.Dokumen_Yang_Akan_Dibuat = rd["Dokumen_Yang_Akan_Dibuat"].ToString();
                    data.Kode_Unit_Kerja = rd["Kode_Unit_Kerja"].ToString();
                    data.Unit_Kerja = rd["Unit_Kerja"].ToString();
                    //if (!Convert.IsDBNull(rd["Disposisi1_Date"]))
                    //{
                    //    data.DisposisiDate1 = Convert.ToDateTime(rd["Disposisi1_Date"].ToString());
                    //}
                    //if (!Convert.IsDBNull(rd["Disposisi2_Date"]))
                    //{
                    //    data.DisposisiDate2 = Convert.ToDateTime(rd["Disposisi2_Date"].ToString());
                    //}
                    //if (!Convert.IsDBNull(rd["Disposisi3_Date"]))
                    //{
                    //    data.DisposisiDate3 = Convert.ToDateTime(rd["Disposisi3_Date"].ToString());
                    //}
                    DP.Add(data);
                }
                return DP;
            }
        }
        public List<clsDisposisi> DP
        {
            get
            {

                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                List<clsDisposisi> DP = new List<clsDisposisi>();
                using (SqlConnection con = new SqlConnection(constr))
                {
                    Int32 pNom = 0;
                    string q = "sp_Disposisi1_Sel";
                    SqlCommand cmd = new SqlCommand(q, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        clsDisposisi data = new clsDisposisi();
                        pNom = pNom + 1;
                        data.No = pNom;
                        data.ID = rd["id"].ToString();
                        data.NoSurat = rd["nomor_surat"].ToString();
                        data.AsalSurat = rd["asal_surat"].ToString();
                        data.TanggalSurat = rd["tanggal_surat"].ToString();
                        data.perihal = rd["perihal"].ToString();
                        data.KODE_SATUAN_KERJA = rd["KODE_SATUAN_KERJA"].ToString();
                        data.SATUAN_KERJA = rd["SatuanKerja"].ToString();
                        data.Kode_Unit_Kerja = rd["Kode_Unit_Kerja"].ToString();
                        data.Unit_Kerja = rd["Unit_Kerja"].ToString();
                        data.tipe = rd["TIPE"].ToString();
                        DP.Add(data);
                    }
                    return DP;
                }
            }
        }
        public List<clsDisposisi> GetKonseptorList()
        {
            clsDisposisiDB db = new clsDisposisiDB();
            List<clsDisposisi> User = new List<clsDisposisi>();
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("SIMHUKDIS.sp_Konseptor_Sel", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    clsDisposisi s = new clsDisposisi();
                    s.Konseptor = rd["UserName"].ToString();
                    //s.GroupDesc = rd["GroupDesc"].ToString();
                    User.Add(s);
                }
                rd.Close();
            }
            return User;
        }
        public int Edit(clsDisposisi disposisi)
        {
            try
            {
                int i = 0;
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string MySql = "sp_Disposisi1_Upd";
                    SqlCommand cmd = new SqlCommand(MySql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", disposisi.ID);
                    cmd.Parameters.AddWithValue("@Status", 2);
                    cmd.Parameters.AddWithValue("@Disposisi_By", disposisi.UpdateUser);
                    cmd.Parameters.AddWithValue("@Disposisi_Note", disposisi.Catatan1);
                    cmd.Parameters.AddWithValue("@Disposisi_Status", disposisi.DisposisiStatus1);
                    cmd.Parameters.AddWithValue("@tipe", disposisi.tipe);
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
    public class clsDisposisi2DB
    {
        public List<clsDisposisi> ListAll()
        {
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            List<clsDisposisi> DP = new List<clsDisposisi>();
            using (SqlConnection con = new SqlConnection(constr))
            {
                Int32 pNom = 0;
                string q = "SIMHUKDIS.sp_Disposisi2_Sel";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    clsDisposisi data = new clsDisposisi();
                    pNom = pNom + 1;
                    data.No = pNom;
                    data.ID = rd["id"].ToString();
                    data.NoSurat = rd["nomor_surat"].ToString();
                    data.AsalSurat = rd["asal_surat"].ToString();
                    data.TanggalSurat = rd["tanggal_surat"].ToString();
                    //if (!Convert.IsDBNull(rd["tanggal_surat"]))
                    //{
                    //    data.TanggalSurat = Convert.ToDateTime(rd["tanggal_surat"].ToString());
                    //}
                    data.perihal = rd["perihal"].ToString();
                    data.Konseptor = rd["konseptor"].ToString();
                    data.UserID = rd["UserID"].ToString();
                    data.KODE_SATUAN_KERJA = rd["KODE_SATUAN_KERJA"].ToString();
                    data.SATUAN_KERJA = rd["SatuanKerja"].ToString();
                    data.Catatan1 = rd["Disposisi1_Notes"].ToString();
                    data.Catatan2 = rd["Disposisi2_Notes"].ToString();
                    data.Catatan3 = rd["Disposisi3_Notes"].ToString();
                    data.DisposisiStatus1 = rd["Disposisi1_Status"].ToString();
                    data.DisposisiStatus2 = rd["Disposisi2_Status"].ToString();
                    data.DisposisiStatus3 = rd["Disposisi3_Status"].ToString();
                    data.DisposisiBy1 = rd["Disposisi1_By"].ToString();
                    data.DisposisiBy2 = rd["Disposisi2_By"].ToString();
                    data.DisposisiBy3 = rd["Disposisi3_By"].ToString();
                    data.DisposisiDate1 = rd["Disposisi1_Date"].ToString();
                    data.DisposisiDate2 = rd["Disposisi2_Date"].ToString();
                    data.DisposisiDate3 = rd["Disposisi3_Date"].ToString();
                    data.Dokumen_Yang_Akan_Dibuat = rd["Dokumen_Yang_Akan_Dibuat"].ToString();
                    data.Kode_Unit_Kerja = rd["Kode_Unit_Kerja"].ToString();
                    data.Unit_Kerja = rd["Unit_Kerja"].ToString();
                    DP.Add(data);
                }
                return DP;
            }
        }
        public List<clsDisposisi> DP
        {
            get
            {

                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                List<clsDisposisi> DP = new List<clsDisposisi>();
                using (SqlConnection con = new SqlConnection(constr))
                {
                    Int32 pNom = 0;
                    string q = "SIMHUKDIS.sp_Disposisi2_Sel";
                    SqlCommand cmd = new SqlCommand(q, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        clsDisposisi data = new clsDisposisi();
                        pNom = pNom + 1;
                        data.No = pNom;
                        data.ID = rd["id"].ToString();
                        data.NoSurat = rd["nomor_surat"].ToString();
                        data.AsalSurat = rd["asal_surat"].ToString();
                        data.TanggalSurat = rd["tanggal_surat"].ToString();
                        data.perihal = rd["perihal"].ToString();
                        data.KODE_SATUAN_KERJA = rd["KODE_SATUAN_KERJA"].ToString();
                        data.SATUAN_KERJA = rd["SatuanKerja"].ToString();
                        data.Kode_Unit_Kerja = rd["Kode_Unit_Kerja"].ToString();
                        data.Unit_Kerja = rd["Unit_Kerja"].ToString();
                        data.tipe = rd["TIPE"].ToString();
                        DP.Add(data);
                    }
                    return DP;
                }
            }
        }
        public List<clsDisposisi> GetKonseptorList()
        {
            clsDisposisi2DB db = new clsDisposisi2DB();
            List<clsDisposisi> User = new List<clsDisposisi>();
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("SIMHUKDIS.sp_Konseptor_Sel", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    clsDisposisi s = new clsDisposisi();
                    s.Konseptor = rd["UserName"].ToString();
                    //s.GroupDesc = rd["GroupDesc"].ToString();
                    User.Add(s);
                }
                rd.Close();
            }
            return User;
        }
        public int Edit(clsDisposisi disposisi)
        {
            try
            {
                int i = 0;
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string MySql = "sp_Disposisi2_Upd";
                    SqlCommand cmd = new SqlCommand(MySql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("i_id", disposisi.ID);
                    cmd.Parameters.AddWithValue("iStatus", 3);
                    cmd.Parameters.AddWithValue("iDisposisi_By", disposisi.UpdateUser);
                    cmd.Parameters.AddWithValue("iDisposisi_Note", disposisi.Catatan2);
                    cmd.Parameters.AddWithValue("iDisposisi_Status", disposisi.DisposisiStatus2);
                    cmd.Parameters.AddWithValue("@tipe", disposisi.tipe);
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
    public class clsDisposisi3DB
    {
        public string GetKonseptor(string Satker)
        {
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            string konseptor = "";
            List<clsDisposisi> DP = new List<clsDisposisi>();
            using (SqlConnection con = new SqlConnection(constr))
            {
                string q = "SIMHUKDIS.sp_SuratMasuk_GetKonseptor";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("iKODE_SATUAN_KERJA", Satker);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    konseptor = rd["konseptor "].ToString();
                }
                return konseptor;
            }
        }
        public List<clsDisposisi> ListAll()
        {
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            List<clsDisposisi> DP = new List<clsDisposisi>();
            using (SqlConnection con = new SqlConnection(constr))
            {
                Int32 pNom = 0;
                string q = "SIMHUKDIS.sp_Disposisi3_Sel";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    clsDisposisi data = new clsDisposisi();
                    pNom = pNom + 1;
                    data.No = pNom;
                    data.ID = rd["id"].ToString();
                    data.NoSurat = rd["nomor_surat"].ToString();
                    data.AsalSurat = rd["asal_surat"].ToString();
                    data.TanggalSurat = rd["tanggal_surat"].ToString();
                    //if (!Convert.IsDBNull(rd["tanggal_surat"]))
                    //{
                    //    data.TanggalSurat = Convert.ToDateTime(rd["tanggal_surat"].ToString());
                    //}
                    data.perihal = rd["perihal"].ToString();
                    data.Konseptor = rd["konseptor"].ToString();
                    data.UserID = rd["UserID"].ToString();
                    data.KODE_SATUAN_KERJA = rd["KODE_SATUAN_KERJA"].ToString();
                    data.SATUAN_KERJA = rd["SatuanKerja"].ToString();
                    data.Catatan1 = rd["Disposisi1_Notes"].ToString();
                    data.Catatan2 = rd["Disposisi2_Notes"].ToString();
                    data.Catatan3 = rd["Disposisi3_Notes"].ToString();
                    data.DisposisiStatus1 = rd["Disposisi1_Status"].ToString();
                    data.DisposisiStatus2 = rd["Disposisi2_Status"].ToString();
                    data.DisposisiStatus3 = rd["Disposisi3_Status"].ToString();
                    data.DisposisiBy1 = rd["Disposisi1_By"].ToString();
                    data.DisposisiBy2 = rd["Disposisi2_By"].ToString();
                    data.DisposisiBy3 = rd["Disposisi3_By"].ToString();
                    data.DisposisiDate1 = rd["Disposisi1_Date"].ToString();
                    data.DisposisiDate2 = rd["Disposisi2_Date"].ToString();
                    data.DisposisiDate3 = rd["Disposisi3_Date"].ToString();
                    data.Dokumen_Yang_Akan_Dibuat = rd["Dokumen_Yang_Akan_Dibuat"].ToString();
                    //if (!Convert.IsDBNull(rd["Disposisi1_Date"]))
                    //{
                    //    data.DisposisiDate1 = Convert.ToDateTime(rd["Disposisi1_Date"].ToString());
                    //}
                    //if (!Convert.IsDBNull(rd["Disposisi2_Date"]))
                    //{
                    //    data.DisposisiDate2 = Convert.ToDateTime(rd["Disposisi2_Date"].ToString());
                    //}
                    //if (!Convert.IsDBNull(rd["Disposisi3_Date"]))
                    //{
                    //    data.DisposisiDate3 = Convert.ToDateTime(rd["Disposisi3_Date"].ToString());
                    //}
                    data.Kode_Unit_Kerja = rd["Kode_Unit_Kerja"].ToString();
                    data.Unit_Kerja = rd["Unit_Kerja"].ToString();
                    DP.Add(data);
                }
                return DP;
            }
        }
        public List<clsDisposisi> DP
        {
            get
            {

                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                List<clsDisposisi> DP = new List<clsDisposisi>();
                using (SqlConnection con = new SqlConnection(constr))
                {
                    Int32 pNom = 0;
                    string q = "SIMHUKDIS.sp_Disposisi3_Sel";
                    SqlCommand cmd = new SqlCommand(q, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        clsDisposisi data = new clsDisposisi();
                        pNom = pNom + 1;
                        data.No = pNom;
                        data.ID = rd["id"].ToString();
                        data.NoSurat = rd["nomor_surat"].ToString();
                        data.AsalSurat = rd["asal_surat"].ToString();
                        data.TanggalSurat = rd["tanggal_surat"].ToString();
                        //if (!Convert.IsDBNull(rd["tanggal_surat"]))
                        //{
                        //    data.TanggalSurat = Convert.ToDateTime(rd["tanggal_surat"].ToString());
                        //}
                        data.perihal = rd["perihal"].ToString();
                        data.Konseptor = rd["konseptor"].ToString();
                        data.UserID = rd["UserID"].ToString();
                        data.KODE_SATUAN_KERJA = rd["KODE_SATUAN_KERJA"].ToString();
                        data.SATUAN_KERJA = rd["SatuanKerja"].ToString();
                        data.Catatan1 = rd["Disposisi1_Notes"].ToString();
                        data.Catatan2 = rd["Disposisi2_Notes"].ToString();
                        data.Catatan3 = rd["Disposisi3_Notes"].ToString();
                        data.DisposisiStatus1 = rd["Disposisi1_Status"].ToString();
                        data.DisposisiStatus2 = rd["Disposisi2_Status"].ToString();
                        data.DisposisiStatus3 = rd["Disposisi3_Status"].ToString();
                        data.DisposisiBy1 = rd["Disposisi1_By"].ToString();
                        data.DisposisiBy2 = rd["Disposisi2_By"].ToString();
                        data.DisposisiBy3 = rd["Disposisi3_By"].ToString();
                        data.DisposisiDate1 = rd["Disposisi1_Date"].ToString();
                        data.DisposisiDate2 = rd["Disposisi2_Date"].ToString();
                        data.DisposisiDate3 = rd["Disposisi3_Date"].ToString();
                        data.Dokumen_Yang_Akan_Dibuat = rd["Dokumen_Yang_Akan_Dibuat"].ToString();
                        data.Kode_Unit_Kerja = rd["Kode_Unit_Kerja"].ToString();
                        data.Unit_Kerja = rd["Unit_Kerja"].ToString();
                        data.tipe = rd["TIPE"].ToString();
                        DP.Add(data);
                    }
                    return DP;
                }
            }
        }
        public int Edit(clsDisposisi disposisi)
        {
            try
            {
                int iStatus;
                if (disposisi.Dokumen_Yang_Akan_Dibuat == "02")
                {
                    iStatus = 7;
                }
                else
                {
                    iStatus = 4;
                }
                int i = 0;
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string MySql = "sp_Disposisi3_Upd";
                    SqlCommand cmd = new SqlCommand(MySql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@i_id", disposisi.ID);
                    cmd.Parameters.AddWithValue("@iStatus", iStatus);
                    cmd.Parameters.AddWithValue("@iDisposisi_By", disposisi.UpdateUser);
                    cmd.Parameters.AddWithValue("@iDisposisi_Note", disposisi.Catatan3);
                    cmd.Parameters.AddWithValue("@iDisposisi_Status", disposisi.DisposisiStatus3);
                    cmd.Parameters.AddWithValue("@iKonseptor", disposisi.Konseptor);
                    cmd.Parameters.AddWithValue("@iDokumen_Yang_Akan_Dibuat", disposisi.Dokumen_Yang_Akan_Dibuat);
                    cmd.Parameters.AddWithValue("@tipe", disposisi.tipe);
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
        public List<clsDisposisi> GetListSatker()
        {
            clsDisposisi3DB db = new clsDisposisi3DB();
            List<clsDisposisi> SK = new List<clsDisposisi>();
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("SIMHUKDIS.sp_Satker_Sel", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    clsDisposisi s = new clsDisposisi();
                    s.SATUAN_KERJA = rd["SATUAN_KERJA"].ToString();
                    s.KODE_SATUAN_KERJA = rd["KODE_SATUAN_KERJA"].ToString();
                    SK.Add(s);
                }
                rd.Close();
            }
            return SK;
        }
        public List<clsDisposisi> GetKonseptorList()
        {
            clsDisposisi3DB db = new clsDisposisi3DB();
            List<clsDisposisi> SK = new List<clsDisposisi>();
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("SIMHUKDIS.sp_Konseptor_Sel", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    clsDisposisi s = new clsDisposisi();
                    s.Konseptor = rd["Fullname"].ToString();
                    s.UserID = rd["UserID"].ToString();
                    //s.GroupDesc = rd["GroupDesc"].ToString();
                    SK.Add(s);
                }
                rd.Close();
            }
            return SK;
        }
    }
}