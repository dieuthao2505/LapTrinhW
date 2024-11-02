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
    public partial class DangNhap : Form
    {
        public DangNhap()
        {
            InitializeComponent();
        }

        private void txbMatKhau_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string enteredUsername = txbTenDangNhap.Text;
            string enteredPassword = txbMatKhau.Text;

            // Kiểm tra tính hợp lệ của tên đăng nhập và mật khẩu từ cơ sở dữ liệu
            if (KiemTraDangNhap(enteredUsername, enteredPassword))
            {
                // Nếu tên đăng nhập và mật khẩu đúng, chuyển sang form SinhVien
                SinhVien f = new SinhVien();
                this.Hide();
                f.ShowDialog();
                this.Close();
            }
            else
            {
                // Nếu tên đăng nhập hoặc mật khẩu sai, hiển thị thông báo
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool KiemTraDangNhap(string tenDangNhap, string matKhau)
        {
            // Câu lệnh SQL để kiểm tra sự tồn tại của tên đăng nhập và mật khẩu
            string query = "SELECT COUNT(*) FROM NGUOIDUNG WHERE TenDangNhap = @TenDangNhap AND MatKhau = @MatKhau";

            // Khai báo các tham số
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TenDangNhap", SqlDbType.VarChar) { Value = tenDangNhap },
                new SqlParameter("@MatKhau", SqlDbType.VarChar) { Value = matKhau }
            };

            try
            {
                // Sử dụng phương thức ExecuteScalar để lấy kết quả từ câu truy vấn
                object result = KetNoi.Instance.ExecuteScalar(query, parameters);

                // Kiểm tra xem kết quả có lớn hơn 0 không (tức là có tài khoản tồn tại)
                return Convert.ToInt32(result) > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kết nối tới cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        private void txbTenDangNhap_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
