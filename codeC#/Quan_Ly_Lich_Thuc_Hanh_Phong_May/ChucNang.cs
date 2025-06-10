using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_Ly_Lich_Thuc_Hanh_Phong_May
{
    class ChucNang
    {
        DataConnect dc;
        SqlDataAdapter da;
        SqlCommand cmd;
        public ChucNang()
        {
            dc = new DataConnect();
        }
        public DataTable GetAllGV()
        {
            DataTable dt = new DataTable();
            SqlConnection con = dc.getConnect(); // đảm bảo dc.getConnect() hoạt động đúng

            using (SqlCommand cmd = new SqlCommand("Hieu_GetAllGiangVien", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                con.Open();
                da.Fill(dt);
                con.Close();
            }

            return dt;
        }

        public DataTable GetAllPM()
        {
            DataTable dt = new DataTable();
            SqlConnection con = dc.getConnect(); // đảm bảo dc.getConnect() hoạt động đúng

            using (SqlCommand cmd = new SqlCommand("Hieu_GetAllPM", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                con.Open();
                da.Fill(dt);
                con.Close();
            }

            return dt;
        }

        public bool ThemGV(tblGV gv)
        {
            SqlConnection con = dc.getConnect();
            try
            {
                SqlCommand cmd = new SqlCommand("Hieu_ThemGiangVien", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@MaGV", SqlDbType.VarChar).Value = gv.MaGV;
                cmd.Parameters.Add("@TenGV", SqlDbType.NVarChar).Value = gv.TenGV;
                cmd.Parameters.Add("@EmailGV", SqlDbType.VarChar).Value = gv.Email;
                cmd.Parameters.Add("@SdtGV", SqlDbType.Char).Value = gv.SDTGV;
                cmd.Parameters.Add("@DcGV", SqlDbType.NText).Value = gv.DiaChi;
                cmd.Parameters.Add("@NsGV", SqlDbType.Date).Value = gv.NgaySinh;
                cmd.Parameters.Add("@GtGV", SqlDbType.Bit).Value = gv.GioiTinh;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu cần: Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }


        public bool ThemPM(tblPM pm)
        {
            SqlConnection con = dc.getConnect();
            try
            {
                SqlCommand cmd = new SqlCommand("Hieu_ThemPM", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@MaPM", SqlDbType.VarChar).Value = pm.MaPhong;
                cmd.Parameters.Add("@TenPM", SqlDbType.NVarChar).Value = pm.TenPhong;
                cmd.Parameters.Add("@SoLM", SqlDbType.Int).Value = pm.SoLuongMay;
                cmd.Parameters.Add("@DiaDiem", SqlDbType.NText).Value = pm.DiaDiem;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                // Ghi log hoặc hiển thị lỗi nếu cần
                //MessageBox.Show("Lỗi SQL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
             
                return false;
            }
            return true;
        }



        public bool SuaGV(tblGV gv)
        {
            SqlConnection con = dc.getConnect();
            try
            {
                cmd = new SqlCommand("Hieu_SuaGiangVien", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@MaGV", SqlDbType.VarChar).Value = gv.MaGV;
                cmd.Parameters.Add("@TenGV", SqlDbType.NVarChar).Value = gv.TenGV;
                cmd.Parameters.Add("@EmailGV", SqlDbType.VarChar).Value = gv.Email;
                cmd.Parameters.Add("@SdtGV", SqlDbType.Char).Value = gv.SDTGV;
                cmd.Parameters.Add("@DcGV", SqlDbType.NText).Value = gv.DiaChi;
                cmd.Parameters.Add("@NsGV", SqlDbType.Date).Value = gv.NgaySinh;
                cmd.Parameters.Add("@GtGV", SqlDbType.Bit).Value = gv.GioiTinh;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool SuaPM(tblPM pm)
        {
            SqlConnection con = dc.getConnect();
            try
            {
                SqlCommand cmd = new SqlCommand("Hieu_SuaPM", con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Thêm các tham số đúng tên giống thủ tục trong SQL Server
                cmd.Parameters.Add("@MaPM", SqlDbType.VarChar).Value = pm.MaPhong;
                cmd.Parameters.Add("@TenPM", SqlDbType.NVarChar).Value = pm.TenPhong;
                cmd.Parameters.Add("@SoLM", SqlDbType.Int).Value = pm.SoLuongMay;
                cmd.Parameters.Add("@DiaDiem", SqlDbType.NText).Value = pm.DiaDiem;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi khi sửa phòng máy: " + e.Message);
                return false;
            }
            return true;
        }

        public bool XoaGV(tblGV gv)
        {
            SqlConnection con = dc.getConnect();
            try
            {
                SqlCommand cmd = new SqlCommand("Hieu_XoaGV", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@MaGV", SqlDbType.VarChar).Value = gv.MaGV;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool XoaPM(tblPM pm)
        {
            SqlConnection con = dc.getConnect();
            try
            {
                SqlCommand cmd = new SqlCommand("Hieu_XoaPM", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@MaPM", SqlDbType.VarChar).Value = pm.MaPhong;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }


        public DataTable FindGV(string keyword)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = dc.getConnect())
            {
                using (SqlCommand cmd = new SqlCommand("Hieu_TimKiemGiangVien_TongQuat", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TuKhoa", keyword);

                    con.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public DataTable FindPM(string keyword)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = dc.getConnect())
            {
                using (SqlCommand cmd = new SqlCommand("Hieu_TimKiemPhongMay_TongQuat", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TuKhoa", keyword);
                   
                    con.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

    }
}
