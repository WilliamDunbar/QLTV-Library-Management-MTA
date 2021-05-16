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
    public partial class ThongTinMuonSach_DocGia : Form
    {
        public ThongTinMuonSach_DocGia()
        {
            InitializeComponent();
        }
        byte[] Byte_HinhAnh;
        private void ThongTinMuonSach_DocGia_Load(object sender, EventArgs e)
        {
            Load_DG();
            Load_SachDangMuon();
        }
        void Load_DG()
        {
            using (Model_QuanLi_ThuVien qltv = new Model_QuanLi_ThuVien())
            {
                DocGia DG = qltv.DocGias.Where(p => p.MaDocGia == Login.MaNguoiDung).FirstOrDefault();
                txbMaDG.Text = DG.MaDocGia;
                txbTenDG.Text = DG.TenDocGia;
                txbDonViDG.Text = DG.DonVi;
                txbLoaiDG.Text = DG.LoaiDocGia;
                txbSDTDG.Text = DG.SDT;
                dTP_NgaySinhDG.Text = DG.NgaySinh.ToString();
                dTP_NgayDK.Text = DG.NgayDK.ToString();
                dTP_NgayHetHan.Text = DG.NgayHetHanDK.ToString();
                Byte_HinhAnh = (byte[])DG.HinhAnh;
                ptbAnhDG.Image = ByteToImage((byte[])DG.HinhAnh);

                var listSoSachMuon = qltv.MuonTras.Where(p => p.MaDocGia == Login.MaNguoiDung && p.DaTra == false).ToList();
                txbSoSachMuon.Text = listSoSachMuon.Count.ToString();
                var listSoLuotViPham = from kq in qltv.XuLyViPhams where kq.MaDocGia == Login.MaNguoiDung select kq.LyDo;
                txbLuotViPham.Text = listSoLuotViPham.ToList().Count.ToString();
            }
        }

        void Load_NvChoMuon(string MaNV)
        {
            using (Model_QuanLi_ThuVien qltv = new Model_QuanLi_ThuVien())
            {
                txbMaNV_Muon.Text = MaNV;
                NhanVien NV = qltv.NhanViens.Where(p => p.MaNhanVien == MaNV).FirstOrDefault();
                ChucVu CV = qltv.ChucVus.Where(p => p.MaChucVu == NV.MaChucVu).FirstOrDefault();
                txbTenNV_Muon.Text = NV.HoTen;
                txbSDTNV_Muon.Text = NV.SDT;
                txbChucVuNV_Muon.Text = CV.TenChucVu;
            }          
        }

        void Load_CsDangMuon(string MaCS)
        {
            using (Model_QuanLi_ThuVien qltv = new Model_QuanLi_ThuVien())
            {
                txbMaSach.Text = MaCS;
                CuonSach CS = qltv.CuonSaches.Where(p => p.MaSach == MaCS).FirstOrDefault();
                DauSach DS = qltv.DauSaches.Where(p => p.MaDauSach == CS.MaDauSach).FirstOrDefault();                
                txbTenSach.Text = DS.TenDauSach;
                txbGiaTien.Text = DS.GiaTien.ToString();
                ptbAnhCS.Image = ByteToImage((byte[])DS.HinhAnh);
            }
        }
       
        void Load_SachDangMuon()
        {
            using (Model_QuanLi_ThuVien qltv = new Model_QuanLi_ThuVien())
            {
                SqlParameter param = new SqlParameter { ParameterName = "MaDocGia", Value = Login.MaNguoiDung };
                var listCuonSachDangMuon = qltv.Database.SqlQuery<TT_MuonSach_DocGia>($"exec Show_SoSachDocGiaDangMuon @MaDocGia",param);
                dtGV_SachChoMuon.DataSource = listCuonSachDangMuon.ToList();
            }
        }
        public static Image ByteToImage(byte[] arrImage)
        {
            if (arrImage == null)
            {
                MessageBox.Show("KO co anh");
                return null;
            }
            MemoryStream ms = new MemoryStream(arrImage, 0, arrImage.Length);
            ms.Write(arrImage, 0, arrImage.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }

        private void dtGV_SachChoMuon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            Load_CsDangMuon(dtGV_SachChoMuon.Rows[i].Cells[1].Value.ToString());
            Load_NvChoMuon(dtGV_SachChoMuon.Rows[i].Cells[2].Value.ToString());
            dtp_NgayMuon.Text = dtGV_SachChoMuon.Rows[i].Cells[3].Value.ToString();
            dtp_HanTra.Text = dtGV_SachChoMuon.Rows[i].Cells[4].Value.ToString();
        }
    }
}
