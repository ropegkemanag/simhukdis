using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SIMHUKDIS.Models
{
    public class clsPenyampaianSKBiroDB
    {
        public List<clsKumpulanSK> PS(string userid, string groupID, string SATKER)
        {
            string constr = ConfigurationManager.ConnectionStrings["dbHukdis"].ConnectionString;
            List<clsKumpulanSK> PemberhentianSementara = new List<clsKumpulanSK>();
            using (SqlConnection con = new SqlConnection(constr))
            {
                string q = "sp_PenyampaianSKBiro_Display";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", userid);
                cmd.Parameters.AddWithValue("@GroupID", groupID);
                cmd.Parameters.AddWithValue("@SATKER", SATKER);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    clsKumpulanSK data = new clsKumpulanSK();
                    data.ID = rd["id"].ToString();
                    data.NO_SK = rd["No_SK"].ToString();
                    data.TANGGAL_SK = rd["Tanggal_SK"].ToString();
                    data.NIP = rd["NIP"].ToString();
                    data.NAMA_LENGKAP = rd["Nama_Lengkap"].ToString();
                    data.JENIS_SK = rd["Jenis_SK"].ToString();
                    data.KODE_JENIS_SK = rd["Kode_Jenis_SK"].ToString();
                    data.TMT_SK = rd["TMT_SK"].ToString();
                    data.KODE_SATUAN_KERJA = rd["Kode_Satuan_Kerja"].ToString();
                    data.SATUAN_KERJA = rd["Satuan_Kerja"].ToString();
                    data.KODE_UNIT_KERJA = rd["Kode_Unit_Kerja"].ToString();
                    data.UNIT_KERJA = rd["Unit_Kerja"].ToString();
                    data.FILE_SK = rd["File_SK"].ToString();
                    data.CREATED_DATE = rd["Created_Date"].ToString();
                    data.CREATED_USER = rd["Created_User"].ToString();
                    data.UPDATE_DATE = rd["Update_Date"].ToString();
                    data.UPDATE_USER = rd["Update_User"].ToString();
                    data.KETERANGAN = rd["Keterangan"].ToString();
                    PemberhentianSementara.Add(data);
                }
                return PemberhentianSementara;
            }
        }
        public int Insert(clsKumpulanSK PS)
        {
            int i;
            string constr = ConfigurationManager.ConnectionStrings["dbHukdis"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                //DateTime TANGGAL_SK = DateTime.ParseExact(PS.TANGGAL_SK, "dd-MM-yyyy", null);

                SqlCommand cmd = new SqlCommand("SP_KUMPULANHUKDIS_INS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@No_SK", PS.NO_SK ?? "");
                cmd.Parameters.AddWithValue("@Tanggal_SK", PS.TANGGAL_SK);
                cmd.Parameters.AddWithValue("@NIP", PS.NIP ?? "");
                cmd.Parameters.AddWithValue("@Nama_Lengkap", PS.NAMA_LENGKAP);
                cmd.Parameters.AddWithValue("@Jenis_SK", PS.JENIS_SK);
                cmd.Parameters.AddWithValue("@TMT_SK", "1900-01-01");
                cmd.Parameters.AddWithValue("@Satuan_Kerja", PS.KODE_SATUAN_KERJA);
                cmd.Parameters.AddWithValue("@Unit_Kerja", PS.KODE_UNIT_KERJA ?? "0");
                cmd.Parameters.AddWithValue("@Keterangan", PS.KETERANGAN);
                cmd.Parameters.AddWithValue("@File_SK", PS.FILE_SK ?? "");
                cmd.Parameters.AddWithValue("@Created_User", PS.CREATED_USER);
                con.Open();
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }
        public int Update(clsKumpulanSK PS)
        {
            int i;
            string constr = ConfigurationManager.ConnectionStrings["dbHukdis"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                DateTime TANGGAL_SK = DateTime.ParseExact(PS.TANGGAL_SK, "dd-MM-yyyy", null);
                DateTime TMT_SK = DateTime.ParseExact(PS.TMT_SK, "dd MMMM yyyy", null);
                SqlCommand cmd = new SqlCommand("SP_KUMPULANHUKDIS_UPD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", PS.ID);
                cmd.Parameters.AddWithValue("@No_SK", PS.NO_SK ?? "");
                cmd.Parameters.AddWithValue("@Tanggal_SK", TANGGAL_SK);
                cmd.Parameters.AddWithValue("@NIP", PS.NIP ?? "");
                cmd.Parameters.AddWithValue("@Nama_Lengkap", PS.NAMA_LENGKAP);
                cmd.Parameters.AddWithValue("@Jenis_SK", PS.KODE_JENIS_SK);
                cmd.Parameters.AddWithValue("@TMT_SK", TMT_SK);
                cmd.Parameters.AddWithValue("@Satuan_Kerja", PS.KODE_SATUAN_KERJA);
                cmd.Parameters.AddWithValue("@Unit_Kerja", PS.KODE_UNIT_KERJA);
                cmd.Parameters.AddWithValue("@Keterangan", PS.KETERANGAN);
                cmd.Parameters.AddWithValue("@File_SK", PS.FILE_SK ?? "");
                cmd.Parameters.AddWithValue("@Update_User", PS.CREATED_USER);
                con.Open();
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }
        public int Delete(int ID)
        {
            int i;
            string constr = ConfigurationManager.ConnectionStrings["dbHukdis"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("SP_KUMPULANSK_DEL", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);
                con.Open();
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }
        public clsKumpulanSK GetList(string ID)
        {
            string constr = ConfigurationManager.ConnectionStrings["dbHukdis"].ConnectionString;
            clsKumpulanSK data = new clsKumpulanSK();
            using (SqlConnection con = new SqlConnection(constr))
            {
                string q = "sp_KumpulanSK_SEL";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    data.ID = rd["id"].ToString();
                    data.NO_SK = rd["No_SK"].ToString();
                    data.TANGGAL_SK = rd["Tanggal_SK"].ToString();
                    data.NIP = rd["NIP"].ToString();
                    data.NAMA_LENGKAP = rd["Nama_Lengkap"].ToString();
                    data.KODE_JENIS_SK = rd["Kode_Jenis_SK"].ToString();
                    data.JENIS_SK = rd["Jenis_SK"].ToString();
                    data.TMT_SK = rd["TMT_SK"].ToString();
                    data.KODE_SATUAN_KERJA = rd["Kode_Satuan_Kerja"].ToString();
                    data.SATUAN_KERJA = rd["Satuan_Kerja"].ToString();
                    data.KODE_UNIT_KERJA = rd["Kode_Unit_Kerja"].ToString();
                    data.UNIT_KERJA = rd["Unit_Kerja"].ToString();
                    data.FILE_SK = rd["File_SK"].ToString();
                    data.CREATED_DATE = rd["Created_Date"].ToString();
                    data.CREATED_USER = rd["Created_User"].ToString();
                    data.UPDATE_DATE = rd["Update_Date"].ToString();
                    data.UPDATE_USER = rd["Update_User"].ToString();
                    data.KETERANGAN = rd["Keterangan"].ToString();
                }
                return data;
            }
        }
        public clsSuratMasukMsgInfo GetMsgInfo(string ID, string NIP,int status)
        {
            string constr = ConfigurationManager.ConnectionStrings["dbHukdis"].ConnectionString;
            clsSuratMasukMsgInfo data = new clsSuratMasukMsgInfo();
            using (SqlConnection con = new SqlConnection(constr))
            {
                string q = "SP_MESSAGEINFO_PenyampaianSurat";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@STATUS", status);
                cmd.Parameters.AddWithValue("@NIP", NIP ?? "");
                cmd.Parameters.AddWithValue("@NO_SK", ID);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    data.PhoneNo = rd["PhoneNo"].ToString();
                    data.Pesan = rd["Pesan"].ToString();
                }
                return data;
            }
        }

    }
}