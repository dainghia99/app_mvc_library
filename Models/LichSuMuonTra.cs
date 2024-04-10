using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace appmvclibrary.Models
{
    public class LichSuMuonTra
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string MaSinhVien { get; set; }
        public string Lop { get; set; }
        
        public List<PhieuMuonTra> PhieuMuonTras { get; set; }

    }
}