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
    public partial class QuanLyMonHoc : Form
    {
        public QuanLyMonHoc()
        {
            InitializeComponent();
        }

        private void txbID_TextChanged(object sender, EventArgs e)
        {

        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ các điều khiển
            
            string maMon = txbMaMH.Text.Trim();
            string tenMon = txbTenMH.Text.Trim();
            int soTinChi;
            int tietThucHanh;
            int tietLyThuyet;

            // Kiểm tra tính hợp lệ của dữ liệu đầu vào
            if (
                !string.IsNullOrEmpty(maMon) &&
                !string.IsNullOrEmpty(tenMon) &&
                int.TryParse(numSoTC.Text, out soTinChi) &&
                int.TryParse(numTietTH.Text, out tietThucHanh) &&
                int.TryParse(numTietLT.Text, out tietLyThuyet))
            {
                // Câu lệnh SQL để thêm vào cơ sở dữ liệu
                string query = "INSERT INTO MONHOC ( MAMONHOC, TENMONHOC, SOTINCHI, TIETTHUCHANH, TIETLYTHUYET) " +
                               "VALUES ( @MaMon, @TenMon, @SoTinChi, @TietThucHanh, @TietLyThuyet)";

                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@MaMon", SqlDbType.VarChar) { Value = maMon },
            new SqlParameter("@TenMon", SqlDbType.VarChar) { Value = tenMon },
            new SqlParameter("@SoTinChi", SqlDbType.Int) { Value = soTinChi },
            new SqlParameter("@TietThucHanh", SqlDbType.Int) { Value = tietThucHanh },
            new SqlParameter("@TietLyThuyet", SqlDbType.Int) { Value = tietLyThuyet }
                };

                try
                {
                    // Thực hiện thêm vào cơ sở dữ liệu
                    bool isSuccess = KetNoi.Instance.ExcuteNonQuery(query, parameters);
                    if (isSuccess)
                    {
                        MessageBox.Show("Thêm môn học thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadMonHocData(); // Gọi phương thức để tải lại dữ liệu từ cơ sở dữ liệu vào DataGridView
                    }
                    else
                    {
                        MessageBox.Show("Không thể thêm môn học!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin và kiểm tra lại các giá trị hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void LoadMonHocData()
        {
            string query = "SELECT * FROM MONHOC"; 

            try
            {
                // Thực hiện truy vấn và lấy dữ liệu vào DataTable
                DataTable dataTable = KetNoi.Instance.ExcuteQuery(query);

                // Thiết lập DataPropertyName cho các cột trong DataGridView
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.Columns.Clear();

                // Thêm các cột vào DataGridView với tên và DataPropertyName tương ứng
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "ID", HeaderText = "ID", DataPropertyName = "ID" });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "MaMonHoc", HeaderText = "Mã Môn", DataPropertyName = "MAMONHOC" });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "TenMonHoc", HeaderText = "Tên Môn", DataPropertyName = "TENMONHOC" });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "TietThucHanh", HeaderText = "Tiết Thực Hành", DataPropertyName = "TIETTHUCHANH" });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "TietThucHanh", HeaderText = "Tiết Thực Hành", DataPropertyName = "TIETTHUCHANH" });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "TietLyThuyet", HeaderText = "Tiết Lý Thuyết", DataPropertyName = "TIETLYTHUYET" });

                // Gán nguồn dữ liệu cho DataGridView
                dataGridView1.DataSource = dataTable;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu môn học: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void QuanLyMonHoc_Load(object sender, EventArgs e)
        {
            LoadMonHocData();
        }
        
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Hỏi người dùng xác nhận việc xóa
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa dòng này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        if (!row.IsNewRow) // Kiểm tra nếu không phải là hàng trống
                        {
                            // Lấy ID từ cột ID trong DataGridView
                            string id = row.Cells["ID"].Value.ToString(); // Thay 'ID' bằng tên cột thực tế nếu khác

                            // Câu lệnh SQL DELETE
                            string query = "DELETE FROM MONHOC WHERE ID = @Id"; // Thay 'KHOA' bằng tên bảng đúng nếu khác
                            SqlParameter[] parameters = new SqlParameter[]
                            {
                        new SqlParameter("@Id", SqlDbType.VarChar) { Value = id }
                            };

                            try
                            {
                                bool isSuccess = KetNoi.Instance.ExcuteNonQuery(query, parameters);
                                if (isSuccess)
                                {
                                    MessageBox.Show("Xóa khoa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    dataGridView1.Rows.Remove(row); // Xóa hàng khỏi DataGridView
                                }
                                else
                                {
                                    MessageBox.Show("Không thể xóa mon học!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1) // Kiểm tra xem có đúng một dòng được chọn
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0]; // Lấy dòng được chọn

                // Lấy ID của môn học hiện tại
                string currentId = selectedRow.Cells["ID"].Value.ToString(); // Thay 'ID' bằng tên cột thực tế nếu khác

                // Lấy thông tin từ các TextBox
                string maMon = txbMaMH.Text.Trim();
                string tenMon = txbTenMH.Text.Trim();
                int soTinChi = (int)numSoTC.Value;
                int tietThucHanh = (int)numTietTH.Value;
                int tietLyThuyet = (int)numTietLT.Value;

                // Kiểm tra tính hợp lệ của dữ liệu đầu vào
                if (!string.IsNullOrEmpty(maMon) && !string.IsNullOrEmpty(tenMon))
                {
                    // Câu lệnh SQL để cập nhật vào cơ sở dữ liệu
                    string query = "UPDATE MONHOC SET MAMONHOC = @MaMon, TENMONHOC = @TenMon, SOTINCHI = @SoTinChi, TIETTHUCHANH = @TietThucHanh, TIETLYTHUYET = @TietLyThuyet WHERE ID = @Id";

                    SqlParameter[] parameters = new SqlParameter[]
                    {
                new SqlParameter("@Id", SqlDbType.VarChar) { Value = currentId }, // ID của môn học hiện tại
                new SqlParameter("@MaMon", SqlDbType.VarChar) { Value = maMon },
                new SqlParameter("@TenMon", SqlDbType.VarChar) { Value = tenMon },
                new SqlParameter("@SoTinChi", SqlDbType.Int) { Value = soTinChi },
                new SqlParameter("@TietThucHanh", SqlDbType.Int) { Value = tietThucHanh },
                new SqlParameter("@TietLyThuyet", SqlDbType.Int) { Value = tietLyThuyet }
                    };

                    try
                    {
                        bool isSuccess = KetNoi.Instance.ExcuteNonQuery(query, parameters);
                        if (isSuccess)
                        {
                            MessageBox.Show("Cập nhật môn học thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadMonHocData(); // Gọi phương thức để tải lại dữ liệu từ cơ sở dữ liệu vào DataGridView
                        }
                        else
                        {
                            MessageBox.Show("Không thể cập nhật môn học!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Lỗi khi cập nhật dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}