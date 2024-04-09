using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace appmvclibrary.Models
{
    public class Contact
    {
        [Key]
        public int Key { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập trường này")]
        [Display(Name = "Họ và tên", Prompt = "Họ và tên")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập trường này")]
        [Display(Name = "Địa chỉ email", Prompt = "Địa chỉ email")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập trường này")]
        [Display(Name = "Số điện thoại", Prompt = "Số điện thoại")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string Phone { get; set; }
        [Display(Name = "Tiêu đề", Prompt = "Tiêu đề")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập trường này")]
        [Display(Name = "Nội dung của liên hệ", Prompt = "Nội dung")]
        public string Message { get; set; }
    }
}