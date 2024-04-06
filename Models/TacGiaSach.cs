using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace appmvclibrary.Models
{
    public class TacGiaSach
    {
        [ForeignKey("SachId")]
        public int SachId {get; set;}
        [ForeignKey("TacGiaId")]
        public int TacGiaId {get; set;}
        public virtual TacGia? TacGia {get; set;}
        public virtual Sach? Sach {get; set;}
    }
}