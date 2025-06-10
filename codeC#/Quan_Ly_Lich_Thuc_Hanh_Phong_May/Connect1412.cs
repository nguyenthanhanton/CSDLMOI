using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Quan_Ly_Lich_Thuc_Hanh_Phong_May
{
    internal class Connect1412
    {
        private static readonly string ConnectionString = "server=DESKTOP-8SM0K2C;" +
                                                          "uid=nbich;pwd=tranthingocbich1412;" +
                                                          "database=QuanLyLichThucHanh";

        private SqlConnection conn;

        public SqlConnection Connection => conn;

        public void Connect()
        {
            try
            {
                if (conn == null)
                    conn = new SqlConnection(ConnectionString);

                if (conn.State != ConnectionState.Open)
                    conn.Open();

                if (conn.State != ConnectionState.Open)
                {
                    MessageBox.Show("Kết nối thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối CSDL: " + ex.Message);
            }
        }

        public void Disconnect()
        {
            if (conn != null && conn.State == ConnectionState.Open)
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
        }

        // Hàm tự kiểm tra kết nối, nếu chưa mở thì mở trước khi truy vấn
        private void EnsureConnectionOpen()
        {
            if (conn == null)
                conn = new SqlConnection(ConnectionString);

            if (conn.State != ConnectionState.Open)
                conn.Open();
        }

        public DataTable ExecuteQuery(string query)
        {
            DataTable dt = new DataTable();

            try
            {
                EnsureConnectionOpen();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi truy vấn: " + ex.Message);
            }

            return dt;
        }

        public int ExecuteNonQuery(string query)
        {
            int result = -1;
            try
            {
                EnsureConnectionOpen();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thực thi: " + ex.Message);
            }
            return result;
        }

        public SqlDataReader ExecuteReader(string query)
        {
            try
            {
                EnsureConnectionOpen();

                SqlCommand cmd = new SqlCommand(query, conn);
                return cmd.ExecuteReader(); // Caller nhớ đóng reader và connection nếu cần
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi truy vấn: " + ex.Message);
                return null;
            }
        }
    }
}
