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
using DoAn1.Core;

namespace DoAn1
{
    public partial class GiaoDienHeThong : Form
    {
        DBManager db = null;
        public GiaoDienHeThong()
        {
            db = new DBManager();
            InitializeComponent();
        }

        private void btnThemLop_Click(object sender, EventArgs e)
        {
            ThemLop Lop = new ThemLop();
            Lop.Show();
            this.Hide();
        }

        private void btnThemDS_Click(object sender, EventArgs e)
        {
            ThemDanhSach DanhSach = new ThemDanhSach();
            DanhSach.Show();
            this.Hide();
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            DangNhap LogIn = new DangNhap();
           DialogResult LogOut = MessageBox.Show("Bạn có chắc muốn đăng xuất hay không", "Thông báo", 
                             MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            switch (LogOut)
            {
                case DialogResult.Yes:
                    LogIn.Show();
                    this.Hide();
                    break;    
            }
        }

        private void btnQuanLyDiemDanh_Click(object sender, EventArgs e)
        {
            QuanLyDiemDanh qldd = new QuanLyDiemDanh();
            qldd.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ThongKe tk = new ThongKe();
            tk.Show();
            this.Hide();
        }
    }
}
