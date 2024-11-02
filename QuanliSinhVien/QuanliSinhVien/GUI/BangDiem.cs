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
    public partial class BangDiem : Form
    {
        public BangDiem()
        {
            InitializeComponent();
        }

        private void BangDiem_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void LoadBangDiemData()
        {
            string query = "SELECT * FROM BANGDIEM"; // Truy vấn để lấy dữ liệu từ bảng điểm

            try
            {
                // Thực hiện truy vấn và lấy dữ liệu vào DataTable
                DataTable dataTable = KetNoi.Instance.ExcuteQuery(query);

                // Thiết lập DataPropertyName cho các cột trong DataGridView
                dataGridView1.AutoGenerateColumns = false; // Không tự động tạo cột
                dataGridView1.Columns.Clear(); // Xóa các cột hiện có

                // Thêm các cột vào DataGridView với tên và DataPropertyName tương ứng
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "ID", HeaderText = "ID", DataPropertyName = "ID" });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "MaSV", HeaderText = "Mã Sinh Viên", DataPropertyName = "MaSV" });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "MaMH", HeaderText = "Mã Môn Học", DataPropertyName = "MaMH" });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Loai", HeaderText = "Loại Điểm", DataPropertyName = "Loai" });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "DiemThi", HeaderText = "Điểm Thi", DataPropertyName = "DiemThi" });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "DiemTrenLop", HeaderText = "Điểm Trên Lớp", DataPropertyName = "DiemTrenLop" });

                // Gán nguồn dữ liệu cho DataGridView
                dataGridView1.DataSource = dataTable;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu bảng điểm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void txbMaSV_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
