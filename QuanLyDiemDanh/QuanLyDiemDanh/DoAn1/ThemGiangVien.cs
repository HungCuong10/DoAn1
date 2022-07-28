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
    public partial class ThemGiangVien : Form
    {
        DBManager db = null;
        SqlConnection conn = null;
        public ThemGiangVien()
        {
            InitializeComponent();
            db = new DBManager();
            conn = db.KetNoi();
        }
        public void taiDuLieu()
        {
            viewGiangVien.DataSource = db.ds_GiangVien();
            viewGiangVien.AutoResizeColumn(1);

            viewGiangVien.Columns[0].HeaderText = "Mã giảng viên";
            viewGiangVien.Columns[1].HeaderText = "Họ tên giảng viên";
            viewGiangVien.Columns[2].HeaderText = "Tài khoản";


        }
        private void ThemGiangVien_Load(object sender, EventArgs e)
        {
            taiDuLieu();

        }
        // Thêm thông tin giảng viên
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtTaiKhoan.Text.Equals("") || txtMaGV.Text.Equals("") || txtHoTen.Text.Equals(""))
            {
                MessageBox.Show("Không được để thông tin trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string Giang_Vien = "INSERT INTO GIANGVIEN (MaGV,HoTenGV,TaiKhoan)"
                    + "VALUES (@magv,@hoten,@tk);";
                SqlCommand cmd = new SqlCommand(Giang_Vien);

                cmd.Connection = conn;

                cmd.Parameters.Add(new SqlParameter("@magv", SqlDbType.VarChar, 10));
                cmd.Parameters["@magv"].Value = this.txtMaGV.Text;

                cmd.Parameters.Add(new SqlParameter("@hoten", SqlDbType.NVarChar, 60));
                cmd.Parameters["@hoten"].Value = this.txtHoTen.Text;

                cmd.Parameters.Add(new SqlParameter("@tk", SqlDbType.VarChar, 20));
                cmd.Parameters["@tk"].Value = this.txtTaiKhoan.Text;

                cmd.ExecuteNonQuery();
                MessageBox.Show("Đã thêm thông tin");
                taiDuLieu();
                conn.Close();
            } 
        }

        

        //Sửa thông tin giảng viên
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtTaiKhoan.Text.Equals("") || txtMaGV.Text.Equals("") || txtHoTen.Text.Equals(""))
            {
                MessageBox.Show("Không được để thông tin trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string Sua_GV = "UPDATE GIANGVIEN SET HoTenGV = @hoten, TaiKhoan = @tk " +
                                 "WHERE MaGV = @magv";
                SqlCommand cmd = new SqlCommand(Sua_GV);

                cmd.Connection = conn;

                cmd.Parameters.Add(new SqlParameter("@magv", SqlDbType.VarChar, 10));
                cmd.Parameters["@magv"].Value = this.txtMaGV.Text;

                cmd.Parameters.Add(new SqlParameter("@hoten", SqlDbType.NVarChar, 60));
                cmd.Parameters["@hoten"].Value = this.txtHoTen.Text;

                cmd.Parameters.Add(new SqlParameter("@tk", SqlDbType.VarChar, 20));
                cmd.Parameters["@tk"].Value = this.txtTaiKhoan.Text;

                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Cập nhật thông tin thành công");
                taiDuLieu();
                conn.Close();
            }
        }
        // Button Quay lại
        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin ad = new Admin();
            ad.Show();
        }
   
        private void viewGiangVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int j;
            j = viewGiangVien.CurrentRow.Index;
            this.txtMaGV.Text = viewGiangVien.Rows[j].Cells[0].Value.ToString();
            this.txtHoTen.Text = viewGiangVien.Rows[j].Cells[1].Value.ToString();
            this.txtTaiKhoan.Text = viewGiangVien.Rows[j].Cells[2].Value.ToString();
            txtMaGV.Enabled = false;
            txtTaiKhoan.Enabled = false;
        }
    }
}
