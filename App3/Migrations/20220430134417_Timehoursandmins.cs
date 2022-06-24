using Microsoft.EntityFrameworkCore.Migrations;

namespace App3.Migrations
{
    public partial class Timehoursandmins : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ExtraMin",
                table: "BioMetricInfoDb",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "RegularMin",
                table: "BioMetricInfoDb",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExtraMin",
                table: "BioMetricInfoDb");

            migrationBuilder.DropColumn(
                name: "RegularMin",
                table: "BioMetricInfoDb");
        }
    }
}
