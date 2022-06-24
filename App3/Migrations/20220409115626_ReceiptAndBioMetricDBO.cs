using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App3.Migrations
{
    public partial class ReceiptAndBioMetricDBO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BioMetricInfoDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeOut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegularHours = table.Column<double>(type: "float", nullable: false),
                    ExtraHours = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Issues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Project = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BioMetricInfoDb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptDb",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceiptID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    AmountSource = table.Column<int>(type: "int", nullable: false),
                    AmountReceivedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceivedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReceiptFilePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptDb", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptDb_ReceiptID",
                table: "ReceiptDb",
                column: "ReceiptID",
                unique: true,
                filter: "[ReceiptID] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BioMetricInfoDb");

            migrationBuilder.DropTable(
                name: "ReceiptDb");
        }
    }
}
