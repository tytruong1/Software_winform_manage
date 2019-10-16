using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BAL;
using BEL;

namespace QLCH_LKMT
{
    public partial class login : Form
    {
        public static string loaiNV;
        public static string MANV;
        public login()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult kq = MessageBox.Show("Bạn có chắc muốn thoát ?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
                this.Close();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            BEL_TK account = new BEL_TK(txtTaiKhoan.Text.Trim(), txtMatKhau.Text);
            BAL_TK xulydangnhap = new BAL_TK();
            //MessageBox.Show(xulydangnhap.KiemTraDangNhap(account), "Thoong Bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DataTable dt = xulydangnhap.KiemTraDangNhap(account);
            loaiNV = dt.Rows[0]["LOAI"].ToString();
            MANV = dt.Rows[0]["MANV"].ToString();
            Admin ad1 = new Admin();
            if (loaiNV != "")
            {
                ad1.loaiNV = loaiNV;
                ad1.ShowDialog();
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu !", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }

        }

       
    }
}
