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
    public partial class ThemTaiKhoan : Form
    {
        DBManager db = null;
        SqlConnection conn = null;
        public ThemTaiKhoan()
        {
            InitializeComponent();
            db = new DBManager();
            conn = db.KetNoi();
        }

        public void taiDuLieu()
        {
            viewTaiKhoan.DataSource = db.ds_TaiKhoan();
            viewTaiKhoan.AutoResizeColumn(0);
            viewTaiKhoan.AutoResizeColumn(1);
            viewTaiKhoan.AutoResizeColumn(2);

            viewTaiKhoan.Columns[0].HeaderText = "Tài khoản";
            viewTaiKhoan.Columns[1].HeaderText = "Mật khẩu";
            viewTaiKhoan.Columns[2].HeaderText = "Vai trò";

        }

        private void ThemTaiKhoan_Load(object sender, EventArgs e)
        {
            taiDuLieu();
        }
        
        //Thêm thông tin
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtTaiKhoan.Text.Equals("") || txtMatKhau.Text.Equals("") || txtVaiTro.Text.Equals(""))
            {
                MessageBox.Show("Không được để thông tin trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string Tai_Khoan = "INSERT INTO DANGNHAP (TaiKhoan,MatKhau,VaiTro)"
                    + "VALUES (@tk,@mk,@vt);";
                SqlCommand cmd = new SqlCommand(Tai_Khoan);

                cmd.Connection = conn;

                cmd.Parameters.Add(new SqlParameter("@tk", SqlDbType.VarChar, 20));
                cmd.Parameters["@tk"].Value = this.txtTaiKhoan.Text;

                cmd.Parameters.Add(new SqlParameter("@mk", SqlDbType.VarChar, 20));
                cmd.Parameters["@mk"].Value = this.txtMatKhau.Text;

                cmd.Parameters.Add(new SqlParameter("@vt", SqlDbType.NVarChar, 60));
                cmd.Parameters["@vt"].Value = this.txtVaiTro.Text;

                cmd.ExecuteNonQuery();
                MessageBox.Show("Đã thêm thông tin");
                taiDuLieu();
                conn.Close();
            }
        }
        // Đổi thông tin
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtTaiKhoan.Text.Equals("") || txtMatKhau.Text.Equals("") || txtVaiTro.Text.Equals(""))
            {
                MessageBox.Show("Không được để thông tin trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string Sua_TK = "UPDATE DANGNHAP SET MatKhau = @mk, VaiTro = @vt " +
                                 "WHERE TaiKhoan = @tk";
                SqlCommand cmd = new SqlCommand(Sua_TK);

                cmd.Connection = conn;

                cmd.Parameters.Add(new SqlParameter("@tk", SqlDbType.VarChar, 20));
                cmd.Parameters["@tk"].Value = this.txtTaiKhoan.Text;

                cmd.Parameters.Add(new SqlParameter("@mk", SqlDbType.VarChar, 20));
                cmd.Parameters["@mk"].Value = this.txtMatKhau.Text;

                cmd.Parameters.Add(new SqlParameter("@vt", SqlDbType.NVarChar, 60));
                cmd.Parameters["@vt"].Value = this.txtVaiTro.Text;

                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Cập nhật thông tin thành công");
                taiDuLieu();
                conn.Close();
            }
        }
        // Hiển thị dữ liệu
        private void viewTaiKhoan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
        //Quay lại
        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin ad = new Admin();
            ad.Show();
        }

        private void viewTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int j;
            j = viewTaiKhoan.CurrentRow.Index;
            this.txtTaiKhoan.Text = viewTaiKhoan.Rows[j].Cells[0].Value.ToString();
            this.txtMatKhau.Text = viewTaiKhoan.Rows[j].Cells[1].Value.ToString();
            this.txtVaiTro.Text = viewTaiKhoan.Rows[j].Cells[2].Value.ToString();
            txtTaiKhoan.Enabled = false;
            conn.Close();
        }
    }
}
