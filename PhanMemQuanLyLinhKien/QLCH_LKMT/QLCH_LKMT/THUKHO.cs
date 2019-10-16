using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using BAL;
using BEL;

namespace QLCH_LKMT
{
    public partial class THUKHO : Form
    {
        //danh sách sản phẩm hiện có
        private DataTable dtSanPham = new DataTable();

        //Danh sách sản phẩm đang nhập vào
        private DataTable dtHoaDonNhap = new DataTable();
        private List<string[]> newImage = new List<string[]>();


        private BAL_LPKIEN xuLySP = new BAL_LPKIEN();
        private BAL_HD xuLyHD = new BAL_HD();

        //danh sách mã sản phẩm đã thêm vào
        private List<string> maSPMoi = new List<string>();
        public THUKHO()
        {
            InitializeComponent();
          
        }

        private void QLHD_Load(object sender, EventArgs e)
        {
            HienThiDSSanPham(dgvSanPham);
            
            //hiển thị Loại, linh kiện hoặc phụ kiện
            cboLoaiSP.Items.Add("Linh Kiện");
            cboLoaiSP.Items.Add("Phụ Kiện");
            cboLoaiSP.SelectedIndex = 0;

            //tạo bảng sao của dgv sản phẩm
            dtHoaDonNhap = ((DataTable)dgvSanPham.DataSource).Clone();
            

            //phương thức tìm kiếm
            cboTimKiem.Items.Add("Tất Cả");
            cboTimKiem.Items.Add("Linh Kiện");
            cboTimKiem.Items.Add("Phụ Kiện");
            cboTimKiem.SelectedIndex = 0;

        }

        private void HienThiDSSanPham(DataGridView dgr)
        {

            dtSanPham = xuLySP.LayDanhSachSanPham();
            dgr.DataSource = dtSanPham;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult thoat = MessageBox.Show("bạn có chắc muốn thoát", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (thoat == DialogResult.Yes)
            {
                this.Close();
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
            btnTaoMoiSP.Enabled = true;
            btnUpdate.Enabled = false;
            btnAnh.Enabled = false;
            if(btnTaoMoiHD.Enabled == false)
            btnAdd.Enabled = true;
            
        }

        private void btnTaoMoiHD_Click(object sender, EventArgs e)
        {
            txtMaHD.Text = xuLyHD.LayMaHD(xuLyHD.LayDanhSachHDNhap());

            
            //Làm khuyết các nút button
            btnAdd.Enabled = true;
            btnTaoMoiHD.Enabled = false;
            btnUpdate.Enabled = false;
            btnTaoMoiSP.Enabled = true;
        }

        public void ThemVaoDataTable(DataGridViewRow grRow)
        {
            DataRow row = dtHoaDonNhap.NewRow();


            //kiểm tra Mã sp có là Mã mới hay cũ
            if (btnTaoMoiSP.Enabled == true)
            {
                row[0] = grRow.Cells[0].Value.ToString();
                row[1] = grRow.Cells[1].Value.ToString();
                row[2] = grRow.Cells[2].Value.ToString();
                row[3] = grRow.Cells[3].Value.ToString();
                row[4] = grRow.Cells[4].Value.ToString();
                row[5] = txtSL.Text;
                row[6] = grRow.Cells[6].Value.ToString();
                row[7] = grRow.Cells[7].Value.ToString();
                row[8] = grRow.Cells[8].Value.ToString();
            }
            else
            {
                row[0] = txtMaSP.Text;
                row[1] = txtTenSP.Text;
                row[2] = txtNSX.Text;
                row[3] = txtGia.Text;
                row[4] = rtbMieuTa.Text;
                row[5] = txtSL.Text;
                row[6] = txtAnh.Text;
                row[7] = cboLoaiSP.Text;
                row[8] = 1;

            }

            dtHoaDonNhap.Rows.Add(row);
        }
        public static string DateNow()
        {
            return DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {

            try
            {
                DataGridViewRow Row = (DataGridViewRow)dgvThemSanPham.Rows[0].Clone();

                for (int i = 0; i < dgvThemSanPham.Rows.Count - 1; i++)
                {

                    int sl = int.Parse(dgvThemSanPham.Rows[i].Cells[4].Value.ToString());

                    //Kiểm tra nếu trùng mã sản phẩm thì chỉ cập nhật số lượng
                    if (dgvThemSanPham.Rows[i].Cells[1].Value.ToString() == txtMaSP.Text)
                    {
                        dgvThemSanPham.Rows[i].Cells[4].Value = (sl + int.Parse(txtSL.Text)).ToString();
                        dtHoaDonNhap.Rows[i]["SL_TONKHO"] = (int.Parse(dtHoaDonNhap.Rows[i]["SL_TONKHO"].ToString()) + int.Parse(txtSL.Text)).ToString();
                        MessageBox.Show("Đã Thêm", "Thông Báo");
                        return;
                    }
                }

                Row.Cells[0].Value = txtMaHD.Text;
                Row.Cells[1].Value = txtMaSP.Text;
                Row.Cells[2].Value = DateNow();
                Row.Cells[3].Value = login.MANV;
                Row.Cells[4].Value = txtSL.Text;
                Row.Cells[5].Value = txtGia.Text;

                ThemVaoDataTable(dgvSanPham.CurrentRow);
                dgvThemSanPham.Rows.Add(Row);
                maSPMoi.Add(txtMaSP.Text.Trim());


                txtMaSP.Text = "";
                txtAnh.Text = "";
                txtGia.Text = "";
                txtNSX.Text = "";
                txtSL.Text = "";
                txtTenSP.Text = "";
                rtbMieuTa.Text = "";
                img.ImageLocation = null;

                btnTaoMoiSP.Enabled = true;
                btnAnh.Enabled = false;
                MessageBox.Show("Đã Thêm", "Thông Báo");
            }
            catch(Exception err)
            {
                MessageBox.Show("Chưa có thông tin sản phẩm.", "Thông Báo");
            }
           

        }
        //Kiểm tra file ảnh đã tồn tại chưa ?
        public bool CheckImageFile(string pathSource, string FileName)
        {
            string[] files = Directory.GetFiles(pathSource);
            for(int i = 0; i< files.Length; i++)
            {
                if(FileName == getName(files[i]))
                
                    return true;
                
            }
            return false;
        }

        private string getName(string name)
        {
            //cắt \\ và chuỗi thành mảng
            string[] tam = name.Split("//".ToArray(), StringSplitOptions.RemoveEmptyEntries);

            //trả về phần tử cuối cùng đồng thời cũng là tên của 1 file
            return tam[tam.Length - 1];
        }

        private void btnAnh_Click(object sender, EventArgs e)
        {
            openFile.Filter = "JPG files(*.*)|*.JPG|PNG files|*.PNG";
            openFile.InitialDirectory = @"C:\";
            openFile.Title = "Mở Files"; ;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                //kiểm tra file ảnh đã tồn tại chưa
                if(CheckImageFile(@"../../img/linh_phukien/", openFile.SafeFileName))
                {
                    MessageBox.Show("Ảnh đã tồn tại.Xin hãy đổi tên khác !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                string[] a = new string[2];
                a[0] = openFile.FileName;
                a[1] = openFile.SafeFileName;
                newImage.Add(a);
                img.ImageLocation = openFile.FileName;
                txtAnh.Text = openFile.SafeFileName;
            }
        }

        private void btnTaoMoiSP_Click(object sender, EventArgs e)
        {
            btnTaoMoiSP.Enabled = false;
            //txtMaSP.Text = xuLySP.LayMaSPMoi(dtSanPham.Rows.Count);
            string MASP = "";

           
            int total = 1;

            do
            {
                MASP = "SP";
                if (total < 10)
                    MASP += "00" + total.ToString();
                else if (total < 100)
                    MASP += "0" + total.ToString();
                else
                    MASP += total.ToString();
                total++;
            } while (!TaoMaSP(MASP, total - 1, dtSanPham) || findString(maSPMoi, MASP));

            txtMaSP.Text = MASP;
            

            btnAnh.Enabled = true;
            btnAdd.Enabled = true;
           
        }
        public bool findString(List<string> ch, string con)
        {
            for(int i=0; i <ch.Count; i++)
            {
                if (ch[i] == con)
                    return true;
            }
            return false;
        }
        

        //kieerm tra ma SP
        public bool TaoMaSP(string MASP, int total, DataTable dt)
        {
            
            int n = dt.Rows.Count;
            for (int i = 0; i < n; i++)
            {
                string maDB = dt.Rows[i].ItemArray[0].ToString();
                if (MASP == maDB || (int)(maDB[maDB.Length - 1] + 48) < total)
                    return false;



            }
            return true;
        }

        private void dgvThemSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int index = dgvThemSanPham.CurrentRow.Index;

                txtMaSP.Text = dtHoaDonNhap.Rows[index]["MASP"].ToString();
                txtTenSP.Text = dtHoaDonNhap.Rows[index]["TENSP"].ToString();
                txtNSX.Text = dtHoaDonNhap.Rows[index]["NHASX"].ToString();
                txtGia.Text = dtHoaDonNhap.Rows[index]["GIA"].ToString();
                rtbMieuTa.Text = dtHoaDonNhap.Rows[index]["MIEUTA"].ToString();
                txtSL.Text = dtHoaDonNhap.Rows[index]["SL_TONKHO"].ToString();
                txtAnh.Text = dtHoaDonNhap.Rows[index]["IMAGE"].ToString();
                cboLoaiSP.Text = dtHoaDonNhap.Rows[index]["LOAI"].ToString();
                img.ImageLocation = @"../../img/linh_phukien/" + txtAnh.Text;

            }
            catch (Exception err)
            {

            }
            //btnTaoMoi.Enabled = true;
            btnUpdate.Enabled = true;
            btnAdd.Enabled = false;
            //btnDelete.Enabled = true;
            btnAnh.Enabled = true;
        }
        public void CapNhatDataTable()
        {
            int index = dgvThemSanPham.CurrentRow.Index;



            dtHoaDonNhap.Rows[index]["TENSP"] = txtTenSP.Text;
            dtHoaDonNhap.Rows[index]["NHASX"] = txtNSX.Text;
            dtHoaDonNhap.Rows[index]["GIA"] = txtGia.Text;
            dtHoaDonNhap.Rows[index]["MIEUTA"] = rtbMieuTa.Text;
            dtHoaDonNhap.Rows[index]["SL_TONKHO"] = txtSL.Text;
            dtHoaDonNhap.Rows[index]["IMAGE"] = txtAnh.Text;
            dtHoaDonNhap.Rows[index]["LOAI"] = cboLoaiSP.Text;
            dtHoaDonNhap.Rows[index]["TRANGTHAI"] = 1;

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            try
            {
                CapNhatDataTable();
                dgvThemSanPham.CurrentRow.Cells[4].Value = txtSL.Text;
                dgvThemSanPham.CurrentRow.Cells[5].Value = txtGia.Text;
                MessageBox.Show("Đã Cập Nhật", "Thông Báo");
            }
            catch(Exception err)
            {
                MessageBox.Show("Cập Nhật Thất Bại", "Thông Báo");
            }
        }


        //truy vấn trong datatable
        public static DataTable SelectData(DataTable pData, string query, DataGridView dgv)
        {
            DataRow[] Rows = pData.Select(query);

            pData = ((DataTable)dgv.DataSource).Clone();
            for (int i = 0; i < Rows.Length; i++)
            {
                DataRow row = pData.NewRow();
                for (int j = 0; j < pData.Columns.Count; j++)
                {
                    row[j] = Rows[i].ItemArray[j].ToString();
                }
                pData.Rows.Add(row);

            }
            
            return pData;
        }
        private void cboTimKiem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTimKiem.Text == "Tất Cả")
            {
                dgvSanPham.DataSource = dtSanPham;
            }
            else if (cboTimKiem.Text == "Phụ Kiện")
            {
                dgvSanPham.DataSource = SelectData(dtSanPham, "LOAI ='Phụ Kiện'", dgvSanPham);
            }
            else if (cboTimKiem.Text == "Linh Kiện")
            {
                dgvSanPham.DataSource = SelectData(dtSanPham, "LOAI ='Linh Kiện'", dgvSanPham);
            }
        }

        private void btnHoanTat_Click(object sender, EventArgs e)
        {
            DialogResult kq = MessageBox.Show("Lưu Hóa Đơn ?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(DialogResult.Yes == kq)
            {
                if (dtHoaDonNhap.Rows.Count > 0)
                {
                    MessageBox.Show("Đã Lưu", "Thông Báo");
                    foreach (DataRow Row in dtHoaDonNhap.Rows)
                    {
                        //Thêm hóa đơn nhập hàng mới
                        xuLyHD.ThemHDNhap(new BEL_HDNHAP(txtMaHD.Text, login.MANV, DateNow()));
                        //kiểm tra sản phẩm đã có sẵn chưa
                        if (SelectData(dtSanPham, "MASP ='" + Row["MASP"] + "'", dgvSanPham).Rows.Count < 1)
                        {
                            //Luu ảnh vào CSDL
                            if (newImage.Count > 0)
                            {
                                File.Copy(newImage[0][0], @"../../img/linh_phukien/" + newImage[0][1]);
                                newImage.Remove(newImage[0]);
                            }

                            //Là sản phẩm mới: -> Thêm SP mới và cập nhật vào ChiTietHoaDon_Nhap
                            BEL_LPKIEN sanPham = new BEL_LPKIEN(Row["MASP"].ToString(), Row["TENSP"].ToString(), Row["NHASX"].ToString(), int.Parse(Row["GIA"].ToString()), Row["MIEUTA"].ToString(), int.Parse(Row["SL_TONKHO"].ToString()), Row["IMAGE"].ToString(), Row["LOAI"].ToString(), bool.Parse(Row["TRANGTHAI"].ToString()));
                            xuLySP.ThemSanPham(sanPham);

                            //Thêm vào ChiTietHoaDonNhap
                            BEL_CTHD chiTietHDNhap = new BEL_CTHD(txtMaHD.Text.ToString(), Row["MASP"].ToString(), int.Parse(Row["GIA"].ToString()), int.Parse(Row["SL_TONKHO"].ToString()));
                            xuLyHD.ThemCTHDNHAP(chiTietHDNhap);
                        }
                        else
                        {
                            //Thêm vào ChiTietHoaDonNhap
                            BEL_CTHD chiTietHDNhap = new BEL_CTHD(txtMaHD.Text.ToString(), Row["MASP"].ToString(), int.Parse(Row["GIA"].ToString()), int.Parse(Row["SL_TONKHO"].ToString()));
                            xuLyHD.ThemCTHDNHAP(chiTietHDNhap);

                            //sản phẩm đã có -> cập nhật số lượng
                            DataRow Rowt = (DataRow)(SelectData(dtSanPham, "MASP ='" + Row["MASP"] + "'", dgvSanPham).Rows[0]);
                            Row["SL_TONKHO"] = int.Parse(Row["SL_TONKHO"].ToString()) + int.Parse(Rowt["SL_TONKHO"].ToString());
                            //Update CSDL
                            BEL_LPKIEN sanPham = new BEL_LPKIEN(Row["MASP"].ToString(), Row["TENSP"].ToString(), Row["NHASX"].ToString(), int.Parse(Row["GIA"].ToString()), Row["MIEUTA"].ToString(), int.Parse(Row["SL_TONKHO"].ToString()), Row["IMAGE"].ToString(), Row["LOAI"].ToString(), bool.Parse(Row["TRANGTHAI"].ToString()));
                            xuLySP.CapNhatSanPham(sanPham);

                        }
                    }

                    //Load lại data lên GUI
                    dtSanPham = xuLySP.LayDanhSachSanPham();
                    dgvSanPham.DataSource = dtSanPham;

                    //xóa Hóa đơn tạm thời
                    dtHoaDonNhap.Clear();
                    maSPMoi.Clear();
                    dgvThemSanPham.Rows.Clear();

                    //tắt các btn chức năng
                    btnTaoMoiHD.Enabled = true;
                    btnTaoMoiSP.Enabled = false;
                    btnAdd.Enabled = false;
                    txtMaHD.Text = "";

                }
                else //Khi không có sản phẩm nào được thêm 
                    MessageBox.Show("Không có sản phẩm.");

            }
        }
    }
}
