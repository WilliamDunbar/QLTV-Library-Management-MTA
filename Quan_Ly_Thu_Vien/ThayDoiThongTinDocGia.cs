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
    public partial class ThayDoiThongTinDocGia : Form
    {
        byte[] Byte_HinhAnh;
        public ThayDoiThongTinDocGia()
        {
            InitializeComponent();
        }
        void Load_DG()
        {
            using (Model_QuanLi_ThuVien qltv=new Model_QuanLi_ThuVien())
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
        private void btThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ThayDoiThongTinDocGia_Load(object sender, EventArgs e)
        {
            Load_DG();
        }

        private void linklb_ThayDoi_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txbTenDG.Enabled = true;
            txbDonViDG.Enabled = true;
            txbLoaiDG.Enabled = true;
            txbSDTDG.Enabled = true;
            btLuu.Visible = true;
            btHuy.Visible = true;
        }

        private void btHuy_Click(object sender, EventArgs e)
        {
            txbTenDG.Enabled = false;
            txbDonViDG.Enabled = false;
            txbLoaiDG.Enabled = false;
            txbSDTDG.Enabled = false;
            btLuu.Visible = false;
            btHuy.Visible = false;
        }

        private void btLuu_Click(object sender, EventArgs e)
        {
            using (Model_QuanLi_ThuVien qltv =new Model_QuanLi_ThuVien())
            {
                SqlParameter[] Param = {
                                    new SqlParameter{ParameterName="MaDocGia",Value=txbMaDG.Text},
                                    new SqlParameter{ParameterName="TenDocGia",Value=txbTenDG.Text},
                                    new SqlParameter{ParameterName="NgaySinh",Value=dTP_NgaySinhDG.Text},
                                    new SqlParameter{ParameterName="DonVi",Value=txbDonViDG.Text},
                                    new SqlParameter{ParameterName="SDT",Value=txbSDTDG.Text},
                                    new SqlParameter{ParameterName="NgayDK",Value=dTP_NgayDK.Text},
                                    new SqlParameter{ParameterName = "NgayHetHanDK", Value = dTP_NgayHetHan.Text },
                                    new SqlParameter{ParameterName = "LoaiDocGia", Value = txbLoaiDG.Text },
                                    new SqlParameter{ParameterName = "HinhAnh", Value = Byte_HinhAnh }
                                };
                qltv.Database.ExecuteSqlCommand($"exec SuaDocGia @MaDocGia,@TenDocGia,@NgaySinh,@DonVi,@SDT,@NgayDK,@NgayHetHanDK,@LoaiDocGia,@HinhAnh", Param);
                MessageBox.Show("Thay doi thong tin thanh cong");
                btLuu.Visible = false;
                btHuy.Visible = false;
            }
        }
    }
}
