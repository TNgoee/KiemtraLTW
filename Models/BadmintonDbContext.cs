using Microsoft.EntityFrameworkCore;

namespace Kiemtra.Models
{
    public class BadmintonDbContext : DbContext
    {
        public BadmintonDbContext(DbContextOptions<BadmintonDbContext> options) : base(options) { }

        public DbSet<GiaiDau> GiaiDaus { get; set; }
        public DbSet<LichThiDau> LichThiDaus { get; set; }
        public DbSet<TranDau> TranDaus { get; set; }
        public DbSet<TrongTai> TrongTais { get; set; }
        public DbSet<VanDongVien> VanDongViens { get; set; }
        public DbSet<DoiTuyen> DoiTuyens { get; set; }
        public DbSet<HLVTruong> HLVTruongs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Tắt cascade delete cho các FK của TranDau
            modelBuilder.Entity<TranDau>()
                .HasOne(td => td.VanDongVien1)
                .WithMany()
                .HasForeignKey(td => td.MaVDV1)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TranDau>()
                .HasOne(td => td.VanDongVien2)
                .WithMany()
                .HasForeignKey(td => td.MaVDV2)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TranDau>()
                .HasOne(td => td.VanDongVien3)
                .WithMany()
                .HasForeignKey(td => td.MaVDV3)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TranDau>()
                .HasOne(td => td.VanDongVien4)
                .WithMany()
                .HasForeignKey(td => td.MaVDV4)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}