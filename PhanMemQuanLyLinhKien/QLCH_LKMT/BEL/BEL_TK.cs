using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEL
{
   public class BEL_TK
    {
        private string taiKhoan;
        private string matKhau;
        public BEL_TK(string tk, string mk)
        {
            taiKhoan = tk;
            matKhau = mk;
        }
        public string _taiKhoan
        {
            set { taiKhoan = value; }
            get { return taiKhoan; }
        }
        public string _matKhau
        {
            set { matKhau = value; }
            get { return matKhau; }
        }
    }
}
