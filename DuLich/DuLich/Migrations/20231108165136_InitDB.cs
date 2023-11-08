using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DuLich.Migrations
{
    public partial class InitDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NguoiDungs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sdt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatKhau = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    PhanQuyen = table.Column<int>(type: "int", nullable: false),
                    NoiO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GioiTinh = table.Column<int>(type: "int", nullable: false),
                    CCCD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnhDaiDien = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDungs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiemThamQuans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChuDichVu = table.Column<int>(type: "int", nullable: false),
                    TenDiaDiem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiaDiem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnhDaiDien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiemThamQuans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiemThamQuans_NguoiDungs_ChuDichVu",
                        column: x => x.ChuDichVu,
                        principalTable: "NguoiDungs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KhachSans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChuDichVu = table.Column<int>(type: "int", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gia = table.Column<int>(type: "int", nullable: false),
                    TenKhachSan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnhDaiDien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChiTietKhachSan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoTaKhachSan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DanhGia = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachSans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KhachSans_NguoiDungs_ChuDichVu",
                        column: x => x.ChuDichVu,
                        principalTable: "NguoiDungs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NhaHangs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChuDichVu = table.Column<int>(type: "int", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TenNhaHang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnhDaiDien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChiTietNhaHang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoTaNhaHang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DanhGia = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhaHangs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NhaHangs_NguoiDungs_ChuDichVu",
                        column: x => x.ChuDichVu,
                        principalTable: "NguoiDungs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChuTour = table.Column<int>(type: "int", nullable: false),
                    TenTour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoNgay = table.Column<int>(type: "int", nullable: false),
                    LuotDanhGia = table.Column<int>(type: "int", nullable: false),
                    ChiTietTour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KhuyenMaiTour = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MoTaTour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HinhAnhTour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GiaTour = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tours_NguoiDungs_ChuTour",
                        column: x => x.ChuTour,
                        principalTable: "NguoiDungs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VanChuyens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChuDichVu = table.Column<int>(type: "int", nullable: false),
                    DiaChiDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChiDi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AnhDaiDien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChiTietDiemDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChiTietDiemDi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThoiGianBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThoiGianKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TaiXe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VanChuyens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VanChuyens_NguoiDungs_ChuDichVu",
                        column: x => x.ChuDichVu,
                        principalTable: "NguoiDungs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiemThamQuanCTs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDichVu = table.Column<int>(type: "int", nullable: false),
                    AnhChiTiet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoTaDichVu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DanhGia = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiemThamQuanCTs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiemThamQuanCTs_DiemThamQuans_MaDichVu",
                        column: x => x.MaDichVu,
                        principalTable: "DiemThamQuans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BinhLuanKhachSan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdKhachSan = table.Column<int>(type: "int", nullable: true),
                    IdNguoiDung = table.Column<int>(type: "int", nullable: true),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThoiGian = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BinhLuanKhachSan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BinhLuanKhachSan_KhachSans_IdKhachSan",
                        column: x => x.IdKhachSan,
                        principalTable: "KhachSans",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BinhLuanKhachSan_NguoiDungs_IdNguoiDung",
                        column: x => x.IdNguoiDung,
                        principalTable: "NguoiDungs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DatKhachSans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdNguoiDung = table.Column<int>(type: "int", nullable: true),
                    IdKhachSan = table.Column<int>(type: "int", nullable: true),
                    NgayDat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayNhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayTra = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatKhachSans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DatKhachSans_KhachSans_IdKhachSan",
                        column: x => x.IdKhachSan,
                        principalTable: "KhachSans",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DatKhachSans_NguoiDungs_IdNguoiDung",
                        column: x => x.IdNguoiDung,
                        principalTable: "NguoiDungs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BinhLuanNhaHang",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdNhaHang = table.Column<int>(type: "int", nullable: true),
                    IdNguoiDung = table.Column<int>(type: "int", nullable: true),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThoiGian = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BinhLuanNhaHang", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BinhLuanNhaHang_NguoiDungs_IdNguoiDung",
                        column: x => x.IdNguoiDung,
                        principalTable: "NguoiDungs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BinhLuanNhaHang_NhaHangs_IdNhaHang",
                        column: x => x.IdNhaHang,
                        principalTable: "NhaHangs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DatNhaHangs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdNguoiDung = table.Column<int>(type: "int", nullable: true),
                    IdNhaHang = table.Column<int>(type: "int", nullable: true),
                    NgayDat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayNhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayTra = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatNhaHangs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DatNhaHangs_NguoiDungs_IdNguoiDung",
                        column: x => x.IdNguoiDung,
                        principalTable: "NguoiDungs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DatNhaHangs_NhaHangs_IdNhaHang",
                        column: x => x.IdNhaHang,
                        principalTable: "NhaHangs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DatTours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdNguoiDung = table.Column<int>(type: "int", nullable: true),
                    IdTour = table.Column<int>(type: "int", nullable: true),
                    NgayDat = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatTours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DatTours_NguoiDungs_IdNguoiDung",
                        column: x => x.IdNguoiDung,
                        principalTable: "NguoiDungs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DatTours_Tours_IdTour",
                        column: x => x.IdTour,
                        principalTable: "Tours",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TourCTs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChuTour = table.Column<int>(type: "int", nullable: false),
                    MaTour = table.Column<int>(type: "int", nullable: false),
                    LichTrinhNgay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChiTietLichTrinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaDiem = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourCTs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourCTs_Tours_MaTour",
                        column: x => x.MaTour,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BinhLuanVanChuyen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdVanChuyen = table.Column<int>(type: "int", nullable: true),
                    IdNguoiDung = table.Column<int>(type: "int", nullable: true),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThoiGian = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BinhLuanVanChuyen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BinhLuanVanChuyen_NguoiDungs_IdNguoiDung",
                        column: x => x.IdNguoiDung,
                        principalTable: "NguoiDungs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BinhLuanVanChuyen_VanChuyens_IdVanChuyen",
                        column: x => x.IdVanChuyen,
                        principalTable: "VanChuyens",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DatVanChuyens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdNguoiDung = table.Column<int>(type: "int", nullable: true),
                    IdVanChuyen = table.Column<int>(type: "int", nullable: true),
                    NgayDat = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatVanChuyens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DatVanChuyens_NguoiDungs_IdNguoiDung",
                        column: x => x.IdNguoiDung,
                        principalTable: "NguoiDungs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DatVanChuyens_VanChuyens_IdVanChuyen",
                        column: x => x.IdVanChuyen,
                        principalTable: "VanChuyens",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "NguoiDungs",
                columns: new[] { "Id", "AnhDaiDien", "CCCD", "Email", "GioiTinh", "HoTen", "MatKhau", "NoiO", "PhanQuyen", "Sdt", "TrangThai" },
                values: new object[] { 1, "default-avatar.jpg", "123456789", "admin@admin.com", 1, "Admin", "admin1", "Ha Noi", 0, "0123456789", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_BinhLuanKhachSan_IdKhachSan",
                table: "BinhLuanKhachSan",
                column: "IdKhachSan");

            migrationBuilder.CreateIndex(
                name: "IX_BinhLuanKhachSan_IdNguoiDung",
                table: "BinhLuanKhachSan",
                column: "IdNguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_BinhLuanNhaHang_IdNguoiDung",
                table: "BinhLuanNhaHang",
                column: "IdNguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_BinhLuanNhaHang_IdNhaHang",
                table: "BinhLuanNhaHang",
                column: "IdNhaHang");

            migrationBuilder.CreateIndex(
                name: "IX_BinhLuanVanChuyen_IdNguoiDung",
                table: "BinhLuanVanChuyen",
                column: "IdNguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_BinhLuanVanChuyen_IdVanChuyen",
                table: "BinhLuanVanChuyen",
                column: "IdVanChuyen");

            migrationBuilder.CreateIndex(
                name: "IX_DatKhachSans_IdKhachSan",
                table: "DatKhachSans",
                column: "IdKhachSan");

            migrationBuilder.CreateIndex(
                name: "IX_DatKhachSans_IdNguoiDung",
                table: "DatKhachSans",
                column: "IdNguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_DatNhaHangs_IdNguoiDung",
                table: "DatNhaHangs",
                column: "IdNguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_DatNhaHangs_IdNhaHang",
                table: "DatNhaHangs",
                column: "IdNhaHang");

            migrationBuilder.CreateIndex(
                name: "IX_DatTours_IdNguoiDung",
                table: "DatTours",
                column: "IdNguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_DatTours_IdTour",
                table: "DatTours",
                column: "IdTour");

            migrationBuilder.CreateIndex(
                name: "IX_DatVanChuyens_IdNguoiDung",
                table: "DatVanChuyens",
                column: "IdNguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_DatVanChuyens_IdVanChuyen",
                table: "DatVanChuyens",
                column: "IdVanChuyen");

            migrationBuilder.CreateIndex(
                name: "IX_DiemThamQuanCTs_MaDichVu",
                table: "DiemThamQuanCTs",
                column: "MaDichVu",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiemThamQuans_ChuDichVu",
                table: "DiemThamQuans",
                column: "ChuDichVu");

            migrationBuilder.CreateIndex(
                name: "IX_KhachSans_ChuDichVu",
                table: "KhachSans",
                column: "ChuDichVu");

            migrationBuilder.CreateIndex(
                name: "IX_NhaHangs_ChuDichVu",
                table: "NhaHangs",
                column: "ChuDichVu");

            migrationBuilder.CreateIndex(
                name: "IX_TourCTs_MaTour",
                table: "TourCTs",
                column: "MaTour",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tours_ChuTour",
                table: "Tours",
                column: "ChuTour");

            migrationBuilder.CreateIndex(
                name: "IX_VanChuyens_ChuDichVu",
                table: "VanChuyens",
                column: "ChuDichVu");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BinhLuanKhachSan");

            migrationBuilder.DropTable(
                name: "BinhLuanNhaHang");

            migrationBuilder.DropTable(
                name: "BinhLuanVanChuyen");

            migrationBuilder.DropTable(
                name: "DatKhachSans");

            migrationBuilder.DropTable(
                name: "DatNhaHangs");

            migrationBuilder.DropTable(
                name: "DatTours");

            migrationBuilder.DropTable(
                name: "DatVanChuyens");

            migrationBuilder.DropTable(
                name: "DiemThamQuanCTs");

            migrationBuilder.DropTable(
                name: "TourCTs");

            migrationBuilder.DropTable(
                name: "KhachSans");

            migrationBuilder.DropTable(
                name: "NhaHangs");

            migrationBuilder.DropTable(
                name: "VanChuyens");

            migrationBuilder.DropTable(
                name: "DiemThamQuans");

            migrationBuilder.DropTable(
                name: "Tours");

            migrationBuilder.DropTable(
                name: "NguoiDungs");
        }
    }
}
