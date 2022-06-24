using Microsoft.EntityFrameworkCore.Migrations;

namespace App3.Migrations
{
    public partial class EmployeeDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Project",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReportingManager",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Project",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ReportingManager",
                table: "Employees");
        }
    }
}
