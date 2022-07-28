using DoAn1.Core;
using GemBox.Spreadsheet;
using GemBox.Spreadsheet.WinFormsUtilities;
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
    public partial class ThemDanhSach : Form
    {
        // lưu danh sách
        List<String> to = new List<String>();
        //Tạo biến Int để gắn cờ hiệu
        int flag = 0;
        String toStr = "";

        DBManager db = null;
        SqlConnection conn = null;
        public ThemDanhSach()
        {
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY"); 
            InitializeComponent();
            viewDanhSach.MultiSelect = true;
            db = new DBManager();
            conn = db.KetNoi();

        }
        // Hiển thị thông tin trên Combobox
        public void FillCombobox()
        {
            cbHocPhan.DataSource = db.ds_Lop();
            cbHocPhan.DisplayMember = "MaLop";
            cbHocPhan.ValueMember = "MaLop";
        }
        public void taidulieu()
        {
            viewDanhSach.DataSource = db.ds_SinhVien();
            viewDanhSach.AutoResizeColumn(0);
            viewDanhSach.AutoResizeColumn(2);
            viewDanhSach.AutoResizeColumn(5);

            viewDanhSach.Columns[2].HeaderText = "Họ đệm";
            viewDanhSach.Columns[3].HeaderText = "Tên";
            viewDanhSach.Columns[4].HeaderText = "Lớp";
            viewDanhSach.Columns[5].HeaderText = "Nhóm thực hành";
        }
        private void ThemDanhSach_Load(object sender, EventArgs e)
        {
            taidulieu();
            FillCombobox();
        }
        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Hide();
            GiaoDienHeThong GiaoDien = new GiaoDienHeThong();
            GiaoDien.Show();
        }
        private void btnThemExcel_Click(object sender, EventArgs e)
        {

            var chooseFile = new OpenFileDialog();
            chooseFile.Filter = "Excel Workbook|*.xlsx";
            viewDanhSach.DataSource = null;
            viewDanhSach.Rows.Clear();
            if (chooseFile.ShowDialog() == DialogResult.OK)
            {
                var danhsach = ExcelFile.Load(chooseFile.FileName);

                DataGridViewConverter.ExportToDataGridView
                                      (danhsach.Worksheets.ActiveWorksheet, this.viewDanhSach,
                                      new ExportToDataGridViewOptions() { ColumnHeaders = true });
            }
        }
        // Nút thêm trên giao diện
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (this.txtMSSV.Text.Equals("") || this.txtLop.Text.Equals("") || this.txtHoDem.Text.Equals("")
                || this.txtTen.Text.Equals(""))
            {
                MessageBox.Show("Không được để thông tin trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (!db.KiemTraMSSV(txtMSSV.Text))
                {
                    string Them_SV = "INSERT INTO SINHVIEN (MSSV,HoDem,Ten,LopHoc,NhomTH) "
                                     + "VALUES (@mssv,@hodem,@ten,@lophoc,@nhomth);";
                    SqlCommand cmd = new SqlCommand(Them_SV);
                    cmd.Connection = conn;

                    cmd.Parameters.Add(new SqlParameter("@mssv", SqlDbType.VarChar, 10));
                    cmd.Parameters["@mssv"].Value = this.txtMSSV.Text;

                    cmd.Parameters.Add(new SqlParameter("@hodem", SqlDbType.NVarChar, 50));
                    cmd.Parameters["@hodem"].Value = this.txtHoDem.Text;

                    cmd.Parameters.Add(new SqlParameter("@ten", SqlDbType.NVarChar, 10));
                    cmd.Parameters["@ten"].Value = this.txtTen.Text;

                    cmd.Parameters.Add(new SqlParameter("@lophoc", SqlDbType.VarChar, 15));
                    cmd.Parameters["@lophoc"].Value = this.txtLop.Text;

                    cmd.Parameters.Add(new SqlParameter("@nhomth", SqlDbType.Int));
                    cmd.Parameters["@nhomth"].Value = this.txtTH.Text;

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã thêm thông tin");
                    taidulieu();
                    conn.Close();
                }
                else
                {
                    MessageBox.Show("Mã sinh viên đã trùng");
                }
            }
        }      
        // Sửa danh sách
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (this.txtMSSV.Text.Equals("") || this.txtLop.Text.Equals("") || this.txtHoDem.Text.Equals("")
                || this.txtTen.Text.Equals(""))
            {
                MessageBox.Show("Không được để thông tin trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string Sua_DS = "UPDATE SINHVIEN SET HoDem = @hodem,Ten = @ten,LopHoc = @lophoc,NhomTH = @nhomth "
                              + "WHERE MSSV = @mssv ";
                SqlCommand cmd = new SqlCommand(Sua_DS);
                cmd.Connection = conn;

                cmd.Parameters.Add(new SqlParameter("@mssv", SqlDbType.VarChar, 10));
                cmd.Parameters["@mssv"].Value = this.txtMSSV.Text;

                cmd.Parameters.Add(new SqlParameter("@hodem", SqlDbType.NVarChar, 50));
                cmd.Parameters["@hodem"].Value = this.txtHoDem.Text;

                cmd.Parameters.Add(new SqlParameter("@ten", SqlDbType.NVarChar, 10));
                cmd.Parameters["@ten"].Value = this.txtTen.Text;

                cmd.Parameters.Add(new SqlParameter("@lophoc", SqlDbType.VarChar, 15));
                cmd.Parameters["@lophoc"].Value = this.txtLop.Text;

                cmd.Parameters.Add(new SqlParameter("@nhomth", SqlDbType.Int));
                cmd.Parameters["@nhomth"].Value = this.txtTH.Text;

                cmd.ExecuteNonQuery();
                MessageBox.Show("Cập nhật thông tin thành công");
                taidulieu();
                conn.Close();
            }
        }
        //Xóa thông tin sinh viên
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            DialogResult ThongBao = MessageBox.Show("Bạn có chắn muốn xóa sinh viên này ra khỏi danh sách", "Thông báo",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (ThongBao == DialogResult.Yes)
            {
                string Xoa = "DELETE FROM SINHVIEN WHERE MSSV = @mssv";
                SqlCommand cmd = new SqlCommand(Xoa);
                cmd.Connection = conn;

                cmd.Parameters.Add(new SqlParameter("@mssv", SqlDbType.VarChar, 10));
                cmd.Parameters["@mssv"].Value = this.txtMSSV.Text;
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã xóa thành công");
                    taidulieu();
                    this.txtMSSV.Enabled = true;
                    this.txtHoDem.Text = "";
                    this.txtTen.Text = "";
                    this.txtLop.Text = "";
                    this.txtTH.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        //Lưu danh sách excel
        private void btnLuu_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (DataGridViewRow dgr in viewDanhSach.SelectedRows.Cast<DataGridViewRow>().Reverse())
            {
                to.Add(dgr.Cells[1].Value.ToString());
                toStr = String.Join(";", to);
               if (!db.KiemTraMSSV(dgr.Cells[1].Value.ToString()))
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    string Them = "INSERT INTO SINHVIEN " + "VALUES(@ma,@hodem,@ten,@lop,@nhomth)";
                    SqlCommand cmd = new SqlCommand(Them);
                    cmd.Connection = conn;

                    cmd.Parameters.Add(new SqlParameter("@ma", SqlDbType.VarChar, 10));
                    cmd.Parameters["@ma"].Value = dgr.Cells[1].Value.ToString();

                    cmd.Parameters.Add(new SqlParameter("@hodem", SqlDbType.NVarChar, 50));
                    cmd.Parameters["@hodem"].Value = dgr.Cells[2].Value.ToString();

                    cmd.Parameters.Add(new SqlParameter("@ten", SqlDbType.NVarChar, 10));
                    cmd.Parameters["@ten"].Value = dgr.Cells[3].Value.ToString();

                    cmd.Parameters.Add(new SqlParameter("@lop", SqlDbType.VarChar, 15));
                    cmd.Parameters["@lop"].Value = dgr.Cells[4].Value.ToString();

                    cmd.Parameters.Add(new SqlParameter("@nhomth", SqlDbType.Int));
                    cmd.Parameters["@nhomth"].Value = dgr.Cells[5].Value.ToString();
                    cmd.ExecuteNonQuery();
                }
                //Thêm vào bảng điểm danh
                 if (!db.KiemTraLopDD(cbHocPhan.SelectedValue.ToString(), dgr.Cells[1].Value.ToString()))
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    string LyThuyet = "INSERT INTO DIEMDANH " +
                                     "VALUES(@malop,@mssv, @hinhthuc)";
                    SqlCommand cmd1 = new SqlCommand(LyThuyet);
                    cmd1.Connection = conn;

                    cmd1.Parameters.Add(new SqlParameter("@malop", SqlDbType.VarChar, 15));
                    cmd1.Parameters["@malop"].Value = cbHocPhan.SelectedValue;

                    cmd1.Parameters.Add(new SqlParameter("@mssv", SqlDbType.VarChar, 10));
                    cmd1.Parameters["@mssv"].Value = dgr.Cells[1].Value.ToString();               

                    cmd1.Parameters.Add(new SqlParameter("@ghichu", SqlDbType.NVarChar, 60));
                    cmd1.Parameters["@ghichu"].Value = DBNull.Value;
     
                    cmd1.Parameters.Add(new SqlParameter("@hinhthuc", SqlDbType.NVarChar, 50));
                    cmd1.Parameters["@hinhthuc"].Value = 1;

                    cmd1.ExecuteNonQuery();

                    string ThucHanh = "INSERT INTO DIEMDANH " +
                                     "VALUES(@malop,@mssv, @hinhthuc)";
                    SqlCommand cmd2 = new SqlCommand(ThucHanh);
                    cmd2.Connection = conn;

                    cmd2.Parameters.Add(new SqlParameter("@malop", SqlDbType.VarChar, 15));
                    cmd2.Parameters["@malop"].Value = cbHocPhan.SelectedValue;

                    cmd2.Parameters.Add(new SqlParameter("@mssv", SqlDbType.VarChar, 10));
                    cmd2.Parameters["@mssv"].Value = dgr.Cells[1].Value.ToString();

                    cmd2.Parameters.Add(new SqlParameter("@ghichu", SqlDbType.NVarChar, 60));
                    cmd2.Parameters["@ghichu"].Value = DBNull.Value;

                    cmd2.Parameters.Add(new SqlParameter("@hinhthuc", SqlDbType.NVarChar, 50));
                    cmd2.Parameters["@hinhthuc"].Value = 2;

                    cmd2.ExecuteNonQuery();
                    //    //Tăng giá trị khi đã thêm thành công
                    flag++;
                    conn.Close();
                }
                else
                {
                    MessageBox.Show("Sinh viên này đã tồn tại");
                    flag = 0;
                    break;
                }
            }
            //Xét điều kiện cho cờ hiệu
            if (flag != 0)
            {
                MessageBox.Show("Đã thêm thành công " + flag + " sinh viên");
            }
        }
        //Hiển thị thông tin lên Textbox
        private void viewDanhSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMSSV.Text = viewDanhSach.CurrentRow.Cells[1].Value.ToString();
            txtHoDem.Text = viewDanhSach.CurrentRow.Cells[2].Value.ToString();
            txtTen.Text = viewDanhSach.CurrentRow.Cells[3].Value.ToString();
            txtLop.Text = viewDanhSach.CurrentRow.Cells[4].Value.ToString();
            txtTH.Text = viewDanhSach.CurrentRow.Cells[5].Value.ToString();
            txtMSSV.Enabled = false;
        }

        private void txtTH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            this.txtMSSV.Enabled = true;
            this.txtMSSV.Text = "";
            this.txtHoDem.Text = "";
            this.txtTen.Text = "";
            this.txtLop.Text = "";
            this.txtTH.Text = "";
        }
    }
}
