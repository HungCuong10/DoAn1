namespace DoAn1
{
    partial class Admin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Admin));
            this.BtnTaiKhoan = new System.Windows.Forms.Button();
            this.btnGiangVien = new System.Windows.Forms.Button();
            this.btnDanXuat = new System.Windows.Forms.Button();
            this.viewDanhSach = new System.Windows.Forms.DataGridView();
            this.cbLopHocPhan = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbHinhThuc = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnChon = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.viewDanhSach)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnTaiKhoan
            // 
            this.BtnTaiKhoan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            resources.ApplyResources(this.BtnTaiKhoan, "BtnTaiKhoan");
            this.BtnTaiKhoan.Name = "BtnTaiKhoan";
            this.BtnTaiKhoan.UseVisualStyleBackColor = false;
            this.BtnTaiKhoan.Click += new System.EventHandler(this.BtnTaiKhoan_Click);
            // 
            // btnGiangVien
            // 
            this.btnGiangVien.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            resources.ApplyResources(this.btnGiangVien, "btnGiangVien");
            this.btnGiangVien.Name = "btnGiangVien";
            this.btnGiangVien.UseVisualStyleBackColor = false;
            this.btnGiangVien.Click += new System.EventHandler(this.btnGiangVien_Click);
            // 
            // btnDanXuat
            // 
            this.btnDanXuat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            resources.ApplyResources(this.btnDanXuat, "btnDanXuat");
            this.btnDanXuat.Name = "btnDanXuat";
            this.btnDanXuat.UseVisualStyleBackColor = false;
            this.btnDanXuat.Click += new System.EventHandler(this.btnDanXuat_Click);
            // 
            // viewDanhSach
            // 
            this.viewDanhSach.AllowUserToAddRows = false;
            this.viewDanhSach.AllowUserToDeleteRows = false;
            this.viewDanhSach.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.viewDanhSach, "viewDanhSach");
            this.viewDanhSach.Name = "viewDanhSach";
            this.viewDanhSach.ReadOnly = true;
            this.viewDanhSach.RowHeadersVisible = false;
            // 
            // cbLopHocPhan
            // 
            this.cbLopHocPhan.FormattingEnabled = true;
            resources.ApplyResources(this.cbLopHocPhan, "cbLopHocPhan");
            this.cbLopHocPhan.Name = "cbLopHocPhan";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // cbHinhThuc
            // 
            this.cbHinhThuc.FormattingEnabled = true;
            resources.ApplyResources(this.cbHinhThuc, "cbHinhThuc");
            this.cbHinhThuc.Name = "cbHinhThuc";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // btnChon
            // 
            this.btnChon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            resources.ApplyResources(this.btnChon, "btnChon");
            this.btnChon.Name = "btnChon";
            this.btnChon.UseVisualStyleBackColor = false;
            this.btnChon.Click += new System.EventHandler(this.btnChon_Click);
            // 
            // Admin
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Controls.Add(this.btnChon);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbHinhThuc);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbLopHocPhan);
            this.Controls.Add(this.viewDanhSach);
            this.Controls.Add(this.btnDanXuat);
            this.Controls.Add(this.btnGiangVien);
            this.Controls.Add(this.BtnTaiKhoan);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "Admin";
            this.Load += new System.EventHandler(this.Admin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.viewDanhSach)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnTaiKhoan;
        private System.Windows.Forms.Button btnGiangVien;
        private System.Windows.Forms.Button btnDanXuat;
        private System.Windows.Forms.DataGridView viewDanhSach;
        private System.Windows.Forms.ComboBox cbLopHocPhan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbHinhThuc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnChon;
    }
}