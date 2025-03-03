using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class TablesUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Budget",
                table: "Jobs",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "JobCategories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "JobId",
                table: "JobCategories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SkillId",
                table: "JobCategories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SkillsId",
                table: "JobCategories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_JobCategories_CategoryId",
                table: "JobCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_JobCategories_JobId",
                table: "JobCategories",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobCategories_SkillsId",
                table: "JobCategories",
                column: "SkillsId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobCategories_Categories_CategoryId",
                table: "JobCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobCategories_Jobs_JobId",
                table: "JobCategories",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobCategories_Skills_SkillsId",
                table: "JobCategories",
                column: "SkillsId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobCategories_Categories_CategoryId",
                table: "JobCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_JobCategories_Jobs_JobId",
                table: "JobCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_JobCategories_Skills_SkillsId",
                table: "JobCategories");

            migrationBuilder.DropIndex(
                name: "IX_JobCategories_CategoryId",
                table: "JobCategories");

            migrationBuilder.DropIndex(
                name: "IX_JobCategories_JobId",
                table: "JobCategories");

            migrationBuilder.DropIndex(
                name: "IX_JobCategories_SkillsId",
                table: "JobCategories");

            migrationBuilder.DropColumn(
                name: "Budget",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "JobCategories");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "JobCategories");

            migrationBuilder.DropColumn(
                name: "SkillId",
                table: "JobCategories");

            migrationBuilder.DropColumn(
                name: "SkillsId",
                table: "JobCategories");
        }
    }
}
