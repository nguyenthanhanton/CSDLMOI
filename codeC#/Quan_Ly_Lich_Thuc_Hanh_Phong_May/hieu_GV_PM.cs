using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Quan_Ly_Lich_Thuc_Hanh_Phong_May
{
    public partial class form_hieu_GVPM: Form
    {
        GVBLL bllGV;
        PMBLL bllPM;
        private bool timKiemHienThi = false;
        private bool isGiangVienVisible = false;
        private bool isPhongMayVisible = false;
        public form_hieu_GVPM()
        {
            InitializeComponent();
            bllGV = new GVBLL();
            bllPM = new PMBLL();
            panel_GV.Visible = false;
            lbl_TK.Visible = false;
            txt_TK.Visible = false;
            txt_TKPM.Visible = false;

        }
        public void showAllGV()
        {
            DataTable dt = bllGV.GetAllGV();
            dgv_GV.DataSource = dt;
            dt.Columns.Add("Giới Tính", typeof(string));
            foreach (DataRow row in dt.Rows)
            {
                bool gt = Convert.ToBoolean(row["GioiTinh"]);
                row["Giới Tính"] = gt ? "Nam" : "Nữ";
            }

            dgv_GV.DataSource = dt;

            // Ẩn cột bit gốc nếu muốn
            if (dgv_GV.Columns.Contains("GioiTinh"))
            {
                dgv_GV.Columns["GioiTinh"].Visible = false;
            }
        }
        public void showAllPM()
        {
            DataTable dt = bllPM.GetAllPM();
            dgv_PM.DataSource = dt;
        }

        public bool CheckData()
        {
            if (string.IsNullOrEmpty(txt_MaGV.Text))
            {
                MessageBox.Show("Bạn chưa nhập mã giảng viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_MaGV.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txt_TenGV.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên giảng viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_TenGV.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txt_EMAILGV.Text))
            {
                MessageBox.Show("Bạn chưa nhập email", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_EMAILGV.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txt_DCGV.Text))
            {
                MessageBox.Show("Bạn chưa nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_DCGV.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txt_SDTGV.Text))
            {
                MessageBox.Show("Bạn chưa nhập số điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_SDTGV.Focus();
                return false;
            }
            if (dt_NSGV.Value == null || dt_NSGV.Value.Date == DateTime.Now.Date)
            {
                MessageBox.Show("Bạn chưa chọn ngày sinh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dt_NSGV.Focus();
                return false;
            }

            // Kiểm tra giới tính (radio button)
            if (rb_GVNam.Checked && rb_GVNu.Checked)
            {
                MessageBox.Show("Bạn chưa chọn giới tính", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }
        public bool CheckData_PM()
        {
            if (string.IsNullOrEmpty(txt_MaPM.Text))
            {
                MessageBox.Show("Bạn chưa nhập mã phòng máy", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_MaPM.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txt_TenPM.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên phòng máy", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_TenPM.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txt_SoLM.Text))
            {
                MessageBox.Show("Bạn chưa nhập số lượng máy", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_SoLM.Focus();
                return false;
            }
            else
            {
                // Kiểm tra số lượng máy có phải là số nguyên dương không
                if (!int.TryParse(txt_SoLM.Text, out int soLuong) || soLuong <= 0)
                {
                    MessageBox.Show("Số lượng máy phải là số nguyên dương", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_SDTGV.Focus();
                    return false;
                }
            }
            if (string.IsNullOrEmpty(txt_DiaDiem.Text))
            {
                MessageBox.Show("Bạn chưa nhập địa điểm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_DiaDiem.Focus();
                return false;
            }

            return true;
        }
        private void btn_GV_Click(object sender, EventArgs e)
        {
            isGiangVienVisible = !isGiangVienVisible; // Đảo trạng thái
            isPhongMayVisible = false; // Ẩn phần phòng máy nếu đang hiện

            if (isGiangVienVisible)
            {
                // Hiện các thành phần giảng viên
                panel_GV.Visible = true;

                gb_MaGV.Text = "Mã giảng viên";
                gb_SDTGV.Text = "Số điện thoại";
                gb_TenGV.Text = "Tên giảng viên";
                gb_EmailGV.Text = "Email";



                gb_DCGV.Visible = true;
                txt_DCGV.Visible = true;

                gb_GTGV.Visible = true;
                rb_GVNam.Visible = true;
                rb_GVNu.Visible = true;

                gb_NSGV.Visible = true;
                dt_NSGV.Visible = true;

                dgv_GV.Visible = true;
                dgv_PM.Visible = false;

                btn_THEM_GVPM.Visible = true;
                btn_SUA_GVPM.Visible = true;
                btn_XOA_GVPM.Visible = true;
                btn_TK_GVPM.Visible = true;

                btn_ThemPM.Visible = false;
                btn_SuaPM.Visible = false;
                btn_XoaPM.Visible = false;
                btn_TKPM.Visible = false;

                txt_MaGV.Visible = true;
                txt_TenGV.Visible = true;
                txt_SDTGV.Visible = true;
                txt_EMAILGV.Visible = true;

                txt_MaPM.Visible = false;
                txt_TenPM.Visible = false;
                txt_SoLM.Visible = false;
                txt_DiaDiem.Visible = false;

                lbl_TK.Visible = false;
                txt_TK.Visible = false;
                txt_TKPM.Visible = false;
            }
            else
            {
                // Ẩn toàn bộ
                panel_GV.Visible = false;
                dgv_GV.Visible = false;
            }  
        }

        private void btn_PM_Click(object sender, EventArgs e)
        {
            isPhongMayVisible = !isPhongMayVisible;
            isGiangVienVisible = false;

            if (isPhongMayVisible)
            {
                panel_GV.Visible = true;

                gb_MaGV.Text = "Mã phòng máy";
                gb_SDTGV.Text = "Số lượng máy";
                gb_TenGV.Text = "Tên phòng máy";
                gb_EmailGV.Text = "Địa điểm";



                gb_DCGV.Visible = false;
                txt_DCGV.Visible = false;

                gb_GTGV.Visible = false;
                rb_GVNam.Visible = false;
                rb_GVNu.Visible = false;

                gb_NSGV.Visible = false;
                dt_NSGV.Visible = false;

                dgv_GV.Visible = false;
                dgv_PM.Visible = true;

                btn_THEM_GVPM.Visible = false;
                btn_SUA_GVPM.Visible = false;
                btn_XOA_GVPM.Visible = false;
                btn_TK_GVPM.Visible = false;

                btn_ThemPM.Visible = true;
                btn_SuaPM.Visible = true;
                btn_XoaPM.Visible = true;
                btn_TKPM.Visible = true;

                txt_MaGV.Visible = false;
                txt_TenGV.Visible = false;
                txt_SDTGV.Visible = false;
                txt_EMAILGV.Visible = false;

                txt_MaPM.Visible = true;
                txt_TenPM.Visible = true;
                txt_SoLM.Visible = true;
                txt_DiaDiem.Visible = true;

                lbl_TK.Visible = false;
                txt_TK.Visible = false;
                txt_TKPM.Visible = false;
            }
            else
            {
                panel_GV.Visible = false;
                dgv_PM.Visible = false;
            }
        }

        private void form_hieu_GVPM_Load(object sender, EventArgs e)
        {

            showAllGV();
            showAllPM();
        }

        private void btn_THEM_GVPM_Click(object sender, EventArgs e)
        {
            if (!CheckData())
                return;

            if (MessageBox.Show("Bạn có muốn thêm giảng viên hay không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                tblGV gv = new tblGV();
                gv.MaGV = txt_MaGV.Text;
                gv.TenGV = txt_TenGV.Text;
                gv.Email = txt_EMAILGV.Text;
                gv.SDTGV = txt_SDTGV.Text;
                gv.DiaChi = txt_DCGV.Text;

                // Ngày sinh
                DateTime ngaySinh;
                if (DateTime.TryParse(dt_NSGV.Text, out ngaySinh))
                {
                    gv.NgaySinh = ngaySinh;
                }
                else
                {
                    MessageBox.Show("Ngày sinh không hợp lệ. Vui lòng chọn đúng định dạng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Giới tính (True: Nam, False: Nữ)
                if (rb_GVNam.Checked)
                    gv.GioiTinh = true;
                else if (rb_GVNu.Checked)
                    gv.GioiTinh = false;
                else
                {
                    MessageBox.Show("Bạn chưa chọn giới tính", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Gọi BLL để thêm giảng viên
                if (bllGV.ThemGV(gv))
                {
                    MessageBox.Show("Thêm giảng viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    showAllGV();
                    // ClearTextBoxes(); // Nếu có
                }
                else
                {
                    MessageBox.Show("Mã giảng viên đã tồn tại !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void dgv_GV_PM_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void dgv_GV_PM_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgv_GV.Rows.Count)
            {
                var row = dgv_GV.Rows[e.RowIndex];

                txt_MaGV.Text = row.Cells["MaGV"].Value != DBNull.Value ? row.Cells["MaGV"].Value.ToString() : "";
                txt_TenGV.Text = row.Cells["TenGV"].Value != DBNull.Value ? row.Cells["TenGV"].Value.ToString() : "";
                txt_EMAILGV.Text = row.Cells["EmailGV"].Value != DBNull.Value ? row.Cells["EmailGV"].Value.ToString() : "";
                txt_SDTGV.Text = row.Cells["SdtGV"].Value != DBNull.Value ? row.Cells["SdtGV"].Value.ToString() : "";
                txt_DCGV.Text = row.Cells["DcGV"].Value != DBNull.Value ? row.Cells["DcGV"].Value.ToString() : "";

                if (row.Cells["NsGV"].Value != DBNull.Value && row.Cells["NsGV"].Value != null)
                {
                    if (DateTime.TryParse(row.Cells["NsGV"].Value.ToString(), out DateTime ns))
                    {
                        dt_NSGV.Value = ns; // Nếu dùng DateTimePicker
                    }
                    else
                    {
                        dt_NSGV.Value = DateTime.Now;
                    }
                }
                else
                {
                    dt_NSGV.Value = DateTime.Now;
                }

                if (row.Cells["GtGV"].Value != DBNull.Value && row.Cells["GtGV"].Value != null)
                {
                    bool gioiTinh = Convert.ToBoolean(row.Cells["GtGV"].Value);
                    rb_GVNam.Checked = gioiTinh;
                    rb_GVNu.Checked = !gioiTinh;
                }
                else
                {
                    rb_GVNam.Checked = false;
                    rb_GVNu.Checked = false;
                }
            }
        }

        private void btn_SUA_GVPM_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn sửa thông tin giảng viên không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
               tblGV gv = new tblGV();
                gv.MaGV = txt_MaGV.Text.Trim();
                gv.TenGV = txt_TenGV.Text.Trim();
                gv.Email = txt_EMAILGV.Text.Trim();
                gv.SDTGV = txt_SDTGV.Text.Trim();
                gv.DiaChi = txt_DCGV.Text.Trim();

                // Ngày sinh
                if (DateTime.TryParse(dt_NSGV.Text, out DateTime ngaySinh))
                {
                    gv.NgaySinh = ngaySinh;
                }
                else
                {
                    MessageBox.Show("Ngày sinh không hợp lệ. Vui lòng chọn lại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Giới tính (Nam = true, Nữ = false)
                if (rb_GVNam.Checked)
                    gv.GioiTinh = true;
                else if (rb_GVNu.Checked)
                    gv.GioiTinh = false;
                else
                {
                    MessageBox.Show("Vui lòng chọn giới tính", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Gọi BLL để update
                if (bllGV.SuaGV(gv))
                {
                    MessageBox.Show("Cập nhật thông tin giảng viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    showAllGV();
                    //ClearGiangVienFields();
                }
                else
                {
                    MessageBox.Show("Đã xảy ra lỗi khi cập nhật giảng viên !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void btn_XOA_GVPM_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xoá giảng viên này không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                tblGV gv = new tblGV();
                gv.MaGV = txt_MaGV.Text.Trim(); // chỉ cần Mã GV là đủ để xóa

                if (bllGV.XoaGV(gv))
                {
                    MessageBox.Show("Xoá giảng viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    showAllGV(); // cập nhật lại datagridview
                    //ClearGiangVienFields(); // xóa thông tin trong textbox (nếu có)
                }
                else
                {
                    MessageBox.Show("Đã có lỗi xảy ra, vui lòng thử lại sau", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_TK_GVPM_Click(object sender, EventArgs e)
        {
            timKiemHienThi = !timKiemHienThi; // Đảo trạng thái

            lbl_TK.Visible = timKiemHienThi;
            txt_TK.Visible = timKiemHienThi;

            // Nếu bạn muốn xóa nội dung cũ khi ẩn:
            if (!timKiemHienThi)
                txt_TK.Text = "";
        }

        private void txt_TK_TextChanged(object sender, EventArgs e)
        {
            string value = txt_TK.Text;
            if (!string.IsNullOrEmpty(value))
            {
                DataTable dt = bllGV.FindGV(value); // Giờ đã có đúng hàm overload
                dt.Columns.Add("Giới Tính", typeof(string));
                foreach (DataRow row in dt.Rows)
                {
                    bool gt = Convert.ToBoolean(row["GioiTinh"]);
                    row["Giới Tính"] = gt ? "Nam" : "Nữ";
                }

                dgv_GV.DataSource = dt;
            }
            else
            {
                showAllGV();
            }
        }

        private void dgv_GV_PM_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            
        }

        private void dgv_PM_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgv_PM.Rows.Count)
            {
                var row = dgv_PM.Rows[e.RowIndex];

                txt_MaPM.Text = row.Cells["MaPM"].Value != DBNull.Value ? row.Cells["MaPM"].Value.ToString() : "";
                txt_TenPM.Text = row.Cells["TenPM"].Value != DBNull.Value ? row.Cells["TenPM"].Value.ToString() : "";
                txt_SoLM.Text = row.Cells["SoLM"].Value != DBNull.Value ? row.Cells["SoLM"].Value.ToString() : "";
                txt_DiaDiem.Text = row.Cells["DiaDiem"].Value != DBNull.Value ? row.Cells["DiaDiem"].Value.ToString() : "";

                // Ẩn hoặc clear các trường không dùng cho Phòng Máy
                //txt_DCGV.Text = "";
                //dt_NSGV.Value = DateTime.Now;
                //rb_GVNam.Checked = false;
                //rb_GVNu.Checked = false;
            }
        }
    

        private void btn_ThemPM_Click(object sender, EventArgs e)
        {
            if (!CheckData_PM()) // Đảm bảo bạn đã có hàm CheckData_PM()
                return;

            if (MessageBox.Show("Bạn có muốn thêm phòng máy hay không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                tblPM pm = new tblPM();
                pm.MaPhong = txt_MaPM.Text;          // Mã phòng (dùng chung textbox, nên đặt lại tên cho dễ hiểu)
                pm.TenPhong = txt_TenPM.Text;        // Tên phòng
                int soLuong;
                if (!int.TryParse(txt_SoLM.Text, out soLuong))
                {
                    MessageBox.Show("Số lượng máy không hợp lệ. Vui lòng nhập số nguyên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                pm.SoLuongMay = soLuong;
                pm.DiaDiem = txt_DiaDiem.Text;       // Địa điểm

                // Gọi BLL để thêm phòng máy
                if (bllPM.ThemPM(pm))
                {
                    MessageBox.Show("Thêm phòng máy thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    showAllPM(); // Hiển thị lại danh sách phòng máy
                                 // ClearTextBoxes(); // Nếu có hàm reset textbox
                }
                else
                {
                    MessageBox.Show("Mã phòng máy đã tồn tại !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txt_EMAILGV_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_SuaPM_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn sửa thông tin phòng máy không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                tblPM pm = new tblPM();
                pm.MaPhong = txt_MaPM.Text.Trim();
                pm.TenPhong = txt_TenPM.Text.Trim();

                // Kiểm tra và ép kiểu số lượng máy
                if (!int.TryParse(txt_SoLM.Text.Trim(), out int soLuong) || soLuong <= 0)
                {
                    MessageBox.Show("Số lượng máy phải là số nguyên dương!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_SoLM.Focus();
                    return;
                }
                pm.SoLuongMay = soLuong;

                pm.DiaDiem = txt_DiaDiem.Text.Trim();

                // Gọi BLL để update
                if (bllPM.SuaPM(pm))
                {
                    MessageBox.Show("Cập nhật thông tin phòng máy thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    showAllPM(); // Hàm để load lại danh sách phòng máy
                                 // ClearPhongMayFields(); // Nếu có hàm xóa nội dung textbox
                }
                else
                {
                    MessageBox.Show("Đã xảy ra lỗi khi cập nhật phòng máy!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_XoaPM_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xoá phòng máy này không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                tblPM pm = new tblPM();
                pm.MaPhong = txt_MaPM.Text.Trim(); // chỉ cần Mã phòng là đủ để xóa

                if (bllPM.XoaPM(pm))
                {
                    MessageBox.Show("Xoá phòng máy thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    showAllPM(); // cập nhật lại datagridview
                                 //ClearPhongMayFields(); // nếu bạn có hàm để xóa các textbox
                }
                else
                {
                    MessageBox.Show("Đã có lỗi xảy ra, vui lòng thử lại sau", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_TKPM_Click(object sender, EventArgs e)
        {
            timKiemHienThi = !timKiemHienThi; // Đảo trạng thái

            lbl_TK.Visible = timKiemHienThi;
            txt_TKPM.Visible = timKiemHienThi;

            // Nếu bạn muốn xóa nội dung cũ khi ẩn:
            if (!timKiemHienThi)
                txt_TKPM.Text = "";
        }

        private void txt_TKPM_TextChanged(object sender, EventArgs e)
        {
            string value = txt_TKPM.Text;
            if (!string.IsNullOrEmpty(value))
            {
                DataTable dt = bllPM.FindPM(value); // Gọi hàm tìm kiếm phòng máy
                dgv_PM.DataSource = dt;             // Hiển thị kết quả lên DataGridView của phòng máy
            }
            else
            {
                showAllPM(); // Hiển thị toàn bộ phòng máy khi ô tìm kiếm trống
            }
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            //formchungcs frm = new formchungcs();
            //frm.Show();

            // Đóng form hiện tại (GVPM)
            this.Close();
        }

        private void lbl_MaGV_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter_1(object sender, EventArgs e)
        {

        }

        private void dt_NSGV_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
