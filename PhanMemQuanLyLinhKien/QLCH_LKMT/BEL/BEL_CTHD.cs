using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEL
{
   public class BEL_CTHD
    {
        private string _MAHD;
        private string _MASP;
        private int _DONGIA;
        private int _SOLUONG;

        public BEL_CTHD(string _MAHD, string _MASP, int _DONGIA,int _SOLUONG)
        {
            this._MAHD = _MAHD;
            this._MASP = _MASP;
            this._DONGIA = _DONGIA;
            this._SOLUONG = _SOLUONG;
        }

        public string MAHD { get { return _MAHD; } set { _MAHD = value; } }
        public string MASP { get { return _MASP; } set { _MASP = value; } }
        public int DONGIA { get { return _DONGIA; } set { _DONGIA = value; } }
        public int SOLUONG { get { return _SOLUONG; } set { _SOLUONG = value; } }


    }
}
