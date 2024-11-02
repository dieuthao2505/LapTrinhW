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
    public partial class QuanLyDiem : Form
    {
        private List<Diem> danhSachDiem = new List<Diem>();
        public QuanLyDiem()
        {
            InitializeComponent();
        }

        private void cmbMaMH_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        
        private void btnThem_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ các điều khiển
            string maSV = cmbMaSV.SelectedItem?.ToString();
            string maMH = cmbMaMH.SelectedItem?.ToString();
            string loai = cmbLoai.SelectedItem?.ToString();
            decimal phanTramTrenLop = numPhanTramTrenLop.Value;
            decimal phanTramThi = numPhanTramThi.Value;
            decimal diemTB = decimal.Parse(txtDiemTB.Text);
            decimal diemThi = decimal.Parse(txbDiemThi.Text);




            // Kiểm tra tính hợp lệ của dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(maSV) || string.IsNullOrWhiteSpace(maMH) || string.IsNullOrWhiteSpace(loai))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Câu lệnh SQL để thêm vào cơ sở dữ liệu
            string query = "INSERT INTO BANGDIEM ( MASV, MAMH, LOAI, DIEMTB, PHANTRAMTRENLOP, PHANTRAMTHI, DIEMTHI) " +
                           "VALUES (@MaSV, @MaMH, @Loai, @DiemThi, @PhanTramTrenLop, @PhanTramThi, @DiemTB)";

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@MaSV", SqlDbType.VarChar) { Value = maSV },
        new SqlParameter("@MaMH", SqlDbType.VarChar) { Value = maMH },
        new SqlParameter("@Loai", SqlDbType.VarChar) { Value = loai },
        new SqlParameter("@DiemThi", SqlDbType.Decimal) { Value = diemThi },
        new SqlParameter("@DiemTB", SqlDbType.Decimal) { Value = diemTB },

        new SqlParameter("@PhanTramTrenLop", SqlDbType.Decimal) { Value = phanTramTrenLop },
        new SqlParameter("@PhanTramThi", SqlDbType.Decimal) { Value = phanTramThi }
            };

            try
            {
                // Thực hiện thêm vào cơ sở dữ liệu
                bool isSuccess = KetNoi.Instance.ExcuteNonQuery(query, parameters);
                if (isSuccess)
                {
                    MessageBox.Show("Thêm điểm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDiemData(); // Gọi phương thức để tải lại dữ liệu từ cơ sở dữ liệu vào DataGridView
                }
                else
                {
                    MessageBox.Show("Không thể thêm điểm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadDiemData()
        {
            string query = "SELECT * FROM BANGDIEM";

            try
            {
                // Thực hiện truy vấn và lấy dữ liệu vào DataTable
                DataTable dataTable = KetNoi.Instance.ExcuteQuery(query);

                // Thiết lập DataPropertyName cho các cột trong DataGridView
                dataGridViewDiem.AutoGenerateColumns = false;
                dataGridViewDiem.Columns.Clear();

                // Thêm các cột vào DataGridView với tên và DataPropertyName tương ứng
                dataGridViewDiem.Columns.Add(new DataGridViewTextBoxColumn() { Name = "ID", HeaderText = "ID", DataPropertyName = "ID" });
                dataGridViewDiem.Columns.Add(new DataGridViewTextBoxColumn() { Name = "MaSV", HeaderText = "Mã Sinh Viên", DataPropertyName = "MaSV" });
                dataGridViewDiem.Columns.Add(new DataGridViewTextBoxColumn() { Name = "MaMH", HeaderText = "Mã Môn Học", DataPropertyName = "MaMH" });
                dataGridViewDiem.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Loai", HeaderText = "Loại Điểm", DataPropertyName = "Loai" });
                dataGridViewDiem.Columns.Add(new DataGridViewTextBoxColumn() { Name = "DiemThi", HeaderText = "Điểm Thi", DataPropertyName = "DiemThi" });
                dataGridViewDiem.Columns.Add(new DataGridViewTextBoxColumn() { Name = "PhanTramTrenLop", HeaderText = "Phần Trăm Trên Lớp", DataPropertyName = "PhanTramTrenLop" });
                dataGridViewDiem.Columns.Add(new DataGridViewTextBoxColumn() { Name = "PhanTramThi", HeaderText = "Phần Trăm Thi", DataPropertyName = "PhanTramThi" });
                dataGridViewDiem.Columns.Add(new DataGridViewTextBoxColumn() { Name = "DiemTB", HeaderText = "Điểm Trung Bình", DataPropertyName = "DiemTB" });

                // Gán nguồn dữ liệu cho DataGridView
                dataGridViewDiem.DataSource = dataTable;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu điểm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        
        private void QuanLyDiem_Load(object sender, EventArgs e)
        {
            LoadDiemData();
            LoadMaSinhVien();
            LoadMaMonHoc();
        }

        private void dataGridViewDiem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridViewDiem.Rows[e.RowIndex];
                cmbMaSV.SelectedItem = row.Cells["MaSV"].Value.ToString();
                cmbMaMH.SelectedItem = row.Cells["MaMH"].Value.ToString();
                cmbLoai.SelectedItem = row.Cells["Loai"].Value.ToString();
                numPhanTramThi.Value = Convert.ToDecimal(row.Cells["PhanTramThi"].Value);
                numPhanTramTrenLop.Value = Convert.ToDecimal(row.Cells["PhanTramTrenLop"].Value);
                txbDiemThi.Text = row.Cells["DiemThi"].Value.ToString();
            }

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridViewDiem.SelectedRows.Count == 1)
            {
                DataGridViewRow selectedRow = dataGridViewDiem.SelectedRows[0];

                // Lấy thông tin từ các điều khiển
                string maSV = cmbMaSV.SelectedItem?.ToString();
                string maMH = cmbMaMH.SelectedItem?.ToString();
                string loai = cmbLoai.SelectedItem?.ToString();
                decimal phanTramTrenLop = numPhanTramTrenLop.Value;
                decimal phanTramThi = numPhanTramThi.Value;
                decimal diemThi = decimal.Parse(txbDiemThi.Text.Trim());
                decimal diemTB = (phanTramTrenLop / 50) + (diemThi * phanTramThi / 100);

                // Kiểm tra tính hợp lệ
                if (!string.IsNullOrEmpty(maSV) && !string.IsNullOrEmpty(maMH) && !string.IsNullOrEmpty(loai))
                {
                    try
                    {
                        // Lấy ID từ dòng đã chọn
                        int id = Convert.ToInt32(selectedRow.Cells["ID"].Value); // Giả sử có cột ID trong DataGridView

                        // Câu lệnh SQL để cập nhật thông tin
                        string query = "UPDATE BANGDIEM SET MaSV = @MaSV, MaMH = @MaMH, Loai = @Loai, DiemThi = @DiemThi, PhanTramTrenLop = @PhanTramTrenLop, PhanTramThi = @PhanTramThi, DiemTB = @DiemTB " +
                                       "WHERE ID = @ID";

                        SqlParameter[] parameters = new SqlParameter[]
                        {
                    new SqlParameter("@ID", SqlDbType.Int) { Value = id },
                    new SqlParameter("@MaSV", SqlDbType.VarChar) { Value = maSV },
                    new SqlParameter("@MaMH", SqlDbType.VarChar) { Value = maMH },
                    new SqlParameter("@Loai", SqlDbType.VarChar) { Value = loai },
                    new SqlParameter("@DiemThi", SqlDbType.Decimal) { Value = diemThi },
                    new SqlParameter("@PhanTramTrenLop", SqlDbType.Decimal) { Value = phanTramTrenLop },
                    new SqlParameter("@PhanTramThi", SqlDbType.Decimal) { Value = phanTramThi },
                    new SqlParameter("@DiemTB", SqlDbType.Decimal) { Value = diemTB }
                        };

                        // Thực hiện câu lệnh cập nhật vào cơ sở dữ liệu
                        bool isSuccess = KetNoi.Instance.ExcuteNonQuery(query, parameters);
                        if (isSuccess)
                        {
                            MessageBox.Show("Sửa điểm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDiemData(); // Tải lại dữ liệu sau khi sửa
                        }
                        else
                        {
                            MessageBox.Show("Không thể sửa điểm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Lỗi khi cập nhật dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridViewDiem.SelectedRows.Count > 0)
            {
                // Hỏi người dùng xác nhận việc xóa
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa dòng này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in dataGridViewDiem.SelectedRows)
                    {
                        if (!row.IsNewRow) // Kiểm tra nếu không phải là hàng trống
                        {
                            try
                            {
                                int id = Convert.ToInt32(row.Cells["ID"].Value); // Lấy ID từ cột ID trong DataGridView

                                // Câu lệnh SQL DELETE
                                string query = "DELETE FROM BANGDIEM WHERE ID = @Id"; // Thay 'DIEM' bằng tên bảng đúng nếu khác
                                SqlParameter[] parameters = new SqlParameter[]
                                {
                            new SqlParameter("@Id", SqlDbType.Int) { Value = id }
                                };

                                bool isSuccess = KetNoi.Instance.ExcuteNonQuery(query, parameters);
                                if (isSuccess)
                                {
                                    MessageBox.Show("Xóa điểm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    LoadDiemData(); // Tải lại dữ liệu sau khi xóa
                                }
                                else
                                {
                                    MessageBox.Show("Không thể xóa điểm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void LoadMaMonHoc()
        {
            string query = "SELECT MAMONHOC FROM MONHOC";
            try
            {
                DataTable dataTable = KetNoi.Instance.ExcuteQuery(query);
                cmbMaMH.DataSource = dataTable;
                cmbMaMH.DisplayMember = "MAMONHOC"; // Tên cột chứa mã lớp trong bảng LOP
                cmbMaMH.ValueMember = "MAMONHOC";
                cmbMaMH.SelectedIndex = -1; // Không chọn mục nào ban đầu
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi khi tải Mã Lớp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadMaSinhVien()
        {
            string query = "SELECT MASINHVIEN FROM SINHVIEN";
            try
            {
                DataTable dataTable = KetNoi.Instance.ExcuteQuery(query);
                cmbMaSV.DataSource = dataTable;
                cmbMaSV.DisplayMember = "MASINHVIEN"; // Tên cột chứa mã lớp trong bảng LOP
                cmbMaSV.ValueMember = "MASINHVIEN";
                cmbMaSV.SelectedIndex = -1; // Không chọn mục nào ban đầu
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi khi tải Mã Lớp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadLoai()
        {
            string query = "SELECT LOAI FROM BANGDIEM"; // Truy vấn để lấy các giá trị khác nhau trong cột LOAI
            try
            {
                DataTable dataTable = KetNoi.Instance.ExcuteQuery(query);
                cmbLoai.DataSource = dataTable;
                cmbLoai.DisplayMember = "LOAI"; // Tên cột chứa dữ liệu loại điểm trong bảng BANGDIEM
                cmbLoai.ValueMember = "LOAI";
                cmbLoai.SelectedIndex = -1; // Không chọn mục nào ban đầu
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu loại điểm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void cmbMaSV_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbLoai_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }


    public class Diem
    {
        public string ID { get; set; }
        public string MaSV { get; set; }
        public string MaMH { get; set; }
        public decimal DiemThi { get; set; }
        public decimal DiemTrenLop { get; set; }
        public string Loai { get; set; }
    }
}
