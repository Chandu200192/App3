using Microsoft.EntityFrameworkCore.Migrations;

namespace App3.Migrations
{
    public partial class OTInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "BioMetricInfoDb",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "OTHours",
                table: "BioMetricInfoDb",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "OTMin",
                table: "BioMetricInfoDb",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "approvalType",
                table: "BioMetricInfoDb",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "BioMetricInfoDb");

            migrationBuilder.DropColumn(
                name: "OTHours",
                table: "BioMetricInfoDb");

            migrationBuilder.DropColumn(
                name: "OTMin",
                table: "BioMetricInfoDb");

            migrationBuilder.DropColumn(
                name: "approvalType",
                table: "BioMetricInfoDb");
        }
    }
}
