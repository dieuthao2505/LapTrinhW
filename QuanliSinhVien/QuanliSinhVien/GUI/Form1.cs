using QuanliSinhVien.BLL;
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

namespace QuanliSinhVien

{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadTaiKhoanData();
            LoadLoaiTaiKhoanData();
        }

        private void btnTem_Click(object sender, EventArgs e)
        {
   
            string tenDangNhap = txbTenDangNhap.Text.Trim();
            string matKhau = txbMatKhau.Text.Trim();
            string loaiTaiKhoan = cmbLoaiTaiKhoan.SelectedItem?.ToString();

            // Kiểm tra tính hợp lệ của dữ liệu đầu vào
            if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau) || matKhau.Length < 3 || string.IsNullOrEmpty(loaiTaiKhoan))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin và đảm bảo mật khẩu không dưới 3 ký tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Câu lệnh SQL để thêm vào cơ sở dữ liệu
            string query = "INSERT INTO NGUOIDUNG (TENDANGNHAP, MATKHAU, LOAI) VALUES (@TENDANGNHAP, @MATKHAU, @LOAI)";

            SqlParameter[] parameters = new SqlParameter[]
            {
        
        new SqlParameter("@TenDangNhap", SqlDbType.VarChar) { Value = tenDangNhap },
        new SqlParameter("@MatKhau", SqlDbType.VarChar) { Value = matKhau },
        new SqlParameter("@Loai", SqlDbType.VarChar) { Value = loaiTaiKhoan }
            };

            try
            {
                // Thực hiện thêm vào cơ sở dữ liệu
                bool isSuccess = KetNoi.Instance.ExcuteNonQuery(query, parameters);
                if (isSuccess)
                {
                    MessageBox.Show("Thêm tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTaiKhoanData(); 
                    
                }
                else
                {
                    MessageBox.Show("Không thể thêm tài khoản!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        private void txbID_TextChanged(object sender, EventArgs e)
        {

        }
        private void LoadTaiKhoanData()
        {
            string query = "SELECT * FROM NGUOIDUNG";

            try
            {
                // Thực hiện truy vấn và lấy dữ liệu vào DataTable
                DataTable dataTable = KetNoi.Instance.ExcuteQuery(query);

                // Thiết lập DataPropertyName cho các cột trong DataGridView
                dgvTaiKhoan.AutoGenerateColumns = false;
                dgvTaiKhoan.Columns.Clear();

                // Thêm các cột vào DataGridView với tên và DataPropertyName tương ứng
                dgvTaiKhoan.Columns.Add(new DataGridViewTextBoxColumn() { Name = "TenDangNhap", HeaderText = "Tên Đăng Nhập", DataPropertyName = "TENDANGNHAP" });
                dgvTaiKhoan.Columns.Add(new DataGridViewTextBoxColumn() { Name = "MatKhau", HeaderText = "Mật Khẩu", DataPropertyName = "MATKHAU" });
                dgvTaiKhoan.Columns.Add(new DataGridViewTextBoxColumn() { Name = "LoaiTaiKhoan", HeaderText = "Loại Tài Khoản", DataPropertyName = "LOAITAIKHOAN" });

                // Gán nguồn dữ liệu cho DataGridView
                dgvTaiKhoan.DataSource = dataTable;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadLoaiTaiKhoanData()
        {
            // Câu lệnh SQL để lấy các loại tài khoản
            string query = "SELECT DISTINCT LOAI FROM NGUOIDUNG"; // Giả sử `LOAITAIKHOAN` là cột chứa loại tài khoản

            try
            {
                DataTable dataTable = KetNoi.Instance.ExcuteQuery(query);

                // Gán dữ liệu cho ComboBox
                cmbLoaiTaiKhoan.DataSource = dataTable;
                cmbLoaiTaiKhoan.DisplayMember = "LOAI";
                cmbLoaiTaiKhoan.ValueMember = "LOAI";
                cmbLoaiTaiKhoan.SelectedIndex = -1; // Không chọn mục nào ban đầu
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu loại tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvTaiKhoan.SelectedRows.Count > 0)
            {
                // Lấy dòng được chọn
                DataGridViewRow row = dgvTaiKhoan.SelectedRows[0];
                string tenDangNhap = row.Cells["TenDangNhap"].Value.ToString();

                // Hỏi người dùng xác nhận việc xóa
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa tài khoản này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        // Thực hiện xóa tài khoản trong cơ sở dữ liệu
                        string query = "DELETE FROM NGUOIDUNG WHERE TENDANGNHAP = @TenDangNhap";
                        SqlParameter[] parameters = new SqlParameter[]
                        {
                    new SqlParameter("@TenDangNhap", SqlDbType.VarChar) { Value = tenDangNhap }
                        };

                        bool isSuccess = KetNoi.Instance.ExcuteNonQuery(query, parameters);

                        if (isSuccess)
                        {
                            MessageBox.Show("Xóa tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadTaiKhoanData(); // Tải lại dữ liệu sau khi xóa
                        }
                        else
                        {
                            MessageBox.Show("Không thể xóa tài khoản!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Lỗi khi kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một tài khoản để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }




        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvTaiKhoan.SelectedRows.Count > 0)
            {
                // Lấy dòng được chọn
                DataGridViewRow selectedRow = dgvTaiKhoan.SelectedRows[0];
                string tenDangNhap = selectedRow.Cells["TenDangNhap"].Value.ToString(); // Lấy tên đăng nhập

                // Lấy thông tin từ các điều khiển trên form
                string matKhau = txbMatKhau.Text.Trim();
                string loaiTaiKhoan = cmbLoaiTaiKhoan.SelectedItem?.ToString();

                // Kiểm tra điều kiện mật khẩu và loại tài khoản hợp lệ
                if (matKhau.Length >= 3 && !string.IsNullOrEmpty(loaiTaiKhoan))
                {
                    // Thực hiện cập nhật vào cơ sở dữ liệu qua lớp BLL
                    bool isSuccess = BLL_TaiKhoan.Instance.Sua(tenDangNhap, matKhau, loaiTaiKhoan);

                    if (isSuccess)
                    {
                        MessageBox.Show("Sửa tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadTaiKhoanData(); // Tải lại dữ liệu tài khoản vào DataGridView
                    }
                    else
                    {
                        MessageBox.Show("Không thể sửa tài khoản!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Mật khẩu phải có ít nhất 6 ký tự và loại tài khoản phải hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                // Xóa trắng các TextBox và đặt lại ComboBox
                txbMatKhau.Clear();
                cmbLoaiTaiKhoan.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một tài khoản để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}
