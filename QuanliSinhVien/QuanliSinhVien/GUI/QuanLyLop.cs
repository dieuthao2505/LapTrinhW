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
    public partial class QuanLyLop : Form
    {
        public QuanLyLop()
        {
            InitializeComponent();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maLop = txbMaLop.Text.Trim();
            string tenLop = txbTenLop.Text.Trim();
            string maKhoa = cmbMaKhoa.Text.Trim();
            string soLuong= numSoLuong.Text.Trim();

            // Kiểm tra nếu thông tin chưa đầy đủ
            if (string.IsNullOrEmpty(maLop) || string.IsNullOrEmpty(tenLop) || string.IsNullOrEmpty(maKhoa))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Câu lệnh SQL để thêm vào cơ sở dữ liệu
            string query = "INSERT INTO LOPHOC (MALOP, TENLOP, MAKHOA, SOLUONG) VALUES (@MaLop, @TenLop, @MaKhoa, @SoLuong)";

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@MaLop", SqlDbType.VarChar) { Value = maLop },
        new SqlParameter("@TenLop", SqlDbType.VarChar) { Value = tenLop },
        new SqlParameter("@MaKhoa", SqlDbType.VarChar) { Value = maKhoa },
        new SqlParameter("@SoLuong", SqlDbType.VarChar) { Value = soLuong }
            };

            try
            {
                // Thực hiện thêm vào cơ sở dữ liệu
                bool isSuccess = KetNoi.Instance.ExcuteNonQuery(query, parameters);
                if (isSuccess)
                {
                    MessageBox.Show("Thêm lớp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadLopData(); // Gọi phương thức để tải lại dữ liệu từ cơ sở dữ liệu vào DataGridView
                }
                else
                {
                    MessageBox.Show("Không thể thêm lớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void LoadLopData()
        {
            string query = "SELECT * FROM LOPHOC";

            try
            {
                // Thực hiện truy vấn và lấy dữ liệu vào DataTable
                DataTable dataTable = KetNoi.Instance.ExcuteQuery(query);

                // Thiết lập DataPropertyName cho các cột trong DataGridView
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.Columns.Clear();

                // Thêm các cột vào DataGridView với tên và DataPropertyName tương ứng
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "ID", HeaderText = "ID", DataPropertyName = "ID" });

                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "MaLop", HeaderText = "Mã Lớp", DataPropertyName = "MALOP" });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "TenLop", HeaderText = "Tên Lớp", DataPropertyName = "TENLOP" });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "MaKhoa", HeaderText = "Mã Khoa", DataPropertyName = "MAKHOA" });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "SoLuong", HeaderText = "Số Lượng", DataPropertyName = "SOLUONG" });

                // Gán nguồn dữ liệu cho DataGridView
                dataGridView1.DataSource = dataTable;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu lớp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Lấy ID từ hàng được chọn
                string currentId = selectedRow.Cells["ID"].Value.ToString();

                // Kiểm tra tính hợp lệ của thông tin nhập
                if (string.IsNullOrEmpty(txbID.Text) || string.IsNullOrEmpty(txbTenLop.Text))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Cập nhật thông tin vào DataGridView
                selectedRow.Cells["ID"].Value = txbID.Text;
                selectedRow.Cells["TenLop"].Value = txbTenLop.Text;

                // Cập nhật vào cơ sở dữ liệu
                string query = "UPDATE LOP SET TenLop = @TenLop WHERE ID = @Id";
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@Id", SqlDbType.VarChar) { Value = currentId },
            new SqlParameter("@TenLop", SqlDbType.VarChar) { Value = txbTenLop.Text }
                };

                try
                {
                    bool isSuccess = KetNoi.Instance.ExcuteNonQuery(query, parameters);
                    if (isSuccess)
                    {
                        MessageBox.Show("Cập nhật lớp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadLopData(); // Tải lại dữ liệu
                    }
                    else
                    {
                        MessageBox.Show("Không thể cập nhật lớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hàng để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
                            try
                            {
                                // Lấy ID từ cột ID trong DataGridView
                                string id = row.Cells["ID"].Value.ToString(); // Thay đổi tên cột nếu khác

                                // Câu lệnh SQL DELETE
                                string query = "DELETE FROM LOPHOC WHERE ID = @Id"; // Thay 'LOP' bằng tên bảng đúng nếu khác
                                SqlParameter[] parameters = new SqlParameter[]
                                {
                            new SqlParameter("@Id", SqlDbType.VarChar) { Value = id }
                                };

                                bool isSuccess = KetNoi.Instance.ExcuteNonQuery(query, parameters);
                                if (isSuccess)
                                {
                                    MessageBox.Show("Xóa lớp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    dataGridView1.Rows.Remove(row); // Xóa hàng khỏi DataGridView
                                }
                                else
                                {
                                    MessageBox.Show("Không thể xóa lớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void LoadMaKhoa()
        {
            string query = "SELECT  MAKHOA FROM KHOA"; // Truy vấn để lấy các giá trị khác nhau trong cột LOAI
            try
            {
                DataTable dataTable = KetNoi.Instance.ExcuteQuery(query);
                cmbMaKhoa.DataSource = dataTable;
                cmbMaKhoa.DisplayMember = "MAKHOA"; // Tên cột chứa dữ liệu loại điểm trong bảng BANGDIEM
                cmbMaKhoa.ValueMember = "MAKHOA";
                cmbMaKhoa.SelectedIndex = -1; // Không chọn mục nào ban đầu
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu loại điểm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void QuanLiLop_Load(object sender, EventArgs e)
        {
            LoadLopData();
            LoadMaKhoa();
        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Lấy hàng được chọn
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                string currentMaLop = selectedRow.Cells["MaLop"].Value.ToString(); // Lấy mã lớp từ hàng được chọn

                // Kiểm tra tính hợp lệ của thông tin nhập
                if (string.IsNullOrEmpty(txbMaLop.Text) || string.IsNullOrEmpty(txbTenLop.Text) || string.IsNullOrEmpty(cmbMaKhoa.Text))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Cập nhật thông tin vào DataGridView
                selectedRow.Cells["MaLop"].Value = txbMaLop.Text.Trim();
                selectedRow.Cells["TenLop"].Value = txbTenLop.Text.Trim();
                selectedRow.Cells["MaKhoa"].Value = cmbMaKhoa.SelectedValue.ToString(); // Cập nhật mã khoa
                selectedRow.Cells["SoLuong"].Value = numSoLuong.Value; // Số lượng

                // Cập nhật vào cơ sở dữ liệu
                string query = "UPDATE LOPHOC SET TENLOP = @TenLop, MAKHOA = @MaKhoa, SOLUONG = @SoLuong WHERE MALOP = @MaLop";
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@MaLop", SqlDbType.VarChar) { Value = currentMaLop },
            new SqlParameter("@TenLop", SqlDbType.VarChar) { Value = txbTenLop.Text.Trim() },
            new SqlParameter("@MaKhoa", SqlDbType.VarChar) { Value = cmbMaKhoa.SelectedValue.ToString() },
            new SqlParameter("@SoLuong", SqlDbType.Int) { Value = (int)numSoLuong.Value }
                };

                try
                {
                    bool isSuccess = KetNoi.Instance.ExcuteNonQuery(query, parameters);
                    if (isSuccess)
                    {
                        MessageBox.Show("Cập nhật lớp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadLopData(); // Tải lại dữ liệu từ cơ sở dữ liệu vào DataGridView
                    }
                    else
                    {
                        MessageBox.Show("Không thể cập nhật lớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Xóa trắng các TextBox
                txbMaLop.Clear();
                txbTenLop.Clear();
                cmbMaKhoa.SelectedIndex = -1; // Đặt lại lựa chọn của ComboBox
                numSoLuong.Value = 0; // Đặt lại giá trị số lượng
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hàng để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}
