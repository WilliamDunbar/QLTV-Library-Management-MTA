using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Thu_Vien.Database
{
    class ThongKeDauSach
    {
        [StringLength(50)]
        public string TenDauSach { get; set; }
        public int? SoLuong { get; set; }
        public int? SoLuotMuon { get; set; }
    }
}
