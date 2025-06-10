namespace Quan_Ly_Lich_Thuc_Hanh_Phong_May
{
    partial class form_hieu_GVPM
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel_QLLTHPM = new System.Windows.Forms.Panel();
            this.btn_PM = new System.Windows.Forms.Button();
            this.btn_GV = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.dgv_GV = new System.Windows.Forms.DataGridView();
            this.MaGV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenGV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DcGV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SdtGV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmailGV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NsGV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GtGV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_THEM_GVPM = new System.Windows.Forms.Button();
            this.btn_XOA_GVPM = new System.Windows.Forms.Button();
            this.btn_SUA_GVPM = new System.Windows.Forms.Button();
            this.btn_TK_GVPM = new System.Windows.Forms.Button();
            this.dt_NSGV = new System.Windows.Forms.DateTimePicker();
            this.rb_GVNam = new System.Windows.Forms.RadioButton();
            this.rb_GVNu = new System.Windows.Forms.RadioButton();
            this.txt_EMAILGV = new System.Windows.Forms.TextBox();
            this.txt_SDTGV = new System.Windows.Forms.TextBox();
            this.txt_TenGV = new System.Windows.Forms.TextBox();
            this.txt_DCGV = new System.Windows.Forms.TextBox();
            this.lbl_TK = new System.Windows.Forms.Label();
            this.txt_TK = new System.Windows.Forms.TextBox();
            this.dgv_PM = new System.Windows.Forms.DataGridView();
            this.MaPM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenPM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoLM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiaDiem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_ThemPM = new System.Windows.Forms.Button();
            this.btn_SuaPM = new System.Windows.Forms.Button();
            this.btn_XoaPM = new System.Windows.Forms.Button();
            this.btn_TKPM = new System.Windows.Forms.Button();
            this.txt_TenPM = new System.Windows.Forms.TextBox();
            this.txt_SoLM = new System.Windows.Forms.TextBox();
            this.txt_DiaDiem = new System.Windows.Forms.TextBox();
            this.txt_TKPM = new System.Windows.Forms.TextBox();
            this.gb_MaGV = new System.Windows.Forms.GroupBox();
            this.txt_MaPM = new System.Windows.Forms.TextBox();
            this.txt_MaGV = new System.Windows.Forms.TextBox();
            this.panel_GV = new System.Windows.Forms.Panel();
            this.gb_GTGV = new System.Windows.Forms.GroupBox();
            this.gb_NSGV = new System.Windows.Forms.GroupBox();
            this.gb_DCGV = new System.Windows.Forms.GroupBox();
            this.gb_EmailGV = new System.Windows.Forms.GroupBox();
            this.gb_TenGV = new System.Windows.Forms.GroupBox();
            this.gb_SDTGV = new System.Windows.Forms.GroupBox();
            this.panel_QLLTHPM.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_GV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_PM)).BeginInit();
            this.gb_MaGV.SuspendLayout();
            this.panel_GV.SuspendLayout();
            this.gb_GTGV.SuspendLayout();
            this.gb_NSGV.SuspendLayout();
            this.gb_DCGV.SuspendLayout();
            this.gb_EmailGV.SuspendLayout();
            this.gb_TenGV.SuspendLayout();
            this.gb_SDTGV.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_QLLTHPM
            // 
            this.panel_QLLTHPM.BackColor = System.Drawing.Color.LightBlue;
            this.panel_QLLTHPM.Controls.Add(this.btn_PM);
            this.panel_QLLTHPM.Controls.Add(this.btn_GV);
            this.panel_QLLTHPM.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_QLLTHPM.Location = new System.Drawing.Point(0, 0);
            this.panel_QLLTHPM.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel_QLLTHPM.Name = "panel_QLLTHPM";
            this.panel_QLLTHPM.Size = new System.Drawing.Size(1408, 82);
            this.panel_QLLTHPM.TabIndex = 3;
            // 
            // btn_PM
            // 
            this.btn_PM.Location = new System.Drawing.Point(1003, 4);
            this.btn_PM.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_PM.Name = "btn_PM";
            this.btn_PM.Size = new System.Drawing.Size(241, 75);
            this.btn_PM.TabIndex = 5;
            this.btn_PM.Text = "Phòng máy";
            this.btn_PM.UseVisualStyleBackColor = true;
            this.btn_PM.Click += new System.EventHandler(this.btn_PM_Click);
            // 
            // btn_GV
            // 
            this.btn_GV.Location = new System.Drawing.Point(751, 4);
            this.btn_GV.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_GV.Name = "btn_GV";
            this.btn_GV.Size = new System.Drawing.Size(241, 75);
            this.btn_GV.TabIndex = 0;
            this.btn_GV.Text = "Giảng viên";
            this.btn_GV.UseVisualStyleBackColor = true;
            this.btn_GV.Click += new System.EventHandler(this.btn_GV_Click);
            // 
            // dgv_GV
            // 
            this.dgv_GV.AllowUserToDeleteRows = false;
            this.dgv_GV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_GV.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv_GV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_GV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaGV,
            this.TenGV,
            this.DcGV,
            this.SdtGV,
            this.EmailGV,
            this.NsGV,
            this.GtGV});
            this.dgv_GV.Location = new System.Drawing.Point(2, 357);
            this.dgv_GV.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgv_GV.Name = "dgv_GV";
            this.dgv_GV.ReadOnly = true;
            this.dgv_GV.RowHeadersWidth = 51;
            this.dgv_GV.RowTemplate.Height = 24;
            this.dgv_GV.Size = new System.Drawing.Size(1387, 510);
            this.dgv_GV.TabIndex = 0;
            this.dgv_GV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_GV_PM_CellClick);
            this.dgv_GV.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_GV_PM_CellContentClick);
            this.dgv_GV.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgv_GV_PM_DataBindingComplete);
            // 
            // MaGV
            // 
            this.MaGV.DataPropertyName = "MaGV";
            this.MaGV.HeaderText = "Mã giảng viên";
            this.MaGV.MinimumWidth = 8;
            this.MaGV.Name = "MaGV";
            this.MaGV.ReadOnly = true;
            // 
            // TenGV
            // 
            this.TenGV.DataPropertyName = "TenGV";
            this.TenGV.HeaderText = "Tên giảng viên";
            this.TenGV.MinimumWidth = 8;
            this.TenGV.Name = "TenGV";
            this.TenGV.ReadOnly = true;
            // 
            // DcGV
            // 
            this.DcGV.DataPropertyName = "DiaChi";
            this.DcGV.HeaderText = "Địa chỉ";
            this.DcGV.MinimumWidth = 8;
            this.DcGV.Name = "DcGV";
            this.DcGV.ReadOnly = true;
            // 
            // SdtGV
            // 
            this.SdtGV.DataPropertyName = "SDTGV";
            this.SdtGV.HeaderText = "Số điện thoại";
            this.SdtGV.MinimumWidth = 8;
            this.SdtGV.Name = "SdtGV";
            this.SdtGV.ReadOnly = true;
            // 
            // EmailGV
            // 
            this.EmailGV.DataPropertyName = "Email";
            this.EmailGV.HeaderText = "Email";
            this.EmailGV.MinimumWidth = 8;
            this.EmailGV.Name = "EmailGV";
            this.EmailGV.ReadOnly = true;
            // 
            // NsGV
            // 
            this.NsGV.DataPropertyName = "NgaySinh";
            this.NsGV.HeaderText = "Ngày sinh";
            this.NsGV.MinimumWidth = 8;
            this.NsGV.Name = "NsGV";
            this.NsGV.ReadOnly = true;
            // 
            // GtGV
            // 
            this.GtGV.DataPropertyName = "GioiTinh";
            this.GtGV.HeaderText = "Giới tính";
            this.GtGV.MinimumWidth = 8;
            this.GtGV.Name = "GtGV";
            this.GtGV.ReadOnly = true;
            this.GtGV.Visible = false;
            // 
            // btn_THEM_GVPM
            // 
            this.btn_THEM_GVPM.Location = new System.Drawing.Point(1148, 15);
            this.btn_THEM_GVPM.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_THEM_GVPM.Name = "btn_THEM_GVPM";
            this.btn_THEM_GVPM.Size = new System.Drawing.Size(241, 75);
            this.btn_THEM_GVPM.TabIndex = 1;
            this.btn_THEM_GVPM.Text = "Thêm";
            this.btn_THEM_GVPM.UseVisualStyleBackColor = true;
            this.btn_THEM_GVPM.Click += new System.EventHandler(this.btn_THEM_GVPM_Click);
            // 
            // btn_XOA_GVPM
            // 
            this.btn_XOA_GVPM.Location = new System.Drawing.Point(1148, 181);
            this.btn_XOA_GVPM.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_XOA_GVPM.Name = "btn_XOA_GVPM";
            this.btn_XOA_GVPM.Size = new System.Drawing.Size(241, 75);
            this.btn_XOA_GVPM.TabIndex = 4;
            this.btn_XOA_GVPM.Text = "Xoá";
            this.btn_XOA_GVPM.UseVisualStyleBackColor = true;
            this.btn_XOA_GVPM.Click += new System.EventHandler(this.btn_XOA_GVPM_Click);
            // 
            // btn_SUA_GVPM
            // 
            this.btn_SUA_GVPM.Location = new System.Drawing.Point(1148, 98);
            this.btn_SUA_GVPM.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_SUA_GVPM.Name = "btn_SUA_GVPM";
            this.btn_SUA_GVPM.Size = new System.Drawing.Size(241, 75);
            this.btn_SUA_GVPM.TabIndex = 5;
            this.btn_SUA_GVPM.Text = "Sửa";
            this.btn_SUA_GVPM.UseVisualStyleBackColor = true;
            this.btn_SUA_GVPM.Click += new System.EventHandler(this.btn_SUA_GVPM_Click);
            // 
            // btn_TK_GVPM
            // 
            this.btn_TK_GVPM.Location = new System.Drawing.Point(1148, 264);
            this.btn_TK_GVPM.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_TK_GVPM.Name = "btn_TK_GVPM";
            this.btn_TK_GVPM.Size = new System.Drawing.Size(241, 75);
            this.btn_TK_GVPM.TabIndex = 6;
            this.btn_TK_GVPM.Text = "Tìm kiếm";
            this.btn_TK_GVPM.UseVisualStyleBackColor = true;
            this.btn_TK_GVPM.Click += new System.EventHandler(this.btn_TK_GVPM_Click);
            // 
            // dt_NSGV
            // 
            this.dt_NSGV.Location = new System.Drawing.Point(31, 38);
            this.dt_NSGV.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dt_NSGV.Name = "dt_NSGV";
            this.dt_NSGV.Size = new System.Drawing.Size(307, 26);
            this.dt_NSGV.TabIndex = 24;
            this.dt_NSGV.ValueChanged += new System.EventHandler(this.dt_NSGV_ValueChanged);
            // 
            // rb_GVNam
            // 
            this.rb_GVNam.AutoSize = true;
            this.rb_GVNam.BackColor = System.Drawing.Color.Lavender;
            this.rb_GVNam.Location = new System.Drawing.Point(39, 44);
            this.rb_GVNam.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rb_GVNam.Name = "rb_GVNam";
            this.rb_GVNam.Size = new System.Drawing.Size(67, 24);
            this.rb_GVNam.TabIndex = 27;
            this.rb_GVNam.TabStop = true;
            this.rb_GVNam.Text = "Nam";
            this.rb_GVNam.UseVisualStyleBackColor = false;
            // 
            // rb_GVNu
            // 
            this.rb_GVNu.AutoSize = true;
            this.rb_GVNu.BackColor = System.Drawing.Color.Lavender;
            this.rb_GVNu.Location = new System.Drawing.Point(155, 44);
            this.rb_GVNu.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rb_GVNu.Name = "rb_GVNu";
            this.rb_GVNu.Size = new System.Drawing.Size(54, 24);
            this.rb_GVNu.TabIndex = 28;
            this.rb_GVNu.TabStop = true;
            this.rb_GVNu.Text = "Nữ";
            this.rb_GVNu.UseVisualStyleBackColor = false;
            // 
            // txt_EMAILGV
            // 
            this.txt_EMAILGV.Location = new System.Drawing.Point(8, 42);
            this.txt_EMAILGV.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_EMAILGV.Name = "txt_EMAILGV";
            this.txt_EMAILGV.Size = new System.Drawing.Size(192, 26);
            this.txt_EMAILGV.TabIndex = 30;
            this.txt_EMAILGV.TextChanged += new System.EventHandler(this.txt_EMAILGV_TextChanged);
            // 
            // txt_SDTGV
            // 
            this.txt_SDTGV.Location = new System.Drawing.Point(17, 38);
            this.txt_SDTGV.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_SDTGV.Name = "txt_SDTGV";
            this.txt_SDTGV.Size = new System.Drawing.Size(192, 26);
            this.txt_SDTGV.TabIndex = 32;
            // 
            // txt_TenGV
            // 
            this.txt_TenGV.Location = new System.Drawing.Point(16, 42);
            this.txt_TenGV.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_TenGV.Name = "txt_TenGV";
            this.txt_TenGV.Size = new System.Drawing.Size(192, 26);
            this.txt_TenGV.TabIndex = 33;
            // 
            // txt_DCGV
            // 
            this.txt_DCGV.Location = new System.Drawing.Point(16, 32);
            this.txt_DCGV.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_DCGV.Name = "txt_DCGV";
            this.txt_DCGV.Size = new System.Drawing.Size(192, 26);
            this.txt_DCGV.TabIndex = 36;
            // 
            // lbl_TK
            // 
            this.lbl_TK.AutoSize = true;
            this.lbl_TK.Location = new System.Drawing.Point(738, 288);
            this.lbl_TK.Name = "lbl_TK";
            this.lbl_TK.Size = new System.Drawing.Size(131, 20);
            this.lbl_TK.TabIndex = 37;
            this.lbl_TK.Text = "Nhập để tìm kiếm";
            // 
            // txt_TK
            // 
            this.txt_TK.Location = new System.Drawing.Point(875, 285);
            this.txt_TK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_TK.Name = "txt_TK";
            this.txt_TK.Size = new System.Drawing.Size(192, 26);
            this.txt_TK.TabIndex = 38;
            this.txt_TK.TextChanged += new System.EventHandler(this.txt_TK_TextChanged);
            // 
            // dgv_PM
            // 
            this.dgv_PM.AllowUserToDeleteRows = false;
            this.dgv_PM.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_PM.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv_PM.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_PM.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaPM,
            this.TenPM,
            this.SoLM,
            this.DiaDiem});
            this.dgv_PM.Location = new System.Drawing.Point(2, 357);
            this.dgv_PM.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgv_PM.Name = "dgv_PM";
            this.dgv_PM.ReadOnly = true;
            this.dgv_PM.RowHeadersWidth = 51;
            this.dgv_PM.RowTemplate.Height = 24;
            this.dgv_PM.Size = new System.Drawing.Size(1387, 510);
            this.dgv_PM.TabIndex = 39;
            this.dgv_PM.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_PM_CellClick);
            // 
            // MaPM
            // 
            this.MaPM.DataPropertyName = "MaPhong";
            this.MaPM.HeaderText = "Mã phòng máy";
            this.MaPM.MinimumWidth = 8;
            this.MaPM.Name = "MaPM";
            this.MaPM.ReadOnly = true;
            // 
            // TenPM
            // 
            this.TenPM.DataPropertyName = "TenPhong";
            this.TenPM.HeaderText = "Tên phòng máy";
            this.TenPM.MinimumWidth = 8;
            this.TenPM.Name = "TenPM";
            this.TenPM.ReadOnly = true;
            // 
            // SoLM
            // 
            this.SoLM.DataPropertyName = "SoLuongMay";
            this.SoLM.HeaderText = "Số lượng máy";
            this.SoLM.MinimumWidth = 8;
            this.SoLM.Name = "SoLM";
            this.SoLM.ReadOnly = true;
            // 
            // DiaDiem
            // 
            this.DiaDiem.DataPropertyName = "DiaDiem";
            this.DiaDiem.HeaderText = "Địa điểm";
            this.DiaDiem.MinimumWidth = 8;
            this.DiaDiem.Name = "DiaDiem";
            this.DiaDiem.ReadOnly = true;
            // 
            // btn_ThemPM
            // 
            this.btn_ThemPM.Location = new System.Drawing.Point(1111, 15);
            this.btn_ThemPM.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_ThemPM.Name = "btn_ThemPM";
            this.btn_ThemPM.Size = new System.Drawing.Size(241, 75);
            this.btn_ThemPM.TabIndex = 40;
            this.btn_ThemPM.Text = "Thêm";
            this.btn_ThemPM.UseVisualStyleBackColor = true;
            this.btn_ThemPM.Click += new System.EventHandler(this.btn_ThemPM_Click);
            // 
            // btn_SuaPM
            // 
            this.btn_SuaPM.Location = new System.Drawing.Point(1111, 98);
            this.btn_SuaPM.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_SuaPM.Name = "btn_SuaPM";
            this.btn_SuaPM.Size = new System.Drawing.Size(241, 75);
            this.btn_SuaPM.TabIndex = 41;
            this.btn_SuaPM.Text = "Sửa";
            this.btn_SuaPM.UseVisualStyleBackColor = true;
            this.btn_SuaPM.Click += new System.EventHandler(this.btn_SuaPM_Click);
            // 
            // btn_XoaPM
            // 
            this.btn_XoaPM.Location = new System.Drawing.Point(1111, 181);
            this.btn_XoaPM.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_XoaPM.Name = "btn_XoaPM";
            this.btn_XoaPM.Size = new System.Drawing.Size(241, 75);
            this.btn_XoaPM.TabIndex = 42;
            this.btn_XoaPM.Text = "Xoá";
            this.btn_XoaPM.UseVisualStyleBackColor = true;
            this.btn_XoaPM.Click += new System.EventHandler(this.btn_XoaPM_Click);
            // 
            // btn_TKPM
            // 
            this.btn_TKPM.Location = new System.Drawing.Point(1111, 264);
            this.btn_TKPM.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_TKPM.Name = "btn_TKPM";
            this.btn_TKPM.Size = new System.Drawing.Size(241, 75);
            this.btn_TKPM.TabIndex = 43;
            this.btn_TKPM.Text = "Tìm kiếm";
            this.btn_TKPM.UseVisualStyleBackColor = true;
            this.btn_TKPM.Click += new System.EventHandler(this.btn_TKPM_Click);
            // 
            // txt_TenPM
            // 
            this.txt_TenPM.Location = new System.Drawing.Point(27, 42);
            this.txt_TenPM.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_TenPM.Name = "txt_TenPM";
            this.txt_TenPM.Size = new System.Drawing.Size(192, 26);
            this.txt_TenPM.TabIndex = 45;
            // 
            // txt_SoLM
            // 
            this.txt_SoLM.Location = new System.Drawing.Point(31, 38);
            this.txt_SoLM.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_SoLM.Name = "txt_SoLM";
            this.txt_SoLM.Size = new System.Drawing.Size(192, 26);
            this.txt_SoLM.TabIndex = 46;
            // 
            // txt_DiaDiem
            // 
            this.txt_DiaDiem.Location = new System.Drawing.Point(22, 42);
            this.txt_DiaDiem.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_DiaDiem.Name = "txt_DiaDiem";
            this.txt_DiaDiem.Size = new System.Drawing.Size(192, 26);
            this.txt_DiaDiem.TabIndex = 47;
            // 
            // txt_TKPM
            // 
            this.txt_TKPM.Location = new System.Drawing.Point(886, 285);
            this.txt_TKPM.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_TKPM.Name = "txt_TKPM";
            this.txt_TKPM.Size = new System.Drawing.Size(192, 26);
            this.txt_TKPM.TabIndex = 48;
            this.txt_TKPM.TextChanged += new System.EventHandler(this.txt_TKPM_TextChanged);
            // 
            // gb_MaGV
            // 
            this.gb_MaGV.BackColor = System.Drawing.Color.LightSteelBlue;
            this.gb_MaGV.Controls.Add(this.txt_MaPM);
            this.gb_MaGV.Controls.Add(this.txt_MaGV);
            this.gb_MaGV.Location = new System.Drawing.Point(32, 26);
            this.gb_MaGV.Name = "gb_MaGV";
            this.gb_MaGV.Size = new System.Drawing.Size(238, 88);
            this.gb_MaGV.TabIndex = 49;
            this.gb_MaGV.TabStop = false;
            this.gb_MaGV.Text = "Mã giảng viên";
            this.gb_MaGV.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // txt_MaPM
            // 
            this.txt_MaPM.Location = new System.Drawing.Point(25, 38);
            this.txt_MaPM.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_MaPM.Name = "txt_MaPM";
            this.txt_MaPM.Size = new System.Drawing.Size(192, 26);
            this.txt_MaPM.TabIndex = 44;
            this.txt_MaPM.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // txt_MaGV
            // 
            this.txt_MaGV.Location = new System.Drawing.Point(14, 38);
            this.txt_MaGV.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_MaGV.Name = "txt_MaGV";
            this.txt_MaGV.Size = new System.Drawing.Size(192, 26);
            this.txt_MaGV.TabIndex = 23;
            // 
            // panel_GV
            // 
            this.panel_GV.BackColor = System.Drawing.Color.Lavender;
            this.panel_GV.Controls.Add(this.gb_GTGV);
            this.panel_GV.Controls.Add(this.gb_NSGV);
            this.panel_GV.Controls.Add(this.gb_DCGV);
            this.panel_GV.Controls.Add(this.gb_EmailGV);
            this.panel_GV.Controls.Add(this.gb_TenGV);
            this.panel_GV.Controls.Add(this.gb_SDTGV);
            this.panel_GV.Controls.Add(this.gb_MaGV);
            this.panel_GV.Controls.Add(this.txt_TKPM);
            this.panel_GV.Controls.Add(this.btn_TKPM);
            this.panel_GV.Controls.Add(this.btn_XoaPM);
            this.panel_GV.Controls.Add(this.btn_SuaPM);
            this.panel_GV.Controls.Add(this.btn_ThemPM);
            this.panel_GV.Controls.Add(this.dgv_PM);
            this.panel_GV.Controls.Add(this.txt_TK);
            this.panel_GV.Controls.Add(this.lbl_TK);
            this.panel_GV.Controls.Add(this.btn_TK_GVPM);
            this.panel_GV.Controls.Add(this.btn_SUA_GVPM);
            this.panel_GV.Controls.Add(this.btn_XOA_GVPM);
            this.panel_GV.Controls.Add(this.btn_THEM_GVPM);
            this.panel_GV.Controls.Add(this.dgv_GV);
            this.panel_GV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_GV.Location = new System.Drawing.Point(0, 82);
            this.panel_GV.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel_GV.Name = "panel_GV";
            this.panel_GV.Size = new System.Drawing.Size(1408, 968);
            this.panel_GV.TabIndex = 5;
            // 
            // gb_GTGV
            // 
            this.gb_GTGV.BackColor = System.Drawing.Color.LightSteelBlue;
            this.gb_GTGV.Controls.Add(this.rb_GVNam);
            this.gb_GTGV.Controls.Add(this.rb_GVNu);
            this.gb_GTGV.Location = new System.Drawing.Point(638, 136);
            this.gb_GTGV.Name = "gb_GTGV";
            this.gb_GTGV.Size = new System.Drawing.Size(283, 92);
            this.gb_GTGV.TabIndex = 55;
            this.gb_GTGV.TabStop = false;
            this.gb_GTGV.Text = "Giới tính";
            // 
            // gb_NSGV
            // 
            this.gb_NSGV.BackColor = System.Drawing.Color.LightSteelBlue;
            this.gb_NSGV.Controls.Add(this.dt_NSGV);
            this.gb_NSGV.Location = new System.Drawing.Point(638, 26);
            this.gb_NSGV.Name = "gb_NSGV";
            this.gb_NSGV.Size = new System.Drawing.Size(372, 88);
            this.gb_NSGV.TabIndex = 54;
            this.gb_NSGV.TabStop = false;
            this.gb_NSGV.Text = "Ngày sinh";
            // 
            // gb_DCGV
            // 
            this.gb_DCGV.BackColor = System.Drawing.Color.LightSteelBlue;
            this.gb_DCGV.Controls.Add(this.txt_DCGV);
            this.gb_DCGV.Location = new System.Drawing.Point(32, 250);
            this.gb_DCGV.Name = "gb_DCGV";
            this.gb_DCGV.Size = new System.Drawing.Size(238, 78);
            this.gb_DCGV.TabIndex = 53;
            this.gb_DCGV.TabStop = false;
            this.gb_DCGV.Text = "Địa chỉ";
            // 
            // gb_EmailGV
            // 
            this.gb_EmailGV.BackColor = System.Drawing.Color.LightSteelBlue;
            this.gb_EmailGV.Controls.Add(this.txt_DiaDiem);
            this.gb_EmailGV.Controls.Add(this.txt_EMAILGV);
            this.gb_EmailGV.Location = new System.Drawing.Point(333, 136);
            this.gb_EmailGV.Name = "gb_EmailGV";
            this.gb_EmailGV.Size = new System.Drawing.Size(243, 92);
            this.gb_EmailGV.TabIndex = 52;
            this.gb_EmailGV.TabStop = false;
            this.gb_EmailGV.Text = "Email";
            // 
            // gb_TenGV
            // 
            this.gb_TenGV.BackColor = System.Drawing.Color.LightSteelBlue;
            this.gb_TenGV.Controls.Add(this.txt_TenPM);
            this.gb_TenGV.Controls.Add(this.txt_TenGV);
            this.gb_TenGV.Location = new System.Drawing.Point(32, 136);
            this.gb_TenGV.Name = "gb_TenGV";
            this.gb_TenGV.Size = new System.Drawing.Size(238, 92);
            this.gb_TenGV.TabIndex = 51;
            this.gb_TenGV.TabStop = false;
            this.gb_TenGV.Text = "Tên giảng viên";
            this.gb_TenGV.Enter += new System.EventHandler(this.groupBox1_Enter_1);
            // 
            // gb_SDTGV
            // 
            this.gb_SDTGV.BackColor = System.Drawing.Color.LightSteelBlue;
            this.gb_SDTGV.Controls.Add(this.txt_SoLM);
            this.gb_SDTGV.Controls.Add(this.txt_SDTGV);
            this.gb_SDTGV.Location = new System.Drawing.Point(333, 26);
            this.gb_SDTGV.Name = "gb_SDTGV";
            this.gb_SDTGV.Size = new System.Drawing.Size(243, 88);
            this.gb_SDTGV.TabIndex = 50;
            this.gb_SDTGV.TabStop = false;
            this.gb_SDTGV.Text = "Số điện thoại";
            // 
            // form_hieu_GVPM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1408, 1050);
            this.Controls.Add(this.panel_GV);
            this.Controls.Add(this.panel_QLLTHPM);
            this.Name = "form_hieu_GVPM";
            this.Text = "Giảng Viên - Phòng Máy";
            this.Load += new System.EventHandler(this.form_hieu_GVPM_Load);
            this.panel_QLLTHPM.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_GV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_PM)).EndInit();
            this.gb_MaGV.ResumeLayout(false);
            this.gb_MaGV.PerformLayout();
            this.panel_GV.ResumeLayout(false);
            this.panel_GV.PerformLayout();
            this.gb_GTGV.ResumeLayout(false);
            this.gb_GTGV.PerformLayout();
            this.gb_NSGV.ResumeLayout(false);
            this.gb_DCGV.ResumeLayout(false);
            this.gb_DCGV.PerformLayout();
            this.gb_EmailGV.ResumeLayout(false);
            this.gb_EmailGV.PerformLayout();
            this.gb_TenGV.ResumeLayout(false);
            this.gb_TenGV.PerformLayout();
            this.gb_SDTGV.ResumeLayout(false);
            this.gb_SDTGV.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_QLLTHPM;
        private System.Windows.Forms.Button btn_PM;
        private System.Windows.Forms.Button btn_GV;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.DataGridView dgv_GV;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn DcGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn SdtGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmailGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn NsGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn GtGV;
        private System.Windows.Forms.Button btn_THEM_GVPM;
        private System.Windows.Forms.Button btn_XOA_GVPM;
        private System.Windows.Forms.Button btn_SUA_GVPM;
        private System.Windows.Forms.Button btn_TK_GVPM;
        private System.Windows.Forms.DateTimePicker dt_NSGV;
        private System.Windows.Forms.RadioButton rb_GVNam;
        private System.Windows.Forms.RadioButton rb_GVNu;
        private System.Windows.Forms.TextBox txt_EMAILGV;
        private System.Windows.Forms.TextBox txt_SDTGV;
        private System.Windows.Forms.TextBox txt_TenGV;
        private System.Windows.Forms.TextBox txt_DCGV;
        private System.Windows.Forms.Label lbl_TK;
        private System.Windows.Forms.TextBox txt_TK;
        private System.Windows.Forms.DataGridView dgv_PM;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaPM;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenPM;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoLM;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiaDiem;
        private System.Windows.Forms.Button btn_ThemPM;
        private System.Windows.Forms.Button btn_SuaPM;
        private System.Windows.Forms.Button btn_XoaPM;
        private System.Windows.Forms.Button btn_TKPM;
        private System.Windows.Forms.TextBox txt_TenPM;
        private System.Windows.Forms.TextBox txt_SoLM;
        private System.Windows.Forms.TextBox txt_DiaDiem;
        private System.Windows.Forms.TextBox txt_TKPM;
        private System.Windows.Forms.GroupBox gb_MaGV;
        private System.Windows.Forms.TextBox txt_MaPM;
        private System.Windows.Forms.TextBox txt_MaGV;
        private System.Windows.Forms.Panel panel_GV;
        private System.Windows.Forms.GroupBox gb_SDTGV;
        private System.Windows.Forms.GroupBox gb_TenGV;
        private System.Windows.Forms.GroupBox gb_EmailGV;
        private System.Windows.Forms.GroupBox gb_DCGV;
        private System.Windows.Forms.GroupBox gb_NSGV;
        private System.Windows.Forms.GroupBox gb_GTGV;
    }
}