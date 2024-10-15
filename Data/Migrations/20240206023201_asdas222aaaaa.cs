using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Otello.Data.Migrations
{
    public partial class asdas222aaaaa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "ProductVariations");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "ProductVariations");

            migrationBuilder.AddColumn<int>(
                name: "colorId",
                table: "ProductVariations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "sizeId",
                table: "ProductVariations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "colorId",
                table: "ProductImages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "colorId",
                table: "ProductVariations");

            migrationBuilder.DropColumn(
                name: "sizeId",
                table: "ProductVariations");

            migrationBuilder.DropColumn(
                name: "colorId",
                table: "ProductImages");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "ProductVariations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "ProductVariations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
