using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEL
{
    public class BEL_HDNHAP
    {
        private string _MAHD;
        private string _NGUOILAP;
        private string _NGAYLAP;
        private int _TRANGTHAI;

        public BEL_HDNHAP(string _MAHD, string _NGUOILAP, string _NGAYLAP)
        {
            this._MAHD = _MAHD;
            this._NGUOILAP = _NGUOILAP;
            this._NGAYLAP = _NGAYLAP;
            this._TRANGTHAI = 1;
        }
        public string MAHD { get { return _MAHD; } set { _MAHD = value; } }
        public string NGUOILAP { get { return _NGUOILAP; } set { _NGUOILAP = value; } }
        public string NGAYLAP { get { return _NGAYLAP; } set { _NGAYLAP = value; } }
        public int TRANGTHAI { get { return _TRANGTHAI; } set { _TRANGTHAI = value; } }
    }
}
