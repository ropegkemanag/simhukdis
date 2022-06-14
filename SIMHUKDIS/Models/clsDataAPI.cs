using MySql.Data.MySqlClient;
using simhukdis.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace SIMHUKDIS.Models
{
    public class clsDataAPI
    {
        public List<Datum> ListAll()
        {
            List<Datum> DataAPI = new List<Datum>();
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                MySqlCommand cmd = new MySqlCommand("sp_Satker_Sel", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                MySqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    Datum s = new Datum();
                    s.KODE_SATUAN_KERJA = rd["KODE_SATUAN_KERJA"].ToString();
                    s.SATUAN_KERJA = rd["SATUAN_KERJA"].ToString();
                    DataAPI.Add(s);
                }
                rd.Close();
            }
            return DataAPI;
        }
        public int InsertUpdate(string KODE_SATUAN_KERJA, string SATUAN_KERJA)
        {
            try
            {
                int i = 0;
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (MySqlConnection con = new MySqlConnection(constr))
                {
                    string MySql = "sp_Satker_InsUpd";
                    MySqlCommand cmd = new MySqlCommand(MySql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("iKODE_SATUAN_KERJA", KODE_SATUAN_KERJA);
                    cmd.Parameters.AddWithValue("iSATUAN_KERJA", SATUAN_KERJA);
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