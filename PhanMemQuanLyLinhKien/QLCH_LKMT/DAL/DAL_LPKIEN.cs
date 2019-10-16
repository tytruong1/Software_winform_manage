using System;
using System.Data;
using BEL;


namespace DAL
{
    public class DAL_LPKIEN:General
    {
        public DataTable LayDanhSachSanPham()
        {
            string query = "SELECT * FROM SANPHAM ";
            dt.Load(this.Read(query));
            return dt;

        }
        public bool ThemSanPham(BEL_LPKIEN SanPham)
        {
            try
            {
               string query = "INSERT INTO SANPHAM VALUES('" + SanPham.MASP + "',N'" + SanPham.TENSP + "',N'" + SanPham.NHASX + "'," + SanPham.GIA + ",N'" + SanPham.MIEUTA + "'," + SanPham.SL_TONKHO + ",'" + SanPham.IMAGE + "',N'" + SanPham.LOAI + "'," +Convert.ToInt32(SanPham.TRANGTHAI) + ")";

                this.Read(query);

                return true;
            }
            catch (Exception err)
            {

            }
            return false;

        }
        public void XoaSanPham(string MASP)
        {
            
                string query = "DELETE SANPHAM WHERE MASP ='" + MASP + "'";
                this.Read(query);
  
        }

        public bool CapNhatSanPham(BEL_LPKIEN SanPham)
        {
            try
            {
                string query = "UPDATE SANPHAM SET TENSP= N'" + SanPham.TENSP + "',NHASX = N'" + SanPham.NHASX + "',GIA = " + SanPham.GIA + ",MIEUTA = N'" + SanPham.MIEUTA + "',SL_TONKHO = " + SanPham.SL_TONKHO + ",IMAGE = '" + SanPham.IMAGE + "',LOAI = N'" + SanPham.LOAI + "',TRANGTHAI = " + Convert.ToInt32(SanPham.TRANGTHAI) + " WHERE MASP = '" + SanPham.MASP + "'";
                this.Read(query);

                return true;
            }
            catch (Exception err)
            {

            }
            return false;
        }
        public bool TaoMaSP(string MASP, int total)
        {
            dt = this.LayDanhSachSanPham();
            int n = dt.Rows.Count;
            for (int i = 0; i < n; i++)
            {
                string maDB = dt.Rows[i].ItemArray[0].ToString();
                if (MASP == maDB || (int)(maDB[maDB.Length - 1] + 48) < total)
                    return false;



            }
            return true;
        }
    }
}
