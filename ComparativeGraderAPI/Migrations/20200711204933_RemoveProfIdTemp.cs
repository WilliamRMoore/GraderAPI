using Microsoft.EntityFrameworkCore.Migrations;

namespace ComparativeGraderAPI.Migrations
{
    public partial class RemoveProfIdTemp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfessorUserId",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "ProfessorUserId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "ProfessorUserId",
                table: "Assignments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfessorUserId",
                table: "Semesters",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfessorUserId",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfessorUserId",
                table: "Assignments",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
