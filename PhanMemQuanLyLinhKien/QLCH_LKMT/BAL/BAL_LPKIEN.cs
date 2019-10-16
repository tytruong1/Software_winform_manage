using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using BEL;
using DAL;

namespace BAL
{
    public class BAL_LPKIEN
    {
        public DataTable LayDanhSachSanPham()
        {

            DAL_LPKIEN dal_NhanVien = new DAL_LPKIEN();
            return dal_NhanVien.LayDanhSachSanPham();
        }
        public bool ThemSanPham(BEL_LPKIEN SanPham)
        {
            //Xử lý dữ liệu dữ liệu
            if (SanPham.MASP == "")
                return false;
            else if (SanPham.TENSP == "")
                return false;

            else if (SanPham.NHASX == "")
                return false;
            else if (SanPham.MIEUTA == "")
                return false;
            else if (SanPham.IMAGE == "")
                return false;
            else if (SanPham.LOAI == "")
                return false;
            

            //Thực hiện công việc truy vấn
            DAL_LPKIEN xuLyThem = new DAL_LPKIEN();
            return xuLyThem.ThemSanPham(SanPham);
        }

        public bool XoaSanPham(string MASP)
        {
            DAL_LPKIEN xuLySP = new DAL_LPKIEN();

            xuLySP.XoaSanPham(MASP);
            return true;
        }
        public bool CapNhatSanPham(BEL_LPKIEN sanPham)
        {
            //Xử lý dữ liệu dữ liệu
            if (sanPham.MASP == "")
                return false;
            if (sanPham.TENSP == "")
                return false;

            if (sanPham.NHASX == "")
                return false;
            if (sanPham.MIEUTA == "")
                return false;
            if (sanPham.IMAGE == "")
                return false;
            if (sanPham.LOAI == "")
                return false;


            //Thực hiện công việc truy vấn
            DAL_LPKIEN xuLyCapNhat = new DAL_LPKIEN();
            return xuLyCapNhat.CapNhatSanPham(sanPham);
        }
        public string LayMaSPMoi(int count)
        {
            string MASP = "";

            DAL_LPKIEN xuLySP = new DAL_LPKIEN();
            int total = 1;

            do
            {
                MASP = "SP";
                if (total < 10)
                    MASP += "00" + total.ToString();
                else if (total < 100)
                    MASP += "0" + total.ToString();
                else
                    MASP += total.ToString();
                total++;
            } while (!xuLySP.TaoMaSP(MASP, total - 1));
            return MASP;
        }
    }
}
