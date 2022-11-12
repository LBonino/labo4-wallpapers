using Microsoft.EntityFrameworkCore.Migrations;

namespace Wallpapers.Data.Migrations
{
    public partial class ImageSizeAsLong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "SizeInBytes",
                table: "Images",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SizeInBytes",
                table: "Images",
                type: "int",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
