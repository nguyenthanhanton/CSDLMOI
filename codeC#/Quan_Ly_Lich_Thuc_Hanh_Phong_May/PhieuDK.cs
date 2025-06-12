using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_Ly_Lich_Thuc_Hanh_Phong_May
{
    public partial class PhieuDK : Form
    {
        private Connect1412 db = new Connect1412();
        public PhieuDK()
        {
            InitializeComponent();
        }
        private void LoadDataToComboBoxes()
        {
            db.Connect();

            // Mã giảng viên
            SqlDataReader readerGV = db.ExecuteReader("SELECT TenGV FROM GiangVien where MaGV != '----'");
            if (readerGV != null)
            {
                while (readerGV.Read())
                {
                    phieu_cbMaGV.Items.Add(readerGV.GetString(0));
                }
                readerGV.Close();
            }

            // Mã lớp học
            SqlDataReader readerLop = db.ExecuteReader("SELECT TenLop FROM LopHoc where MaLop != '----'");
            if (readerLop != null)
            {
                while (readerLop.Read())
                {
                    phieu_cbMaLop.Items.Add(readerLop.GetString(0));
                }
                readerLop.Close();
            }

            // Tên môn học
            SqlDataReader readerMH = db.ExecuteReader("SELECT TenMH FROM MonHoc where MaMH != '----'");
            if (readerMH != null)
            {
                while (readerMH.Read())
                {
                    phieu_cbTenMon.Items.Add(readerMH.GetString(0));
                }
                readerMH.Close();
            }

            db.Disconnect();
        }
        private void ResetForm()
        {
            phieu_txtSoBuoi.Text = "";
            phieu_dtpLTH.Value = DateTime.Today;
            phieu_dtpLTH.Enabled = false;
            phieu_cbMaGV.Text = "Chọn tên giảng viên";
            phieu_cbMaLop.Text = "Chọn tên lớp học";
            phieu_cbTenMon.Text = "Chọn tên môn học";
            phieu_txtNam.Text = "xxxx-xxxx";
            rdbHK1.Checked = false;
            rdbHK2.Checked = false;
        }
        private void PhieuDK_Load(object sender, EventArgs e)
        {
            LoadDataToComboBoxes();
            ResetForm();
        }
        private void DangKyLichThucHanh(int soBuoi, DateTime ngay, string tenLop, string tenMon, 
            int hk, string namHoc, string tenGV)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("bich_dangKyLTH", db.Connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@soBuoi", soBuoi);
                cmd.Parameters.AddWithValue("@ngay", ngay);
                cmd.Parameters.AddWithValue("@tenLop", tenLop);
                cmd.Parameters.AddWithValue("@tenMon", tenMon);
                cmd.Parameters.AddWithValue("@hk", hk);
                cmd.Parameters.AddWithValue("@nam", namHoc);
                cmd.Parameters.AddWithValue("@tenGV", tenGV);
                // Thêm tham số output
                SqlParameter outputParam = new SqlParameter("@output", SqlDbType.NVarChar, 100);
                outputParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outputParam);

                cmd.ExecuteNonQuery();

                // Lấy giá trị từ tham số output
                string resultMessage = outputParam.Value.ToString();

                MessageBox.Show(resultMessage, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void phieu_btn_dky_Click(object sender, EventArgs e)
        {
            db.Connect();

            string maGV = phieu_cbMaGV.SelectedItem?.ToString() ?? "";
            string maLop = phieu_cbMaLop.SelectedItem?.ToString() ?? "";
            string tenMon = phieu_cbTenMon.SelectedItem?.ToString() ?? "";
            string namHoc = phieu_txtNam.Text.Trim();
            int soBuoi = 0;
            int hk = 0;
            DateTime ngay = phieu_dtpLTH.Value; // value là property, không phải method

            // Danh sách lỗi
            List<string> danhSachLoi = new List<string>();

            // Kiểm tra các ComboBox
            if (string.IsNullOrEmpty(maGV))
                danhSachLoi.Add("Chưa chọn tên GV.");
            if (string.IsNullOrEmpty(maLop))
                danhSachLoi.Add("Chưa chọn tên lớp.");
            if (string.IsNullOrEmpty(tenMon))
                danhSachLoi.Add("Chưa chọn tên môn.");

            // Kiểm tra năm học
            if (!Regex.IsMatch(namHoc, @"^\d{4}-\d{4}$"))
                danhSachLoi.Add("Năm học phải theo định dạng xxxx-xxxx (ví dụ: 2024-2025).");
            else
            {
                var parts = namHoc.Split('-');
                int namTruoc = int.Parse(parts[0]);
                int namSau = int.Parse(parts[1]);
                if (namSau - namTruoc != 1)
                    danhSachLoi.Add("Năm học không hợp lệ: năm sau phải hơn năm trước đúng 1 năm.");
            }
            // Kiểm tra số buổi
            if (!int.TryParse(phieu_txtSoBuoi.Text.Trim(), out soBuoi) || soBuoi <= 0)
                danhSachLoi.Add("Số buổi phải là số nguyên dương.");
            // Kiểm tra học kỳ
            if (rdbHK1.Checked)
                hk = 1;
            else if (rdbHK2.Checked)
                hk = 2;
            else
                danhSachLoi.Add("Chưa chọn học kỳ.");
            // Nếu có lỗi
            if (danhSachLoi.Count > 0)
            {
                string thongBao = "Vui lòng sửa các lỗi sau:\n- " + string.Join("\n- ", danhSachLoi);
                MessageBox.Show(thongBao, "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            } else
            {
                DangKyLichThucHanh(soBuoi, ngay, maLop, tenMon, hk, namHoc, maGV);
            }     
            if (this.Parent != null)
            {
                // Tìm form cha của TabPage (lần ngược lên)
                Control parent = this.Parent;
                while (parent != null && !(parent is Form))
                {
                    parent = parent.Parent;
                }
                if (parent is FormBich formBich)
                {
                    formBich.LoadDanhSachLTH();
                }
            }
        }

        private void phieu_btn_huy_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có muốn tiếp tục đăng ký không?", // Nội dung
                "Xác nhận",                             // Tiêu đề
                MessageBoxButtons.YesNo,                // 2 nút Yes - No
                MessageBoxIcon.Question                 // Icon dấu hỏi
            );

            if (result == DialogResult.Yes)
            {
                // Tiếp tục đăng ký: Làm mới form
                ResetForm();
            }
            else if (result == DialogResult.No)
            {
                if (this.Parent != null)
                {
                    this.Parent.Controls.Remove(this);

                    Control parent = this.Parent;
                    while (parent != null && !(parent is Form))
                    {
                        parent = parent.Parent;
                    }

                    if (parent is FormBich formBich)
                    {
                        formBich.LoadDanhSachLTH();
                    }
                }
            }
        }
    }
}
