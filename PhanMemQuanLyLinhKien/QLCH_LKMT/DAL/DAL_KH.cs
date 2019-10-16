using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using BEL;

namespace DAL
{
    public class DAL_KH:General
    {
        public DataTable LayDanhSachKH()
        {
  
            string query = "SELECT * FROM KHACHHANG ";
            dt.Load(this.Read(query));
            return dt;

        }
        public bool ThemKhachHang(BEL_KH khachHang)
        {
            try
            {
                string query = "INSERT INTO KHACHHANG VALUES('" + khachHang.MAKH + "',N'" + khachHang.TENKH + "','" + khachHang.NGSINH + "',N'" + khachHang.PHAI + "',N'" + khachHang.DCHI + "','" + khachHang.SDT + "'," + khachHang.DIEM_TICH_LUY + ")";

                this.Read(query);

                return true;
            }
            catch (Exception err)
            {

            }
            return false;

        }

        public void XoaKhachHang(string MAKH)
        {

            string query = "DELETE KHACHHANG WHERE MAKH ='" + MAKH + "'";
            this.Read(query);

        }
        public bool CapNhatKhachHang(BEL_KH khachHang)
        {
            try
            {
                string query = "UPDATE KHACHHANG SET MAKH ='" + khachHang.MAKH + "', TENKH = N'" + khachHang.TENKH + "', NGSINH ='" + khachHang.NGSINH + "',PHAI = N'" + khachHang.PHAI + "', DIACHI = N'" + khachHang.DCHI + "',SDT = '" + khachHang.SDT + "', DIEM_TICH_LUY = " + khachHang.DIEM_TICH_LUY;

                this.Read(query);

                return true;
            }
            catch (Exception err)
            {

            }
            return false;

        }
        public bool TaoMaKH(string MAKH, int total)
        {
            dt = this.LayDanhSachKH();
            int n = dt.Rows.Count;
            for (int i = 0; i < n; i++)
            {
                string maDB = dt.Rows[i].ItemArray[0].ToString();
                if (MAKH == maDB || (int)(maDB[maDB.Length - 1] + 48) < total)
                    return false;

            }
            return true;
        }
    }
}
