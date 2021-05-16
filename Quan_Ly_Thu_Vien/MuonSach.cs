using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
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
        public int xuly;
        public string MaMT0, MaSach0, MaDG0, NgayMuon0, HanTra0;
        public int hieumuon, hieutra, catthangmuon, catngaymuon, catngaytra, catthangtra, songaymuon, sothangmuon, sonammuon, songaytra, sothangtra, sonamtra, kq;



        private void cboMaDG0_TextChanged(object sender, EventArgs e)
        {
            Model_QuanLi_ThuVien View_SachMuon = new Model_QuanLi_ThuVien();
            txtMaDocGia.Text = cboMaDG0.Text;
            ////////////
            
            var listSoSachMuon = View_SachMuon.MuonTras.Where(p => p.MaDocGia == cboMaDG0.Text && p.DaTra == false).ToList();
            txtSachMuon.Text = listSoSachMuon.Count.ToString();

            /////////////
            var listSoLuotViPham = from kq in View_SachMuon.XuLyViPhams where kq.MaDocGia == cboMaDG0.Text select kq.LyDo;
            txtViPham.Text = listSoLuotViPham.ToList().Count.ToString();
        }

        private void txtNDTimKiem_TextChanged(object sender, EventArgs e)
        {
            
            btnNhap.Enabled = false;
            btnChoMuon0.Enabled = false;
            btnHuy0.Enabled = false;
            if (radMaDG.Checked)
            {
                if (txtNDTimKiem.Text == "")
                {
                    btnNhap.Enabled = true; btnGiaHan.Enabled = true;
                }
                SqlParameter idParam = new SqlParameter { ParameterName = "NoiDung", Value = txtNDTimKiem.Text };
                Model_QuanLi_ThuVien MtV1 = new Model_QuanLi_ThuVien();
                var lstTimkiemDocGia = MtV1.ThongTinMuonTras.SqlQuery("TimKiemMaDG @NoiDung", idParam).ToList();
                dtgrdView_Muon.DataSource = lstTimkiemDocGia;
                dtgrdView_Muon.AutoGenerateColumns = false;

            }
            else if (radMaSach.Checked)
            {
                if (txtNDTimKiem.Text == "")
                {
                    btnNhap.Enabled = true; btnGiaHan.Enabled = true;
                }
                SqlParameter idParam = new SqlParameter { ParameterName = "NoiDung", Value = txtNDTimKiem.Text };
                Model_QuanLi_ThuVien MtV1 = new Model_QuanLi_ThuVien();
                var lstTimkiemSach = MtV1.ThongTinMuonTras.SqlQuery("TimKiemMaSach @NoiDung", idParam).ToList();
                dtgrdView_Muon.DataSource = lstTimkiemSach;
                dtgrdView_Muon.AutoGenerateColumns = false;
            }
        }

        private void MuonSach_Load(object sender, EventArgs e)
        {
            load_MuonTra();
            layMaSach_DocGia();

            setControlsMuon(false);

            cboMaSach0.Text = "";
            cboMaDG0.Text = "";
            MaNVMtxt0.Text = "";
            MuonTratxt0.Text = "";
            MuonTratxt0.Enabled = false;
            btnHuy0.Enabled = true;
            btnGiaHan.Enabled = false;
            //  cboTinhTrang0.Text = "False";
            //  cboTinhTrang0.Enabled = false;
            btnChoMuon0.Text = "Cho Mượn";
            btnChoMuon0.TextAlign = System.Windows.Forms.HorizontalAlignment.Center; ;
            btnChoMuon0.Enabled = true;
            xuly = 0;
            radMaDG.Checked = true;

        }


        public string ngaymuon, thangmuon, nammuon, ngaytra, thangtra, namtra, ngaydgmuon, ngaydgtra;

        private void timkiembutton_Click(object sender, EventArgs e)
        {

        }

        public MuonSach()
        {
            InitializeComponent();
        }

        private void setControlsMuon(bool edit)
        {
            MuonTratxt0.Enabled = edit;
            cboMaSach0.Enabled = edit;
            cboMaDG0.Enabled = edit;
            MaNVMtxt0.Enabled = edit;
            dtmNgayHetHan0.Enabled = edit;
            //  cboTinhTrang0.Enabled = edit;
            dtmNgayMuon0.Enabled = edit;
        }
        private void load_MuonTra()
        {

            Model_QuanLi_ThuVien MtV1 = new Model_QuanLi_ThuVien();
            var lstMuonTra = MtV1.ThongTinMuonTras.SqlQuery("select * from ThongTinMuonTra").ToList();
            dtgrdView_Muon.DataSource = lstMuonTra;

            //dtgrdView_Tra.DataSource = lstMuonTra;


        }

        private int SoSanhHanTra(string hantra, string ngaytra1)
        {
            catthangmuon = ngaytra1.IndexOf("/"); // tong so ki tu truoc / 

            thangmuon = ngaytra1.Substring(0, catthangmuon);// tra ve 1 chuoi co do dai length bat dau vi tri 0

            catngaymuon = ngaytra1.LastIndexOf("/"); // so ki tu truoc / cuoi cung

            hieumuon = (catngaymuon - 1) - catthangmuon;

            ngaymuon = ngaytra1.Substring(catthangmuon + 1, hieumuon);
            nammuon = ngaytra1.Substring(catngaymuon + 1, 4);
            // MessageBox.Show("Ngay Muon " + ngaymuon + "Thang Muon " + thangmuon + "NamMuon" + nammuon);
            songaymuon = Convert.ToInt32(ngaymuon);
            sothangmuon = Convert.ToInt32(thangmuon);
            sonammuon = Convert.ToInt32(nammuon);

            catthangtra = hantra.IndexOf("/");
            thangtra = hantra.Substring(0, catthangtra);
            catngaytra = hantra.LastIndexOf("/");
            hieutra = (catngaytra - 1) - catthangtra;
            ngaytra = hantra.Substring(catthangtra + 1, hieutra);
            namtra = hantra.Substring(catngaytra + 1, 4);

            songaytra = Convert.ToInt32(ngaytra);
            sothangtra = Convert.ToInt32(thangtra);
            sonamtra = Convert.ToInt32(namtra);

            DateTime tgMuon = new DateTime(sonammuon, sothangmuon, songaymuon);
            DateTime tgTra = new DateTime(sonamtra, sothangtra, songaytra);


            //MessageBox.Show("Ngày mượn: " + ngaymuon + "Tháng mượn: " + thangmuon + "Năm mượn: " + nammuon);
            kq = tgTra.CompareTo(tgMuon);

            return kq;
        }
        private void giaHanSach()
        {

            if (SoSanhHanTra(dtmNgayHetHan0.Text, dtmNgayMuon0.Text) == 1)
            {   //---- tiến hành up date ngày tháng luôn
                Model_QuanLi_ThuVien MtV2 = new Model_QuanLi_ThuVien();

                SqlParameter[] idParam =
                { new SqlParameter { ParameterName="NgayMuon", Value=dtmNgayMuon0.Text },
                new SqlParameter { ParameterName = "NgayHanTra", Value = dtmNgayHetHan0.Text },
                new SqlParameter { ParameterName = "MaMuonTra", Value = MuonTratxt0.Text  }

                };
                MtV2.Database.ExecuteSqlCommand("exec GiaHan @NgayMuon,@NgayHanTra,@MaMuonTra", idParam);
                MessageBox.Show("Gia hạn thành công.", "Thông Báo");

                load_MuonTra();


                setControlsMuon(false);
                btnNhap.Enabled = true;
                btnChoMuon0.Text = "Cho Mượn";
                btnChoMuon0.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
                btnChoMuon0.Enabled = false;
                btnGiaHan.Enabled = true;
                btnHuy0.Enabled = false;

                dtgrdView_Muon.Enabled = true;
            }
            else
            {
                MessageBox.Show("Xem lại thời gian cho mượn.", "Thông Báo");
            }
        }

        public void layMaSach_DocGia()
        {
            Model_QuanLi_ThuVien MtV1 = new Model_QuanLi_ThuVien();
            var lstDocGia = MtV1.ThongTinDocGias.SqlQuery("select * from ThongTinDocGia").ToList();
            cboMaDG0.DataSource = lstDocGia;
            cboMaDG0.DisplayMember = " Mã Độc Giả";
            cboMaDG0.ValueMember = "MaDocGia";
            var lstMaSach = MtV1.CuonSaches.SqlQuery("select * from CuonSach").ToList();
            cboMaSach0.DataSource = lstMaSach;
            cboMaSach0.ValueMember = "MaSach";

        }

        private void muonsach()
        {
            //// điều kiện txtViPham.Text ,txtSachMuon
            ///chưa cần điều kiện check thử 
            if ((Convert.ToInt32(txtViPham.Text) >= 3) || (Convert.ToInt32(txtSachMuon.Text) >= 6))
            {
                MessageBox.Show("Không đủ điều kiện mượn sách");
            }
            else
            {
                if (SoSanhHanTra(dtmNgayHetHan0.Text, dtmNgayMuon0.Text) == 1)
                {
                    try
                    {
                        Model_QuanLi_ThuVien MtV2 = new Model_QuanLi_ThuVien();
                        SqlParameter[] idParam =
                           { new SqlParameter { ParameterName="MaSach", Value=cboMaSach0.Text },
                new SqlParameter { ParameterName = "MaDocGia", Value = cboMaDG0.Text },
                new SqlParameter { ParameterName = "MaNVMuon", Value = MaNVMtxt0.Text },
                new SqlParameter { ParameterName = "NgayMuon", Value = dtmNgayMuon0.Text  },
                 new SqlParameter { ParameterName = "NgayHanTra", Value = dtmNgayHetHan0.Text },

                };
                        int Check_Sach = MtV2.Database.ExecuteSqlCommand("MuonMoiSach @MaSach,@MaDocGia,@MaNVMuon,@NgayMuon,@NgayHanTra", idParam);
                        if (Check_Sach == -1)
                            MessageBox.Show("Cuốn sách đanng cho mượn .Hãy mượn cuốn sách khác");
                        else
                        {
                            MessageBox.Show("Cho Mươn thành công");
                        }
                        load_MuonTra();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Lỗi Hệ thống");
                    }
                }
                else { MessageBox.Show("Xem lại thời gian cho mượn"); }
            }

        }

        private void dtgrdView_Muon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            MaMT0 = MuonTratxt0.Text = dtgrdView_Muon.CurrentRow.Cells[0].Value.ToString();
            MaSach0 = cboMaSach0.Text = dtgrdView_Muon.CurrentRow.Cells[1].Value.ToString();

            MaDG0 = cboMaDG0.Text = dtgrdView_Muon.CurrentRow.Cells[2].Value.ToString();
            MaNVMtxt0.Text = dtgrdView_Muon.CurrentRow.Cells[3].Value.ToString();
            NgayMuon0 = dtmNgayMuon0.Text = dtgrdView_Muon.CurrentRow.Cells[5].Value.ToString();

            //   ;
            HanTra0 = dtmNgayHetHan0.Text = dtgrdView_Muon.CurrentRow.Cells[6].Value.ToString();
        }

        private void btnChoMuon0_Click(object sender, EventArgs e)
        {
            if (xuly == 0)
            {
                muonsach();
            }
            else if (xuly == 1)
            {
                giaHanSach();
            }
        }

        private void btnNhap_Click(object sender, EventArgs e)
        {
            layMaSach_DocGia();

            setControlsMuon(true);

            cboMaSach0.Text = "";
            cboMaDG0.Text = "";
            MaNVMtxt0.Text = Login.MaNguoiDung;
            MuonTratxt0.Text = "";
            MuonTratxt0.Enabled = false;
            btnHuy0.Enabled = true;
            btnGiaHan.Enabled = false;

            btnChoMuon0.Text = "Cho Mượn";
            btnChoMuon0.TextAlign = System.Windows.Forms.HorizontalAlignment.Center; ;
            btnChoMuon0.Enabled = true;
            xuly = 0;

        }

        private void btnGiaHan_Click(object sender, EventArgs e)
        {
            setControlsMuon(true);
            MuonTratxt0.Enabled = false;
            cboMaDG0.Enabled = false;
            cboMaSach0.Enabled = false;


            MaNVMtxt0.Enabled = false;
            ////////////

            btnNhap.Enabled = false;
            btnChoMuon0.Text = "Lưu";
            btnChoMuon0.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            btnChoMuon0.Enabled = true;
            btnGiaHan.Enabled = false;
            btnHuy0.Enabled = true;
            xuly = 1;
        }

        private void btnHuy0_Click(object sender, EventArgs e)
        {
            MuonTratxt0.Text = MaMT0;
            cboMaSach0.Text = MaSach0;
            dtmNgayMuon0.Text = NgayMuon0;
            btnChoMuon0.Text = "Cho Mượn";
            btnChoMuon0.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            btnNhap.Enabled = true;
            btnChoMuon0.Enabled = false;
            btnGiaHan.Enabled = true;
            btnHuy0.Enabled = false;
            setControlsMuon(false);
            dtgrdView_Muon.Enabled = true;
        }


    }
}
