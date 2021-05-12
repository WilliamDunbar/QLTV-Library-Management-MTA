using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_Ly_Thu_Vien
{
    public partial class TaiKhoan : Form
    {
        public TaiKhoan()
        {
            InitializeComponent();
        }

        private void btThongTinNV_Click(object sender, EventArgs e)
        {
            //Pop-up Form
            Form formBackround = new Form();
            try
            {
                formBackround.StartPosition = FormStartPosition.Manual;
                formBackround.FormBorderStyle = FormBorderStyle.None;
                formBackround.Opacity = .50d;
                formBackround.BackColor = Color.WhiteSmoke;
                formBackround.WindowState = FormWindowState.Maximized;
                formBackround.TopMost = true;
                if(DangNhap.ThuThuOrDocGia == true)
                {
                    using (ThayDoiThongTinNhanVIen ttnv = new ThayDoiThongTinNhanVIen())
                    {

                        ttnv.TopMost = true;
                        formBackround.Show();
                        ttnv.StartPosition = FormStartPosition.CenterScreen;
                        ttnv.Owner = formBackround;
                        ttnv.ShowDialog();
                    }
                }
                else
                {
                    using (ThayDoiThongTinDocGia ttdg = new ThayDoiThongTinDocGia())
                    {
                        ttdg.TopMost = true;
                        formBackround.Show();
                        ttdg.StartPosition = FormStartPosition.CenterScreen;
                        ttdg.Owner = formBackround;
                        ttdg.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                formBackround.Dispose();
                this.Close();
            }
        }

        private void btThayDoiMK_Click(object sender, EventArgs e)
        {
            Form formBackround = new Form();
            try
            {
                using (DoiMatKhau dmk = new DoiMatKhau())
                {
                    formBackround.StartPosition = FormStartPosition.Manual;
                    formBackround.FormBorderStyle = FormBorderStyle.None;
                    formBackround.Opacity = .50d;
                    formBackround.BackColor = Color.WhiteSmoke;
                    formBackround.WindowState = FormWindowState.Maximized;
                    formBackround.TopMost = true;
                    dmk.TopMost = true;
                    formBackround.Show();
                    dmk.StartPosition = FormStartPosition.CenterScreen;
                    dmk.Owner = formBackround;
                    dmk.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                formBackround.Dispose();
                this.Close();
            }
        }

        private void BtDangKyTK_Click(object sender, EventArgs e)
        {
            Form formBackround = new Form();
            try
            {
                using (DangKyNhanVien dknv = new DangKyNhanVien())
                {
                    formBackround.StartPosition = FormStartPosition.Manual;
                    formBackround.FormBorderStyle = FormBorderStyle.None;
                    formBackround.Opacity = .50d;
                    formBackround.BackColor = Color.WhiteSmoke;
                    formBackround.WindowState = FormWindowState.Maximized;
                    formBackround.TopMost = true;
                    dknv.TopMost = true;
                    formBackround.Show();
                    dknv.StartPosition = FormStartPosition.CenterScreen;
                    dknv.Owner = formBackround;
                    dknv.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                formBackround.Dispose();
                this.Close();
            }

        }

        private void btThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
