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
    public partial class FormThongKePhieu : Form
    {
        private Connect1412 db = new Connect1412();
        public FormThongKePhieu()
        {
            InitializeComponent();
        }

        private void panelPTT_Paint(object sender, PaintEventArgs e)
        {

        }
        private void LoadDanhSachPTT()
        {
            db.Connect();
            try
            {
                string updateSql = "EXEC CapNhatTatCaHeSoTaiTruc";
                db.ExecuteNonQuery(updateSql);
                string sql = "SELECT * FROM dbo.bich_xemPTT() ORDER BY SoPhieu";
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
        private void ResetCombobox()
        {
            cbTenNV.Items.Clear();
            cbTenNV.Text = "Chọn tên nhân viên";
            cbTenNV.SelectedIndex = -1;
            cbThang.Items.Clear();
            cbThang.Text = "Chọn tháng";
            for (int i = 1; i <= 12; i++)
            {
                cbThang.Items.Add(i.ToString());
            }
            cbThang.SelectedIndex = -1;
            cbNam.Items.Clear();
            cbNam.Text = "Chọn năm";
            for (int i = 2020; i <= 2030; i++)
            {
                cbNam.Items.Add(i.ToString());
            }
            cbNam.SelectedIndex = -1;
            db.Connect();
            try
            {
                SqlDataReader rd = db.ExecuteReader("SELECT TenNV FROM NhanVien where MaNV <> '----'");
                if (rd != null)
                {
                    while (rd.Read())
                    {
                        cbTenNV.Items.Add(rd.GetString(0));
                    }
                    rd.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                db.Disconnect(); // Tự đóng kết nối!
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string tenNV = cbTenNV.SelectedItem?.ToString() ?? "";
            string strThang = cbThang.SelectedItem?.ToString() ?? "";
            string strNam = cbNam.SelectedItem?.ToString() ?? "";

            if (tenNV == "" && strThang == "" && strNam == "")
            {
                MessageBox.Show("Chưa chọn thông tin để thống kê.", "Thông báo");
                return;
            }
            else
            {
                try
                {
                    DataTable dt = new DataTable();
                    db.Connect();
                    int thang = 0;
                    int nam = 0;
                    int.TryParse(strThang, out thang);
                    int.TryParse(strNam, out nam);

                    SqlCommand cmd = new SqlCommand("bich_thongKePTT", db.Connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ten", tenNV);
                    cmd.Parameters.AddWithValue("@thang", thang);
                    cmd.Parameters.AddWithValue("@nam", nam);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
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
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            LoadDanhSachPTT();
            ResetCombobox();
        }

        private void FormThongKePhieu_Load(object sender, EventArgs e)
        {
            LoadDanhSachPTT();
            ResetCombobox();
        }
    }
}
