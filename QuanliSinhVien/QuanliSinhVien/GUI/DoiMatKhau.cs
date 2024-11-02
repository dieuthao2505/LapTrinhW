using QuanliSinhVien.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanliSinhVien.GUI
{
    public partial class DoiMatKhau : Form
    {
        public DoiMatKhau()
        {
            InitializeComponent();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txbTenDangNhap.Text;
            string matKhauCu = txbMatKhauCu.Text;
            string matKhauMoi = txbMatKhauMoi.Text;
            string xacNhanMatKhauMoi = txbXacNhanMatKhauMoi.Text;

            // Kiểm tra mật khẩu mới và xác nhận mật khẩu mới
            if (matKhauMoi != xacNhanMatKhauMoi)
            {
                MessageBox.Show("Mật khẩu mới và xác nhận mật khẩu không khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra xem tên đăng nhập và mật khẩu cũ có hợp lệ không
            if (!KiemTraMatKhauCu(tenDangNhap, matKhauCu))
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu cũ không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Cập nhật mật khẩu mới trong cơ sở dữ liệu
            if (CapNhatMatKhau(tenDangNhap, matKhauMoi))
            {
                MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Đổi mật khẩu thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool KiemTraMatKhauCu(string tenDangNhap, string matKhauCu)
        {
            // Câu lệnh SQL để kiểm tra tên đăng nhập và mật khẩu cũ
            string query = "SELECT COUNT(*) FROM NguoiDung WHERE TenDangNhap = @TenDangNhap AND MatKhau = @MatKhauCu";

            // Khai báo tham số
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TenDangNhap", SqlDbType.VarChar) { Value = tenDangNhap },
                new SqlParameter("@MatKhauCu", SqlDbType.VarChar) { Value = matKhauCu }
            };

            try
            {
                // Sử dụng phương thức ExecuteScalar để kiểm tra
                object result = KetNoi.Instance.ExecuteScalar(query, parameters);
                return Convert.ToInt32(result) > 0; // Trả về true nếu có tài khoản tồn tại
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kiểm tra mật khẩu cũ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool CapNhatMatKhau(string tenDangNhap, string matKhauMoi)
        {
            // Câu lệnh SQL để cập nhật mật khẩu mới
            string query = "UPDATE NguoiDung SET MatKhau = @MatKhauMoi WHERE TenDangNhap = @TenDangNhap";

            // Khai báo tham số
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TenDangNhap", SqlDbType.VarChar) { Value = tenDangNhap },
                new SqlParameter("@MatKhauMoi", SqlDbType.VarChar) { Value = matKhauMoi }
            };

            try
            {
                // Sử dụng phương thức ExecuteNonQuery để cập nhật
                return KetNoi.Instance.ExcuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật mật khẩu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
