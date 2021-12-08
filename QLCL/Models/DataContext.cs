using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using QLCL.Models;
using System.Data.SqlClient;
using QLCL.Models;

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
                string str = "select TenCongNhan, NamSinh, NuocVe, count(*) as SoTrieuChung " +
                  "from CONGNHAN cn join CN_TC cntc on cn.MaCongNhan = cntc.MaCongNhan " +
                  "group by TenCongNhan, NamSinh, NuocVe " +
                  "having count(*) >= @stc";
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
        public List<CongNhanModels> sqlListCNByTenDCL(string madiemCL)
        {
            List<CongNhanModels> list = new List<CongNhanModels>();
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = @"select * " +
                      "from CONGNHAN " +
                      "where MaDiemCL = @MaDiemCL";
                SqlCommand cmd = new SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("MaDiemCL", madiemCL);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new CongNhanModels
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

