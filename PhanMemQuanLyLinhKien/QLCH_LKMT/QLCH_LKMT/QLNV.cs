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
    public partial class QLNV : Form
    {
        private DataTable dt = new DataTable();
        private BAL_NV xuLyNV = new BAL_NV();
        public QLNV()
        {
            InitializeComponent();
        }

       

        private void HienThiDSNhanVien(DataGridView dgr)
        {
            
            dt = xuLyNV.layDanhSachNhanVien();
            dgr.DataSource = dt ;
        }

        private void QLNV_Load(object sender, EventArgs e)
        {
            HienThiDSNhanVien(dataGridView1);
            cboLoaiNV.Items.Add("Quản Lý");
            cboLoaiNV.Items.Add("Thu Ngân");
            cboLoaiNV.Items.Add("Thủ Kho");

            cboTimKiem.Items.Add("Tất Cả");
            cboTimKiem.Items.Add("Quản Lý");
            cboTimKiem.Items.Add("Thu Ngân");
            cboTimKiem.Items.Add("Thủ Kho");

            cboTimKiem.SelectedItem = cboTimKiem.Items[0];
            
        }
        public string[] GetNgayThang(string date)
        {
            string[] ngayThang = date.Split("/".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            return ngayThang;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //tạo dữ liệu
            string[] tmp = GetNgayThang(dateTimePicker1.Text);
            
            string maNV = txtMaNV.Text;
            string tenNV = txtHoTen.Text;
            DateTime ngSinh = new DateTime(int.Parse(tmp[2]), int.Parse(tmp[0]), int.Parse(tmp[1]));
                
            string gioiTinh;
            string diaChi = rtbDiaChi.Text;
            string SDT = txtSDT.Text;
            float luong;
            if (txtLuong.Text == "")
                luong = 0;
            else
                luong = float.Parse(txtLuong.Text);
            BEL_TK tk = new BEL_TK(txtTK.Text, txtMK.Text);

            string loaiNV = cboLoaiNV.Text;
            if (rdbNam.Checked == true)
                gioiTinh = "Nam";
            else
                gioiTinh = "Nữ";

            //xử lý thêm nhân viên
                BEL_NV nhanVien = new BEL_NV(maNV, tenNV, ngSinh, gioiTinh, diaChi, SDT, luong, tk, loaiNV);
            if (xuLyNV.ThemNhanVien(nhanVien))
            {
                ThemVaoDataTable(nhanVien);
                dataGridView1.DataSource = dt;
                MessageBox.Show("Đã Thêm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            else
            {
                MessageBox.Show("Thêm Thất Bại. Có thể bạn đã bỏ trống dữ liệu nào đó.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Question);

            }

        }
        public void ThemVaoDataTable(BEL_NV nhanVien)
        {
            DataRow row = dt.NewRow();
            row[0] = nhanVien._maNV;
            row[1] = nhanVien._tenNV;
            row[2] = nhanVien._ngSinh;
            row[3] = nhanVien._gioiTinh;
            row[4] = nhanVien._diaChi;
            row[5] = nhanVien._SDT;
            row[6] = nhanVien._luong;
            row[7] = nhanVien._tk._taiKhoan;
            row[8] = nhanVien._tk._matKhau;
            row[9] = nhanVien._loaiNV;
            dt.Rows.Add(row);
        }
        public void CapNhatDataTable(BEL_NV nhanVien)
        {
            int index = dataGridView1.CurrentRow.Index;

            
          
            dt.Rows[index]["TEN_NV"] = nhanVien._tenNV;
            dt.Rows[index]["NGSINH"] = nhanVien._ngSinh;
            dt.Rows[index]["PHAI"] = nhanVien._gioiTinh;
            dt.Rows[index]["DIACHI"] = nhanVien._diaChi;
            dt.Rows[index]["SDT"] = nhanVien._SDT;
            dt.Rows[index]["LUONG"] = nhanVien._luong;
            dt.Rows[index]["TAIKHOAN"] = nhanVien._tk._taiKhoan;
            dt.Rows[index]["MATKHAU"] = nhanVien._tk._matKhau;
            dt.Rows[index]["LOAI"] = nhanVien._loaiNV;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult thoat = MessageBox.Show("Bạn có chắc muốn thoát ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (thoat == DialogResult.Yes)
            {
                this.Close();
            }

        }

      

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtMaNV.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                txtHoTen.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                txtTK.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
                txtMK.Text = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();
                txtSDT.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                txtLuong.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
                rtbDiaChi.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                dateTimePicker1.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                string chucVu = dataGridView1.SelectedRows[0].Cells[9].Value.ToString();
                string gioiTinh = dataGridView1.SelectedRows[0].Cells[3].Value.ToString().Trim();
                //string[] ngayThang = dataGridView1.SelectedRows[0].Cells[2].Value.ToString().Trim().Split("/".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                //DateTime ngaySinh = new DateTime(int.Parse(ngayThang[2]),int.Parse(ngayThang[0]),int.Parse(ngayThang[0]));
                ////dateTimePicker1.Value = ngaySinh;


                if (gioiTinh == "Nam")
                    rdbNam.Checked = true;
                else
                    rdbNu.Checked = true;

                if (chucVu == "admin")
                    cboLoaiNV.Text = cboLoaiNV.Items[0].ToString();
                else if (chucVu == "thukho")
                    cboLoaiNV.Text = cboLoaiNV.Items[2].ToString();
                else
                    cboLoaiNV.Text = cboLoaiNV.Items[1].ToString();
            }
            catch(Exception err)
            {

            }
            btnTaoMoi.Enabled = true;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            btnAdd.Enabled = false;

        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            txtMaNV.Text = xuLyNV.LayMaNhanVienMoi(dt.Rows.Count);

            //txtHoTen.Text = "";
            //txtLuong.Text = "";
            //txtMK.Text = "";
            //txtSDT.Text = "";
            //txtTK.Text = "";
            //rtbDiaChi.Text = "";
            //Làm khuyết các nút button
            btnAdd.Enabled = true;
            btnTaoMoi.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //xóa nhân viên dưới DB
            if (xuLyNV.XoaNhanVien(txtMaNV.Text))
            {
                //Xóa Nhân viên trong DataTable
                dt.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                dataGridView1.DataSource = dt;
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
            //tạo dữ liệu
            
            string[] tmp = GetNgayThang(dateTimePicker1.Text);

            string maNV = txtMaNV.Text;
            string tenNV = txtHoTen.Text;
            DateTime ngSinh = new DateTime(int.Parse(tmp[2]), int.Parse(tmp[0]), int.Parse(tmp[1]));

            string gioiTinh;
            string diaChi = rtbDiaChi.Text;
            string SDT = txtSDT.Text;
            float luong;
            if (txtLuong.Text == "")
                luong = 0;
            else
                luong = float.Parse(txtLuong.Text);
            BEL_TK tk = new BEL_TK(txtTK.Text, txtMK.Text);

            string loaiNV = cboLoaiNV.Text;
            if (rdbNam.Checked == true)
                gioiTinh = "Nam";
            else
                gioiTinh = "Nữ";

            //xử lý thêm nhân viên
            BEL_NV nhanVien = new BEL_NV(maNV, tenNV, ngSinh, gioiTinh, diaChi, SDT, luong, tk, loaiNV);
            if (xuLyNV.CapNhatNhanVien(nhanVien))
            {
                CapNhatDataTable(nhanVien);
                dataGridView1.DataSource = dt;
                MessageBox.Show("Đã cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại. Có thể bạn đã bỏ trống dữ liệu nào đó.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Question);

            }
            //btnAdd.Enabled = false;
            //btnTaoMoi.Enabled = true;
            //btnUpdate.Enabled = true;
            //btnDelete.Enabled = true;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {

        }

        public void SelectData(DataTable pData, string query)
        {
            DataRow[] Rows = pData.Select(query);

            pData = ((DataTable)dataGridView1.DataSource).Clone();
            for(int i = 0; i < Rows.Length; i++)
            {
                DataRow row = pData.NewRow();
                for (int j = 0; j < pData.Columns.Count; j++)
                {
                    row[j] = Rows[i].ItemArray[j].ToString();
                }
                pData.Rows.Add(row);

            }
            dataGridView1.DataSource = pData;
        }

        private void cboTimKiem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cboTimKiem.Text == "Tất Cả")
            {
                dataGridView1.DataSource = dt;
            }
            else if(cboTimKiem.Text == "Quản Lý")
            {
                SelectData(dt, "LOAI ='admin'");
            }
            else if(cboTimKiem.Text == "Thủ Kho")
            {
                SelectData(dt, "LOAI ='ThuKho'");
            }
            else if(cboTimKiem.Text == "Thu Ngân")
            {
                SelectData(dt, "LOAI ='ThuNgan'");
            }
        }
    }
}
