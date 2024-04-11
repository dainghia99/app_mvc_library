using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace appmvclibrary.Models.User
{
    public class AppUser : IdentityUser
    {
        [Display(Name = "Họ và tên")]
        public string? FullName { get; set; }

        [Column(TypeName = "nvarchar")]
        [StringLength(400)] 
    
        [Display(Name = "Địa chỉ")]
        public string? HomeAdress { get; set; }
        [Display(Name = "Mã sinh viên")]
        public string? StudentCode { get; set; }

        [Display(Name = "Lớp")]
        public string? Lop { get; set; }
        [Display(Name = "Khoa")]
        public string? Khoa { get; set; }

        // [Required]       
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }
    }
}