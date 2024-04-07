using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace appmvclibrary.Models
{
    public class TacGia
    {
        [Key]
        public int Id {get; set;}
        [Display(Name = "Tên tác giả")]
        // [RegularExpression(@"^[^\W_""]*$", ErrorMessage = "Chỉ dùng các ký tự thường")]
        [Required(ErrorMessage = "Vui lòng nhập trường này")]
        public string Name {get; set;}
        [Display(Name = "Ngày sinh")]
        public DateTime? NgaySinh {get; set;}

        [Display(Name = "Tiểu sử")]
        public string? TieuSu {get; set;}
        public DateTime? CreatedAt {get; set;}
        public DateTime? UpdatedAt {get; set;}
        public virtual List<TacGiaSach>? TacGiaSach {get; set;}
    }
}