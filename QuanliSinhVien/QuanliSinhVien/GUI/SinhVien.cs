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
    public partial class SinhVien : Form

    {
        private List<QLSinhVien> danhSachSinhVien = new List<QLSinhVien>();
        public SinhVien()
        {
            InitializeComponent();

        }

        private void quảnLýToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            //f.ShowDialog();
            this.Show();
        }

        private void txbTenSV_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maSV = txbMaSV.Text;
            string tenSV = txbTenSV.Text;
            DateTime ngaySinh = dtpkNgaySinh.Value;
            string gioiTinh = rdNam.Checked ? "Nam" : "Nữ";
            string queQuan = txbQueQuan.Text;
            DateTime ngayNhapHoc = dtpkNgayNhapHoc.Value;
            string maKhoa = txbMaKhoa.Text;
            string maLop = txbMaLop.Text;
            string maCoVan = txbMaCoVan.Text;
        


            string query = "INSERT INTO SINHVIEN (MASINHVIEN, TENSINHVIEN, NGAYSINH, GIOITINH, QUEQUAN, NGAYNHAPHOC, MALOP, MAKHOA, MACOVAN) " +
                           "VALUES ( @MaSV, @TenSV, @NgaySinh, @GioiTinh, @QueQuan, @NgayNhapHoc, @MaLop, @MaKhoa, @MaCoVan)";

            SqlParameter[] parameters = new SqlParameter[]
{
    new SqlParameter("@MaSV", SqlDbType.VarChar) { Value = maSV },
    new SqlParameter("@TenSV", SqlDbType.VarChar) { Value = tenSV },
    new SqlParameter("@NgaySinh", SqlDbType.Date) { Value = ngaySinh },
    new SqlParameter("@GioiTinh", SqlDbType.VarChar) { Value = gioiTinh },
    new SqlParameter("@QueQuan", SqlDbType.VarChar) { Value = queQuan },
    new SqlParameter("@NgayNhapHoc", SqlDbType.Date) { Value = ngayNhapHoc },
        new SqlParameter("@MaLop", SqlDbType.VarChar) { Value = maLop },
            new SqlParameter("@MaKhoa", SqlDbType.VarChar) { Value = maKhoa },
                new SqlParameter("@MaCoVan", SqlDbType.VarChar) { Value = maCoVan },
                





        };

            try
            {
                bool isSuccess = KetNoi.Instance.ExcuteNonQuery(query, parameters);
                if (isSuccess)
                {
                    MessageBox.Show("Thêm sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadSinhVienData();
                }
                else
                {
                    MessageBox.Show("Không thể thêm sinh viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvSinhVien.SelectedRows.Count > 0)
            {
                // Lấy hàng được chọn
                DataGridViewRow row = dgvSinhVien.SelectedRows[0];
                if (!int.TryParse(txbID.Text, out int id))
                {
                    MessageBox.Show("ID không hợp lệ. Vui lòng kiểm tra lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Lấy thông tin từ các TextBox
                string maSV = txbMaSV.Text.Trim();
                string tenSV = txbTenSV.Text.Trim();
                DateTime ngaySinh = dtpkNgaySinh.Value;
                string gioiTinh = rdNam.Checked ? "Nam" : "Nữ";
                string queQuan = txbQueQuan.Text.Trim();
                DateTime ngayNhapHoc = dtpkNgayNhapHoc.Value;
                string maKhoa = txbMaKhoa.Text.Trim();
                string maLop = txbMaLop.Text.Trim();
                string maCoVan = txbMaCoVan.Text.Trim();

                // Kiểm tra tính hợp lệ của thông tin
                if (string.IsNullOrWhiteSpace(maSV) || string.IsNullOrWhiteSpace(tenSV))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin sinh viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Câu lệnh SQL để cập nhật
                string query = "UPDATE SINHVIEN SET MASINHVIEN = @MaSV, TENSINHVIEN = @TenSV, NGAYSINH = @NgaySinh, GIOITINH = @GioiTinh, " +
                               "QUEQUAN = @QueQuan, NGAYNHAPHOC = @NgayNhapHoc, MALOP = @MaLop, MAKHOA = @MaKhoa, MACOVAN = @MaCoVan " +
                               "WHERE ID = @Id";

                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@MaSV", SqlDbType.VarChar) { Value = maSV },
            new SqlParameter("@TenSV", SqlDbType.VarChar) { Value = tenSV },
            new SqlParameter("@NgaySinh", SqlDbType.Date) { Value = ngaySinh },
            new SqlParameter("@GioiTinh", SqlDbType.VarChar) { Value = gioiTinh },
            new SqlParameter("@QueQuan", SqlDbType.VarChar) { Value = queQuan },
            new SqlParameter("@NgayNhapHoc", SqlDbType.Date) { Value = ngayNhapHoc },
            new SqlParameter("@MaLop", SqlDbType.VarChar) { Value = maLop },
            new SqlParameter("@MaKhoa", SqlDbType.VarChar) { Value = maKhoa },
            new SqlParameter("@MaCoVan", SqlDbType.VarChar) { Value = maCoVan },
            new SqlParameter("@Id", SqlDbType.Int) { Value = id }
                };

                try
                {
                    bool isSuccess = KetNoi.Instance.ExcuteNonQuery(query, parameters);
                    if (isSuccess)
                    {
                        MessageBox.Show("Cập nhật sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadSinhVienData(); // Tải lại dữ liệu sau khi cập nhật
                    }
                    else
                    {
                        MessageBox.Show("Không thể cập nhật sinh viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hàng để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }





        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvSinhVien.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa sinh viên này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in dgvSinhVien.SelectedRows)
                    {
                        if (!row.IsNewRow) // Kiểm tra nếu không phải là hàng trống
                        {
                            try
                            {
                                int id = Convert.ToInt32(row.Cells["ID"].Value); // Lấy ID từ cột `ID` trong DataGridView

                                // Câu lệnh SQL DELETE
                                string query = "DELETE FROM SINHVIEN WHERE ID = @Id";
                                SqlParameter[] parameters = new SqlParameter[]
                                {
                            new SqlParameter("@Id", SqlDbType.Int) { Value = id }
                                };

                                bool isSuccess = KetNoi.Instance.ExcuteNonQuery(query, parameters);
                                if (isSuccess)
                                {
                                    MessageBox.Show("Xóa sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    LoadSinhVienData(); // Tải lại dữ liệu sau khi xóa
                                }
                                else
                                {
                                    MessageBox.Show("Không thể xóa sinh viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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



        private void dgvSinhVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvSinhVien.Rows.Count)
            {
                DataGridViewRow row = dgvSinhVien.Rows[e.RowIndex];

                // Gán giá trị từ DataGridView vào các TextBox
                txbID.Text = row.Cells["ID"].Value?.ToString() ?? string.Empty;
                txbMaSV.Text = row.Cells["MaSinhVien"].Value.ToString();
                txbTenSV.Text = row.Cells["TenSinhVien"].Value.ToString();
                dtpkNgaySinh.Value = DateTime.Parse(row.Cells["NgaySinh"].Value.ToString());
                rdNam.Checked = row.Cells["GioiTinh"].Value.ToString() == "Nam";
                rdNu.Checked = !rdNam.Checked;
                txbQueQuan.Text = row.Cells["QueQuan"].Value.ToString();
            }
        }

        private void txbMaSV_TextChanged(object sender, EventArgs e)
        {

        }

        private void quảnLýLớpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuanLyLop f = new QuanLyLop();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void quảnLýCốVấnHọcTậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CoVanHocTap f = new CoVanHocTap();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void quảnLýMônHọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuanLyMonHoc f = new QuanLyMonHoc();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void quảnLýTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void quảnLýDiểmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuanLyDiem f = new QuanLyDiem();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void quảnLýKhoa_Click(object sender, EventArgs e)
        {
            QuanLyKhoa f = new QuanLyKhoa();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chứcNăngToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void LoadSinhVienData()
        {
            string query = "SELECT * FROM SINHVIEN";

            try
            {
                DataTable dataTable = KetNoi.Instance.ExcuteQuery(query);

                // Thiết lập DataPropertyName cho các cột trong DataGridView
                dgvSinhVien.AutoGenerateColumns = false;

                // Xóa các cột hiện có trong DataGridView (nếu có)
                dgvSinhVien.Columns.Clear();

                // Thêm cột và thiết lập DataPropertyName tương ứng
                dgvSinhVien.Columns.Add(new DataGridViewTextBoxColumn() { Name = "ID", HeaderText = "ID", DataPropertyName = "ID" });
                dgvSinhVien.Columns.Add(new DataGridViewTextBoxColumn() { Name = "MaSinhVien", HeaderText = "Mã SV", DataPropertyName = "MASINHVIEN" });
                dgvSinhVien.Columns.Add(new DataGridViewTextBoxColumn() { Name = "TenSinhVien", HeaderText = "Tên SV", DataPropertyName = "TENSINHVIEN" });
                dgvSinhVien.Columns.Add(new DataGridViewTextBoxColumn() { Name = "NgaySinh", HeaderText = "Ngày Sinh", DataPropertyName = "NGAYSINH" });
                dgvSinhVien.Columns.Add(new DataGridViewTextBoxColumn() { Name = "GioiTinh", HeaderText = "Giới Tính", DataPropertyName = "GIOITINH" });
                dgvSinhVien.Columns.Add(new DataGridViewTextBoxColumn() { Name = "QueQuan", HeaderText = "Quê Quán", DataPropertyName = "QUEQUAN" });
                dgvSinhVien.Columns.Add(new DataGridViewTextBoxColumn() { Name = "NgayNhapHoc", HeaderText = "Ngày Nhập Học", DataPropertyName = "NGAYNHAPHOC" });
                dgvSinhVien.Columns.Add(new DataGridViewTextBoxColumn() { Name = "MaLop", HeaderText = "Mã Lớp", DataPropertyName = "MALOP" });
                dgvSinhVien.Columns.Add(new DataGridViewTextBoxColumn() { Name = "MaKhoa", HeaderText = "Mã Khoa", DataPropertyName = "MAKHOA" });
                dgvSinhVien.Columns.Add(new DataGridViewTextBoxColumn() { Name = "MaCoVan", HeaderText = "Mã Cố Vấn", DataPropertyName = "MACOVAN" });

                // Gán nguồn dữ liệu cho DataGridView
                dgvSinhVien.DataSource = dataTable;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu sinh viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void SinhVien_Load(object sender, EventArgs e)
        {
            LoadSinhVienData();
        }
        public class QLSinhVien
        {
            public string ID { get; set; }
            public string MaSV { get; set; }
            public string TenSV { get; set; }
            public DateTime NgaySinh { get; set; }
            public string GioiTinh { get; set; }
            public string QueQuan { get; set; }
            public string MaLop { get; set; }
            public string MaKhoa { get; set; }
            public string MaCoVan { get; set; }
            public DateTime NgayNhapHoc { get; set; }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThongTinChiTiet f = new ThongTinChiTiet();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void đổiMậtKhẩuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoiMatKhau f = new DoiMatKhau();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DangNhap f = new DangNhap();
            f.Show();
            this.Close();
        }

        private void txbQueQuan_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
