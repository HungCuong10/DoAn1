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
    public partial class QuanLyDiemDanh : Form
    {        
        int flag = 0;
        int TongCong = 0;
        DBManager db = null;
        SqlConnection conn = null;
        DataTable t;
        public QuanLyDiemDanh()
        {
            InitializeComponent();
            db = new DBManager();
            conn = db.KetNoi();
            dtDiemDanh.Value = DateTime.Today;
        }
        public void FillCombox()
        {
            cbLopHoc.DataSource = db.ds_Lop();
            cbLopHoc.DisplayMember = "MaLop";
            cbLopHoc.ValueMember = "MaLop";
        }
        private void QuanLyDiemDanh_Load(object sender, EventArgs e)
        {
            FillCombox();
            cbHinhThuc.Items.Add("Lý thuyết");
            cbHinhThuc.Items.Add("Thực hành");      
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {           
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (DataGridViewRow dgr in viewDanhSach.SelectedRows.Cast<DataGridViewRow>().Reverse())
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                if (dgr.Cells[0].Value != DBNull.Value)
                {
                    string diemdanh = "INSERT INTO DIEMDANHCT VALUES(@MaDiemDanh, @NgayDiemDanh)";
                    SqlCommand cmd = new SqlCommand(diemdanh);
                    cmd.Connection = conn;

                    cmd.Parameters.Add(new SqlParameter("@MaDiemDanh", SqlDbType.Int));
                    cmd.Parameters["@MaDiemDanh"].Value = dgr.Cells[2].Value;

                    cmd.Parameters.Add(new SqlParameter("@NgayDiemDanh", SqlDbType.Date));
                    cmd.Parameters["@NgayDiemDanh"].Value = dtDiemDanh.Value;

                    cmd.ExecuteNonQuery();
                    flag++;
                    conn.Close();
                }
                TongCong++;               
            }
            if (flag != 0)
            {
                MessageBox.Show("Ghi danh " + flag + "/" + TongCong + " sinh viên");
            }
        }
        // Nút quay lại
        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Hide();
            GiaoDienHeThong GiaoDien = new GiaoDienHeThong();
            GiaoDien.Show();
        }
        // Nút chọn
        private void btnChon_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            // Lý thuyết
            if (cbHinhThuc.SelectedItem == "Lý thuyết")
            {
                string malop = cbLopHoc.SelectedValue.ToString();
                string sql = "SELECT sv.STT, dd.MaDiemDanh, dd.MSSV, sv.HoDem, sv.Ten, sv.LopHoc " +
                             "FROM SINHVIEN sv, LOPHOCPHAN lh, DIEMDANH dd " +
                             "WHERE sv.MSSV = dd.MSSV and lh.MaLop = dd.MaLop and lh.MaLop = @malop AND dd.HinhThuc = 1" +
                             " ORDER BY STT";

                SqlCommand cmd = new SqlCommand(sql);
                cmd.Connection = conn;

                cmd.Parameters.Add(new SqlParameter("@malop", SqlDbType.VarChar, 15));
                cmd.Parameters["@malop"].Value = malop;
                SqlDataReader r = cmd.ExecuteReader();
                t = new DataTable();
                t.Columns.Add(new DataColumn("Có mặt", typeof(Boolean)));
                
                t.Load(r);
                viewDanhSach.DataSource = t;
                // Có mặt
                viewDanhSach.AutoResizeColumn(0);
                // STT
                viewDanhSach.Columns[1].Width = 40;
                viewDanhSach.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                // Mã điểm danh
                viewDanhSach.Columns[2].HeaderText = "Mã điểm danh";
                viewDanhSach.Columns[2].Width = 78;
                viewDanhSach.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                // MSSV
                viewDanhSach.AutoResizeColumn(3);
                viewDanhSach.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                // Họ đệm
                viewDanhSach.Columns[4].HeaderText = "Họ đệm";
                viewDanhSach.Columns[4].Width = 130;
                // Tên
                viewDanhSach.Columns[5].HeaderText = "Tên";
                viewDanhSach.Columns[5].Width = 60;
                // Lớp
                viewDanhSach.Columns[6].HeaderText = "Lớp";
                viewDanhSach.AutoResizeColumn(6);                                                                          
            }
            // Thực hành
            if (cbHinhThuc.SelectedItem == "Thực hành")
            {
                string malop = cbLopHoc.SelectedValue.ToString();
                string sql = "SELECT sv.STT, dd.MaDiemDanh, dd.MSSV, sv.HoDem, sv.Ten, sv.LopHoc, sv.NhomTH " +
                             "FROM SINHVIEN sv, LOPHOCPHAN lh, DIEMDANH dd " +
                             "WHERE sv.MSSV = dd.MSSV and lh.MaLop = dd.MaLop and lh.MaLop = @malop AND dd.HinhThuc = 2" +
                             " ORDER BY STT";

                SqlCommand cmd = new SqlCommand(sql);
                cmd.Connection = conn;

                cmd.Parameters.Add(new SqlParameter("@malop", SqlDbType.VarChar, 15));
                cmd.Parameters["@malop"].Value = malop;
                SqlDataReader r = cmd.ExecuteReader();
                t = new DataTable();
                t.Columns.Add(new DataColumn("Có mặt", typeof(Boolean)));
                t.Load(r);
                viewDanhSach.DataSource = t;
                // Có mặt
                viewDanhSach.AutoResizeColumn(0);
                // STT
                viewDanhSach.Columns[1].Width = 40;
                viewDanhSach.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                // Mã điểm danh
                viewDanhSach.Columns[2].HeaderText = "Mã điểm danh";
                viewDanhSach.Columns[2].Width = 78; 
                viewDanhSach.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                // MSSV
                viewDanhSach.AutoResizeColumn(3);
                viewDanhSach.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                // Họ đệm
                viewDanhSach.Columns[4].HeaderText = "Họ đệm";
                viewDanhSach.Columns[4].Width = 130; 
                // Tên
                viewDanhSach.Columns[5].HeaderText = "Tên";
                viewDanhSach.Columns[5].Width = 60; 
                // Lớp
                viewDanhSach.Columns[6].HeaderText = "Lớp";
                viewDanhSach.AutoResizeColumn(6); 
                // Nhóm
                viewDanhSach.Columns[7].HeaderText = "Nhóm";
                viewDanhSach.Columns[7].Width = 40; 
                viewDanhSach.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;                                                                                              
            }
        }
    }
}
