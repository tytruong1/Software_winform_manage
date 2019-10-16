using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BEL;
using System.Data;
using DAL;

namespace BAL
{
   public class BAL_HD
    {
        public DataTable LayDanhSachCTHDNhap()
        {
            DAL_HD xuLyHD = new DAL_HD();
            return xuLyHD.LayDanhSachCTHDNhap();
        }
        public string LayMaHD(DataTable dt)
        {
            string MAHD = "";

            DAL_HD xuLyHD = new DAL_HD();
            int total = 1;

            do
            {
                MAHD = "HD";
                if (total < 10)
                    MAHD += "00" + total.ToString();
                else if (total < 100)
                    MAHD += "0" + total.ToString();
                else
                    MAHD += total.ToString();
                total++;
            } while (!xuLyHD.TaoMaHD(MAHD, total - 1, dt));
            return MAHD;
        }
        public DataTable LayDanhSachHDNhap()
        {
            DAL_HD xuLyHD = new DAL_HD();

            return xuLyHD.LayDanhSachHDNhap();
        }
        public DataTable LayDanhSachHDBan()
        {
            DAL_HD xuLyHD = new DAL_HD();

            return xuLyHD.LayDanhSachHDBan();
        }


        public bool ThemCTHDNHAP(BEL_CTHD chiTietHDNhap)
        {
            DAL_HD xuLyHD = new DAL_HD();

            return xuLyHD.ThemCTHDNHAP(chiTietHDNhap);
        }
        public bool ThemCTHDBan(BEL_CTHD chiTietHDBan)
        {
            DAL_HD xuLyHD = new DAL_HD();

            return xuLyHD.ThemCTHDBan(chiTietHDBan);
        }
        public bool ThemHDNhap(BEL_HDNHAP HD)
        {
            DAL_HD xuLyHD = new DAL_HD();

            return xuLyHD.ThemHDNhap(HD);
        }
        public bool ThemHDBan(BEL_HDBAN HD)
        {
            DAL_HD xuLyHD = new DAL_HD();

            return xuLyHD.ThemHDBan(HD);
        }
    }
}
