using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace appmvclibrary.Models
{
    public class SachCategory
    {
        [ForeignKey("SachId")]
        public int SachId {get; set; }
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }
        public Sach? Sach { get; set; }
    }
}