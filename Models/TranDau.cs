using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kiemtra.Models
{
    public class TranDau
    {
        [Key]
        public string MaTran { get; set; }
        public string VongDau { get; set; }
        [ForeignKey("LichThiDau")]
        public string MaLich { get; set; }
        [ForeignKey("TrongTai")]
        public string MaTT { get; set; }
        [ForeignKey("VanDongVien1")]
        public string? MaVDV1 { get; set; }
        [ForeignKey("VanDongVien2")]
        public string? MaVDV2 { get; set; }
        [ForeignKey("VanDongVien3")]
        public string? MaVDV3 { get; set; }
        [ForeignKey("VanDongVien4")]
        public string? MaVDV4 { get; set; }
        public string KetQua { get; set; }

        public LichThiDau LichThiDau { get; set; }
        public TrongTai TrongTai { get; set; }
        public VanDongVien VanDongVien1 { get; set; }
        public VanDongVien VanDongVien2 { get; set; }
        public VanDongVien VanDongVien3 { get; set; }
        public VanDongVien VanDongVien4 { get; set; }
    }
}