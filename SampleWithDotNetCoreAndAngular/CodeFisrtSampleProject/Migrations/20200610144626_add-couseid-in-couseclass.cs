using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeFisrtSampleProject.Migrations
{
    public partial class addcouseidincouseclass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "CourseClasses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CourseClasses_CourseId",
                table: "CourseClasses",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseClasses_Courses_CourseId",
                table: "CourseClasses",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseClasses_Courses_CourseId",
                table: "CourseClasses");

            migrationBuilder.DropIndex(
                name: "IX_CourseClasses_CourseId",
                table: "CourseClasses");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "CourseClasses");
        }
    }
}
