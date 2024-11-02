using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;

namespace QuanliSinhVien.DAL
{
    internal class KetNoi
    {
        // Chuỗi kết nối cơ sở dữ liệu
        string connectionString = @"Data Source=LAPTOP-E01MG43U\SQLEXPRESS02;Initial Catalog=QLISNHVIEN;User ID=sa;Password=1234567;TrustServerCertificate=True;";


        private static KetNoi instance;

        public static KetNoi Instance
        {
            get
            {
                if (instance == null)
                    instance = new KetNoi();
                return instance;
            }
        }

        private KetNoi() { }

        // Lấy danh sách
        public DataTable ExcuteQuery(string query, SqlParameter[] parameters = null)
        {
            DataTable data = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        AddParameters(command, query, parameters);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(data);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                Console.WriteLine("Error Code: " + ex.ErrorCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Error: " + ex.Message);
            }
            return data;
        }

        // Thêm, sửa, xoá
        public bool ExcuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            int result = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open(); // Mở kết nối
                    if (connection.State == ConnectionState.Open) // Kiểm tra trạng thái kết nối
                    {
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            AddParameters(command, query, parameters);
                            result = command.ExecuteNonQuery(); // Thực thi nếu kết nối đã mở thành công
                        }
                    }
                    else
                    {
                        Console.WriteLine("Kết nối không được mở thành công.");
                        return false;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                Console.WriteLine("Error Code: " + ex.ErrorCode);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Error: " + ex.Message);
                return false;
            }
            return result > 0;
        }

        // Phương thức hỗ trợ để thêm tham số
        private void AddParameters(SqlCommand command, string query, SqlParameter[] parameters)
        {
            if (parameters != null)
            {
                // In thông tin từng tham số
                for (int i = 0; i < parameters.Length; i++)
                {
                    Console.WriteLine($"Tham số {i + 1}: Tên = {parameters[i].ParameterName}, Giá trị = {parameters[i].Value}, Kiểu = {parameters[i].Value?.GetType()}");
                }

                // Tìm tất cả các tham số trong câu lệnh SQL (các chuỗi bắt đầu bằng '@')
                var parameterNames = Regex.Matches(query, @"@\w+")
                                          .Cast<Match>()
                                          .Select(m => m.Value)
                                          .ToArray();

                // Đảm bảo số lượng tên tham số khớp với số lượng giá trị trong `parameters`
                if (parameterNames.Length != parameters.Length)
                {
                    throw new ArgumentException("Số lượng tên tham số không khớp với số lượng giá trị.");
                }

                // Thêm các `SqlParameter` vào `command.Parameters`
                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }
        }
        public object ExecuteScalar(string query, SqlParameter[] parameters = null)
        {
            object result = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }
                        result = command.ExecuteScalar();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
            }

            return result;
        }




        // Hàm để xác định SqlDbType dựa trên kiểu của đối tượng
        private SqlDbType GetSqlDbType(object value, bool isDateOnly = true)
        {
            if (value == null)
            {
                return GetSqlDbType(Type.GetTypeCode(value.GetType())); // Chọn kiểu mặc định nếu giá trị là null
            }

            switch (Type.GetTypeCode(value.GetType()))
            {
                case TypeCode.Int32:
                    return SqlDbType.Int;
                case TypeCode.String:
                    return SqlDbType.VarChar;
                case TypeCode.DateTime:
                    return isDateOnly ? SqlDbType.Date : SqlDbType.DateTime;
                case TypeCode.Boolean:
                    return SqlDbType.Bit;
                case TypeCode.Decimal:
                    return SqlDbType.Decimal;
                case TypeCode.Double:
                    return SqlDbType.Float;
                case TypeCode.Byte:
                    return SqlDbType.TinyInt;
                
                default:
                    return SqlDbType.Char;
            }
        }


    }
}
