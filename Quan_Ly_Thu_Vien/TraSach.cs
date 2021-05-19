using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quan_Ly_Thu_Vien.Database;
namespace Quan_Ly_Thu_Vien
{
    public partial class TraSach : Form
    {
        public TraSach()
        {
            InitializeComponent();
        }
        private string MaDG;
        private string MaMT, MaSach, NgayMuon, NgayHetHan;
        public static Image ByteToImage(byte[] arrImage)
        {
            if (arrImage == null)
            {
                return null;
            }
            MemoryStream ms = new MemoryStream(arrImage, 0, arrImage.Length);
            ms.Write(arrImage, 0, arrImage.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }
        private void btnTraSach1_Click(object sender, EventArgs e)
        {
            if (MaMT == null)
            {
                MessageBox.Show("Bạn hãy click vào cuốn sách cần trả");
            }
            else
            {
                Form formBackround = new Form();
                try
                {
                    formBackround.StartPosition = FormStartPosition.Manual;
                    formBackround.FormBorderStyle = FormBorderStyle.None;
                    formBackround.Opacity = .50d;
                    formBackround.BackColor = Color.WhiteSmoke;
                    formBackround.WindowState = FormWindowState.Maximized;
                    formBackround.TopMost = true;
                    using (ViPham gh = new ViPham(MaMT, NgayMuon, NgayHetHan, MaDG, MaSach))
                    {
                        gh.TopMost = true;
                        formBackround.Show();
                        gh.StartPosition = FormStartPosition.CenterScreen;
                        gh.Owner = formBackround;
                        gh.ShowDialog();
                        Load_DSMuon();
                        load_TTCaNhan();
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
                MaMT = null;
            }
        }
        private void load_TTCaNhan()
        {
            Model_QuanLi_ThuVien qltv = new Model_QuanLi_ThuVien();
            SqlParameter idParam = new SqlParameter { ParameterName = "NoiDung", Value = MaDG };
            //var lstSoSachMuon = qltv.ThongTinMuons.SqlQuery("TimKiemMaDG @NoiDung", idParam).ToList();
            var lstSoSachMuon = from kq in qltv.ThongTinMuons where kq.MaDocGia == MaDG select kq.MaSach;
            txtLuotMuon.Text = lstSoSachMuon.ToList().Count.ToString();
            var listSoLuotViPham = from kq in qltv.XuLyViPhams where kq.MaDocGia == MaDG select kq.LyDo;
            txtLuotViPham.Text = listSoLuotViPham.ToList().Count.ToString();
            DocGia TTDG = qltv.DocGias.Where(p => p.MaDocGia == MaDG).SingleOrDefault();
            //////////////////////////////////
            txtTenNguoiTra.Text = TTDG.TenDocGia;
            txtDonVi.Text = TTDG.DonVi;
            ptbAnhDG.Image = ByteToImage((byte[])TTDG.HinhAnh);
        }
        void Load_DSMuon()
        {
            Model_QuanLi_ThuVien qltv = new Model_QuanLi_ThuVien();
            var lstMuonTra = qltv.ThongTinMuons.SqlQuery("select * from ThongTinMuon").ToList();
            dtgrdView_Tra.DataSource = lstMuonTra;
            txtNDTimKiem.Text = "";
        }
        private void TraSach_Load(object sender, EventArgs e)
        {
            Load_DSMuon();
        }

        private void txtNDTimKiem_TextChanged(object sender, EventArgs e)
        {
            TT_dau();
            if (radMaDG.Checked)
            {

                SqlParameter idParam = new SqlParameter { ParameterName = "NoiDung", Value = txtNDTimKiem.Text };
                Model_QuanLi_ThuVien MtV1 = new Model_QuanLi_ThuVien();
                var lstTimkiemDocGia = MtV1.ThongTinMuons.SqlQuery("TimKiemMaDG @NoiDung", idParam).ToList();
                dtgrdView_Tra.DataSource = lstTimkiemDocGia;
                dtgrdView_Tra.AutoGenerateColumns = false;

            }
            else if (radMaSach.Checked)
            {


                SqlParameter idParam = new SqlParameter { ParameterName = "NoiDung", Value = txtNDTimKiem.Text };
                Model_QuanLi_ThuVien MtV1 = new Model_QuanLi_ThuVien();
                var lstTimkiemSach = MtV1.ThongTinMuons.SqlQuery("TimKiemMaSach @NoiDung", idParam).ToList();
                dtgrdView_Tra.DataSource = lstTimkiemSach;
                dtgrdView_Tra.AutoGenerateColumns = false;
            }
        }
        private void TT_dau()
        {
            txtTenNguoiTra.Text = "";
            txtLuotMuon.Text = "";
            txtLuotViPham.Text = "";
            txtTenNguoiTra.Text = "";
            txtDonVi.Text = "";
        }

        private void dtgrdView_Tra_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int i = e.RowIndex;
                MaMT = dtgrdView_Tra.Rows[i].Cells[0].Value.ToString();
                MaSach = dtgrdView_Tra.Rows[i].Cells[1].Value.ToString();
                MaDG = dtgrdView_Tra.Rows[i].Cells[2].Value.ToString();
                NgayMuon = dtgrdView_Tra.Rows[i].Cells[4].Value.ToString();
                NgayHetHan = dtgrdView_Tra.Rows[i].Cells[5].Value.ToString();
                load_TTCaNhan();
            }
            catch (Exception)
            {

            }
            
        }

    
    }
}
