using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Otello.Data.Migrations
{
    public partial class asdas222asba : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Product");

            migrationBuilder.AddColumn<int>(
                name: "colorId",
                table: "Product",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "colorId",
                table: "Product");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
