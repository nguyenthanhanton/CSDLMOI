using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_Ly_Lich_Thuc_Hanh_Phong_May
{
    public partial class Tonform : Form
    {
        TonSQL tonSQL = new TonSQL();
        int thamso = 0;
        public Tonform()
        {
            InitializeComponent();
            tonSQL.taoketnoi(); // Mở kết nối đến cơ sở dữ liệu
            label1.Visible = false; // Ẩn nhãn label1
            label2.Visible = false; // Ẩn nhãn label1
            label3.Visible = false; // Ẩn nhãn label1
            label4.Visible = false; // Ẩn nhãn label1
            label5.Visible = false; // Ẩn nhãn label1
            lablegioitinh.Visible = false; // Ẩn nhãn label1          
            labelngaysinh.Visible = false; // Ẩn nhãn label1
            clable.Visible = false; // Ẩn nhãn label1
            t1.Visible = false; // Ẩn TextBox ban đầu
            t2.Visible = false; // Ẩn TextBox ban đầu
            t3.Visible = false; // Ẩn TextBox ban đầu
            t4.Visible = false; // Ẩn TextBox ban đầu
            t5.Visible = false; // Ẩn TextBox ban đầu
            comboBox1.Visible = false; // Ẩn ComboBox ban đầu
            radioButton1.Visible = false; // Ẩn RadioButton ban đầu
            radioButton2.Visible = false; // Ẩn RadioButton ban đầu
            dateTimePicker1.Visible = false; // Ẩn DateTimePicker ban đầu
            dataGridView1.Visible = false; // Ẩn DataGridView ban đầu

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
        private void Tonform_Load(object sender, EventArgs e)
        {
            this.Size = new Size(1050, 670); // Đặt kích thước của form
        }

        private void btn_tonnv_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            groupBox2.Visible = true;
            groupBox3.Visible = true;
            groupBox4.Visible = true;
            groupBox5.Visible = true;
            groupBox6.Visible = true;
            groupBox7.Visible = true;
            groupBox8.Visible = true;
            thamso = 1;
            label1.Visible = true; 
            label2.Visible = true; 
            label3.Visible = true; 
            label4.Visible = true; 
            label5.Visible = true; 
            lablegioitinh.Visible = true;          
            labelngaysinh.Visible = true; 
            clable.Visible = true; 
            t1.Visible = true; 
            t2.Visible = true; 
            t3.Visible = true; 
            t4.Visible = true; 
            t5.Visible = true; 
            comboBox1.Visible = true; 
            radioButton1.Visible = true;
            radioButton2.Visible = true; 
            dateTimePicker1.Visible = true; 
            dataGridView1.Visible = true; 
            dataGridView1.Visible = true; 
            label1.Text = "Mã nhân viên:";
            label2.Text = "Họ và tên:";
            label5.Text = "Địa chỉ:";
            label4.Text = "Số điện thoại:";
            label3.Text = "Email:";


            clable.Text = "Chức vụ:";
            labelngaysinh.Text = "Ngày sinh:";
            lablegioitinh.Text = "Giới tính:";
            t1.Text = "";
            t2.Text = "";
            t3.Text = "";
            t4.Text = "";
            t5.Text = "";
            comboBox1.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            DataTable dt = tonSQL.laydanhsachnhanvien();
            loaddata(dt);

            tonSQL.LayTenChucVu(comboBox1);
        }

        private void btn_toncv_Click(object sender, EventArgs e)
        {
            thamso = 2;
            groupBox1.Visible = true;
            groupBox2.Visible = true;
            groupBox4.Visible = true;
            groupBox3.Visible = false;
            groupBox6.Visible = false;
            groupBox5.Visible = false;
            groupBox7.Visible = false;
            groupBox8.Visible = false;
            dataGridView1.Visible = true;
            DataTable dt = tonSQL.laydanhsachchucvu();
            loaddata(dt);

            t1.Visible = true;
            t2.Visible = true;
            t3.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;


            t4.Visible = false; 
            t5.Visible = false;
            label4.Visible = false; 
            label5.Visible = false; 
            clable.Visible = false;
            labelngaysinh.Visible = false;
            lablegioitinh.Visible = false;
            radioButton1.Visible = false;
            radioButton2.Visible = false;
            comboBox1.Visible = false;
            dateTimePicker1.Visible = false;

            label1.Text = "Mã Chức vụ";
            label2.Text = "Tên Chức vụ";
            label3.Text = "Định mức";
            t1.Text = "";
            t2.Text = "";  
            t3.Text = "";

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (thamso == 1)
            {

                // Bỏ qua khi click tiêu đề cột
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                    t1.Text = row.Cells["Mã nhân viên"].Value.ToString();
                    t2.Text = row.Cells["Họ và Tên"].Value.ToString();
                    t3.Text = row.Cells["Email"].Value.ToString();
                    t4.Text = row.Cells["Số điện thoại"].Value.ToString();
                    t5.Text = row.Cells["Địa chỉ"].Value.ToString(); 

                    // Giới tính
                    bool gioiTinh = Convert.ToBoolean(row.Cells["Giới Tính"].Value);
                    if (gioiTinh)
                        radioButton1.Checked = true;
                    else
                        radioButton2.Checked = true;

                    // Chức vụ - gán theo tên hiển thị
                    comboBox1.Text = row.Cells["Tên Chức vụ"].Value.ToString();

                    // Ngày sinh nếu có cột
                    dateTimePicker1.Value = Convert.ToDateTime(row.Cells["Ngày sinh"].Value);
                }
            }
            else if(thamso  == 2){

                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                    t1.Text = row.Cells["Mã Chức vụ"].Value.ToString();
                    t2.Text = row.Cells["Tên Chức vụ"].Value.ToString();
                    t3.Text = row.Cells["Định mức"].Value.ToString();                 
                }


            }
        }

        private void btn_tonedit_Click(object sender, EventArgs e)
        {
            string a, b, c, d, f;
            a = t1.Text.Trim();
            b = t2.Text.Trim();
            c = t3.Text.Trim();
            d = t4.Text.Trim();
            f = t5.Text.Trim();
            DateTime k = dateTimePicker1.Value;
            bool gioitinh = radioButton1.Checked ? true : false;
            string chucvu = comboBox1.Text.Trim();
            if (thamso == 1)
            {
                if (!Regex.IsMatch(a, @"^NV\d{2}$"))
                {
                    MessageBox.Show("Mã chức vụ có mẫu NV__ với _ là 1 số ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (!tonSQL.timkiemmanv(a))
                {
                    MessageBox.Show("Mã nhân viên  không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (b == "" || b.Any(char.IsDigit))
                {
                    MessageBox.Show("Tên không được để trống hoặc có số!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (tonSQL.timemail(c,a))
                {
                    MessageBox.Show("email đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (tonSQL.timsdt(d,a))
                {

                    MessageBox.Show("Số điện thoại đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thêm nhân viên này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        tonSQL.suanhanvien(a, b, c, d, f, k, gioitinh, chucvu);
                        MessageBox.Show("thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Cập nhật lại danh sách sau khi sửa
                        DataTable dt = tonSQL.laydanhsachnhanvien();
                        loaddata(dt);

                    }


                }
            }

            if (thamso == 2)
            {
                if (!Regex.IsMatch(a, @"^CV\d{2}$"))
                {
                    MessageBox.Show("Mã chức vụ có mẫu CV__ với _ là 1 số ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (!tonSQL.timkiemmacv(a))
                {
                    MessageBox.Show("Mã chức vụ không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (b == "" && c == "")
                {
                    MessageBox.Show("Vui lòng nhập tên chức vụ và định mức!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (b != "" && tonSQL.timkiemtencv(a,b))
                {
                    MessageBox.Show("Tên chức vụ đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (c != "" && !Regex.IsMatch(c, @"^\d+$"))
                {
                    MessageBox.Show("Định mức phải là số nguyên dương!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn sửa chức vụ này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    tonSQL.suachucvu(a, b, c);
                    MessageBox.Show("Sửa chức vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Cập nhật lại danh sách sau khi sửa
                    DataTable dt = tonSQL.laydanhsachchucvu();
                    loaddata(dt);
                }
            }
        }

        private void btn_tonadd_Click(object sender, EventArgs e)
        {
            string a, b, c, d, f;
            a = t1.Text.Trim();
            b = t2.Text.Trim();
            c = t3.Text.Trim();
            d = t4.Text.Trim();
            f = t5.Text.Trim();
            DateTime k = dateTimePicker1.Value;
            bool gioitinh = radioButton1.Checked ? true : false;
            string chucvu = comboBox1.Text.Trim();
            if (thamso == 1)
            {
                if(!Regex.IsMatch(a, @"^NV\d{2}$")){
                    MessageBox.Show("Mã chức vụ có mẫu NV__ với _ là 1 số ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (tonSQL.timkiemmanv(a))
                {
                    MessageBox.Show("Mã nhân viên  đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if(b=="" || b.Any(char.IsDigit))
                {
                    MessageBox.Show("Tên không được để trống hoặc có số!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (tonSQL.timemail(c,a))
                {
                    MessageBox.Show("email đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (tonSQL.timsdt(d,a))
                {

                    MessageBox.Show("Số điện thoại đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn sửa nhân viên này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        tonSQL.themnhanvien(a, b, c, d, f, k, gioitinh, chucvu);
                        MessageBox.Show("sủa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Cập nhật lại danh sách sau khi sửa
                        DataTable dt = tonSQL.laydanhsachnhanvien();
                        loaddata(dt);

                    }


                }


            }

            if (thamso == 2)
            {
                if (!Regex.IsMatch(a, @"^CV\d{2}$"))
                {
                    MessageBox.Show("Mã chức vụ có mẫu CV__ với _ là 1 số ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (tonSQL.timkiemmacv(a))
                {
                    MessageBox.Show("Mã chức vụ đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (b == "" && c == "")
                {
                    MessageBox.Show("Vui lòng nhập tên chức vụ và định mức!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (b != "" && tonSQL.timkiemtencv(a, b))
                {
                    MessageBox.Show("Tên chức vụ đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (c != "" && !Regex.IsMatch(c, @"^\d+$"))
                {
                    MessageBox.Show("Định mức phải là số nguyên dương!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thêm chức vụ này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    tonSQL.themchucvu(a, b, c);
                    MessageBox.Show("thêm chức vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Cập nhật lại danh sách sau khi sửa
                    DataTable dt = tonSQL.laydanhsachchucvu();
                    loaddata(dt);
                }
            }

        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            string a, b, c, d, f;
            a = t1.Text.Trim();
            b = t2.Text.Trim();
            c = t3.Text.Trim();
            d = t4.Text.Trim();
            f = t5.Text.Trim();
            DateTime k = dateTimePicker1.Value;
            bool gioitinh = radioButton1.Checked ? true : false;
            string chucvu = comboBox1.Text.Trim();
            if (thamso == 1)
            {
                DataTable dt = tonSQL.laydanhsachtimkiemnv(a, b);
                loaddata(dt);

            }

            if (thamso == 2)
            {
                if (c != "" && !Regex.IsMatch(c, @"^\d+$"))
                {
                    MessageBox.Show("Định mức phải là số nguyên dương!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    
                    DataTable dt = tonSQL.laydanhsachtimkiemchucvu(a, b, c);
                    loaddata(dt);

                }
            }
        }

        private void btn_tonxoa_Click(object sender, EventArgs e)
        {
            string a, b, c, d, f;
            a = t1.Text.Trim();
            b = t2.Text.Trim();
            c = t3.Text.Trim();
            d = t4.Text.Trim();
            f = t5.Text.Trim();
     

            //  MessageBox.Show(""+t.ToString("yyyy/MM/dd"), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            if (thamso == 1)
            {
                if (!Regex.IsMatch(a, @"^NV\d{2}$"))
                {
                    MessageBox.Show("Mã chức vụ có mẫu NV__ với _ là 1 số ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (!tonSQL.timkiemmanv(a))
                {
                    MessageBox.Show("Mã nhân viên  không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (tonSQL.kiemtranhanviencophieutaitructrongtuonglaikhong(a, DateTime.Now))
                {
                    MessageBox.Show("nhân viên  còn có phiếu tại trực trong tương lại vui lòng phân công phiếu tải trực đó cho nhân viên khác để xóa nhân viên này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    tonSQL.xoanhanvien(a);
                    MessageBox.Show("xóa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Cập nhật lại danh sách sau khi sửa
                    DataTable dt = tonSQL.laydanhsachnhanvien();
                    loaddata(dt);
                }

            }
            if (thamso == 2)
            {
                if (!Regex.IsMatch(a, @"^CV\d{2}$"))
                {
                    MessageBox.Show("Mã chức vụ có mẫu CV__ với _ là 1 số ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (!tonSQL.timkiemmacv(a))
                {
                    MessageBox.Show("Mã chức vụ không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                t2.Text = "";
                t3.Text = "";
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa chức vụ này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    tonSQL.xoachucvu(a);
                    MessageBox.Show("xóa chức vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Cập nhật lại danh sách sau khi sửa
                    DataTable dt = tonSQL.laydanhsachchucvu();
                    loaddata(dt);
                }

            }
        }

        private void btn_tonthanhtoan_Click(object sender, EventArgs e)
        {
            tonthanhtoanform    tonthanhtoanform = new tonthanhtoanform();
            this.Hide();
            tonthanhtoanform.ShowDialog();
            this.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void clable_Click(object sender, EventArgs e)
        {

        }
    }
}
