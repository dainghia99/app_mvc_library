using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using appmvclibrary.Models;

namespace appmvclibrary.Areas.QuanLySach.Models
{
    public class ThemMoiSachModel : Sach
    {
        [Display(Name = "Chọn thể loại sách")]
        public int[]? CategoryIds { get; set; }
    }
}