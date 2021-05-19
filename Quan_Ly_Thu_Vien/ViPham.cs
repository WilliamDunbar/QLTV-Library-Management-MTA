using Quan_Ly_Thu_Vien.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_Ly_Thu_Vien
{
    public partial class ViPham : Form
    {
        public ViPham(string MaMT_TS, string NgayMuon_TS, string NgayHetHan_TS,string MaDG_TS,string MaSach_TS )
        {
            InitializeComponent();
            MaMT = MaMT_TS;
            NgayMuon = NgayMuon_TS;
            NgayHetHan = NgayHetHan_TS;
            MaDG = MaDG_TS;
            MaSach = MaSach_TS;
        }
        private string MaNVTra = Login.MaNguoiDung;
        private string MaMT, NgayMuon, NgayHetHan, MaDG, MaSach;


        private void ViPham_Load(object sender, EventArgs e)
        {
            dtmNgayTra1.Text= DateTime.Now.ToShortDateString();
            dtmNgayTra1.Enabled = false;
            dtmHanTra1.Text = NgayHetHan;
            dtmHanTra1.Enabled = false;
        }
        private int Hieusongay(string ngaymuon, string ngaytra)
        {
            DateTime dt_NgayMuon = Convert.ToDateTime(ngaymuon);
            DateTime dt_NgayTra = Convert.ToDateTime(ngaytra);
            TimeSpan Time = dt_NgayTra - dt_NgayMuon;
            int TongSoNgay = Time.Days;
            return TongSoNgay;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            int Kq = Hieusongay(dtmNgayTra1.Text, dtmHanTra1.Text);
            int Kq1 = Hieusongay(NgayMuon, dtmNgayTra1.Text);

            if (Kq1 < 0)
            {
                MessageBox.Show("Ngày trả phải lớn hơn ngày mượn .Vui lòng xem lại");
            }


            else
            {
                if (cboViPham.Text == "Mất Sách")
                {
                    MessageBox.Show("Xử lý mất sách ");
                    XuLyViPham_TraSach(1);
                    this.Close();
                }
                else if (cboViPham.Text == "Hỏng Tài Liệu" && Kq < 0)
                {
                    MessageBox.Show("Xử lý hỏng tài liệu và quá hạn");
                    XuLyViPham_TraSach(2);
                    this.Close();
                }
                else if (cboViPham.Text == "Hỏng Tài Liệu" && Kq >= 0)
                {
                    MessageBox.Show("Xử lý hỏng tài liệu ");
                    XuLyViPham_TraSach(3);
                    this.Close();
                }
                else if (Kq >= 0)
                {
                    MessageBox.Show(" Đủ điều kiện trả sách ");
                    XuLyViPham_TraSach(5);
                    this.Close();

                }
                else
                {
                    MessageBox.Show(" Qúa hạn trả sách ");
                    XuLyViPham_TraSach(4);
                    this.Close();
                }
            }
        }

        private void XuLy_ViPham(string MaDocGia, string LyDo, string MaSach, int TienPhat, string NgayXuLy)
        {
            Model_QuanLi_ThuVien MtV1 = new Model_QuanLi_ThuVien();
            SqlParameter[] idParam1 =
         { new SqlParameter { ParameterName = "MaDocGia", Value =MaDocGia  },
         new SqlParameter { ParameterName="MaSach", Value=MaSach},
                 new SqlParameter { ParameterName="LyDo", Value=LyDo},
                new SqlParameter { ParameterName = "TienPhat", Value = TienPhat },
                  new SqlParameter { ParameterName = "NgayXuLy", Value =NgayXuLy }};
            MtV1.Database.ExecuteSqlCommand("XuLyViPhamSach @MaDocGia,@LyDo,@TienPhat,@NgayXuLy,@MaSach", idParam1);
        }
        private void XuLy_TraSach()
        {
            Model_QuanLi_ThuVien MtV1 = new Model_QuanLi_ThuVien();
            SqlParameter[] idParam =
                 { new SqlParameter { ParameterName = "MaMuonTra", Value = Convert.ToInt32(MaMT) },
                 new SqlParameter { ParameterName="NgayTra", Value=dtmNgayTra1.Text },

                  new SqlParameter { ParameterName = "MaNVTra", Value =MaNVTra}};
            MtV1.Database.ExecuteSqlCommand("exec TraSach @MaMuonTra,@NgayTra,@MaNVTra", idParam);
        }
        private void XuLyViPham_TraSach(int choice)
        {
            if (choice == 1)
            {  //Xử lý mất sách 

                try
                {
                    Model_QuanLi_ThuVien MtV1 = new Model_QuanLi_ThuVien();
                    XuLy_ViPham(MaDG, cboViPham.Text, MaSach, 100000, dtmNgayTra1.Text);
                    SqlParameter idParam = new SqlParameter { ParameterName = "MaSach", Value = MaSach };
                    MtV1.Database.ExecuteSqlCommand("XuLyMatSach @MaSach", idParam);
                    MessageBox.Show("Quyển sách đã bị xóa khỏi hệ thống , đề nghị xử phạt ", "Thông Báo");
                    ReportViPham fm = new ReportViPham(cboViPham.Text, 100000, MaDG);
                    fm.Show();
                }
                catch (Exception)
                {
                    MessageBox.Show("Xảy ra lỗi hệ thống , yêu cầu bạn nhập đúng thông tin");
                }

            }
            else if (choice == 2)
            {
                //Xử lý hỏng tài liệu và quá hạn             
                try
                {
                    XuLy_TraSach();
                    XuLy_ViPham(MaDG, cboViPham.Text + "Quá Hạn",MaSach, 50000, dtmNgayTra1.Text);
                    MessageBox.Show("Trả sách thành công , đề nghị xử phạt ", "Thông Báo");
                    ReportViPham fm = new ReportViPham(cboViPham.Text + ",Quá Hạn", 50000, MaDG);
                    fm.Show();
                }
                catch (Exception)
                {
                    MessageBox.Show("Xảy ra lỗi hệ thống , yêu cầu bạn nhập đúng thông tin");
                }
             
              
          
            }
            else if (choice == 3)
            {
                //Xử lý hỏng tài liệu  
                try
                {
                    XuLy_TraSach();
                    MessageBox.Show("Trả sách thành công , đề nghị xử phạt ", "Thông Báo");
                    XuLy_ViPham(MaDG, cboViPham.Text,MaSach, 30000, dtmNgayTra1.Text);
                    ReportViPham fm = new ReportViPham(cboViPham.Text, 30000, MaDG);
                    fm.Show();
                }
                catch (Exception)
                {
                    MessageBox.Show("Xảy ra lỗi hệ thống , yêu cầu bạn nhập đúng thông tin");
                }
 
            }
            else if (choice == 4)
            {
                //quá hạn 
                try
                {
                    XuLy_TraSach();
                    XuLy_ViPham(MaDG, "Quá Hạn",MaSach, 20000, dtmNgayTra1.Text);
                    MessageBox.Show("Trả sách thành công , đề nghị xử phạt ", "Thông Báo");
                    ReportViPham fm = new ReportViPham("Quá Hạn", 20000, MaDG);
                    fm.Show();
                }
                catch (Exception)
                {
                    MessageBox.Show("Xảy ra lỗi hệ thống , yêu cầu bạn nhập đúng thông tin");
                }
                cboViPham.Text = "";
            }
            else if (choice == 5)
            {
                try
                {
                    XuLy_TraSach();
                    MessageBox.Show(" Trả sách thành công");
                }
                catch (Exception)
                {
                    MessageBox.Show("Xảy ra lỗi hệ thống , yêu cầu bạn nhập đúng thông tin");
                }
                cboViPham.Text = "";
            }

        }
        private void btThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
