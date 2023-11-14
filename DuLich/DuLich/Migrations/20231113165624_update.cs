using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DuLich.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DanhSachDichVu",
                table: "NguoiDungs",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DanhSachDichVu",
                table: "NguoiDungs");
        }
    }
}
