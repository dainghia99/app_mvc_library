using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace appmvclibrary.Models
{
    public class Sach
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Tên sách")]
        [Required(ErrorMessage = "Vui lòng nhập trường này")]
        public string TenSach { get; set; }
        [Display(Name = "Mô tả ngắn")]
        [Required(ErrorMessage = "Vui lòng nhập trường này")]
        public string MoTaNgan { get; set; }
        [Display(Name = "Mô tả")]
        public string Description { get; set; }
        [Display(Name = "Trạng thái công khai")]
        public bool IsPublic { get; set; }
        [Display(Name = "Trạng thái hiện tại")]
        public int State { get; set;}
        [Display(Name = "Số lượng")]
        public int Quantity { get; set; }
        [Display(Name = "Giá")]
        public Decimal Gia {get; set; }
        [Display(Name = "Chuỗi hiển thị")]
        public string Slug { get; set; }

        public virtual List<TacGiaSach>? TacGiaSach { get; set; }
        public List<SachCategory>? SachCategories {get; set;}
        [Display(Name = "Ngày tạo")]
        public DateTime Created { get; set; }
        [Display(Name = "Ngày cập nhật")]
        public DateTime UpdatAt { get; set; }
    }
}