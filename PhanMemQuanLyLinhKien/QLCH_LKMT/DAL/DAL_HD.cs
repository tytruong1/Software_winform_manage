using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BEL;
using System.Data;

namespace DAL
{
   public  class DAL_HD:General
    {
        public DataTable LayDanhSachHDNhap()
        {
            string query = "SELECT MAHD FROM HOADONNHAP";
            DataTable dt = new DataTable();
            dt.Load(this.Read(query));
            return dt;
        }
        public DataTable LayDanhSachHDBan()
        {
            string query = "SELECT MAHD FROM HOADON";
            DataTable dt = new DataTable();
            dt.Load(this.Read(query));
            return dt;
        }
        public DataTable LayDanhSachCTHDNhap()
        {
            string query = "SELECT * FROM CHITIETHOADON_NHAP";
            DataTable dt = new DataTable();
            dt.Load(this.Read(query));
            return dt;
        }

        public bool TaoMaHD(string MANV, int total, DataTable dt)
        {
           
            int n = dt.Rows.Count;
            for (int i = 0; i < n; i++)
            {
                string maDB = dt.Rows[i].ItemArray[0].ToString();
                if (MANV == maDB || (int)(maDB[maDB.Length - 1] + 48) < total)
                    return false;
            }
            return true;
        }
        public bool ThemCTHDNHAP(BEL_CTHD chiTietHDNhap)
        {
            try
            {
                string query = "INSERT INTO CHITIETHOADON_NHAP VALUES('" + chiTietHDNhap.MAHD + "','" + chiTietHDNhap.MASP + "'," + chiTietHDNhap.DONGIA+ "," + chiTietHDNhap.SOLUONG + ")";

                this.Read(query);

                return true;
            }
            catch (Exception err)
            {

            }
            return false;
        }
        public bool ThemCTHDBan(BEL_CTHD chiTietHDBan)
        {
            try
            {
                string query = "INSERT INTO CHITIETHOADON VALUES('" + chiTietHDBan.MAHD + "','" + chiTietHDBan.MASP + "'," + chiTietHDBan.SOLUONG + "," + chiTietHDBan.DONGIA + ")";

                this.Read(query);

                return true;
            }
            catch (Exception err)
            {

            }
            return false;
        }
        public bool ThemHDNhap(BEL_HDNHAP HD)
        {
            try
            {
                string query = "INSERT INTO HOADONNHAP VALUES('" + HD.MAHD + "','" + HD.NGUOILAP + "','" + HD.NGAYLAP + "'," + HD.TRANGTHAI + ")";

                this.Read(query);

                return true;
            }
            catch (Exception err)
            {

            }
            return false;
        }
        public bool ThemHDBan(BEL_HDBAN HD)
        {
            try
            {
                string query = "INSERT INTO HOADON VALUES('" + HD.MAHD + "','" + HD.MAKH +"','" + HD.NGAYLAP + "','" + HD.NGUOILAP + "'," + HD.TRANGTHAI + ")";

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
