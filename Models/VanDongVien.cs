using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kiemtra.Models
{
    public class VanDongVien
    {
        [Key]
        public string MaVDV { get; set; }
        public string HoTen { get; set; }
        public int NamSinh { get; set; }
        public string GioiTinh { get; set; }
        [ForeignKey("DoiTuyen")]
        public string MaDoi { get; set; }
        public int Diem { get; set; }

        public DoiTuyen DoiTuyen { get; set; }
    }
}