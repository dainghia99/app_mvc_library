using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace appmvclibrary.Models
{
    public class LichSuMuonTra
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Họ tên sinh viên")]
        public string Name { get; set; }
        [Display(Name = "Mã sinh viên")]
        public string MaSinhVien { get; set; }
        [Display(Name = "Lớp")]
        public string Lop { get; set; }

        public bool TrangThai { get; set; }
        public Sach? sach { get; set; }
        public List<Order>? Orders { get; set; }

    }
}