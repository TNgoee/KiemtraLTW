using Microsoft.AspNetCore.Mvc;
using Kiemtra.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Kiemtra.ViewComponents
{
    public class VanDongVienStatsViewComponent : ViewComponent
    {
        private readonly BadmintonDbContext _context;
        public VanDongVienStatsViewComponent(BadmintonDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var now = DateTime.Now.Year;
            var vanDongViens = await _context.VanDongViens.ToListAsync();
            var giaiDaus = await _context.GiaiDaus.ToListAsync();
            var lichThiDaus = await _context.LichThiDaus.ToListAsync();
            var tranDaus = await _context.TranDaus.ToListAsync();

            var loaiGiaiList = giaiDaus.Select(g => g.LoaiGiai).Distinct().ToList();
            var stats = new List<StatItem>();
            foreach (var loai in loaiGiaiList)
            {
                var giaiIds = giaiDaus.Where(g => g.LoaiGiai == loai).Select(g => g.MaGiai).ToList();
                var lichIds = lichThiDaus.Where(l => giaiIds.Contains(l.MaGiai)).Select(l => l.MaLich).ToList();
                var tranTrongLoai = tranDaus.Where(t => lichIds.Contains(t.MaLich)).ToList();
                var vdvIds = tranTrongLoai
                    .SelectMany(t => new[] { t.MaVDV1, t.MaVDV2, t.MaVDV3, t.MaVDV4 })
                    .Where(id => !string.IsNullOrEmpty(id))
                    .Distinct()
                    .ToList();
                var vdvTrongLoai = vanDongViens.Where(v => vdvIds.Contains(v.MaVDV)).ToList();
                stats.Add(new StatItem
                {
                    LoaiGiai = loai,
                    SoLuongVDV = vdvTrongLoai.Count,
                    TuoiTrungBinh = vdvTrongLoai.Any() ? Math.Round(vdvTrongLoai.Average(v => now - v.NamSinh), 1) : 0
                });
            }
            var tongSoLuong = vanDongViens.Count();
            var tuoiTB = vanDongViens.Any() ? Math.Round(vanDongViens.Average(v => now - v.NamSinh), 1) : 0;
            return View(new VanDongVienStatsViewModel
            {
                Stats = stats,
                TongSoLuong = tongSoLuong,
                TuoiTrungBinhToanBo = tuoiTB
            });
        }
    }
    public class VanDongVienStatsViewModel
    {
        public List<StatItem> Stats { get; set; }
        public int TongSoLuong { get; set; }
        public double TuoiTrungBinhToanBo { get; set; }
    }
    public class StatItem
    {
        public string LoaiGiai { get; set; }
        public int SoLuongVDV { get; set; }
        public double TuoiTrungBinh { get; set; }
    }
}