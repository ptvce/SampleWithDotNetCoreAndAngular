using Microsoft.EntityFrameworkCore.Migrations;

namespace SampleWithDotNetCoreAndAngular.Migrations
{
    public partial class addcategoryIdincustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Customers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CategoryId",
                table: "Customers",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Categories_CategoryId",
                table: "Customers",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Categories_CategoryId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_CategoryId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Customers");
        }
    }
}
