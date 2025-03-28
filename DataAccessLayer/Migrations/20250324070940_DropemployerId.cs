using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class DropemployerId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Employees_EmployeeId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_EmployeeId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Jobs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "Jobs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_EmployeeId",
                table: "Jobs",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Employees_EmployeeId",
                table: "Jobs",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
