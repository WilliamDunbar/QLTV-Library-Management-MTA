using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Thu_Vien.Database
{
    class TT_MuonSach_DocGia
    {
        [Key]
        public int MaMuonTra { get; set; }

        [Required]
        [StringLength(10)]
        public string MaSach { get; set; }
        [StringLength(10)]
        public string MaNVmuon { get; set; }
        public DateTime? NgayMuon { get; set; }
        public DateTime? NgayHanTra { get; set; }
    }
}
