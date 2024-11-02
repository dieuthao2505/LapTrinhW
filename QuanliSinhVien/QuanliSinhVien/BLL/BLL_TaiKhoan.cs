using QuanliSinhVien.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanliSinhVien.BLL
{
    internal class BLL_TaiKhoan
    {
        private static BLL_TaiKhoan instance;
        private List<TaiKhoan> danhSachTaiKhoan; // Declare the list as a private field


        public static BLL_TaiKhoan Instance
        {

            get
            {
                if (instance == null)
                {
                    instance = new BLL_TaiKhoan();
                }
                return instance;
            }
        }

        private BLL_TaiKhoan() {
            danhSachTaiKhoan = new List<TaiKhoan>();
        }

        public DataTable DanhSach()
        {
            return DAL_TaiKhoan.Instance.DanhSach();
        }
  


        public bool Them(string ten, string matkhau, string loai)
        {
            matkhau = HeThong.Hash(matkhau);
            return DAL_TaiKhoan.Instance.Them(ten, matkhau, loai);

        }
        public bool Xoa(string tenDangNhap)
        {
            // Find the account by username
            TaiKhoan taiKhoan = danhSachTaiKhoan.FirstOrDefault(tk => tk.TenDangNhap == tenDangNhap);
            if (taiKhoan != null)
            {
                danhSachTaiKhoan.Remove(taiKhoan);
                return true;
            }
            return false;
        }
        public bool Sua(string tenDangNhap, string matKhau, string loaiTaiKhoan)
        {
            // Find the account by username
            TaiKhoan taiKhoan = danhSachTaiKhoan.FirstOrDefault(tk => tk.TenDangNhap == tenDangNhap);
            if (taiKhoan != null)
            {
                taiKhoan.MatKhau = matKhau;
                taiKhoan.LoaiTaiKhoan = loaiTaiKhoan;
                return true;
            }
            return false;
        }

    }
    public class TaiKhoan
    {
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string LoaiTaiKhoan { get; set; }
    }
}
