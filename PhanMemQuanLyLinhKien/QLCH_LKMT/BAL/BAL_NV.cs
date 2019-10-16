using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BEL;
using System.Data;

namespace BAL
{
   public  class BAL_NV
    {
        public DataTable layDanhSachNhanVien()
        {
           
            DAL_NV dal_NhanVien = new DAL_NV();
            return dal_NhanVien.LayDanhSachNhanVien();
        }
        public bool ThemNhanVien(BEL_NV nhanVien)
        {
            //Xử lý dữ liệu dữ liệu
            if (nhanVien._diaChi == "")
                return false;
            if (nhanVien._SDT == "")
                return false;
           
            if(nhanVien._tenNV == "")
                return false;
            if(nhanVien._loaiNV == "")
                return false;
            if(nhanVien._tk._taiKhoan == "")
                return false;
            if(nhanVien._tk._matKhau == "")
                return false;
            if (nhanVien._loaiNV == "Quản Lý")
                nhanVien._loaiNV = "admin";
            else if (nhanVien._loaiNV == "Thu Ngân")
                nhanVien._loaiNV = "ThuNgan";
            else
                nhanVien._loaiNV = "ThuKho";
           

            //Thực hiện công việc truy vấn
            DAL_NV xuLyThem = new DAL_NV();
            return xuLyThem.ThemNhanVien(nhanVien);
        }
        public bool XoaNhanVien(string MANV)
        {
                DAL_NV xuLyNV = new DAL_NV();
            
                xuLyNV.XoaNhanVien(MANV);
                return true;
        }
        public bool CapNhatNhanVien(BEL_NV nhanVien)
        {
            //Xử lý dữ liệu dữ liệu
            if (nhanVien._diaChi == "")
                return false;
            if (nhanVien._SDT == "")
                return false;

            if (nhanVien._tenNV == "")
                return false;
            if (nhanVien._loaiNV == "")
                return false;
            if (nhanVien._tk._taiKhoan == "")
                return false;
            if (nhanVien._tk._matKhau == "")
                return false;
            if (nhanVien._loaiNV == "Quản Lý")
                nhanVien._loaiNV = "admin";
            else if (nhanVien._loaiNV == "Thu Ngân")
                nhanVien._loaiNV = "ThuNgan";
            else
                nhanVien._loaiNV = "ThuKho";


            //Thực hiện công việc truy vấn
            DAL_NV xuLyCapNhat = new DAL_NV();
            return xuLyCapNhat.CapNhatNhanVien(nhanVien);
        }
        public string LayMaNhanVienMoi(int count)
        {
            string MANV = "";
            
            DAL_NV xuLyNV = new DAL_NV();
            int total = 1;

            do
            {
                MANV = "NV";
                if (total < 10)
                    MANV += "00" + total.ToString();
                else if (total < 100)
                    MANV += "0" + total.ToString();
                else
                    MANV += total.ToString();
                total++;
            } while (!xuLyNV.TaoMaNhanVien(MANV, total - 1));
            return MANV;
        }
    }
}
