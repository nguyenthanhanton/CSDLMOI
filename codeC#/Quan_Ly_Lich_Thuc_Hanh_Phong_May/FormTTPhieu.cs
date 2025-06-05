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
    public partial class FormTTPhieu : Form
    {
        private Connect1412 db = new Connect1412();
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
        public void LoadThongTin(
            string maLich, string maNV, string maPhong, string soPhieu, string noiDungTH, string ngayTH,
            string gioBD, string gioKT, string loaiGT, string hstt, string soGio)
        {
            btnCapNhat.Visible = false;
            btnCancel.Visible = false;
            txtMaLich.Text = maLich;
            txtMaNV.Text = maNV;
            txtMaPhong.Text = maPhong;
            txtSoPhieu.Text = soPhieu;
            txtNoiDung.Text = noiDungTH;
            txtHSTT.Text = hstt;
            txtSoGio.Text = soGio;
            if (DateTime.TryParse(ngayTH, out DateTime dtNgayTH))
            {
                dtpNgayTH.Value = dtNgayTH;
            }

            if (DateTime.TryParse(gioBD, out DateTime dtGioBD))
            {
                dtpBD.Value = dtGioBD;
            }

            if (DateTime.TryParse(gioKT, out DateTime dtGioKT))
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
            this.Enabled = false;
        }
        public void SetThongTin(
            string maLich, string maNV, string maPhong, string soPhieu, string noiDungTH, string ngayTH,
            string gioBD, string gioKT, string loaiGT, string hstt, string soGio)
        {
            btnCapNhat.Visible = true;
            btnCancel.Visible = true;
            txtMaLich.Text = maLich;
            txtMaNV.Text = maNV;
            txtMaPhong.Text = maPhong;
            txtSoPhieu.Text = soPhieu;
            txtNoiDung.Text = noiDungTH;
            txtHSTT.Text = hstt;
            txtSoGio.Text = soGio;
            if (DateTime.TryParse(ngayTH, out DateTime dtNgayTH))
            {
                dtpNgayTH.Value = dtNgayTH;
            }

            if (DateTime.TryParse(gioBD, out DateTime dtGioBD))
            {
                dtpBD.Value = dtGioBD;
            }

            if (DateTime.TryParse(gioKT, out DateTime dtGioKT))
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
        }
        private void FormTTPhieu_Load(object sender, EventArgs e)
        {

        }
        private void CapNhatPhieuTaiTruc(string maLich, string maNV, string maPhong, string noiDungTH, TimeSpan gioBD, TimeSpan gioKT, string loaiGT, DateTime ngayTH)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("bich_suaPTT", db.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@maLich", maLich);
                    cmd.Parameters.AddWithValue("@maNV", maNV);
                    cmd.Parameters.AddWithValue("@maPhong", maPhong);
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
            string maLich = txtMaLich.Text ?? "";
            string maPhong = txtMaPhong.Text ?? "";
            string maNV = txtMaNV.Text ?? "";

            string nd = txtNoiDung.Text.ToString() ?? "";
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

            //if(ngay < DateTime.Today)
            //{
            //    danhSachLoi.Add("Không được cập nhật những phiếu trực đã thực hành rồi");
            //}
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
            CapNhatPhieuTaiTruc(maLich, maNV, maPhong, nd, gioBatDau, gioKetThuc, loai, ngay);
            ResetFormBich();          
        }

        private void btnCancel_Click(object sender, EventArgs e)
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
