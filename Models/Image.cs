using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace appmvclibrary.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        
        public Sach Sach { get; set; }
    }
}