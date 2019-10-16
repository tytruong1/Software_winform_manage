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
using System.IO;

namespace QLCH_LKMT
{
    public partial class QLSP : Form
    {
        private DataTable dt = new DataTable();
        private BAL_LPKIEN xuLySP = new BAL_LPKIEN();
        public QLSP()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {

        }

        private void HienThiDSSanPham(DataGridView dgr)
        {

            dt = xuLySP.LayDanhSachSanPham();
            dgr.DataSource = dt;
        }

        private void QLSP_Load(object sender, EventArgs e)
        {
            HienThiDSSanPham(dgvSanPham);

            //hiển thị Loại, linh kiện hoặc phụ kiện
            cboLoaiSP.Items.Add("Linh Kiện");
            cboLoaiSP.Items.Add("Phụ Kiện");
            cboLoaiSP.SelectedIndex = 0;

            //phương thức tìm kiếm
            cboTimKiem.Items.Add("Tất Cả");
            cboTimKiem.Items.Add("Linh Kiện");
            cboTimKiem.Items.Add("Phụ Kiện");
            cboTimKiem.SelectedIndex = 0;

        }

        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //string masp =dgvSanPham.CurrentRow.Cells[0].Value.ToString();

                txtMaSP.Text = dgvSanPham.CurrentRow.Cells[0].Value.ToString();
                txtTenSP.Text = dgvSanPham.CurrentRow.Cells[1].Value.ToString();
                txtNSX.Text = dgvSanPham.CurrentRow.Cells[2].Value.ToString();
                txtGia.Text = dgvSanPham.CurrentRow.Cells[3].Value.ToString();
                rtbMieuTa.Text = dgvSanPham.CurrentRow.Cells[4].Value.ToString();
                txtSL.Text = dgvSanPham.CurrentRow.Cells[5].Value.ToString();
                txtAnh.Text = dgvSanPham.CurrentRow.Cells[6].Value.ToString();
                cboLoaiSP.Text = dgvSanPham.CurrentRow.Cells[7].Value.ToString();
                imgSanPham.ImageLocation = @"../../img/linh_phukien/" + txtAnh.Text;

            }
            catch (Exception err)
            {

            }
            btnTaoMoi.Enabled = true;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            btnAdd.Enabled = false;

        }
        public void ThemVaoDataTable(BEL_LPKIEN sanPham)
        {
            DataRow row = dt.NewRow();
            row[0] = sanPham.MASP;
            row[1] = sanPham.TENSP;
            row[2] = sanPham.NHASX;
            row[3] = sanPham.GIA;
            row[4] = sanPham.MIEUTA;
            row[5] = sanPham.SL_TONKHO;
            row[6] = sanPham.IMAGE;
            row[7] = sanPham.LOAI;
            row[8] = sanPham.TRANGTHAI;
           
            dt.Rows.Add(row);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //tạo dữ liệu
            string MASP = txtMaSP.Text.Trim();
            string TENSP = txtTenSP.Text.Trim();
            string NHASX = txtNSX.Text.Trim();
            float GIA;
            string MIEUTA = rtbMieuTa.Text.Trim();
            int SL_TONKHO;
            string IMAGE = txtAnh.Text.Trim();
            string LOAI = cboLoaiSP.Text.Trim();
            bool TRANGTHAI = true;


            //kiểm tra ràng buộc ép kiểu cho float và int
            if (txtGia.Text == "")
                GIA = 0;
            else
                GIA = float.Parse(txtGia.Text);

            if (txtSL.Text == "")
                SL_TONKHO = 0;
            else
                SL_TONKHO = int.Parse(txtSL.Text);


            //xử lý thêm sản phẩm
            BEL_LPKIEN sanPham = new BEL_LPKIEN(MASP, TENSP, NHASX, GIA, MIEUTA, SL_TONKHO, IMAGE, LOAI, TRANGTHAI);
            if (xuLySP.ThemSanPham(sanPham))
            {
                ThemVaoDataTable(sanPham);
                dgvSanPham.DataSource = dt;
                MessageBox.Show("Đã Thêm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Question);

                //lưu file ảnh
                File.Copy(openFile.FileName, @"../../img/linh_phukien/" + openFile.SafeFileName);

                btnTaoMoi.Enabled = true;
                btnAdd.Enabled = false;
            }
            else
            {
                MessageBox.Show("Thêm Thất Bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Question);

            }
           

        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            txtMaSP.Text = xuLySP.LayMaSPMoi(dt.Rows.Count);

            //txtTenSP.Text = "";
            //txtNSX.Text = "";
            //rtbMieuTa.Text = "";
            //txtGia.Text = "";
            //txtSL.Text = "";
            //txtAnh.Text = "";
            //imgSanPham.ImageLocation = "";
            //Làm khuyết các nút button

            btnAdd.Enabled = true;
            btnTaoMoi.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //xóa nhân viên dưới DB
            if (xuLySP.XoaSanPham(txtMaSP.Text.Trim()))
            {
                //Xóa Nhân viên trong DataTable
                dt.Rows.RemoveAt(dgvSanPham.CurrentRow.Index);
                dgvSanPham.DataSource = dt;
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

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void btnAnh_Click(object sender, EventArgs e)
        {
           
            openFile.Filter = "JPG files(*.*)|*.JPG|PNG files|*.PNG";
            openFile.InitialDirectory = @"C:\";
            openFile.Title = "Mở Files"; ;
            if(openFile.ShowDialog() == DialogResult.OK)
            {
                imgSanPham.ImageLocation = openFile.FileName;
                txtAnh.Text = openFile.SafeFileName;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //tạo dữ liệu
            string MASP = txtMaSP.Text.Trim();
            string TENSP = txtTenSP.Text.Trim();
            string NHASX = txtNSX.Text.Trim();
            float GIA;
            string MIEUTA = rtbMieuTa.Text.Trim();
            int SL_TONKHO;
            string IMAGE = txtAnh.Text.Trim();
            string LOAI = cboLoaiSP.Text.Trim();
            bool TRANGTHAI = true; 


            //kiểm tra ràng buộc ép kiểu cho float và int
            if (txtGia.Text == "")
                GIA = 0;
            else
                GIA = float.Parse(txtGia.Text);

            if (txtSL.Text == "")
                SL_TONKHO = 0;
            else
                SL_TONKHO = int.Parse(txtSL.Text);


            //xử lý cập nhật sản phẩm
            BEL_LPKIEN sanPham = new BEL_LPKIEN(MASP, TENSP, NHASX, GIA, MIEUTA, SL_TONKHO, IMAGE, LOAI, TRANGTHAI);
            if (xuLySP.CapNhatSanPham(sanPham))
            {
                CapNhatDataTable(sanPham);
                dgvSanPham.DataSource = dt;
                MessageBox.Show("Đã Cập Nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
                btnTaoMoi.Enabled = true;
                btnAdd.Enabled = false;
            }
            else
            {
                MessageBox.Show("Cập Nhật Thất Bại. Có thể bạn đã bỏ trống dữ liệu nào đó.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Question);

            }

        }
        public void CapNhatDataTable(BEL_LPKIEN sanPham)
        {
            int index = dgvSanPham.CurrentRow.Index;


        
            dt.Rows[index]["TENSP"] = sanPham.TENSP;
            dt.Rows[index]["NHASX"] = sanPham.NHASX;
            dt.Rows[index]["GIA"] = sanPham.GIA;
            dt.Rows[index]["MIEUTA"] = sanPham.MIEUTA;
            dt.Rows[index]["SL_TONKHO"] = sanPham.SL_TONKHO;
            dt.Rows[index]["IMAGE"] = sanPham.IMAGE;
            dt.Rows[index]["LOAI"] = sanPham.LOAI;
            dt.Rows[index]["TRANGTHAI"] = sanPham.TRANGTHAI;
            
        }

        public void SelectData(DataTable pData, string query)
        {
            DataRow[] Rows = pData.Select(query);

            pData = ((DataTable)dgvSanPham.DataSource).Clone();
            for (int i = 0; i < Rows.Length; i++)
            {
                DataRow row = pData.NewRow();
                for (int j = 0; j < pData.Columns.Count; j++)
                {
                    row[j] = Rows[i].ItemArray[j].ToString();
                }
                pData.Rows.Add(row);

            }
            dgvSanPham.DataSource = pData;
        }
        private void cboTimKiem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTimKiem.Text == "Tất Cả")
            {
                dgvSanPham.DataSource = dt;
            }
            else if (cboTimKiem.Text == "Phụ Kiện")
            {
                SelectData(dt, "LOAI ='Phụ Kiện'");
            }
            else if (cboTimKiem.Text == "Linh Kiện")
            {
                SelectData(dt, "LOAI ='Linh Kiện'");
            }
            
        }
    }
}
