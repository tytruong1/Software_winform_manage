using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEL
{
    public class BEL_NV
    {
        private string maNV;
        private string tenNV;
        private DateTime ngSinh;
        private string gioiTinh;
        private string diaChi;
        private string SDT;
        private float luong;
        private BEL_TK tk;
        private string loaiNV;
        
        //methods

        public BEL_NV(string maNV, string tenNV,DateTime ngSinh,string gioiTinh, string diaChi,string SDT,float luong, BEL_TK tk,string loaiNV)
        {
            
            this.maNV = maNV;
            this.tenNV = tenNV;
            this.ngSinh = ngSinh;
            this.gioiTinh = gioiTinh;
            this.diaChi = diaChi;
            this.SDT = SDT;
            this.luong = luong;
            this.tk = new BEL_TK(tk._taiKhoan, tk._matKhau);
            this.loaiNV = loaiNV;
        }

        public string _maNV { get { return maNV; } set { maNV = value; } }
        public string _tenNV { get { return tenNV; } set { tenNV = value; } }
        public DateTime _ngSinh { get { return ngSinh; } set { ngSinh = value; } }
        public string _gioiTinh { get { return gioiTinh; } set { gioiTinh = value; } }
        public string _diaChi { get { return diaChi; } set { diaChi = value; } }
        public string _SDT { get { return SDT; } set { SDT = value; } }
        public float _luong { get { return luong; } set { luong = value; } }
        public BEL_TK _tk { get { return tk; } set { tk = value; } }
        public string _loaiNV { get { return loaiNV; } set { loaiNV = value; } }
    }
}
