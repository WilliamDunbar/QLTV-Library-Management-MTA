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
    public partial class QuanLyDauSach_DG : Form
    {
        public QuanLyDauSach_DG()
        {
            InitializeComponent();
        }
        public static string MaDS;
        public static string TenDS;
        byte[] Byte_HinhAnh;
        void Load_DauSach()
        {
            using (Model_QuanLi_ThuVien qltv = new Model_QuanLi_ThuVien())
            {
                var ListDS = qltv.ThongTinDauSaches.SqlQuery("exec LoadThongTinDauSach");
                dtGV_DauSach.DataSource = ListDS.ToList();
            }

        }

        private void QuanLyDauSach_DG_Load(object sender, EventArgs e)
        {
            Load_DauSach();
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

        private void dtGV_DauSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            using (Model_QuanLi_ThuVien qltv = new Model_QuanLi_ThuVien())
            {
               // TrangThaiBanDau();
                int i = e.RowIndex;
                txbMaDS.Text = dtGV_DauSach.Rows[i].Cells[0].Value.ToString();
                MaDS = txbMaDS.Text;
                txbTenDS.Text = dtGV_DauSach.Rows[i].Cells[1].Value.ToString();
                txbTenNXB.Text = dtGV_DauSach.Rows[i].Cells[2].Value.ToString();
                dtbNXB.Text = dtGV_DauSach.Rows[i].Cells[3].Value.ToString();
                txbSotrang.Text = dtGV_DauSach.Rows[i].Cells[4].Value.ToString();
                txbGiaTien.Text = dtGV_DauSach.Rows[i].Cells[5].Value.ToString();
                txbSoLuong.Text = dtGV_DauSach.Rows[i].Cells[6].Value.ToString();
                var ListTG = from kq in qltv.SangTacs where kq.MaDauSach == txbMaDS.Text select kq.TacGia.TenTacGia;
                var ListTheLoai = from kq in qltv.TheLoaiDauSaches where kq.MaDauSach == txbMaDS.Text select kq.TheLoai.TenTheLoai;
                cbbTacGia.DataSource = ListTG.ToList();
                cbbTheLoai.DataSource = ListTheLoai.ToList();
                DauSach ds = qltv.DauSaches.Where(p => p.MaDauSach == txbMaDS.Text).FirstOrDefault();
                Byte_HinhAnh = (byte[])ds.HinhAnh;
                ptbAnhDS.Image = ByteToImage((byte[])ds.HinhAnh);
            }
        }

        private void txbTimKiem_TextChanged(object sender, EventArgs e)
        {
            using (Model_QuanLi_ThuVien qltv = new Model_QuanLi_ThuVien())
            {
                SqlParameter Search_Ma = new SqlParameter { ParameterName = "MaDauSach", Value = txbTimKiem.Text };
                SqlParameter Search_Ten = new SqlParameter { ParameterName = "TenDauSach", Value = txbTimKiem.Text };
                SqlParameter Search_TG = new SqlParameter { ParameterName = "TenTacGia", Value = txbTimKiem.Text };
                SqlParameter Search_TL = new SqlParameter { ParameterName = "TenTheLoai", Value = txbTimKiem.Text };
                if (rdbMaDS.Checked == true)
                {
                    var ListDS = qltv.ThongTinDauSaches.SqlQuery($"exec TimKiemDauSach_TheoMaDS @MaDauSach", Search_Ma);
                    dtGV_DauSach.DataSource = ListDS.ToList();
                }
                else if (rdbTenDS.Checked == true)
                {
                    var ListDS = qltv.ThongTinDauSaches.SqlQuery($"exec TimKiemDauSach_TheoTenDS @TenDauSach", Search_Ten);
                    dtGV_DauSach.DataSource = ListDS.ToList();
                }
                else if (rdbTacGia.Checked == true)
                {
                    var ListDS = qltv.ThongTinDauSaches.SqlQuery($"exec TimKiemDauSach_TheoTacGia @TenTacGia", Search_TG);
                    dtGV_DauSach.DataSource = ListDS.ToList();
                }
                else if (rdbTLDS.Checked == true)
                {
                    var ListDS = qltv.ThongTinDauSaches.SqlQuery($"exec TimKiemDauSach_TheoTheLoaiDS @TenTheLoai", Search_TL);
                    dtGV_DauSach.DataSource = ListDS.ToList();
                }
            }
        }
    }
}
