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
    public partial class GiaHan : Form
    {
        public GiaHan( string MaMT_MS,string NgayMuon_MS, string NgayTra_MS)

        {
            InitializeComponent();
            dtpNgayMuon.Text =NgayMuon_MS;
            dtpNgayTra.Text =NgayTra_MS;
            MaMT = MaMT_MS;
            dtpNgayMuon.Enabled = false;
          
        }
        private string MaMT;

        private void btThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GiaHan_Load(object sender, EventArgs e)
        {
   

        }
        private int Hieusongay(string ngaymuon, string ngaytra)
        {
            DateTime dt_NgayMuon = Convert.ToDateTime(ngaymuon);
            DateTime dt_NgayTra = Convert.ToDateTime(ngaytra);
            TimeSpan Time = dt_NgayTra - dt_NgayMuon;
            int TongSoNgay = Time.Days;
            return TongSoNgay;
        }

        private void giaHanSach()
        {

            if (Hieusongay(dtpNgayMuon.Text,dtpNgayTra.Text) > 0)
            {   //---- tiến hành up date ngày tháng luôn
                Model_QuanLi_ThuVien MtV2 = new Model_QuanLi_ThuVien();

                SqlParameter[] idParam =
                { new SqlParameter { ParameterName="NgayMuon", Value=dtpNgayMuon.Text },
                new SqlParameter { ParameterName = "NgayHanTra", Value = dtpNgayTra.Text },
                new SqlParameter { ParameterName = "MaMuonTra", Value =MaMT }

                };
                int i = MtV2.Database.ExecuteSqlCommand("exec GiaHan @NgayMuon,@NgayHanTra,@MaMuonTra", idParam);
                if (i < 0) { MessageBox.Show("Xảy ra lỗi , thông tin mượn trả không có trong hệ thống.", "Thông Báo"); }
                else
                {
                    MessageBox.Show("Gia hạn thành công.", "Thông Báo");
                
                    this.Close();

                }

                }
            else
            {
                MessageBox.Show("Xem lại thời gian cho mượn.", "Thông Báo");
            }
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            
                giaHanSach();
            
        }
    }
}
