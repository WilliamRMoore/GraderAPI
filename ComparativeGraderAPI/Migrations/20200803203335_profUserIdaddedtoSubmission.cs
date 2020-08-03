using Microsoft.EntityFrameworkCore.Migrations;

namespace ComparativeGraderAPI.Migrations
{
    public partial class profUserIdaddedtoSubmission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProfessorUserId",
                table: "Submissions",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfessorUserId",
                table: "Submissions");
        }
    }
}
