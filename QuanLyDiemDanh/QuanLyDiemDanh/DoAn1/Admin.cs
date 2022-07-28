using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoAn1.Core;
using System.Data.SqlClient;

namespace DoAn1
{
    public partial class Admin : Form
    {
        DBManager db = null;
        SqlConnection conn = null;
        DataTable t;
        public Admin()
        {
            InitializeComponent();
            db = new DBManager();
            conn = db.KetNoi();
        }
        public void FillCombox()
        {
            cbLopHocPhan.DataSource = db.ds_Lop();
            cbLopHocPhan.DisplayMember = "MaLop";
            cbLopHocPhan.ValueMember = "MaLop";
        }
        private void Admin_Load(object sender, EventArgs e)
        {
            FillCombox();
            cbHinhThuc.Items.Add("Lý thuyết");
            cbHinhThuc.Items.Add("Thực hành");
        }
        // Đăng nhập
        private void btnDanXuat_Click(object sender, EventArgs e)
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

        // Thêm tài khoản
        private void BtnTaiKhoan_Click(object sender, EventArgs e)
        {
            ThemTaiKhoan Tk = new ThemTaiKhoan();
            Tk.Show();
            Hide();
        }
        // Thêm giảng viên
        private void btnGiangVien_Click(object sender, EventArgs e)
        {
            ThemGiangVien Gv = new ThemGiangVien();
            Gv.Show();
            Hide();
        }

        private void btnChon_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (cbHinhThuc.SelectedItem == "Lý thuyết")
            {
                string malop = cbLopHocPhan.SelectedValue.ToString();
                string sql = "SELECT sv.MSSV, sv.HoDem, sv.Ten, sv.LopHoc, COUNT(ct.MaDiemDanh) " +
                             "FROM DIEMDANHCT ct, DIEMDANH dd, SINHVIEN sv, LOPHOCPHAN lh " +
                             "WHERE lh.MaLop = dd.MaLop AND ct.MaDiemDanh = dd.MaDiemDanh AND " +
                             "sv.MSSV = dd.MSSV AND lh.MaLop = @malop AND dd.HinhThuc = 1 " +
                             "GROUP BY sv.MSSV, sv.HoDem, sv.Ten, sv.LopHoc " +
                             "ORDER BY sv.Ten";

                SqlCommand cmd = new SqlCommand(sql);
                cmd.Connection = conn;

                cmd.Parameters.Add(new SqlParameter("@malop", SqlDbType.VarChar, 15));
                cmd.Parameters["@malop"].Value = malop;

                SqlDataReader r = cmd.ExecuteReader();
                t = new DataTable();
                t.Load(r);
                viewDanhSach.DataSource = t;
                viewDanhSach.AutoResizeColumn(0);
                // Họ đệm
                viewDanhSach.Columns[1].HeaderText = "Họ đệm";
                viewDanhSach.Columns[1].Width = 135;
                // Tên
                viewDanhSach.Columns[2].HeaderText = "Tên";
                viewDanhSach.Columns[2].Width = 60;
                viewDanhSach.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                // Lớp
                viewDanhSach.AutoResizeColumn(3);
                viewDanhSach.Columns[3].HeaderText = "Lớp";
                // Tham gia
                viewDanhSach.Columns[4].HeaderText = "Số buổi tham gia";
                viewDanhSach.Columns[4].Width = 90;
                viewDanhSach.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            // Hình thức
            if (cbHinhThuc.SelectedItem == "Thực hành")
            {
                string malop = cbLopHocPhan.SelectedValue.ToString();
                string sql = "SELECT sv.MSSV, sv.HoDem, sv.Ten, sv.LopHoc, sv.NhomTH, COUNT(ct.MaDiemDanh) " +
                             "FROM DIEMDANHCT ct, DIEMDANH dd, SINHVIEN sv, LOPHOCPHAN lh " +
                             "WHERE lh.MaLop = dd.MaLop AND ct.MaDiemDanh = dd.MaDiemDanh AND " +
                             "sv.MSSV = dd.MSSV AND lh.MaLop = @malop AND dd.HinhThuc = 2 " +
                             "GROUP BY sv.MSSV, sv.HoDem, sv.Ten, sv.LopHoc, sv.NhomTH";

                SqlCommand cmd = new SqlCommand(sql);
                cmd.Connection = conn;

                cmd.Parameters.Add(new SqlParameter("@malop", SqlDbType.VarChar, 15));
                cmd.Parameters["@malop"].Value = malop;

                SqlDataReader r = cmd.ExecuteReader();
                t = new DataTable();
                t.Load(r);
                viewDanhSach.DataSource = t;
                // MSSV
                viewDanhSach.AutoResizeColumn(0);
                // Họ đệm
                viewDanhSach.Columns[1].HeaderText = "Họ đệm";
                viewDanhSach.Columns[1].Width = 135;
                // Tên
                viewDanhSach.Columns[2].HeaderText = "Tên";
                viewDanhSach.Columns[2].Width = 60;
                viewDanhSach.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                // Lớp
                viewDanhSach.AutoResizeColumn(3);
                viewDanhSach.Columns[3].HeaderText = "Lớp";
                // Nhóm
                viewDanhSach.Columns[4].HeaderText = "Nhóm";
                viewDanhSach.Columns[4].Width = 40;
                viewDanhSach.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                // Số buổi tham gia                                             
                viewDanhSach.Columns[5].HeaderText = "Số buổi tham gia";
                viewDanhSach.Columns[5].Width = 90;
                viewDanhSach.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
    }
}
