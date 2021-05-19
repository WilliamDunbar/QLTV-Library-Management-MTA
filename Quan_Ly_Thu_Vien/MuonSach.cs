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
using System.Windows.Documents;
using System.Windows.Forms;
using Quan_Ly_Thu_Vien.Database;

namespace Quan_Ly_Thu_Vien
{
    public partial class MuonSach : Form
    {
        public MuonSach()
        {
            InitializeComponent();
        }
        
        private byte[] Byte_HinhAnh;
        private string MaDG,MaSach;
        private string NgayMuon, NgayTra;
        private string MaMT;
        private void btMuonMoi_Click(object sender, EventArgs e)
        {
            Form quanlycuonsach = new MuonMoi();

            quanlycuonsach.TopLevel = false;
            btnGiaHan.Controls.Add(quanlycuonsach);
            quanlycuonsach.Dock = DockStyle.Fill;
            btnGiaHan.Tag = quanlycuonsach;
            quanlycuonsach.BringToFront();
            quanlycuonsach.Show();
        }

        private void btnGiaHan_Click(object sender, EventArgs e)
        {
           
        }
        public void load_MuonTra()
        {

            Model_QuanLi_ThuVien MtV1 = new Model_QuanLi_ThuVien();
            var lstMuonTra = MtV1.ThongTinMuons.SqlQuery("select * from ThongTinMuon").ToList();
            dtgrdView_Muon.DataSource = lstMuonTra;
           

        }

        private void Bandau() 
        {
        }


        private byte[] ConvertImageToBytes(string text)
        {
            FileStream fs;
            fs = new FileStream(text, FileMode.Open, FileAccess.Read);
            byte[] picbyte = new byte[fs.Length];
            fs.Read(picbyte, 0, System.Convert.ToInt32(fs.Length));
            fs.Close();
            return picbyte;

        }
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
        private void ThongTin_MuonSach()
        {
            using (Model_QuanLi_ThuVien qltv = new Model_QuanLi_ThuVien())
            {
                CuonSach TTCS = qltv.CuonSaches.Where(p => p.MaSach == MaSach).SingleOrDefault();
                DauSach TTDS = qltv.DauSaches.Where(p => p.MaDauSach == TTCS.MaDauSach).SingleOrDefault();            
                DocGia TTDG = qltv.DocGias.Where(p => p.MaDocGia == MaDG).SingleOrDefault();
                //////////////////////////////////
                txtTenDG.Text = TTDG.TenDocGia;
                txtDonVi.Text = TTDG.DonVi;
                txtSDT.Text = TTDG.SDT;
                ptbAnhDG.Image = ByteToImage((byte[])TTDG.HinhAnh);
                ptbAnhDS.Image = ByteToImage((byte[])TTDS.HinhAnh);
                txbTenCuonSach.Text = TTDS.TenDauSach;
                txbSoTrang.Text = TTDS.SoTrang.ToString();
                txbGiaTien.Text = TTDS.GiaTien.ToString();
               ////////////////////////////////////////
               //xử lý hình ảnh
               //doi lai vieww thanh ten 


            } }

        private void dtgrdView_Muon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int i = e.RowIndex;
                MaMT = dtgrdView_Muon.Rows[i].Cells[0].Value.ToString();
                MaSach = dtgrdView_Muon.Rows[i].Cells[1].Value.ToString();
                MaDG = dtgrdView_Muon.Rows[i].Cells[2].Value.ToString();
                NgayMuon = dtgrdView_Muon.Rows[i].Cells[4].Value.ToString();
                NgayTra = dtgrdView_Muon.Rows[i].Cells[5].Value.ToString();
                ThongTin_MuonSach();
            }
            catch(Exception){

            }
           
            
        }



        private void btnGiaHan1_Click(object sender, EventArgs e)
        {
            if (MaMT == null)
            {
                MessageBox.Show("Bạn cần click vào cuốn sách  cần gia hạn");
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
                    using (GiaHan gh = new GiaHan(MaMT, NgayMuon, NgayTra))
                    {
                        gh.TopMost = true;
                        formBackround.Show();
                        gh.StartPosition = FormStartPosition.CenterScreen;
                        gh.Owner = formBackround;
                        gh.ShowDialog();


                    }
                    MaMT = null;
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
        }

        private void MuonSach_Load(object sender, EventArgs e)
        {
            load_MuonTra();
            ////////////////
            ///trang thai ban dau 
            radMaDG.Checked = true;
            btnGiaHan1.Enabled = true;
        }

        private void txtNDTimKiem_TextChanged(object sender, EventArgs e)
        {
            btMuonMoi.Enabled = false;
            
           

            if (radMaDG.Checked)
            {
                if (txtNDTimKiem.Text == "")
                {
                    btMuonMoi.Enabled = true;
                    btnGiaHan1.Enabled = false;

                }
                btnGiaHan1.Enabled = true;
                SqlParameter idParam = new SqlParameter { ParameterName = "NoiDung", Value = txtNDTimKiem.Text };
                Model_QuanLi_ThuVien MtV1 = new Model_QuanLi_ThuVien();
                var lstTimkiemDocGia = MtV1.ThongTinMuons.SqlQuery("TimKiemMaDG @NoiDung", idParam).ToList();
                dtgrdView_Muon.DataSource = lstTimkiemDocGia;
              //  dtgrdView_Muon.AutoGenerateColumns = false;

            }
            else if (radMaSach.Checked)
            {
                if (txtNDTimKiem.Text == "")
                {
                    btMuonMoi.Enabled = true;
                    btnGiaHan1.Enabled = false;

                }
              
                SqlParameter idParam = new SqlParameter { ParameterName = "NoiDung", Value = txtNDTimKiem.Text };
                Model_QuanLi_ThuVien MtV1 = new Model_QuanLi_ThuVien();
                var lstTimkiemSach = MtV1.ThongTinMuons.SqlQuery("TimKiemMaSach @NoiDung", idParam).ToList();
                dtgrdView_Muon.DataSource = lstTimkiemSach;
             //   dtgrdView_Muon.AutoGenerateColumns = false;
            }
        }

        private void radMaDG_CheckedChanged(object sender, EventArgs e)
        {
           
        }
    }
}
