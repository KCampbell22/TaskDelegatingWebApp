using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskDelegatingWebApp.Migrations
{
    /// <inheritdoc />
    public partial class WeekToTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WeekName",
                table: "Week",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WeekId",
                table: "TaskItem",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WeekName",
                table: "TaskItem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskItem_WeekId",
                table: "TaskItem",
                column: "WeekId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItem_Week_WeekId",
                table: "TaskItem",
                column: "WeekId",
                principalTable: "Week",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskItem_Week_WeekId",
                table: "TaskItem");

            migrationBuilder.DropIndex(
                name: "IX_TaskItem_WeekId",
                table: "TaskItem");

            migrationBuilder.DropColumn(
                name: "WeekName",
                table: "Week");

            migrationBuilder.DropColumn(
                name: "WeekId",
                table: "TaskItem");

            migrationBuilder.DropColumn(
                name: "WeekName",
                table: "TaskItem");
        }
    }
}
