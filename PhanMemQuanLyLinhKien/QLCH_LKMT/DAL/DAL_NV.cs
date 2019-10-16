using System;
using System.Data;
using BEL;


namespace DAL
{
    public class DAL_NV:General
    {
        public DataTable LayDanhSachNhanVien()
        {
            string query = "SELECT * FROM NHANVIEN ";
            dt.Load(this.Read(query));
            return dt;
           
        }
        public bool ThemNhanVien(BEL_NV nhanVien)
        {
            try
            {
                string query = "INSERT INTO NHANVIEN VALUES('" + nhanVien._maNV + "',N'" + nhanVien._tenNV + "','" + nhanVien._ngSinh.Year.ToString() + "/" + nhanVien._ngSinh.Month.ToString() + "/" + nhanVien._ngSinh.Day.ToString() + "',N'" + nhanVien._gioiTinh + "',N'" + nhanVien._diaChi + "','" + nhanVien._SDT + "'," + nhanVien._luong + ",'" + nhanVien._tk._taiKhoan + "','" + nhanVien._tk._matKhau + "','" + nhanVien._loaiNV + "')";
                this.Read(query);
                
                return true;
            }
            catch(Exception err)
            {
                
            }
            return false;
           
        }
        public void XoaNhanVien(string MANV)
        {
            try
            {
                string query = "DELETE NHANVIEN WHERE MANV ='" + MANV + "'";
                this.Read(query);
            }
            catch(Exception err)
            {
                throw;
            }
            
            
        }
        public bool TaoMaNhanVien(string MANV, int total)
        {
            dt = this.LayDanhSachNhanVien();
            int n = dt.Rows.Count;
            for (int i = 0 ; i < n; i++ )
            {
                string maDB = dt.Rows[i].ItemArray[0].ToString();
                if (MANV == maDB || (int)(maDB[maDB.Length - 1] + 48) < total)
                    return false;
               


            }
            return true;
        }
        public bool CapNhatNhanVien(BEL_NV nhanVien)
        {
            try
            {
                string query = "UPDATE NHANVIEN SET TEN_NV = N'" + nhanVien._tenNV + "',NGSINH = '" + nhanVien._ngSinh.Year.ToString() + "/" + nhanVien._ngSinh.Month.ToString() + "/" + nhanVien._ngSinh.Day.ToString() + "',PHAI = N'" + nhanVien._gioiTinh + "', DIACHI = N'" + nhanVien._diaChi + "', SDT = '" + nhanVien._SDT + "', LUONG = " + nhanVien._luong + ", TAIKHOAN = '" + nhanVien._tk._taiKhoan + "', MATKHAU = '" + nhanVien._tk._matKhau + "', LOAI =  '" + nhanVien._loaiNV + "' WHERE MANV = '" + nhanVien._maNV + "'";
                this.Read(query);

                return true;
            }
            catch (Exception err)
            {

            }
            return false;
        }
    }
}
