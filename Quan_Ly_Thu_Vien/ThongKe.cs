using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quan_Ly_Thu_Vien.Database;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Media;
using System.Windows;

namespace Quan_Ly_Thu_Vien
{
    public partial class ThongKe : Form
    {
        public ThongKe()
        {
            InitializeComponent();
        }

        void Load_ThongKeSach()
        {
            using(Model_QuanLi_ThuVien qltv=new Model_QuanLi_ThuVien())
            {
                var listDS = qltv.DauSaches.ToList();
                txbSoLuongDS.Text = listDS.Count.ToString();
                SqlParameter TongGT = new SqlParameter { ParameterName = "TongGiaTri", Direction = System.Data.ParameterDirection.Output, Value = -1, SqlDbType = System.Data.SqlDbType.Int };
                qltv.Database.ExecuteSqlCommand(" exec TongGiaTienDauSach @TongGiaTri OUTPUT",TongGT);
                txbTongGiaTri.Text = TongGT.Value.ToString();
                SqlParameter SoLuongQuaHan = new SqlParameter { ParameterName = "SoLuongQuaHan", Direction = System.Data.ParameterDirection.Output, Value = -1, SqlDbType = System.Data.SqlDbType.Int };
                qltv.Database.ExecuteSqlCommand(" exec SoLuongCsDangQuaHan @SoLuongQuaHan OUTPUT", SoLuongQuaHan);
                txbSoLuongSach_DangQuaHan.Text = SoLuongQuaHan.Value.ToString();
                var listCS = qltv.CuonSaches.ToList();
                txbSoLuongCS.Text = listCS.Count.ToString();
                var listCS_DangMUon = qltv.MuonTras.Where(p=>p.DaTra==false).ToList();
                txbSoLuongCS_DangDuocMuon.Text = listCS_DangMUon.Count.ToString();
                txbSoLuongCs_ChuaMuon.Text = (int.Parse(txbSoLuongCS.Text)-int.Parse(txbSoLuongCS_DangDuocMuon.Text)).ToString();

            }
        }

        void Load_ThongKeDG()
        {
            using (Model_QuanLi_ThuVien qltv = new Model_QuanLi_ThuVien())
            {
                int SoLuongDG_DangMuon = 0;
                var listDG = qltv.DocGias.ToList();
                txbSoLuongDG.Text = listDG.Count.ToString();
                var listMuonTra = from kq in qltv.MuonTras where kq.DaTra == false select kq.MaDocGia;
                //var listMuonTraQuaHan = from kq in qltv.MuonTras where kq.DaTra == false && kq. select kq.MaDocGia;
                foreach (var itemlistDG in listDG)
                {
                    foreach (var itemlistMuonTra in listMuonTra)
                    {
                        if(itemlistDG.MaDocGia==itemlistMuonTra)
                        {
                            SoLuongDG_DangMuon++;
                            break;
                        }
                    }
                }
                txbSoLuongDG_DangMuon.Text = SoLuongDG_DangMuon.ToString();
                var listViPham = from kq in qltv.XuLyViPhams select kq.MaDocGia;
                txbSoLuotViPham.Text = listViPham.ToList().Count.ToString();
            }
        }
        //int soluongsinhviengioi()
        //{
        //    SqlParameter[] para =
        //     {
        //        new SqlParameter{ ParameterName="@MaLHP",Direction=System.Data.ParameterDirection.Input,Value=txtMaLHP.Text,SqlDbType=System.Data.SqlDbType.VarChar},
        //        new SqlParameter{ ParameterName="@a",Direction=System.Data.ParameterDirection.Output,Value=-1,SqlDbType=System.Data.SqlDbType.Int}
        //    };
        //    var courselist = dt.Database.ExecuteSqlCommand("EXEC [dbo].[SoHocSinhGioi] @MaLHP,@a output", para);
        //    int sohsg = (int)para[1].Value;
        //    return sohsg;
        //}


        private void Load_LineChart()
        {
            int nam;
            int.TryParse(cbbNamThongKe.Text, out nam);
            using(Model_QuanLi_ThuVien qltv = new Model_QuanLi_ThuVien())
            {
                SqlParameter thang1 = new SqlParameter { ParameterName = "result", Direction = System.Data.ParameterDirection.Output, Value = -1, SqlDbType = System.Data.SqlDbType.Int };
                qltv.Database.ExecuteSqlCommand(" exec ThongKeLuotMuon 1,"+cbbNamThongKe.Text+" ,@result OUTPUT", thang1);
                SqlParameter thang2 = new SqlParameter { ParameterName = "result", Direction = System.Data.ParameterDirection.Output, Value = -1, SqlDbType = System.Data.SqlDbType.Int };
                qltv.Database.ExecuteSqlCommand(" exec ThongKeLuotMuon 2," + cbbNamThongKe.Text + " ,@result OUTPUT", thang2);
                SqlParameter thang3 = new SqlParameter { ParameterName = "result", Direction = System.Data.ParameterDirection.Output, Value = -1, SqlDbType = System.Data.SqlDbType.Int };
                qltv.Database.ExecuteSqlCommand(" exec ThongKeLuotMuon 3," + cbbNamThongKe.Text + " ,@result OUTPUT", thang3);
                SqlParameter thang4 = new SqlParameter { ParameterName = "result", Direction = System.Data.ParameterDirection.Output, Value = -1, SqlDbType = System.Data.SqlDbType.Int };
                qltv.Database.ExecuteSqlCommand(" exec ThongKeLuotMuon 4," + cbbNamThongKe.Text + " ,@result OUTPUT", thang4);
                SqlParameter thang5 = new SqlParameter { ParameterName = "result", Direction = System.Data.ParameterDirection.Output, Value = -1, SqlDbType = System.Data.SqlDbType.Int };
                qltv.Database.ExecuteSqlCommand(" exec ThongKeLuotMuon 5," + cbbNamThongKe.Text + " ,@result OUTPUT", thang5);
                SqlParameter thang6 = new SqlParameter { ParameterName = "result", Direction = System.Data.ParameterDirection.Output, Value = -1, SqlDbType = System.Data.SqlDbType.Int };
                qltv.Database.ExecuteSqlCommand(" exec ThongKeLuotMuon 6," + cbbNamThongKe.Text + " ,@result OUTPUT", thang6);
                SqlParameter thang7 = new SqlParameter { ParameterName = "result", Direction = System.Data.ParameterDirection.Output, Value = -1, SqlDbType = System.Data.SqlDbType.Int };
                qltv.Database.ExecuteSqlCommand(" exec ThongKeLuotMuon 7," + cbbNamThongKe.Text + " ,@result OUTPUT", thang7);
                SqlParameter thang8 = new SqlParameter { ParameterName = "result", Direction = System.Data.ParameterDirection.Output, Value = -1, SqlDbType = System.Data.SqlDbType.Int };
                qltv.Database.ExecuteSqlCommand(" exec ThongKeLuotMuon 8," + cbbNamThongKe.Text + " ,@result OUTPUT", thang8);
                SqlParameter thang9 = new SqlParameter { ParameterName = "result", Direction = System.Data.ParameterDirection.Output, Value = -1, SqlDbType = System.Data.SqlDbType.Int };
                qltv.Database.ExecuteSqlCommand(" exec ThongKeLuotMuon 9," + cbbNamThongKe.Text + " ,@result OUTPUT", thang9);
                SqlParameter thang10 = new SqlParameter { ParameterName = "result", Direction = System.Data.ParameterDirection.Output, Value = -1, SqlDbType = System.Data.SqlDbType.Int };
                qltv.Database.ExecuteSqlCommand(" exec ThongKeLuotMuon 10," + cbbNamThongKe.Text + " ,@result OUTPUT", thang10);
                SqlParameter thang11 = new SqlParameter { ParameterName = "result", Direction = System.Data.ParameterDirection.Output, Value = -1, SqlDbType = System.Data.SqlDbType.Int };
                qltv.Database.ExecuteSqlCommand(" exec ThongKeLuotMuon 11," + cbbNamThongKe.Text + " ,@result OUTPUT", thang11);
                SqlParameter thang12 = new SqlParameter { ParameterName = "result", Direction = System.Data.ParameterDirection.Output, Value = -1, SqlDbType = System.Data.SqlDbType.Int };
                qltv.Database.ExecuteSqlCommand(" exec ThongKeLuotMuon 12," + cbbNamThongKe.Text + " ,@result OUTPUT", thang12);

                LCBieuDoThongKe.Series = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = "Năm" + cbbNamThongKe.Text,
                        Fill = Brushes.LightPink,
                        Stroke = Brushes.LightCoral,
                        //LineSmoothness = 0,
                        Values = new ChartValues<int>{(int)thang1.Value, (int)thang2.Value, (int)thang3.Value, (int)thang4.Value,
                                                      (int)thang5.Value, (int)thang6.Value, (int)thang7.Value, (int)thang8.Value,
                                                      (int)thang9.Value, (int)thang10.Value, (int)thang11.Value, (int)thang12.Value}
                    }

                };
                

            }
            
        }
        void Load_ThongKeDS()
        {
            using (Model_QuanLi_ThuVien qltv = new Model_QuanLi_ThuVien())
            {
                var ThongKe = qltv.Database.SqlQuery<ThongKeDauSach>("exec show_ThongKeDauSach ");
                dtgv_ThongKeDS.DataSource = ThongKe.ToList();
            }

        }

        private void ThongKe_Load(object sender, EventArgs e)
        {

            Load_ThongKeSach();
            Load_ThongKeDG();
            Load_ThongKeDS();
            Func<double, string> formatFunc = (x) => string.Format("{0:0.0}", x);
            LCBieuDoThongKe.AxisX.Add(new Axis
            {
                Title = "THÁNG",
                Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" },
                FontSize = 14,
                Foreground = Brushes.Green,
                FontWeight = FontWeights.Bold,
                Separator = new Separator
                {
                    StrokeThickness = 1,
                    StrokeDashArray = new System.Windows.Media.DoubleCollection(new double[] { 2 }),
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(64, 79, 86))
                }
            });  
            LCBieuDoThongKe.AxisY.Add(new Axis
            {
                Title = "LƯỢT MƯỢN",
                LabelFormatter = formatFunc,
                FontSize = 14,
                Foreground = Brushes.Green,
                FontWeight = FontWeights.Bold,
                MinRange = 1,
                MinValue = -1,
                 Separator = new Separator
                 {
                     StrokeThickness = 1,
                     StrokeDashArray = new System.Windows.Media.DoubleCollection(new double[] { 2 }),
                     Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(64, 79, 86)) 
                 }
            });
            Load_LineChart();
        }

        private void cbbNamThongKe_SelectedIndexChanged(object sender, EventArgs e)
        {

            Load_LineChart();
        }

    
    }
}
