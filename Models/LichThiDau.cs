using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kiemtra.Models
{
    public class LichThiDau
    {
        [Key]
        public string MaLich { get; set; }
        [ForeignKey("GiaiDau")]
        public string MaGiai { get; set; }
        public DateTime NgayThiDau { get; set; }
        public string DiaDiem { get; set; }

        public GiaiDau GiaiDau { get; set; }
        public ICollection<TranDau> TranDaus { get; set; }
    }
}