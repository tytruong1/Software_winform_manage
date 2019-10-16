using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAL;
using BEL;

namespace BAL
{
   public class BAL_KH
    {
        public DataTable LayDanhSachKH()
        {
            DAL_KH xuLyKH = new DAL_KH();
            return xuLyKH.LayDanhSachKH();
        }

        public bool ThemKhachHang(BEL_KH khachHang)
        {
            //Xử lý dữ liệu dữ liệu
            if (khachHang.TENKH == "")
                return false;
            else if (khachHang.SDT == "")
                return false;
            else if (khachHang.DCHI == "")
                return false;

            //Thực hiện công việc truy vấn
            DAL_KH xuLyThem = new DAL_KH();
            return xuLyThem.ThemKhachHang(khachHang);
        }

        public bool XoaKhachHang(string MAKH)
        {
            DAL_KH xuLyKH = new DAL_KH();

            xuLyKH.XoaKhachHang(MAKH);
            return true;
        }
        public bool CapNhatKhachHang(BEL_KH khachHang)
        {
            //Xử lý dữ liệu dữ liệu
            if (khachHang.TENKH == "")
                return false;
            else if (khachHang.SDT == "")
                return false;
            else if (khachHang.DCHI == "")
                return false;

            //Thực hiện công việc truy vấn
            DAL_KH xuLyKH = new DAL_KH();
            return xuLyKH.CapNhatKhachHang(khachHang);
        }

        public string LayMaKHMoi(int count)
        {
            string MAKH = "";

            DAL_KH xuLyKH = new DAL_KH();
            int total = 1;

            do
            {
                MAKH = "KH";
                if (total < 10)
                    MAKH += "00" + total.ToString();
                else if (total < 100)
                    MAKH += "0" + total.ToString();
                else
                    MAKH += total.ToString();
                total++;
            } while (!xuLyKH.TaoMaKH(MAKH, total - 1));
            return MAKH;
        }
    }
}
