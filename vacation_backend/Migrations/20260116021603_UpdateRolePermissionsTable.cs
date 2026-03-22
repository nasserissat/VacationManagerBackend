using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vacation_backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRolePermissionsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeExtraBenefitDay_Employees_EmployeeId",
                table: "EmployeeExtraBenefitDay");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeExtraBenefitDay_ExtraBenefitDays_ExtraBenefitDayId",
                table: "EmployeeExtraBenefitDay");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Permissions_PermissionsId",
                table: "RolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Roles_RolesId",
                table: "RolePermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeExtraBenefitDay",
                table: "EmployeeExtraBenefitDay");

            migrationBuilder.DropColumn(
                name: "EmployeeExtraBenefitDayId",
                table: "Employees");

            migrationBuilder.RenameTable(
                name: "EmployeeExtraBenefitDay",
                newName: "EmployeeExtraBenefitDays");

            migrationBuilder.RenameColumn(
                name: "RolesId",
                table: "RolePermissions",
                newName: "PermissionId");

            migrationBuilder.RenameColumn(
                name: "PermissionsId",
                table: "RolePermissions",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_RolePermissions_RolesId",
                table: "RolePermissions",
                newName: "IX_RolePermissions_PermissionId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeExtraBenefitDay_ExtraBenefitDayId",
                table: "EmployeeExtraBenefitDays",
                newName: "IX_EmployeeExtraBenefitDays_ExtraBenefitDayId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeExtraBenefitDay_EmployeeId_ExtraBenefitDayId_Year",
                table: "EmployeeExtraBenefitDays",
                newName: "IX_EmployeeExtraBenefitDays_EmployeeId_ExtraBenefitDayId_Year");

            migrationBuilder.AddColumn<DateTime>(
                name: "GrantedAt",
                table: "RolePermissions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "GrantedByUserId",
                table: "RolePermissions",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Year",
                table: "Holidays",
                type: "int",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeCode",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeExtraBenefitDays",
                table: "EmployeeExtraBenefitDays",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeExtraBenefitDays_Employees_EmployeeId",
                table: "EmployeeExtraBenefitDays",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeExtraBenefitDays_ExtraBenefitDays_ExtraBenefitDayId",
                table: "EmployeeExtraBenefitDays",
                column: "ExtraBenefitDayId",
                principalTable: "ExtraBenefitDays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_Permissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_Roles_RoleId",
                table: "RolePermissions",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeExtraBenefitDays_Employees_EmployeeId",
                table: "EmployeeExtraBenefitDays");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeExtraBenefitDays_ExtraBenefitDays_ExtraBenefitDayId",
                table: "EmployeeExtraBenefitDays");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Permissions_PermissionId",
                table: "RolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Roles_RoleId",
                table: "RolePermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeExtraBenefitDays",
                table: "EmployeeExtraBenefitDays");

            migrationBuilder.DropColumn(
                name: "GrantedAt",
                table: "RolePermissions");

            migrationBuilder.DropColumn(
                name: "GrantedByUserId",
                table: "RolePermissions");

            migrationBuilder.DropColumn(
                name: "EmployeeCode",
                table: "Employees");

            migrationBuilder.RenameTable(
                name: "EmployeeExtraBenefitDays",
                newName: "EmployeeExtraBenefitDay");

            migrationBuilder.RenameColumn(
                name: "PermissionId",
                table: "RolePermissions",
                newName: "RolesId");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "RolePermissions",
                newName: "PermissionsId");

            migrationBuilder.RenameIndex(
                name: "IX_RolePermissions_PermissionId",
                table: "RolePermissions",
                newName: "IX_RolePermissions_RolesId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeExtraBenefitDays_ExtraBenefitDayId",
                table: "EmployeeExtraBenefitDay",
                newName: "IX_EmployeeExtraBenefitDay_ExtraBenefitDayId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeExtraBenefitDays_EmployeeId_ExtraBenefitDayId_Year",
                table: "EmployeeExtraBenefitDay",
                newName: "IX_EmployeeExtraBenefitDay_EmployeeId_ExtraBenefitDayId_Year");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Year",
                table: "Holidays",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeExtraBenefitDayId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeExtraBenefitDay",
                table: "EmployeeExtraBenefitDay",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeExtraBenefitDay_Employees_EmployeeId",
                table: "EmployeeExtraBenefitDay",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeExtraBenefitDay_ExtraBenefitDays_ExtraBenefitDayId",
                table: "EmployeeExtraBenefitDay",
                column: "ExtraBenefitDayId",
                principalTable: "ExtraBenefitDays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_Permissions_PermissionsId",
                table: "RolePermissions",
                column: "PermissionsId",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_Roles_RolesId",
                table: "RolePermissions",
                column: "RolesId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
