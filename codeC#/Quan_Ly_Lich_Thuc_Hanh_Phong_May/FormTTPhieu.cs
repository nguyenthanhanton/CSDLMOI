using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;

namespace Quan_Ly_Lich_Thuc_Hanh_Phong_May
{
    public partial class FormTTPhieu : Form
    {
        private Connect1412 db = new Connect1412();

        // Khai báo biến toàn cục để lưu trữ thông tin
        private string maLich;
        private string maNV;
        private string maPhong;
        private int soPhieu;
        private string noiDungTH;
        private string ngayTH;
        private string gioBD;
        private string gioKT;
        private string loaiGT;
        private string hstt;
        private string soGio;

        public FormTTPhieu()
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
                    formBich.LoadDanhSachPTT();
                }
            }
        }

        // Hàm nhận dữ liệu từ form ngoài và gán vào biến toàn cục
        public void SetDataFromOtherForm(
            string maLich, string maNV, string maPhong, string soPhieu, string noiDungTH, string ngayTH,
            string gioBD, string gioKT, string loaiGT, string hstt, string soGio)
        {
            this.maLich = maLich;
            this.maNV = maNV;
            this.maPhong = maPhong;
            this.soPhieu = int.TryParse(soPhieu, out int sPhieu) ? sPhieu : 0;
            this.noiDungTH = noiDungTH;
            this.ngayTH = ngayTH;
            this.gioBD = gioBD;
            this.gioKT = gioKT;
            this.loaiGT = loaiGT;
            this.hstt = hstt;
            this.soGio = soGio;
        }

        public void LoadThongTin()
        {
            btnCapNhat.Visible = false;
            btnCancel.Visible = false;

            txtMaLich.Text = maLich;
            txtMaNV.Text = maNV;
            txtMaPhong.Text = maPhong;
            txtSoPhieu.Text = soPhieu.ToString();
            txtNoiDung.Text = noiDungTH;
            txtHSTT.Text = hstt;
            txtSoGio.Text = soGio;

            if (DateTime.TryParseExact(ngayTH, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dtNgayTH) ||
                DateTime.TryParse(ngayTH, out dtNgayTH))
            {
                dtpNgayTH.Value = dtNgayTH;
            }

            if (TimeSpan.TryParse(gioBD, out TimeSpan tsGioBD))
            {
                dtpBD.Value = DateTime.Today.Add(tsGioBD);
            }
            else if (DateTime.TryParse(gioBD, out DateTime dtGioBD))
            {
                dtpBD.Value = dtGioBD;
            }

            if (TimeSpan.TryParse(gioKT, out TimeSpan tsGioKT))
            {
                dtpKT.Value = DateTime.Today.Add(tsGioKT);
            }
            else if (DateTime.TryParse(gioKT, out DateTime dtGioKT))
            {
                dtpKT.Value = dtGioKT;
            }

            switch (loaiGT)
            {
                case "Hành chính":
                    rdbHC.Checked = true;
                    rdbGN.Checked = false;
                    break;
                case "Giờ nghỉ":
                    rdbGN.Checked = true;
                    rdbHC.Checked = false;
                    break;
                default:
                    rdbHC.Checked = false;
                    rdbGN.Checked = false;
                    break;
            }

            this.Enabled = false;
        }

        public bool SetThongTin()
        {
            btnCapNhat.Visible = true;
            btnCancel.Visible = true;

            txtMaLich.Text = maLich;
            txtMaNV.Text = maNV;
            txtMaPhong.Text = maPhong;
            txtSoPhieu.Text = soPhieu.ToString();
            txtNoiDung.Text = noiDungTH;
            txtHSTT.Text = hstt;
            txtSoGio.Text = soGio;

            if(DateTime.TryParseExact(ngayTH, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dtNgayTH) ||
                DateTime.TryParse(ngayTH, out dtNgayTH))
            {
                dtpNgayTH.Value = dtNgayTH;
                if(dtNgayTH <= DateTime.Today)
                {
                    MessageBox.Show("Không được cập nhật lịch trực đã thực hiện hoạc thực hiện trong ngày hôm nay.");
                    return false;
                }
            }
            

            if (TimeSpan.TryParse(gioBD, out TimeSpan tsGioBD))
            {
                dtpBD.Value = DateTime.Today.Add(tsGioBD);
            }
            else if (DateTime.TryParse(gioBD, out DateTime dtGioBD))
            {
                dtpBD.Value = dtGioBD;
            }

            if (TimeSpan.TryParse(gioKT, out TimeSpan tsGioKT))
            {
                dtpKT.Value = DateTime.Today.Add(tsGioKT);
            }
            else if (DateTime.TryParse(gioKT, out DateTime dtGioKT))
            {
                dtpKT.Value = dtGioKT;
            }

            switch (loaiGT)
            {
                case "Hành chính":
                    rdbHC.Checked = true;
                    break;
                case "Giờ nghỉ":
                    rdbGN.Checked = true;
                    break;
                default:
                    rdbHC.Checked = false;
                    rdbGN.Checked = false;
                    break;
            }

            txtSoPhieu.Enabled = false;
            txtMaLich.Enabled = false;
            txtMaNV.Enabled = false;
            txtMaPhong.Enabled = false;
            txtHSTT.Enabled = false;
            txtSoGio.Enabled = false;
            return true;
        }

        private void FormTTPhieu_Load(object sender, EventArgs e)
        {

        }

        private void CapNhatPhieuTaiTruc(int soPhieu, string noiDungTH, TimeSpan gioBD, TimeSpan gioKT, string loaiGT, DateTime ngayTH)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("bich_suaPTT", db.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@stt", soPhieu);
                    cmd.Parameters.AddWithValue("@nd", noiDungTH);
                    cmd.Parameters.AddWithValue("@ngay", ngayTH.Date);
                    cmd.Parameters.AddWithValue("@gioBD", gioBD);
                    cmd.Parameters.AddWithValue("@gioKT", gioKT);
                    cmd.Parameters.AddWithValue("@loaiGT", loaiGT);

                    SqlParameter outputParam = new SqlParameter("@output", SqlDbType.NVarChar, 100)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputParam);

                    cmd.ExecuteNonQuery();

                    string resultMessage = outputParam.Value.ToString();
                    MessageBox.Show(resultMessage, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            db.Connect();
            string nd = txtNoiDung.Text ?? "";
            DateTime ngay = dtpNgayTH.Value.Date;
            TimeSpan gioBatDau = dtpBD.Value.TimeOfDay;
            TimeSpan gioKetThuc = dtpKT.Value.TimeOfDay;
            string loai = "";

            if (rdbHC.Checked)
            {
                loai = "Hành chính";
            }
            if (rdbGN.Checked)
            {
                loai = "Giờ nghỉ";
            }

            // Danh sách lỗi
            List<string> danhSachLoi = new List<string>();

            if (ngay <= DateTime.Today)
            {
                danhSachLoi.Add("Ngày thực hành mới phải sau ngày hôm nay.");
            }
            if (gioBatDau >= gioKetThuc)
            {
                danhSachLoi.Add("Giờ bắt đầu phải nhỏ hơn giờ kết thúc.");
            }
            if ((rdbHC.Checked && rdbGN.Checked) || ((!rdbHC.Checked) && (!rdbGN.Checked)))
            {
                danhSachLoi.Add("Chọn 1 trong 2 loại giờ trực..");
            }
            if (danhSachLoi.Count > 0)
            {
                string thongBao = "Vui lòng sửa các lỗi sau:\n- " + string.Join("\n- ", danhSachLoi);
                MessageBox.Show(thongBao, "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                CapNhatPhieuTaiTruc(soPhieu, nd, gioBatDau, gioKetThuc, loai, ngay);
                ResetFormBich();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có muốn tiếp tục thực hiện không?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                this.Close();
            }
        }
    }
}
