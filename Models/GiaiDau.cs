using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kiemtra.Models
{
    public class GiaiDau
    {
        [Key]
        public string MaGiai { get; set; }
        public string TenGiai { get; set; }
        public string LoaiGiai { get; set; }
        public DateTime ThoiGian { get; set; }
        public string DiaDiem { get; set; }

        public ICollection<LichThiDau> LichThiDaus { get; set; }
    }
}