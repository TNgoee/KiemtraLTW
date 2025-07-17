using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kiemtra.Models
{
    public class TrongTai
    {
        [Key]
        public string MaTT { get; set; }
        public string HoTen { get; set; }
        public int SoTran { get; set; }
        public float HeSoLuong { get; set; }
        public float LuongCoBan { get; set; }

        public ICollection<TranDau> TranDaus { get; set; }
    }
}