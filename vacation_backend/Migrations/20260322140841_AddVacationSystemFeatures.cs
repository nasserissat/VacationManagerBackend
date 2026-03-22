using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vacation_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddVacationSystemFeatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubstituteEmployeeId",
                table: "VacationRequests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CompanyPolicies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorksOnSaturdays = table.Column<bool>(type: "bit", nullable: false),
                    WorksOnSundays = table.Column<bool>(type: "bit", nullable: false),
                    DailyWorkHours = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyPolicies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VacationBalanceLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    DaysChanged = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VacationRequestId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacationBalanceLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VacationBalanceLogs_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VacationBalanceLogs_VacationRequests_VacationRequestId",
                        column: x => x.VacationRequestId,
                        principalTable: "VacationRequests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VacationRequestActions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VacationRequestId = table.Column<int>(type: "int", nullable: false),
                    ActionByUserId = table.Column<int>(type: "int", nullable: false),
                    ActionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacationRequestActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VacationRequestActions_Users_ActionByUserId",
                        column: x => x.ActionByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VacationRequestActions_VacationRequests_VacationRequestId",
                        column: x => x.VacationRequestId,
                        principalTable: "VacationRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VacationRequestAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VacationRequestId = table.Column<int>(type: "int", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacationRequestAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VacationRequestAttachments_VacationRequests_VacationRequestId",
                        column: x => x.VacationRequestId,
                        principalTable: "VacationRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VacationRequests_SubstituteEmployeeId",
                table: "VacationRequests",
                column: "SubstituteEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_VacationBalanceLogs_EmployeeId",
                table: "VacationBalanceLogs",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_VacationBalanceLogs_VacationRequestId",
                table: "VacationBalanceLogs",
                column: "VacationRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_VacationRequestActions_ActionByUserId",
                table: "VacationRequestActions",
                column: "ActionByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_VacationRequestActions_VacationRequestId",
                table: "VacationRequestActions",
                column: "VacationRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_VacationRequestAttachments_VacationRequestId",
                table: "VacationRequestAttachments",
                column: "VacationRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_VacationRequests_Employees_SubstituteEmployeeId",
                table: "VacationRequests",
                column: "SubstituteEmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VacationRequests_Employees_SubstituteEmployeeId",
                table: "VacationRequests");

            migrationBuilder.DropTable(
                name: "CompanyPolicies");

            migrationBuilder.DropTable(
                name: "VacationBalanceLogs");

            migrationBuilder.DropTable(
                name: "VacationRequestActions");

            migrationBuilder.DropTable(
                name: "VacationRequestAttachments");

            migrationBuilder.DropIndex(
                name: "IX_VacationRequests_SubstituteEmployeeId",
                table: "VacationRequests");

            migrationBuilder.DropColumn(
                name: "SubstituteEmployeeId",
                table: "VacationRequests");
        }
    }
}
