using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Kiemtra.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IServiceProvider serviceProvider)
        {
            using (var context = new BadmintonDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<BadmintonDbContext>>()))
            {
                // GiaiDau
                if (!context.GiaiDaus.Any())
                {
                    context.GiaiDaus.AddRange(
                        new GiaiDau { MaGiai = "GD1", TenGiai = "Giải Mở Rộng", LoaiGiai = "Đơn", ThoiGian = new DateTime(2024, 5, 1), DiaDiem = "Hà Nội" },
                        new GiaiDau { MaGiai = "GD2", TenGiai = "Giải Trẻ", LoaiGiai = "Đôi", ThoiGian = new DateTime(2024, 6, 10), DiaDiem = "TP.HCM" },
                        new GiaiDau { MaGiai = "GD3", TenGiai = "Giải Quốc Gia", LoaiGiai = "Đơn", ThoiGian = new DateTime(2024, 7, 20), DiaDiem = "Đà Nẵng" }
                    );
                }
                // HLVTruong
                if (!context.HLVTruongs.Any())
                {
                    context.HLVTruongs.AddRange(
                        new HLVTruong { MaHLV = "HLV1", HoTen = "Nguyễn Văn A", NgaySinh = new DateTime(1980, 1, 1), Sdt = "0901234567", ChuyenMon = "Chuyên gia đơn" },
                        new HLVTruong { MaHLV = "HLV2", HoTen = "Trần Thị B", NgaySinh = new DateTime(1975, 2, 2), Sdt = "0912345678", ChuyenMon = "Chuyên gia đôi" },
                        new HLVTruong { MaHLV = "HLV3", HoTen = "Lê Văn C", NgaySinh = new DateTime(1985, 3, 3), Sdt = "0923456789", ChuyenMon = "Chuyên gia trẻ" }
                    );
                }
                // DoiTuyen
                if (!context.DoiTuyens.Any())
                {
                    context.DoiTuyens.AddRange(
                        new DoiTuyen { MaDoi = "DT1", TenDoi = "Đội 1", MaHLV = "HLV1" },
                        new DoiTuyen { MaDoi = "DT2", TenDoi = "Đội 2", MaHLV = "HLV2" },
                        new DoiTuyen { MaDoi = "DT3", TenDoi = "Đội 3", MaHLV = "HLV3" }
                    );
                }
                // VanDongVien
                if (!context.VanDongViens.Any())
                {
                    context.VanDongViens.AddRange(
                        new VanDongVien { MaVDV = "VDV1", HoTen = "Phạm Văn D", NamSinh = 2000, GioiTinh = "Nam", MaDoi = "DT1", Diem = 100 },
                        new VanDongVien { MaVDV = "VDV2", HoTen = "Ngô Thị E", NamSinh = 2001, GioiTinh = "Nữ", MaDoi = "DT2", Diem = 120 },
                        new VanDongVien { MaVDV = "VDV3", HoTen = "Đỗ Văn F", NamSinh = 2002, GioiTinh = "Nam", MaDoi = "DT3", Diem = 110 }
                    );
                }
                // Thêm VDV4 vào seed VanDongVien nếu chưa có
                if (!context.VanDongViens.Any(v => v.MaVDV == "VDV4"))
                {
                    context.VanDongViens.Add(
                        new VanDongVien { MaVDV = "VDV4", HoTen = "Lý Thị G", NamSinh = 2003, GioiTinh = "Nữ", MaDoi = "DT1", Diem = 90 }
                    );
                }
                // TrongTai
                if (!context.TrongTais.Any())
                {
                    context.TrongTais.AddRange(
                        new TrongTai { MaTT = "TT1", HoTen = "Trọng Tài 1", SoTran = 10, HeSoLuong = 1.2f, LuongCoBan = 5000000 },
                        new TrongTai { MaTT = "TT2", HoTen = "Trọng Tài 2", SoTran = 15, HeSoLuong = 1.3f, LuongCoBan = 6000000 },
                        new TrongTai { MaTT = "TT3", HoTen = "Trọng Tài 3", SoTran = 12, HeSoLuong = 1.1f, LuongCoBan = 5500000 }
                    );
                }
                // LichThiDau
                if (!context.LichThiDaus.Any())
                {
                    context.LichThiDaus.AddRange(
                        new LichThiDau { MaLich = "L1", MaGiai = "GD1", NgayThiDau = new DateTime(2024, 5, 2), DiaDiem = "Hà Nội" },
                        new LichThiDau { MaLich = "L2", MaGiai = "GD2", NgayThiDau = new DateTime(2024, 6, 11), DiaDiem = "TP.HCM" },
                        new LichThiDau { MaLich = "L3", MaGiai = "GD3", NgayThiDau = new DateTime(2024, 7, 21), DiaDiem = "Đà Nẵng" }
                    );
                }
                // TranDau
                if (!context.TranDaus.Any())
                {
                    context.TranDaus.AddRange(
                        // T1: Đơn, VDV1 vs VDV2, đội 1 thắng
                        new TranDau { MaTran = "T1", VongDau = "Vòng 1", MaLich = "L1", MaTT = "TT1", MaVDV1 = "VDV1", MaVDV2 = null, MaVDV3 = "VDV2", MaVDV4 = null, KetQua = "2-0" },
                        // T2: Đôi, VDV2+VDV3 vs VDV1+VDV4 (giả sử có VDV4), đội 1 thắng
                        new TranDau { MaTran = "T2", VongDau = "Vòng 2", MaLich = "L2", MaTT = "TT2", MaVDV1 = "VDV2", MaVDV2 = "VDV3", MaVDV3 = "VDV1", MaVDV4 = "VDV4", KetQua = "2-1" },
                        // T3: Đơn, VDV3 vs VDV1, đội 2 thắng
                        new TranDau { MaTran = "T3", VongDau = "Vòng 3", MaLich = "L3", MaTT = "TT3", MaVDV1 = "VDV3", MaVDV2 = null, MaVDV3 = "VDV1", MaVDV4 = null, KetQua = "0-2" }
                    );
                }
                context.SaveChanges();
            }
        }
    }
}