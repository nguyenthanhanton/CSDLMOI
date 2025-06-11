using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_Ly_Lich_Thuc_Hanh_Phong_May
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            try
            {
                using (MemoryStream ms = new MemoryStream(Properties.Resources.anhLogIn))
                {
                    pictureBox1.Image = Image.FromStream(ms);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load ảnh: " + ex.Message);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txt_pass.UseSystemPasswordChar = false; // Hiện mật khẩu
            }
            else
            {
                txt_pass.UseSystemPasswordChar = true;  // Ẩn mật khẩu
            }

        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc muốn thoát không?","Thông báo",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.TopLevel = false;
            //this.FormBorderStyle = FormBorderStyle.None;
            this.ControlBox = false;
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            this.Hide();
            TrangChu trangChu = new TrangChu();
            trangChu.ShowDialog();
            this.Show();
        }
    }
}
