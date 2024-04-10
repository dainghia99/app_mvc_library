using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace appmvclibrary.Models
{
    public class PhieuMuonTra
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lop { get; set; }
        public string MaSinhVien { get; set; }
        public bool TrangThai { get; set; }
        public int Quantity { get; set; }
        public Sach? sach { get; set; }
        public DateTime NgayMuon { get; set; }
        public DateTime? NgayTra { get; set; }
        public DateTime NgayTaoPhieu { get; set; }
    }
}