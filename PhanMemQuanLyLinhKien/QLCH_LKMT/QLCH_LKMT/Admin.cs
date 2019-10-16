using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCH_LKMT
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }
        public string loaiNV = "";
       

        private void Admin_Load(object sender, EventArgs e)
        {
            if(loaiNV == "ThuKho")
            {
                btnNhanVien.Enabled = false;
                btnHoaDon.Enabled = false;
                btnKhachHang.Enabled = false;
                btnSanPham.Enabled = false;
                btnThuNgan.Enabled = false;
            }
            else if(loaiNV == "ThuNgan")
            {
                btnNhanVien.Enabled = false;
                btnSanPham.Enabled = false;
                btnHoaDon.Enabled = false;
                btnThuKho.Enabled = false;
            }
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            this.Hide();
            QLNV nhanVien = new QLNV();
            nhanVien.ShowDialog();
            this.Show();
        }

        private void btnSanPham_Click(object sender, EventArgs e)
        {
            this.Hide();
            QLSP sanPham = new QLSP();
            sanPham.ShowDialog();
            this.Show();
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            this.Hide();
            THONGKE thongKe= new THONGKE();
            thongKe.ShowDialog();
            this.Show();

        }
        
        private void btnKhachHang_Click(object sender, EventArgs e)
        {

            this.Hide();
            QLKH khachHang = new QLKH();
            khachHang.ShowDialog();
            this.Show();
        }

        private void btnThuNgan_Click(object sender, EventArgs e)
        {
            this.Hide();
            NHANVIEN thungan = new NHANVIEN();
            thungan.ShowDialog();
            this.Show();
        }

        private void btnThuKho_Click(object sender, EventArgs e)
        {
            this.Hide();
            THUKHO thuKho = new THUKHO();
            thuKho.ShowDialog();
            this.Show();
        }
    }
}
