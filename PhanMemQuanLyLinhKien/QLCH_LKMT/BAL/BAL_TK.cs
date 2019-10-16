using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BEL;
using DAL;
using System.Data;

namespace BAL
{
    public class BAL_TK
    {
        public DataTable KiemTraDangNhap(BEL_TK account)
        {
            DAL_TK xulydangnhap = new DAL_TK();

            DataTable chuoi = new DataTable();
            try
            {

                chuoi = xulydangnhap.kiemTraTaiKhoan(account);
            }
            catch(Exception err)
            {   
            }
                return chuoi;
        }
    }
}
