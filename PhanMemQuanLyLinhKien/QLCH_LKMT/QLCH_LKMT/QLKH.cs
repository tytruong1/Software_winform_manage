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
    public partial class QLKH : Form
    {
        private DataTable dt = new DataTable();
        private BAL_KH xuLyKH = new BAL_KH();
        public QLKH()
        {
            InitializeComponent();
        }

       

       
         private void HienThiDSKhachHang(DataGridView dgv)
        {
           
            dt = xuLyKH.LayDanhSachKH();
            dgv.DataSource = dt;
        }

        private void QLKH_Load(object sender, EventArgs e)
        {
            HienThiDSKhachHang(dgvKhachHang);
            

        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            txtMaKH.Text = xuLyKH.LayMaKHMoi(dt.Rows.Count);

            

            btnAdd.Enabled = true;
            btnTaoMoi.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        public void ThemVaoDataTable(BEL_KH khachHang)
        {
            DataRow row = dt.NewRow();
            row[0] = khachHang.MAKH;
            row[1] = khachHang.TENKH;
            row[2] = khachHang.NGSINH;
            row[3] = khachHang.PHAI;
            row[4] = khachHang.DCHI;
            row[5] = khachHang.SDT;
            row[6] = khachHang.DIEM_TICH_LUY;
            
            dt.Rows.Add(row);
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //xử lý thêm khach hàng
            string gioiTinh;
            int diem = 0;
            if (rdbNam.Checked == true)
                gioiTinh = "Nam";
            else
                gioiTinh = "Nữ";

            if (txtTichLuy.Text != "")
                diem = int.Parse(txtTichLuy.Text);

            BEL_KH khachHang = new BEL_KH(txtMaKH.Text, txtHoTen.Text, dtpNgaySinh.Text, gioiTinh, rtbDiaChi.Text, txtSDT.Text, diem);
            if (xuLyKH.ThemKhachHang(khachHang))
            {
                ThemVaoDataTable(khachHang);
                dgvKhachHang.DataSource = dt;
                MessageBox.Show("Đã Thêm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Question);

             
                btnTaoMoi.Enabled = true;
                btnAdd.Enabled = false;
            }
            else
            {
                MessageBox.Show("Thêm Thất Bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Question);

            }
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaKH.Text = dgvKhachHang.CurrentRow.Cells[0].Value.ToString();
            txtHoTen.Text = dgvKhachHang.CurrentRow.Cells[1].Value.ToString();
            dtpNgaySinh.Text = dgvKhachHang.CurrentRow.Cells[2].Value.ToString();

            if (dgvKhachHang.CurrentRow.Cells[3].Value.ToString() == "Nam")
                rdbNam.Checked = true;
            else
                rdbNu.Checked = true;
            rtbDiaChi.Text = dgvKhachHang.CurrentRow.Cells[4].Value.ToString();
            txtSDT.Text = dgvKhachHang.CurrentRow.Cells[5].Value.ToString();
            txtTichLuy.Text = dgvKhachHang.CurrentRow.Cells[6].Value.ToString();


            btnTaoMoi.Enabled = true;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            btnAdd.Enabled = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //xóa nhân viên dưới DB
            if (xuLyKH.XoaKhachHang(txtMaKH.Text.Trim()))
            {
                //Xóa Nhân viên trong DataTable
                int index = (dgvKhachHang.CurrentRow.Index);
                dt.Rows.RemoveAt(index);
                dgvKhachHang.DataSource = dt;
                MessageBox.Show("Đã Xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            else
            {
                MessageBox.Show("Xóa không thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            btnTaoMoi.Enabled = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //xử lý cập nhật khach hàng
            string gioiTinh;
            int diem = 0;
            if (rdbNam.Checked == true)
                gioiTinh = "Nam";
            else
                gioiTinh = "Nữ";

            if (txtTichLuy.Text != "")
                diem = int.Parse(txtTichLuy.Text);

            BEL_KH khachHang = new BEL_KH(txtMaKH.Text, txtHoTen.Text, dtpNgaySinh.Text, gioiTinh, rtbDiaChi.Text, txtSDT.Text, diem);
            if (xuLyKH.CapNhatKhachHang(khachHang))
            {
                CapNhatDataTable(khachHang);
                dgvKhachHang.DataSource = dt;
                MessageBox.Show("Đã Cập Nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Question);


                btnTaoMoi.Enabled = true;
                btnAdd.Enabled = false;
            }
            else
            {
                MessageBox.Show("Cập Nhật Thất Bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Question);

            }
        }
        public void CapNhatDataTable(BEL_KH khachhang)
        {
            int index = dgvKhachHang.CurrentRow.Index;



            dt.Rows[index]["TENKH"] = khachhang.TENKH;
            dt.Rows[index]["NGSINH"] = khachhang.NGSINH;
            dt.Rows[index]["PHAI"] = khachhang.PHAI;
            dt.Rows[index]["DIACHI"] = khachhang.DCHI;
            dt.Rows[index]["SDT"] = khachhang.SDT;
            dt.Rows[index]["DIEM_TICH_LUY"] = khachhang.DIEM_TICH_LUY;
            

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult thoat = MessageBox.Show("Bạn có chắc muốn thoát", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (thoat == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
