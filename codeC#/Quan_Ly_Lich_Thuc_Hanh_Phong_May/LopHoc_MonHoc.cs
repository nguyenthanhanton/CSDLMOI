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
            dataGrid.DataSource = lopHocBindingSource;
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
            DateTime currentClicked = DateTime.Now;
            if((currentClicked - lastClicked).TotalMilliseconds < SystemInformation.DoubleClickTime)
            {
                fullTab.SelectedTab = (fullTab.SelectedTab == tabPage1) ? tabPage2 : tabPage1;
                string query = fullTab.SelectedTab == tabPage1 ? refreshLopHoc : refreshMonHoc;
                LoadData(query);
            }
            lastClicked = currentClicked;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DateTime currentClicked = DateTime.Now;
            if ((currentClicked - lastClicked).TotalMilliseconds < SystemInformation.DoubleClickTime)
            {
                fullTab.SelectedTab = (fullTab.SelectedTab == tabPage2) ? tabPage1 : tabPage2;
                string query = fullTab.SelectedTab == tabPage2 ? refreshMonHoc : refreshLopHoc;
                LoadData(query);
            }
            lastClicked = currentClicked;
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
            // TODO: This line of code loads data into the 'quanLyLichThucHanhDataSet.LopHoc' table. You can move, or remove it, as needed.
            this.lopHocTableAdapter.Fill(this.quanLyLichThucHanhDataSet.LopHoc);
            // TODO: This line of code loads data into the 'quanLyLichThucHanhDataSet.MonHoc' table. You can move, or remove it, as needed.
            this.monHocTableAdapter.Fill(this.quanLyLichThucHanhDataSet.MonHoc);

        }

        private void fullTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fullTab.SelectedTab == tabPage1) dataGrid.DataSource = lopHocBindingSource;
            else dataGrid.DataSource = monHocBindingSource;
        }

        private void btn_Quang_Click(object sender, EventArgs e)
        {
        }

        //CSDL Lop Hoc
        private void lopHoc_Add_Click(object sender, EventArgs e)
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
                if(btnDH.Checked == true) cmd.Parameters.AddWithValue("@LoaiHinhDT", "Đại học");
                else if(btnCH.Checked == true) cmd.Parameters.AddWithValue("@LoaiHinhDT", "Cao học");
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            LoadData(refreshLopHoc);
        }
        private void lopHoc_Update_Click(object sender, EventArgs e)
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
                cmd.ExecuteNonQuery();
            }
            LoadData(refreshLopHoc);
        }

        private void lopHoc_Del_Click(object sender, EventArgs e)
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
                cmd.ExecuteNonQuery();
            }
            LoadData(refreshLopHoc);
        }

        private void lopHoc_Buscar_Click(object sender, EventArgs e)
        {
            string query = "EXEC Quang_TimKiemLopHoc @MaLop";
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
                cmd.ExecuteNonQuery();

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable search = new DataTable();
                adapter.Fill(search);
                dataGrid.DataSource = search;
            }
        }
        private void clearLopHoc_Click(object sender, EventArgs e)
        {
            MaLH.Clear();
            TenLH.Clear();
            soKH.SelectedIndex = 0;
            quanSo.Value = 1;
            btnDH.Checked = btnCH.Checked = false;
        }

        //CSDL Mon Hoc
        private void monHoc_Add_Click(object sender, EventArgs e)
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
                cmd.ExecuteNonQuery();
            }
            LoadData(refreshMonHoc);
        }
        private void monHoc_Buscar_Click(object sender, EventArgs e)
        {
            string query = "EXEC Quang_TimKiemMonHoc @MaMH";
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
                cmd.ExecuteNonQuery();

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable search = new DataTable();
                adapter.Fill(search);
                dataGrid.DataSource = search;
            }
        }
        private void monHoc_Del_Click(object sender, EventArgs e)
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
                cmd.ExecuteNonQuery();
            }
            LoadData(refreshMonHoc);
        }
        private void monHoc_Update_Click(object sender, EventArgs e)
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
                if (soTC.SelectedIndex == 0) {
                    MessageBox.Show("Please select the credit score!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                cmd.Parameters.AddWithValue("@MaMH", MaMH.Text);
                cmd.Parameters.AddWithValue("@TenMH", tenMH.Text);
                cmd.Parameters.AddWithValue("@SoTinChi", Convert.ToInt32(soTC.SelectedItem));
                cmd.Parameters.AddWithValue("@SoTiet", Convert.ToInt32(soTiet.Value));
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            LoadData(refreshMonHoc);
        }
        private void clearMonHoc_Click(object sender, EventArgs e)
        {
            MaMH.Clear();
            tenMH.Clear();
            soTC.SelectedIndex = 0;
            soTiet.Value = 1;
        }

        private void btnDH_CheckedChanged(object sender, EventArgs e)
        {
            if(btnDH.Checked) btnCH.Checked = false;
        }

        private void btnCH_CheckedChanged(object sender, EventArgs e)
        {
            if (btnCH.Checked) btnDH.Checked = false;
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
