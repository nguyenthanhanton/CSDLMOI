using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Quan_Ly_Lich_Thuc_Hanh_Phong_May
{
    public partial class LopHoc_MonHoc : Form
    {
        private DateTime lastClicked = DateTime.MinValue;
        private const string connection = "Data Source=PEANUT\\SQLEXPRESS;Initial Catalog=QuanLyLichThucHanh;Integrated Security=True;";
        private const string refreshLopHoc = "EXEC Quang_DanhSachLopHoc", refreshMonHoc = "EXEC Quang_DanhSachMonHoc";
        public LopHoc_MonHoc()
        {
            InitializeComponent();
            trackBar1.Minimum = 1;
            trackBar1.Maximum = 100;
            trackBar1.TickStyle = TickStyle.None;

            trackBar2.Minimum = 1;
            trackBar2.Maximum = 75;
            trackBar2.TickStyle = TickStyle.None;
            trackBar2.TickFrequency = 7;

            quanSo.Minimum = 1;
            quanSo.Maximum = 100;
            quanSo.DecimalPlaces = 0;

            soTiet.Minimum = 1;
            soTiet.Maximum = 75;
            soTiet.DecimalPlaces = 0;

            quanSo.Value = soTiet.Value = 1;
            soKH.SelectedIndex = soTC.SelectedIndex = 0;
            fullTab.SelectedTab = tabPage1;
            controlTab.SelectedTab = tabPage3;
            LoadData(refreshLopHoc);

            MHSwap.Text = LHSwap.Text = "Cho phép chỉnh sửa";
            MaLH.Enabled = TenLH.Enabled = quanSo.Enabled = soKH.Enabled = btnDH.Enabled = btnCH.Enabled = MaMH.Enabled = tenMH.Enabled = soTC.Enabled = soTiet.Enabled = false;
            trackBar1.Enabled = trackBar2.Enabled = false;
        }

        private void btn_TT_GV_Click(object sender, EventArgs e)
        {
            fullTab.SelectedIndex = 0;
        }

        private void btn_TT_MH_Click(object sender, EventArgs e)
        {
            fullTab.SelectedIndex = 1;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            quanSo.Value = trackBar1.Value;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            trackBar1.Value = (int)quanSo.Value;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            trackBar2.Value = (int)soTiet.Value;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            soTiet.Value = trackBar2.Value;
        }

        private void btn_TT_TK_Click(object sender, EventArgs e)
        {
            bool allowEdits = !MaLH.Enabled;
            MaLH.Enabled = allowEdits;
            TenLH.Enabled = allowEdits;
            quanSo.Enabled = allowEdits;
            soKH.Enabled = allowEdits;
            btnDH.Enabled = allowEdits;
            btnCH.Enabled = allowEdits;
            trackBar1.Enabled = allowEdits;
            MHSwap.Text = allowEdits ? "Tắt chỉnh sửa" : "Cho phép chỉnh sửa";
            if (MHSwap.Text != "Tắt chỉnh sửa")
            {
                MaLH.Cursor = TenLH.Cursor = quanSo.Cursor = soKH.Cursor = btnDH.Cursor = btnCH.Cursor = trackBar1.Cursor = Cursors.No;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool allowEdits = !MaMH.Enabled;
            MaMH.Enabled = allowEdits;
            tenMH.Enabled = allowEdits;
            soTC.Enabled = allowEdits;
            soTiet.Enabled = allowEdits;
            trackBar2.Enabled = allowEdits;
            LHSwap.Text = allowEdits ? "Tắt chỉnh sửa" : "Cho phép chỉnh sửa";
            if (LHSwap.Text != "Tắt chỉnh sửa")
            {
                MaLH.Cursor = TenLH.Cursor = quanSo.Cursor = soKH.Cursor = btnDH.Cursor = btnCH.Cursor = trackBar1.Cursor = Cursors.No;
            }
        }

        private void LoadData(string query)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGrid.DataSource = dt;
            }
        }

        private void LopHoc_MonHoc_Load(object sender, EventArgs e)
        {

        }

        private void fullTab_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void btn_Quang_Click(object sender, EventArgs e)
        {
            formchungcs formchungcs = new formchungcs();
            formchungcs.Show();
            this.Close();
        }

        //CSDL Lop Hoc
        private void lopHoc_Add_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Xác nhận thêm lớp học?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (res == DialogResult.Yes && MHSwap.Text == "Tắt chỉnh sửa")
            {
                string query = "EXEC Quang_InsertLopHoc @MaLop, @TenLop, @QuanSo, @KhoaHoc, @LoaiHinhDT";
                SqlConnection conn = new SqlConnection(connection);
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (string.IsNullOrEmpty(MaLH.Text) || string.IsNullOrWhiteSpace(MaLH.Text))
                    {
                        MessageBox.Show("MaLop is required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (string.IsNullOrEmpty(TenLH.Text) || string.IsNullOrWhiteSpace(TenLH.Text))
                    {
                        MessageBox.Show("TenLH is required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (soKH.SelectedIndex == 0)
                    {
                        MessageBox.Show("Please select the class number!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    cmd.Parameters.AddWithValue("@MaLop", MaLH.Text);
                    cmd.Parameters.AddWithValue("@TenLop", TenLH.Text);
                    cmd.Parameters.AddWithValue("@QuanSo", Convert.ToInt32(quanSo.Value));
                    cmd.Parameters.AddWithValue("@KhoaHoc", Convert.ToInt32(soKH.SelectedItem));
                    if (btnDH.Checked == true) cmd.Parameters.AddWithValue("@LoaiHinhDT", "Đại học");
                    else if (btnCH.Checked == true) cmd.Parameters.AddWithValue("@LoaiHinhDT", "Cao học");
                    conn.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (System.Data.SqlClient.SqlException)
                    {
                        MessageBox.Show("Lớp học đã tồn tại!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                LoadData(refreshLopHoc);
            } 
            else if (res == DialogResult.Yes && MHSwap.Text == "Cho phép chỉnh sửa")
            {
                MessageBox.Show("Chưa có cấp phép chính sửa dữ liệu!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lopHoc_Update_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Xác nhận cập nhật lớp học?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (res == DialogResult.Yes && MHSwap.Text == "Tắt chỉnh sửa")
            {
                string query = "EXEC Quang_UpdateLopHoc @MaLop, @TenLop, @QuanSo, @KhoaHoc, @LoaiHinhDT";
                SqlConnection conn = new SqlConnection(connection);
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (string.IsNullOrEmpty(MaLH.Text) || string.IsNullOrWhiteSpace(MaLH.Text))
                    {
                        MessageBox.Show("MaLop is required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (string.IsNullOrEmpty(TenLH.Text) || string.IsNullOrWhiteSpace(TenLH.Text))
                    {
                        MessageBox.Show("TenLH is required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (soKH.SelectedIndex == 0)
                    {
                        MessageBox.Show("Please select the class number!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    cmd.Parameters.AddWithValue("@MaLop", MaLH.Text);
                    cmd.Parameters.AddWithValue("@TenLop", TenLH.Text);
                    cmd.Parameters.AddWithValue("@QuanSo", Convert.ToInt32(quanSo.Value));
                    cmd.Parameters.AddWithValue("@KhoaHoc", Convert.ToInt32(soKH.SelectedItem));
                    if (btnDH.Checked) cmd.Parameters.AddWithValue("@LoaiHinhDT", btnDH.Text);
                    else if (btnCH.Checked) cmd.Parameters.AddWithValue("@LoaiHinhDT", btnCH.Text);
                    conn.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (System.Data.SqlClient.SqlException)
                    {
                        MessageBox.Show("Lớp học không tồn tại!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                LoadData(refreshLopHoc);
            }
            else if (res == DialogResult.Yes && MHSwap.Text == "Cho phép chỉnh sửa")
            {
                MessageBox.Show("Chưa có cấp phép chính sửa dữ liệu!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lopHoc_Del_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Xác nhận xóa lớp học?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (res == DialogResult.Yes && MHSwap.Text == "Tắt chỉnh sửa")
            {
                string query = "EXEC Quang_DeleteLopHoc @MaLop";
                SqlConnection conn = new SqlConnection(connection);
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (string.IsNullOrEmpty(MaLH.Text) || string.IsNullOrWhiteSpace(MaLH.Text))
                    {
                        MessageBox.Show("MaLop is required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    cmd.Parameters.AddWithValue("@MaLop", MaLH.Text);
                    conn.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (System.Data.SqlClient.SqlException)
                    {
                        MessageBox.Show("Lớp học không tồn tại!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                LoadData(refreshLopHoc);
            }
            else if (res == DialogResult.Yes && MHSwap.Text == "Cho phép chỉnh sửa")
            {
                MessageBox.Show("Chưa có cấp phép chính sửa dữ liệu!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lopHoc_Buscar_Click(object sender, EventArgs e)
        {
            string query = "EXEC Quang_TimKiemLopHoc @MaLop", searching = searchLH.Text.Trim();
            SqlConnection conn = new SqlConnection(connection);
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                if (string.IsNullOrEmpty(searchLH.Text) || string.IsNullOrWhiteSpace(searchLH.Text))
                {
                    MessageBox.Show("Input data!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                cmd.Parameters.AddWithValue("@MaLop", '%' + searching + '%');
                conn.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable search = new DataTable();
                    adapter.Fill(search);
                    dataGrid.DataSource = search;
                }
                finally
                {
                    conn.Close();
                }

            }
        }
        private void clearLopHoc_Click(object sender, EventArgs e)
        {
            MaLH.Clear();
            TenLH.Clear();
            soKH.SelectedIndex = 0;
            quanSo.Value = 1;
            btnDH.Checked = btnCH.Checked = false;
            searchLH.Clear();
            LoadData(refreshLopHoc);
        }

        //CSDL Mon Hoc
        private void monHoc_Add_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Xác nhận thêm môn học?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (res == DialogResult.Yes && LHSwap.Text == "Tắt chỉnh sửa")
            {
                string query = "EXEC Quang_InsertMonHoc @MaMH, @TenMH, @SoTinChi, @SoTiet";
                SqlConnection conn = new SqlConnection(connection);
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (string.IsNullOrEmpty(MaMH.Text) || string.IsNullOrWhiteSpace(MaMH.Text))
                    {
                        MessageBox.Show("MaMH is required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (string.IsNullOrEmpty(tenMH.Text) || string.IsNullOrWhiteSpace(tenMH.Text))
                    {
                        MessageBox.Show("TenMH is required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (soTC.SelectedIndex == 0)
                    {
                        MessageBox.Show("Please select the credit score!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    cmd.Parameters.AddWithValue("@MaMH", MaMH.Text);
                    cmd.Parameters.AddWithValue("@TenMH", tenMH.Text);
                    cmd.Parameters.AddWithValue("@SoTinChi", Convert.ToInt32(soTC.SelectedItem));
                    cmd.Parameters.AddWithValue("@SoTiet", Convert.ToInt32(soTiet.Value));
                    conn.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (System.Data.SqlClient.SqlException)
                    {
                        MessageBox.Show("Môn học đã tồn tại!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                LoadData(refreshMonHoc);
            }
            else if (res == DialogResult.Yes && LHSwap.Text == "Cho phép chỉnh sửa")
            {
                MessageBox.Show("Chưa có cấp phép chính sửa dữ liệu!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void monHoc_Buscar_Click(object sender, EventArgs e)
        {
            string query = "EXEC Quang_TimKiemMonHoc @MaMH", searching = searchMH.Text.Trim();
            SqlConnection conn = new SqlConnection(connection);
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                if (string.IsNullOrEmpty(searchMH.Text) || string.IsNullOrWhiteSpace(searchMH.Text))
                {
                    MessageBox.Show("Input data!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                cmd.Parameters.AddWithValue("@MaMH", '%' + searching + '%');
                conn.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable search = new DataTable();
                    adapter.Fill(search);
                    dataGrid.DataSource = search;
                } finally
                {
                    conn.Close();
                }

            }
        }
        private void monHoc_Del_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Xác nhận xóa môn học?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (res == DialogResult.Yes && LHSwap.Text == "Tắt chỉnh sửa")
            {
                string query = "EXEC Quang_DeleteMonHoc @MaMH";
                SqlConnection conn = new SqlConnection(connection);
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (string.IsNullOrEmpty(MaMH.Text) || string.IsNullOrWhiteSpace(MaMH.Text))
                    {
                        MessageBox.Show("MaMH is required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    cmd.Parameters.AddWithValue("@MaMH", MaMH.Text);
                    conn.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (System.Data.SqlClient.SqlException)
                    {
                        MessageBox.Show("Môn học không tồn tại!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                LoadData(refreshMonHoc);
            }
            else if (res == DialogResult.Yes && LHSwap.Text == "Cho phép chỉnh sửa")
            {
                MessageBox.Show("Chưa có cấp phép chính sửa dữ liệu!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void monHoc_Update_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Xác nhận cập nhật môn học?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (res == DialogResult.Yes && LHSwap.Text == "Tắt chỉnh sửa")
            {
                string query = "EXEC Quang_UpdateMonHoc @MaMH, @TenMH, @SoTinChi, @SoTiet";
                SqlConnection conn = new SqlConnection(connection);
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (string.IsNullOrEmpty(MaMH.Text) || string.IsNullOrWhiteSpace(MaMH.Text))
                    {
                        MessageBox.Show("MaMH is required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (string.IsNullOrEmpty(tenMH.Text) || string.IsNullOrWhiteSpace(tenMH.Text))
                    {
                        MessageBox.Show("TenMH is required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (soTC.SelectedIndex == 0)
                    {
                        MessageBox.Show("Please select the credit score!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    cmd.Parameters.AddWithValue("@MaMH", MaMH.Text);
                    cmd.Parameters.AddWithValue("@TenMH", tenMH.Text);
                    cmd.Parameters.AddWithValue("@SoTinChi", Convert.ToInt32(soTC.SelectedItem));
                    cmd.Parameters.AddWithValue("@SoTiet", Convert.ToInt32(soTiet.Value));
                    conn.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (System.Data.SqlClient.SqlException)
                    {
                        MessageBox.Show("Môn học không tồn tại!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                LoadData(refreshMonHoc);
            }
            else if (res == DialogResult.Yes && LHSwap.Text == "Cho phép chỉnh sửa")
            {
                MessageBox.Show("Chưa có cấp phép chính sửa dữ liệu!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void clearMonHoc_Click(object sender, EventArgs e)
        {
            MaMH.Clear();
            tenMH.Clear();
            soTC.SelectedIndex = 0;
            soTiet.Value = 1;
            searchMH.Clear();
            LoadData(refreshMonHoc);
        }

        private void btnDH_CheckedChanged(object sender, EventArgs e)
        {
            if(btnDH.Checked) btnCH.Checked = false;
        }

        private void btnCH_CheckedChanged(object sender, EventArgs e)
        {
            if (btnCH.Checked) btnDH.Checked = false;
        }

        private void lbl_LTHPM_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            fullTab.SelectedTab = (fullTab.SelectedTab == tabPage2) ? tabPage1 : tabPage2;
            controlTab.SelectedTab = (controlTab.SelectedTab == tabPage4) ? tabPage3 : tabPage4;
            lbl_LTHPM.Text = "TỔNG HỢP DỮ LIỆU LỚP HỌC";
            string query = fullTab.SelectedTab == tabPage2 ? refreshMonHoc : refreshLopHoc;
            LoadData(query);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fullTab.SelectedTab = (fullTab.SelectedTab == tabPage1) ? tabPage2 : tabPage1;
            controlTab.SelectedTab = (controlTab.SelectedTab == tabPage3) ? tabPage4 : tabPage3;
            lbl_LTHPM.Text = "TỔNG HỢP DỮ LIỆU MÔN HỌC";
            string query = fullTab.SelectedTab == tabPage1 ? refreshLopHoc : refreshMonHoc;
            LoadData(query);
        }

        //Tac dong vao DataGridView
        private void dataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow rows = dataGrid.Rows[e.RowIndex];
                if (fullTab.SelectedTab == tabPage1)
                {
                    string eduLvl = rows.Cells["LoaiHinhDT"].Value?.ToString();
                    if (eduLvl == "Đại học")
                    {
                        btnDH.Checked = true;
                        btnCH.Checked = false;
                    }
                    else if (eduLvl == "Cao học")
                    {
                        btnCH.Checked = true;
                        btnDH.Checked = false;
                    }
                    MaLH.Text = rows.Cells["MaLop"].Value?.ToString();
                    TenLH.Text = rows.Cells["TenLop"].Value?.ToString();
                    soKH.SelectedItem = Convert.ToString(rows.Cells["KhoaHoc"].Value ?? "58");
                    quanSo.Value = Convert.ToInt32(rows.Cells["QuanSo"].Value ?? 1);

                } else if (fullTab.SelectedTab == tabPage2)
                {
                    MaMH.Text = rows.Cells["MaMH"].Value?.ToString();
                    tenMH.Text = rows.Cells["TenMH"].Value?.ToString();
                    soTC.SelectedItem = Convert.ToString(rows.Cells["SoTC"].Value ?? "0");
                    soTiet.Value = Convert.ToInt32(rows.Cells["SoTiet"].Value ?? 0);
                }
            }
        }
    }
}
