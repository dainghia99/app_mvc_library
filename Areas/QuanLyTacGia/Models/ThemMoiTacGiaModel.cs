using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using appmvclibrary.Models;

namespace appmvclibrary.Areas.QuanLyTacGia.Models
{
    public class ThemMoiTacGiaModel : TacGia
    {
        [Display(Name = "Sách đã sáng tác")]
        public int[]? SachIds { get; set; }
    }
}