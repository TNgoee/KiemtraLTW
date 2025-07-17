using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kiemtra.Models
{
    public class HLVTruong
    {
        [Key]
        public string MaHLV { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string Sdt { get; set; }
        public string ChuyenMon { get; set; }

        public ICollection<DoiTuyen> DoiTuyens { get; set; }
    }
}