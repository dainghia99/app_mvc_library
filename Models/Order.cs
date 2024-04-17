using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace appmvclibrary.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public List<PhieuMuonTra>? PhieuMuonTra {set; get;}
        public LichSuMuonTra? LichSuMuonTra { set; get; }
    }
}