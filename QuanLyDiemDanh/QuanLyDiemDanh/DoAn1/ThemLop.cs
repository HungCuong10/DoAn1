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
    public partial class ThemLop : Form
    {
        DBManager db = null;
        SqlConnection conn = null;
        public ThemLop()
        {
            InitializeComponent();
            db = new DBManager(); 
            conn = db.KetNoi();
        }
        // Combobox MÃ Giảng viên
        public void FillCombobox()
        {
            cbMaGV.DataSource = db.ds_GiangVien();
            cbMaGV.DisplayMember = "HoTenGV"; // Hiển thị ra combobox
            cbMaGV.ValueMember = "MaGV"; // Ẩn bên trong để thêm vào CSDL
        }
       // Lên kiết bảng Danh sách lớp bên CSDL vào hàm taiDuLieu
        public void taiDuLieu() 
        {          
            viewLop.DataSource = db.ds_Lop();
            //Căn chỉnh ô trong DataGridView
            viewLop.AutoResizeColumn(1);
            viewLop.AutoResizeColumn(2);

            viewLop.Columns[0].HeaderText = " Mã lớp";
            viewLop.Columns[1].HeaderText = "Tên lớp";
            viewLop.Columns[2].HeaderText = "Giảng viên";
        }
        // Tải dữ liệu và thao tác trên combobox lên giao diện Thêm lớp
        private void ThemLop_Load(object sender, EventArgs e)
        {
            taiDuLieu();
            FillCombobox();
        }
        // Quay lại
        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Hide();
            GiaoDienHeThong GiaoDien = new GiaoDienHeThong();
            GiaoDien.Show();
        }
        // Thêm lớp
        private void btnThemLop_Click(object sender, EventArgs e)
        {
            if(this.txtMaLop.Text.Equals("") || this.txtTenLop.Text.Equals("") )
            {
                MessageBox.Show("Không được để thông tin trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (!db.KiemTra(txtMaLop.Text)) // Kiểm tra trùng lặp thông tin
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    // Thêm dữ liệu vào CSDL
                    string Them_Lop = "INSERT INTO LOPHOCPHAN (MaLop,TenMonHoc,MaGV)"
                    + "VALUES (@ma,@tenmh,@magv);";
                    SqlCommand cmd = new SqlCommand(Them_Lop);

                    cmd.Connection = conn;

                    cmd.Parameters.Add(new SqlParameter("@ma", SqlDbType.VarChar, 15));
                    cmd.Parameters["@ma"].Value = this.txtMaLop.Text;

                    cmd.Parameters.Add(new SqlParameter("@tenmh", SqlDbType.NVarChar, 80));
                    cmd.Parameters["@tenmh"].Value = this.txtTenLop.Text;

                    cmd.Parameters.Add(new SqlParameter("@magv", SqlDbType.VarChar, 10));
                    cmd.Parameters["@magv"].Value = cbMaGV.SelectedValue.ToString();

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã thêm thông tin");
                    taiDuLieu();
                    conn.Close();
                }
              else
                {
                    MessageBox.Show("Lớp đã tồn tại");
                }              
            }
        }
        // Button Sửa lớp
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (this.txtMaLop.Text.Equals("") || this.txtTenLop.Text.Equals(""))
            {
                MessageBox.Show("Không được để thông tin trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                //Sửa thông tin lớp học phần
                string Sua_Lop = "UPDATE LOPHOCPHAN SET TenMonHoc = @tenmh, MaGV = @magv " +
                                     "WHERE MaLop = @ma";
                    SqlCommand cmd = new SqlCommand(Sua_Lop);
                    cmd.Connection = conn;

                    cmd.Parameters.Add(new SqlParameter("@ma", SqlDbType.VarChar, 15));
                    cmd.Parameters["@ma"].Value = this.txtMaLop.Text;

                    cmd.Parameters.Add(new SqlParameter("@tenmh", SqlDbType.NVarChar, 80));
                    cmd.Parameters["@tenmh"].Value = this.txtTenLop.Text;

                    cmd.Parameters.Add(new SqlParameter("@magv", SqlDbType.VarChar, 10));
                    cmd.Parameters["@magv"].Value = cbMaGV.SelectedValue.ToString();

                    
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cập nhật thông tin thành công");
                    taiDuLieu();
                    conn.Close();
            }
        }

        private void cbMaGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string ma = cbMaGV.SelectedValue.ToString();
            //Console.WriteLine(ma);
            //string sql = "SELECT HoTenGV " +
            //             "FROM GIANGVIEN " +
            //             "WHERE MaGV = @magv";

            //SqlCommand cmd = new SqlCommand(sql);
            //cmd.Connection = conn;

            //cmd.Parameters.Add(new SqlParameter("@magv", SqlDbType.VarChar, 10));
            //cmd.Parameters["@magv"].Value = magv;

            //using (var r = cmd.ExecuteReader())
            //{
            //    if (r.Read())
            //    {
            //        txtTenGV.Text = r.GetString(0);
            //    }
            //}
        }
        // Hiển thị thông tin lên TextBox
        private void viewLop_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaLop.Text = viewLop.CurrentRow.Cells[0].Value.ToString();
            txtTenLop.Text = viewLop.CurrentRow.Cells[1].Value.ToString();
            cbMaGV.Text = viewLop.CurrentRow.Cells[2].Value.ToString();
            txtMaLop.Enabled = false;
        }
    }
}
 
        


