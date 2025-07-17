using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kiemtra.Models
{
    public class DoiTuyen
    {
        [Key]
        public string MaDoi { get; set; }
        public string TenDoi { get; set; }
        [ForeignKey("HLVTruong")]
        public string MaHLV { get; set; }

        public HLVTruong HLVTruong { get; set; }
        public ICollection<VanDongVien> VanDongViens { get; set; }
    }
}