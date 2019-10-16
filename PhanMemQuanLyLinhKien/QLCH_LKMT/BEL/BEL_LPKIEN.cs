using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEL
{
    public class BEL_LPKIEN
    {
        private string _MASP;
        private string _TENSP;
        private string _NHASX;
        
        private float _GIA;
        private string _MIEUTA;
        private int _SL_TONKHO;
        private string _IMAGE;
        private string _LOAI;
        private bool _TRANGTHAI;

        public BEL_LPKIEN( string _MASP, string _TENSP, string _NHASX, float _GIA, string _MIEUTA, int _SL_TONKHO, string _IMAGE, string _LOAI, bool _TRANGTHAI)
        {
            this._MASP = _MASP;
            this._TENSP = _TENSP;
            this._NHASX = _NHASX;
            this._GIA = _GIA;
            this._MIEUTA = _MIEUTA;
            this._SL_TONKHO = _SL_TONKHO;
            this._IMAGE = _IMAGE;
            this._LOAI = _LOAI;
            this._TRANGTHAI = _TRANGTHAI;
    }


        public string MASP { get { return _MASP; } set { _MASP = value; } }
        public string TENSP { get { return _TENSP; } set { _TENSP = value; } }
        public string NHASX { get { return _NHASX; } set { _NHASX = value; } }

        public float GIA { get { return _GIA; } set { _GIA = value; } }
        public string MIEUTA { get { return _MIEUTA; } set { _MIEUTA = value; } }
        public int SL_TONKHO { get { return _SL_TONKHO; } set { _SL_TONKHO = value; } }
        public string IMAGE { get { return _IMAGE; } set { _IMAGE = value; } }
        public string LOAI { get { return _LOAI; } set {_LOAI = value; } }
        public bool TRANGTHAI { get { return _TRANGTHAI; } set { _TRANGTHAI = value; } }

    }
}
