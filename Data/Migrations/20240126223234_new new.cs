using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Otello.Data.Migrations
{
    public partial class newnew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParentModel_Product_ProductModelId",
                table: "ParentModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ParentModel_ProductCategories_ProductCategoriesModelId",
                table: "ParentModel");

            migrationBuilder.RenameColumn(
                name: "ProductModelId",
                table: "ParentModel",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "ProductCategoriesModelId",
                table: "ParentModel",
                newName: "ProductCategoriesId");

            migrationBuilder.RenameIndex(
                name: "IX_ParentModel_ProductModelId",
                table: "ParentModel",
                newName: "IX_ParentModel_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ParentModel_ProductCategoriesModelId",
                table: "ParentModel",
                newName: "IX_ParentModel_ProductCategoriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParentModel_Product_ProductId",
                table: "ParentModel",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParentModel_ProductCategories_ProductCategoriesId",
                table: "ParentModel",
                column: "ProductCategoriesId",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParentModel_Product_ProductId",
                table: "ParentModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ParentModel_ProductCategories_ProductCategoriesId",
                table: "ParentModel");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ParentModel",
                newName: "ProductModelId");

            migrationBuilder.RenameColumn(
                name: "ProductCategoriesId",
                table: "ParentModel",
                newName: "ProductCategoriesModelId");

            migrationBuilder.RenameIndex(
                name: "IX_ParentModel_ProductId",
                table: "ParentModel",
                newName: "IX_ParentModel_ProductModelId");

            migrationBuilder.RenameIndex(
                name: "IX_ParentModel_ProductCategoriesId",
                table: "ParentModel",
                newName: "IX_ParentModel_ProductCategoriesModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParentModel_Product_ProductModelId",
                table: "ParentModel",
                column: "ProductModelId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParentModel_ProductCategories_ProductCategoriesModelId",
                table: "ParentModel",
                column: "ProductCategoriesModelId",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
