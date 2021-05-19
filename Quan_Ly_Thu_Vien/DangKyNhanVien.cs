using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quan_Ly_Thu_Vien.Database;
namespace Quan_Ly_Thu_Vien
{
    public partial class DangKyNhanVien : Form
    {
        public DangKyNhanVien()
        {
            InitializeComponent();
        }


        private void txbXacNhanMK_Validating(object sender, CancelEventArgs e)
        {
            string source = txbMatKhau.Text;
            string destination = txbXacNhanMK.Text;
            if (source.Equals(destination)) errorProvider1.Clear();
            else
            {
                e.Cancel = true;
                txbXacNhanMK.Focus();
                errorProvider1.SetError(txbXacNhanMK, "Mật khẩu không trùng khớp :( ");
            }
        }

        private void txbSDTNV_Validating(object sender, CancelEventArgs e)
        {
            Regex invalidCharsRegex = new Regex(@"^[0-9]+$");
            string text = txbSDTNV.Text;
            bool result = invalidCharsRegex.IsMatch(text);
            if (result == false)
            {
                e.Cancel = true;
                txbSDTNV.Focus();
                errorProvider1.SetError(txbSDTNV, "Số điện thoại chỉ gồm số !");
            }
            else errorProvider1.Clear();
        }

        private void txbTenNV_Validating(object sender, CancelEventArgs e)
        {
            Regex invalidCharsRegex = new Regex(@"^[\D \s ]+$");
            string text = txbTenNV.Text;
            bool result = invalidCharsRegex.IsMatch(text);
            if (result == false)
            {
                e.Cancel = true;
                txbTenNV.Focus();
                errorProvider1.SetError(txbTenNV, "Chỉ gồm các chữ cái !");
            }
            else errorProvider1.Clear();
        }

        private void txbTenDangNhap_Validating(object sender, CancelEventArgs e)
        {
            Regex invalidCharsRegex = new Regex(@"^[a-zA-Z0-9\s ]+$");
            string text = txbTenDangNhap.Text;
            bool result = invalidCharsRegex.IsMatch(text);
            if (result == false)
            {
                e.Cancel = true;
                txbTenDangNhap.Focus();
                errorProvider1.SetError(txbTenDangNhap, "Chỉ gồm số và chữ cái !");
            }
            else errorProvider1.Clear();
        }

        private void btDangKy_Click(object sender, EventArgs e)
        {
            using (Model_QuanLi_ThuVien qltv = new Model_QuanLi_ThuVien())
            {
                var listMaNV = from kq in qltv.NhanViens select kq.MaNhanVien;
                bool tmt = false;
                var listTenDN = from kq in qltv.TaiKhoanNVs select kq.TenDangNhap;
                bool tmt1 = false;
                foreach ( var item in listMaNV){if (txbMaNV.Text == item) tmt = true; }
                foreach (var item in listTenDN){  if (txbTenDangNhap.Text == item) tmt1 = true; }
                if (txbMaNV.Text == "") MessageBox.Show("Chua nhap ma nhan vien!");
                else if (txbTenNV.Text == "") MessageBox.Show("Chua nhap ten nhan vien!");
                else if (dTP_NgaySinhNV.Text == "") MessageBox.Show("Chua nhap ngay sinh nhan vien!");
                else if (cbbChucVu.Text == "") MessageBox.Show("Chua nhap chuc vu nhan vien!");
                else if (txbSDTNV.Text == "") MessageBox.Show("Chua nhap SDT nhan vien!");
                else if (txbTenDangNhap.Text == "") MessageBox.Show("Chua nhap ten dang nhap!");
                else if (txbMatKhau.Text == "") MessageBox.Show("Chua nhap mat khau!");
                else if (txbXacNhanMK.Text == "") MessageBox.Show("Chua nhap lai mat khau!");
                else if (txbMatKhau.Text != txbXacNhanMK.Text) MessageBox.Show("Mat khau nhap lai khong dung!");
                else if (tmt == true) MessageBox.Show("Ma nha vien da ton tai!");
                else if (tmt1 == true) MessageBox.Show("Ten dang nhap da ton tai");
                else
                {
                    ChucVu tkNV = qltv.ChucVus.Where(p => p.TenChucVu == cbbChucVu.Text).FirstOrDefault();
                    qltv.Database.ExecuteSqlCommand($"exec ThemNhanVien '{txbMaNV.Text}',N'{txbTenNV.Text}','{dTP_NgaySinhNV.Text}','{txbSDTNV.Text}','{tkNV.MaChucVu}'");
                    qltv.Database.ExecuteSqlCommand($"exec ThemTaiKhoanNV '{txbMaNV.Text}','{txbTenDangNhap.Text}','{txbMatKhau.Text}'");
                    MessageBox.Show("Dang ki thanh cong");
                }
            }
        }

        private void cbbChucVu_MouseClick(object sender, MouseEventArgs e)
        {
            using (Model_QuanLi_ThuVien qltv = new Model_QuanLi_ThuVien())
            {
                var listChucVu = from kq in qltv.ChucVus select kq.TenChucVu;
                cbbChucVu.DataSource = listChucVu.ToList();
            }
        }

        private void btHuy_Click(object sender, EventArgs e)
        {
            txbMaNV.Text = "";
            txbTenNV.Text = "";
            dTP_NgaySinhNV.Text = "";
            cbbChucVu.Text = "";
            txbSDTNV.Text = "";
            txbTenDangNhap.Text = "";
            txbMatKhau.Text = "";
            txbXacNhanMK.Text = "";
        }

        private void btThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
