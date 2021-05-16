using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quan_Ly_Thu_Vien.Database;

namespace Quan_Ly_Thu_Vien
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void exitbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void xemMK_Click(object sender, EventArgs e)
        {
            if(txbMatKhau.UseSystemPasswordChar == true)
            {
                txbMatKhau.UseSystemPasswordChar = false;
            }
            else
            {
                txbMatKhau.UseSystemPasswordChar = true;
            }
            
        }

        public static string MaNguoiDung;
        public static bool ThuThuOrDocGia;
        private void btDangNhap_Click(object sender, EventArgs e)
        {
            using (Model_QuanLi_ThuVien qltv = new Model_QuanLi_ThuVien())
            {
                if (rdoBtThuThu.Checked == true)
                {

                    TaiKhoanNV tkNV = qltv.TaiKhoanNVs.Where(p => p.TenDangNhap == txbTenDangNhap.Text && p.MatKhau == txbMatKhau.Text).SingleOrDefault();

                    if (tkNV == null) MessageBox.Show("Ten dang nhap hoac mat khau khong dung!");
                    else
                    {
                        MaNguoiDung = tkNV.MaNhanVien;
                        ThuThuOrDocGia = true;
                        Form fr = new Trangchu();
                        this.Visible = false;
                        fr.ShowDialog();
                        this.Close();
                    }
                }

                else if (rdoBtDocGia.Checked == true)
                {
                    TaiKhoanDG tkDG = qltv.TaiKhoanDGs.Where(p => p.TenDangNhap == txbTenDangNhap.Text && p.MatKhau == txbMatKhau.Text).SingleOrDefault();
                    if (tkDG == null) MessageBox.Show("Ten dang nhap hoac mat khau khong dung!");
                    else
                    {
                        MaNguoiDung = tkDG.MaDocGia;
                        ThuThuOrDocGia = false;
                        Trangchu fr = new Trangchu();
                        fr.iconBtMuonSach.Visible = false;
                        fr.iconBtTraSach.Visible = false;
                        this.Visible = false;
                        fr.ShowDialog();
                        this.Close();
                    }
                }
            }
        }
    }
}
