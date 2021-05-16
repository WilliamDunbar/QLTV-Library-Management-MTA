using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quan_Ly_Thu_Vien.Database;
namespace Quan_Ly_Thu_Vien
{
    public partial class Trangchu : Form
    {
        private IconButton currentButton;
        private Panel leftBorderButton;
        private Form currentChildForm;
        private int ImageNumber = 1;

        public Trangchu()
        {
            InitializeComponent();
            
        }

        private void exitbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FullScreen_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal) WindowState = FormWindowState.Maximized;
            else WindowState = FormWindowState.Normal;
        }

        #region Move
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void ScreenMove(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        #endregion Move

        private void iconBt_DocGia_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            if (Login.ThuThuOrDocGia == true)
            {
                OpenChildForm(new QuanLyDocGia_NV());
            }
            else
            {
                OpenChildForm(new ThongTinMuonSach_DocGia());
            }
        }

        private void iconBt_Sach_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            if(Login.ThuThuOrDocGia == true)
            {
                OpenChildForm(new QuanLyDauSach_NV());
            }
            else
            {
                OpenChildForm(new QuanLyDauSach_DG());
            }
            
        }

        private void iconBtMuonSach_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            OpenChildForm(new MuonSach());
        }

        private void iconBtTraSach_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            OpenChildForm(new TraSach());
        }

        private void iconBtThongKe_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            OpenChildForm(new ThongKe());
        }

        private void iconBtDangXuat_Click(object sender, EventArgs e)
        {

        }
        //************
        private void DisableButton()
        {
            if (currentButton != null)
            {
                currentButton.BackColor = Color.FromArgb(92, 179, 99);
                currentButton.IconColor = Color.WhiteSmoke;
                currentButton.ImageAlign = ContentAlignment.MiddleCenter;
            }
        }
        private void ActivateButton(object senderbutton)
        {
            if (senderbutton != null)
            {
                DisableButton();
                currentButton = (IconButton)senderbutton;
                currentButton.BackColor = Color.WhiteSmoke;
                currentButton.IconColor = Color.FromArgb(92, 179, 99);
                currentButton.ImageAlign = ContentAlignment.MiddleCenter;
                //Left border button
                //leftBorderButton.BackColor = Color.FromArgb(92, 179, 99);
                //leftBorderButton.Location = new Point(0, currentButton.Location.Y);
                //leftBorderButton.Visible = true;
                //leftBorderButton.BringToFront();
                ////Current Child Form Icon
                //currentButton.IconChar = currentButton.IconChar;
                //currentButton.IconColor = color;

            }
        }
        private void Reset()
        {
            DisableButton();

        }
        private void MTA_Click(object sender, EventArgs e)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            Reset();
        }
        private void OpenChildForm(Form childForm)
        {
            //open only form
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            //End
            childForm.TopLevel = false;
            //childForm.FormBorderStyle = FormBorderStyle.None;

            panelDesktop.Controls.Add(childForm);
            panelDesktop.Tag = childForm;
            panelDesktop.ForeColor = childForm.ForeColor;
            childForm.BringToFront();
            childForm.Dock = DockStyle.Fill;
            childForm.Show();
        }

        private void accountChip_Click(object sender, EventArgs e)
        {
            Form formBackround = new Form();
            try
            {
                formBackround.StartPosition = FormStartPosition.Manual;
                formBackround.FormBorderStyle = FormBorderStyle.None;
                formBackround.Opacity = .0d;
                formBackround.BackColor = Color.FromArgb(115, 249, 181);
                formBackround.WindowState = FormWindowState.Maximized;
                formBackround.TopMost = true;
                formBackround.Show();
                using (TaiKhoan tk = new TaiKhoan())
                {
                    if (this.WindowState == FormWindowState.Normal) tk.Location = new Point(1550, 145);
                    else tk.Location = new Point(1600, 125);
                    if(Login.ThuThuOrDocGia == false)
                    {
                        tk.BtDangKyTK.Visible = false;
                    }
                    tk.Owner = formBackround;
                    tk.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                formBackround.Dispose();
            }
        }

        private void Trangchu_Load(object sender, EventArgs e)
        {
            using (Model_QuanLi_ThuVien qltv = new Model_QuanLi_ThuVien())
            {
                if(Login.ThuThuOrDocGia == true)
                {
                    NhanVien NV = qltv.NhanViens.Where(p => p.MaNhanVien == Login.MaNguoiDung).FirstOrDefault();
                    accountChip.Text = NV.HoTen;
                }
                else
                {
                    DocGia DG = qltv.DocGias.Where(p => p.MaDocGia == Login.MaNguoiDung).FirstOrDefault();
                    accountChip.Text = DG.TenDocGia;
                }

               
            }
        }
        //************

        private void Slider()
        {
            if(ImageNumber == 4)
            {
                ImageNumber = 1;
            }
            SliderPicture.ImageLocation = string.Format(@"D:\Quan_ly_thu_vien\QLTV-Library-Management-MTA\Quan_Ly_Thu_Vien\Picture\{0}.jpg", ImageNumber);
            ImageNumber++;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Slider();
        }
    }
}
