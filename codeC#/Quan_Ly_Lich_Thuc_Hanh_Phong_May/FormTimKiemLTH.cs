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
    public partial class FormTimKiemLTH : Form
    {
        private Connect1412 db = new Connect1412();
        public event Action<DataTable> TimKiemXong;
        public FormTimKiemLTH()
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

        private void FormTimKiemLTH_Load(object sender, EventArgs e)
        {

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
            if (result == DialogResult.Yes)
            {
                ResetFormBich();
            }
        }
        public DataTable TimKiemThongTin()
        {
            db.Connect();
            string maLich = txtMaLich.Text?.Trim() ?? "";
            string maGV = txtMaGV.Text?.Trim() ?? "";
            string soBuoi = txtSoBuoi.Text?.Trim() ?? "";
            string maLop = txtMaLop.Text?.Trim() ?? "";
            string maMon = txtMaMon.Text?.Trim() ?? "";
            string tenMon = txtTenMH.Text?.Trim() ?? "";
            string namHoc = txtNamHoc.Text?.Trim() ?? "";

            string hocKy = "";
            if (rdbHK1.Checked)
                hocKy = "1";
            else if (rdbHK2.Checked)
                hocKy = "2";

            string ngay = txtNgay.Text?.Trim() ?? "";
            string thang = txtThang.Text?.Trim() ?? "";
            string nam = txtNam.Text?.Trim() ?? "";
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("bich_TimKiemLTH", db.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MaLich", maLich ?? "");
                    cmd.Parameters.AddWithValue("@MaGV", maGV ?? "");
                    cmd.Parameters.AddWithValue("@SoBuoi", soBuoi ?? "");
                    cmd.Parameters.AddWithValue("@MaLop", maLop ?? "");
                    cmd.Parameters.AddWithValue("@MaMon", maMon ?? "");
                    cmd.Parameters.AddWithValue("@TenMon", tenMon ?? "");
                    cmd.Parameters.AddWithValue("@HocKy", hocKy ?? "");
                    cmd.Parameters.AddWithValue("@NamHoc", namHoc ?? "");
                    cmd.Parameters.AddWithValue("@Ngay", ngay ?? "");
                    cmd.Parameters.AddWithValue("@Thang", thang ?? "");
                    cmd.Parameters.AddWithValue("@Nam", nam ?? "");

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

        private void btnOK_Click(object sender, EventArgs e)
        {
            DataTable dt = TimKiemThongTin();

            TimKiemXong?.Invoke(dt);
        }
    }
}
