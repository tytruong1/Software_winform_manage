
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEL
{
    class BEL_NHANVIEN
    {
        
            private string maNV;
            private string ten_NV;
            private DateTime ngSinh;
            private string phai;
            private string diaChi;
            private string sDT;
            private string luong;


        public BEL_NHANVIEN(string maNV, string ten_NV, DateTime ngSinh, string phai,string diaChi,string sDT,string luong)
            {
                this.maNV = maNV;
                this.ten_NV=ten_NV;
                this.ngSinh = ngSinh;
                this.phai = phai;
                this.diaChi = diaChi;
                this.sDT = sDT;
                this.luong = luong;

            }
            public string _maNV
            {
                set { maNV = value; }
                get { return maNV; }
            }
            public string _tenNV
            {
                set { ten_NV = value; }
                get { return ten_NV; }
            }
            public DateTime _ngSinh
            {
                set { ngSinh = value;}
                get { return ngSinh; }
            }
            public string _phai
            {
            set { phai = value; }
            get { return phai; }
            }
            public string _diaChi
            {
            set { diaChi = value; }
            get { return diaChi; }
            }
            public string _sDT
            {
            set { sDT = value; }
            get { return sDT; }
            }
    }
}
