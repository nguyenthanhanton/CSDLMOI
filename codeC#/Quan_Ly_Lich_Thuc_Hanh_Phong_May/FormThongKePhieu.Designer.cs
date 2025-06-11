namespace Quan_Ly_Lich_Thuc_Hanh_Phong_May
{
    partial class FormThongKePhieu
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
            this.panelPTT = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbTenNV = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.dataGridViewPTT = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbNam = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbThang = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panelPTT.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPTT)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelPTT
            // 
            this.panelPTT.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panelPTT.Controls.Add(this.groupBox3);
            this.panelPTT.Controls.Add(this.button2);
            this.panelPTT.Controls.Add(this.btnReset);
            this.panelPTT.Controls.Add(this.dataGridViewPTT);
            this.panelPTT.Controls.Add(this.groupBox2);
            this.panelPTT.Controls.Add(this.groupBox1);
            this.panelPTT.Controls.Add(this.label2);
            this.panelPTT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPTT.Location = new System.Drawing.Point(0, 0);
            this.panelPTT.Name = "panelPTT";
            this.panelPTT.Size = new System.Drawing.Size(1694, 864);
            this.panelPTT.TabIndex = 2;
            this.panelPTT.Paint += new System.Windows.Forms.PaintEventHandler(this.panelPTT_Paint);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox3.Controls.Add(this.cbTenNV);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(980, 28);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(238, 64);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tên nhân viên";
            // 
            // cbTenNV
            // 
            this.cbTenNV.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTenNV.FormattingEnabled = true;
            this.cbTenNV.Location = new System.Drawing.Point(6, 29);
            this.cbTenNV.Name = "cbTenNV";
            this.cbTenNV.Size = new System.Drawing.Size(216, 28);
            this.cbTenNV.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(1202, 634);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(152, 39);
            this.button2.TabIndex = 18;
            this.button2.Text = "Thống kê";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(1389, 634);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(167, 39);
            this.btnReset.TabIndex = 17;
            this.btnReset.Text = "Load dữ liệu";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // dataGridViewPTT
            // 
            this.dataGridViewPTT.AllowUserToAddRows = false;
            this.dataGridViewPTT.AllowUserToDeleteRows = false;
            this.dataGridViewPTT.AllowUserToResizeColumns = false;
            this.dataGridViewPTT.AllowUserToResizeRows = false;
            this.dataGridViewPTT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPTT.Location = new System.Drawing.Point(150, 113);
            this.dataGridViewPTT.Name = "dataGridViewPTT";
            this.dataGridViewPTT.RowHeadersWidth = 51;
            this.dataGridViewPTT.RowTemplate.Height = 24;
            this.dataGridViewPTT.Size = new System.Drawing.Size(1406, 495);
            this.dataGridViewPTT.TabIndex = 16;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox2.Controls.Add(this.cbNam);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(1421, 28);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(135, 64);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Năm";
            // 
            // cbNam
            // 
            this.cbNam.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbNam.FormattingEnabled = true;
            this.cbNam.Location = new System.Drawing.Point(6, 29);
            this.cbNam.Name = "cbNam";
            this.cbNam.Size = new System.Drawing.Size(118, 28);
            this.cbNam.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox1.Controls.Add(this.cbThang);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(1266, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(132, 64);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tháng";
            // 
            // cbThang
            // 
            this.cbThang.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbThang.FormattingEnabled = true;
            this.cbThang.Location = new System.Drawing.Point(6, 29);
            this.cbThang.Name = "cbThang";
            this.cbThang.Size = new System.Drawing.Size(118, 28);
            this.cbThang.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(143, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(480, 41);
            this.label2.TabIndex = 13;
            this.label2.Text = "LỊCH TRỰC PHÒNG MÁY THÁNG";
            // 
            // FormThongKePhieu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1694, 864);
            this.Controls.Add(this.panelPTT);
            this.Name = "FormThongKePhieu";
            this.Text = "FormThongKePhieu";
            this.Load += new System.EventHandler(this.FormThongKePhieu_Load);
            this.panelPTT.ResumeLayout(false);
            this.panelPTT.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPTT)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelPTT;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cbTenNV;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.DataGridView dataGridViewPTT;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbNam;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbThang;
        private System.Windows.Forms.Label label2;
    }
}