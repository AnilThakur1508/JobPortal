using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeTablenewFiledIsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResumeId",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "WorkExperience",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_ExperienceLevelId",
                table: "Jobs",
                column: "ExperienceLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_ExperienceLevels_ExperienceLevelId",
                table: "Jobs",
                column: "ExperienceLevelId",
                principalTable: "ExperienceLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_ExperienceLevels_ExperienceLevelId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_ExperienceLevelId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "WorkExperience",
                table: "Employees");

            migrationBuilder.AddColumn<Guid>(
                name: "ResumeId",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
