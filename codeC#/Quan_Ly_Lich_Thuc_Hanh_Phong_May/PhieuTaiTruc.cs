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

namespace Quan_Ly_Lich_Thuc_Hanh_Phong_May
{
    public partial class PhieuTaiTruc : Form
    {
        private Connect1412 db = new Connect1412();
        public PhieuTaiTruc()
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
        private void LoadDataToComboBoxes()
        {
            db.Connect();

            SqlDataReader readerGV = db.ExecuteReader("SELECT MaLich FROM LichTH");
            if (readerGV != null)
            {
                while (readerGV.Read())
                {
                    cbMaLich.Items.Add(readerGV.GetString(0));
                }
                readerGV.Close();
            }
            SqlDataReader readerLop = db.ExecuteReader("SELECT MaNV FROM NhanVien");
            if (readerLop != null)
            {
                while (readerLop.Read())
                {
                    cbMaNV.Items.Add(readerLop.GetString(0));
                }
                readerLop.Close();
            }
            SqlDataReader readerMH = db.ExecuteReader("SELECT MaPhong FROM PhongMay");
            if (readerMH != null)
            {
                while (readerMH.Read())
                {
                    cbMaPM.Items.Add(readerMH.GetString(0));
                }
                readerMH.Close();
            }
            db.Disconnect();
        }
        private void ResetForm()
        {
            cbMaLich.Text = "Chọn mã lịch";
            cbMaNV.Text = "Chọn mã nhân viên";
            cbMaPM.Text = "Chọn mã phòng máy";
            txtNoiDung.Text = "";
            rdbHC.Checked = false;
            rdbGN.Checked = false;

        }
        private void PhieuTaiTruc_Load(object sender, EventArgs e)
        {
            LoadDataToComboBoxes();
            ResetForm();
        }
        private void XepLichTruc(string maLich, string maNV, string maPhong, string nd,
    DateTime ngay, TimeSpan gioBD, TimeSpan gioKT, string loaiGioTruc)
        {
            try
            {
                // 1. Gọi thủ tục bich_xepLichTruc để thêm lịch trực
                using (SqlCommand cmd = new SqlCommand("bich_xepLichTruc", db.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@maLich", maLich);
                    cmd.Parameters.AddWithValue("@maNV", maNV);
                    cmd.Parameters.AddWithValue("@maPM", maPhong);
                    cmd.Parameters.AddWithValue("@nd", nd);
                    cmd.Parameters.AddWithValue("@ngay", ngay.Date);  // Lấy ngày
                    cmd.Parameters.AddWithValue("@gioBD", gioBD); // Lấy giờ bắt đầu
                    cmd.Parameters.AddWithValue("@gioKT", gioKT); // Lấy giờ kết thúc
                    cmd.Parameters.AddWithValue("@loaiGT", loaiGioTruc);

                    // Thêm tham số output
                    SqlParameter outputParam = new SqlParameter("@output", SqlDbType.NVarChar, 100)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputParam);

                    cmd.ExecuteNonQuery();

                    // Lấy giá trị từ tham số output
                    string resultMessage = outputParam.Value.ToString();
                    MessageBox.Show(resultMessage, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // 2. Gọi thủ tục tinhHeSoTT để tính hệ số trực
                using (SqlCommand cmdTinhHeSo = new SqlCommand("tinhHeSoTT", db.Connection))
                {
                    cmdTinhHeSo.CommandType = CommandType.StoredProcedure;

                    cmdTinhHeSo.Parameters.AddWithValue("@ml", maLich);
                    cmdTinhHeSo.Parameters.AddWithValue("@mn", maNV);
                    cmdTinhHeSo.Parameters.AddWithValue("@mp", maPhong);

                    cmdTinhHeSo.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnThem_Click(object sender, EventArgs e)
        {
            db.Connect();
            string maLich = cbMaLich.SelectedItem?.ToString() ?? "";
            string maNV = cbMaNV.SelectedItem?.ToString() ?? "";
            string maPM = cbMaPM.SelectedItem?.ToString() ?? "";
            string nd = txtNoiDung.Text.ToString() ?? "";
            DateTime ngay = dtpNgay.Value.Date;
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

            // Kiểm tra các ComboBox
            if (string.IsNullOrEmpty(maLich))
                danhSachLoi.Add("Chưa chọn mã lịch.");
            if (string.IsNullOrEmpty(maNV))
                danhSachLoi.Add("Chưa chọn mã nhân viên.");
            if (string.IsNullOrEmpty(maPM))
                danhSachLoi.Add("Chưa chọn mã phòng máy.");
            if (ngay < DateTime.Today)
            {
                danhSachLoi.Add("Ngày thực hành phải cùng hoặc sau ngày hôm nay");
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
            XepLichTruc(maLich, maNV, maPM, nd,ngay, gioBatDau, gioKetThuc, loai);
            ResetFormBich();
        }

        private void button2_Click(object sender, EventArgs e)
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
                        formBich.LoadDanhSachPTT();
                    }
                }
            }
        }
    }
}
