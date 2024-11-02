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
    public partial class QuanLyKhoa : Form
    {
        private List<Khoa> danhSachKhoa = new List<Khoa>();
        public QuanLyKhoa()
        {
            InitializeComponent();

        }

        private void txbID_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ các điều khiển
            string maKhoa = txbMaKhoa.Text.Trim();
            string tenKhoa = txbTenKhoa.Text.Trim();

            // Kiểm tra nếu thông tin chưa đầy đủ
            if (string.IsNullOrEmpty(maKhoa) || string.IsNullOrEmpty(tenKhoa))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Câu lệnh SQL để thêm vào cơ sở dữ liệu
            string query = "INSERT INTO KHOA (MAKHOA, TENKHOA) VALUES ( @MaKhoa, @TenKhoa)";

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@MaKhoa", SqlDbType.VarChar) { Value = maKhoa },
        new SqlParameter("@TenKhoa", SqlDbType.VarChar) { Value = tenKhoa }
            };

            try
            {
                // Thực hiện thêm vào cơ sở dữ liệu
                bool isSuccess = KetNoi.Instance.ExcuteNonQuery(query, parameters);
                if (isSuccess)
                {
                    MessageBox.Show("Thêm khoa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadKhoaData(); // Gọi phương thức để tải lại dữ liệu từ cơ sở dữ liệu vào DataGridView
                }
                else
                {
                    MessageBox.Show("Không thể thêm khoa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Xóa trắng các TextBox sau khi thêm
            txbID.Clear();
            txbMaKhoa.Clear();
            txbTenKhoa.Clear();
        }
        private void LoadKhoaData()
        {
            string query = "SELECT * FROM KHOA";

            try
            {
                // Thực hiện truy vấn và lấy dữ liệu vào DataTable
                DataTable dataTable = KetNoi.Instance.ExcuteQuery(query);

                // Thiết lập DataPropertyName cho các cột trong DataGridView
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.Columns.Clear();

                // Thêm các cột vào DataGridView với tên và DataPropertyName tương ứng
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "ID", HeaderText = "ID", DataPropertyName = "ID" });

                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "MaKhoa", HeaderText = "Mã Khoa", DataPropertyName = "MAKHOA" });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "TenKhoa", HeaderText = "Tên Khoa", DataPropertyName = "TENKHOA" });

                // Gán nguồn dữ liệu cho DataGridView
                dataGridView1.DataSource = dataTable;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu khoa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Lấy hàng được chọn
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                string currentId = row.Cells["ID"].Value.ToString();

                // Kiểm tra nếu thông tin trong TextBox có hợp lệ
                if (string.IsNullOrEmpty(txbID.Text) || string.IsNullOrEmpty(txbTenKhoa.Text))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Cập nhật giá trị từ các TextBox vào DataGridView
                row.Cells["ID"].Value = txbID.Text;
                row.Cells["TenKhoa"].Value = txbTenKhoa.Text;

                // Cập nhật dữ liệu vào cơ sở dữ liệu
                string query = "UPDATE KHOA SET TenKhoa = @TenKhoa WHERE ID = @Id";
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@TenKhoa", SqlDbType.VarChar) { Value = txbTenKhoa.Text },
            new SqlParameter("@Id", SqlDbType.VarChar) { Value = currentId }
                };

                try
                {
                    bool isSuccess = KetNoi.Instance.ExcuteNonQuery(query, parameters);
                    if (isSuccess)
                    {
                        MessageBox.Show("Cập nhật khoa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadKhoaData(); // Tải lại dữ liệu từ cơ sở dữ liệu vào DataGridView
                    }
                    else
                    {
                        MessageBox.Show("Không thể cập nhật khoa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Sau khi sửa, xóa trắng các TextBox
                txbID.Clear();
                txbTenKhoa.Clear();
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
                            string id = row.Cells["ID"].Value.ToString(); // Lấy ID từ cột ID trong DataGridView

                            // Câu lệnh SQL DELETE
                            string query = "DELETE FROM KHOA WHERE ID = @Id"; // Thay 'KHOA' bằng tên bảng đúng nếu khác
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
                                    MessageBox.Show("Không thể xóa khoa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy hàng được chọn
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Gán giá trị từ DataGridView vào các TextBox
                txbID.Text = row.Cells["ID"].Value.ToString();
                txbMaKhoa.Text = row.Cells["MaKhoa"].Value.ToString();
                txbTenKhoa.Text = row.Cells["TenKhoa"].Value.ToString();
            }
        }
        public class Khoa
        {
            public string ID { get; set; }
            public string MaKhoa { get; set; }
            public string TenKhoa { get; set; }
        }

        private void QuanLyKhoa_Load(object sender, EventArgs e)
        {
            LoadKhoaData();
        }
    }
}
