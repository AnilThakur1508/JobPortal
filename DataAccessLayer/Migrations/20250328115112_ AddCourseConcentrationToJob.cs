using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseConcentrationToJob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CourseConcentration",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_JobCourses_JobId",
                table: "JobCourses",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobCourses_Jobs_JobId",
                table: "JobCourses",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobCourses_Jobs_JobId",
                table: "JobCourses");

            migrationBuilder.DropIndex(
                name: "IX_JobCourses_JobId",
                table: "JobCourses");

            migrationBuilder.DropColumn(
                name: "CourseConcentration",
                table: "Jobs");
        }
    }
}
