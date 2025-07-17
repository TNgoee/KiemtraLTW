using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kiemtra.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GiaiDaus",
                columns: table => new
                {
                    MaGiai = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenGiai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoaiGiai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThoiGian = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiaDiem = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaiDaus", x => x.MaGiai);
                });

            migrationBuilder.CreateTable(
                name: "HLVTruongs",
                columns: table => new
                {
                    MaHLV = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sdt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChuyenMon = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HLVTruongs", x => x.MaHLV);
                });

            migrationBuilder.CreateTable(
                name: "TrongTais",
                columns: table => new
                {
                    MaTT = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoTran = table.Column<int>(type: "int", nullable: false),
                    HeSoLuong = table.Column<float>(type: "real", nullable: false),
                    LuongCoBan = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrongTais", x => x.MaTT);
                });

            migrationBuilder.CreateTable(
                name: "LichThiDaus",
                columns: table => new
                {
                    MaLich = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaGiai = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NgayThiDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiaDiem = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LichThiDaus", x => x.MaLich);
                    table.ForeignKey(
                        name: "FK_LichThiDaus_GiaiDaus_MaGiai",
                        column: x => x.MaGiai,
                        principalTable: "GiaiDaus",
                        principalColumn: "MaGiai",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoiTuyens",
                columns: table => new
                {
                    MaDoi = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenDoi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaHLV = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoiTuyens", x => x.MaDoi);
                    table.ForeignKey(
                        name: "FK_DoiTuyens_HLVTruongs_MaHLV",
                        column: x => x.MaHLV,
                        principalTable: "HLVTruongs",
                        principalColumn: "MaHLV",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VanDongViens",
                columns: table => new
                {
                    MaVDV = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NamSinh = table.Column<int>(type: "int", nullable: false),
                    GioiTinh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaDoi = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Diem = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VanDongViens", x => x.MaVDV);
                    table.ForeignKey(
                        name: "FK_VanDongViens_DoiTuyens_MaDoi",
                        column: x => x.MaDoi,
                        principalTable: "DoiTuyens",
                        principalColumn: "MaDoi",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TranDaus",
                columns: table => new
                {
                    MaTran = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VongDau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaLich = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaTT = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaVDV1 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaVDV2 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaVDV3 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaVDV4 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    KetQua = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TranDaus", x => x.MaTran);
                    table.ForeignKey(
                        name: "FK_TranDaus_LichThiDaus_MaLich",
                        column: x => x.MaLich,
                        principalTable: "LichThiDaus",
                        principalColumn: "MaLich",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TranDaus_TrongTais_MaTT",
                        column: x => x.MaTT,
                        principalTable: "TrongTais",
                        principalColumn: "MaTT",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TranDaus_VanDongViens_MaVDV1",
                        column: x => x.MaVDV1,
                        principalTable: "VanDongViens",
                        principalColumn: "MaVDV",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TranDaus_VanDongViens_MaVDV2",
                        column: x => x.MaVDV2,
                        principalTable: "VanDongViens",
                        principalColumn: "MaVDV",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TranDaus_VanDongViens_MaVDV3",
                        column: x => x.MaVDV3,
                        principalTable: "VanDongViens",
                        principalColumn: "MaVDV",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TranDaus_VanDongViens_MaVDV4",
                        column: x => x.MaVDV4,
                        principalTable: "VanDongViens",
                        principalColumn: "MaVDV",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoiTuyens_MaHLV",
                table: "DoiTuyens",
                column: "MaHLV");

            migrationBuilder.CreateIndex(
                name: "IX_LichThiDaus_MaGiai",
                table: "LichThiDaus",
                column: "MaGiai");

            migrationBuilder.CreateIndex(
                name: "IX_TranDaus_MaLich",
                table: "TranDaus",
                column: "MaLich");

            migrationBuilder.CreateIndex(
                name: "IX_TranDaus_MaTT",
                table: "TranDaus",
                column: "MaTT");

            migrationBuilder.CreateIndex(
                name: "IX_TranDaus_MaVDV1",
                table: "TranDaus",
                column: "MaVDV1");

            migrationBuilder.CreateIndex(
                name: "IX_TranDaus_MaVDV2",
                table: "TranDaus",
                column: "MaVDV2");

            migrationBuilder.CreateIndex(
                name: "IX_TranDaus_MaVDV3",
                table: "TranDaus",
                column: "MaVDV3");

            migrationBuilder.CreateIndex(
                name: "IX_TranDaus_MaVDV4",
                table: "TranDaus",
                column: "MaVDV4");

            migrationBuilder.CreateIndex(
                name: "IX_VanDongViens_MaDoi",
                table: "VanDongViens",
                column: "MaDoi");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TranDaus");

            migrationBuilder.DropTable(
                name: "LichThiDaus");

            migrationBuilder.DropTable(
                name: "TrongTais");

            migrationBuilder.DropTable(
                name: "VanDongViens");

            migrationBuilder.DropTable(
                name: "GiaiDaus");

            migrationBuilder.DropTable(
                name: "DoiTuyens");

            migrationBuilder.DropTable(
                name: "HLVTruongs");
        }
    }
}
