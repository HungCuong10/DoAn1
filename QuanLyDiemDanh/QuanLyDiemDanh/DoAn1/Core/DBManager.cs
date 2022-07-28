using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn1.Core
{
    class DBManager
    {
        private String ChuoiKetNoi = null;
        private SqlConnection conn = null;
        SqlDataAdapter adapter; // Lấy trạng thái kết nối CSDL

        public SqlConnection getConn()
        {
            return conn;
        }
        // Cổng kết nối CSDL qua Visual Studio
        public DBManager()
        {
            ChuoiKetNoi = @"Server = DESKTOP-BJOE2SJ\HUNGCUONG; Database = QuanLyDiemDanh; Integrated Security = True";
        }
        public SqlDataAdapter getAdapter()
        {
            return this.adapter;
        }
        // Hàm Ketnoi
        public SqlConnection KetNoi()
        {
            try
            {
                conn = new SqlConnection(ChuoiKetNoi);
                conn.Open();
                return conn;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        // Data Table danh sách lớp 
        public DataTable ds_Lop()
        {
            KetNoi();
            string lop = "SELECT MaLop,TenMonHoc ,HoTenGV " +
                         "FROM LOPHOCPHAN as lh LEFT JOIN GIANGVIEN as gv ON lh.MaGV = gv.MaGV ";
            adapter = new SqlDataAdapter(lop, conn); // Dẫn dữ liệu
            DataTable dt = new DataTable(); // Nơi chứa dữ liệu

            adapter.Fill(dt); // Đưa dữ liệu vô DataTable
            return dt;
        }
        // Hàm kiểm tra thông tin trùng lặp
        public Boolean KiemTra(string ma)
        {
            DataRow[] TimMa = ds_Lop().Select("MaLop = '" + ma + "'");
            if(TimMa.Length != 0)
            {
                return true;
            }
            return false;
        }
        // Hàm kiểm tra MSSV
        public Boolean KiemTraMSSV(string mssv)
        {
            DataRow[] TimMa = ds_SinhVien().Select("MSSV = '" + mssv + "'");
            if (TimMa.Length != 0)
            {
                return true;
            }
            return false;
        }
        // Hàm kiểm tra mã lớp
        public Boolean KiemTraLopDD(string malop, string sv)
        {
            DataRow[] DiemDanh = checkMaDD().Select("MaLop = '" + malop + "'" + "AND MSSV = '" + sv +"'");
            if (DiemDanh.Length != 0)
            {
                return true;
            }
            return false;
        }       
        // Data Table Tài khoản
        public DataTable ds_TaiKhoan()
        {
            KetNoi();
            string taikhoan = "SELECT TaiKhoan, MatKhau, VaiTro " +
                              "FROM DANGNHAP";
            adapter = new SqlDataAdapter(taikhoan, conn); // Dẫn dữ liệu
            DataTable dt = new DataTable(); // Nơi chứa dữ liệu
            adapter.Fill(dt); // Đưa dữ liệu vô DataTable
            return dt;
        }
  
       // DataTable Giảng viên
        public DataTable ds_GiangVien()
        {
            KetNoi();
            string taikhoan = "SELECT MaGV, HoTenGV, TaiKhoan" +
                              " FROM GIANGVIEN";
            adapter = new SqlDataAdapter(taikhoan, conn); // Dẫn dữ liệu
            DataTable dt = new DataTable(); // Nơi chứa dữ liệu
            adapter.Fill(dt); // Đưa dữ liệu vô DataTable
            return dt;
        }
        // DataTable Sinh viên
        public DataTable ds_SinhVien()
        {
            KetNoi();
            string sinhvien = "SELECT * FROM SINHVIEN ORDER BY Stt";
            adapter = new SqlDataAdapter(sinhvien, conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }
       public DataTable checkMaDD()
        {
            KetNoi();
            string check = "SELECT * FROM DIEMDANH";
            adapter = new SqlDataAdapter(check, conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }     
    }
}

