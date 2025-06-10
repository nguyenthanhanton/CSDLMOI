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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Quan_Ly_Lich_Thuc_Hanh_Phong_May
{
    public partial class FormUpdateLTH : Form
    {
        private Connect1412 db = new Connect1412();
        public FormUpdateLTH()
        {
            InitializeComponent();
        }
        private void ResetFormBich()
        {
            if (this.Parent != null)
            {
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
        public void SetThongTin(string maLich, string maGV, string soBuoi, string maLop, string tenMon, string ngayDK, string hocKy, string namHoc)
        {
            upd_txtMaLich.Text = maLich;
            upd_txtMaGV.Text = maGV;
            upd_txtSoBuoi.Text = soBuoi;
            upd_txtMaLop.Text = maLop;
            upd_txtTenMH.Text = tenMon;
            upd_txtNgay.Text = ngayDK;
            upd_txtNam.Text = namHoc;
            if (hocKy == "1")
                rdbHK1.Checked = true;
            else
                rdbHK2.Checked = true;
            upd_txtMaLich.Enabled = false;
            upd_txtMaGV.Enabled= false;
            upd_txtMaLop.Enabled = false;
            upd_txtTenMH.Enabled = false;
            upd_txtNgay.Enabled = false;
        }
        public void LoadThongTin(string maLich, string maGV, string soBuoi, string maLop, string tenMon, string ngayDK, string hocKy, string namHoc)
        {
            phieu_btn_update.Visible = false;
            phieu_btn_cancel.Visible = false;

            upd_txtMaLich.Text = maLich;
            upd_txtMaGV.Text = maGV;
            upd_txtSoBuoi.Text = soBuoi;
            upd_txtMaLop.Text = maLop;
            upd_txtTenMH.Text = tenMon;
            upd_txtNgay.Text = ngayDK;
            upd_txtNam.Text = namHoc;
            if (hocKy == "1")
                rdbHK1.Checked = true;
            else
                rdbHK2.Checked = true;
            this.Enabled = false;
        }
        

        private void FormUpdateLTH_Load(object sender, EventArgs e)
        {
        }
        private void CapNhatLichThucHanh(string maLich, int soBuoi, int hk, string namHoc)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("bich_suaLTH", db.Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@maLich", maLich);
                cmd.Parameters.AddWithValue("@soBuoi", soBuoi);
                cmd.Parameters.AddWithValue("@hk", hk);
                cmd.Parameters.AddWithValue("@nam", namHoc);
                // Thêm tham số output
                SqlParameter outputParam = new SqlParameter("@output", SqlDbType.NVarChar, 100);
                outputParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outputParam);

                cmd.ExecuteNonQuery();

                this.Close();   
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void phieu_btn_update_Click(object sender, EventArgs e)
        {
            db.Connect();
            string maLich = upd_txtMaLich.Text.Trim();
            string namHoc = upd_txtNam.Text.Trim();
            int soBuoi = 0;
            int hk = 0;
            // Danh sách lỗi
            List<string> danhSachLoi = new List<string>();

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
            if (!int.TryParse(upd_txtSoBuoi.Text.Trim(), out soBuoi) || soBuoi <= 0)
                danhSachLoi.Add("Số buổi phải là số nguyên dương.");
            // Kiểm tra học kỳ
            if (rdbHK1.Checked)
                hk = 1;
            else if (rdbHK2.Checked)
                hk = 2;
            else
                danhSachLoi.Add("Chưa chọn học kỳ.");
            if (danhSachLoi.Count > 0)
            {
                string thongBao = "Vui lòng sửa các lỗi sau:\n- " + string.Join("\n- ", danhSachLoi);
                MessageBox.Show(thongBao, "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            } else
            {
                CapNhatLichThucHanh(maLich, soBuoi, hk, namHoc);
                ResetFormBich();
            }    
        }

        private void phieu_btn_cancel_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có muốn tiếp tục thực hiện không?", // Nội dung
                "Xác nhận",                             // Tiêu đề
                MessageBoxButtons.YesNo,                // 2 nút Yes - No
                MessageBoxIcon.Question                 // Icon dấu hỏi
            );

            if (result == DialogResult.No)
            {
                this.Close();
            }
        }
    }
}
