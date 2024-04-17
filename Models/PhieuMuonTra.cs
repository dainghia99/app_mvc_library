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
        [Display(Name = "Tên sinh viên")]
        public string Name { get; set; }
        [Display(Name = "Lớp")]
        public string Lop { get; set; }
        [Display(Name = "Mã sinh viên")]
        public string MaSinhVien { get; set; }
        [Display(Name = "Trạng thái")]
        public bool TrangThai { get; set; }
        public int Quantity { get; set; }
        [Display(Name = "Sách mượn")]
        public Sach? sach { get; set; }
        [Display(Name = "Ngày mượn")]
        public DateTime NgayMuon { get; set; }
        [Display(Name = "Ngày đến hạn")]
        [Required(ErrorMessage = "Vui lòng chọn ngày trả sách")]
        public DateTime NgayTra { get; set; }
        public DateTime NgayTaoPhieu { get; set; }

        public Order? Order { get; set; }
        
    }
}