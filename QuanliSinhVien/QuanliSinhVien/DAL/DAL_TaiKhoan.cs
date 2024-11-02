using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanliSinhVien.DAL
{
    internal class DAL_TaiKhoan
    {
        private static DAL_TaiKhoan instance;

        public static DAL_TaiKhoan Instance
        {

            get { if (instance == null) instance = new DAL_TaiKhoan(); return instance; }
            private set => instance
            =
            value;
        }

        private DAL_TaiKhoan() { }
        public bool DangNhap(string tenDangNhap, string matKhau)
        {
            if (string.IsNullOrWhiteSpace(tenDangNhap) || string.IsNullOrWhiteSpace(matKhau))
            {
                Console.WriteLine("Tên đăng nhập hoặc mật khẩu không được để trống.");
                return false;
            }

            string query = "SELECT COUNT(*) FROM NGUOIDUNG WHERE TENDANGNHAP = @TenDangNhap AND MATKHAU = @MatKhau";
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@TenDangNhap", SqlDbType.VarChar) { Value = tenDangNhap },
        new SqlParameter("@MatKhau", SqlDbType.VarChar) { Value = matKhau }
            };

            try
            {
                object result = KetNoi.Instance.ExecuteScalar(query, parameters);
                int count = (result != null) ? Convert.ToInt32(result) : 0;
                return count > 0;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
                return false;
            }
        }

        public bool Them(string ten, string matkhau, string loai)
        {
            string sql = "INSERT INTO TaiKhoan (TENDANGNHAP, MATKHAU, LOAI) VALUES (@TenDangNhap, @MatKhau, @LoaiTaiKhoan)";
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@TenDangNhap", SqlDbType.VarChar) { Value = ten },
        new SqlParameter("@MatKhau", SqlDbType.VarChar) { Value = matkhau },
        new SqlParameter("@LoaiTaiKhoan", SqlDbType.VarChar) { Value = loai }
            };
            return KetNoi.Instance.ExcuteNonQuery(sql, parameters);
        }


        public bool Sua(string ten, string matkhau, string loai, int id)
        {
            string sql = "UPDATE Taikhoan SET TENDANGNHAP = @TenDangNhap, MATKHAU = @MatKhau, LOAI = @LoaiTaiKhoan WHERE Id = @id";
            return KetNoi.Instance.ExcuteNonQuery(sql, new SqlParameter[]
            {
        new SqlParameter("@TenDangNhap", ten),
        new SqlParameter("@MatKhau", matkhau),
        new SqlParameter("@LoaiTaiKhoan", loai),
        new SqlParameter("@id", id)
            });
        }

        public bool Xoa(int id)
        {
            string sql = "DELETE FROM Taikhoan WHERE Id = @id";
            return KetNoi.Instance.ExcuteNonQuery(sql, new SqlParameter[]
            {
        new SqlParameter("@id", id)
            });
        }
  

        public DataTable DanhSach()
        {
            return KetNoi.Instance.ExcuteQuery("SELECT * FROM NGUOIDUNG");
        }
    }

}
