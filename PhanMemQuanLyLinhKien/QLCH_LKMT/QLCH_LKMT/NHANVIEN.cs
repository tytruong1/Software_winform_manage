using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BEL;
using BAL;
using System.IO;

namespace QLCH_LKMT
{
    public partial class NHANVIEN : Form
    {

        //danh sách sản phẩm hiện có
        private DataTable dtSanPham = new DataTable();

        //Danh sách sản phẩm đang nhập vào
        private DataTable dtHoaDonBan = new DataTable();
        private List<string[]> newImage = new List<string[]>();


        private BAL_LPKIEN xuLySP = new BAL_LPKIEN();
        private BAL_HD xuLyHD = new BAL_HD();
        public NHANVIEN()
        {
            InitializeComponent();
        }
        private void HienThiDSSanPham(DataGridView dgr)
        {

            dtSanPham = xuLySP.LayDanhSachSanPham();
            dgr.DataSource = dtSanPham;
        }

        private void NHANVIEN_Load(object sender, EventArgs e)
        {
            //danh sách sản phẩm
            HienThiDSSanPham(dgvSanPham);
            //Hiển thị DS mã KH
            cboDanhSachKH(cboMaKH);

            //hiển thị Loại, linh kiện hoặc phụ kiện
            cboLoaiSP.Items.Add("Linh Kiện");
            cboLoaiSP.Items.Add("Phụ Kiện");
            cboLoaiSP.SelectedIndex = 0;

            //tạo bảng sao của dgv sản phẩm
            dtHoaDonBan = ((DataTable)dgvSanPham.DataSource).Clone();


            //phương thức tìm kiếm
            cboTimKiem.Items.Add("Tất Cả");
            cboTimKiem.Items.Add("Linh Kiện");
            cboTimKiem.Items.Add("Phụ Kiện");
            cboTimKiem.SelectedIndex = 0;
        }


        //load DS mã KH vào ComboBox
        public void  cboDanhSachKH(ComboBox cbo)
        {
            DataTable dtKH = (new BAL_KH()).LayDanhSachKH();
            foreach(DataRow Row in dtKH.Rows)
            {
                cbo.Items.Add(Row["MAKH"]);
            }
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
                img.ImageLocation = @"../../img/linh_phukien/" + txtAnh.Text;

            }
            catch (Exception err)
            {

            }
           // btnTaoMaKH.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
           
            if (btnTaoMoiHD.Enabled == false)
                btnAdd.Enabled = true;
        }

        private void btnTaoMoiHD_Click(object sender, EventArgs e)
        {
            txtMaHD.Text = xuLyHD.LayMaHD(xuLyHD.LayDanhSachHDBan());


            //Làm khuyết các nút button
            btnAdd.Enabled = true;
            btnTaoMoiHD.Enabled = false;
            btnUpdate.Enabled = false;
            btnTaoMaKH.Enabled = true;
        }

        private void btnTaoMaKH_Click(object sender, EventArgs e)
        {
            QLKH formKH = new QLKH();
            formKH.ShowDialog();

            //xóa danh sách khách hantyàng cũ
            cboMaKH.Items.Clear();

            //Load lại danh Sách mã KH
            cboDanhSachKH(cboMaKH);
            cboMaKH.SelectedIndex = cboMaKH.Items.Count - 1;

            //
            //cboMaKH.Enabled = false;
            btnTaoMaKH.Enabled = false;
        }

        private string ChuyenDinhDangTien(string gia)
        {
            string[] tmp = gia.Split(".".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            gia = tmp[0];
            for (int i = 1; i < tmp.Length; i++)
                gia += tmp[i];
            return gia;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //Kiểm tra số lượng bán có lớn hơn số lượng tồn
                if (int.Parse(dgvSanPham.CurrentRow.Cells[5].Value.ToString())>= int.Parse(txtSL.Text) && txtSL.Text.Trim() !="0")
                {
                    MessageBox.Show("Đã Thêm", "Thông Báo");
                    DataGridViewRow Row = (DataGridViewRow)dgvThemSanPham.Rows[0].Clone();
                    //cộng tiền
                    lblTongTien.Text = ChuyenDinhDangTien(lblTongTien.Text);
                     lblTongTien.Text = (int.Parse(lblTongTien.Text) + int.Parse(txtSL.Text) * int.Parse(txtGia.Text)).ToString();
                        XuLyTongTien();
                    for (int i = 0; i < dgvThemSanPham.Rows.Count - 1; i++)
                    {
                        //số lượng sản phẩm trong bảng tạm thời
                        int sl = int.Parse(dgvThemSanPham.Rows[i].Cells[5].Value.ToString());

                        //Kiểm tra nếu trùng mã sản phẩm thì chỉ cập nhật số lượng
                        if (dgvThemSanPham.Rows[i].Cells[2].Value.ToString() == txtMaSP.Text)
                        {
                            dgvThemSanPham.Rows[i].Cells[5].Value = (sl + int.Parse(txtSL.Text)).ToString();
                            dtHoaDonBan.Rows[i]["SL_TONKHO"] = (sl + int.Parse(txtSL.Text)).ToString();

                            //cập nhật sl trong dataGrid view
                            dgvSanPham.CurrentRow.Cells[5].Value = int.Parse(dgvSanPham.CurrentRow.Cells[5].Value.ToString()) - int.Parse(txtSL.Text);

                            //Làm trống dữ liệu đang hiện thị
                            txtMaSP.Text = "";
                            txtAnh.Text = "";
                            txtGia.Text = "";
                            txtNSX.Text = "";
                            txtSL.Text = "";
                            txtTenSP.Text = "";
                            rtbMieuTa.Text = "";
                            img.ImageLocation = null;

                            return;
                        }
                    }

                    Row.Cells[0].Value = txtMaHD.Text;
                    Row.Cells[1].Value = cboMaKH.Text;
                    Row.Cells[2].Value = txtMaSP.Text;
                    Row.Cells[3].Value = THUKHO.DateNow();
                    Row.Cells[4].Value = login.MANV;
                    Row.Cells[5].Value = txtSL.Text;
                    Row.Cells[6].Value = txtGia.Text;

                    
                    //thêm vào dtHoaDonBan tạm thời
                    ThemVaoDataTable(dgvSanPham.CurrentRow);

                    //Cập nhật sô lượng vào dataGridView
                    dgvSanPham.CurrentRow.Cells[5].Value = int.Parse(dgvSanPham.CurrentRow.Cells[5].Value.ToString()) - int.Parse(txtSL.Text);

                    //thêm vào data grid view
                    dgvThemSanPham.Rows.Add(Row);
                    // maSPMoi.Add(txtMaSP.Text.Trim());


                    //Làm trống dữ liệu đang hiện thị
                    txtMaSP.Text = "";
                    txtAnh.Text = "";
                    txtGia.Text = "";
                    txtNSX.Text = "";
                    txtSL.Text = "";
                    txtTenSP.Text = "";
                    rtbMieuTa.Text = "";
                    img.ImageLocation = null;

                    //btnTaoMaKH.Enabled = true;
                    //btnAnh.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Bạn chưa nhập số lượng, Hoặc số lượng trong kho không đủ.", "Thông Báo");
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Chưa có thông tin sản phẩm.", "Thông Báo");
            }
        }
        public void ThemVaoDataTable(DataGridViewRow grRow)
        {
            DataRow row = dtHoaDonBan.NewRow();


            
                row[0] = grRow.Cells[0].Value.ToString();
                row[1] = grRow.Cells[1].Value.ToString();
                row[2] = grRow.Cells[2].Value.ToString();
                row[3] = grRow.Cells[3].Value.ToString();
                row[4] = grRow.Cells[4].Value.ToString();
                row[5] = txtSL.Text;
                row[6] = grRow.Cells[6].Value.ToString();
                row[7] = grRow.Cells[7].Value.ToString();
                row[8] = grRow.Cells[8].Value.ToString();


            dtHoaDonBan.Rows.Add(row);
        }

        private void cboTimKiem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTimKiem.Text == "Tất Cả")
            {
                dgvSanPham.DataSource = dtSanPham;
            }
            else if (cboTimKiem.Text == "Phụ Kiện")
            {
                dgvSanPham.DataSource = THUKHO.SelectData(dtSanPham, "LOAI ='Phụ Kiện'", dgvSanPham);
            }
            else if (cboTimKiem.Text == "Linh Kiện")
            {
                dgvSanPham.DataSource = THUKHO.SelectData(dtSanPham, "LOAI ='Linh Kiện'", dgvSanPham);
            }
        }

        private void dgvThemSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int index = dgvThemSanPham.CurrentRow.Index;

                txtMaSP.Text = dtHoaDonBan.Rows[index]["MASP"].ToString();
                txtTenSP.Text = dtHoaDonBan.Rows[index]["TENSP"].ToString();
                txtNSX.Text = dtHoaDonBan.Rows[index]["NHASX"].ToString();
                txtGia.Text = dtHoaDonBan.Rows[index]["GIA"].ToString();
                rtbMieuTa.Text = dtHoaDonBan.Rows[index]["MIEUTA"].ToString();
                txtSL.Text = dtHoaDonBan.Rows[index]["SL_TONKHO"].ToString();
                txtAnh.Text = dtHoaDonBan.Rows[index]["IMAGE"].ToString();
                cboLoaiSP.Text = dtHoaDonBan.Rows[index]["LOAI"].ToString();
                img.ImageLocation = @"../../img/linh_phukien/" + txtAnh.Text;

            }
            catch (Exception err)
            {

            }
            //btnTaoMoi.Enabled = true;
            btnUpdate.Enabled = true;
            btnAdd.Enabled = false;
            btnDelete.Enabled = true;
            //btnAnh.Enabled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (dtHoaDonBan.Rows.Count > 0)
            {
                //cộng tiền
                lblTongTien.Text = ChuyenDinhDangTien(lblTongTien.Text);
                lblTongTien.Text = (int.Parse(lblTongTien.Text) - int.Parse(txtSL.Text) * int.Parse(txtGia.Text)).ToString();
                XuLyTongTien();
                //lấy số lượng của Sp trong bảng tạm thời
                DataRow Rowdt = dtHoaDonBan.Rows[dgvThemSanPham.CurrentRow.Index];
                int sl = int.Parse(Rowdt["SL_TONKHO"].ToString());

                //Duyệt mã SP cần xóa và cập nhật lại Số lượng tồn
                for (int i = 0; i < dgvSanPham.Rows.Count - 1; i++)
                {
                    if (dgvSanPham.Rows[i].Cells[0].Value == Rowdt["MASP"])
                    {
                        //Cập nhật số lượng tồn
                        dgvSanPham.Rows[i].Cells[5].Value = sl + int.Parse(dgvSanPham.Rows[i].Cells[5].Value.ToString());

                        //Cập nhật lại danh sách hiển thị các bảng
                        dtHoaDonBan.Rows.Remove(Rowdt);
                        dgvThemSanPham.Rows.RemoveAt(dgvThemSanPham.CurrentRow.Index);

                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show("Còn gì nữa đâu mà xóa .", "Thông Báo");
                btnUpdate.Enabled = false;
            }

        }

        private void cboMaKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboMaKH.Enabled = false;
            btnTaoMaKH.Enabled = false;
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            DialogResult kq = MessageBox.Show("Thanh Toán Hóa Đơn ?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DialogResult.Yes == kq)
            {
                //Kiểm có tồn tại sản phẩm trong hóa đơn
                    if (dtHoaDonBan.Rows.Count > 0)
                    {
                     MessageBox.Show("Đã Lưu", "Thông Báo");
                    lblTongTien.Text = "0";

                    //duyệt Bảng dữ liệu tạm thời để update số lượng
                    foreach (DataRow Row in dtHoaDonBan.Rows)
                        {
                            //Thêm hóa đơn bán hàng mới
                            xuLyHD.ThemHDBan(new BEL_HDBAN(txtMaHD.Text,cboMaKH.Text, THUKHO.DateNow(), login.MANV));

                            //Thêm vào ChiTietHoaDon
                            BEL_CTHD chiTietHDBan = new BEL_CTHD(txtMaHD.Text.ToString(), Row["MASP"].ToString(), int.Parse(Row["GIA"].ToString()), int.Parse(Row["SL_TONKHO"].ToString()));
                            xuLyHD.ThemCTHDBan(chiTietHDBan);

                            //Tạo Row dữ liệu clone từ database và update dữ liệu
                            DataRow Rowt = (DataRow)(THUKHO.SelectData(dtSanPham, "MASP ='" + Row["MASP"] + "'", dgvSanPham).Rows[0]);
                            Row["SL_TONKHO"] = int.Parse(Rowt["SL_TONKHO"].ToString());
                            //Update CSDL
                            BEL_LPKIEN sanPham = new BEL_LPKIEN(Row["MASP"].ToString(), Row["TENSP"].ToString(), Row["NHASX"].ToString(), int.Parse(Row["GIA"].ToString()), Row["MIEUTA"].ToString(), int.Parse(Row["SL_TONKHO"].ToString()), Row["IMAGE"].ToString(), Row["LOAI"].ToString(), bool.Parse(Row["TRANGTHAI"].ToString()));
                            xuLySP.CapNhatSanPham(sanPham);
                        }

                            //Load lại data lên GUI
                            dtSanPham = xuLySP.LayDanhSachSanPham();
                            dgvSanPham.DataSource = dtSanPham;

                            //xóa Hóa đơn tạm thời
                            dtHoaDonBan.Clear();
                            dgvThemSanPham.Rows.Clear();

                            //tắt các btn chức năng
                            btnTaoMoiHD.Enabled = true;
                            btnTaoMaKH.Enabled = true;
                            cboMaKH.Enabled = true;
                            btnAdd.Enabled = false;
                            txtMaHD.Text = "";

                        }
                    else //Giỏ hàng không có sản phẩm
                        MessageBox.Show("Không có sản phẩm.", "Thông Báo");

                }
            }
        
        private void XuLyTongTien()
        {
            string gia = lblTongTien.Text;
            List<char> conten = new List<char>();
            int dem = 0;
            for (int i = gia.Length - 1; i >= 0; i--, dem++)
            {
                if (dem == 3)
                {
                    conten.Add('.');
                    dem = 0;
                }
                conten.Add(gia[i]);

            }
            gia = "";
            for (int i = conten.Count - 1; i >= 0; i--)
            {
                gia += conten[i];
            }
            lblTongTien.Text = gia;
        }

        private void lblTongTien_TextChanged(object sender, EventArgs e)
        {
            
        
        }

        private void lblTongTien_Click(object sender, EventArgs e)
        {

        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
           
        }
    }
}
