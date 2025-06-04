using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Quan_Ly_Lich_Thuc_Hanh_Phong_May
{
    public partial class tonthanhtoanform : Form
    {
        TonSQL tonSQL = new TonSQL();
        public tonthanhtoanform()
        {
            InitializeComponent();

        }
        public void loaddata(DataTable dt)
        {

            dataGridView1.DataSource = dt;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
            dataGridView1.ScrollBars = ScrollBars.Vertical;
            int totalHeight = dataGridView1.RowCount * dataGridView1.RowTemplate.Height + dataGridView1.ColumnHeadersHeight;
            dataGridView1.Height = Math.Min(totalHeight, 300);
        }
        private void tonthanhtoanform_Load(object sender, EventArgs e)
        {
            tonSQL.taoketnoi();
            int startYear = 2000;
            int currentYear = DateTime.Now.Year;
            dataGridView1.Visible = false;
            for (int year = startYear; year <= currentYear; year++)
            {
                nam.Items.Add(year);
            }
        }

        private void btn_tonadd_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            DataTable dt = tonSQL.laydanhthanhtoan();
            loaddata(dt);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                manv.Text = row.Cells["Mã nhân viên"].Value.ToString();
                thang.Text = row.Cells["Tháng"].Value.ToString();
                nam.Text = row.Cells["Năm"].Value.ToString();
                

               
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string a, b, c;
            a=manv.Text.Trim();
            b=nam.Text.Trim();
            c=thang.Text.Trim();
            DateTime now = DateTime.Now;
            if (manv.Text == "" || thang.Text == "" || nam.Text == "")
            {
                MessageBox.Show("vui lòng nhập đủ thông số để thanh toán ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else  if(int.Parse(b)==now.Year&&now.Month== int.Parse(c))
            {

                MessageBox.Show("hiện tại tháng này chưa hết ngày nên không thanh toán được ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (!tonSQL.thanhtoanchua(a,b,c))
            {
                MessageBox.Show("nhân viên này đã thanh toán rồi ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;

            }
            else
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thanh toán cho  nhân viên này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    tonSQL.thanhtoanchonv(a,b,c);  
                    MessageBox.Show("thanh toán thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Cập nhật lại danh sách sau khi sửa
                    DataTable dt = tonSQL.laydanhthanhtoan();
                    loaddata(dt);
                }


            }
        }
    }
}
