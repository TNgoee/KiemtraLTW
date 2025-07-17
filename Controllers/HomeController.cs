using Microsoft.AspNetCore.Mvc;
using Kiemtra.Models;
using Microsoft.EntityFrameworkCore;

namespace Kiemtra.Controllers
{
    public class HomeController : Controller
    {
        private readonly BadmintonDbContext _context;

        public HomeController(BadmintonDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> DSVanDongVien()
        {
            var vanDongViens = await _context.VanDongViens.ToListAsync();
            return View(vanDongViens);
        }

        public async Task<IActionResult> DSTrongTai()
        {
            var trongTais = await _context.TrongTais.ToListAsync();
            return View(trongTais);
        }

        public async Task<IActionResult> DSTranDau()
        {
            var tranDaus = await _context.TranDaus.ToListAsync();
            return View(tranDaus);
        }

        public async Task<IActionResult> DSDoiTuyen()
        {
            var doiTuyens = await _context.DoiTuyens.ToListAsync();
            return View(doiTuyens);
        }

        public async Task<IActionResult> DSLichThiDau()
        {
            var lichThiDaus = await _context.LichThiDaus.ToListAsync();
            return View(lichThiDaus);
        }

        public async Task<IActionResult> DSGiaiDau()
        {
            var giaiDaus = await _context.GiaiDaus.ToListAsync();
            return View(giaiDaus);
        }

        [HttpGet]
        public IActionResult TraCuuDiem()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TraCuuDiem(string tenVDV)
        {
            var vdvList = await _context.VanDongViens.Where(v => v.HoTen == tenVDV).ToListAsync();
            var tranDaus = await _context.TranDaus.ToListAsync();
            var giaiDaus = await _context.GiaiDaus.ToListAsync();
            var lichThiDaus = await _context.LichThiDaus.ToListAsync();

            var result = new List<TraCuuVDVResult>();

            foreach (var vdv in vdvList)
            {
                var tranThamGia = tranDaus.Where(t =>
                    t.MaVDV1 == vdv.MaVDV || t.MaVDV2 == vdv.MaVDV ||
                    t.MaVDV3 == vdv.MaVDV || t.MaVDV4 == vdv.MaVDV).ToList();

                int tongDiem = 0;
                var chiTiet = new List<string>();

                foreach (var tran in tranThamGia)
                {
                    var lich = lichThiDaus.FirstOrDefault(l => l.MaLich == tran.MaLich);
                    var giai = giaiDaus.FirstOrDefault(g => g.MaGiai == lich?.MaGiai);
                    string loaiGiai = giai?.LoaiGiai ?? "";
                    string vong = RemoveDiacritics(tran.VongDau ?? "").ToLower();

                    int diem = 0;
                    bool laDoi = loaiGiai.Contains("Đôi");
                    bool laDon = loaiGiai.Contains("Đơn");

                    // Xác định thắng trận: đúng đội thắng dựa vào KetQua và vị trí MaVDV1-4
                    bool laThang = false;
                    if (!string.IsNullOrEmpty(tran.KetQua))
                    {
                        var ketQua = tran.KetQua.Split('-');
                        if (ketQua.Length == 2)
                        {
                            int score1, score2;
                            if (int.TryParse(ketQua[0], out score1) && int.TryParse(ketQua[1], out score2))
                            {
                                // Nếu vận động viên là MaVDV1 hoặc MaVDV2 (đội 1) và score1 > score2 => thắng
                                // Nếu vận động viên là MaVDV3 hoặc MaVDV4 (đội 2) và score2 > score1 => thắng
                                if ((tran.MaVDV1 == vdv.MaVDV || tran.MaVDV2 == vdv.MaVDV) && score1 > score2)
                                    laThang = true;
                                if ((tran.MaVDV3 == vdv.MaVDV || tran.MaVDV4 == vdv.MaVDV) && score2 > score1)
                                    laThang = true;
                            }
                        }
                    }

                    if (laThang)
                    {
                        // Nhận diện vòng đấu
                        int diemVongDon = 0, diemVongDoi = 0;
                        if (vong.Contains("chung")) { diemVongDon = 500; diemVongDoi = 450; }
                        else if (vong.Contains("ban") || vong.Contains("vong 3")) { diemVongDon = 400; diemVongDoi = 350; }
                        else if (vong.Contains("tu") || vong.Contains("vong 2")) { diemVongDon = 300; diemVongDoi = 250; }
                        else if (vong.Contains("loai") || vong.Contains("vong 1")) { diemVongDon = 200; diemVongDoi = 150; }

                        if (laDon) diem = diemVongDon;
                        else if (laDoi) diem = diemVongDoi;
                    }

                    tongDiem += diem;
                    chiTiet.Add($"{tran.MaTran} - {giai?.TenGiai} - {tran.VongDau} - {(laThang ? "Thắng" : "Thua")} - +{diem} điểm");
                }

                result.Add(new TraCuuVDVResult
                {
                    VDV = vdv,
                    TranThamGia = chiTiet,
                    TongDiem = tongDiem
                });
            }

            ViewBag.TenVDV = tenVDV;
            return View(result);
        }

        // Hàm chuẩn hóa tiếng Việt không dấu
        public static string RemoveDiacritics(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;
            var normalized = text.Normalize(System.Text.NormalizationForm.FormD);
            var sb = new System.Text.StringBuilder();
            foreach (var c in normalized)
            {
                if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) != System.Globalization.UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }
            return sb.ToString().Normalize(System.Text.NormalizationForm.FormC);
        }

        public class TraCuuVDVResult
        {
            public VanDongVien VDV { get; set; }
            public List<string> TranThamGia { get; set; }
            public int TongDiem { get; set; }
        }
    }
}