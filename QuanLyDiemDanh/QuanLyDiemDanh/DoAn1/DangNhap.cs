using DoAn1.Core;
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

namespace DoAn1
{
    public partial class DangNhap : Form
    {
        DBManager db = null;
        public DangNhap()
        {
            InitializeComponent();

            db = new DBManager();
            if (db.KetNoi() == null)
            {
                MessageBox.Show("Kết nối CSDL thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }
        // Xét vai trò đăng nhập
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            String sql = "SELECT * FROM DANGNHAP WHERE TaiKhoan ='"
              + txtTaiKhoan.Text + "' AND MatKhau = '" + txtMatKhau.Text + "'";
            SqlCommand cmd = new SqlCommand(sql, db.getConn());

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    String vaitro = reader.GetString(2);
                    if (vaitro.Equals("Giảng viên"))
                    {
                        GiaoDienHeThong GiaoDien = new GiaoDienHeThong();
                        GiaoDien.Show();
                        this.Hide();
                    }
                    if(vaitro.Equals("Admin"))
                    {
                        Admin ad = new Admin();
                        ad.Show();
                        this.Hide();
                    }
                }
            }
            else
            {
                MessageBox.Show(this, "Tài khoản hoặc mật khẩu của bạn không chính xác",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                reader.Close();
            }
        }
        //Thoát
        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
