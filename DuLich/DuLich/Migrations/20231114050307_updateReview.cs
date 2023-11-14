using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DuLich.Migrations
{
    public partial class updateReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BinhLuanKhachSan_KhachSans_IdKhachSan",
                table: "BinhLuanKhachSan");

            migrationBuilder.DropForeignKey(
                name: "FK_BinhLuanKhachSan_NguoiDungs_IdNguoiDung",
                table: "BinhLuanKhachSan");

            migrationBuilder.DropForeignKey(
                name: "FK_BinhLuanNhaHang_NguoiDungs_IdNguoiDung",
                table: "BinhLuanNhaHang");

            migrationBuilder.DropForeignKey(
                name: "FK_BinhLuanNhaHang_NhaHangs_IdNhaHang",
                table: "BinhLuanNhaHang");

            migrationBuilder.DropForeignKey(
                name: "FK_BinhLuanVanChuyen_NguoiDungs_IdNguoiDung",
                table: "BinhLuanVanChuyen");

            migrationBuilder.DropForeignKey(
                name: "FK_BinhLuanVanChuyen_VanChuyens_IdVanChuyen",
                table: "BinhLuanVanChuyen");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BinhLuanVanChuyen",
                table: "BinhLuanVanChuyen");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BinhLuanNhaHang",
                table: "BinhLuanNhaHang");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BinhLuanKhachSan",
                table: "BinhLuanKhachSan");

            migrationBuilder.RenameTable(
                name: "BinhLuanVanChuyen",
                newName: "BinhLuanVanChuyens");

            migrationBuilder.RenameTable(
                name: "BinhLuanNhaHang",
                newName: "BinhLuanNhaHangs");

            migrationBuilder.RenameTable(
                name: "BinhLuanKhachSan",
                newName: "BinhLuanKhachSans");

            migrationBuilder.RenameIndex(
                name: "IX_BinhLuanVanChuyen_IdVanChuyen",
                table: "BinhLuanVanChuyens",
                newName: "IX_BinhLuanVanChuyens_IdVanChuyen");

            migrationBuilder.RenameIndex(
                name: "IX_BinhLuanVanChuyen_IdNguoiDung",
                table: "BinhLuanVanChuyens",
                newName: "IX_BinhLuanVanChuyens_IdNguoiDung");

            migrationBuilder.RenameIndex(
                name: "IX_BinhLuanNhaHang_IdNhaHang",
                table: "BinhLuanNhaHangs",
                newName: "IX_BinhLuanNhaHangs_IdNhaHang");

            migrationBuilder.RenameIndex(
                name: "IX_BinhLuanNhaHang_IdNguoiDung",
                table: "BinhLuanNhaHangs",
                newName: "IX_BinhLuanNhaHangs_IdNguoiDung");

            migrationBuilder.RenameIndex(
                name: "IX_BinhLuanKhachSan_IdNguoiDung",
                table: "BinhLuanKhachSans",
                newName: "IX_BinhLuanKhachSans_IdNguoiDung");

            migrationBuilder.RenameIndex(
                name: "IX_BinhLuanKhachSan_IdKhachSan",
                table: "BinhLuanKhachSans",
                newName: "IX_BinhLuanKhachSans_IdKhachSan");

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "BinhLuanVanChuyens",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Reply",
                table: "BinhLuanVanChuyens",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "BinhLuanNhaHangs",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Reply",
                table: "BinhLuanNhaHangs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "BinhLuanKhachSans",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Reply",
                table: "BinhLuanKhachSans",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BinhLuanVanChuyens",
                table: "BinhLuanVanChuyens",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BinhLuanNhaHangs",
                table: "BinhLuanNhaHangs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BinhLuanKhachSans",
                table: "BinhLuanKhachSans",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "BinhLuanDiemThamQuans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDiemThamQuan = table.Column<int>(type: "int", nullable: true),
                    IdNguoiDung = table.Column<int>(type: "int", nullable: true),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThoiGian = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    Reply = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BinhLuanDiemThamQuans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BinhLuanDiemThamQuans_DiemThamQuans_IdDiemThamQuan",
                        column: x => x.IdDiemThamQuan,
                        principalTable: "DiemThamQuans",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BinhLuanDiemThamQuans_NguoiDungs_IdNguoiDung",
                        column: x => x.IdNguoiDung,
                        principalTable: "NguoiDungs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BinhLuanTours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTour = table.Column<int>(type: "int", nullable: true),
                    IdNguoiDung = table.Column<int>(type: "int", nullable: true),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThoiGian = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    Reply = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BinhLuanTours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BinhLuanTours_NguoiDungs_IdNguoiDung",
                        column: x => x.IdNguoiDung,
                        principalTable: "NguoiDungs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BinhLuanTours_Tours_IdTour",
                        column: x => x.IdTour,
                        principalTable: "Tours",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BinhLuanDiemThamQuans_IdDiemThamQuan",
                table: "BinhLuanDiemThamQuans",
                column: "IdDiemThamQuan");

            migrationBuilder.CreateIndex(
                name: "IX_BinhLuanDiemThamQuans_IdNguoiDung",
                table: "BinhLuanDiemThamQuans",
                column: "IdNguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_BinhLuanTours_IdNguoiDung",
                table: "BinhLuanTours",
                column: "IdNguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_BinhLuanTours_IdTour",
                table: "BinhLuanTours",
                column: "IdTour");

            migrationBuilder.AddForeignKey(
                name: "FK_BinhLuanKhachSans_KhachSans_IdKhachSan",
                table: "BinhLuanKhachSans",
                column: "IdKhachSan",
                principalTable: "KhachSans",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BinhLuanKhachSans_NguoiDungs_IdNguoiDung",
                table: "BinhLuanKhachSans",
                column: "IdNguoiDung",
                principalTable: "NguoiDungs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BinhLuanNhaHangs_NguoiDungs_IdNguoiDung",
                table: "BinhLuanNhaHangs",
                column: "IdNguoiDung",
                principalTable: "NguoiDungs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BinhLuanNhaHangs_NhaHangs_IdNhaHang",
                table: "BinhLuanNhaHangs",
                column: "IdNhaHang",
                principalTable: "NhaHangs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BinhLuanVanChuyens_NguoiDungs_IdNguoiDung",
                table: "BinhLuanVanChuyens",
                column: "IdNguoiDung",
                principalTable: "NguoiDungs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BinhLuanVanChuyens_VanChuyens_IdVanChuyen",
                table: "BinhLuanVanChuyens",
                column: "IdVanChuyen",
                principalTable: "VanChuyens",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BinhLuanKhachSans_KhachSans_IdKhachSan",
                table: "BinhLuanKhachSans");

            migrationBuilder.DropForeignKey(
                name: "FK_BinhLuanKhachSans_NguoiDungs_IdNguoiDung",
                table: "BinhLuanKhachSans");

            migrationBuilder.DropForeignKey(
                name: "FK_BinhLuanNhaHangs_NguoiDungs_IdNguoiDung",
                table: "BinhLuanNhaHangs");

            migrationBuilder.DropForeignKey(
                name: "FK_BinhLuanNhaHangs_NhaHangs_IdNhaHang",
                table: "BinhLuanNhaHangs");

            migrationBuilder.DropForeignKey(
                name: "FK_BinhLuanVanChuyens_NguoiDungs_IdNguoiDung",
                table: "BinhLuanVanChuyens");

            migrationBuilder.DropForeignKey(
                name: "FK_BinhLuanVanChuyens_VanChuyens_IdVanChuyen",
                table: "BinhLuanVanChuyens");

            migrationBuilder.DropTable(
                name: "BinhLuanDiemThamQuans");

            migrationBuilder.DropTable(
                name: "BinhLuanTours");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BinhLuanVanChuyens",
                table: "BinhLuanVanChuyens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BinhLuanNhaHangs",
                table: "BinhLuanNhaHangs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BinhLuanKhachSans",
                table: "BinhLuanKhachSans");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "BinhLuanVanChuyens");

            migrationBuilder.DropColumn(
                name: "Reply",
                table: "BinhLuanVanChuyens");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "BinhLuanNhaHangs");

            migrationBuilder.DropColumn(
                name: "Reply",
                table: "BinhLuanNhaHangs");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "BinhLuanKhachSans");

            migrationBuilder.DropColumn(
                name: "Reply",
                table: "BinhLuanKhachSans");

            migrationBuilder.RenameTable(
                name: "BinhLuanVanChuyens",
                newName: "BinhLuanVanChuyen");

            migrationBuilder.RenameTable(
                name: "BinhLuanNhaHangs",
                newName: "BinhLuanNhaHang");

            migrationBuilder.RenameTable(
                name: "BinhLuanKhachSans",
                newName: "BinhLuanKhachSan");

            migrationBuilder.RenameIndex(
                name: "IX_BinhLuanVanChuyens_IdVanChuyen",
                table: "BinhLuanVanChuyen",
                newName: "IX_BinhLuanVanChuyen_IdVanChuyen");

            migrationBuilder.RenameIndex(
                name: "IX_BinhLuanVanChuyens_IdNguoiDung",
                table: "BinhLuanVanChuyen",
                newName: "IX_BinhLuanVanChuyen_IdNguoiDung");

            migrationBuilder.RenameIndex(
                name: "IX_BinhLuanNhaHangs_IdNhaHang",
                table: "BinhLuanNhaHang",
                newName: "IX_BinhLuanNhaHang_IdNhaHang");

            migrationBuilder.RenameIndex(
                name: "IX_BinhLuanNhaHangs_IdNguoiDung",
                table: "BinhLuanNhaHang",
                newName: "IX_BinhLuanNhaHang_IdNguoiDung");

            migrationBuilder.RenameIndex(
                name: "IX_BinhLuanKhachSans_IdNguoiDung",
                table: "BinhLuanKhachSan",
                newName: "IX_BinhLuanKhachSan_IdNguoiDung");

            migrationBuilder.RenameIndex(
                name: "IX_BinhLuanKhachSans_IdKhachSan",
                table: "BinhLuanKhachSan",
                newName: "IX_BinhLuanKhachSan_IdKhachSan");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BinhLuanVanChuyen",
                table: "BinhLuanVanChuyen",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BinhLuanNhaHang",
                table: "BinhLuanNhaHang",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BinhLuanKhachSan",
                table: "BinhLuanKhachSan",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BinhLuanKhachSan_KhachSans_IdKhachSan",
                table: "BinhLuanKhachSan",
                column: "IdKhachSan",
                principalTable: "KhachSans",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BinhLuanKhachSan_NguoiDungs_IdNguoiDung",
                table: "BinhLuanKhachSan",
                column: "IdNguoiDung",
                principalTable: "NguoiDungs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BinhLuanNhaHang_NguoiDungs_IdNguoiDung",
                table: "BinhLuanNhaHang",
                column: "IdNguoiDung",
                principalTable: "NguoiDungs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BinhLuanNhaHang_NhaHangs_IdNhaHang",
                table: "BinhLuanNhaHang",
                column: "IdNhaHang",
                principalTable: "NhaHangs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BinhLuanVanChuyen_NguoiDungs_IdNguoiDung",
                table: "BinhLuanVanChuyen",
                column: "IdNguoiDung",
                principalTable: "NguoiDungs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BinhLuanVanChuyen_VanChuyens_IdVanChuyen",
                table: "BinhLuanVanChuyen",
                column: "IdVanChuyen",
                principalTable: "VanChuyens",
                principalColumn: "Id");
        }
    }
}
