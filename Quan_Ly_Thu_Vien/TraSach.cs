﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
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
 
        public string ngaymuon, thangmuon, nammuon, ngaytra, thangtra, namtra, ngaydgmuon, ngaydgtra;

        private void dtgrdView_Tra_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            MuonTratxt1.Text=dtgrdView_Tra.Rows[i].Cells[0].Value.ToString();
         //   MuonTratxt1.Text = dtgrdView_Tra.CurrentRow.Cells[0].Value.ToString();
            txtMaSach1.Text = dtgrdView_Tra.Rows[i].Cells[1].Value.ToString(); ;
             txtMaDG1.Text = dtgrdView_Tra.Rows[i].Cells[2].Value.ToString();
            if (dtgrdView_Tra.CurrentRow.Cells[4].Value == null) { txtMaNVTra.Text = ""; }
            else txtMaNVTra.Text = dtgrdView_Tra.Rows[i].Cells[4].Value.ToString(); ;
            dtmHanTra1.Text = dtgrdView_Tra.Rows[i].Cells[6].Value.ToString();
            //if (dtgrdView_Tra.Rows[i].Cells[7].Value.ToString() == null) { dtmNgayTra1.Text ==}
            //else
            //{ dtmNgayTra1.Text = dtgrdView_Tra.Rows[i].Cells[7].Value.ToString(); }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            setControlsTra(false);
        }

        private void txtNDTimKiem1_TextChanged(object sender, EventArgs e)
        {
            if (radMaDG1.Checked)
            {

                SqlParameter idParam = new SqlParameter { ParameterName = "NoiDung", Value = txtNDTimKiem1.Text };
                Model_QuanLi_ThuVien MtV1 = new Model_QuanLi_ThuVien();
                var lstTimkiemDocGia = MtV1.ThongTinMuonTras.SqlQuery("TimKiemMaDG @NoiDung", idParam).ToList();
                dtgrdView_Tra.DataSource = lstTimkiemDocGia;
                dtgrdView_Tra.AutoGenerateColumns = false;

            }
            else if (radMaSach1.Checked)
            {
                SqlParameter idParam = new SqlParameter { ParameterName = "NoiDung", Value = txtNDTimKiem1.Text };
                Model_QuanLi_ThuVien MtV1 = new Model_QuanLi_ThuVien();
                var lstTimkiemSach = MtV1.ThongTinMuonTras.SqlQuery("TimKiemMaSach @NoiDung", idParam).ToList();
                dtgrdView_Tra.DataSource = lstTimkiemSach;
                dtgrdView_Tra.AutoGenerateColumns = false;

            }
        }

        private void TraSach_Load(object sender, EventArgs e)
        {
            load_MuonTra();
            setControlsTra(false);
            txtMaNVTra.Enabled = false;
            txtMaNVTra.Enabled = false;
            radMaDG1.Checked = true;
        }

        private void btnTraSach1_Click(object sender, EventArgs e)
        {
            dtmNgayTra1.Enabled = true;
            btnCheck.Enabled = true;
            cboViPham.Enabled = true;
            btnHuy.Enabled = true;
            txtMaNVTra.Text = Login.MaNguoiDung;
            txtMaNVTra.Enabled = false;
        }

        private void load_MuonTra()
        {

            Model_QuanLi_ThuVien MtV1 = new Model_QuanLi_ThuVien();
            var lstMuonTra = MtV1.ThongTinMuonTras.SqlQuery("select * from ThongTinMuonTra").ToList();
            dtgrdView_Tra.DataSource = lstMuonTra;

        }
        private void XuLyViPham(int choice)
        {
            if (choice == 1)
            {  //Xử lý mất sách 
               //// Bước 2 : Xử lý Vi Phạm trong database ( chua chen duoc ky tu co dau vao)

                try
                {
                    Model_QuanLi_ThuVien MtV1 = new Model_QuanLi_ThuVien();
                    SqlParameter[] idParam1 =
                 { new SqlParameter { ParameterName = "MaDocGia", Value =txtMaDG1.Text  },
                 new SqlParameter { ParameterName="LyDo", Value=cboViPham.Text},
                new SqlParameter { ParameterName = "TienPhat", Value = 100000 },
                  new SqlParameter { ParameterName = "NgayXuLy", Value =dtmNgayTra1.Text }};
                    MtV1.Database.ExecuteSqlCommand("XuLyViPhamSach @MaDocGia,@LyDo,@TienPhat,@NgayXuLy", idParam1);


                    // Bước 1 : Xử lý sách trong database: Xóa quyển sách có liên quan ( thành công)

                    SqlParameter idParam = new SqlParameter { ParameterName = "MaSach", Value = txtMaSach1.Text };

                    MtV1.Database.ExecuteSqlCommand("XuLyMatSach @MaSach", idParam);
                    MessageBox.Show("Quyển sách đã bị xóa khỏi hệ thống , đề nghị xử phạt ", "Thông Báo");

                    ReportViPham fm = new ReportViPham(cboViPham.Text, 100000, txtMaDG1.Text);
                    fm.Show();



                }



                catch (Exception)
                {
                    MessageBox.Show("Xảy ra lỗi hệ thống , yêu cầu bạn nhập đúng thông tin");
                }
                load_MuonTra();
                setControlsTra(false);
                cboViPham.Text = "";
            }
            else if (choice == 2)
            {
                //Xử lý hỏng tài liệu và quá hạn 
                //thay doi tinh trang sach trong database 
                // them vao do la them vao phan xu ly vi pham , dag xet la hong tai lieu
                try
                {
                    Model_QuanLi_ThuVien MtV1 = new Model_QuanLi_ThuVien();
                    SqlParameter[] idParam =
                    { new SqlParameter { ParameterName = "MaMuonTra", Value = Convert.ToInt32(MuonTratxt1.Text) },
                 new SqlParameter { ParameterName="NgayTra", Value=dtmNgayTra1.Text },

                  new SqlParameter { ParameterName = "MaNVTra", Value = txtMaNVTra.Text}};
                    MtV1.Database.ExecuteSqlCommand("exec TraSach @MaMuonTra,@NgayTra,@MaNVTra", idParam);


                    SqlParameter[] idParam1 =
                 { new SqlParameter { ParameterName = "MaDocGia", Value =txtMaDG1.Text  },
                 new SqlParameter { ParameterName="LyDo", Value=cboViPham.Text+",Qúa Hạn"},
                new SqlParameter { ParameterName = "TienPhat", Value = 50000 },
                  new SqlParameter { ParameterName = "NgayXuLy", Value =dtmNgayTra1.Text }};
                    MtV1.Database.ExecuteSqlCommand("XuLyViPhamSach @MaDocGia,@LyDo,@TienPhat,@NgayXuLy", idParam1);
                    MessageBox.Show("Trả sách thành công , đề nghị xử phạt ", "Thông Báo");
                    ReportViPham fm = new ReportViPham(cboViPham.Text + ",Quá Hạn", 50000, txtMaDG1.Text);
                    fm.Show();
                }



                catch (Exception)
                {
                    MessageBox.Show("Xảy ra lỗi hệ thống , yêu cầu bạn nhập đúng thông tin");
                }
                setControlsTra(false);
                load_MuonTra();
                cboViPham.Text = "";
            }
            else if (choice == 3)
            {
                //Xử lý hỏng tài liệu  
                //thay doi tinh trang sach trong database 
                // them vao do la them vao phan xu ly vi pham , dag xet la hỏng tài liệu
                try
                {
                    Model_QuanLi_ThuVien MtV1 = new Model_QuanLi_ThuVien();
                    SqlParameter[] idParam =
                    { 
                        new SqlParameter { ParameterName = "MaMuonTra", Value = Convert.ToInt32(MuonTratxt1.Text) },
                        new SqlParameter { ParameterName="NgayTra", Value=dtmNgayTra1.Text },
                        new SqlParameter { ParameterName = "MaNVTra", Value = txtMaNVTra.Text}
                    };
                    MtV1.Database.ExecuteSqlCommand("exec TraSach @MaMuonTra,@NgayTra,@MaNVTra", idParam);
                    MessageBox.Show("Trả sách thành công , đề nghị xử phạt ", "Thông Báo");
                    SqlParameter[] idParam1 ={
                        new SqlParameter { ParameterName = "MaDocGia", Value =txtMaDG1.Text  },
                        new SqlParameter { ParameterName="LyDo", Value=cboViPham.Text},
                        new SqlParameter { ParameterName = "TienPhat", Value = 30000 },
                        new SqlParameter { ParameterName = "NgayXuLy", Value =dtmNgayTra1.Text }
                    };
                    MtV1.Database.ExecuteSqlCommand("XuLyViPhamSach @MaDocGia,@LyDo,@TienPhat,@NgayXuLy", idParam1);
                    ReportViPham fm = new ReportViPham(cboViPham.Text, 30000, txtMaDG1.Text);
                    fm.Show();
                }
                catch (Exception)
                {
                    MessageBox.Show("Xảy ra lỗi hệ thống , yêu cầu bạn nhập đúng thông tin");
                }

                setControlsTra(false);
                load_MuonTra();
                cboViPham.Text = "";
            }
            else if (choice == 4)
            {
                //quá hạn 
                //thay doi tinh trang sach trong database 
                // them vao do la them vao phan xu ly vi pham , dag xet la quá hạn 
                try
                {
                    Model_QuanLi_ThuVien MtV1 = new Model_QuanLi_ThuVien();
                    SqlParameter[] idParam ={
                        new SqlParameter { ParameterName = "MaMuonTra", Value = Convert.ToInt32(MuonTratxt1.Text) },
                        new SqlParameter { ParameterName="NgayTra", Value=dtmNgayTra1.Text },
                        new SqlParameter { ParameterName = "MaNVTra", Value = txtMaNVTra.Text}
                    };
                    MtV1.Database.ExecuteSqlCommand("exec TraSach @MaMuonTra,@NgayTra,@MaNVTra", idParam);
                    MessageBox.Show("Trả sách thành công , đề nghị xử phạt ", "Thông Báo");
                    SqlParameter[] idParam1 = {
                        new SqlParameter { ParameterName = "MaDocGia", Value =txtMaDG1.Text  },
                        new SqlParameter { ParameterName="LyDo", Value="Quá Hạn"},
                        new SqlParameter { ParameterName = "TienPhat", Value = 20000 },
                        new SqlParameter { ParameterName = "NgayXuLy", Value =dtmNgayTra1.Text }
                    };
                    MtV1.Database.ExecuteSqlCommand("XuLyViPhamSach @MaDocGia,@LyDo,@TienPhat,@NgayXuLy", idParam1);
                    ReportViPham fm = new ReportViPham("Quá Hạn", 20000, txtMaDG1.Text);
                    fm.Show();
                }
                catch (Exception)
                {
                    MessageBox.Show("Xảy ra lỗi hệ thống , yêu cầu bạn nhập đúng thông tin");
                }

                setControlsTra(false);
                load_MuonTra();
                cboViPham.Text = "";
            }
            else if (choice == 5)
            {
                try
                {
                    Model_QuanLi_ThuVien MtV1 = new Model_QuanLi_ThuVien();
                    SqlParameter[] idParam ={
                        new SqlParameter { ParameterName = "MaMuonTra", Value = Convert.ToInt32(MuonTratxt1.Text) },
                        new SqlParameter { ParameterName="NgayTra", Value=dtmNgayTra1.Text },
                        new SqlParameter { ParameterName = "MaNVTra", Value = txtMaNVTra.Text}
                    };
                    MtV1.Database.ExecuteSqlCommand("exec TraSach @MaMuonTra,@NgayTra,@MaNVTra", idParam);
                }
                catch (Exception)
                {
                    MessageBox.Show("Xảy ra lỗi hệ thống , yêu cầu bạn nhập đúng thông tin");
                }
                cboViPham.Text = "";
                setControlsTra(false);
                load_MuonTra();
            }

        }
        private void btnCheck_Click(object sender, EventArgs e)
        {
            int Kq = SoSanhHanTra1(dtmNgayTra1.Text, dtmHanTra1.Text);


            if (cboViPham.Text == "Mất Sách")
            {
                MessageBox.Show("Xử lý mất sách ");
                XuLyViPham(1);
            }
            else if (cboViPham.Text == "Hỏng Tài Liệu" && Kq > 0)
            {
                MessageBox.Show("Xử lý hỏng tài liệu và quá hạn");
                XuLyViPham(2);
            }
            else if (cboViPham.Text == "Hỏng Tài Liệu" && Kq <= 0)
            {
                MessageBox.Show("Xử lý hỏng tài liệu ");
                XuLyViPham(3);
            }
            else if (Kq <= 0)
            {
                MessageBox.Show(" Đủ điều kiện trả sách ");
                XuLyViPham(5);

            }
            else
            {
                MessageBox.Show(" Qúa hạn trả sách ");
                XuLyViPham(4);
            }
        }

        //private void dtgrdView_Tra_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //   MuonTratxt1.Text = dtgrdView_Tra.CurrentRow.Cells[0].Value.ToString();
        //     txtMaSach1.Text = dtgrdView_Tra.CurrentRow.Cells[1].Value.ToString();
        //    txtMaDG1.Text = dtgrdView_Tra.CurrentRow.Cells[2].Value.ToString();
        //    if (dtgrdView_Tra.CurrentRow.Cells[4].Value == null) { txtMaNVTra.Text = ""; }
        //    else  txtMaNVTra.Text = dtgrdView_Tra.CurrentRow.Cells[4].Value.ToString();
        //    dtmHanTra1.Text = dtgrdView_Tra.CurrentRow.Cells[6].Value.ToString();
        //    if (dtgrdView_Tra.CurrentRow.Cells[7].Value == null) { }
        //    else
        //    { dtmNgayTra1.Text = dtgrdView_Tra.CurrentRow.Cells[7].Value.ToString(); }
        //}

        private int hieumuon, hieutra, catthangmuon, catngaymuon, catngaytra, catthangtra, songaymuon, sothangmuon, sonammuon, songaytra, sothangtra, sonamtra, kq;
        private void setControlsTra(bool edit)
        {
            MuonTratxt1.Enabled = edit;
            txtMaDG1.Enabled = edit;
            txtMaSach1.Enabled = edit;

            dtmHanTra1.Enabled = edit;
            dtmNgayTra1.Enabled = edit;
            cboViPham.Enabled = edit;
            btnCheck.Enabled = edit;
            cboViPham.Enabled = edit;
            btnHuy.Enabled = edit;
        }
        public int SoSanhHanTra1(string hantra, string ngaytra1)
        {
            catthangmuon = ngaytra1.IndexOf("/");
            thangmuon = ngaytra1.Substring(0, catthangmuon);
            catngaymuon = ngaytra1.LastIndexOf("/");
            hieumuon = (catngaymuon - 1) - catthangmuon;
            ngaymuon = ngaytra1.Substring(catthangmuon + 1, hieumuon);
            nammuon = ngaytra1.Substring(catngaymuon + 1, 4);
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
            kq = tgTra.CompareTo(tgMuon);
            return kq;
        }
    }
}
