using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class SkillIdAddedInJobCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SkillId",
                table: "JobCategories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_JobCategories_SkillId",
                table: "JobCategories",
                column: "SkillId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobCategories_Skill_SkillId",
                table: "JobCategories",
                column: "SkillId",
                principalTable: "Skill",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobCategories_Skill_SkillId",
                table: "JobCategories");

            migrationBuilder.DropIndex(
                name: "IX_JobCategories_SkillId",
                table: "JobCategories");

            migrationBuilder.DropColumn(
                name: "SkillId",
                table: "JobCategories");
        }
    }
}
