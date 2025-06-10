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
    public partial class FormTimKiem : Form
    {
        private Connect1412 db = new Connect1412();
        public event Action<DataTable> TimKiemXong;
        public FormTimKiem()
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

        private void FormTìmKiem_Load(object sender, EventArgs e)
        {

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
            if (result == DialogResult.Yes)
            {
                ResetFormBich();
            }
        }
        public DataTable TimKiemThongTin()
        {
            db.Connect();
            string soPhieu = txtSoPhieu.Text ?? "";
            string maLich = txtMaLich.Text ?? "";
            string tenNV = txtMaNV.Text ?? "";
            string tenPhong = txtMaPhong.Text ?? "";
            string noiDung = txtNoiDung.Text ?? "";

            TimeSpan gioBD = dtpBD.Value.TimeOfDay;
            TimeSpan gioKT = dtpKT.Value.TimeOfDay;
            string loai = "";
            if (rdbHC.Checked)
                loai = "Hành chính";
            else if (rdbGN.Checked)
                loai = "Giờ nghỉ";

            string ngay = txtNgay.Text?.Trim() ?? "";
            string thang = txtThang.Text?.Trim() ?? "";
            string nam = txtNam.Text?.Trim() ?? "";
            float hstt1 = 0;
            float hstt2 = 0;

            float soGio1 = 0;
            float soGio2 = 0;

            if (!string.IsNullOrWhiteSpace(txtHS1.Text))
                float.TryParse(txtHS1.Text.Trim(), out hstt1);

            if (!string.IsNullOrWhiteSpace(txtHS2.Text))
                float.TryParse(txtHS2.Text.Trim(), out hstt2);

            if (!string.IsNullOrWhiteSpace(txtSoGio1.Text))
                float.TryParse(txtSoGio1.Text.Trim(), out soGio1);

            if (!string.IsNullOrWhiteSpace(txtSoGio2.Text))
                float.TryParse(txtSoGio2.Text.Trim(), out soGio2);
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("bich_TimKiemPTT", db.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SoPhieu",soPhieu);
                    cmd.Parameters.AddWithValue("@MaLich", maLich);
                    cmd.Parameters.AddWithValue("@TenNV",  tenNV);
                    cmd.Parameters.AddWithValue("@TenPhong",  tenPhong);
                    cmd.Parameters.AddWithValue("@NoiDung", noiDung);

                    cmd.Parameters.AddWithValue("@GioBD", gioBD == TimeSpan.Zero ? (object)DBNull.Value : gioBD);
                    cmd.Parameters.AddWithValue("@GioKT", gioKT == TimeSpan.Zero ? (object)DBNull.Value : gioKT);
                    cmd.Parameters.AddWithValue("@Loai", loai);
                    if (int.TryParse(ngay, out int _ngay))
                        cmd.Parameters.AddWithValue("@Ngay", _ngay);
                    else
                        cmd.Parameters.AddWithValue("@Ngay", 0);

                    if (int.TryParse(thang, out int _thang))
                        cmd.Parameters.AddWithValue("@Thang", _thang);
                    else
                        cmd.Parameters.AddWithValue("@Thang", 0);

                    if (int.TryParse(nam, out int _nam))
                        cmd.Parameters.AddWithValue("@Nam", _nam);
                    else
                        cmd.Parameters.AddWithValue("@Nam", 0);
                    cmd.Parameters.AddWithValue("@HS1", hstt1);
                    cmd.Parameters.AddWithValue("@HS2", hstt2);
                    cmd.Parameters.AddWithValue("@SG1", soGio1);
                    cmd.Parameters.AddWithValue("@SG2", soGio2);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt; // Trả về bảng kết quả
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            DataTable dt = TimKiemThongTin();
            TimKiemXong?.Invoke(dt);
        }
    }
}
