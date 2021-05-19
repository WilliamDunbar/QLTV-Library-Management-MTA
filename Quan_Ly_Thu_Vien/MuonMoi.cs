using Quan_Ly_Thu_Vien.Database;
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

namespace Quan_Ly_Thu_Vien
{
    public partial class MuonMoi : Form
    {
        public MuonMoi()
        {
            InitializeComponent();
        }

        private void btBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void set_controlTT()
        {
            txtTenDG.Enabled = false;
            txtViPham.Enabled = false;
            txtTenCuonSach.Enabled = false;
            txtSachMuon.Enabled = false;
            cboTinhTrang.Enabled = false;

        }
        private void TT_bandau()
        {
            txtTenDG.Text = "";
            txtViPham.Text = "";
            txtTenCuonSach.Text = "";
            txtSachMuon.Text = "";
            cboTinhTrang.Text = "";

            dtpNgayMuon.Text = DateTime.Now.ToShortDateString();
            dtpNgayMuon.Enabled = false;
            dtpNgayTra.Text = DateTime.Now.ToShortDateString();
            txbMaDG.Text = "";
            txbMaCuonSach.Text = "";
        }
        private void MuonMoi_Load(object sender, EventArgs e)
        {
            set_controlTT();
            TT_bandau();
        }
       private  int Hieusongay(string ngaymuon, string ngaytra)
        {
            DateTime dt_NgayMuon = Convert.ToDateTime(ngaymuon);
            DateTime dt_NgayTra = Convert.ToDateTime(ngaytra);
            TimeSpan Time = dt_NgayTra-dt_NgayMuon;
            int TongSoNgay = Time.Days;
            return TongSoNgay;
        }
 
        private void muonsach()
        {
            if ((Convert.ToInt32(txtViPham.Text) >= 3) || (Convert.ToInt32(txtSachMuon.Text) >= 6))
            {
                MessageBox.Show("Không đủ điều kiện mượn sách");
              //  TT_bandau();
            }
            else
            {
                
                  if(Hieusongay( dtpNgayMuon.Text,dtpNgayTra.Text)>0)
                {
                    try
                    {
                        
                        if (cboTinhTrang.Text == "False")
                        {
                            MessageBox.Show("Cuốn sách đang cho mượn .Hãy mượn cuốn sách khác");
                           // TT_bandau();
                        }
                        else
                        {
                            MessageBox.Show("Đủ điều kiện mượn sách");
                            load_TTmuon();
                            btKiemTra.Enabled = false;
                            btnChoMuon0.Enabled = true;
                            //TT_bandau();
                        }
                 
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Lỗi Hệ thống");
                       // TT_bandau();
                    }
                }
                else { MessageBox.Show("Xem lại thời gian cho mượn"); }
            }

        }

        private void btKiemTra_Click(object sender, EventArgs e)
        {
            using (Model_QuanLi_ThuVien qltv = new Model_QuanLi_ThuVien())
            {
                if (txbMaDG.Text == "")
                {
                    MessageBox.Show("Bạn đã nhập thiếu thông tin độc giả");
                }
                else if (txbMaCuonSach.Text == "")
                {
                    MessageBox.Show("Bạn đã nhập thiếu thông tin cuốn sách");
                }
                else
                {  //////////////////////////
                    try
                    {   /// thông tin cuốn sach
                        ThongTinCuonSach TTCS = qltv.ThongTinCuonSaches.Where(p => p.MaSach == txbMaCuonSach.Text).SingleOrDefault();
                        ////////////////thông tin độc giả
                        var listSoLuotViPham = from kq in qltv.XuLyViPhams where kq.MaDocGia == txbMaDG.Text select kq.LyDo;
                        ThongTinDocGia TTDG = qltv.ThongTinDocGias.Where(p => p.MaDocGia == txbMaDG.Text).SingleOrDefault();
                        //////////////////////////////////
                        if (TTCS == null || TTDG == null)
                        { MessageBox.Show("Yêu cầu bạn nhập đúng thông tin mã độc giả và mã cuốn sách"); }
                        else
                        {
                            txtTenDG.Text = TTDG.TenDocGia.ToString();
                            txtTenCuonSach.Text = TTCS.TenDauSach.ToString();
                            cboTinhTrang.Text = TTCS.TinhTrang.ToString();
                            load_TTmuon();
                            muonsach();

                        }


                    }
                    catch (Exception)
                    {
                        MessageBox.Show("");
                    }
                }
            }

        }

        private void load_TTmuon()
        {
            Model_QuanLi_ThuVien qltv = new Model_QuanLi_ThuVien();
            SqlParameter idParam = new SqlParameter { ParameterName = "NoiDung", Value = txbMaDG.Text };
            //var lstSoSachMuon = qltv.ThongTinMuons.SqlQuery("TimKiemMaDG @NoiDung", idParam).ToList();
            var lstSoSachMuon = from kq in qltv.ThongTinMuons where kq.MaDocGia == txbMaDG.Text select kq.MaSach;
            txtSachMuon.Text = lstSoSachMuon.ToList().Count.ToString();
            var listSoLuotViPham = from kq in qltv.XuLyViPhams where kq.MaDocGia == txbMaDG.Text select kq.LyDo;
            txtViPham.Text = listSoLuotViPham.ToList().Count.ToString();

            var lstMuonTra = qltv.ThongTinMuons.SqlQuery("select * from ThongTinMuon where MaDocGia = @NoiDung ", idParam).ToList();
            dtgrdView_Muon.DataSource = lstMuonTra;
            ThongTinCuonSach TTCS = qltv.ThongTinCuonSaches.Where(p => p.MaSach == txbMaCuonSach.Text).SingleOrDefault();
            txtTenCuonSach.Text = TTCS.TenDauSach.ToString();
            cboTinhTrang.Text = TTCS.TinhTrang.ToString();

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
        private void btnChoMuon0_Click(object sender, EventArgs e)
        {
            {
                Model_QuanLi_ThuVien MtV2 = new Model_QuanLi_ThuVien();
                SqlParameter[] idParam =
                   { new SqlParameter { ParameterName="MaSach", Value=txbMaCuonSach.Text },
                new SqlParameter { ParameterName = "MaDocGia", Value =txbMaDG.Text },
                new SqlParameter { ParameterName = "MaNVMuon", Value = Login.MaNguoiDung },
                new SqlParameter { ParameterName = "NgayMuon", Value = dtpNgayMuon.Text  },
                 new SqlParameter { ParameterName = "NgayHanTra", Value = dtpNgayTra.Text },


                };
                //   int Check_Sach = MtV2.Database.ExecuteSqlCommand("MuonMoiSach @MaSach,@MaDocGia,@MaNVMuon,@NgayMuon,@NgayHanTra", idParam);
                int Check_Sach = MtV2.Database.ExecuteSqlCommand("MuonMoiSach @MaSach,@MaDocGia,@MaNVMuon,@NgayMuon,@NgayHanTra", idParam);
                if (Check_Sach >= 0)
                {
                    MessageBox.Show("cho mượn sách thành công ");
                    load_TTmuon();

                }
                btnChoMuon0.Enabled = false;
            }
        }

        private void btnHuy0_Click(object sender, EventArgs e)
        {
            TT_bandau();
            btnChoMuon0.Enabled = false;
            btKiemTra.Enabled = true;
        }

        private void dtgrdView_Muon_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
