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
    public class clsView
    {
        [Key]
        public string ID { get; set; }
        public string NoSurat { get; set; }
        public string NIP { get; set; }
        public string Nama_Lengkap { get; set; }
        public string SatKer { get; set; }
        public string Konseptor { get; set; }
        public string TelaahNo { get; set; }
        public string FileTelaah { get; set; }
    }
    public class clsViewDB
    {
        public List<clsView> DataList()
        {
                string constr = ConfigurationManager.ConnectionStrings["dbHukdis"].ConnectionString;
                List<clsView> cm = new List<clsView>();
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string q = "SELECT * FROM DBO.VWTELAAH";
                    SqlCommand cmd = new SqlCommand(q, con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        clsView data = new clsView();
                        data.ID =rd["id"].ToString();
                        data.NIP =rd["NIP"].ToString();
                        data.NoSurat = rd["NOMOR_SURAT"].ToString();
                        data.Nama_Lengkap = rd["NAMA_LENGKAP"].ToString();
                        data.SatKer = rd["SATUAN_KERJA"].ToString();
                        data.Konseptor = rd["KONSEPTOR"].ToString();
                        data.TelaahNo = rd["TELAAHNO"].ToString();
                        data.FileTelaah = rd["FILETELAAH"].ToString();
                        cm.Add(data);
                    }
                    return cm;
                }
            }
        }
    }