using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Quan_Ly_Lich_Thuc_Hanh_Phong_May
{
    internal class TonSQL
    {
        SqlConnection conn;
        public void taoketnoi()
        {
            string ketnoi = "server=DESKTOP-8SM0K2C;" +
                                                          "uid=nbich;pwd=tranthingocbich1412;" +
                                                          "database=QuanLyLichThucHanh";

            conn = new SqlConnection(ketnoi);
            conn.Open();
        }
        public void dongketnoi()
        {
            conn.Close();
            conn.Dispose();
        }
        public DataTable laydanhsachnhanvien()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand("[dbo].[laydanhsachnhanvien]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }

            return dt;
        }

        public DataTable laydanhsachchucvu()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand("[dbo].[laydanhsachchucvu]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }

            return dt;
        }

        public void LayTenChucVu(ComboBox comboBox)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[laydanhsachtenchucvu]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    comboBox.DataSource = dt;
                    comboBox.DisplayMember = "Tên Chức vụ"; // Tên chức vụ hiển thị
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }
        }
        public bool timkiemmacv(string a)
        {
            bool co = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[timmach]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số cho stored procedure
                    cmd.Parameters.AddWithValue("@cv",a);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))                   {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        // Kiểm tra nếu có dòng nào thì tồn tại mã chức vụ
                        if (dt.Rows.Count > 0)
                        {
                            co = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }
            return co;
        }
        public bool timkiemtencv(string a, string b)
        {
            bool co = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[timtencv]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Đúng thứ tự tham số
                    cmd.Parameters.AddWithValue("@macv", a);
                    cmd.Parameters.AddWithValue("@ten", b);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            co = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }
            return co;
        }

        public void suachucvu(string a,string b, string c)
        {


            try
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[suachucvu]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Thêm tham số cho stored procedure
                    cmd.Parameters.AddWithValue("@macv", a);
                    cmd.Parameters.AddWithValue("@Tencv", b);
                    cmd.Parameters.AddWithValue("@dm", c);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }
        }
        public void themchucvu(string a, string b, string c)
        {


            try
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[themcv]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Thêm tham số cho stored procedure
                    cmd.Parameters.AddWithValue("@macv", a);
                    cmd.Parameters.AddWithValue("@ten", b);
                    cmd.Parameters.AddWithValue("@dm", c);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }
        }
        public DataTable laydanhsachtimkiemchucvu(string a, string b,string c)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand("[dbo].[laydanhsachtimkiemchucvu]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@macv", a);
                cmd.Parameters.AddWithValue("@tencv", b);
                cmd.Parameters.AddWithValue("@dm", c);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }

            return dt;
        }

        public bool timkiemmanv(string a)
        {
            bool co = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[timmanv]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Đúng thứ tự tham số
                    cmd.Parameters.AddWithValue("@manv", a);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            co = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }
            return co;


        }
        public void themnhanvien(string ma,string ten,string email,string sdt,string diachi,DateTime ns,bool gt,string tencv) {

            try
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[themnhanvien]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Thêm tham số cho stored procedure
                    cmd.Parameters.AddWithValue("@manv", ma);
                    cmd.Parameters.AddWithValue("@tennv", ten);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@sdt", sdt);
                    cmd.Parameters.AddWithValue("@diachi", diachi);
                    cmd.Parameters.AddWithValue("@ns", ns);
                    cmd.Parameters.AddWithValue("@gt", gt);
                    cmd.Parameters.AddWithValue("@tencv", tencv);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }
        }
        public void suanhanvien(string ma, string ten, string email, string sdt, string diachi, DateTime ns, bool gt, string tencv)
        {

            try
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[suanhanvien]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Thêm tham số cho stored procedure
                    cmd.Parameters.AddWithValue("@manv", ma);
                    cmd.Parameters.AddWithValue("@tennv", ten);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@sdt", sdt);
                    cmd.Parameters.AddWithValue("@diachi", diachi);
                    cmd.Parameters.AddWithValue("@ns", ns);
                    cmd.Parameters.AddWithValue("@gt", gt);
                    cmd.Parameters.AddWithValue("@tencv", tencv);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }
        }
        public bool timemail(string a,string b)
        {
            bool co = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[timemail]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Đúng thứ tự tham số
                    cmd.Parameters.AddWithValue("@email", a);
                    cmd.Parameters.AddWithValue("@manv", b);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            co = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }
            return co;


        }
        public bool timsdt(string a, string b)
        {
            bool co = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[timsdt]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Đúng thứ tự tham số
                    cmd.Parameters.AddWithValue("@sdt", a);
                    cmd.Parameters.AddWithValue("@manv", b);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            co = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }
            return co;


        }
        public DataTable laydanhsachtimkiemnv(string a,string b)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand("[dbo].[laydanhsachtimkiemnv]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@manv", a);
                cmd.Parameters.AddWithValue("@ten", b);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }

            return dt;

        }
        public void xoachucvu(string a)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[xoachucvu]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Thêm tham số cho stored procedure
                    cmd.Parameters.AddWithValue("@macv", a);
                   
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }


        }
        public bool kiemtranhanviencophieutaitructrongtuonglaikhong(string a,DateTime ngay)
        {
            bool co = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[kiemtranhanviencophieutaitructrongtuonglaikhong]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@manv", a);
                    cmd.Parameters.AddWithValue("@date", ngay);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            co = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }
            return co;


        }
        public void xoanhanvien(string a)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[xoanhanvien]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Thêm tham số cho stored procedure
                    cmd.Parameters.AddWithValue("@manv", a);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }


        }
        public DataTable laydanhthanhtoan()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand("[dbo].[laydulieu]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }

            return dt;
        }
        public bool thanhtoanchua(string a, string b,string c)
        {
            bool co = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[thanhtoanchua]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@manv", a);
                    cmd.Parameters.AddWithValue("@nam", b);
                    cmd.Parameters.AddWithValue("@thang", c);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            co = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }
            return co;


        }
        public void thanhtoanchonv(string a,string b, string c)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[thanhtoanchonv]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Thêm tham số cho stored procedure
                    cmd.Parameters.AddWithValue("@manv", a);
                    cmd.Parameters.AddWithValue("@nam", b);
                    cmd.Parameters.AddWithValue("@thang", c);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }


        }


    }
}
