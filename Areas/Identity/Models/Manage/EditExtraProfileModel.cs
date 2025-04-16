using System;
using System.ComponentModel.DataAnnotations;

namespace App.Areas.Identity.Models.ManageViewModels
{
  public class EditExtraProfileModel
  {
      [Display(Name = "Tên tài khoản")]
      public string UserName { get; set; }

      [Display(Name = "Địa chỉ email")]
      public string UserEmail { get; set; }
      [Display(Name = "Số điện thoại")]
      public string PhoneNumber { get; set; }

      [Display(Name = "Địa chỉ")]
      [StringLength(400)]
      public string HomeAdress { get; set; }


      [Display(Name = "Ngày sinh")]
      public DateTime? BirthDate { get; set; }
      [Display(Name = "Mã sinh viên")]
      public string? StudentCode { get;  set; }
      [Display(Name = "Khoa")]
      public string? Khoa { get;  set; }
      [Display(Name = "Lớp")]
      public string? Lop { get;  set; }
      [Display(Name = "Họ tên đầy đủ")]
      public string? FullName { get;  set; }
    }
}