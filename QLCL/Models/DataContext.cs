using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using QLCL.Models;
using System.Data.SqlClient;
using QLCL.Models;
using System.Collections;

namespace QLCL.Models
{
    public class DataContext
    {
        public string ConnectionString { get; set; } // Biến thành viên

        public DataContext(string connectionstring)
        {
            this.ConnectionString = connectionstring;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
        public int sqlInsertDiemCachLy(DiemCachLyModels cachly)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "insert into diemcachly values(@madiemcl, @tendiemcl,@diachicl)";
                SqlCommand cmd = new SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("madiemcl", cachly.MaDiemCL);
                cmd.Parameters.AddWithValue("tendiemcl", cachly.TenDiemCL);
                cmd.Parameters.AddWithValue("diachicl", cachly.DiachiCL);
                return (cmd.ExecuteNonQuery());
            }
        }

        

        public List<object> Lietketrieuchung(int stc)
        {
            List<object> list = new List<object>();
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = " select TenCongNhan, NamSinh, NuocVe, count(TenTrieuChung) as SoTrieuChung "  +
                             "from CN_TC cn join CONGNHAN cntc on cn.MaCongNhan = cntc.MaCongNhan join TRIEUCHUNG tc on cn.MaTrieuChung = tc.MaTrieuChung " +
                             "group by TenCongNhan, NamSinh, NuocVe " +
                             "having count(TenTrieuChung) >=   @stc";
                SqlCommand cmd = new SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("stc", stc);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new
                        {
                            TenCongNhan = reader["TenCongNhan"].ToString(),
                            NamSinh = Convert.ToInt32(reader["NamSinh"]),
                            NuocVe = reader["NuocVe"].ToString(),
                            SoTrieuChung = Convert.ToInt32(reader["SoTrieuChung"]),
                        });
                    }
                    reader.Close();
                }
                return list;
            }

        }
        public List<object> slqSelectDiaDiem()
        {
            List<object> list = new List<object>();
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "select * " + "from DIEMCACHLY ";
                SqlCommand cmd = new SqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new 
                        {
                           MaDiemCL = reader["MaDiemCL"].ToString(),
                           TenDiemCL = reader["TenDiemCL"].ToString(),
                           DiachiCL = reader["DiaChiCL"].ToString(),
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }
        public List<object> ListCNByTenDCL(string MaCongNhan)
        {
            List<object> list = new List<object>();
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "select MaCongNhan, TenCongNhan " +
                  "from CONGNHAN cn join DIEMCACHLY dcl on cn.MaDiemCL = dcl.MaDiemCL" +
                  "where dcl.MaDiemCL = @MaDiemCL";
                 SqlCommand cmd = new SqlCommand(str, conn);
            cmd.Parameters.AddWithValue("MaCongNhan", MaCongNhan);
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new
                    {
                        MaCongNhan = reader["MaCongNhan"].ToString(),
                        TenCongNhan = reader["TenCongNhan"].ToString(),
                    });

                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }
    }
}

