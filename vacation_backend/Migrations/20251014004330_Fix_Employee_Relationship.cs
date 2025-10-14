using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vacation_backend.Migrations
{
    /// <inheritdoc />
    public partial class Fix_Employee_Relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExtraBenefitDays_Employees_EmployeeId",
                table: "ExtraBenefitDays");

            migrationBuilder.DropIndex(
                name: "IX_ExtraBenefitDays_EmployeeId",
                table: "ExtraBenefitDays");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "ExtraBenefitDays");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeExtraBenefitDayId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmployeeExtraBenefitDay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    ExtraBenefitDayId = table.Column<int>(type: "int", nullable: false),
                    UsedDays = table.Column<int>(type: "int", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConsumedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeExtraBenefitDay", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeExtraBenefitDay_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeExtraBenefitDay_ExtraBenefitDays_ExtraBenefitDayId",
                        column: x => x.ExtraBenefitDayId,
                        principalTable: "ExtraBenefitDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeExtraBenefitDay_EmployeeId_ExtraBenefitDayId_Year",
                table: "EmployeeExtraBenefitDay",
                columns: new[] { "EmployeeId", "ExtraBenefitDayId", "Year" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeExtraBenefitDay_ExtraBenefitDayId",
                table: "EmployeeExtraBenefitDay",
                column: "ExtraBenefitDayId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeExtraBenefitDay");

            migrationBuilder.DropColumn(
                name: "EmployeeExtraBenefitDayId",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "ExtraBenefitDays",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ExtraBenefitDays_EmployeeId",
                table: "ExtraBenefitDays",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExtraBenefitDays_Employees_EmployeeId",
                table: "ExtraBenefitDays",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
