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
    public partial class TrangChu : Form
    {
        private Connect1412 db = new Connect1412();
        public TrangChu()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        private void TrangChu_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximumSize = new Size(1700, 800);
            this.WindowState = FormWindowState.Maximized;
            Reset();
            HighlightButton(btnTrangChu);
        }
        private void LoadChildForm(Form childForm)
        {
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            childForm.MaximumSize = new Size(1714,983);
            childForm.WindowState = FormWindowState.Maximized;
            this.panelHienForm.Controls.Clear();

            this.panelHienForm.Controls.Add(childForm);
            childForm.Show();
        }
        private void HighlightButton(Button Button)
        {
            foreach (Control control in tableLayoutPanel1.Controls) // giả sử các nút nằm trong panelMenu
            {
                if (control is Button btn)
                {
                    btn.BackColor = SystemColors.GradientInactiveCaption; // màu mặc định
                }
            }
            Button.BackColor = Color.LightSteelBlue;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }    
        }
        private void button6_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            LopHoc_MonHoc f = new LopHoc_MonHoc();
            LoadChildForm(f);
        }

        private void btnGVPM_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            hieu_GVPM f = new hieu_GVPM();
            LoadChildForm(f);
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            Tonform2 frm = new Tonform2();
            LoadChildForm(frm);
        }
        private void Reset()
        {
            this.panelHienForm.Controls.Clear();
            this.panelHienForm.Controls.Add(this.panelTrangChu);
            FormThongKePhieu f = new FormThongKePhieu();
            f.TopLevel = false;
            f.FormBorderStyle = FormBorderStyle.None;
            f.Dock = DockStyle.Fill;
            panelLuong.Controls.Clear();
            panelLuong.Controls.Add(f);
            f.Show();
        }
        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            Reset();
        }

        private void btnLTH_Click(object sender, EventArgs e)
        {
            FormBich f = new FormBich();
            LoadChildForm(f);
        }

        private void btnLuong_Click(object sender, EventArgs e)
        {
            tonthanhtoanform f = new tonthanhtoanform();
            f.TopLevel = false;
            f.FormBorderStyle = FormBorderStyle.None;
            f.Dock = DockStyle.Fill;
            panelLuong.Controls.Clear();
            panelLuong.Controls.Add(f);
            f.Show();
        }
        private void btnLichTruc_Click(object sender, EventArgs e)
        {
            FormThongKePhieu f = new FormThongKePhieu();
            f.TopLevel = false;
            f.FormBorderStyle = FormBorderStyle.None;
            f.Dock = DockStyle.Fill;
            panelLuong.Controls.Clear();
            panelLuong.Controls.Add(f);
            f.Show();
        }
    }
}
