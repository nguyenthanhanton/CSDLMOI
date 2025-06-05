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
    public partial class FormBich : Form
    {

        private Connect1412 db = new Connect1412();

        FormUpdateLTH f = new FormUpdateLTH();
        FormTTPhieu p = new FormTTPhieu();
        public FormBich()
        {
            InitializeComponent();
        }
        public void LoadDanhSachLTH()
        {

            try
            {
                db.Connect();
                string sql = "SELECT * FROM dbo.bich_xemLTH()";
                DataTable dt = db.ExecuteQuery(sql);
                dataGridViewLTH.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                db.Disconnect();
            }
            dataGridViewLTH.Columns["TenMH"].Width = 210;
        }
        public void LoadDanhSachPTT()
        {
            try
            {
                db.Connect();
                string sql = "SELECT * FROM PhieuTaiTruc";
                DataTable dt = db.ExecuteQuery(sql);

                dataGridViewPTT.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                db.Disconnect();
            }
        }
        private void LoadThongTinLTH()
        {
            string maLich = "";
            string maGiangVien = "";
            string soBuoi = "";
            string maLopHoc = "";
            string tenMonHoc = "";
            string ngayDK = "";
            string hocKy = "";
            string namHoc = "";
            if (dataGridViewLTH.CurrentRow != null)
            {
                DataGridViewRow row = dataGridViewLTH.CurrentRow;
                maLich = row.Cells["MaLich"].Value?.ToString();
                maGiangVien = row.Cells["MaGV"].Value?.ToString();
                soBuoi = row.Cells["SoBuoi"].Value?.ToString();
                maLopHoc = row.Cells["MaLop"].Value?.ToString();
                tenMonHoc = row.Cells["TenMH"].Value?.ToString();
                ngayDK = row.Cells["NgayDK"].Value?.ToString();
                hocKy = row.Cells["HK"].Value?.ToString();
                namHoc = row.Cells["NamHoc"].Value?.ToString();
                if (DateTime.TryParse(ngayDK, out DateTime date))
                {
                    ngayDK = date.ToString("dd/MM/yyyy");
                }
            }
            else
            {
                MessageBox.Show("Chưa chọn dòng nào!");
                return;
            }

            f.FormClosed += f_FormClosed;
            f.TopLevel = false;
            f.FormBorderStyle = FormBorderStyle.None;
            f.Location = new Point(1030, 13);

            page_lth.Controls.Add(f);
            f.Show();
            f.LoadThongTin(maLich, maGiangVien, soBuoi, maLopHoc, tenMonHoc, ngayDK, hocKy, namHoc);
        }
        private void LoadThongTinPTT()
        {
            string maLich = "";
            string maNV = "";
            string maPhong = "";
            string soPhieu = "";
            string noiDungTH = "";
            string ngayTH = "";
            string gioBD = "";
            string gioKT = "";
            string loaiGT = "";
            string hstt = "";
            string soGio = "";
            if (dataGridViewPTT.CurrentRow != null)
            {
                DataGridViewRow row = dataGridViewPTT.CurrentRow;
                maLich = row.Cells["MaLich"].Value?.ToString() ?? "";                      
                maPhong = row.Cells["MaPhong"].Value?.ToString() ?? "";
                maNV = row.Cells["MaNV"].Value?.ToString() ?? "";
                soPhieu = row.Cells["SoPhieu"].Value?.ToString() ?? ""; 
                noiDungTH = row.Cells["NoiDungTH"].Value?.ToString() ?? "";
                ngayTH = row.Cells["NgayTH"].Value?.ToString() ?? "";
                gioBD = row.Cells["GioBD"].Value?.ToString() ?? "";
                gioKT = row.Cells["GioKT"].Value?.ToString() ?? "";
                hstt = row.Cells["HeSoTaiTruc"].Value?.ToString() ?? "";
                soGio = row.Cells["SoGio"].Value?.ToString() ?? "";
                loaiGT = row.Cells["LoaiGioTruc"].Value?.ToString() ?? "";
                // Chuyển định dạng ngày
                if (DateTime.TryParse(ngayTH, out DateTime date))
                {
                    ngayTH = date.ToString("dd/MM/yyyy");
                }
            }
            else
            {
                MessageBox.Show("Chưa chọn dòng nào!");
                return;
            }

            p.FormClosed += p_FormClosed;
            p.TopLevel = false;
            p.FormBorderStyle = FormBorderStyle.None;
            p.Location = new Point(1030, 13);

            page_PTT.Controls.Add(p);
            p.Show();
            p.LoadThongTin(maLich, maNV, maPhong, soPhieu, noiDungTH, ngayTH, gioBD, gioKT,loaiGT, hstt, soGio);
        }
        private void dataGridViewLTH_SelectionChanged(object sender, EventArgs e)
        {
            LoadThongTinLTH();
        }
        private void FormBich_Load(object sender, EventArgs e)
        {
            this.MaximumSize = new Size(int.MaxValue, 670);
            this.WindowState = FormWindowState.Maximized;
            page_lth.Text = "Lịch thực hành";
            page_PTT.Text = "Lịch trực";
            
            LoadDanhSachLTH();
            dataGridViewLTH.SelectionChanged += dataGridViewLTH_SelectionChanged;
            LoadDanhSachPTT();
            dataGridViewPTT.SelectionChanged += dataGridViewPTT_SelectionChanged;
        }
        private void f_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoadDanhSachLTH();
        }
        private void p_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoadDanhSachPTT();
        }

        private void bich_btn_dklth_Click(object sender, EventArgs e)
        {
            f.Hide();
            cbLocLTH.SelectedIndex = -1;
            cbDataLocLTH.SelectedIndex = -1;
            cbDataLocLTH.Items.Clear();
            cbLocLTH.Text = "Lọc theo";
            cbDataLocLTH.Text = "Thông tin lọc";
            PhieuDK p = new PhieuDK();
            p.TopLevel = false;
            p.FormClosed += p_FormClosed;
            p.FormBorderStyle = FormBorderStyle.None;
            p.Location = new Point(1030, 30); // Vị trí

            foreach (Control ctrl in page_lth.Controls)
            {
                if (ctrl is Form)
                {
                    ctrl.Visible = false;
                }
            }
            // Thêm vào TabPage
            page_lth.Controls.Add(p);       
            p.Show();
            LoadDanhSachLTH();
        }

        private void bich_btn_capnhat_Click(object sender, EventArgs e)
        {
            f.Hide();
            cbLocLTH.SelectedIndex = -1;
            cbDataLocLTH.SelectedIndex = -1;
            cbDataLocLTH.Items.Clear();
            cbLocLTH.Text = "Lọc theo";
            cbDataLocLTH.Text = "Thông tin lọc";
            string maLich = "";
            string maGiangVien = "";
            string soBuoi = "";
            string maLopHoc = "";
            string tenMonHoc = "";
            string ngayDK = "";
            string hocKy = "";
            string namHoc = "";
            if (dataGridViewLTH.CurrentRow != null)
            {
                DataGridViewRow row = dataGridViewLTH.CurrentRow;
                maLich = row.Cells["MaLich"].Value?.ToString();
                maGiangVien = row.Cells["MaGV"].Value?.ToString();
                soBuoi = row.Cells["SoBuoi"].Value?.ToString();
                maLopHoc = row.Cells["MaLop"].Value?.ToString();
                tenMonHoc = row.Cells["TenMH"].Value?.ToString();
                ngayDK = row.Cells["NgayDK"].Value?.ToString();
                hocKy = row.Cells["HK"].Value?.ToString();
                namHoc = row.Cells["NamHoc"].Value?.ToString();
                if (DateTime.TryParse(ngayDK, out DateTime date))
                {
                    ngayDK = date.ToString("dd/MM/yyyy"); 
                }
            }
            else
            {
                MessageBox.Show("Chưa chọn dòng nào!");
                return;
            }

            FormUpdateLTH f1 = new FormUpdateLTH();
            f1.FormClosed += f_FormClosed;
            f1.TopLevel = false;
            f1.FormBorderStyle = FormBorderStyle.None;
            f1.Location = new Point(1030, 30);
            foreach (Control ctrl in page_lth.Controls)
            {
                if (ctrl is Form)
                {
                    ctrl.Visible = false;
                }
            }
            page_lth.Controls.Add(f1);
            f1.Show();
            f1.SetThongTin(maLich, maGiangVien, soBuoi, maLopHoc, tenMonHoc, ngayDK, hocKy, namHoc);
            LoadDanhSachLTH();
        }
        private void XoaLichThucHanh(string maLich)
        {
            db.Connect();
            try
            {
                SqlCommand cmd = new SqlCommand("bich_xoaLTH", db.Connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@maLich", maLich);

                // Thêm tham số output
                SqlParameter outputParam = new SqlParameter("@output", SqlDbType.NVarChar, 100);
                outputParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outputParam);

                cmd.ExecuteNonQuery();

                string resultMessage = outputParam.Value.ToString();

                MessageBox.Show(resultMessage, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bich_btn_xoalth_Click(object sender, EventArgs e)
        {
            string maLich = "";
            if (dataGridViewLTH.SelectedRows.Count == 0)
            {
                MessageBox.Show("Chưa chọn dòng nào!");
                return;
            }
            DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa lịch thực hành này không?", // Nội dung
                    "Xác nhận",                             // Tiêu đề
                    MessageBoxButtons.YesNo,                // 2 nút Yes - No
                    MessageBoxIcon.Question                 // Icon dấu hỏi
                );

            if (result == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dataGridViewLTH.SelectedRows)
                {
                    if (row.Cells["MaLich"].Value != null)
                    {
                        maLich = row.Cells["MaLich"].Value.ToString();
                        XoaLichThucHanh(maLich);
                    }
                }
                LoadDanhSachLTH();
            }          
        }

        private void cbLocLTH_SelectedIndexChanged(object sender, EventArgs e)
        {
            db.Connect();
            string selectedField = cbLocLTH.SelectedItem?.ToString() ?? "";
            if (selectedField == "") return;

            cbDataLocLTH.Items.Clear();
            cbDataLocLTH.Text = "Chọn thông tin";
            if (selectedField == "Mã giảng viên")
            {
                cbDataLocLTH.Text = "Chọn mã giảng viên";
                SqlDataReader readerGV = db.ExecuteReader("SELECT MaGV FROM GiangVien");
                if (readerGV != null)
                {
                    while (readerGV.Read())
                    {
                        cbDataLocLTH.Items.Add(readerGV.GetString(0));
                    }
                    readerGV.Close();
                }
            }
            else if (selectedField == "Mã lớp học")
            {
                cbDataLocLTH.Text = "Chọn mã lớp học";
                SqlDataReader readerGV = db.ExecuteReader("SELECT MaLop FROM LopHoc");
                if (readerGV != null)
                {
                    while (readerGV.Read())
                    {
                        cbDataLocLTH.Items.Add(readerGV.GetString(0));
                    }
                    readerGV.Close();
                }
            }
            else if (selectedField == "Tên môn học")
            {
                cbDataLocLTH.Text = "Chọn tên môn học";
                SqlDataReader readerGV = db.ExecuteReader("SELECT TenMH FROM MonHoc");
                if (readerGV != null)
                {
                    while (readerGV.Read())
                    {
                        cbDataLocLTH.Items.Add(readerGV.GetString(0));
                    }
                    readerGV.Close();
                }
            }
            else
            {
                MessageBox.Show("Chọn lọc theo Mã giảng viên/ Mã lớp học/ Tên môn học !!!", "Thông báo", MessageBoxButtons.OK);
                return;
            }
        }
        private void bich_btn_loc_Click(object sender, EventArgs e)
        {
            db.Connect();
            string s1 = cbLocLTH.SelectedItem?.ToString() ?? "";
            string s2 = cbDataLocLTH.SelectedItem?.ToString() ?? "";
            if (string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2))
            {
                MessageBox.Show("Vui lòng chọn đầy đủ điều kiện lọc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string sql = "";
            if (s1 == "Mã giảng viên")
            {
                sql = "select MaLich, SoBuoi, MaGV, MaLop, LichTh.MaMH, TenMH, HK, NamHoc, NgayDK " +
                    "from LichTh join MonHoc on LichTh.MaMH = MonHoc.MaMH " +
                    "where MaGV = @s2";
            }
            else if (s1 == "Mã lớp học")
            {
                sql = "select MaLich, SoBuoi, MaGV, MaLop, LichTh.MaMH, TenMH, HK, NamHoc, NgayDK " +
                        "from LichTh join MonHoc on LichTh.MaMH = MonHoc.MaMH " +
                        "where MaLop = @s2" ;
            }
            else if (s1 == "Tên môn học")
            {
                sql = "select MaLich, SoBuoi, MaGV, MaLop, LichTh.MaMH, TenMH, HK, NamHoc, NgayDK " +
                        "from LichTh join MonHoc on LichTh.MaMH = MonHoc.MaMH " +
                        "where TenMH = @s2";
            }
            try
            {              
                SqlCommand cmd = new SqlCommand(sql, db.Connection);
                cmd.Parameters.AddWithValue("@s2", s2);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridViewLTH.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            dataGridViewLTH.Columns["TenMH"].Width = 210;
        }

        private void btn_resetLTH_Click(object sender, EventArgs e)
        {
            cbLocLTH.SelectedIndex = -1; 
            cbDataLocLTH.SelectedIndex = -1;
            cbLocLTH.Text = "Lọc theo";
            cbDataLocLTH.Text = "Thông tin lọc";
            f.Show();

            foreach (Control ctrl in page_lth.Controls)
            {
                if (ctrl is Form frm && frm != f)
                {
                    frm.Close(); // hoặc frm.Dispose();
                }
            }
            LoadDanhSachLTH();
        }
        private void CapNhatKetQuaTimKiem(DataTable dt)
        {
            dataGridViewLTH.DataSource = dt;
        }
        private void bich_btn_tim_Click(object sender, EventArgs e)
        {
            f.Hide();
            cbLocLTH.SelectedIndex = -1;
            cbDataLocLTH.SelectedIndex = -1;
            cbDataLocLTH.Items.Clear();
            cbLocLTH.Text = "Lọc theo";
            cbDataLocLTH.Text = "Thông tin lọc";
            foreach (Control ctrl in page_lth.Controls)
            {
                if (ctrl is Form)
                {
                    ctrl.Visible = false;
                }
            }
            FormTimKiemLTH f1 = new FormTimKiemLTH();
            f1.FormClosed += f_FormClosed;
            f1.TimKiemXong += CapNhatKetQuaTimKiem;
            f1.TopLevel = false;
            f1.FormBorderStyle = FormBorderStyle.None;
            f1.Location = new Point(1030, 13);
            page_lth.Controls.Add(f1);
            
            f1.Show();
            LoadDanhSachLTH();
        }

        private void cbLocPTT_SelectedIndexChanged(object sender, EventArgs e)
        {
            db.Connect();
            string selectedField = cbLocPTT.SelectedItem?.ToString() ?? "";
            if (selectedField == "") return;

            cbDataLocPTT.Items.Clear();
            cbDataLocPTT.Text = "Thông tin lọc";
            if (selectedField == "Mã lịch")
            {
                SqlDataReader readerGV = db.ExecuteReader("SELECT MaLich FROM LichTH");
                if (readerGV != null)
                {
                    cbDataLocPTT.Text = "Chọn mã lịch";
                    while (readerGV.Read())
                    {
                        cbDataLocPTT.Items.Add(readerGV.GetString(0));
                    }
                    readerGV.Close();
                }
            }
            else if (selectedField == "Mã nhân viên")
            {
                SqlDataReader readerGV = db.ExecuteReader("SELECT MaNV FROM NhanVien");
                if (readerGV != null)
                {
                    cbDataLocPTT.Text = "Chọn nhân viên";
                    while (readerGV.Read())
                    {
                        cbDataLocPTT.Items.Add(readerGV.GetString(0));
                    }
                    readerGV.Close();
                }
            }
            else if (selectedField == "Mã phòng máy")
            {
                SqlDataReader readerGV = db.ExecuteReader("SELECT MaPhong FROM PhongMay");
                if (readerGV != null)
                {
                    cbDataLocPTT.Text = "Chọn mã phòng máy";
                    while (readerGV.Read())
                    {
                        cbDataLocPTT.Items.Add(readerGV.GetString(0));
                    }
                    readerGV.Close();
                }
            }
            else
            {
                MessageBox.Show("Chọn lọc theo Mã lịch/ Mã nhân viên/ Mã phòng máy !!!", "Thông báo", MessageBoxButtons.OK);
                return;
            }
        }

        private void btnLocPTT_Click(object sender, EventArgs e)
        {
            db.Connect();
            string s1 = cbLocPTT.SelectedItem?.ToString() ?? "";
            string s2 = cbDataLocPTT.SelectedItem?.ToString() ?? "";
            if (string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2))
            {
                MessageBox.Show("Vui lòng chọn đầy đủ điều kiện lọc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string sql = "";
            if (s1 == "Mã lịch")
            {
                sql = "select * from PhieuTaiTruc where MaLich = @s2";
            }
            else if (s1 == "Mã nhân viên")
            {
                sql = "select * from PhieuTaiTruc where MaNV = @s2";
            }
            else if (s1 == "Mã phòng máy")
            {
                sql = "select * from PhieuTaiTruc where MaPhong = @s2";
            }
            try
            {
                SqlCommand cmd = new SqlCommand(sql, db.Connection);
                cmd.Parameters.AddWithValue("@s2", s2);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridViewPTT.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewPTT_SelectionChanged(object sender, EventArgs e)
        {
            LoadThongTinPTT();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabPage selectedTab = tabControl1.SelectedTab;

            if (selectedTab == page_lth)
            {
                LoadDanhSachLTH();
                dataGridViewLTH.SelectionChanged += dataGridViewLTH_SelectionChanged;                
            }
            else if (selectedTab == page_PTT)
            {
                LoadDanhSachPTT();
                dataGridViewPTT.SelectionChanged += dataGridViewPTT_SelectionChanged;
            }
        }

        private void ptt_btnDK_Click(object sender, EventArgs e)
        {
            p.Hide();
            cbLocPTT.SelectedIndex = -1;
            cbDataLocPTT.SelectedIndex = -1;
            cbDataLocPTT.Items.Clear();
            cbLocPTT.Text = "Lọc theo";
            cbDataLocPTT.Text = "Thông tin lọc";
            PhieuTaiTruc ptt = new PhieuTaiTruc();
            ptt.FormClosed += p_FormClosed;
            ptt.TopLevel = false;
            ptt.FormBorderStyle = FormBorderStyle.None;

            ptt.Location = new Point(1030, 30); // Vị trí
            foreach (Control ctrl in page_PTT.Controls)
            {
                if (ctrl is Form)
                {
                    ctrl.Visible = false;
                }
            }
            // Thêm vào TabPage
            page_PTT.Controls.Add(ptt);
            ptt.Show();
            LoadDanhSachPTT();
        }

        private void ptt_btnUpd_Click(object sender, EventArgs e)
        {
            p.Hide();
            cbLocPTT.SelectedIndex = -1;
            cbDataLocPTT.SelectedIndex = -1;
            cbDataLocPTT.Items.Clear();
            cbLocPTT.Text = "Lọc theo";
            cbDataLocPTT.Text = "Thông tin lọc";
            string maLich = "";
            string maNV = "";
            string maPhong = "";
            string soPhieu = "";
            string noiDungTH = "";
            string ngayTH = "";
            string gioBD = "";
            string gioKT = "";
            string loaiGT = "";
            string hstt = "";
            string soGio = "";
            if (dataGridViewPTT.CurrentRow != null)
            {
                DataGridViewRow row = dataGridViewPTT.CurrentRow;
                maLich = row.Cells["MaLich"].Value?.ToString() ?? "";
                maPhong = row.Cells["MaPhong"].Value?.ToString() ?? "";
                maNV = row.Cells["MaNV"].Value?.ToString() ?? "";
                soPhieu = row.Cells["SoPhieu"].Value?.ToString() ?? "";
                noiDungTH = row.Cells["NoiDungTH"].Value?.ToString() ?? "";
                ngayTH = row.Cells["NgayTH"].Value?.ToString() ?? "";
                gioBD = row.Cells["GioBD"].Value?.ToString() ?? "";
                gioKT = row.Cells["GioKT"].Value?.ToString() ?? "";
                hstt = row.Cells["HeSoTaiTruc"].Value?.ToString() ?? "";
                soGio = row.Cells["SoGio"].Value?.ToString() ?? "";
                loaiGT = row.Cells["LoaiGioTruc"].Value?.ToString() ?? "";
                // Chuyển định dạng ngày
                if (DateTime.TryParse(ngayTH, out DateTime date))
                {
                    ngayTH = date.ToString("dd/MM/yyyy");
                }
            }
            else
            {
                MessageBox.Show("Chưa chọn dòng nào!");
                return;
            }

            FormTTPhieu p1 = new FormTTPhieu();
            p1.FormClosed += p_FormClosed;
            p1.TopLevel = false;
            p1.FormBorderStyle = FormBorderStyle.None;
            p1.Location = new Point(1030, 30);

            foreach (Control ctrl in page_PTT.Controls)
            {
                if (ctrl is Form)
                {
                    ctrl.Visible = false;
                }
            }
            page_PTT.Controls.Add(p1);
            p1.Show();
            p1.SetThongTin(maLich, maNV, maPhong,soPhieu, noiDungTH, ngayTH, gioBD,gioKT,loaiGT, hstt,soGio);
            LoadDanhSachPTT();
        }
        private void XoaPhieuTaiTruc(string maLich, string maNV, string maPhong)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("bich_xoaPTT", db.Connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@maLich", maLich);
                cmd.Parameters.AddWithValue("@maNV", maNV);
                cmd.Parameters.AddWithValue("@maPhong", maPhong);

                // Thêm tham số output
                SqlParameter outputParam = new SqlParameter("@output", SqlDbType.NVarChar, 100);
                outputParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outputParam);

                cmd.ExecuteNonQuery();

                string resultMessage = outputParam.Value.ToString();

                MessageBox.Show(resultMessage, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ptt_btnXoa_Click(object sender, EventArgs e)
        {
            db.Connect();
            string maLich = "";
            string maNV = "";
            string maPhong = "";
            if (dataGridViewPTT.SelectedRows.Count == 0)
            {
                MessageBox.Show("Chưa chọn dòng nào!");
                return;
            }
            DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa lịch thực hành này không?", // Nội dung
                    "Xác nhận",                             // Tiêu đề
                    MessageBoxButtons.YesNo,                // 2 nút Yes - No
                    MessageBoxIcon.Question                 // Icon dấu hỏi
                );

            if (result == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dataGridViewPTT.SelectedRows)
                {
                        maLich = row.Cells["MaLich"].Value.ToString();
                        maNV = row.Cells["MaNV"].Value.ToString();
                        maPhong = row.Cells["MaPhong"].Value.ToString();
                        XoaPhieuTaiTruc(maLich, maNV, maPhong);
                }
                LoadDanhSachPTT();
            }
        }
        private void CapNhatKetQuaTimKiemPTT(DataTable dt)
        {
            dataGridViewPTT.DataSource = dt;
        }

        private void ptt_btnTK_Click(object sender, EventArgs e)
        {
            p.Hide();
            cbLocPTT.SelectedIndex = -1;
            cbDataLocPTT.SelectedIndex = -1;
            cbDataLocPTT.Items.Clear();
            cbLocPTT.Text = "Lọc theo";
            cbDataLocPTT.Text = "Thông tin lọc";
            foreach (Control ctrl in page_PTT.Controls)
            {
                if (ctrl is Form)
                {
                    ctrl.Visible = false;
                }
            }
            FormTimKiem f1 = new FormTimKiem();
            f1.FormClosed += p_FormClosed;
            f1.TimKiemXong += CapNhatKetQuaTimKiem;
            f1.TopLevel = false;
            f1.FormBorderStyle = FormBorderStyle.None;
            f1.Location = new Point(1030, 13);
            page_PTT.Controls.Add(f1);

            f1.Show();
            LoadDanhSachPTT();
        }

        private void ptt_btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?",
                                          "Xác nhận thoát",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close(); 
                db.Disconnect();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?",
                                          "Xác nhận thoát",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
                db.Disconnect();
            }
        }

        private void ptt_btnReset_Click(object sender, EventArgs e)
        {
            cbLocPTT.SelectedIndex = -1;
            cbDataLocPTT.SelectedIndex = -1;
            cbDataLocPTT.Items.Clear();
            cbLocPTT.Text = "Lọc theo";
            cbDataLocPTT.Text = "Thông tin lọc";
            p.Show();

            foreach (Control ctrl in page_PTT.Controls)
            {
                if (ctrl is Form frm && frm != p)
                {
                    frm.Close(); // hoặc frm.Dispose();
                }
            }
            LoadDanhSachPTT();
        }
    }
}
