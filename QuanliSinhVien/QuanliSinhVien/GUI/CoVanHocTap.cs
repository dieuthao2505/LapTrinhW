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
    public partial class CoVanHocTap : Form
    {
        public CoVanHocTap()
        {
            InitializeComponent();
        }

        private void txbMaCVHT_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ các điều khiển
            
            string maCVHT = txbMaCVHT.Text.Trim();
            string tenCVHT = txbTenCVHT.Text.Trim();
            DateTime ngaySinh = dtpkNgaySinh.Value;
            string gioiTinh = rdNam.Checked ? "Nam" : "Nữ";
            string maKhoa = cmbMaKhoa.SelectedItem?.ToString();
            string maLop = cmbMaLop.SelectedItem?.ToString();

            // Kiểm tra tính hợp lệ của dữ liệu
            if (!string.IsNullOrEmpty(maCVHT) && !string.IsNullOrEmpty(tenCVHT) &&
                !string.IsNullOrEmpty(maKhoa) && !string.IsNullOrEmpty(maLop))
            {
                // Câu lệnh SQL để thêm vào cơ sở dữ liệu
                string query = "INSERT INTO COVANHOCTAP (MaCVHT, TenCVHT, NgaySinh, GioiTinh, MaKhoa, MaLop) " +
                               "VALUES (@MaCVHT, @TenCVHT, @NgaySinh, @GioiTinh, @MaKhoa, @MaLop)";

                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@MaCVHT", SqlDbType.VarChar) { Value = maCVHT },
            new SqlParameter("@TenCVHT", SqlDbType.VarChar) { Value = tenCVHT },
            new SqlParameter("@NgaySinh", SqlDbType.Date) { Value = ngaySinh },
            new SqlParameter("@GioiTinh", SqlDbType.VarChar) { Value = gioiTinh },
            new SqlParameter("@MaKhoa", SqlDbType.VarChar) { Value = maKhoa },
            new SqlParameter("@MaLop", SqlDbType.VarChar) { Value = maLop }
                };

                try
                {
                    // Thực hiện thêm vào cơ sở dữ liệu
                    bool isSuccess = KetNoi.Instance.ExcuteNonQuery(query, parameters);
                    if (isSuccess)
                    {
                        MessageBox.Show("Thêm cố vấn học tập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadCoVanHocTapData(); 
                    }
                    else
                    {
                        MessageBox.Show("Không thể thêm cố vấn học tập!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void txbID_TextChanged(object sender, EventArgs e)
        {

        }
        private void LoadMaKhoa()
        {
            string query = "SELECT MAKHOA FROM KHOA"; 
            try
            {
                DataTable dataTable = KetNoi.Instance.ExcuteQuery(query);
                cmbMaKhoa.DataSource = dataTable;
                cmbMaKhoa.DisplayMember = "MAKHOA"; // Tên cột chứa mã khoa trong bảng KHOA
                cmbMaKhoa.ValueMember = "MaKhoa";
                cmbMaKhoa.SelectedIndex = -1; // Không chọn mục nào ban đầu
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi khi tải Mã Khoa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadMaLop()
        {
            string query = "SELECT MALOP FROM LOPHOC";
            try
            {
                DataTable dataTable = KetNoi.Instance.ExcuteQuery(query);
                cmbMaLop.DataSource = dataTable;
                cmbMaLop.DisplayMember = "MALOP"; // Tên cột chứa mã lớp trong bảng LOP
                cmbMaLop.ValueMember = "MALOP";
                cmbMaLop.SelectedIndex = -1; // Không chọn mục nào ban đầu
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi khi tải Mã Lớp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCoVanHocTapData()
        {
            string query = "SELECT * FROM COVANHOCTAP";

            try
            {
                DataTable dataTable = KetNoi.Instance.ExcuteQuery(query);

                // Thiết lập DataPropertyName cho các cột trong DataGridView
                dataGridViewCVHT.AutoGenerateColumns = false;
                dataGridViewCVHT.Columns.Clear(); // Xóa các cột hiện có (nếu có)

                // Thêm cột và thiết lập DataPropertyName tương ứng với tên cột trong bảng COVANHOCTAP
                dataGridViewCVHT.Columns.Add(new DataGridViewTextBoxColumn() { Name = "ID", HeaderText = "ID", DataPropertyName = "ID" });
                dataGridViewCVHT.Columns.Add(new DataGridViewTextBoxColumn() { Name = "MaCVHT", HeaderText = "Mã CVHT", DataPropertyName = "MACVHT" });
                dataGridViewCVHT.Columns.Add(new DataGridViewTextBoxColumn() { Name = "TenCVHT", HeaderText = "Tên CVHT", DataPropertyName = "TENCVHT" });
                dataGridViewCVHT.Columns.Add(new DataGridViewTextBoxColumn() { Name = "NgaySinh", HeaderText = "Ngày Sinh", DataPropertyName = "NGAYSINH" });
                dataGridViewCVHT.Columns.Add(new DataGridViewTextBoxColumn() { Name = "GioiTinh", HeaderText = "Giới Tính", DataPropertyName = "GIOITINH" });
                dataGridViewCVHT.Columns.Add(new DataGridViewTextBoxColumn() { Name = "MaKhoa", HeaderText = "Mã Khoa", DataPropertyName = "MAKHOA" });
                dataGridViewCVHT.Columns.Add(new DataGridViewTextBoxColumn() { Name = "MaLop", HeaderText = "Mã Lớp", DataPropertyName = "MALOP" });

                // Gán nguồn dữ liệu cho DataGridView
                dataGridViewCVHT.DataSource = dataTable;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu cố vấn học tập: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CoVanHocTap_Load(object sender, EventArgs e)
        {
            LoadCoVanHocTapData();
            LoadMaKhoa();
            LoadMaLop();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridViewCVHT.SelectedRows.Count > 0)
            {
                // Hỏi người dùng xác nhận việc xóa
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa dòng này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in dataGridViewCVHT.SelectedRows)
                    {
                        if (!row.IsNewRow) // Kiểm tra nếu không phải là hàng trống
                        {
                            try
                            {
                                int id = Convert.ToInt32(row.Cells["ID"].Value); // Lấy ID từ cột ID trong DataGridView

                                // Câu lệnh SQL DELETE
                                string query = "DELETE FROM COVANHOCTAP WHERE ID = @Id";
                                SqlParameter[] parameters = new SqlParameter[]
                                {
                            new SqlParameter("@Id", SqlDbType.Int) { Value = id }
                                };

                                bool isSuccess = KetNoi.Instance.ExcuteNonQuery(query, parameters);
                                if (isSuccess)
                                {
                                    MessageBox.Show("Xóa cố vấn học tập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    LoadCoVanHocTapData(); // Tải lại dữ liệu sau khi xóa
                                }
                                else
                                {
                                    MessageBox.Show("Không thể xóa cố vấn học tập!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            catch (SqlException ex)
                            {
                                MessageBox.Show("Lỗi khi xóa dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        


        private void cmbMaKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            if (dataGridViewCVHT.SelectedRows.Count > 0)
            {
                // Hỏi người dùng xác nhận việc sửa
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn sửa dòng này?", "Xác nhận sửa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    // Lấy thông tin từ các điều khiển
                    DataGridViewRow selectedRow = dataGridViewCVHT.SelectedRows[0]; // Lấy dòng đã chọn
                    int id = Convert.ToInt32(selectedRow.Cells["ID"].Value); // Lấy ID
                    string maCVHT = txbMaCVHT.Text.Trim();
                    string tenCVHT = txbTenCVHT.Text.Trim();
                    DateTime ngaySinh = dtpkNgaySinh.Value;
                    string gioiTinh = rdNam.Checked ? "Nam" : "Nữ";
                    string maKhoa = cmbMaKhoa.SelectedItem?.ToString();
                    string maLop = cmbMaLop.SelectedItem?.ToString();

                    // Kiểm tra tính hợp lệ của dữ liệu
                    if (!string.IsNullOrEmpty(maCVHT) && !string.IsNullOrEmpty(tenCVHT) &&
                        !string.IsNullOrEmpty(maKhoa) && !string.IsNullOrEmpty(maLop))
                    {
                        // Câu lệnh SQL để sửa trong cơ sở dữ liệu
                        string query = "UPDATE COVANHOCTAP SET MaCVHT = @MaCVHT, TenCVHT = @TenCVHT, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh, MaKhoa = @MaKhoa, MaLop = @MaLop " +
                                       "WHERE ID = @Id";

                        SqlParameter[] parameters = new SqlParameter[]
                        {
                            new SqlParameter("@Id", SqlDbType.Int) { Value = id },
                            new SqlParameter("@MaCVHT", SqlDbType.VarChar) { Value = maCVHT },
                            new SqlParameter("@TenCVHT", SqlDbType.VarChar) { Value = tenCVHT },
                            new SqlParameter("@NgaySinh", SqlDbType.Date) { Value = ngaySinh },
                            new SqlParameter("@GioiTinh", SqlDbType.VarChar) { Value = gioiTinh },
                            new SqlParameter("@MaKhoa", SqlDbType.VarChar) { Value = maKhoa },
                            new SqlParameter("@MaLop", SqlDbType.VarChar) { Value = maLop }
                        };

                        try
                        {
                            // Thực hiện sửa trong cơ sở dữ liệu
                            bool isSuccess = KetNoi.Instance.ExcuteNonQuery(query, parameters);
                            if (isSuccess)
                            {
                                MessageBox.Show("Sửa cố vấn học tập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadCoVanHocTapData(); // Tải lại dữ liệu sau khi sửa
                            }
                            else
                            {
                                MessageBox.Show("Không thể sửa cố vấn học tập!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dòng cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
