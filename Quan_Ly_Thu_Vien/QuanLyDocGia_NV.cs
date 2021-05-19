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
    public partial class QuanLyDocGia_NV : Form
    {

        bool ThemOrSua;

        public QuanLyDocGia_NV()
        {
            InitializeComponent();
        }
        public void Load_DG()
        {
            using (Model_QuanLi_ThuVien qltv = new Model_QuanLi_ThuVien())
            {
                var listDG = qltv.Database.SqlQuery<ThongTinDocGia>($"exec LoadThongTinDocGia");
                dtgv_loadDG.DataSource = listDG.ToList();

            }
        }

        private void QuanLyDocGia_NV_Load(object sender, EventArgs e)
        {
            Load_DG();
        }

        public void TrangThaiBanDau()
        {
            btThemDG.Enabled = true;
            btSuaDG.Enabled = true;
            btXoaDG.Enabled = true;
            btLuuDG.Enabled = false;
            btHuyDG.Enabled = false;
            txbMaDG.Enabled = false;
            txbTenDG.Enabled = false;
            txbDonViDG.Enabled = false;
            dtpNgaySinhDG.Enabled = false;
            txbSdtDG.Enabled = false;
            dtpNgayDK.Enabled = false;
            dtpNgayHH.Enabled = false;
            txbLoaiDG.Enabled = false;
            txbTaiKhoanDG.Enabled = false;
            txbMatKhauDG.Enabled = false;
            ptbAnhDS.Enabled = false;
        }

        void TrangThaiThemOrSuaDG()
        {
            btThemDG.Enabled = false;
            btSuaDG.Enabled = false;
            btXoaDG.Enabled = false;
            btLuuDG.Enabled = true;
            btHuyDG.Enabled = true;
            txbTenDG.Enabled = true;
            txbDonViDG.Enabled = true;
            dtpNgaySinhDG.Enabled = true;
            txbSdtDG.Enabled = true;
            dtpNgayDK.Enabled = true;
            dtpNgayHH.Enabled = true;
            txbLoaiDG.Enabled = true;
            txbMatKhauDG.Enabled = true;
            ptbAnhDS.Enabled = true;
        }



        private void dtgv_loadDG_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                using (Model_QuanLi_ThuVien qltv = new Model_QuanLi_ThuVien())
                {
                    TrangThaiBanDau();
                    int i = e.RowIndex;
                    txbMaDG.Text = dtgv_loadDG.Rows[i].Cells[0].Value.ToString();
                    txbTenDG.Text = dtgv_loadDG.Rows[i].Cells[1].Value.ToString();
                    dtpNgaySinhDG.Text = dtgv_loadDG.Rows[i].Cells[2].Value.ToString();
                    txbDonViDG.Text = dtgv_loadDG.Rows[i].Cells[3].Value.ToString();
                    txbSdtDG.Text = dtgv_loadDG.Rows[i].Cells[4].Value.ToString();
                    dtpNgayDK.Text = dtgv_loadDG.Rows[i].Cells[5].Value.ToString();
                    dtpNgayHH.Text = dtgv_loadDG.Rows[i].Cells[6].Value.ToString();
                    TaiKhoanDG TK = qltv.TaiKhoanDGs.Where(p => p.MaDocGia == txbMaDG.Text).FirstOrDefault();
                    txbTaiKhoanDG.Text = TK.TenDangNhap;
                    txbMatKhauDG.Text = TK.MatKhau;
                    if (dtgv_loadDG.Rows[i].Cells[7].Value == null) txbLoaiDG.Text = "NULL";
                    else txbLoaiDG.Text = dtgv_loadDG.Rows[i].Cells[7].Value.ToString();

                    DocGia dg = qltv.DocGias.Where(p => p.MaDocGia == txbMaDG.Text).FirstOrDefault();
                    Byte_HinhAnh = (byte[])dg.HinhAnh;
                    ptbAnhDS.Image = ByteToImage((byte[])dg.HinhAnh);
                }
            }
            catch (Exception)
            {

            }
            
        }
        // sửa
        private byte[] ConvertImageToBytes(string text)
        {
            FileStream fs;
            fs = new FileStream(text, FileMode.Open, FileAccess.Read);
            byte[] picbyte = new byte[fs.Length];
            fs.Read(picbyte, 0, System.Convert.ToInt32(fs.Length));
            fs.Close();
            return picbyte;

        }
        string textImagePath;
        byte[] Byte_HinhAnh;
        private void ptbAnhDS_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog OpenFDL = new OpenFileDialog();
            OpenFDL.Filter = OpenFDL.Filter = "JPG files (.jpg)|.jpg|All files (.)|*.*";
            OpenFDL.FilterIndex = 1;
            OpenFDL.RestoreDirectory = true;
            if (OpenFDL.ShowDialog() == DialogResult.OK)
            {
                ptbAnhDS.ImageLocation = OpenFDL.FileName;
                textImagePath = OpenFDL.FileName.ToString();
            }
            Byte_HinhAnh = ConvertImageToBytes(textImagePath);
            ptbAnhDS.Image = ByteToImage(Byte_HinhAnh);
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
        private void btLuuDG_Click(object sender, EventArgs e)
        {
            using (Model_QuanLi_ThuVien qltv = new Model_QuanLi_ThuVien())
            {
                if (txbMaDG.Text == "") MessageBox.Show("Ban chua nhap ma doc gia!");
                else if (txbTenDG.Text == "") MessageBox.Show("Ban chua nhap ten doc gia!");
                else if (txbDonViDG.Text == "") MessageBox.Show("Ban chua nhap don vi doc gia!");
                else if (dtpNgaySinhDG.Text == "") MessageBox.Show("Ban chua nhap ngay sinh doc gia!");
                else if (txbSdtDG.Text == "") MessageBox.Show("Ban chua nhap so dien thoai doc gia!");
                else if (dtpNgayDK.Text == "") MessageBox.Show("Ban chua nhap ngay dang ky!");
                else if (dtpNgayHH.Text == "") MessageBox.Show("Ban chua nhap ngay het han!");
                else if (txbLoaiDG.Text == "") MessageBox.Show("Ban chua nhap loai doc gia!");
                else if (txbTaiKhoanDG.Text == "") MessageBox.Show("Ban chua nhap tai khoan!");
                else if (txbMatKhauDG.Text == "") MessageBox.Show("Ban chua nhap mat khau!");
                else
                {
                    // Sửa
                    SqlParameter[] Param = {
                                    new SqlParameter{ParameterName="MaDocGia",Value=txbMaDG.Text},
                                    new SqlParameter{ParameterName="TenDocGia",Value=txbTenDG.Text},
                                    new SqlParameter{ParameterName="NgaySinh",Value=dtpNgaySinhDG.Text},
                                    new SqlParameter{ParameterName="DonVi",Value=txbDonViDG.Text},
                                    new SqlParameter{ParameterName="SDT",Value=txbSdtDG.Text},
                                    new SqlParameter{ParameterName="NgayDK",Value=dtpNgayDK.Text},
                                    new SqlParameter{ParameterName = "NgayHetHanDK", Value = dtpNgayHH.Text },
                                    new SqlParameter{ParameterName = "LoaiDocGia", Value = txbLoaiDG.Text },
                                    new SqlParameter{ParameterName = "HinhAnh", Value = Byte_HinhAnh }
                                };
                    if (ThemOrSua == true)
                    {
                        bool madg = false;
                        bool matk = false;
                        var listMaDG = from kq in qltv.DocGias select kq.MaDocGia;
                        var listMaTK = from kq in qltv.TaiKhoanDGs select kq.TenDangNhap;
                        foreach (var item in listMaDG) { if (txbMaDG.Text == item) madg = true; }
                        foreach (var item in listMaTK) { if (txbMaDG.Text == item) matk = true; }
                        if (madg == true) MessageBox.Show("Ma doc gia nay da ton tai!");
                        else if (matk == true) MessageBox.Show("Ten tai khoan nay da ton tai!");
                        else
                        {
                            try
                            {
                                qltv.Database.ExecuteSqlCommand($"exec ThemDocGia @MaDocGia,@TenDocGia,@NgaySinh,@DonVi,@SDT,@NgayDK,@NgayHetHanDK,@LoaiDocGia,@HinhAnh", Param);
                                qltv.Database.ExecuteSqlCommand($"exec ThemTaiKhoanDG '{txbMaDG.Text}','{txbTaiKhoanDG.Text}','{txbMatKhauDG.Text}'");
                                MessageBox.Show("Them thanh cong doc gia");
                                Load_DG();
                                TrangThaiBanDau();
                            }
                            catch (Exception) { MessageBox.Show("Ma doc gia phai duoc bat dau bang 'DG' !"); }
                        }
                    }
                    else
                    {
                        qltv.Database.ExecuteSqlCommand($"exec SuaTaiKhoanDG '{txbMaDG.Text}','{txbMatKhauDG.Text}'");
                        qltv.Database.ExecuteSqlCommand($"exec SuaDocGia @MaDocGia,@TenDocGia,@NgaySinh,@DonVi,@SDT,@NgayDK,@NgayHetHanDK,@LoaiDocGia,@HinhAnh",Param );
                        MessageBox.Show("Sua thanh cong");
                        Load_DG();
                        TrangThaiBanDau();
                    }
                }
            }
        }
//Thêm Sửa Xóa
        private void btSuaDG_Click(object sender, EventArgs e)
        {
            ThemOrSua = false;
            TrangThaiThemOrSuaDG();
        }

        private void btThemDG_Click(object sender, EventArgs e)
        {
            ThemOrSua = true;
            TrangThaiThemOrSuaDG();
            ptbAnhDS.Image = null;
            txbMaDG.Enabled = true;
            txbTaiKhoanDG.Enabled = true;
            txbMaDG.Text = "";
            txbTenDG.Text = "";
            txbDonViDG.Text = "";
            txbSdtDG.Text = "";
            txbLoaiDG.Text = "";
            txbTaiKhoanDG.Text = "";
            txbMatKhauDG.Text = "";
        }

        private void btXoaDG_Click(object sender, EventArgs e)
        {

        }

        private void btHuyDG_Click(object sender, EventArgs e)
        {
            TrangThaiBanDau();
        }

        private void txbTimKiem_TextChanged(object sender, EventArgs e)
        {
            using (Model_QuanLi_ThuVien qltv = new Model_QuanLi_ThuVien())
            {
                SqlParameter Search_Ma = new SqlParameter { ParameterName = "MaDocGia", Value = txbTimKiem.Text };
                SqlParameter Search_Ten = new SqlParameter { ParameterName = "TenDocGia", Value = txbTimKiem.Text };
                if (rdbMaDG.Checked == true)
                {
                    var listDG = qltv.ThongTinDocGias.SqlQuery($"exec TimKiemDocGia_TheoMa @MaDocGia", Search_Ma);
                    dtgv_loadDG.DataSource = listDG.ToList();
                }
                else if (rdbTenDG.Checked == true)
                {
                    var listDG = qltv.ThongTinDocGias.SqlQuery($"exec TimKiemDocGia_TheoTen @TenDocGia", Search_Ten);
                    dtgv_loadDG.DataSource = listDG.ToList();
                }
            }
        }


    }
}
