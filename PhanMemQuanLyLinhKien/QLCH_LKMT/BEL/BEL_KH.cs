using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEL
{
    public class BEL_KH
    {
        private string _MAKH;
        private string _TENKH;
        private string _NGSINH;
        private string _PHAI;
        private string _DCHI;
        private string _SDT;
        private int _DIEM_TICH_LUY;

        public BEL_KH(string _MAKH, string _TENKH, string _NGSINH, string _PHAI, string _DCHI, string _SDT, int _DIEM_TICH_LUY)
        {
            this._MAKH = _MAKH;
            this._TENKH = _TENKH;
            this._NGSINH = _NGSINH;
            this._PHAI = _PHAI;
            this._DCHI = _DCHI;
            this._SDT = _SDT;
            this._DIEM_TICH_LUY = _DIEM_TICH_LUY;
        }
        public string MAKH { get { return _MAKH ; } set { _MAKH = value; } }
        public string TENKH { get { return _TENKH; } set { _TENKH = value; } }
        public string NGSINH { get { return _NGSINH; } set { _NGSINH = value; } }
        public string PHAI { get { return _PHAI; } set { _PHAI = value; } }
        public string DCHI { get { return _DCHI; } set { _DCHI = value; } }
        public string SDT { get { return _SDT; } set { _SDT = value; } }
        public int DIEM_TICH_LUY { get { return _DIEM_TICH_LUY; } set { _DIEM_TICH_LUY = value; } }

    }
}
