using BEL;
using System.Data;

namespace DAL
{
    public class DAL_TK:General
    {
        public DataTable kiemTraTaiKhoan(BEL_TK account)
        {
            string query = "SELECT LOAI, MANV FROM NHANVIEN WHERE TAIKHOAN='" + account._taiKhoan + "' AND MATKHAU ='" + account._matKhau + "'";
            dt.Load(this.Read(query));
            return dt;
        }

    }
}
