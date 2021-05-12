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
    public partial class DoiMatKhau : Form
    {
        public DoiMatKhau()
        {
            InitializeComponent();
        }

        private void btThoat_Click(object sender, EventArgs e)
        {    
            this.Close();
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
            else
            {

                errorProvider1.Clear();

            }
        }

        private void txbXacNhanMK_Validating(object sender, CancelEventArgs e)
        {
            //string source = txbMatKhaumoi.Text;
            //string destination = txbXacNhanMK.Text;
            //if (source.Equals(destination))
            //{
            //    errorProvider1.Clear();
            //}
            //else
            //{
            //    e.Cancel = true;
            //    txbXacNhanMK.Focus();
            //    errorProvider1.SetError(txbXacNhanMK, "Mật khẩu không trùng khớp :( ");
            //}
        }



        private void xemMKcu_Click(object sender, EventArgs e)
        {
     
            if (txbMkCu.UseSystemPasswordChar == true)
            {
                
                txbMkCu.UseSystemPasswordChar = false;
            }
            else txbMkCu.UseSystemPasswordChar = true;
        }

        private void xemMKmoi_Click(object sender, EventArgs e)
        {
            if (txbMkMoi.UseSystemPasswordChar == true)
            {

                txbMkMoi.UseSystemPasswordChar = false;
            }
            else txbMkMoi.UseSystemPasswordChar = true;
        }

        private void xemMKxacnhan_Click(object sender, EventArgs e)
        {
            if (txbNhapLaiMk.UseSystemPasswordChar == true)
            {

                txbNhapLaiMk.UseSystemPasswordChar = false;
            }
            else txbNhapLaiMk.UseSystemPasswordChar = true;
        }

        private void nhap_Click(object sender, EventArgs e)
        {
            if (txbMkCu.UseSystemPasswordChar == true) txbMkCu.UseSystemPasswordChar = false;
            else txbMkCu.UseSystemPasswordChar = true;
        }

        private void btLuu_Click(object sender, EventArgs e)
        {
            using (Model_QuanLi_ThuVien qltv = new Model_QuanLi_ThuVien())
            {

                if (DangNhap.ThuThuOrDocGia == true)
                {
                    TaiKhoanNV tkNV = qltv.TaiKhoanNVs.Where(p => p.MaNhanVien == DangNhap.MaNguoiDung).FirstOrDefault();
                    if (txbTenDangNhap.Text == "") MessageBox.Show("Chua nhap ten tai khoan!");
                    else if (txbMkCu.Text == "") MessageBox.Show("Chua nhap mat khau cu!");
                    else if (txbMkMoi.Text == "") MessageBox.Show("Chua nhap mat khau moi!");
                    else if (txbNhapLaiMk.Text == "") MessageBox.Show("Chua nhap lai mat khau moi!");
                    else if (txbTenDangNhap.Text != tkNV.TenDangNhap || txbMkCu.Text != tkNV.MatKhau) MessageBox.Show("Ten dang nhap hoac mat khau khong dung!");
                    else if (txbMkMoi.Text != txbNhapLaiMk.Text) MessageBox.Show("Nhap mat khau moi khong dung!");
                    else
                    {
                        qltv.Database.ExecuteSqlCommand($"exec SuaTaiKhoanNV '{DangNhap.MaNguoiDung}','{txbMkMoi.Text}'");
                        MessageBox.Show("Doi mat khau thanh cong!");
                    }
                }
                else if (DangNhap.ThuThuOrDocGia == false)
                {
                    TaiKhoanDG tkDG = qltv.TaiKhoanDGs.Where(p => p.MaDocGia == DangNhap.MaNguoiDung).FirstOrDefault();
                    if (txbTenDangNhap.Text == "") MessageBox.Show("Chua nhap ten tai khoan!");
                    else if (txbMkCu.Text == "") MessageBox.Show("Chua nhap mat khau cu!");
                    else if (txbMkMoi.Text == "") MessageBox.Show("Chua nhap mat khau moi!");
                    else if (txbNhapLaiMk.Text == "") MessageBox.Show("Chua nhap lai mat khau moi!");
                    else if (txbTenDangNhap.Text != tkDG.TenDangNhap || txbMkCu.Text != tkDG.MatKhau) MessageBox.Show("Ten dang nhap hoac mat khau khong dung!");
                    else if (txbMkMoi.Text != txbNhapLaiMk.Text) MessageBox.Show("Nhap mat khau moi khong dung!");
                    else
                    {
                        qltv.Database.ExecuteSqlCommand($"exec SuaTaiKhoanDG '{DangNhap.MaNguoiDung}','{txbMkMoi.Text}'");
                        MessageBox.Show("Doi mat khau thanh cong!");
                    }
                }
            }
        }

        private void btHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
