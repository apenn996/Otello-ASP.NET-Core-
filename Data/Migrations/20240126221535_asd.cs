using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Otello.Data.Migrations
{
    public partial class asd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParentModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductModelId = table.Column<int>(type: "int", nullable: false),
                    ProductCategoriesModelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParentModel_Product_ProductModelId",
                        column: x => x.ProductModelId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParentModel_ProductCategories_ProductCategoriesModelId",
                        column: x => x.ProductCategoriesModelId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParentModel_ProductCategoriesModelId",
                table: "ParentModel",
                column: "ProductCategoriesModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ParentModel_ProductModelId",
                table: "ParentModel",
                column: "ProductModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParentModel");
        }
    }
}
