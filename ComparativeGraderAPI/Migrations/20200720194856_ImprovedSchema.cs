using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ComparativeGraderAPI.Migrations
{
    public partial class ImprovedSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Assignments");

            migrationBuilder.AddColumn<string>(
                name: "Season",
                table: "Semesters",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CourseDescription",
                table: "Courses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CourseName",
                table: "Courses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfessorUserId",
                table: "Courses",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Courses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "Assignments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Season",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "CourseDescription",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CourseName",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "ProfessorUserId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Assignments");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Semesters",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Semesters",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Assignments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Assignments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
